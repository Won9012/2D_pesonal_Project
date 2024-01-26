using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject spore;  //������ ������
    public GameObject greennail; // �Ķ��޷���
    public GameObject rednail; // ����������

    private MonsterControll monster;
    public int poolSize = 10;
    private List<GameObject> sporePool = new List<GameObject>();
    private List<GameObject> greensnailPool = new List<GameObject>();
    private List<GameObject> redsnailPool = new List<GameObject>();

    public float respawnDelayMin = 3f;  // ������ �ּ� �ð�
    public float respawnDelayMax = 5f;  // �̽��� �ִ� �ð�

    //���Ͱ� ���� ������ ������ ����.
    public GameObject money;
    public GameObject potion;

    private void Start()
    {
        InitializePool(spore, sporePool);
        InitializePool(greennail, greensnailPool);
        InitializePool(rednail, redsnailPool);

        // ���� ���� ����
        StartCoroutine(SpawnMonsters());
    }

    private void InitializePool(GameObject prefab, List<GameObject> pool)
    {
        for(int i = 0; i < poolSize;  i++)
        {
            //���� ����
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

            // �������� ���� ���� ����
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
                // ���� ���� ��ġ ����
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
        //���Ͱ� �� Ȱ��ȭ �Ǿ������� ���� ����
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
        //���Ͱ� �� Ȱ��ȭ �Ǿ������� ���� ����
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
        //���Ͱ� �� Ȱ��ȭ �Ǿ������� ���� ����
        return null;
    }

    public void ReturnMonster(GameObject monster)
    {
        monster.SetActive(false);
    }
}
