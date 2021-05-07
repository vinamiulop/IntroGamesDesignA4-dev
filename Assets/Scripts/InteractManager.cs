using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager instance;

    private FreeObj holdingFreeObj;
    
    private GameObject holdingPlacementGhost;
    [SerializeField]
    private Material ghostMat;

    public Transform holdTrans;
    public Transform freeObjectParent;

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

        if(holdTrans == null)
        {
            Debug.LogError("Hold position not set.");
        }
        if(freeObjectParent == null)
        {
            Debug.LogError("Free Objects Parent not set.");
        }
    }

    private void Start()    
    {
        InputManager.instance.mouseclickIA.performed += Onclick;   
    }

    private void Update() 
    {
        if(holdingPlacementGhost != null)
        {
            if(RaycastManager.instance.GetHoveringObjectInteract() == null)//if not pointing at another interactable
            {
                Vector3 ghostPosition;
                if(RaycastManager.instance.TryGetRaycastHitPosition(out ghostPosition))//if is hovering over a surface
                {
                    if(RaycastManager.instance.hitNormalIsUp())//if hover surfact normal is upwards
                    {
                        ghostPosition.y += 0.2f;
                        holdingPlacementGhost.transform.position = ghostPosition;
                        holdingPlacementGhost.transform.rotation = Quaternion.identity;
                        holdingPlacementGhost.SetActive(true);
                        
                        return;
                    }
                }
            }
            holdingPlacementGhost.SetActive(false);
        }
    }

    void Onclick(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Interactable hoverInteract = RaycastManager.instance.GetHoveringObjectInteract();
        if(hoverInteract == null)
        {
            if(holdingFreeObj != null && holdingPlacementGhost.activeSelf)
            {
                tweenObject(holdingFreeObj.gameObject, holdingPlacementGhost.transform.position, Quaternion.identity);
                holdingFreeObj.SetRigidbodyKinematic(false);
                holdingFreeObj.transform.parent = freeObjectParent;
                holdingFreeObj = null;
                Destroy(holdingPlacementGhost);
            }
            return;
        }
        if(hoverInteract.interactType() == Interactable.InteractType.freeObject)
        {    
            FreeObj hoverFreeobj = (FreeObj)hoverInteract;
            if(holdingFreeObj != null)
            {
                tweenObject(holdingFreeObj.gameObject, hoverFreeobj.transform.position + new Vector3(0,0.05f,0), hoverFreeobj.transform.rotation);
                holdingFreeObj.SetRigidbodyKinematic(false);
                holdingFreeObj.transform.parent = freeObjectParent;
            }
            holdingFreeObj = hoverFreeobj;
            changePlacementGhost(holdingFreeObj.gameObject);
            tweenObject(holdingFreeObj.gameObject, holdTrans);
            holdingFreeObj.SetRigidbodyKinematic(true);
            holdingFreeObj.transform.parent = holdTrans;
        }
    }

    void tweenObject(GameObject GOtoTween, Transform endPosTrans)
    {
        Tween newTween = GOtoTween.AddComponent<Tween>();
        newTween.endTrans = endPosTrans;   
        newTween.disableCollider = true; 
    }

    void tweenObject(GameObject GOtoTween, Vector3 endPosV3, Quaternion endRotQuat)
    {
        Tween newTween = GOtoTween.AddComponent<Tween>();
        newTween.endPos = endPosV3;
        newTween.endRot = endRotQuat;   
        newTween.disableCollider = true; 
    }

    void changePlacementGhost(GameObject ghostOriginObj)
    {
        Destroy(holdingPlacementGhost);
        holdingPlacementGhost = new GameObject("ghostObject");
        
        holdingPlacementGhost.transform.localScale = ghostOriginObj.transform.localScale;
       
        MeshFilter meshFil = holdingPlacementGhost.AddComponent<MeshFilter>();
        meshFil.mesh = ghostOriginObj.GetComponent<MeshFilter>().sharedMesh;

        MeshRenderer meshRend = holdingPlacementGhost.AddComponent<MeshRenderer>();

        Color ghostColor = ghostOriginObj.GetComponent<MeshRenderer>().material.color;
        ghostColor.a = 0.4f;
        Material tempGhostMat = new Material(ghostMat);
        tempGhostMat.color = ghostColor;
        meshRend.material = tempGhostMat;
    }
}
