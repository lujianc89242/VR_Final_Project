using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFailling : MonoBehaviour
{
    Color materialColor;
    [SerializeField] AudioClip SoundToPlay;
    [SerializeField] float Volume;
    AudioSource audio;
    public bool alreadyPlayed = false;
    [SerializeField] List<GameObject> FallingObjects;
    [SerializeField] List<GameObject> DestroyObjects;

    // Start is called before the first frame update
    void Start()
    {
        // gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        // gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        //activating door/button
        if(collider.gameObject.tag == "Player")
        {
            if(!alreadyPlayed)
            {
                audio.PlayOneShot(SoundToPlay, Volume);
                alreadyPlayed = true;
            }
            StartCoroutine(waitToPlay());
            foreach (var item in FallingObjects)
            {
                item.AddComponent<Rigidbody>();
            }
            foreach (var item in DestroyObjects)
            {
                Destroy(item.gameObject);
            }
            //openDoor.GetComponent<Animator>().SetBool("openDoor", true);
            //gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);

            //transform.parent.GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator waitToPlay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        //openDoor.GetComponent<AudioSource>().Play();
    }

}
