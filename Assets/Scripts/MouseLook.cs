using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float sensitivity = 1.5f;
    public float smoothing = 1.5f;

    private float xMousePos;
    private float smoothMousePos;

    private float currentLookingPos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput();
        ModifyInput();
        MovePlayer();
    }

    private void GetInput()
    {
        xMousePos = Input.GetAxisRaw("Mouse X");
    }

    private void ModifyInput()
    {
        xMousePos *= sensitivity * smoothing;
        smoothMousePos = Mathf.Lerp(smoothMousePos, xMousePos, 1f / smoothing);
    }

    private void MovePlayer()
    {
        currentLookingPos += smoothMousePos;
        transform.localRotation = Quaternion.AngleAxis(currentLookingPos, transform.up);
    }
}
