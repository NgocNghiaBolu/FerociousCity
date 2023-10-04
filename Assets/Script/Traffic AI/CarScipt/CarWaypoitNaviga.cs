using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWaypoitNaviga : MonoBehaviour
{
    [Header("Car AI")]
    public CarNaviga car;
    public Waypoint currentWaypoint;

    private void Awake()
    {
        car = GetComponent<CarNaviga>();
    }

    private void Start()
    {
        car.LocateDestination(currentWaypoint.GetPosition());//toi dia diem da cho truoc
    }

    private void Update()
    {
        if (car.destinationReached)//neu toi dia diem can toi roi thi next xang dia diem moi
        {
            currentWaypoint = currentWaypoint.nextWaypoint;
            car.LocateDestination(currentWaypoint.GetPosition());
        }
    }
}
