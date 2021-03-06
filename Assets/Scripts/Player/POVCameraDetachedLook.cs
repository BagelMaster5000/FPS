﻿using UnityEngine;
using UnityEngine.InputSystem;

// For first person camera movement with smoothed camera.
// NOT to be attached to player object
public class POVCameraDetachedLook : MonoBehaviour
{
    InputMaster inputs;
    InputAction camRotation;

    [Header("Player Tracking")]
    [SerializeField] Transform playerBody;
    Vector3 posOffset;

    [Header("Speed and Smoothness")]
    [SerializeField] float baseLookSensitivity = 25;
    float curLookSensitivity;
    [SerializeField] float camMoveLerpFactor = 2;
    bool focused;
    [Range(0.01f, 1)]
    [SerializeField] float focusedSensitivityMultiplier = 0.5f;

    float curUpDownRotation = 0.0f;
    float curLeftRightRotation = 0.0f;

    private void Awake()
    {
        inputs = new InputMaster();
        camRotation = inputs.Game.Looking;
        camRotation.Enable();

        posOffset = transform.position - playerBody.position;
    }

    private void OnDisable() { camRotation.Disable(); }

    private void Start()
    {
        posOffset = transform.position - playerBody.position;
        curLookSensitivity = baseLookSensitivity;
        Application.targetFrameRate = 60; // FIXME Need this in debug object or elsewhere
    }

    private void Update()
    {
        if (PauseMenu.gamePaused) return;

        curLookSensitivity = (focused) ? baseLookSensitivity * focusedSensitivityMultiplier : baseLookSensitivity;


        curLeftRightRotation += camRotation.ReadValue<Vector2>().x * curLookSensitivity * Time.fixedDeltaTime;

        curUpDownRotation -= camRotation.ReadValue<Vector2>().y * curLookSensitivity * Time.fixedDeltaTime;
        curUpDownRotation = Mathf.Clamp(curUpDownRotation, -90f, 90f);
    }

    void LateUpdate()
    {
        playerBody.localRotation = Quaternion.Euler(0, curLeftRightRotation, 0); // Player body turning
        transform.localRotation = Quaternion.Euler(curUpDownRotation, curLeftRightRotation, 0); // Camera rotating

        // Smoothing camera position
        transform.position =
            (playerBody.position + posOffset) +
            (transform.position - (playerBody.position + posOffset)) /
            (camMoveLerpFactor / 50 / Time.unscaledDeltaTime) * ((camMoveLerpFactor / 50 / Time.unscaledDeltaTime) - 1);
    }

    #region Mutators and Accessors
    public void SetFocused(bool setFocused) => focused = setFocused;

    public float GetBaseLookSensitivity() { return baseLookSensitivity; }
    #endregion
}
