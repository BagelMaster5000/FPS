using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    InputMaster inputs;

    [Header("General Movement")]
    [SerializeField] float speed = 5;
    float speedSmoothStartMultiplier = 0;
    [SerializeField] float speedSmoothStartDuration = 0.5f;
    [SerializeField] float jumpHeight = 3;
    Vector2 curMoveDirection;
    bool moving;
    bool movingBackwards;

    [Header("Jumping")]
    [SerializeField] Transform groundChecker;
    [SerializeField] float groundCheckDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    bool grounded;

    // Events
    public event Action OnLanded;
    public event Action OnJumped;
    public event Action OnStartedMoving;
    public event Action OnStoppedMoving;

    #region Inputs
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputs = new InputMaster();
        inputs.Game.Moving.performed += ctx => SpeedRefresh(ctx.ReadValue<Vector2>());
        inputs.Game.Moving.performed += ctx => moving = true;
        inputs.Game.Moving.performed += ctx => OnStartedMoving?.Invoke();
        inputs.Game.Moving.canceled += ctx => SpeedRefresh(Vector2.zero);
        inputs.Game.Moving.canceled += ctx => moving = false;
        inputs.Game.Moving.canceled += ctx => OnStoppedMoving?.Invoke();
        inputs.Game.Jump.performed += ctx => Jump();
        inputs.Game.Moving.Enable();
        inputs.Game.Jump.Enable();
    }

    private void OnDisable() { inputs.Game.Moving.Disable(); inputs.Game.Jump.Disable(); }
    #endregion

    private void Start() { StartCoroutine(MovementAndGroundedLoop()); }

    #region Jumping
    private void Jump()
    {
        if (grounded)
        {
            rb.velocity += Vector3.up * Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHeight);
            OnJumped?.Invoke();
        }
    }

    private void GroundedRefresh(ref bool prevGrounded)
    {
        prevGrounded = grounded;
        grounded = Physics.CheckSphere(groundChecker.position, groundCheckDistance, groundMask);
        if (!prevGrounded && grounded)
            OnLanded?.Invoke();
    }
    #endregion

    #region Directional Movement
    /* Updates players current movement direction with input values.
     * Called with Vector2.zero when player stops moving.
     * @param direcion to set player's movement direction to
     */
    private void SpeedRefresh(Vector2 moveDirection)
    {
        this.curMoveDirection = moveDirection;
        this.curMoveDirection.Normalize();
        movingBackwards = this.curMoveDirection.y < 0;
    }

    IEnumerator MovementAndGroundedLoop()
    {
        bool prevGrounded = false;
        while (true)
        {
            GroundedRefresh(ref prevGrounded);

            Move();

            yield return new WaitForFixedUpdate();
        }
    }

    // Players speed linearly increases to max from 0 as direction is pressed
    private void Move()
    {
        speedSmoothStartMultiplier = moving ?
            Mathf.Clamp(speedSmoothStartMultiplier + Time.deltaTime / speedSmoothStartDuration, 0, 1) :
            0;
        rb.velocity = (transform.right * curMoveDirection.x + transform.forward * curMoveDirection.y) * speed * speedSmoothStartMultiplier +
            Vector3.up * rb.velocity.y;
    }
    #endregion

    #region Mutators and Accessors
    public void SetSpeed(float newSpeed) { speed = newSpeed; }

    public bool GetMoving() { return moving; }
    public bool GetMovingBackwards() { return movingBackwards; }
    public bool GetGrounded() { return grounded; }
    public float GetSpeed() { return speed; }
    #endregion
}
