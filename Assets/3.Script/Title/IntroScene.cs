using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    public InputField idInputField; // ���̵� �Է¹��� InputField

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // ���� Ű�� ������ ��
            StartMainGame();
        }
    }

    public void StartMainGame()
    {
        string playerId = idInputField.text; // ���̵� �Է¹���
        PlayerPrefs.SetString("PlayerID", playerId); // ���̵� ����
        PlayerPrefs.Save(); // ������� ����

        // MainGame ������ ��ȯ
        SceneManager.LoadScene("MainGame");
    }
}