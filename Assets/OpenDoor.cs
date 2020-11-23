using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] GameObject table;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openDoor()
    {
        doorAnimator.SetBool("character_nearby", true);
        table.SetActive(false);
    }

}
