using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEnder : MonoBehaviour
{
    [Header("GameObject Active")]
    public GameObject MainPlayer;
    public GameObject MainCamera;
    public GameObject TPSCam;
    public GameObject AimCam;
    public GameObject AimCanvas;
    public GameObject PlayerUI;
    public GameObject MiniMapCam;
    public GameObject MinimapCanvas;
    public GameObject SaveCanvas;
    public GameObject PoliceRoad;
    public GameObject Gangster;
    public GameObject Car;
    public GameObject CarPolice;
    public SpawnPolice1 SPPolice1;
    public SpawnPolice2 SPPolice2;
    public SpawnFBI SPFBI;


    [Header("GameObject DeActive")]
    public GameObject CutScene;
    public GameObject PlayerCutScene;
    public GameObject CutSceneCam;
    public GameObject BusCutScene;
    public GameObject Rebel1;
    public GameObject Rebel2;
    public GameObject ShowPlayerCollider;
    public GameObject cutSceneEnder;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CutSceneCamera")
        {
            //deActive
            CutScene.SetActive(false);
            PlayerCutScene.SetActive(false);
            CutSceneCam.SetActive(false);
            BusCutScene.SetActive(false);
            Rebel1.SetActive(false);
            Rebel2.SetActive(false);
            ShowPlayerCollider.SetActive(false);
            cutSceneEnder.SetActive(false);

            //Active
            MainPlayer.SetActive(true);
            MainCamera.SetActive(true);
            TPSCam.SetActive(true);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
            PlayerUI.SetActive(true);
            MiniMapCam.SetActive(true);
            MinimapCanvas.SetActive(true);
            SaveCanvas.SetActive(true);
            PoliceRoad.SetActive(true);
            Gangster.SetActive(true);
            Car.SetActive(true);
            CarPolice.SetActive(true);
            SPFBI.GetComponent<SpawnFBI>().enabled = true;
            SPPolice1.GetComponent<SpawnPolice1>().enabled = true;
            SPPolice2.GetComponent<SpawnPolice2>().enabled = true;

        }
    }


}
