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
    public float jumpCooldown = 5f;
    public float attackCooldown = 2f;
    public float movementDisableDuration = 3f;
    public float attackAnimationDuration = 3f;

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
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            if (movementDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movementDirection);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    isRunning = true;
                    animator.SetFloat("Speed", 1f); // Set speed to 2 when running
                }
                else
                {
                    isRunning = false;
                    animator.SetFloat("Speed", .5f); // Set speed to 1 when walking
                }
            }
            else
            {
                animator.SetFloat("Speed", 0f); // Set speed to 0 when idle
            }

            // Move the character using Rigidbody
            rb.MovePosition(rb.position + movementDirection * (isRunning ? runSpeed : walkSpeed) * Time.deltaTime);
        }

        // Handle jumping with cooldown
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && Time.time - lastJumpTime > jumpCooldown)
        {
            isJumping = true;
            lastJumpTime = Time.time;
            StartCoroutine(JumpRoutine());
        }

        // Handle attacking with cooldown
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time - lastAttackTime > attackCooldown)
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
        animator.SetTrigger("Attack");

        // Implement attack logic here

        // Wait for the specified duration (attackAnimationDuration) before enabling movement controls
        yield return new WaitForSeconds(attackAnimationDuration);

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