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
        Application.targetFrameRate = 120;
    }

    private void Update()
    {
        if (paused) return;

        leftRightRot += camRotation.ReadValue<Vector2>().x * baseLookSensitivity * Time.deltaTime;

        upDownRot -= camRotation.ReadValue<Vector2>().y * baseLookSensitivity * Time.deltaTime;
        upDownRot = Mathf.Clamp(upDownRot, -90f, 90f);
    }

    void LateUpdate()
    {
        playerBody.localRotation = Quaternion.Euler(0, leftRightRot, 0); // Player turning
        transform.localRotation = Quaternion.Euler(upDownRot, leftRightRot, 0); // Camera rotation

        // Smoothing camera position
        transform.position =
            (playerBody.position + posOffset) +
            (transform.position - (playerBody.position + posOffset)) /
            (camMoveLerpFactor / 50 / Time.unscaledDeltaTime) * ((camMoveLerpFactor / 50 / Time.unscaledDeltaTime) - 1);
    }
}
