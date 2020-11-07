﻿using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    InputMaster inputs;

    [Header("General Movement")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float speedIncreaser = 0;
    [SerializeField] private float timeMovingUntilFullSpeed = 0.5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3;
    Vector2 moveDirection;
    bool moving;
    bool movingBackwards;

    [Header("Jumping")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    // Events
    public event Action OnLanded;
    public event Action OnJumped;
    public event Action OnStartedMoving;
    public event Action OnStoppedMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputs = new InputMaster();
        inputs.Game.Moving.performed += ctx => RefreshPlayerSpeed(ctx.ReadValue<Vector2>());
        inputs.Game.Moving.performed += ctx => moving = true;
        inputs.Game.Moving.performed += ctx => OnStartedMoving?.Invoke();
        inputs.Game.Moving.canceled += ctx => RefreshPlayerSpeed(Vector2.zero);
        inputs.Game.Moving.canceled += ctx => moving = false;
        inputs.Game.Moving.canceled += ctx => OnStoppedMoving?.Invoke();
        inputs.Game.Jump.performed += ctx => Jump();
        inputs.Game.Moving.Enable();
        inputs.Game.Jump.Enable();
    }

    private void OnDisable() { inputs.Game.Moving.Disable(); inputs.Game.Jump.Disable(); }

    private void Start() { StartCoroutine(MovePlayer()); }

    /* Updates players current movement direction with input values.
     * Called with Vector2.zero when player stops moving.
     * @param direcion to set player's movement direction to
     */
    private void RefreshPlayerSpeed(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
        this.moveDirection.Normalize();
        movingBackwards = this.moveDirection.y < 0;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity += Vector3.up * Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHeight);
            OnJumped?.Invoke();
        }
    }

    IEnumerator MovePlayer()
    {
        bool oldIsGrounded;
        while (true)
        {
            // Ground checking
            oldIsGrounded = isGrounded;
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (!oldIsGrounded && isGrounded)
                OnLanded?.Invoke();

            speedIncreaser = moving ? Mathf.Clamp(speedIncreaser + Time.deltaTime / timeMovingUntilFullSpeed, 0, 1) : 0;
/*            if (speedIncreaser != 0)
                print(speedIncreaser);*/
            Vector3 move = (transform.right * moveDirection.x + transform.forward * moveDirection.y) * speed * speedIncreaser;
            move.y = rb.velocity.y;
            rb.velocity = move;

            yield return new WaitForFixedUpdate();
        }
    }

    #region Mutators and Accessors

    public void SetSpeed(float newSpeed) { speed = newSpeed; }

    public bool GetMoving() { return moving; }
    public bool GetMovingBackwards() { return movingBackwards; }
    public bool GetGrounded() { return isGrounded; }
    public float GetSpeed() { return speed; }

    #endregion
}
