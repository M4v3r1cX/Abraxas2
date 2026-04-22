using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 5f;
    float cameraVerticalRotation = 0f;
    bool lockedMouseCursor = true;
    bool visibleCursor = false;
    float topClamp = -90f;
    float bottomClamp = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, topClamp, bottomClamp);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);
    }
}
