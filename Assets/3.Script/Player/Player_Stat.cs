using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stat : MonoBehaviour
{
    public int Level;
    public int dex;
    public int att;  
    public float Curhp;
    public float Maxhp;
    public float Curmp;
    public float mpMax;    
    public float exp;
    public float ExpMax;
    public int Points = 0; // 사용 가능한  포인트
    public float money;

    Movement movement;

    public GameObject LvupPrefab;

   

    private void Start()
    {
        movement = FindObjectOfType<Movement>();
        Invoke("reMp", 5.0f);
        Invoke("reHp", 5.0f);
    }

   
    private void Update()
    {
        Vector3 lvPosiotion = new Vector3(movement.transform.position.x, movement.transform.position.y + 3.8f, 0);
        if (exp >= ExpMax)
        {

            Level++;
            Points++;
            ExpMax = ExpMax * 1.5f;
            exp = exp - ExpMax;
            Maxhp = Maxhp + 50f;
            Curhp = Maxhp;
            mpMax = mpMax + 20f;
            Curmp = mpMax;
            att = att + 2;
            GameObject Prefab = Instantiate(LvupPrefab, lvPosiotion, Quaternion.identity);
            Destroy(Prefab, 1.4f);
        }
    }

    private void reMp()
    {
        if(Curmp < mpMax)
        {
            Curmp += 3f;
            if(Curmp > mpMax)
            {
                Curmp = mpMax;
            }
        }

        Invoke("reMp", 5.0f);
    }

    private void reHp()
    {
        if(Curhp < Maxhp)
        {
            Curhp += 5f;
            if(Curhp > Maxhp)
            {
                Curhp = Maxhp;
            }
        }
        Invoke("reHp", 5.0f);
    }
}
