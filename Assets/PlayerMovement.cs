using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    InputMaster inputs;

    public float speed = 5;
    public float gravity = -9.8f;
    public float jumpHeight = 3;

    Vector2 moveDirection;
    bool moving;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputs = new InputMaster();
        inputs.Game.Moving.performed += ctx => RefreshPlayerSpeed(ctx.ReadValue<Vector2>());
        inputs.Game.Moving.performed += ctx => moving = true;
        inputs.Game.Moving.canceled += ctx => RefreshPlayerSpeed(Vector2.zero);
        inputs.Game.Moving.canceled += ctx => moving = false;
        inputs.Game.Jump.performed += ctx => Jump();
        inputs.Game.Moving.Enable();
        inputs.Game.Jump.Enable();
    }

    private void OnDisable() { inputs.Game.Moving.Disable(); inputs.Game.Jump.Disable(); }

    private void Start() { StartCoroutine(MovePlayer()); }

    private void RefreshPlayerSpeed(Vector2 moveDirection) { this.moveDirection = moveDirection; }

    private void Jump()
    {
        if (isGrounded)
            velocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
    }

    IEnumerator MovePlayer()
    {
        while (true)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            
            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            Vector3 move = transform.right * moveDirection.x + transform.forward * moveDirection.y;
            controller.Move(move / 100.0f * speed);

            velocity.y += gravity / 100.0f + Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            yield return new WaitForFixedUpdate();
        }
    }


    public bool IsMoving() { return moving; }

    public bool IsGrounded() { return isGrounded; }

    public float GetSpeed() { return speed; }

    public void SetSpeed(float newSpeed) { speed = newSpeed; }
}
