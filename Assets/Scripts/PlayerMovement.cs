using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControls controls;

    public CharacterController2D controller;
    public Animator animator;

    public float walkSpeed = 40f;
    public float runMultiplier = 1.5f;

    float horizontalMove = 0f;
    bool _isJumping = false;
    bool _isCrouching = false;


    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Left.performed += ctx => horizontalMove = OnMoveLeft();
        controls.Gameplay.Left.canceled += ctx => horizontalMove = 0f;
        controls.Gameplay.Right.performed += ctx => horizontalMove = OnMoveRight();
        controls.Gameplay.Right.canceled += ctx => horizontalMove = 0f;

        controls.Gameplay.Run.performed += ctx => horizontalMove *= Run();
        controls.Gameplay.Run.canceled += ctx => horizontalMove /= DontRun();

        controls.Gameplay.Jump.performed += ctx => OnJump();
        controls.Gameplay.Crouch.performed += ctx => OnCrouch();
        controls.Gameplay.Crouch.canceled += ctx => OnStand();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
       /* horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed; */

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

       /* if (Input.GetButtonDown("Jump"))
        {
            _isJumping = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            _isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            _isCrouching = false;
        }*/
    }

    float OnMoveLeft()
    {
        return -1f * walkSpeed;
    }

    float OnMoveRight()
    {
        return 1f * walkSpeed;
    }

    float Run()
    {
        animator.SetBool("isRunning", true);
        return runMultiplier;
    }

    float DontRun()
    {
        animator.SetBool("isRunning", false);
        return runMultiplier;
    }

    void OnJump()
    {
        _isJumping = true;
        animator.SetBool("isJumping", true);
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void OnCrouch()
    {
        _isCrouching = true;
        animator.SetBool("isCrouching", true);
    }

    void OnStand()
    {
        _isCrouching = false;
        animator.SetBool("isCrouching", false);
    }

    /*public void OnCrouching(bool isCrouching)
    {
        Debug.Log("Are you crouching? " + isCrouching);
        animator.SetBool("isCrouching", isCrouching);
    }*/

    private void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, _isCrouching, _isJumping);
        _isJumping = false;

        if (transform.position.y < -80f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
