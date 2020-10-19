using UnityEngine;

// For first person camera movement with lerped camera.
// NOT to be attached to player object
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
    const float CAM_MOVE_LERP_FACTOR = 10;
    const float CAM_ROT_LERP_FACTOR = 9;

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
        posOffset = transform.position - playerBody.position;
    }

    void LateUpdate()
    {
/*        print("playerBody.eulerAngles.y = " + playerBody.eulerAngles.y);
        print("leftRightRot = " + leftRightRot);*/
        playerBody.localRotation = Quaternion.Euler(0, Mathf.LerpAngle(playerBody.eulerAngles.y, leftRightRot, 1 / CAM_ROT_LERP_FACTOR), 0);
        //playerBody.localRotation = Quaternion.Euler(0, leftRightRot, 0);
        transform.localRotation = Quaternion.Euler(Mathf.LerpAngle(transform.eulerAngles.x, upDownRot, 1 / CAM_ROT_LERP_FACTOR), Mathf.LerpAngle(playerBody.eulerAngles.y, leftRightRot, 1 / CAM_ROT_LERP_FACTOR), 0);
        transform.position =
            //(goToPosition.position + posOffset); //No smoothing
            (playerBody.position + posOffset) + (transform.position - (playerBody.position + posOffset)) / CAM_MOVE_LERP_FACTOR * (CAM_MOVE_LERP_FACTOR - 1); // With smoothing
    }

    /* Rotates camera when mouse is moved
     * @param lookDirection x and y delta of mouse 
     */
    void RotateCam(Vector2 lookDirection)
    {
        if (paused) return;

        leftRightRot += lookDirection.x * lookSensitivity * Time.fixedDeltaTime;

        upDownRot -= lookDirection.y * lookSensitivity * Time.fixedDeltaTime;
        upDownRot = Mathf.Clamp(upDownRot, -90f, 90f);
    }
}
