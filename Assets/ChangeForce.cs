using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeForce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
  void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            PortalBehavior.portalExitForce = collider.gameObject.GetComponent<Rigidbody>().velocity;

            
        }


    }
}