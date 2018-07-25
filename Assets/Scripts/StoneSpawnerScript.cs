using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawnerScript : MonoBehaviour {

    public GameObject[] stones;

    public Transform left;
    public Transform right;
    private GameObject stone;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            stone = Instantiate(stones[Random.Range(0, stones.Length)]);
            stone.transform.position = new Vector3(
                Random.Range(left.transform.position.x, right.transform.position.x),
                left.transform.position.y,
                left.transform.position.z
            );
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
