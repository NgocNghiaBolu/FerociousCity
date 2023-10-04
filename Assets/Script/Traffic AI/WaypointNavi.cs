using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavi : MonoBehaviour
{
    [Header("Character")]
    public CharacterAI character;
    public Waypoint currentWp;
    int directionWalk;
    bool shouldBranch = false;

    private void Awake()
    {
        character = GetComponent<CharacterAI>();
    }

    private void Start()
    {
        directionWalk = Mathf.RoundToInt(Random.Range(0f, 1f));
        character.LocateDestination(currentWp.GetPosition());
    }

    private void Update()
    {
        if (character.destinationReached)
        {
            if(currentWp.branches != null && currentWp.branches.Count > 0)//
            {
                shouldBranch = Random.Range(0f, 1f) <= currentWp.branchRatio ? true : false;//neu ti le be hon hoac bang ratio da cho thi motj la dung hai la sai
            }
            if (shouldBranch)
            {
                currentWp = currentWp.branches[Random.Range(0, currentWp.branches.Count - 1)];
            }
            else
            {
                if (directionWalk == 0)
                {
                    if(currentWp.nextWaypoint != null)
                    {
                        currentWp = currentWp.nextWaypoint;//tham chieu diem WP tiep theo
                    }
                    else
                    {
                        currentWp = currentWp.previousWaypoint;
                        directionWalk = 1;
                    } 
                }
                else if (directionWalk == 1)
                {
                    if (currentWp.previousWaypoint != null)
                    {
                        currentWp = currentWp.previousWaypoint;//tham chieu diem WP hien tai
                    }
                    else
                    {
                        currentWp = currentWp.nextWaypoint;
                        directionWalk = 0;
                    }
                }
            }
            
            character.LocateDestination(currentWp.GetPosition());//character se toi noi duoc gan 
        }
        
    }
}
