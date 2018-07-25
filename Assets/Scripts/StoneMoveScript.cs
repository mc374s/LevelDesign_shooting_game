using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMoveScript : MonoBehaviour {

    public Vector3 velocity = new Vector3(0, -0.005f, 0);
    public Vector3 rotateSpeed = new Vector3(0.2f, 0.2f, 0.2f);
    public int HP = 20;
    public GameObject explosion;
    public GameObject explosionSmall;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position += velocity;
        gameObject.transform.Rotate(rotateSpeed);
        if (gameObject.transform.position.y < GameManagerScript.leftBottom.y - 1f || transform.position.x < GameManagerScript.leftBottom.x - 1f || transform.position.x > GameManagerScript.rightTop.x + 1f)
        {
            Destroy(gameObject);
        }
        if (HP < 0)
        {
            if (explosion)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "STONE")
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(transform.rotation.eulerAngles.x / 50, transform.rotation.eulerAngles.y / 50, 0));
            other.GetComponent<Rigidbody>().AddForce(new Vector3(other.transform.rotation.eulerAngles.x / 50, other.transform.rotation.eulerAngles.y / 50, 0));
        }
        if (other.tag == "BULLET")
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0.5f, 0));
        }
        if(other.tag=="UNIT")
        {
            HP--;
            if (explosionSmall)
            {
                Instantiate(explosionSmall, other.transform.position, other.transform.rotation);
            }
            PlayerScript.energie -= 2;
        }
    }

}
