using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
            transform.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = true;

            
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "grab_panel")
        {
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject.GetComponent<BoxCollider>());
            
            while(transform.childCount > 0 && transform.GetChild(0) != null)
            {
                GameObject selectedGO = transform.GetChild(0).gameObject;
                selectedGO.transform.parent = null;
                selectedGO.GetComponent<Rigidbody>().isKinematic = false;
                Destroy(selectedGO, 3);
            }
        }
    }

}
