using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public enum InteractType {notSet, freeObject, placementZone};

    protected InteractType _interactType;

    public virtual void Start() 
    {

    }

    public void HoverEnter()
    {
        Debug.Log("Hover Enter:" + gameObject.name);
    }

    public void HoverExit()
    {
        Debug.Log("Hover Exit:" + gameObject.name);
    }

    public InteractType interactType()
    {
        return _interactType;
    }
}
