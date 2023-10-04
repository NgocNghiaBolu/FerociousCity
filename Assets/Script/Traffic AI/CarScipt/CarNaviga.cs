using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNaviga : MonoBehaviour
{
    [Header("Car Info")]
    public float moveSpeed;
    public float turningSped = 298f;
    public float stopSpeed = 1f;
    public GameObject SensorCar;
    float detectionRange = 10f;

    [Header("Destination var")]
    public Vector3 destination;
    public bool destinationReached;
    public Player player;

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(SensorCar.transform.position, SensorCar.transform.forward, out hit, detectionRange))
        {
            Debug.Log(hit.transform.name);//show ten ma car cam bien duoc

            CharacterAI character = hit.transform.GetComponent<CharacterAI>();
            Player playerBD = hit.transform.GetComponent<Player>();

            if(character != null)//gap ng thi di cham lai
            {
                moveSpeed = 0.3f;
                return;
            }
            else if(playerBD != null)//gap ng thi di cham lai
            {
                moveSpeed = 0.3f;
                return;
            }
        }

        Drive();
    }

    public void Drive()
    {
        moveSpeed = 9f;
        if (transform.position != destination)
        {
            Vector3 destinaDirection = destination - transform.position;
            destinaDirection.y = 0;
            float distanceDestina = destinaDirection.magnitude;

            if (distanceDestina >= stopSpeed)
            {
                //turn
                destinationReached = false;
                Quaternion targetRotate = Quaternion.LookRotation(destinaDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotate, turningSped * Time.deltaTime);

                //Move
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            else
            {
                destinationReached = true;
            }
        }
    }

    public void LocateDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationReached = false;
    }
}
