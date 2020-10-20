using UnityEngine;

// For first person camera movement with lerped camera.
// NOT to be attached to player object
public class Look : MonoBehaviour
{
    InputMaster inputs;

    const float BASE_LOOK_SENSITIVITY = 10;

    // Rotational values
    float upDownRot = 0.0f;
    float leftRightRot = 0.0f;

    // Camera lerping
    public Transform playerBody;
    public Vector3 posOffset;
    const float CAM_MOVE_LERP_FACTOR = 2;
    const float CAM_ROT_LERP_FACTOR = 5;

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
        Debug.Log(1 / CAM_ROT_LERP_FACTOR * 100 * Time.unscaledDeltaTime);
        // Smoothing player turning
        playerBody.localRotation = Quaternion.Euler(
            0,
            Mathf.LerpAngle(playerBody.eulerAngles.y, leftRightRot, Mathf.Clamp(1 / CAM_ROT_LERP_FACTOR * 100 * Time.unscaledDeltaTime, 0.01f, 1)),
            0);
        // Smooth camera rotation
        transform.localRotation = Quaternion.Euler(
            Mathf.LerpAngle(transform.eulerAngles.x, upDownRot, Mathf.Clamp(1 / CAM_ROT_LERP_FACTOR * 100 * Time.unscaledDeltaTime, 0.01f, 1)),
            Mathf.LerpAngle(playerBody.eulerAngles.y, leftRightRot, Mathf.Clamp(1 / CAM_ROT_LERP_FACTOR * 100 * Time.unscaledDeltaTime, 0.01f, 1)),
            0);
        // Smoothing player movement
        transform.position =
            (playerBody.position + posOffset) +
            (transform.position - (playerBody.position + posOffset)) /
            (CAM_MOVE_LERP_FACTOR / 50 / Time.unscaledDeltaTime) * ((CAM_MOVE_LERP_FACTOR / 50 / Time.unscaledDeltaTime) - 1);
    }

    /* Rotates camera when mouse is moved
     * @param lookDirection x and y delta of mouse 
     */
    void RotateCam(Vector2 lookDirection)
    {
        if (paused) return;

        leftRightRot += lookDirection.x * BASE_LOOK_SENSITIVITY / 10000 / Time.deltaTime;

        upDownRot -= lookDirection.y * BASE_LOOK_SENSITIVITY / 10000 / Time.deltaTime;
        upDownRot = Mathf.Clamp(upDownRot, -90f, 90f);
    }
}
