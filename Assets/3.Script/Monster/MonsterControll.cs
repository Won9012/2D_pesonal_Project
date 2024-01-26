using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControll : MonoBehaviour
{
    private Animator animator;
    private MonsterMovement monsterMovement;
    private Player_Stat player_Stat;

    public int level;
    public float exp;
    public float att;
    public GameObject[] itemPrefab;
    public float moneyDrop;
    public float minMoneyDrop = 30f;
    public float maxMoneyDrop = 500f;
    public float curHp;
    public float maxHp;

    private bool isDead = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        monsterMovement = GetComponent<MonsterMovement>();
        player_Stat = FindObjectOfType<Player_Stat>();
        moneyDrop = Random.Range(minMoneyDrop, maxMoneyDrop);
    }


    public void TakeDamage(float damage)
    {
        curHp -= damage;
        animator.SetTrigger("Hit");

        // 몬스터의 체력이 0 이하로 떨어지면 죽음 처리
        if (curHp <= 0)
        {
            StartCoroutine(DieCoroutine());
        }

    }

    private IEnumerator DieCoroutine()
    {
        isDead = true;
        animator.SetBool("isDead", isDead);
        SoundManager.instance.PlaySnailDie();
        // 애니메이션이 시작될 때 확인
        Debug.Log("Die anim");

        float deathAnimationDuration = -0.3f;
        yield return new WaitForSeconds(deathAnimationDuration);

        if (monsterMovement != null)
        {
            monsterMovement.StopMoving();
        }
        // 애니메이션이 끝날떄.
        Debug.Log("Die animation completed");
        
        player_Stat.exp += exp;
        curHp = maxHp;

        for (int i = 0; i < itemPrefab.Length; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, 0); // 아이템의 위치를 무작위로 설정
            Vector3 spawnPosition = transform.position + randomOffset;

            GameObject newItem = Instantiate(itemPrefab[i], spawnPosition, Quaternion.identity);

            Destroy(newItem, 6f);
           // SoundManager.instance.PlayDropItem();
            // 위치를 조절하려면 아래와 같이 offset을 더해주면 됩니다.
            // GameObject newItem = Instantiate(itemPrefab[i], transform.position + 2f, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }
}
