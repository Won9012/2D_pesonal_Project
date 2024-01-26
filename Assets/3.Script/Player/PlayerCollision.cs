using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Movement playerMovement; // 플레이어 움직임 스크립트 참조
    private void Start()
    {
        playerMovement = GetComponent<Movement>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // print("충돌하나? 11");
        if (collision.CompareTag("Redsnail"))
        {
            //print("충돌하나? 22");
            // 플레이어가 몬스터와 충돌한 경우
            Vector2 collisionDirection = (transform.position - collision.transform.position).normalized;
            

            // 충돌 방향으로 약간 밀어내기 (원하는 힘과 방향을 조절하세요)
            //float pushForce = 5f;
            
            playerMovement.ApplyForce(collisionDirection);

            // 플레이어 무적 상태 설정 (무적 지속 시간 조절)
            float invincibilityDuration = 1f;
            playerMovement.SetInvincible(invincibilityDuration);
            StartCoroutine(playerMovement.InvincibilityFlash(invincibilityDuration));
        }
    }




}
