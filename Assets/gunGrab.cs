using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunGrab : MonoBehaviour
{
    PlayerControls controls;
    [SerializeField] Text pickupText; //will prompt player if they want to pick up an object

    [SerializeField] Transform moveToPos; //move to the gun's position

    bool isGrabbing;

    //so we can only have one prompt/one GO if we are looking at multiple colliders
    //ex: if there are two boxes with colliders next to each other, we only want to pick up the first box
    //this bool will make sure only one GO can be prompted/interacted with at once
    bool isViewing;

    GameObject grabbingObject; //which object is player grabbing?

    float t; //will be used to lerp the object's position from wherever it is to the moveToPos transform
    
    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Gameplay.Grab.performed += context => isGrabbing = true; //if player held button down, starts sprinting (changes sprint multiplier)
        controls.Gameplay.Grab.canceled += context => isGrabbing = false; //if player released key, changes spring multiplier back to 1



    }

    // Update is called once per frame
    void Update()
    {
        
        //if a GO is in our view and we want to pick it up
        if(isViewing && isGrabbing)
        {
            //code to fire once
            if(pickupText.gameObject.activeSelf)
            {
                pickupText.gameObject.SetActive(false);
                grabbingObject.GetComponent<Rigidbody>().isKinematic = true;

                //plays gun grab sound
                //turns on particle effects
                transform.GetChild(0).GetComponent<AudioSource>().Play();
                transform.GetChild(0).Find("grabPos").Find("hold").GetComponent<ParticleSystem>().Play();
                transform.GetChild(0).Find("grabPos").Find("hold").GetChild(0).GetComponent<ParticleSystem>().Play();

            }

            //for putting the object we want to grab in our "moveToPos" transform
            if (grabbingObject.transform.parent != moveToPos.transform)
            {
                grabbingObject.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0.5f, 0));

                //for move operations -- checking to see distance between object we want to grab and the grabPos on the gun
                if (Mathf.Abs(Vector3.Distance(moveToPos.position, grabbingObject.transform.position)) >= 0.05f)
                {
                    lerpToPos();
                }
                //if player is really close to object
                else
                {
                    grabbingObject.transform.parent = moveToPos.transform;
                    grabbingObject.transform.localPosition = Vector3.zero;
                }
            }
        }

        //for dropping the object
        if(!isGrabbing && grabbingObject != null && grabbingObject.transform.parent == moveToPos.transform)
        {
            grabbingObject.transform.parent = null;
            grabbingObject.GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(0).GetComponent<AudioSource>().Stop();
            transform.GetChild(0).Find("grabPos").Find("hold").GetComponent<ParticleSystem>().Stop();
            transform.GetChild(0).Find("grabPos").Find("hold").GetChild(0).GetComponent<ParticleSystem>().Stop();
        }

        
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == LayerMask.NameToLayer("grabable"))
        {
            isViewing = true;
            grabbingObject = collider.gameObject;
            pickupText.gameObject.SetActive(true);
        }

        
    }


    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("grabable"))
        {
            isViewing = false;
            grabbingObject = null;
            pickupText.gameObject.SetActive(false);
        }
    }



    void lerpToPos()
    {
        if(grabbingObject != null)
        {
            grabbingObject.transform.position = Vector3.Lerp(grabbingObject.transform.position, moveToPos.transform.position, t);
            grabbingObject.transform.rotation = Quaternion.RotateTowards(grabbingObject.transform.rotation, moveToPos.transform.rotation, t * 3);

            t += Time.deltaTime;
            
            if(Mathf.Abs(Vector3.Distance(moveToPos.position, grabbingObject.transform.position)) < 0.05f)
            {
                grabbingObject.transform.parent = moveToPos.transform;

            }
            
        }

        
    }



}
