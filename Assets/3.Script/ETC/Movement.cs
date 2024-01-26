using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //캐릭터와 몬스터의 기본 움직임 관리 ##
    //여기서 캐릭터 애니메이션 및 몬스터 애니메이션도 관리합니다.
    //캐릭터는 화살표 입력값에 따라 움직임을 감지한다.
    //단, 로프 + 사다리 를 만날때에는 만나는녀석 태그의 이름에 따라 다른 모션을 취한다.
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Player_Stat player_Stat;

    public StageData stageData;

    //무적판정
    private bool isInvincible = false;
    private float invincibilityTime = 0f;

    private bool isWalk = false;
    private bool isGround = false;
    private bool isPlat = false;
    private bool isJump = false;
    private bool isProne = false;
    private bool isProneSta = false;
    private bool isLaddder = false;
    private bool isNotMoveLadder = false;
    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        player_Stat = GetComponent<Player_Stat>();
        //stageData = FindObjectOfType<StageData>();
        // stageData = GetComponent<StageData>();
    }
    private void Update()
    {
        PlayerMove();
        if(player_Stat.Curhp <= 0)
        {
            //hp가 0일때 뒤지고 , 죽고난다음에는 죽는 애니메이션 출력후 3초후에 부활시킨다.
            isDead = true;
            anim.SetBool("isDead", isDead);

            Invoke("ResurrectPlayer", 3f);

        }


    }

    private void ResurrectPlayer()
    {
        // 부활 시키고 필요한 초기화 작업을 수행
        isDead = false;
        anim.SetBool("isDead", isDead);
        player_Stat.Curhp = player_Stat.Maxhp;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    public void ApplyForce(Vector2 force)
    {
        // Rigidbody2D를 사용하여 힘을 적용합니다.
        rb.AddForce(new Vector2(force.x * 5f, force.y * 2f), ForceMode2D.Impulse);
    }

    public void SetInvincible(float duration)
    {
        isInvincible = true;
        invincibilityTime = duration;
    }

    private void PlayerMove()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");
        // 벨로시티 1로 고정해주는거
        Vector3 moveVelocity = new Vector3(horizontalInput, 0, 0).normalized * speed * Time.deltaTime;
        transform.position += moveVelocity;

        //캐릭의 현 위치
        Vector3 PlayerPosition = transform.position;
        //StageData에 설정한만큼 맵 이동제한(범위제한)
        PlayerPosition.x = Mathf.Clamp(PlayerPosition.x, stageData.LimitMin.x, stageData.LimitMax.x);
        PlayerPosition.y = Mathf.Clamp(PlayerPosition.y, stageData.LimitMin.y, stageData.LimitMax.y);
        transform.position = PlayerPosition;


        //캐릭 좌우 설정 초기에 flip 켜져있음. (오른쪽방향 바라보는중)
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = false;
            boxCollider.offset = new Vector2(-Mathf.Abs(boxCollider.offset.x), boxCollider.offset.y);
            transform.position = transform.position;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = true;
            boxCollider.offset = new Vector2(Mathf.Abs(boxCollider.offset.x), boxCollider.offset.y);
            transform.position = transform.position;
        }

        //walk 애니메이션 출력 조건
        if (Mathf.Abs(horizontalInput) > 0)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
        //walk 애니메이션 출력

        //jump 출력조건 => 바닥일때만 점프가 가능하게.

        //todo 점프사운드
        anim.SetBool("isWalk", isWalk);



        // ================================점프 구현 ========================= //
        // 일반 땅 ( 아래점프 불가능 한 영역) , 엎드려 있을때는 점프 불가능!
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            SoundManager.instance.PlayJumpSound();
            Debug.Log(isGround);
            // Debug.Log("점프??");
            if (!isProne)
            {
                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                isGround = false;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isPlat)
        {
            SoundManager.instance.PlayJumpSound();
            Debug.Log(isGround);
            Debug.Log(isPlat);
            if (!isProne)
            {

                rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                isPlat = false;
            }
            else if (isProne)
            {
                rb.AddForce(Vector2.up * -3.5f, ForceMode2D.Force);
                boxCollider.isTrigger = true;
                Invoke("ResetTrigger", 0.75f);
            }
        }

        //점프 모션 발동 조건
        if (rb.velocity.y > 0 || rb.velocity.y < 0)
        {
            isJump = true;
            anim.SetBool("isJump", isJump);
        }
        else
        {
            isJump = false;
            anim.SetBool("isJump", isJump);
        }
        //y축 (점프 제어 속도검사)
        //   Debug.Log("공중y축 속도체크: " + rb.velocity.y);

        //엎드리는 모션(하단공격)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Debug.Log("엎드려!");
            isProne = true;
            anim.SetBool("isProne", isProne);
        }
        else
        {
            isProne = false;
            anim.SetBool("isProne", isProne);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isProneSta = true;
            anim.SetBool("isProneSta", isProneSta);
            if (isProne)
            {
                SoundManager.instance.PlayAttSound();
            }

        }
        else
        {
            if (Input.GetKeyUp(KeyCode.C))
            {
                isProneSta = false;
                anim.SetBool("isProneSta", isProneSta);
            }

        }



        // 로프에 닿았을떄 첫번쨰 애니메이션 출력( 한프레임 고정)
        // 로프에서 키보드 입력이 들어왔을때 움직이는 모션 출력 
        // 움직이는 입력이 끝났을때 다시 한프레임으로 고정..
        // dksldhodksehlsmseo mm

        if (isNotMoveLadder)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                print("로프 되냐?");
                anim.SetBool("isNotLadder", isNotMoveLadder);
                rb.gravityScale = 0;
                rb.velocity = rb.velocity * 0;
                isLaddder = true;
            }
        }
        else
        {
            isNotMoveLadder = false;
            anim.SetBool("isNotLadder", isNotMoveLadder);
        }

        if (isLaddder)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("isLadder", isLaddder);
                transform.position += new Vector3(horizontalInput * 1f * Time.deltaTime, VerticalInput * 1f * Time.deltaTime, 0);
                if(transform.position.y > 3.2f)
                {
                    transform.position = new Vector3(transform.position.x, 3.4f, 0);
                    isLaddder = false;
                    anim.SetBool("isLadder", isLaddder);
                }

            }
            else
            {
                isLaddder = false;
                anim.SetBool("isLadder", isLaddder);
            }
        }






    }
    //땅일때만 점프 할 수 있도록 제한 (공중점프 X)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //땅이랑 닿았을때만 점프가능하게
        isJump = false;
      //  Debug.Log(isGround);
        if (collision.gameObject.CompareTag("Ground") && collision.contacts[0].normal == Vector2.up)
        {
            isGround = true;
            isJump = false;
        }
        //플렛폼일떄 아래로 점프하면 잠깐 트리거 켜서 아래점프 가능하게해주기
        else if(collision.gameObject.CompareTag("Plat") && collision.contacts[0].normal == Vector2.up)
        {
            isPlat = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = false;
                boxCollider.isTrigger = true;
                Invoke("ResetTrigger", 1f);
            }
        }
    }

    private void ResetTrigger()
    {
        boxCollider.isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isNotMoveLadder = true;
        }
      //  print("isNotLadder: " + isNotMoveLadder);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isLaddder = false;
        isNotMoveLadder = false;
        rb.gravityScale = 1f;
       // print("isNotLadder밖으로: " + isNotMoveLadder);
    }

    public IEnumerator InvincibilityFlash(float duration)
    {
        float flashInterval = 0.2f; // 깜박임 주기
        float timer = 0f;

        while (timer < duration)
        {
            // 깜박임 주기에 따라 SpriteRenderer를 껐다 켜줍니다.
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }

        // 무적 상태가 끝나면 SpriteRenderer를 다시 켜줍니다.
        spriteRenderer.enabled = true;

        // 무적 상태 해제
        isInvincible = false;
    }


}
