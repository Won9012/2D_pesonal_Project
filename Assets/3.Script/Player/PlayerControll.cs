using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public GameObject skillPrefab;
    public float skillRange = 7f;
    public int skillDamageMultiplier = 2; // 스킬 데미지의 배수

    private Player_Stat playerStat; // 플레이어의 스탯 정보
   // public DmgText dmgText;
    private void Start()
    {
        // 플레이어의 스탯 정보 스크립트를 가져옴
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
        // 가장 가까운 몬스터 찾기
        GameObject closestMonster = FindClosestMonster();
        if (closestMonster != null)
        {
            // 플레이어와 몬스터 사이의 거리 계산
            float distanceToMonster = Vector3.Distance(transform.position, closestMonster.transform.position);

            // 거리가 7f 이하인 경우에만 스킬 발사
            if (distanceToMonster <= skillRange)
            {
                // 스킬을 몬스터 위치에 생성
                Vector3 skillPosition = closestMonster.transform.position;
                GameObject skillEffect = Instantiate(skillPrefab, skillPosition, Quaternion.identity);
                Destroy(skillEffect, 0.5f);

                // 몬스터에게 피해 입히기
                MonsterControll monster = closestMonster.GetComponent<MonsterControll>();
                if (monster != null)
                {
                    // 플레이어의 공격력(att)의 2배로 데미지 설정
                    int skillDamage = playerStat.att * skillDamageMultiplier;
                    monster.TakeDamage(skillDamage);
                    SoundManager.instance.PlayTakeDamge();

                    //대미지 텍스트 표시
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