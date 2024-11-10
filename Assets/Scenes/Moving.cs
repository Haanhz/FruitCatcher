using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed = 0.5f;
    public bool loose = false;
    // Update is called once per frame
    void Update()
    {
        if (loose == false)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-speed, 0, 0);
            }

        }
    }
}
