using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    public InputField idInputField; // 아이디를 입력받을 InputField

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // 엔터 키가 눌렸을 때
            StartMainGame();
        }
    }

    public void StartMainGame()
    {
        string playerId = idInputField.text; // 아이디를 입력받음
        PlayerPrefs.SetString("PlayerID", playerId); // 아이디를 저장
        PlayerPrefs.Save(); // 변경사항 저장

        // MainGame 씬으로 전환
        SceneManager.LoadScene("MainGame");
    }
}