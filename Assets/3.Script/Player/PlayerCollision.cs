using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Movement playerMovement; // �÷��̾� ������ ��ũ��Ʈ ����
    private void Start()
    {
        playerMovement = GetComponent<Movement>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // print("�浹�ϳ�? 11");
        if (collision.CompareTag("Redsnail"))
        {
            //print("�浹�ϳ�? 22");
            // �÷��̾ ���Ϳ� �浹�� ���
            Vector2 collisionDirection = (transform.position - collision.transform.position).normalized;
            

            // �浹 �������� �ణ �о�� (���ϴ� ���� ������ �����ϼ���)
            //float pushForce = 5f;
            
            playerMovement.ApplyForce(collisionDirection);

            // �÷��̾� ���� ���� ���� (���� ���� �ð� ����)
            float invincibilityDuration = 1f;
            playerMovement.SetInvincible(invincibilityDuration);
            StartCoroutine(playerMovement.InvincibilityFlash(invincibilityDuration));
        }
    }




}
