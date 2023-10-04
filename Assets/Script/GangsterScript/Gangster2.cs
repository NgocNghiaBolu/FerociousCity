using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gangster2 : MonoBehaviour
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
            Idle();
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

    public void Idle()
    {
        currentMoveSpeed = 0f;
        transform.LookAt(playerBD.transform);
        animator.SetBool("Idle", true);
        animator.SetBool("Run", false);

    }

    public void ChasePlayer()
    {
        transform.position += transform.forward * currentMoveSpeed * Time.deltaTime;
        transform.LookAt(playerBD.transform);

        animator.SetBool("Run", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Shoot", false);

        currentMoveSpeed = runSpeed;
    }

    private void Shoot()
    {
        moveSpeed = 0f;
        transform.LookAt(playerBD.transform);//nhin theo player neu player di chuyen huong khac

        //ani
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
        animator.SetBool("Shoot", true);

        if (!previousShoot)//neu chua ban thi ban
        {
            RaycastHit hit;
            if (Physics.Raycast(ShootAreaRaycast.transform.position, ShootAreaRaycast.transform.forward, out hit, shootRange))
            {
                Debug.Log(hit.transform.name);

                PlayerHealth playerBody = hit.transform.GetComponent<PlayerHealth>();
                CharacterAI character = hit.transform.GetComponent<CharacterAI>();

                if (playerBody != null)
                {
                    playerBody.TakeDamage(giveDamage);
                    GameObject hitImpactBlood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(hitImpactBlood, 0.8f);
                }
                else if (character != null)
                {
                    character.CharacterHitDam(giveDamage);
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

    public void Gangster2TakeDamage(float damage)
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
