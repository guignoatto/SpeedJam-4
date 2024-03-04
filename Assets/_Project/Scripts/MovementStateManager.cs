using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

enum MovementState
{
    Idle,
    Walking,
    Running,
    Jumping,
    Falling,
    Biting
}

public class MovementStateManager : MonoBehaviour
{
    public float moveSpeed = 4f;
    private float speedMultiplier = 1f;
    [HideInInspector] public Vector3 dir;

    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private int maxJump = 2;
    [SerializeField] private int jumpCount = 0;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource doubleJumpSound;

    [SerializeField] private Vector3 velocity;

    [SerializeField] private Animator playerAnimator;

    private bool checkGrounded = true;
    private Vector3 spherePos;
    private float hzInput, vInput;
    private CharacterController controller;
    private MovementState movementState;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetDirectionMove();
        Gravity();
    }

    private void GetDirectionMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        speedMultiplier = PlayerManager.Instance.playerSpeedMultiplier;
        controller.Move(dir * moveSpeed * speedMultiplier * Time.deltaTime);

        if (controller.velocity.x == 0 && controller.velocity.z == 0
            && IsGrounded()
            && movementState != MovementState.Idle
            && movementState != MovementState.Biting)
        {
            movementState = MovementState.Idle;
            playerAnimator.SetTrigger("Idle");
        }
        else if (controller.velocity.x != 0 && controller.velocity.z != 0 
            && IsGrounded()
            && movementState != MovementState.Walking
            && movementState != MovementState.Biting)
        {
            movementState = MovementState.Walking;
            playerAnimator.SetTrigger("Walking");
        };
    }

    private bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask) && checkGrounded) 
        { 
            jumpCount = 0;
            return true; 
        }

        return false;
    }

    private void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2f;
        }


        if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || jumpCount < maxJump))
        {
            velocity.y = jumpForce;
            StartCoroutine(Jumping());

            if (!movementState.Equals(MovementState.Jumping) || jumpCount < maxJump)
            {
                jumpCount++;
                if (jumpCount == 0)
                {
                    jumpCount = 1;
                }

                if (jumpCount == 1)
                {   
                    jumpSound.Play();
                    movementState = MovementState.Jumping;
                    playerAnimator.SetTrigger("Falling");
                }
                else if (jumpCount == 2)
                {
                    doubleJumpSound.pitch = Random.Range(2f, 3f);
                    doubleJumpSound.Play();
                    movementState = MovementState.Jumping;
                    playerAnimator.SetTrigger("Jump");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            movementState = MovementState.Biting;
            playerAnimator.SetTrigger("Bite");
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (controller.velocity.x == 0 && controller.velocity.z == 0)
            {
                movementState = MovementState.Idle;
                playerAnimator.SetTrigger("Idle");
            }
            else
            {
                movementState = MovementState.Walking;
                playerAnimator.SetTrigger("Walking");
            }
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private IEnumerator Jumping()
    {
        checkGrounded = false;
        yield return new WaitForSeconds(0.5f);
        checkGrounded = true;
    }

}
