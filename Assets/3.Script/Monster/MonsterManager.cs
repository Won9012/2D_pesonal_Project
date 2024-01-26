using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterManager : MonoBehaviour
{
    public MonsterSpawn monsterSpawn;

    private void SpanSpore()
    {
        //생성해둔 몬스터 가져오기
        GameObject monster = monsterSpawn.GetRednail();
        if(monster != null )
        {
          //  monster.transform.position = position;
        }
    }
}
