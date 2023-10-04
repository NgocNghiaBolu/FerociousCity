using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class UZI : MonoBehaviour
{
    //Rifle Moverment
    [Header("Player Moverment")]
    public float speedPlayer = 2f;
    public float runPlayer = 3f;

    [Header("Player Animation and Gravity ")]
    public CharacterController characContrl;
    public float gravity = -9.8f;
    public Animator animator;

    [Header("Player Camera")]
    public Transform playerCamera;

    [Header("Player Jump and Velocity")]
    public float jumpRange = 1f;
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.3f;
    public LayerMask surfaceMask;

    //Rifle Shooting
    [Header("Rifle Things")]
    public Camera cam;
    public float damage = 10f;
    public float shootRange = 100f;
    public float fireCharge = 10f;
    private float nextTimeToShoot = 0f;
    public Transform hand;
    public Transform playerTransform;
    public UZI2 uzi2;
    bool isMoving;
    bool UZIActive = true;
        
    [Header("Rifle Ammo and Reload")]
    public int maxAmmo = 15;
    public int mag = 10;
    public int presentAmmo;
    public float reloadTime = 4.3f;
    bool setReload = false;


    [Header("Rifle UI and Sound ")]
    public GameObject AmmoOut;

    [Header("Rifle Effects")]
    public ParticleSystem effectsGun;
    public GameObject bloodEffect;
    public GameObject hitImpact;

    private void Awake()
    {
        transform.SetParent(hand);
        Cursor.lockState = CursorLockMode.Locked;
        presentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (UZIActive == true)
        {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("GunUZI");
        }

        if (isMoving == false)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
            {
                animator.SetBool("AimIdle", false);
                animator.SetBool("Shoot", true);

                nextTimeToShoot = Time.time + 1f / fireCharge; //time bde ban 1 vien dan ra
                Shoot();
            }
            else
            {
                animator.SetBool("AimIdle", true);
                animator.SetBool("Shoot", false);
            }
        }

        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        characContrl.Move(velocity * Time.deltaTime);

        PlayerMove();
        PlayerRun();
        PlayerJump();

        if (setReload) return;

        if (presentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

    }

    private void PlayerMove()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("WalkForward", true);
            animator.SetBool("RunForward", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(playerTransform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            playerTransform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characContrl.Move(moveDirection.normalized * speedPlayer * Time.deltaTime);
            jumpRange = 0.5f;
            isMoving = true;
            uzi2.isMoving = true;
        }
        else
        {
            animator.SetBool("WalkForward", false);
            animator.SetBool("RunForward", false);
            jumpRange = 1f;
            isMoving = false;
            uzi2.isMoving = false;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("AimIdle", false);
            animator.SetTrigger("Jump");

            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("AimIdle", true);
            animator.ResetTrigger("Jump");
        }
    }

    void PlayerRun()
    {
        if (Input.GetButton("Run") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("WalkForward", false);
                animator.SetBool("RunForward", true);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(playerTransform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                playerTransform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characContrl.Move(moveDirection.normalized * runPlayer * Time.deltaTime);
                jumpRange = 0f;
                isMoving = true;
                uzi2.isMoving = true;
            }
            else
            {
                animator.SetBool("WalkForward", true);
                animator.SetBool("RunForward", false);
                jumpRange = 1f;
                isMoving = false;
                uzi2.isMoving = false;
            }
        }
    }

    void Shoot()
    {
        if (mag == 0)
        {
            StartCoroutine(ShowAmmoOut());
            return;
        }

        presentAmmo--;

        if (presentAmmo == 0)
        {
            mag--;
        }
        //UI
        AmmoUI.instance.UpdateAmmo(presentAmmo);
        AmmoUI.instance.UpdateMag(mag);

        effectsGun.Play();
        RaycastHit hit;


        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, shootRange))
        {
            Debug.Log(hit.transform.name);

            HealthObject obj = hit.transform.GetComponent<HealthObject>();
            Police policeBody = hit.transform.GetComponent<Police>();
            Police2 police2 = hit.transform.GetComponent<Police2>();
            CharacterAI characterBody = hit.transform.GetComponent<CharacterAI>();
            Gangster1 gangster1 = hit.transform.GetComponent<Gangster1>();
            Gangster2 gangster2 = hit.transform.GetComponent<Gangster2>();
            Boss boss = hit.transform.GetComponent<Boss>();

            if (obj != null)
            {
                obj.HitDamage(damage);
                GameObject hitImpactGo = Instantiate(hitImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitImpactGo, 0.8f);
            }
            else if (policeBody != null)
            {
                policeBody.PoliceTakeDamage(damage);
                GameObject hitImpactBlood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitImpactBlood, 0.8f);
            }
            else if (police2 != null)
            {
                police2.Police2TakeDamage(damage);
                GameObject hitImpactBlood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitImpactBlood, 0.8f);
            }

            else if (characterBody != null)
            {
                characterBody.CharacterHitDam(damage);
                GameObject hitImpactBlood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitImpactBlood, 0.8f);
            }
            else if (gangster1 != null)
            {
                gangster1.GangsterTakeDamage(damage);
                GameObject hitImpactBlood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitImpactBlood, 0.8f);
            }
            else if (gangster2 != null)
            {
                gangster2.Gangster2TakeDamage(damage);
                GameObject hitImpactBlood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitImpactBlood, 0.8f);
            }
            else if (boss != null)
            {
                boss.TakeDamage(damage);
                GameObject hitImpactBlood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitImpactBlood, 0.8f);
            }

        }
    }

    IEnumerator Reload()
    {
        setReload = true;
        speedPlayer = 0f;
        runPlayer = 0f;
        Debug.Log("Reloading...");
        animator.SetBool("Reload", true);
        //ani sound
        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("Reload", false);
        speedPlayer = 2f;
        runPlayer = 3f;
        presentAmmo = maxAmmo;
        setReload = false;
    }

    IEnumerator ShowAmmoOut()
    {
        AmmoOut.SetActive(true);
        yield return new WaitForSeconds(4f);
        AmmoOut.SetActive(false);
    }
}
