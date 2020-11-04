using UnityEngine;
using UnityEngine.InputSystem;

// For first person camera movement with lerped camera.
// NOT to be attached to player object
public class Look : MonoBehaviour
{
    InputMaster inputs;
    InputAction camRotation;

    [Header("Camera Movement")]
    [SerializeField] private float baseLookSensitivity = 25;
    [SerializeField] private float camMoveLerpFactor = 2;
    [SerializeField] private float camRotLerpFactor = 5;

    // Rotational values
    float upDownRot = 0.0f;
    float leftRightRot = 0.0f;

    // Camera lerping
    [SerializeField] private Transform playerBody;
    [SerializeField] private Vector3 posOffset;

    public static bool paused;

    private void Awake()
    {
        // Setting up inputs
        inputs = new InputMaster();
        camRotation = inputs.Game.Looking;
        camRotation.Enable();
    }

    private void OnDisable() { camRotation.Disable(); } // Disabling inputs

    private void Start()
    {
        posOffset = transform.position - playerBody.position;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (paused) return;

        //print(inputs.ControllerScheme);

        leftRightRot += camRotation.ReadValue<Vector2>().x * baseLookSensitivity * Time.deltaTime;

        upDownRot -= camRotation.ReadValue<Vector2>().y * baseLookSensitivity * Time.deltaTime;
        upDownRot = Mathf.Clamp(upDownRot, -90f, 90f);

        // Smoothing player turning
        playerBody.localRotation = Quaternion.Euler(
            0,
            Mathf.LerpAngle(playerBody.eulerAngles.y, leftRightRot, 1 / camRotLerpFactor),
            0);
        // Smooth camera rotation
        transform.localRotation = Quaternion.Euler(
            Mathf.LerpAngle(transform.eulerAngles.x, upDownRot, 1 / camRotLerpFactor),
            Mathf.LerpAngle(playerBody.eulerAngles.y, leftRightRot, 1 / camRotLerpFactor),
            0);
    }

    void LateUpdate()
    {
        // Debug.Log(1 / CAM_ROT_LERP_FACTOR * 100 * Time.unscaledDeltaTime);
        // Smoothing player movement
        transform.position =
            (playerBody.position + posOffset) +
            (transform.position - (playerBody.position + posOffset)) /
            (camMoveLerpFactor / 50 / Time.unscaledDeltaTime) * ((camMoveLerpFactor / 50 / Time.unscaledDeltaTime) - 1);
    }

    /* Rotates camera when mouse is moved
     * @param lookDirection x and y delta of mouse 
     */
    /*void RotateCam(Vector2 lookDirection)
    {
        if (paused) return;

        leftRightRot += lookDirection.x * BASE_LOOK_SENSITIVITY / 10000 / Time.deltaTime;

        upDownRot -= lookDirection.y * BASE_LOOK_SENSITIVITY / 10000 / Time.deltaTime;
        upDownRot = Mathf.Clamp(upDownRot, -90f, 90f);
    }*/
}
