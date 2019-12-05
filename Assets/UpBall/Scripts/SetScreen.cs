using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreen : MonoBehaviour
{
    [SerializeField]
    private int _setWidth;
    [SerializeField]
    private int _setHeight;
    void Awake()
    {
        Screen.SetResolution(Screen.width, Screen.width * _setWidth/ _setHeight, true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
