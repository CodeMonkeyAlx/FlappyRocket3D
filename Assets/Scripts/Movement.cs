 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audio;
    [SerializeField] float thrustForce = 10f;
    [SerializeField] float RotationThrust = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //Cache refference to rigid body varible.
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            if(!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
    }

    void ProcessRotation() 
    {
       if(Input.GetKey(KeyCode.A))
        {
            //rotate rocket left
            ApplyRotation(RotationThrust);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            //Rotate Rocket Right.
            ApplyRotation(-RotationThrust);
        }    
    }

    void ApplyRotation(float rotationThisFrame) 
    {
        rb.freezeRotation = true; //Freeze Rotation so we can manually control the rocket.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //Unfreeze constraints. 
    }
}
