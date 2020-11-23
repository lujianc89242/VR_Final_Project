using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class missile : MonoBehaviour
{
    Vector3 startPos; //to be reset if player starts at beginning
    Vector3 endPos;
    
    float distance;
    float timeBetweenObjects;
    float speed = 25;

    public int time;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3(424.1f, -5.237f, -21.6f);
        endPos = new Vector3(-6.012f, -5.237f, -21.62f);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, endPos);
        timeBetweenObjects = distance / speed;

        transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);

        time = (int) (distance / speed);

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("secondLevel");
        }

        if(collision.gameObject.name == "specialDoor")
        {
            //destroys doors and activates particle effects for explosions and flames
            Destroy(collision.gameObject.transform.Find("door1").gameObject);
            Destroy(collision.gameObject.transform.Find("door2").gameObject);
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            collision.gameObject.transform.Find("BigExplosionEffect").gameObject.SetActive(true);
            collision.gameObject.transform.Find("WallFlames").gameObject.SetActive(true);

            Destroy(gameObject);
        }

        
    }

}
