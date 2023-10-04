using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gangster1 : MonoBehaviour
{
    [Header("Gangster Infomation")]
    public float moveSpeed;
    public float runSpeed;
    private float currentMoveSpeed;
    public float turningSpeed = 298f;
    public float stopSpeed = 1f;
    public float GangsterHealth = 110f;
    public float presentHealth;

    [Header("Gangster AI")]
    public LayerMask playerLayer;
    public float chaseRadius;
    public float shootRadius;
    public bool playerInChaseRadius;
    public bool playerInShootRadius;
    public GameObject playerBD;

    [Header("Destination var")]
    public Vector3 destination;
    public bool destinationReached;

    [Header("Gangster Shooting")]
    public float giveDamage = 5f;
    public float shootRange = 50f;
    public GameObject ShootAreaRaycast;
    public GameObject bloodEffect;
    public float timeBtwShoot;
    bool previousShoot;
    public Player player;

    [Header("Gangster Animation")]
    public Animator animator;


    private void Start()
    {
        presentHealth = GangsterHealth;
        currentMoveSpeed = moveSpeed;
        playerBD = GameObject.Find("Player");
        player = GameObject.FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        playerInChaseRadius = Physics.CheckSphere(transform.position, chaseRadius, playerLayer);//ktra co trong khu vuc de truy na khong
        playerInShootRadius = Physics.CheckSphere(transform.position, shootRadius, playerLayer);// kra coi trong tam de shoot chua

        if (!playerInChaseRadius && !playerInShootRadius) 
        { 
            Walk();
        }
        if (playerInChaseRadius && !playerInShootRadius )
        { 
            ChasePlayer();
        }
        if (playerInChaseRadius && playerInShootRadius) 
        { 
            Shoot();
        }
    }

    public void Walk()
    {
        if (transform.position != destination)//neu da co huong di thi di chuyen
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float distanceDetination = destinationDirection.magnitude;

            if (distanceDetination >= stopSpeed)
            {
                //turn
                destinationReached = false;
                Quaternion targetRotate = Quaternion.LookRotation(destinationDirection);//quay theo huong duoc hi dinh tiep theo
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotate, turningSpeed * Time.deltaTime);

                //move
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                animator.SetBool("Shoot", false);
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

    public void ChasePlayer()
    {
        transform.position += transform.forward * currentMoveSpeed * Time.deltaTime;
        transform.LookAt(playerBD.transform);

        animator.SetBool("Run", true);
        animator.SetBool("Walk", false);
        animator.SetBool("Shoot", false);

        currentMoveSpeed = runSpeed;
    }

    private void Shoot()
    {
        moveSpeed = 0f;
        transform.LookAt(playerBD.transform);//nhin theo player neu player di chuyen huong khac

        //ani
        animator.SetBool("Walk", false);
        animator.SetBool("Run", false);
        animator.SetBool("Shoot", true);

        if (!previousShoot)//neu chua ban thi ban
        {
            RaycastHit hit;
            if (Physics.Raycast(ShootAreaRaycast.transform.position, ShootAreaRaycast.transform.forward, out hit, shootRange))
            {
                Debug.Log(hit.transform.name);

                PlayerHealth playerBody = hit.transform.GetComponent<PlayerHealth>();

                if (playerBody != null)
                {
                    playerBody.TakeDamage(giveDamage);
                    GameObject hitImpactBlood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(hitImpactBlood, 0.8f);
                }
            }
            previousShoot = true;
            Invoke(nameof(ActiveShoot), timeBtwShoot);// dung mot khoang timeBtwshoot roi ban tiep
        }
    }

    private void ActiveShoot()
    {
        previousShoot = false;
    }

    public void GangsterTakeDamage(float damage)
    {
        presentHealth -= damage;
        if (presentHealth <= 0)
        {
            animator.SetBool("Die", true);
            Die();
        }
    }

    void Die()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        currentMoveSpeed = 0f;
        shootRange = 0f;
        Destroy(gameObject, 3.5f);
        player.currentKills += 1;
        player.playerMoney += 17;
    }
}
