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
    private SpriteRenderer _sprite;
    void Start()
    {
        _text = GetComponent<Text>();
        _sprite = GetComponent<SpriteRenderer>();

        if(_text != null)
        {
            _text.text = values[GetKey()];
        }
        else if(_sprite != null)
        {
            _sprite.sprite = sprties[GetKey()];
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
