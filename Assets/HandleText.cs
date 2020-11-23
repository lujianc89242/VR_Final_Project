using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleText : MonoBehaviour
{
    [SerializeField] Text messageText;
    [SerializeField] GameObject fadeCanvas;

    string[] messages;
    int onMessage = 0;

    // Start is called before the first frame update
    void Start()
    {
        messages = new string[3] { "You are safe now.", "Your journey will begin shortly.", "Sit back and enjoy the ride :)" };


    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator switchMessage()
    {
        if (onMessage < 3)
        {
            yield return new WaitForSeconds(4);
            messageText.text = messages[onMessage];
            onMessage++;
            StartCoroutine(switchMessage());
        }
        else
        {
            yield return new WaitForSeconds(2);
            fadeCanvas.SetActive(true);
        }
    }



    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            StartCoroutine(switchMessage());


        }


    }


}
