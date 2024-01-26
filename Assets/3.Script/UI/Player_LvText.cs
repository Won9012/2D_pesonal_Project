using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_LvText : MonoBehaviour
{
    Player_Stat player_Stat;
    private Text text;
    // 인트로에서 저장한 플레이어 아이디 가져오기;

    private void Start()
    {
        text = GetComponent<Text>();
        player_Stat = FindObjectOfType<Player_Stat>();
        string playerId = PlayerPrefs.GetString("PlayerID", "DefaultID");
    }
    void Update()
    {
        text.text = $"Lv : {player_Stat.Level}    ID : {PlayerPrefs.GetString("PlayerID", "DefaultID")}";       
    }
}


