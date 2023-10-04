using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UZI2 : MonoBehaviour
{
    [Header("Rifle Things")]
    public Camera cam;
    public float damage = 10f;
    public float shootRange = 100f;
    public float fireCharge = 10f;
    private float nextTimeToShoot = 0f;
    public Transform hand;
    public bool isMoving;

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
    public GameObject hitImpact;

    private void Awake()
    {
        transform.SetParent(hand);
        Cursor.lockState = CursorLockMode.Locked;
        presentAmmo = maxAmmo;
    }

    private void Update()
    {
        if (setReload) return;

        if (presentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (isMoving == false)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
            {
                nextTimeToShoot = Time.time + 1f / fireCharge; //time bde ban 1 vien dan ra
                Shoot();
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

        effectsGun.Play();
        RaycastHit hit;


        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, shootRange))
        {
            Debug.Log(hit.transform.name);

            HealthObject obj = hit.transform.GetComponent<HealthObject>();
            if (obj != null)
            {
                obj.HitDamage(damage);
                GameObject hitImpactGo = Instantiate(hitImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(hitImpactGo);
            }

        }
    }

    IEnumerator Reload()
    {
        setReload = true;
        Debug.Log("Reloading...");
        //ani sound
        yield return new WaitForSeconds(reloadTime);

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
