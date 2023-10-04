using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Wheels Transforms")]
    public Transform frontRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform backRightWheelTransform;
    public Transform backLeftWheelTransform;

    [Header("Wheels Colliders")]
    public WheelCollider frontRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider backLeftWheelCollider;

    [Header("Vehicle Engine")]
    public float acceleration = 110f;
    private float presentAcceleration = 0f;
    public float brakingForce = 200f;
    private float presentBraking = 0f;
    public GameObject carCamera;

    [Header("Vehicle Drive")]
    public PlayerMoverment player;
    public float radiusGoCar = 3f;
    public Transform DoorCar;
    private bool isOpened = false;


    [Header("Camera Driver")]
    public GameObject TPSCam;
    public GameObject AimCam;
    public GameObject PlayerCharacter;
    public GameObject AimCamCanvas;

    [Header("Vehicle Steering")]
    public float wheelsTorque = 20f;
    private float presentTurnAngle = 0f;

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radiusGoCar)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpened = true;
                radiusGoCar = 5000f;
                PlayerCharacter.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = DoorCar.transform.position;
                isOpened = false;
                radiusGoCar = 3f;
                PlayerCharacter.SetActive(true);
            }
        }

        if(isOpened == true)
        {
            TPSCam.SetActive(false);
            AimCam.SetActive(false);
            AimCamCanvas.SetActive(false);
            carCamera.SetActive(true);

            MoveVehicle();
            VehicleSteering();
            Braking();
        }
        else if(isOpened == false)
        {
            TPSCam.SetActive(true);
            AimCam.SetActive(true);
            AimCamCanvas.SetActive(true);
            carCamera.SetActive(false);
        }

    }

    void MoveVehicle()
    {
        frontRightWheelCollider.motorTorque = presentAcceleration;
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;
        backLeftWheelCollider.motorTorque = presentAcceleration;

        presentAcceleration = acceleration * Input.GetAxis("Vertical");
    }

    void VehicleSteering()
    {
        presentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");//quay banh xe
        frontRightWheelCollider.steerAngle = presentTurnAngle;
        frontLeftWheelCollider.steerAngle = presentTurnAngle;

        //animate
        WheelsSteering(frontRightWheelCollider, frontRightWheelTransform);
        WheelsSteering(frontLeftWheelCollider, frontLeftWheelTransform);
        WheelsSteering(backRightWheelCollider, backRightWheelTransform);
        WheelsSteering(backLeftWheelCollider, backLeftWheelTransform);
    }

    void WheelsSteering(WheelCollider WCollider, Transform WTransform)
    {
        Vector3 position;
        Quaternion rotation;

        WCollider.GetWorldPose(out position, out rotation);

        WTransform.position = position;
        WTransform.rotation = rotation;
    }

    void Braking()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            presentBraking = brakingForce;
        }
        else 
        {
            presentBraking = 0f;
        }

        frontRightWheelCollider.brakeTorque = presentBraking;
        frontLeftWheelCollider.brakeTorque = presentBraking;
        backRightWheelCollider.brakeTorque = presentBraking;
        backRightWheelCollider.brakeTorque = presentBraking;
    }
}
