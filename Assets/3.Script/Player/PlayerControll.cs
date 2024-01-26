using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public GameObject skillPrefab;
    public float skillRange = 7f;
    public int skillDamageMultiplier = 2; // ��ų �������� ���

    private Player_Stat playerStat; // �÷��̾��� ���� ����
   // public DmgText dmgText;
    private void Start()
    {
        // �÷��̾��� ���� ���� ��ũ��Ʈ�� ������
        playerStat = GetComponent<Player_Stat>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(playerStat.Curmp >= 5)
            {
                UseSkill();
                playerStat.Curmp -= 5f;
            }

        }
    }

    private void UseSkill()
    {
        // ���� ����� ���� ã��
        GameObject closestMonster = FindClosestMonster();
        if (closestMonster != null)
        {
            // �÷��̾�� ���� ������ �Ÿ� ���
            float distanceToMonster = Vector3.Distance(transform.position, closestMonster.transform.position);

            // �Ÿ��� 7f ������ ��쿡�� ��ų �߻�
            if (distanceToMonster <= skillRange)
            {
                // ��ų�� ���� ��ġ�� ����
                Vector3 skillPosition = closestMonster.transform.position;
                GameObject skillEffect = Instantiate(skillPrefab, skillPosition, Quaternion.identity);
                Destroy(skillEffect, 0.5f);

                // ���Ϳ��� ���� ������
                MonsterControll monster = closestMonster.GetComponent<MonsterControll>();
                if (monster != null)
                {
                    // �÷��̾��� ���ݷ�(att)�� 2��� ������ ����
                    int skillDamage = playerStat.att * skillDamageMultiplier;
                    monster.TakeDamage(skillDamage);
                    SoundManager.instance.PlayTakeDamge();

                    //����� �ؽ�Ʈ ǥ��
                    DmgText dmgText = GetComponent<DmgText>();
                    if(dmgText != null)
                    {
                        dmgText.ShowDamageText(skillDamage, closestMonster.transform.position);
                    }
                    
                }
            }
        }
    }

    private GameObject FindClosestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject closestMonster = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject monster in monsters)
        {
            float distanceToMonster = Vector3.Distance(transform.position, monster.transform.position);
            if (distanceToMonster < closestDistance)
            {
                closestMonster = monster;
                closestDistance = distanceToMonster;
            }
        }

        return closestMonster;
    }
}