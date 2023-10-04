using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMoverment : MonoBehaviour
{
    [Header("Player Moverment")]
    public float speedPlayer = 2f;
    public float runPlayer = 3f;

    [Header("Player Animation and Gravity ")]
    public Animator animator;
    public CharacterController characContrl;
    public float gravity = -9.8f;

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
    bool playerControlActive = true;
    public Player player;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {
        if(playerControlActive == true)
        {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Player");
        }

        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if(onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        characContrl.Move(velocity * Time.deltaTime);

        PlayerMove();
        PlayerRun();
        PlayerJump();
    }

    private void PlayerMove()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

        if(direction.magnitude >= 0.1f)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Run", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characContrl.Move(moveDirection.normalized * speedPlayer * Time.deltaTime);
            jumpRange = 0.5f;
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            jumpRange = 1f;
        }
    }

    private void PlayerJump()
    {
        if(Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
            
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.ResetTrigger("Jump");
        }
    }

    void PlayerRun()
    {
        if(Input.GetButton("Run") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
        {
            float horizontalAxis = Input.GetAxisRaw("Horizontal");
            float verticalAxis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontalAxis, 0f, verticalAxis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characContrl.Move(moveDirection.normalized * runPlayer * Time.deltaTime);
                jumpRange = 0.2f;
            }
            else
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
                jumpRange = 1f;
            }
        }
    }

}
