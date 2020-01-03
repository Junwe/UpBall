using System.Collections;
using System.Collections.Generic;
/// <summary>
/// enum을 이용해 문자열을 분배하는 클래스.
/// </summary>

public class StringFinder
{
    public List<string> ContatinsToGroundTypeInList(List<string> keys, GROUNDTYPE type)
    {
        List<string> tempKeys = new List<string>();

        foreach (string s in keys)
        {
            string test = System.Enum.GetName(typeof(GROUNDTYPE), type);
            if (s.IndexOf(System.Enum.GetName(typeof(GROUNDTYPE), type)) >= 0)
            {
                tempKeys.Add(s);
            }
        }

        return tempKeys;
    }

    public string ContatinsToGroundPosInList(List<string> keys, GROUNDPOS type)
    {
        string tempKey = string.Empty;

        foreach (string s in keys)
        {
            if (s.IndexOf(System.Enum.GetName(typeof(GROUNDPOS), type)) >= 0)
            {
                tempKey = s;
            }
        }

        return tempKey;
    }
}
