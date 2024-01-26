using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lv_varView : MonoBehaviour
{
    public Player_Stat player_Stat;
    public Slider expSlider;
    // Update is called once per frame
    void Update()
    {
        expSlider.value = player_Stat.exp / player_Stat.ExpMax;
    }
}
