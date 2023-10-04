using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Items Slots")]
    public GameObject Weapon1;
    public bool isWeapon1Picked = false;
    public bool isWeapon1Active = false;

    public GameObject Weapon2;
    public bool isWeapon2Picked = false;
    public bool isWeapon2Active = false;

    public GameObject Weapon3;
    public bool isWeapon3Picked = false;
    public bool isWeapon3Active = false;

    public GameObject Weapon4;
    public bool isWeapon4Picked = false;
    public bool isWeapon4Active = false;


    [Header("Weapon to Use")]
    public GameObject HandGun1;
    public GameObject HandGun2;
    public GameObject ShotGun;
    public GameObject UZI1;
    public GameObject UZI2;
    public GameObject Bazooka;

    [Header("Scripts")]
    public PlayerMoverment playerScript;
    public HandGun handgun1Script;
    public HandGun2 handgun2Script;
    public ShotGun shotgunScript;
    public UZI uzi1Script;
    public UZI2 uzi2Script;
    public Bazooka bazookaScripts;
    

    [Header("Inventory")]
    public GameObject InventoryPanel;
    bool isPause = false;
    public SwicherCamera camSW;
    public GameObject AimCam;
    public GameObject TPSCam;


    private void Update()
    {
        if(Input.GetKeyDown("1") && isWeapon1Picked == true)
        {
            isWeapon1Active = true;
            isWeapon2Active = false;
            isWeapon3Active = false;
            isWeapon4Active = false;
            isRifleActive();
        }
        else if (Input.GetKeyDown("2") && isWeapon2Picked == true)
        {
            isWeapon1Active = false;
            isWeapon2Active = true;
            isWeapon3Active = false;
            isWeapon4Active = false;
            isRifleActive();
        }
        else if (Input.GetKeyDown("3") && isWeapon3Picked == true)
        {
            isWeapon1Active = false;
            isWeapon2Active = false;
            isWeapon3Active = true;
            isWeapon4Active = false;
            isRifleActive();
        }
        else if (Input.GetKeyDown("4") && isWeapon4Picked == true)
        {
            isWeapon1Active = false;
            isWeapon2Active = false;
            isWeapon3Active = false;
            isWeapon4Active = true;
            isRifleActive();
        }
        else if (Input.GetKeyDown("tab"))
        {
            if (isPause)//neu khong nhan pause thi dau Inventory di
            {
                HideInventory();
            }
            else
            {
                if(isWeapon1Picked == true)
                {
                    Weapon1.SetActive(true);
                }
                if (isWeapon2Picked == true)
                {
                    Weapon2.SetActive(true);
                }
                if (isWeapon3Picked == true)
                {
                    Weapon3.SetActive(true);
                }
                if (isWeapon4Picked == true)
                {
                    Weapon4.SetActive(true);
                }
                ShowInventory();
            }
        }
    }

    void isRifleActive()
    {
        if(isWeapon1Active == true)
        {
            HandGun1.SetActive(true);
            HandGun2.SetActive(true);
            ShotGun.SetActive(false);
            UZI1.SetActive(false);
            UZI2.SetActive(false);
            Bazooka.SetActive(false);

            playerScript.GetComponent<PlayerMoverment>().enabled = false;
            camSW.GetComponent<SwicherCamera>().enabled = true;
            handgun1Script.GetComponent<HandGun>().enabled = true;
            handgun2Script.GetComponent<HandGun2>().enabled = true;
            shotgunScript.GetComponent<ShotGun>().enabled = false;
            uzi1Script.GetComponent<UZI>().enabled = false;
            uzi2Script.GetComponent<UZI2>().enabled = false;
            bazookaScripts.GetComponent<Bazooka>().enabled = false;
        }
        else if (isWeapon2Active == true)
        {
            HandGun1.SetActive(false);
            HandGun2.SetActive(false);
            ShotGun.SetActive(true);
            UZI1.SetActive(false);
            UZI2.SetActive(false);
            Bazooka.SetActive(false);

            playerScript.GetComponent<PlayerMoverment>().enabled = false;
            camSW.GetComponent<SwicherCamera>().enabled = true;
            handgun1Script.GetComponent<HandGun>().enabled = false;
            handgun2Script.GetComponent<HandGun2>().enabled = false;
            shotgunScript.GetComponent<ShotGun>().enabled = true;
            uzi1Script.GetComponent<UZI>().enabled = false;
            uzi2Script.GetComponent<UZI2>().enabled = false;
            bazookaScripts.GetComponent<Bazooka>().enabled = false;
        }

        else if (isWeapon3Active == true)
        {
            HandGun1.SetActive(false);
            HandGun2.SetActive(false);
            ShotGun.SetActive(false);
            UZI1.SetActive(true);
            UZI2.SetActive(true);
            Bazooka.SetActive(false);

            playerScript.GetComponent<PlayerMoverment>().enabled = false;
            camSW.GetComponent<SwicherCamera>().enabled = true;
            handgun1Script.GetComponent<HandGun>().enabled = false;
            handgun2Script.GetComponent<HandGun2>().enabled = false;
            shotgunScript.GetComponent<ShotGun>().enabled = false;
            uzi1Script.GetComponent<UZI>().enabled = true;
            uzi2Script.GetComponent<UZI2>().enabled = true;
            bazookaScripts.GetComponent<Bazooka>().enabled = false;
        }

        else if (isWeapon4Active == true)
        {
            HandGun1.SetActive(false);
            HandGun2.SetActive(false);
            ShotGun.SetActive(false);
            UZI1.SetActive(false);
            UZI2.SetActive(false);
            Bazooka.SetActive(true);

            playerScript.GetComponent<PlayerMoverment>().enabled = false;
            camSW.GetComponent<SwicherCamera>().enabled = true;
            handgun1Script.GetComponent<HandGun>().enabled = false;
            handgun2Script.GetComponent<HandGun2>().enabled = false;
            shotgunScript.GetComponent<ShotGun>().enabled = false;
            uzi1Script.GetComponent<UZI>().enabled = false;
            uzi2Script.GetComponent<UZI2>().enabled = false;
            bazookaScripts.GetComponent<Bazooka>().enabled = true;

        }
    }
    void ShowInventory()
    {
        camSW.GetComponent<SwicherCamera>().enabled = false;
        AimCam.SetActive(false);
        TPSCam.SetActive(false);

        InventoryPanel.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }

    void HideInventory()
    {
        camSW.GetComponent<SwicherCamera>().enabled = true;
        AimCam.SetActive(true);
        TPSCam.SetActive(true);

        InventoryPanel.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }
}
