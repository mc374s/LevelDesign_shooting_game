using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticleBulletSystem;

public class PlayerScript : MonoBehaviour {

    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 speedAcc = new Vector3(0.001f, 0.0005f, 0);
    public Vector3 speedMax = new Vector3(0.005f, 0.007f, 0);
    public Vector3 speedDrag = new Vector3(0.0005f, 0.0006f, 0);
    public int clusterNumber2 = 6;
    public Transform muzzule;
    public GameObject Bullet;

    private Vector3 leftBottom,rightTop;
    private float radius;

    // Use this for initialization
    void Start()
    {
        leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 3));
        rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 3));

        radius = GetComponent<SphereCollider>().radius;

        Debug.Log("rightTop.y : " + rightTop.y + "  rightTop.x : " + rightTop.x);
    }

    // Update is called once per frame
    void Update () {

        Move();
        if (Input.GetKeyDown(KeyCode.J))
        {
            Instantiate(Bullet, transform.position,transform.rotation);
            //StartCoroutine(Shot());
        }

    }

    void Move()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x += speedAcc.x;
            if (velocity.x > speedMax.x)
            {
                velocity.x = speedMax.x;
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            velocity.x -= speedAcc.y;
            if (velocity.x < -speedMax.x)
            {
                velocity.x = -speedMax.x;
            }
        }
        else {
            if (velocity.x > 0)
            {
                velocity.x -= speedDrag.x;
                if (velocity.x < 0)
                {
                    velocity.x = 0;
                }
            }
            if (velocity.x < 0)
            {
                velocity.x += speedDrag.x;
                if (velocity.x > 0)
                {
                    velocity.x = 0;
                }
            }
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            velocity.y += speedAcc.y;
            if (velocity.y > speedMax.y)
            {
                velocity.y = speedMax.y;
            }
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            velocity.y -= speedAcc.y;
            if (velocity.y < -speedMax.y)
            {
                velocity.y = -speedMax.y;
            }
        }
        else
        {
            if (velocity.y > 0)
            {
                velocity.y -= speedDrag.y;
                if (velocity.y < 0)
                {
                    velocity.y = 0;
                }
            }
            if (velocity.y < 0)
            {
                velocity.y += speedDrag.y;
                if (velocity.y > 0)
                {
                    velocity.y = 0;
                }
            }
        }


        transform.position += velocity;
        if (transform.position.x > rightTop.x - radius)
        {
            transform.position = new Vector3(rightTop.x - radius, transform.position.y, transform.position.z);
        }
        if (transform.position.x < leftBottom.x + radius)
        {
            transform.position = new Vector3(leftBottom.x + radius, transform.position.y, transform.position.z);
        }
        if (transform.position.y > rightTop.y - radius)
        {
            transform.position = new Vector3(transform.position.x, rightTop.y - radius, transform.position.z);
        }
        if (transform.position.y < leftBottom.y + radius)
        {
            transform.position = new Vector3(transform.position.x, leftBottom.y + radius, transform.position.z);
        }


        transform.rotation = Quaternion.Euler(0, -velocity.x * 2500, 0);
    }

    IEnumerator Shot()
    {

        WaitForSeconds wait = new WaitForSeconds(0.1f);
        for (int i = 0; i < 10; i++)
        {
            ClusterMissileRandom();
            yield return wait;
        }
        yield return new WaitForSeconds(1);
    }

    void ClusterMissileRandom()
    {
        muzzule.transform.rotation = Random.rotation;
        ParticleManager.manager.Emit(clusterNumber2, 1, muzzule.position, muzzule.rotation);
        Transform pt = ParticleManager.manager.particle[clusterNumber2].transform;
        pt.position = Vector3.zero;
        pt.eulerAngles = Vector3.zero;
    }
}
