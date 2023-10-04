using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PickupItems : MonoBehaviour
{
    [Header("Items Info")]
    public int ItemPrice;
    public int ItemRadius;
    public string ItemTag;
    private GameObject ItemToPick; 

    [Header("Player Info")]
    public Player player;
    public Inventory inventory;
    public Missions mission;

    private void Start()
    {
        ItemToPick = GameObject.FindWithTag(ItemTag);
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < ItemRadius)//neu vi tri player be hon pham vi cua Mon Hang
        {
            if (Input.GetKeyDown("f"))
            {
                if(ItemPrice > player.playerMoney)
                {
                    Debug.Log("Ypo are Broke @");
                    //Show Ui
                }
                else
                {
                    if (mission.Mission1 == true && mission.Mission2 == true && mission.Mission4 == false)
                    {
                        mission.Mission3 = true;
                        player.playerMoney += 600;
                    }

                    if (ItemTag == "HandGunPickup")
                    {
                        player.playerMoney -= ItemPrice;
                        inventory.Weapon1.SetActive(true);
                        inventory.isWeapon1Picked = true;
                        Debug.Log(ItemTag);
                    }
                    else if (ItemTag == "ShotGunPickup")
                    {
                        player.playerMoney -= ItemPrice;
                        inventory.Weapon2.SetActive(true);
                        inventory.isWeapon2Picked = true;
                        Debug.Log(ItemTag);
                    }
                    else if (ItemTag == "UZIPickup")
                    {
                        player.playerMoney -= ItemPrice;
                        inventory.Weapon3.SetActive(true);
                        inventory.isWeapon3Picked = true;
                        Debug.Log(ItemTag);
                    }
                    else if (ItemTag == "BazookaPickup")
                    {
                        player.playerMoney -= ItemPrice;
                        inventory.Weapon4.SetActive(true);
                        inventory.isWeapon4Picked = true;
                        Debug.Log(ItemTag);
                    }
                    ItemToPick.SetActive(false);
                }
            }
        }
    }
}
