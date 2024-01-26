using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMainGame : MonoBehaviour
{
    public void Login_Btn()
    {
        SceneManager.LoadScene("MainGame");
    }
}
