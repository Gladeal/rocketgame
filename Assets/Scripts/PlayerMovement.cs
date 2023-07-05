using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 50f;

    [SerializeField] AudioClip engineSound;

    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles1;
    [SerializeField] ParticleSystem rightThrusterParticles1;
    [SerializeField] ParticleSystem leftThrusterParticles2;
    [SerializeField] ParticleSystem rightThrusterParticles2;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            startThrust();
        }
        else
        {
            stopThrust();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            startLeftSideThrust();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            startRightSideThrust();
        }
        else
        {
            stopSideThrust();
        }
    }

    void startThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineSound);
        }
        else if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
    }

    void stopThrust()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();
    }
    
    void startLeftSideThrust()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrusterParticles1.isPlaying && !rightThrusterParticles2.isPlaying)
        {
            rightThrusterParticles1.Play();
            rightThrusterParticles2.Play();
        }
    }

    void startRightSideThrust()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrusterParticles1.isPlaying && !leftThrusterParticles2.isPlaying)
        {
            leftThrusterParticles1.Play();
            leftThrusterParticles2.Play();
        }
    }

    private void stopSideThrust()
    {
        leftThrusterParticles1.Stop();
        leftThrusterParticles2.Stop();
        rightThrusterParticles1.Stop();
        rightThrusterParticles2.Stop();
    }

    void ApplyRotation(float RotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * RotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;    
    }
}