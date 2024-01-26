using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_VarView : MonoBehaviour
{
    public Player_Stat player_Stat;
    public Slider mpSlider;
    // Update is called once per frame
    void Update()
    {
        mpSlider.value = player_Stat.Curhp / player_Stat.Maxhp;
    }
}
