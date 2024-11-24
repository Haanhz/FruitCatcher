using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving2 : MonoBehaviour
{
    public float speed = 0.5f;
    public bool loose = false;
    // Update is called once per frame
    void Update()
    {
        if (loose == false)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, speed* Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow) )
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }

        }
    }
}

