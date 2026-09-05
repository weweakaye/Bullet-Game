using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    public Camera playerCamera;

    public float normalFOV = 60f;
    public float zoomFOV = 35f;
    public float zoomSpeed = 10f;

    void Update()
    {
        if (playerCamera == null)
            return;

        bool zooming = false;

        if (Mouse.current != null)
        {
            zooming = Mouse.current.rightButton.isPressed;
        }

        float targetFOV;

        if (zooming)
        {
            targetFOV = zoomFOV;
        }
        else
        {
            targetFOV = normalFOV;
        }

        playerCamera.fieldOfView = Mathf.Lerp(
            playerCamera.fieldOfView,
            targetFOV,
            zoomSpeed * Time.deltaTime
        );
    }
}