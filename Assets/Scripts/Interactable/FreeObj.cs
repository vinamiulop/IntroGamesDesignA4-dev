using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeObj : Interactable
{
    public override void Start()
    {
        base._interactType = InteractType.freeObject;
        base.Start();
    }
    
    public void SetRigidbodyKinematic(bool enable)
    {
        Rigidbody rb;
        if((rb = GetComponent<Rigidbody>()) != null)
        {
            rb.isKinematic = enable;
        }
    }
}
