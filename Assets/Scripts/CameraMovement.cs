using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float lookXSensitivity = 0.8f;
    public float lookYSensitivity = 0.8f;

    float XLook;
    float YLook;

    public InputAction lookAction;
    private Transform camTransform;

    private void Start() 
    {
        lookAction.performed += Look;
        camTransform = gameObject.GetComponent<Transform>();
    }

    private void OnEnable()
    {
        lookAction.Enable();
    }

    private void OnDisable() 
    {
        lookAction.Disable();    
    }
    
    public void Look(InputAction.CallbackContext context)
    {
        Vector2 lookDelta = context.ReadValue<Vector2>();
        XLook += lookDelta.x * lookXSensitivity;
        YLook -= lookDelta.y * lookYSensitivity;
        YLook = Mathf.Clamp(YLook, -90, 90);

        transform.rotation = Quaternion.Euler(YLook, XLook, 0);
    }
}
