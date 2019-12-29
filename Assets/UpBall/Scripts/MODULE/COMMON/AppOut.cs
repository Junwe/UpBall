using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppOut : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.Quit();
        }
    }
}
