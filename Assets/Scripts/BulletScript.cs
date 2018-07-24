using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public Vector3 speed;
    public Vector3 speedAcc;
    public Vector3 speedMax;

    private Vector3 leftBottom, rightTop;

    // Use this for initialization
    void Start () {
        leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 3));
        rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 3));
        Destroy(gameObject, 10.0f);
	}
	
	// Update is called once per frame
	void Update () {
        speed += speedAcc;
        if (Mathf.Abs(speed.x) > Mathf.Abs(speedMax.x))
        {
            speed.x = (speed.x / Mathf.Abs(speed.x)) * speedMax.x;
        }
        if (Mathf.Abs(speed.y) > Mathf.Abs(speedMax.y))
        {
            speed.y = (speed.y / Mathf.Abs(speed.y)) * speedMax.y;
        }
        if (Mathf.Abs(speed.z) > Mathf.Abs(speedMax.z))
        {
            speed.z = (speed.z / Mathf.Abs(speed.z)) * speedMax.z;
        }
        transform.Translate(speed);
        if (transform.position.x > rightTop.x || transform.position.x < leftBottom.x || transform.position.y > rightTop.y || transform.position.y < leftBottom.y) {
            Destroy(gameObject);
        }
	}

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "ENEMY")
        {
            Debug.Log(collision.name);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    //public void OnCollisionEnter(Collision collision)
    //{
    //}

}
