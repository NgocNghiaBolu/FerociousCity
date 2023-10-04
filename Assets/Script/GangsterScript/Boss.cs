using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Player player;
    public float bossHealth = 200f;
    public Animator animator;
    public Missions mission;

    private void Update()
    {
        if(bossHealth < 200)
        {
            //ani
        }
        if(bossHealth <= 0)
        {
            //mission
            if (mission.Mission1 == true && mission.Mission2 == true && mission.Mission3 == true)
            {
                mission.Mission4 = true;
                player.playerMoney += 1000;
            }

            //ani
            Object.Destroy(gameObject);
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }

    }

    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
    } 
}
