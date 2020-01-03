using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleQuit : MonoBehaviour
{

    uint exitCountValue = 0;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            exitCountValue++;

            if (!IsInvoking("disable_DoubleClick"))
                Invoke("disable_DoubleClick", 0.3f);
        }

        if (exitCountValue == 2)
        {
            CancelInvoke("disable_DoubleClick");

            Application.Quit();
            Debug.Log("a");
        }

    }
    void disable_DoubleClick()
    {

        exitCountValue = 0;

    }

}
