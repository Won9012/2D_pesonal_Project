using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private float Change;
    private Rigidbody2D rb;
    private bool moveRight;
    private bool monMove = false;
    private SpriteRenderer sprite;
    private Animator anim;
    private float timeSinceLastDirectionChange = 0f;
    public StageData MonstageData;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        moveRight = Random.Range(0, 2) == 0; //0�϶� ������ 1�϶� ����
     //   ChangeMoveDirectionTime();
    }

    private void ChangeMoveDirectionTime()
    {
        Change = Random.Range(1,5);
    }


    private void Update()
    {
       // print(Change);
        // �ð� ����� ����Ͽ� ������ �ֱ������� ����
        timeSinceLastDirectionChange += Time.deltaTime;
        if (timeSinceLastDirectionChange >= Change)
        {
            ChangeDirection();
            ChangeMoveDirectionTime();
            timeSinceLastDirectionChange = 0.0f;
        }

        Vector3 MonsterPosition = transform.position;
        MonsterPosition.x = Mathf.Clamp(MonsterPosition.x, MonstageData.LimitMin.x, MonstageData.LimitMax.x);
        MonsterPosition.y = Mathf.Clamp(MonsterPosition.y, MonstageData.LimitMin.y, MonstageData.LimitMax.y);
        transform.position = MonsterPosition;
    }
    private void FixedUpdate()
    {
        float moveDirection = moveRight ? 1 : -1;
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if(rb.velocity.x > 0)
        {
            sprite.flipX = true;
        }
        else if(rb.velocity.x < 0)
        {
            sprite.flipX = false;
        }
        /*    if(rb.velocity.x != 0)
            {
                monMove = true;
                anim.SetBool("monMove", monMove);
            }*/


    }

    public void StopMoving()
    {
        // Rigidbody2D�� velocity�� 0���� �����Ͽ� �̵��� ����ϴ�.
        rb.velocity = Vector2.zero;
    }


    private IEnumerator ChangeDirection_co()
    {
        while (true)
        {
            yield return new WaitForSeconds(Change);
        }
    }

    public void ChangeDirection()
    {
        moveRight = !moveRight;
    }
}
