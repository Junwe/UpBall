using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class localizationText : MonoBehaviour
{
    public string[] keys;
    public string[] values;
    public Sprite[] sprties;

    private Text _text;
    private Image _image;
    void Start()
    {
        _text = GetComponent<Text>();
        _image = GetComponent<Image>();

        if(_text != null)
        {
            _text.text = values[GetKey()];
        }
        else if(_image != null)
        {
            _image.sprite = sprties[GetKey()];
        }
    }

    private int GetKey()
    {
        int i = System.Array.IndexOf(keys, localizationData.Instance.Key);
        if( i == -1)
            return System.Array.IndexOf(keys, "English");
        
        return i;
    }

}
