using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GangsterWaypoint1 : MonoBehaviour
{
    [Header("Gangster")]
    public Gangster1 gangster;
    public Waypoint currentWP;
    int direction;
    bool shouldBranch = false;

    private void Awake()
    {
        gangster = GetComponent<Gangster1>();
    }

    private void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        gangster.LocateDestination(currentWP.GetPosition());
    }

    private void Update()
    {
        if (gangster.destinationReached)
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
            gangster.LocateDestination(currentWP.GetPosition());
        }
    }
}
