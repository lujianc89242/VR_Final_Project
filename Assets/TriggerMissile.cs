using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerMissile : MonoBehaviour
{
    [SerializeField] GameObject missile;
    [SerializeField] Animator lowerDoor;
    [SerializeField] Text secondsText;

    //handle timer on screen as well


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lowerDoor.enabled && missile != null && missile.activeSelf)
        {
            secondsText.text = missile.GetComponent<missile>().time.ToString();
        }

    }


    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            lowerDoor.enabled = true;
            missile.SetActive(true);
            lowerDoor.GetComponent<AudioSource>().Play();
            
        }


        
    }

}
