using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Singleton;

    // Rigidbody for unity movement and collisions
    public Rigidbody body;
    // Base charcter movement speed
    public float movementSpeed = 75;
    // Acceeration multiplyer, this defines how much of our speed we are currently traveling
    // Between 0 - 1
    public float acceleration = 0;
    // How fast we Accelerate and Deccelerate
    public float accelerationDecomposition = 0.5f;
    public float accelerationAmmount = 0.5f;
    // Base rotationSpeed
    public float rotationSpeed = 2;

    //implementing shit
    public AnimationCurve lowGearAccelerationCurve = new AnimationCurve(new Keyframe(0, 0.2f, 0, 1.16f, 0, 0.4f), new Keyframe(0.05f, 0.3f, 0, 0, 0.41f, 0.285f), new Keyframe(1, 0, -0.063f, 0, 0.83f, 0));
    public AnimationCurve highGearAccelerationCurve = new AnimationCurve(new Keyframe(0, 0.045f, 0, 0.004f, 0, 0.31f), new Keyframe(0.4f, 0.1f, 0.23f, 0.23f, 0.3f, 0.47f), new Keyframe(0.64f, 0.18f, -0.002f, -0.002f, 0.6f, 0.35f), new Keyframe(1, 0.05f, -7.343f, 0, 0.023f, 0));

    public bool IsHighGear = false;
    public bool IsAlive = true;

    private float crashTimeOutTimer = 0f;
    public float crashTimer = 2f;

    public AudioSource explosionSound;

    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        explosionSound = GetComponent<AudioSource>();
    }

    public void Update()
    {

        if(crashTimeOutTimer > 0f && !IsAlive)
        {
            crashTimeOutTimer -= Time.deltaTime;
        }
        else if(crashTimeOutTimer <= 0f && !IsAlive)
        {
            IsAlive = true;
            acceleration = 0;
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            IsHighGear = false;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            IsHighGear = true;
        }

        // if our acceleration is greater than zero then we want to start decelerating
        if (Input.GetKey(KeyCode.Space) && IsAlive)
        {
            acceleration += ((accelerationAmmount)* Time.deltaTime) * ((IsHighGear == true) ? highGearAccelerationCurve.Evaluate(acceleration) : lowGearAccelerationCurve.Evaluate(acceleration)) + accelerationDecomposition * Time.deltaTime;
            if (acceleration > 1f) acceleration = 1f;
        }

        if (acceleration > 0)
        {
            acceleration -= accelerationDecomposition * Time.deltaTime;
        }

        // if our acceleration drops below zero some how we want to clamp it
        if(acceleration < 0)
        {
            acceleration = 0;
        }

        if (acceleration > 0f)
        {
            //if the left key is pressed and we are still moving, apply rotation to the player
            if (Input.GetKey(KeyCode.LeftArrow) && IsAlive)
            {
                body.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (-rotationSpeed), transform.rotation.eulerAngles.z), movementSpeed * Time.deltaTime));
            }

            //if the right key is pressed and we are still moving, apply rotation to the player
            else if (Input.GetKey(KeyCode.RightArrow) && IsAlive)
            {
                body.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (rotationSpeed), transform.rotation.eulerAngles.z), movementSpeed * Time.deltaTime));
            }

            //  Move the player based on our acceleration and direction
            body.MovePosition(transform.position + transform.forward * (movementSpeed * acceleration) * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Crashable")
        {
            IsAlive = false;
            crashTimeOutTimer = crashTimer;

            explosionSound.Play();
        }
    }
}
