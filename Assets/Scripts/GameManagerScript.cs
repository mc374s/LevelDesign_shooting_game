using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public static Vector3 leftBottom = new Vector3(-2.3f, -2.45f, 0);
    public static Vector3 rightTop = new Vector3(2.3f, 0.18f, 0);

    public static bool doRestartGame;


    // Use this for initialization
    void Start () {
        //leftBottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 3));
        //rightTop = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 3));
        leftBottom = new Vector3(-2.3f, -2.45f, 0);
        rightTop = new Vector3(2.3f, 0.18f, 0);
        doRestartGame = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (doRestartGame)
        {
            StartCoroutine("ReloadGame");
        }
	}

    IEnumerator ReloadGame()
    {
        Debug.Log("Start Coroutine");
        yield return new WaitForSeconds(4.0f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("End Coroutine");
        yield return null;
    }
}
