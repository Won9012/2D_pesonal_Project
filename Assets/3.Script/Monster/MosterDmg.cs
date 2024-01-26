using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterDmg : MonoBehaviour
{
    private Player_Stat player;
    private MonsterControll redsnail;
    private Animator anim;

    private bool isDie = false;
    private void Start()
    {
        player = GetComponent<Player_Stat>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Redsnail"))
        {
            Debug.Log("���������� �°��ִ�?");
            MonsterControll redsnail = collision.gameObject.GetComponent<MonsterControll>();
            print(redsnail.att);
            Debug.Log(player.Curhp);

            if (redsnail != null)
            {
                // �÷��̾��� ü���� Redsnail ������ ���ݷ�(att)��ŭ ���ҽ�Ŵ
                player.Curhp -= redsnail.att;
                Debug.Log(player.Curhp);
            }

            if(player.Curhp <= 0)
            {
                player.Curhp = 0;
                isDie = true;
                anim.SetBool("isDie",isDie);
                Invoke("isDieFalse", 3f);
                anim.SetBool("isDie", isDie);
            }
        }
    }

    private void isDieFalse()
    {
        isDie = false;
    }

    private void Update()
    {
       // print(player.Curhp);
        if (redsnail != null)
        {
            print(redsnail.att);
        }
    }
}
