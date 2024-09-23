using System.Collections;
using UnityEngine;


namespace CuteTownAssetPack
{
public class PlayerCharacterController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpForce = 5f;
    public float jumpCooldown = 0.1f;
    public float attackCooldown = 0.1f;
    public float movementDisableDuration = 1f;
    public float attackAnimationDuration = 1f;

    private bool isJumping = false;
    private bool areMovementControlsDisabled = false;
    private bool isRunning = false;
    private float lastJumpTime = 0f;
    private float lastAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

     void FixedUpdate()
        {
            if (!areMovementControlsDisabled)
            {
                // Check if Left Shift is pressed to start running
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    isRunning = true;
                    animator.SetFloat("Speed", 1f); // Set speed to run
                    Vector3 movementDirection = transform.forward; // Always move forward
                    rb.MovePosition(rb.position + movementDirection * runSpeed * Time.deltaTime);
                }
                else
                {
                    isRunning = false;
                    // Handle normal movement with arrow keys
                    float horizontalInput = -Input.GetAxis("Horizontal");
                    float verticalInput = -Input.GetAxis("Vertical");

                    Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

                    if (movementDirection != Vector3.zero)
                    {
                        transform.rotation = Quaternion.LookRotation(movementDirection);
                        animator.SetFloat("Speed", 0.5f); // Set speed to walk
                    }
                    else
                    {
                        animator.SetFloat("Speed", 0f); // Idle when not moving
                    }

                    // Move the player using Rigidbody
                    rb.MovePosition(rb.position + movementDirection * walkSpeed * Time.deltaTime);
                }
            }

            // Handle jumping
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping && Time.time - lastJumpTime > jumpCooldown)
            {
                isJumping = true;
                lastJumpTime = Time.time;
                StartCoroutine(JumpRoutine());
            }

            // Handle attacking
            if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackCooldown)
            {
                lastAttackTime = Time.time;
                StartCoroutine(AttackRoutine());
            }
        }


    IEnumerator JumpRoutine()
    {
        animator.SetTrigger("Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f); // Adjust based on your animation length
        isJumping = false;
    }

    IEnumerator AttackRoutine()
    {
        // Disable movement controls
        DisableMovementControls();

        // Trigger attack animation
        animator.SetTrigger("NormalATK");

        // Implement attack logic here

        // Wait for the specified duration (attackAnimationDuration) before enabling movement controls
        yield return new WaitForSeconds(0.1f);

        // Enable movement controls after duration
        EnableMovementControls();
    }

    IEnumerator RunRoutine()
    {
        // Disable movement controls
        DisableMovementControls();

        // Trigger attack animation
        animator.SetTrigger("Run");
        transform.Translate(Vector3.forward * Time.deltaTime);
        yield return new WaitForSeconds(0.1f);
        // Enable movement controls after duration
        EnableMovementControls();
    }

    void DisableMovementControls()
    {
        // Set areMovementControlsDisabled flag to true to disable movement
        areMovementControlsDisabled = true;
    }

    void EnableMovementControls()
    {
        // Reset areMovementControlsDisabled flag to false to enable movement
        areMovementControlsDisabled = false;
    }
}
}