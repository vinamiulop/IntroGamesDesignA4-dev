using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public InputAction mouseclickIA;
    public InputAction escKeyIA;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }    
        else
        {
            Destroy(this);
        }

        mouseclickIA.Enable();
        escKeyIA.Enable();
    }

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true; 
    }
}
