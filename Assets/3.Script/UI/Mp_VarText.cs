using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Mp_VarText : MonoBehaviour
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
            text.text = $"MP: {playerStat.Curmp}/{playerStat.mpMax}";
        }
        else
        {
            Debug.LogWarning("Player_Stat ��ũ��Ʈ�� �������� ���߽��ϴ�.");
        }
    }
}
