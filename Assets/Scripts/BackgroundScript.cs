using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public GameObject background1;
    public GameObject starBig1;
    public GameObject starSmall1;
    public GameObject background2;
    public GameObject starBig2;
    public GameObject starSmall2;
    public float backgroundSpeed;
    public float starBigSpeed;
    public float starSmallSpeed;
    private Vector3 initPosition;
    private Vector3 resetPosition;
    // Use this for initialization
    void Start () {
        initPosition = background2.transform.position;
        resetPosition = initPosition;

    }
	
	// Update is called once per frame
	void Update () {
        background1.transform.Translate(new Vector3(0, backgroundSpeed, 0));
        if (background1.transform.position.y < -resetPosition.y)
        {
            background1.transform.position = initPosition;
        }
        background2.transform.Translate(new Vector3(0, backgroundSpeed, 0));
        if (background2.transform.position.y < -resetPosition.y)
        {
            background2.transform.position = initPosition;
        }

        starBig1.transform.Translate(new Vector3(0, starBigSpeed, 0));
        if (starBig1.transform.position.y < -resetPosition.y)
        {
            starBig1.transform.position = initPosition;
        }
        starBig2.transform.Translate(new Vector3(0, starBigSpeed, 0));
        if (starBig2.transform.position.y < -resetPosition.y)
        {
            starBig2.transform.position = initPosition;
        }

        starSmall1.transform.Translate(new Vector3(0, starSmallSpeed, 0));
        if (starSmall1.transform.position.y < -resetPosition.y)
        {
            //starSmall1.transform.position = new Vector3(initPosition.x, -initPosition.y, initPosition.z);
            starSmall1.transform.position = initPosition;
        }
        starSmall2.transform.Translate(new Vector3(0, starSmallSpeed, 0));
        if (starSmall2.transform.position.y < -resetPosition.y)
        {
            //starSmall2.transform.position = new Vector3(initPosition.x, -initPosition.y, initPosition.z);
            starSmall2.transform.position = initPosition;
        }
    }
}
