using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public static RaycastManager instance;
    
    private Vector3 castHitPosition;
    private Vector3 hitNormal;
    private bool rayHitting;

    RaycastHit hitInfo;
    Interactable HoveringObjectInteract;

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
    }
        
    private void Start() 
    {
    }

    void Update()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2)), out hitInfo, 50);
        if(hitInfo.collider != null)
        {
            castHitPosition = hitInfo.point;
            hitNormal = hitInfo.normal;
            rayHitting = true;
            HoverUpdate(hitInfo.collider.GetComponent<Interactable>());
        }
        else
        {
            rayHitting = false;
            HoverUpdate(null);
        }
    }

    private void HoverUpdate(Interactable hoverInt)
    {
        if(hoverInt == null) //If now hovering over nothing
        {
            if(HoveringObjectInteract == null) //If originallty hovering over nothing already
            {
                return;
            }
            else //Hover from something to nothing
            {
                HoveringObjectInteract.HoverExit();
                HoveringObjectInteract = hoverInt;
            }
        }
        else //If now hovering over something
        {
            if(HoveringObjectInteract == null)
            {
                HoveringObjectInteract = hoverInt;
                HoveringObjectInteract.HoverEnter();
            }
            else if(HoveringObjectInteract == hoverInt)
            {
                
            }
            else
            {
                HoveringObjectInteract.HoverExit();
                HoveringObjectInteract = hoverInt;
                HoveringObjectInteract.HoverEnter();
            }
        }
    }

    public Interactable GetHoveringObjectInteract()
    {
        if(HoveringObjectInteract != null)
        {
            return HoveringObjectInteract;
        }
        return null;
    }

    public bool TryGetRaycastHitPosition(out Vector3 position)
    {
        position = castHitPosition;
        if(rayHitting)
        {
            return true;
        }
        return false;
    }

    public bool hitNormalIsUp()
    {
        return Vector3.Angle(hitNormal, Vector3.up) < 5f;
    }
}
