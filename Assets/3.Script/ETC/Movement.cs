using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //ĳ���Ϳ� ������ �⺻ ������ ���� ##
    //���⼭ ĳ���� �ִϸ��̼� �� ���� �ִϸ��̼ǵ� �����մϴ�.
    //ĳ���ʹ� ȭ��ǥ �Է°��� ���� �������� �����Ѵ�.
    //��, ���� + ��ٸ� �� ���������� �����³༮ �±��� �̸��� ���� �ٸ� ����� ���Ѵ�.
    [SerializeField] private float speed;
    [SerializeField] private float JumpForce;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Player_Stat player_Stat;

    public StageData stageData;

    //��������
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
            //hp�� 0�϶� ������ , �װ��������� �״� �ִϸ��̼� ����� 3���Ŀ� ��Ȱ��Ų��.
            isDead = true;
            anim.SetBool("isDead", isDead);

            Invoke("ResurrectPlayer", 3f);

        }


    }

    private void ResurrectPlayer()
    {
        // ��Ȱ ��Ű�� �ʿ��� �ʱ�ȭ �۾��� ����
        isDead = false;
        anim.SetBool("isDead", isDead);
        player_Stat.Curhp = player_Stat.Maxhp;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    public void ApplyForce(Vector2 force)
    {
        // Rigidbody2D�� ����Ͽ� ���� �����մϴ�.
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
        // ���ν�Ƽ 1�� �������ִ°�
        Vector3 moveVelocity = new Vector3(horizontalInput, 0, 0).normalized * speed * Time.deltaTime;
        transform.position += moveVelocity;

        //ĳ���� �� ��ġ
        Vector3 PlayerPosition = transform.position;
        //StageData�� �����Ѹ�ŭ �� �̵�����(��������)
        PlayerPosition.x = Mathf.Clamp(PlayerPosition.x, stageData.LimitMin.x, stageData.LimitMax.x);
        PlayerPosition.y = Mathf.Clamp(PlayerPosition.y, stageData.LimitMin.y, stageData.LimitMax.y);
        transform.position = PlayerPosition;


        //ĳ�� �¿� ���� �ʱ⿡ flip ��������. (�����ʹ��� �ٶ󺸴���)
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

        //walk �ִϸ��̼� ��� ����
        if (Mathf.Abs(horizontalInput) > 0)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
        //walk �ִϸ��̼� ���

        //jump ������� => �ٴ��϶��� ������ �����ϰ�.

        //todo ��������
        anim.SetBool("isWalk", isWalk);



        // ================================���� ���� ========================= //
        // �Ϲ� �� ( �Ʒ����� �Ұ��� �� ����) , ����� �������� ���� �Ұ���!
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            SoundManager.instance.PlayJumpSound();
            Debug.Log(isGround);
            // Debug.Log("����??");
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

        //���� ��� �ߵ� ����
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
        //y�� (���� ���� �ӵ��˻�)
        //   Debug.Log("����y�� �ӵ�üũ: " + rb.velocity.y);

        //���帮�� ���(�ϴܰ���)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Debug.Log("�����!");
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



        // ������ ������� ù���� �ִϸ��̼� ���( �������� ����)
        // �������� Ű���� �Է��� �������� �����̴� ��� ��� 
        // �����̴� �Է��� �������� �ٽ� ������������ ����..
        // dksldhodksehlsmseo mm

        if (isNotMoveLadder)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                print("���� �ǳ�?");
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
    //���϶��� ���� �� �� �ֵ��� ���� (�������� X)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //���̶� ��������� ���������ϰ�
        isJump = false;
      //  Debug.Log(isGround);
        if (collision.gameObject.CompareTag("Ground") && collision.contacts[0].normal == Vector2.up)
        {
            isGround = true;
            isJump = false;
        }
        //�÷����ϋ� �Ʒ��� �����ϸ� ��� Ʈ���� �Ѽ� �Ʒ����� �����ϰ����ֱ�
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
       // print("isNotLadder������: " + isNotMoveLadder);
    }

    public IEnumerator InvincibilityFlash(float duration)
    {
        float flashInterval = 0.2f; // ������ �ֱ�
        float timer = 0f;

        while (timer < duration)
        {
            // ������ �ֱ⿡ ���� SpriteRenderer�� ���� ���ݴϴ�.
            spriteRenderer.enabled = !spriteRenderer.enabled;

            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }

        // ���� ���°� ������ SpriteRenderer�� �ٽ� ���ݴϴ�.
        spriteRenderer.enabled = true;

        // ���� ���� ����
        isInvincible = false;
    }


}
