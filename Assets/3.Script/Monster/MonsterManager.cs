using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterManager : MonoBehaviour
{
    public MonsterSpawn monsterSpawn;

    private void SpanSpore()
    {
        //�����ص� ���� ��������
        GameObject monster = monsterSpawn.GetRednail();
        if(monster != null )
        {
          //  monster.transform.position = position;
        }
    }
}
