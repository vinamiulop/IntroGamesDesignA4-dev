using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    public Transform endTrans;
    public Vector3 endPos;
    public Quaternion endRot;

    public bool disableCollider;

    private float delta = 0;

    private void Start() 
    {
        startPos = transform.position;
        startRot = transform.rotation;    

        if(disableCollider)
        {
            if(GetComponent<Collider>() != null)
            {
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    void Update()
    {
        if(endTrans != null) //update endPos if needed
        {
            endPos = endTrans.position;
            endRot = endTrans.rotation;
        }
        
        if(delta > 0.95f) //set and delete self when reached end position
        {
            transform.position = endPos;

            if(GetComponent<Collider>() != null)
            {
                GetComponent<Collider>().enabled = true;
            }

            Destroy(this);
        }

        //move and update delta
        transform.position = Vector3.Lerp(startPos, endPos, delta);
        transform.rotation = Quaternion.Slerp(startRot, endRot, delta);
        delta += Time.deltaTime*4;
    }
}
