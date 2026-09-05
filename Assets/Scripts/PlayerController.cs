using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float jumpForce = 7f;
    public float gravity = -20f;

    [Header("Mouse Look")]
    public Transform playerCamera;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;

    [Header("Animation")]
    public Animator animator;

    private CharacterController controller;
    

    private Vector3 velocity;
    private float cameraRotationX = 0f;

    private bool cursorLocked = true;

    [Header("Third Person Camera")]
public Vector3 thirdPersonPosition = new Vector3(0f, 2f, -4f);
public Vector3 scopePosition = new Vector3(0f, 1.6f, 0.2f);

public float normalFOV = 60f;
public float scopeFOV = 30f;

public float cameraMoveSpeed = 10f;

    void Start()
{
    // Get Character Controller
    controller = GetComponent<CharacterController>();

    // Automatically find Animator
    if (animator == null)
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Reset animation when the scene starts again
    if (animator != null)
    {
        animator.enabled = true;
        animator.Rebind();
        animator.Update(0f);

        animator.SetFloat("Speed", 0f);
        animator.ResetTrigger("Jump");
    }

    // Reset player movement
    velocity = Vector3.zero;
    cameraRotationX = 0f;

    // Start with cursor locked
    LockCursor();
}

void OnEnable()
{
    if (animator != null)
    {
        animator.enabled = true;
        animator.Rebind();
        animator.Update(0f);
    }
}

    void Update()
{
    HandleCursor();

    if (cursorLocked)
    {
        HandleMouseLook();
    }

    HandleMovement();
    HandleAnimations();
    HandleScope();
}

    // =====================================================
    // CURSOR
    // =====================================================

    void HandleCursor()
    {
        // Press ESC
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (cursorLocked)
            {
                UnlockCursor();
            }
            else
            {
                LockCursor();
            }

            return;
        }

        // If cursor is visible, check for a mouse click
        if (!cursorLocked &&
            Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            // If clicking a UI button:
            // DO NOT lock the cursor.
            if (EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // Clicked the game world
            LockCursor();
        }
    }

    void LockCursor()
    {
        cursorLocked = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        cursorLocked = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // =====================================================
    // MOVEMENT
    // =====================================================

    void HandleMovement()
    {
        if (Keyboard.current == null)
            return;

        float horizontal = 0f;
        float vertical = 0f;

        // A
        if (Keyboard.current.aKey.isPressed)
        {
            horizontal = -1f;
        }

        // D
        if (Keyboard.current.dKey.isPressed)
        {
            horizontal = 1f;
        }

        // W
        if (Keyboard.current.wKey.isPressed)
        {
            vertical = 1f;
        }

        // S
        if (Keyboard.current.sKey.isPressed)
        {
            vertical = -1f;
        }

        Vector3 move =
            transform.right * horizontal +
            transform.forward * vertical;

        // Prevent diagonal movement from being faster
        move = Vector3.ClampMagnitude(move, 1f);

        // -------------------------------------------------
        // WALK / RUN
        // -------------------------------------------------

        float speed = walkSpeed;

        if (Keyboard.current.leftShiftKey.isPressed)
        {
            speed = runSpeed;
        }

        controller.Move(
            move * speed * Time.deltaTime
        );

        // -------------------------------------------------
        // GROUND
        // -------------------------------------------------

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // -------------------------------------------------
        // JUMP
        // -------------------------------------------------

        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            controller.isGrounded)
        {
            velocity.y =
                Mathf.Sqrt(jumpForce * -2f * gravity);

            // Trigger jump animation
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }

        // -------------------------------------------------
        // GRAVITY
        // -------------------------------------------------

        velocity.y += gravity * Time.deltaTime;

        controller.Move(
            velocity * Time.deltaTime
        );
    }

    // =====================================================
    // MOUSE LOOK
    // =====================================================

    void HandleMouseLook()
    {
        if (Mouse.current == null)
            return;

        if (playerCamera == null)
            return;

        Vector2 mouseDelta =
            Mouse.current.delta.ReadValue();

        float mouseX =
            mouseDelta.x *
            mouseSensitivity *
            Time.deltaTime;

        float mouseY =
            mouseDelta.y *
            mouseSensitivity *
            Time.deltaTime;

        // -------------------------------------------------
        // LEFT / RIGHT
        // -------------------------------------------------

        transform.Rotate(
            Vector3.up * mouseX
        );

        // -------------------------------------------------
        // UP / DOWN
        // -------------------------------------------------

        cameraRotationX -= mouseY;

        cameraRotationX =
            Mathf.Clamp(
                cameraRotationX,
                -maxLookAngle,
                maxLookAngle
            );

        playerCamera.localRotation =
            Quaternion.Euler(
                cameraRotationX,
                0f,
                0f
            );
    }

    // =====================================================
    // ANIMATIONS
    // =====================================================

    void HandleAnimations()
    {
        if (animator == null)
            return;

        if (Keyboard.current == null)
            return;

        bool moving =
            Keyboard.current.wKey.isPressed ||
            Keyboard.current.aKey.isPressed ||
            Keyboard.current.sKey.isPressed ||
            Keyboard.current.dKey.isPressed;

        bool running =
            Keyboard.current.leftShiftKey.isPressed;

        // -------------------------------------------------
        // IDLE
        // -------------------------------------------------

        if (!moving)
        {
            animator.SetFloat("Speed", 0f);
        }

        // -------------------------------------------------
        // RUN
        // -------------------------------------------------

        else if (running)
        {
            animator.SetFloat("Speed", 9f);
        }

        // -------------------------------------------------
        // WALK
        // -------------------------------------------------

        else
        {
            animator.SetFloat("Speed", 5f);
        }
    }

    void OnGUI()
{
    if (animator == null)
        return;

    GUI.Label(
        new Rect(20, 20, 300, 30),
        "Animator Speed: " + animator.GetFloat("Speed").ToString("F2")
    );
}

void HandleScope()
{
    if (playerCamera == null)
        return;

    bool aiming = Mouse.current != null &&
                  Mouse.current.rightButton.isPressed;

    Vector3 targetPosition;

    float targetFOV;

    if (aiming)
    {
        targetPosition = scopePosition;
        targetFOV = scopeFOV;
    }
    else
    {
        targetPosition = thirdPersonPosition;
        targetFOV = normalFOV;
    }

    playerCamera.localPosition = Vector3.Lerp(
        playerCamera.localPosition,
        targetPosition,
        cameraMoveSpeed * Time.deltaTime
    );

    Camera cam = playerCamera.GetComponent<Camera>();

    if (cam != null)
    {
        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,
            targetFOV,
            cameraMoveSpeed * Time.deltaTime
        );
    }
}
}