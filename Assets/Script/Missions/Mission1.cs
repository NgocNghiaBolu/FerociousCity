using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1 : MonoBehaviour
{
    public Player player;
    public Missions mission;

    public GameObject saveGameUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //UI
            player.SavePlayer();
            StartCoroutine(SaveGameUI());
        }
        if(mission.Mission2 == false && mission.Mission3 == false && mission.Mission4 == false)
        {
            mission.Mission1 = true;
            player.playerMoney += 300;
        }
    }

    IEnumerator SaveGameUI()
    {
        saveGameUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        saveGameUI.SetActive(false);
    }
}
