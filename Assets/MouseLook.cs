using UnityEngine;

public class MouseLook : MonoBehaviour
{
    InputMaster inputs;
    public Transform playerBody;
    public float lookSensitivity = 60;

    float xRot = 0.0f;

    private void Awake()
    {
        inputs = new InputMaster();
        inputs.Game.Looking.performed += ctx => CamMoved(ctx.ReadValue<Vector2>());
        inputs.Game.Looking.Enable();
    }

    private void OnDisable() { inputs.Game.Looking.Disable(); }

    private void Start() { Cursor.lockState = CursorLockMode.Locked; }

    private void CamMoved(Vector2 lookDirection)
    {
        playerBody.Rotate(Vector3.up * lookDirection.x / 100.0f * lookSensitivity);

        xRot -= lookDirection.y / 100.0f * lookSensitivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }
}
