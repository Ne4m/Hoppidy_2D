using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Movement_Reverse : MonoBehaviour
{
    public float rotateSpeed;
    //Transform tr;

    public Vector3 rot;
    public int count;

    Transform[] childs;

    // Start is called before the first frame update
    void Start()
    {
        //tr = this.GetComponent<Transform>();

        count = transform.childCount;
        Debug.Log(count);

        childs = transform.Cast<Transform>().ToArray();


    }

    // Update is called once per frame
    void Update()
    {



        //Debug.Log("Editor Z: " + rot.z + "Normal Z: " + transform.rotation.z);
        getInput();
    }

    void getInput()
    {

        for (int i = 0; i < count; i++)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (transform.rotation.z <= 0.2588191)
                {
                    childs[i].Rotate(0, 0, rotateSpeed * Time.deltaTime);
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (transform.rotation.z >= -0.2588191)
                {
                    childs[i].Rotate(0, 0, rotateSpeed * Time.deltaTime * -1);
                }
            }
        }

    }
}
