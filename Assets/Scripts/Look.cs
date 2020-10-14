using System.Collections;
using UnityEngine;

// For first person camera movement with lerped camera.
public class Look : MonoBehaviour
{
    InputMaster inputs;

    public float lookSensitivity = 60;

    // Rotational values
    float upDownRot = 0.0f;
    float leftRightRot = 0.0f;

    // Camera lerping
    public Transform playerBody;
    public Vector3 posOffset;
    const float CAM_LERP_FACTOR = 2.5f;

    public static bool paused;

    private void Awake()
    {
        // Setting up inputs
        inputs = new InputMaster();
        inputs.Game.Looking.performed += ctx => RotateCam(ctx.ReadValue<Vector2>());
        inputs.Game.Looking.Enable();
    }

    private void OnDisable() { inputs.Game.Looking.Disable(); } // Disabling inputs

    private void Start()
    {
        // Hides and disables cursor at start of scene
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        posOffset = transform.position - playerBody.position;
    }

    void LateUpdate()
    {
        playerBody.localRotation = Quaternion.Euler(0, leftRightRot, 0);
        transform.localRotation = Quaternion.Euler(upDownRot, leftRightRot, 0);
        transform.position =
            //(goToPosition.position + posOffset); //No smoothing
            (playerBody.position + posOffset) + (transform.position - (playerBody.position + posOffset)) / CAM_LERP_FACTOR * (CAM_LERP_FACTOR - 1); // With smoothing
    }

    /* Rotates camera when mouse is moved
     * @param lookDirection x and y delta of mouse 
     */
    void RotateCam(Vector2 lookDirection)
    {
        if (paused) return;

        leftRightRot += lookDirection.x / 100.0f * lookSensitivity;

        upDownRot -= lookDirection.y / 100.0f * lookSensitivity;
        upDownRot = Mathf.Clamp(upDownRot, -90f, 90f);
    }
}
