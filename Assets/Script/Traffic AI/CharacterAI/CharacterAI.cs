using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
    [Header("Character Info")]
    public float moveSpeed;
    public float turningSpeed = 298f;
    public float stopSpeed = 1f;
    private float characterHealth = 100f;
    public float presentHealth;
    public Player player;

    [Header("Destination var")]
    public Vector3 destination;
    public bool destinationReached;
    public Animator animator;

    private void Start()
    {
        presentHealth = characterHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        Walk();
    }

    public void Walk()
    {
        if(transform.position != destination)
        {
            Vector3 destinaDirection = destination - transform.position;
            destinaDirection.y = 0;
            float distanceDestina = destinaDirection.magnitude;

            if(distanceDestina >= stopSpeed)
            {
                //turn
                destinationReached = false;
                Quaternion targetRotate = Quaternion.LookRotation(destinaDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotate, turningSpeed * Time.deltaTime);

                //Move
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
            else
            {
                destinationReached = true;
            }
        }
    }

    public void LocateDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationReached = false;
    }

    public void CharacterHitDam(float damage)
    {
        presentHealth -= damage;

        if(presentHealth <= 0)
        {
            animator.SetBool("Die", true);
            Die();
        }
    }

    void Die()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;//    
        moveSpeed = 0f;
        Destroy(gameObject, 4f);
        player.currentKills += 1;
        player.playerMoney += 7;
    }
}
