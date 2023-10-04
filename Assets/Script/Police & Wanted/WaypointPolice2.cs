using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPolice2 : MonoBehaviour
{
    [Header("Police")]
    public Police2 police;
    public Waypoint currentWP;
    int direction;
    bool shouldBranch = false;

    private void Awake()
    {
        police = GetComponent<Police2>();
    }

    private void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        police.LocateDestination(currentWP.GetPosition());
    }

    private void Update()
    {
        if (police.destinationReached)
        {
            if (currentWP.branches != null & currentWP.branches.Count > 0)
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWP.branchRatio ? true : false;
            }
            if (shouldBranch)
            {
                currentWP = currentWP.branches[Random.Range(0, currentWP.branches.Count - 1)];
            }
            else
            {
                if (direction == 0)
                {
                    if (currentWP.nextWaypoint != null)
                    {
                        currentWP = currentWP.nextWaypoint;// tham chieu to waypoint tiep theo
                    }
                    else
                    {
                        currentWP = currentWP.previousWaypoint;//neu khong cu di thnag
                        direction = 1;
                    }
                }
                else if (direction == 1)
                {
                    if (currentWP.previousWaypoint != null)
                    {
                        currentWP = currentWP.previousWaypoint;
                    }
                    else
                    {
                        currentWP = currentWP.nextWaypoint;
                        direction = 0;
                    }
                }
            }
            police.LocateDestination(currentWP.GetPosition());
        }
    }
}
