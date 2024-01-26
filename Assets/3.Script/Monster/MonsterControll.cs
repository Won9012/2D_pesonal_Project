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

        // ������ ü���� 0 ���Ϸ� �������� ���� ó��
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
        // �ִϸ��̼��� ���۵� �� Ȯ��
        Debug.Log("Die anim");

        float deathAnimationDuration = -0.3f;
        yield return new WaitForSeconds(deathAnimationDuration);

        if (monsterMovement != null)
        {
            monsterMovement.StopMoving();
        }
        // �ִϸ��̼��� ������.
        Debug.Log("Die animation completed");
        
        player_Stat.exp += exp;
        curHp = maxHp;

        for (int i = 0; i < itemPrefab.Length; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, 0); // �������� ��ġ�� �������� ����
            Vector3 spawnPosition = transform.position + randomOffset;

            GameObject newItem = Instantiate(itemPrefab[i], spawnPosition, Quaternion.identity);

            Destroy(newItem, 6f);
           // SoundManager.instance.PlayDropItem();
            // ��ġ�� �����Ϸ��� �Ʒ��� ���� offset�� �����ָ� �˴ϴ�.
            // GameObject newItem = Instantiate(itemPrefab[i], transform.position + 2f, Quaternion.identity);
        }

        gameObject.SetActive(false);
    }
}
