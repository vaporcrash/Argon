using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] float controlSpeed = 1f;
    [SerializeField] float xRange = 7f;
    [SerializeField] float yRange = 7f;
    [SerializeField] float positionPitchFactor = -1f;
    [SerializeField] float controlPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -5f;
    [SerializeField] GameObject laser;
    float xThrow,yThrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessRotation(){
        float pitchDueToPosition = transform.localPosition.y + positionPitchFactor;
        float pitchDueToControlFactor = yThrow + controlPitchFactor;
        float pitch = pitchDueToPosition * pitchDueToControlFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }
    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring(){
        if(Input.GetButton("Fire1"))
        {
            ToggleLaser(true);
        }
        else
        {
            ToggleLaser(false);
        }
    }

    void ToggleLaser(bool isActive)
    {
        var emissionModule = laser.GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = isActive;
    }
}
