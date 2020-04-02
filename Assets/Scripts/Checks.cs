using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Checks : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector3 startPosition;
    private Quaternion pStartPosition;
    public Button resButton;
    public Scene scene;

    public GameObject attempt;
    public GameObject leveltxt;
    public int attemptCounter;
    private Text textattempt;
    private Text levelTxt;

    private bool checkFall;

    int count;

    public Transform platformMain;
    public Transform[] platformMainChilds;

    private int sceneIndex;


    // Start is called before the first frame update
    void Start()
    {
        attempt = GameObject.FindGameObjectWithTag("attempt");
        leveltxt = GameObject.FindGameObjectWithTag("LevelText");

        if (attempt != null & leveltxt != null)
        {
            textattempt = attempt.GetComponent<Text>();
            levelTxt = leveltxt.GetComponent<Text>();
        }

        attemptCounter = 1;

        rb = this.GetComponent<Rigidbody2D>();

        startPosition = rb.transform.position;
        
        checkFall = false;


        platformMainChilds = platformMain.Cast<Transform>().ToArray();

        count = platformMain.childCount;

        pStartPosition = platformMain.transform.rotation;

        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Scene Count: " + SceneManager.sceneCountInBuildSettings);
    }

    void Update()
    {


        for (int i = 0; i < count; i++)
        {
            if (rb.position.y < (platformMainChilds[i].position.y - 2))
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                Restart();
            }
        }


        if ((Vector2.Distance(this.transform.position, startPosition) <= 1) & checkFall == true)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            //Debug.Log("Equal");


            for (int i = 0; i < count; i++)
            {
                platformMainChilds[i].rotation = pStartPosition;
            }



            checkFall = false;
            attemptCounter++;
        }


        textattempt.text = "Attempt: " + attemptCounter;

        levelTxt.text = "Level: " + (sceneIndex + 1).ToString();

        if (Input.GetKey(KeyCode.R))
        {
            Restart();
            attemptCounter = 0;
        }
    }

    void Restart()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        checkFall = true;
        rb.velocity = Vector2.zero;
        rb.MovePosition(startPosition);
    }

        

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "WinGround")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Debug.Log("WIN");
            Debug.Log("Scene Index " + sceneIndex);
            Restart();
            attemptCounter = 0;

            if (sceneIndex < SceneManager.sceneCountInBuildSettings-1)
            {
                sceneIndex++;
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                sceneIndex = 0;
                SceneManager.LoadScene(0);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

}
