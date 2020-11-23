using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    Color materialColor;
    [SerializeField] GameObject openDoor;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
    }

    void OnTriggerEnter(Collider collider)
    {
        //activating door/button
        if (collider.gameObject.layer == LayerMask.NameToLayer("grabable"))
        {
            StartCoroutine(waitToPlay());
            openDoor.GetComponent<Animator>().SetBool("openDoor", true);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);

            transform.parent.GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator waitToPlay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        openDoor.GetComponent<AudioSource>().Play();

    }


    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("grabable"))
        {
            openDoor.GetComponent<Animator>().SetBool("openDoor", false);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
        }

    }



}
