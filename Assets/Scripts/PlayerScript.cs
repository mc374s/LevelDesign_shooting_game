using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticleBulletSystem;
using UnityEngine.SceneManagement;

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
    
    public SimpleHealthBar healthBar;
    public int hpMax;
    public static int HP;
    public SimpleHealthBar energieBar;
    public int energieMax;
    public static int energie = 0;
    public GameObject explosion;
    public GameObject damageEff;

    public GameObject units;
    public GameObject[] unit;

    // Use this for initialization
    void Start()
    {
        //leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 3));
        //rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 3));
        leftBottom = GameManagerScript.leftBottom;
        rightTop = GameManagerScript.rightTop;

        radius = GetComponent<SphereCollider>().radius;
        
        Debug.Log("rightTop.y : " + rightTop.y + "  rightTop.x : " + rightTop.x);
        HP = hpMax;
    }

    // Update is called once per frame
    void Update () {

        Move();
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject clone = Instantiate(Bullet, transform.position, transform.rotation);
            //clone.transform.Rotate(0, 0, Random.Range(-30, 30));

            //GameObject center=Instantiate(Bullet, transform.position, transform.rotation);
            //center.transform.rotation = Quaternion.Euler(0, 0, 30);
            //Instantiate(Bullet, transform.position, transform.rotation);
            //StartCoroutine(Shot());
        }

        if (Input.GetKeyDown(KeyCode.K))
        {

            units.transform.Translate(new Vector3(0, 0.2f, 0));
            for (int i = 0, max = unit.Length; i < max; ++i)
            {
                unit[i].transform.localPosition = new Vector3(0, 0, 0);
                unit[i].transform.Translate(new Vector3(0.1f, 0, 0));
            }
        }

        if (HP < 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            HP = 0;
            Destroy(gameObject);
            //StartCoroutine("RestartGame");
            GameManagerScript.doRestartGame = true;

        }
        healthBar.UpdateBar(HP, hpMax);
        energieBar.UpdateBar(energie, energieMax);
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

    public void OnTriggerEnter(Collider collision)
    {
        if(collision.tag=="WALL")
        {
            Debug.Log(collision.name);
            if (collision.name=="Front")
            {
                transform.position = new Vector3(transform.position.x, collision.transform.position.y - radius - 0.6f, transform.position.z);
            }
            if (collision.name == "Left")
            {
                transform.position = new Vector3(collision.transform.position.x - radius - 0.6f, transform.position.y, transform.position.z);
            }
        }
        if (collision.tag == "STONE")
        {
            HP -= 30;
            
            Instantiate(damageEff, transform.position, transform.rotation);
            Debug.Log("Player Hitted " + collision.name);
        }
    }
    //public void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.collider.name);
    //}
}
