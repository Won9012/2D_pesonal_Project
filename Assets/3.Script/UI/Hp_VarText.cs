using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_VarText : MonoBehaviour
{
    public Player_Stat playerStat; // Player_Stat ��ũ��Ʈ ����
    private Text text; // UI Text ����

    private void Update()
    {
        text = GetComponent<Text>(); // UI Text ������Ʈ ��������
                                     //  print(playerStat.Curmp);
        if (playerStat != null)
        {

            // Player_Stat ��ũ��Ʈ���� ������ �����ͼ� UI Text�� ���
            text.text = $"HP: {playerStat.Curhp}/{playerStat.Maxhp}";
        }
        else
        {
            Debug.LogWarning("Player_Stat ��ũ��Ʈ�� �������� ���߽��ϴ�.");
        }
    }
}
