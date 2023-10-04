using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission2 : MonoBehaviour
{
    public Player player;
    public Missions mission;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (mission.Mission1 == true && mission.Mission3 == false && mission.Mission4 == false)
            {
                mission.Mission2 = true;
                player.playerMoney += 400;
            }
        }
    }
}
