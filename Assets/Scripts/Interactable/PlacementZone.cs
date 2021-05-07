using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementZone : Interactable
{
    public Transform placementTrans;

    public override void Start()
    {
        base._interactType = InteractType.placementZone;
        base.Start();
    }

    
}
