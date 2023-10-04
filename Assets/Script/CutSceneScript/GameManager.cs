using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("GameObject Continue")]
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


    [Header("GameObject Start")]
    public GameObject CutScene;
    public GameObject PlayerCutScene;
    public GameObject CutSceneCam;
    public GameObject BusCutScene;
    public GameObject Rebel1;
    public GameObject Rebel2;
    public GameObject ShowPlayerCollider;
    public GameObject cutSceneEnder;

    public Player player;

    private void Start()
    {
        if(MainMenu.instance.continueGame == true)
        {
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
            SPFBI.GetComponent<SpawnFBI>().enabled = true;
            SPPolice1.GetComponent<SpawnPolice1>().enabled = true;
            SPPolice2.GetComponent<SpawnPolice2>().enabled = true;

            //loading old data
            player.LoadPlayer();

            CutScene.SetActive(false);
            PlayerCutScene.SetActive(false);
            CutSceneCam.SetActive(false);
            BusCutScene.SetActive(false);
            Rebel1.SetActive(false);
            Rebel2.SetActive(false);
            ShowPlayerCollider.SetActive(false);
            cutSceneEnder.SetActive(false);
        }

        if (MainMenu.instance.startGame == true)
        {
            MainPlayer.SetActive(false);
            MainCamera.SetActive(false);
            TPSCam.SetActive(false);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
            PlayerUI.SetActive(false);
            MiniMapCam.SetActive(false);
            MinimapCanvas.SetActive(false);
            SaveCanvas.SetActive(false);
            PoliceRoad.SetActive(false);
            Gangster.SetActive(false);
            SPFBI.GetComponent<SpawnFBI>().enabled = false;
            SPPolice1.GetComponent<SpawnPolice1>().enabled = false;
            SPPolice2.GetComponent<SpawnPolice2>().enabled = false;

            CutScene.SetActive(true);
            PlayerCutScene.SetActive(false);
            CutSceneCam.SetActive(true);
            BusCutScene.SetActive(true);
            Rebel1.SetActive(true);
            Rebel2.SetActive(true);
            ShowPlayerCollider.SetActive(true);
            cutSceneEnder.SetActive(true);
        }
    }

}
