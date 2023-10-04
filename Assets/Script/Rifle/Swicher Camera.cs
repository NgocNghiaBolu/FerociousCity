using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwicherCamera : MonoBehaviour
{
    [Header("Camera to Assign")]
    public GameObject AimCam;
    public GameObject TPCam;
    public Animator animator;

    private void Update()
    {
        if(Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Shoot", false);

            TPCam.SetActive(false);
            AimCam.SetActive(true);
        }
        else if (Input.GetButton("Fire2"))
        {
            animator.SetBool("Shoot", true);
            animator.SetBool("Walk", true);

            TPCam.SetActive(false);
            AimCam.SetActive(true);
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Shoot", false);

            TPCam.SetActive(true);
            AimCam.SetActive(false);
        }

    }
}
