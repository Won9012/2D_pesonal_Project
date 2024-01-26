using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringId : MonoBehaviour
{
    // Start is called before the first frame update
    private string playerID;

    private void Start()
    {
        // PlayerPrefs���� ���̵� �ҷ�����
        if (PlayerPrefs.HasKey("PlayerID"))
        {
            playerID = PlayerPrefs.GetString("PlayerID");
            Debug.Log("Player ID: " + playerID);
        }
        else
        {
            Debug.LogWarning("Player ID not found.");
        }
    }
}
