using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class character_movement : MonoBehaviour
{
    PlayerControls controls;
    Rigidbody playerRigidbody;

    Vector2 moveByVector; //we will move player by this vector amount

    //multiplies the walking vector value by this float (to achieve sprinting)
    //default is 1
    float sprintMultiplier = 1;

    float moveMultiplier = 1.5f;

    bool isGrounded; //used for managing jumps and detecting if player is touching ground

    void Awake()
    {
        controls = new PlayerControls();
        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        //jumping
        controls.Gameplay.Jump.performed += context => Jump();

        //movement
        controls.Gameplay.Movement.performed += context => moveByVector = context.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += context => moveByVector = Vector2.zero;

        //sprinting
        controls.Gameplay.Sprint.performed += context => sprintMultiplier = 2; //if player held button down, starts sprinting (changes sprint multiplier)
        controls.Gameplay.Sprint.canceled += context => sprintMultiplier = 1; //if player released key, changes spring multiplier back to 1
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Vector2 (x,y)
        //L movement = (-1, y)
        //R movement = (1,y)

        //Up movement (x, 1)
        //Down movement (x, -1)
    }

    // Update is called once per frame
    void Update()
    {
        if (moveByVector.y > 0)
        {
            Vector3 input = new Vector3(moveByVector.x * sprintMultiplier * moveMultiplier, 0, moveByVector.y * sprintMultiplier * moveMultiplier);
            float cameraRot = Camera.main.transform.rotation.eulerAngles.y;
            gameObject.GetComponent<Rigidbody>().position += Quaternion.Euler(0, cameraRot, 0) * input * 1 * Time.deltaTime;

            gameObject.GetComponent<Animator>().SetBool("isWalking", true);

        }
        
        if(moveByVector.y < 0)
        {
            Vector3 input = new Vector3(-moveByVector.x * sprintMultiplier * moveMultiplier, 0, -moveByVector.y * sprintMultiplier * moveMultiplier);
            float cameraRot = Camera.main.transform.rotation.eulerAngles.y;
            gameObject.GetComponent<Rigidbody>().position -= Quaternion.Euler(0, cameraRot, 0) * input * 1 * Time.deltaTime;
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
        }

        if(moveByVector.y == 0)
        {
            gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        }


    }

    void Jump()
    {
        if(isGrounded)
        {
            //adds a small amount of jump force
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 3);  
        }
    }


    void OnCollisionEnter(Collision collision) 
    {
        //if player's collider hit (entered) the ground's collider, player is on the ground
        if(collision.gameObject.layer == LayerMask.NameToLayer("ground") || collision.gameObject.layer == LayerMask.NameToLayer("ground_ForPortal"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision) 
    {
        //if player's collider is leaving (exiting) the ground's collider, player is no longer grounded
        if(collision.gameObject.layer == LayerMask.NameToLayer("ground") || collision.gameObject.layer == LayerMask.NameToLayer("ground_ForPortal")) 
        {
            isGrounded = false;
        }
    }


    public void footstepSound()
    {
        gameObject.GetComponent<AudioSource>().Play();

        
    }


    void OnEnable() 
    {
        controls.Gameplay.Enable(); //enables controls when script is enabled
    }

    void OnDisable() 
    {
        controls.Gameplay.Disable(); //disables controls when script is disabled
    }
}
