using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_VarText : MonoBehaviour
{
    public Player_Stat playerStat; // Player_Stat 스크립트 참조
    private Text text; // UI Text 참조

    private void Update()
    {
        text = GetComponent<Text>(); // UI Text 컴포넌트 가져오기
                                     //  print(playerStat.Curmp);
        if (playerStat != null)
        {

            // Player_Stat 스크립트에서 정보를 가져와서 UI Text에 출력
            text.text = $"HP: {playerStat.Curhp}/{playerStat.Maxhp}";
        }
        else
        {
            Debug.LogWarning("Player_Stat 스크립트를 참조하지 못했습니다.");
        }
    }
}
