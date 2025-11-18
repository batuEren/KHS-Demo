using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 5f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 200f;
    public Transform cameraTransform; 

    [Header("Ground Check")]
    public Transform groundCheck; 
    public float groundCheckRadius = 0.2f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private float xRotation = 0f;
    private bool jumpRequested = false;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleGroundCheck();
        HandleJumpInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJumpPhysics();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the body left/right (yaw)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera up/down (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D
        float vertical = Input.GetAxisRaw("Vertical");     // W/S

        Vector3 moveDir = (transform.forward * vertical + transform.right * horizontal).normalized;

        Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = moveDir * moveSpeed;
        targetVelocity.y = currentVelocity.y; 

        rb.velocity = targetVelocity;
    }

    void HandleGroundCheck()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        }
        else
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);
        }
    }

    void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }
    }

    void HandleJumpPhysics()
    {
        if (jumpRequested)
        {
            // reset vertical velocity before applying jump
            Vector3 vel = rb.velocity;
            vel.y = 0f;
            rb.velocity = vel;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }
}
