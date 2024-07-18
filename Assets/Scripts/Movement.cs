using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 100;
    [SerializeField] float rotateThrust = 30;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoostParticle;
    [SerializeField] ParticleSystem sideLeftParticle;
    [SerializeField] ParticleSystem sideRightParticle;


    Rigidbody rb;
    AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Thrust();
        Rotation();
    }

    void Thrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThruisting();
        }

        else
        {
            StopThrusting();
        }

    }

    void Rotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThruisting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoostParticle.isPlaying)
        {
            mainBoostParticle.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainBoostParticle.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-rotateThrust);
        if (!sideLeftParticle.isPlaying)
        {
            sideLeftParticle.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotateThrust);
        if (!sideRightParticle.isPlaying)
        {
            sideRightParticle.Play();
        }
    }

    void StopRotating()
    {
        sideRightParticle.Stop();
        sideLeftParticle.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
