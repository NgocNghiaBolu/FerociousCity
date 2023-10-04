using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject ammoSphere;
    public Transform shootPoint;
    private void Update()
    {
        ShootPlayer();
    }

    void ShootPlayer()
    {
        Rigidbody rb = Instantiate(ammoSphere, shootPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        rb.AddForce(transform.up * 7, ForceMode.Impulse);
    }
}
