using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    //naming doesnt matter because player will be able to enter/exit through both types, but for simplicity
    [SerializeField] GameObject enterPortal;
    [SerializeField] GameObject exitPortal;
    [SerializeField] Transform gunFirePos; //the (local) position that gun will be firing from

    [SerializeField] AudioClip gunShoot;

    GameObject firstPortal;
    GameObject secondPortal;

    RaycastHit raycastHit;

    PlayerControls controls;

    
    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Gameplay.ShootFirstPortal.performed += _ => shootPortal(true);
        controls.Gameplay.ShootSecondPortal.performed += _ => shootPortal(false);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void shootPortal(bool isFirst)
    {
        if (Physics.Raycast(gunFirePos.position, gunFirePos.forward, out raycastHit))
        {
            //plays gun shot sound
            //turns on gun particle effects
            gameObject.GetComponent<AudioSource>().PlayOneShot(gunShoot);
            gameObject.transform.Find("grabPos").Find("shoot").GetComponent<ParticleSystem>().Play();
            foreach (Transform x in gameObject.transform.Find("grabPos").Find("shoot"))
            {
                x.GetComponent<ParticleSystem>().Play();
            }


            //if player fires a shot and portal exists, destroys it
            if(isFirst && firstPortal != null)
            {
                Destroy(firstPortal);
            }
            if(!isFirst && secondPortal != null)
            {
                Destroy(secondPortal);
            }


            //wall portal
            if(raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("wall_ForPortal"))
            {
                //first portal creation
                if(isFirst)
                {
                    firstPortal = Instantiate(enterPortal);
                    firstPortal.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, raycastHit.collider.transform.localEulerAngles.y));
                    firstPortal.transform.position = new Vector3(raycastHit.point.x - 0.02f, raycastHit.point.y, raycastHit.point.z);
                    firstPortal.name = "portal1";
                }
                //second portal
                else
                {
                    secondPortal = Instantiate(exitPortal);
                    secondPortal.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, raycastHit.collider.transform.localEulerAngles.y));
                    secondPortal.transform.position = new Vector3(raycastHit.point.x - 0.02f, raycastHit.point.y, raycastHit.point.z);
                    secondPortal.name = "portal2";
                }

            }

            //floor portal
            else if(raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("ground_ForPortal"))
            {
                if(isFirst)
                {
                    firstPortal = Instantiate(enterPortal);
                    firstPortal.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    firstPortal.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y + 0.05f, raycastHit.point.z);
                    firstPortal.name = "portal1";
                }
                else
                {
                    secondPortal = Instantiate(exitPortal);
                    secondPortal.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    secondPortal.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y + 0.05f, raycastHit.point.z);
                    secondPortal.name = "portal2";
                }
            }

            //ceiling portal
            else if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("ceiling_ForPortal"))
            {
                if(isFirst)
                {
                    firstPortal = Instantiate(enterPortal);
                    firstPortal.transform.rotation = Quaternion.Euler(new Vector3(-180, 90, 0));
                    firstPortal.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y - 0.05f, raycastHit.point.z);
                    firstPortal.name = "portal1";
                }

                else
                {
                    secondPortal = Instantiate(exitPortal);
                    secondPortal.transform.rotation = Quaternion.Euler(new Vector3(-180, 90, 0));
                    secondPortal.transform.position = new Vector3(raycastHit.point.x, raycastHit.point.y - 0.05f, raycastHit.point.z);
                    secondPortal.name = "portal2";
                }


            }






        }
    }


}
