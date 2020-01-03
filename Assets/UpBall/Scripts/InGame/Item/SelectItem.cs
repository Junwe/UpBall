using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectItem : MonoBehaviour
{
    public Image Image_Item;
    public Image Image_Select;

    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().enabled = isActive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelect(bool bSelect)
    {
        Image_Select.enabled = bSelect;
    }

}
