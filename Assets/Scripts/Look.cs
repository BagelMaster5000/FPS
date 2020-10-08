using System.Collections;
using UnityEngine;

public class Look : MonoBehaviour
{
    InputMaster inputs;
    public Transform playerBody;
    public float lookSensitivity = 60;

    float upDownRot = 0.0f;
    float leftRightRot = 0.0f;

    public static bool paused;

    private void Awake()
    {
        // Setting up inputs
        inputs = new InputMaster();
        inputs.Game.Looking.performed += ctx => StartCoroutine(RotateCam(ctx.ReadValue<Vector2>()));
        //inputs.Game.Looking.performed += ctx => CamMoved(ctx.ReadValue<Vector2>());
        inputs.Game.Looking.Enable();
    }

    private void OnDisable() { inputs.Game.Looking.Disable(); } // Disabling inputs

    private void Start()
    {
        // Hides and disables cursor at start of scene
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        playerBody.localRotation = Quaternion.Euler(0, leftRightRot, 0);
        transform.localRotation = Quaternion.Euler(upDownRot, 0, 0);
    }

    /* Rotates camera when mouse is moved
     * @param lookDirection x and y delta of mouse 
     */
    /*private void CamMoved(Vector2 lookDirection)
    {
        if (paused)
            return;

        playerBody.Rotate(Vector3.up * lookDirection.x / 100.0f * lookSensitivity);

        upDownRot -= lookDirection.y / 100.0f * lookSensitivity;
        upDownRot = Mathf.Clamp(upDownRot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }*/

    /* Rotates camera when mouse is moved
     * @param lookDirection x and y delta of mouse 
     */
    IEnumerator RotateCam(Vector2 lookDirection)
    {
        yield return new WaitForFixedUpdate();
        if (paused)
            yield break;

        leftRightRot += lookDirection.x / 100.0f * lookSensitivity;
        //playerBody.Rotate(Vector3.up * lookDirection.x / 100.0f * lookSensitivity);

        upDownRot -= lookDirection.y / 100.0f * lookSensitivity;
        upDownRot = Mathf.Clamp(upDownRot, -90f, 90f);
        //transform.localRotation = Quaternion.Euler(upDownRot, 0, 0);
    }
}
