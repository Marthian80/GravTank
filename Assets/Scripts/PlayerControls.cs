using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Player movement settings accross screen")]
    [Tooltip("How fast ship moves accross the screen")]
    [SerializeField] float speed = 23f;
    [Tooltip("How far the ship can move to left and right side of the screen")]
    [SerializeField] float xRange = 10.5f;
    [Tooltip("How far the ship can move to top and bottom of the screen")]
    [SerializeField] float yRange = 9f;    

    [Header("Storage for player ship lasers that fire particles")]
    [Tooltip("All laser weapons prefabs on the ship should be added here")]
    [SerializeField] GameObject[] lasers;
    [SerializeField] AudioClip fireLaser;

    [Header("Settings for roll movement when player ship moves")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2.5f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    private AudioSource audioSource;

    float xThrow;
    float yPull;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
        
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();        
    }

    private void ProcessRotation()
    {
        float positionWithPitchFactor = transform.localPosition.y * positionPitchFactor;
        float controlledPitchPull = yPull * controlPitchFactor;

        float pitch = positionWithPitchFactor + controlledPitchPull;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yPull = Input.GetAxis("Vertical");

        float offsetX = xThrow * Time.deltaTime * speed;
        float rawPosX = transform.localPosition.x + offsetX;
        float clampedXPos = Mathf.Clamp(rawPosX, -xRange, xRange);

        float offsetY = yPull * Time.deltaTime * speed;
        float rawPosY = transform.localPosition.y + offsetY;
        float clampedYPos = Mathf.Clamp(rawPosY, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(fireLaser);
            }
        }
        else
        {
            audioSource.Stop();
            SetLasersActive(false);
        }
    }       

    private void SetLasersActive(bool lasersActive)
    {
        foreach(var laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = lasersActive;
        }
    }
}
