using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject spore;  //스포아 프리팹
    public GameObject greennail; // 파랑달랭이
    public GameObject rednail; // 빨강달팽이

    private MonsterControll monster;
    public int poolSize = 10;
    private List<GameObject> sporePool = new List<GameObject>();
    private List<GameObject> greensnailPool = new List<GameObject>();
    private List<GameObject> redsnailPool = new List<GameObject>();

    public float respawnDelayMin = 3f;  // 리스폰 최소 시간
    public float respawnDelayMax = 5f;  // 이스폰 최대 시간

    //몬스터가 가진 아이템 프리팹 설정.
    public GameObject money;
    public GameObject potion;

    private void Start()
    {
        InitializePool(spore, sporePool);
        InitializePool(greennail, greensnailPool);
        InitializePool(rednail, redsnailPool);

        // 몬스터 스폰 시작
        StartCoroutine(SpawnMonsters());
    }

    private void InitializePool(GameObject prefab, List<GameObject> pool)
    {
        for(int i = 0; i < poolSize;  i++)
        {
            //몬스터 생성
            GameObject monster = Instantiate(prefab);
            monster.SetActive(false);
            pool.Add(monster);
        }
    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(respawnDelayMin, respawnDelayMax));

            GameObject monsterToSpawn = null;

            // 무작위로 몬스터 유형 선택
            int monsterType = Random.Range(0, 3);

            switch (monsterType)
            {
                case 0:
                    monsterToSpawn = GetSpore();
                    break;
                case 1:
                    monsterToSpawn = GetBluenail();
                    break;
                case 2:
                    monsterToSpawn = GetRednail();
                    break;
            }

            if (monsterToSpawn != null)
            {
                // 몬스터 스폰 위치 설정
                Vector3 spawnPosition = transform.position;
                monsterToSpawn.transform.position = spawnPosition;
                monsterToSpawn.SetActive(true);
            }
        }
    }

    public GameObject GetSpore()
    {
        foreach(GameObject monster in sporePool)
        {
            if (!monster.activeInHierarchy)
            {
                monster.SetActive(true);
                return monster;
            }
        }
        //몬스터가 다 활성화 되어있을때 루프 방지
        return null;
    }

    public GameObject GetBluenail()
    {
        foreach (GameObject monster in greensnailPool)
        {
            if (!monster.activeInHierarchy)
            {
                monster.SetActive(true);
                return monster;
            }
        }
        //몬스터가 다 활성화 되어있을때 루프 방지
        return null;
    }

    public GameObject GetRednail()
    {
        foreach (GameObject monster in redsnailPool)
        {
            if (!monster.activeInHierarchy)
            {
                monster.SetActive(true);
                return monster;
            }
        }
        //몬스터가 다 활성화 되어있을때 루프 방지
        return null;
    }

    public void ReturnMonster(GameObject monster)
    {
        monster.SetActive(false);
    }
}
