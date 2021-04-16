using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandInteraction : MonoBehaviour {

    public SteamVR_Action_Boolean m_GrabAction = null;
    public GameObject rightHandLoc = null;
    public GameObject leftHandLoc = null;
    public SteamVR_Behaviour_Pose rightHand = null;
    private SteamVR_Behaviour_Pose m_Pose = null;

    private FixedJoint m_Joint = null;
    private Interactable m_CurrentInteractable = null;
    private bool touchingPlate = false; //Am i touching the plate stack?
    private bool touchingInsert = false; //Am i touching an insert 
    public List<Interactable> m_ContactInteractable = new List<Interactable>();

    //Instantiate objects
    public GameObject plate;
    private GameObject food = null;

    private void Awake() {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }


    private void Update() {
        //Checks to see the nearest interactable every frame
        //Down
        if (m_GrabAction.GetStateDown(m_Pose.inputSource)) {
            //If user is interacting with the plate stack
            if (touchingPlate) {
                SpawnObj(plate);
            }
            else if (touchingInsert)
            {
                SpawnObj(food);
            }
            else 
            {
                Pickup();
            }
            
        }

        if (m_GrabAction.GetStateUp(m_Pose.inputSource)) {
            Drop();
        }
    }
    private void OnTriggerEnter(Collider other) {
        
        //Am i currently touching the plate stack?
        if(other.GetComponent<PlateStack>())
        {
            touchingPlate = true;
            return;
        }
        
        else if (other.GetComponent<FoodInsert>())
        {
            touchingInsert = true;
            food = other.GetComponent<FoodInsert>().food;
            return;
        }
        else if (other.GetComponent<Interactable>() == null) {
            return;
        }

        m_ContactInteractable.Add(other.gameObject.GetComponent<Interactable>());
       
    }
    public void OnTriggerExit(Collider other) {
        //Did i leave the plate stack?
        if(other.GetComponent<PlateStack>())
        {
            touchingPlate = false;
            return;
        }
        else if (other.GetComponent<FoodInsert>())
        {
            touchingInsert = false;
            food = null;
            return;
        }
        else if (other.GetComponent<Interactable>() == null) {
            return;
        }

        m_ContactInteractable.Remove(other.gameObject.GetComponent<Interactable>());
    } 
    public void Pickup() {
        m_CurrentInteractable = GetNearestInteractable();
        //Null Check
        if (!m_CurrentInteractable) {
            return;
        }

        //Already Held
        if(m_CurrentInteractable.m_ActiveHand) {
            m_CurrentInteractable.m_ActiveHand.Drop();
        }


        //Position
        //Check if its the righthand picking up the object
        if (m_Pose.inputSource == rightHand.inputSource) {
            m_CurrentInteractable.transform.position = rightHandLoc.transform.position;
        }
        //Check if its the lefthand picking up the object
        else {
            m_CurrentInteractable.transform.position = leftHandLoc.transform.position;
        }
        //Attach fixed joint
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        //Set active hand
        m_CurrentInteractable.m_ActiveHand = this;

    }
    public void Drop() {
        //Null Check
        if (!m_CurrentInteractable) {
            return;
        }

        //Apply Velocity
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();
        //Detach
        m_Joint.connectedBody = null;
        //Clear
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
    }

    private Interactable GetNearestInteractable() {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0f;

        foreach(Interactable interactable in m_ContactInteractable)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance) {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }

    //Am i spawning a plate?
    private void SpawnObj(GameObject spawn) {
        if (spawn ==null)
        {
            return;
        }
        GameObject objSpawn;
        if (m_Pose.inputSource == rightHand.inputSource) {
            objSpawn = Instantiate(spawn, rightHandLoc.transform.position, Quaternion.identity);
        }
        //Check if its the lefthand picking up the object
        else {
           objSpawn = Instantiate(spawn, leftHandLoc.transform.position, Quaternion.identity);
        }
        
       m_CurrentInteractable = objSpawn.GetComponent<Interactable>();

       Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        m_CurrentInteractable.m_ActiveHand = this;
    }
}
