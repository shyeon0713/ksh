using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController
{
    // �÷��̾� �̵��ӵ�
    public float maxSpeed;

    public GameObject hitEffect;
    // ������Ʈ�� ĳ�ø� ó���� ������
    // ��� �÷��̾� ĳ���Ͱ� ���������� ������ �ִ� �����̹Ƿ�, protected�� �̿��� ���.
    // �޸� �Ҹ� �ּ�ȭ �ϱ� ����.
    // ------------------------------------------
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;
    protected Animator anim;
    protected CapsuleCollider2D playerCollider;
    protected Transform tr;
    protected new AudioSource audio;

    // ------------------------------------------


    // ��ũ��Ʈ ĳ��ó��
    protected Player playerScr;
    protected Health healthScr;

    void Start()
    {
        // ���콺 Ŀ�� ������ �������� �ϱ�
        Cursor.lockState = CursorLockMode.Confined;
    }
    public virtual void Init()
    {
        maxSpeed = 14.0f;
        /*hitflash = ObjectPooler.Instance.GetEffectObject();*/
    }
    public virtual void SetCashComponent() {
        // �÷��̾� ������Ʈ ĳ�� ó��
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        playerCollider = GetComponentInChildren<CapsuleCollider2D>();
        tr = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();

    }
    public virtual void LoadResources() { }
    
    public virtual void SetScrCash()
    {
        //Script ĳ�� ó��
        playerScr = GetComponent<Player>();
        healthScr = GetComponent<Health>();
    }
    
    public virtual void Gravity()
    { }
    public virtual void Move() {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) // Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) // Left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);


        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }

        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.5)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }

    // ���콺 �Է� ��ǥ�κ��� �÷��̾� ������ ���� ���� ���ϱ�
    public virtual Vector2 GetPlayerToMouseUnitVector()
    {
        // �÷��̾��� ���� ��ǥ�� ��ũ�� ��ǥ�� ����
        Vector2 playerScreenPosition = transform.position;
        // ���콺 ��Ŭ������ ���콺 ��ũ�� ��ǥ
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ���콺 Ŭ�� ������ �÷��̾��� ��ũ�� ��ǥ�� ���� ����
        Vector2 playerToMouseVector = (mouseScreenPosition - playerScreenPosition).normalized;

        return playerToMouseVector;
    }
}
