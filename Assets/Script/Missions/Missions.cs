using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour
{
    public Text missionText;

    public bool Mission1 = false;
    public bool Mission2 = false;
    public bool Mission3 = false;
    public bool Mission4 = false;

    void Update()
    {
        if(Mission1 == false && Mission2 == false && Mission3 == false && Mission4 == false )
        {
            //UI
            missionText.text = "Go your House";
        }
        if (Mission1 == true && Mission2 == false && Mission3 == false && Mission4 == false )
        {
            //UI
            missionText.text = "Let's meet Bolu In The PoliceStation";
        }
        if (Mission1 == true&& Mission2 == true && Mission3 == false && Mission4 == false )
        {
            //UI
            missionText.text = "Find Weapon";
        }
        if (Mission1 == true && Mission2 == true && Mission3 == true && Mission4 == false )
        {
            //UI
            missionText.text = "Kill One People";
        }
        if (Mission1 == true && Mission2 == true && Mission3 == true && Mission4 == true)
        {
            //UI
            missionText.text = "You Completed All Missions";
        }

    }
}
