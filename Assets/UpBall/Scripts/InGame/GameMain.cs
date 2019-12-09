using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoDestorySingleton<GameMain>
{
    public GameObject[] Players;
    public GameObject PlayerParent;
    private IPlayer _player;

    // Start is called before the first frame update
    void Awake()
    {
        CreatePlayer(PlayerPrefs.GetInt("PlayerSelect",0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePlayer(int num)
    {
        GameObject temp = Instantiate(Players[GetIndex((PLAYERTYPE)num)],PlayerParent.transform);

        _player = temp.GetComponent<IPlayer>();
    }

    private int GetIndex(PLAYERTYPE type)
    {
        return (int)type - ((int)type / 2);
    }
}
