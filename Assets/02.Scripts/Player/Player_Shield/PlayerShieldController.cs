using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShieldController : PlayerController
{
    // Field
    #region PlayerShield Private Properties
    private float parryingDuration;
    private bool isParrying;
    public bool IsParrying { get; set; }
    private bool isDefending;
    private bool defended;
    // ���� �ݶ��̴� ���� ������
    private Vector2 shieldPosition;
    // �и� ����
    private AudioClip swingSfx;

    #endregion
    #region PlayerShiled Public Properties
    public GameObject shield;
    #endregion
    #region PlayerShiled GetterAndSetter
    public void setDefended(bool defended)
    {
        this.defended = defended;
    }

    #endregion
    // Method
    #region PlayerShiled StartAndUpdate
    // Start is called before the first frame update
    void Start()
    {
        SetScrCash();
        SetCashComponent();
        LoadResources();
        Init();
    }

    private void OnEnable()
    {
        SetScrCash();
        SetCashComponent();
        Init();
    }
    void Update()
    {
        HandleMouseInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move By Key Control(Move Speed)
        Move();

        //Landing Platform
        Gravity();
    }
    #endregion
    #region PlayerSheild Basic Settings
    //�⺻ ����
    public override void Init()
    {
        //Move Variable
        maxSpeed = 7.5f;

        isParrying = false;
        parryingDuration = 0.25f;

        isDefending = false;
        shield.SetActive(false);

        defended = false;

        shieldPosition = new Vector3(0.8263f, 0, 0);
}

    // ���ҽ� �ε�
    public override void LoadResources()
    {
        swingSfx = Resources.Load<AudioClip>("PlayerAudios/swing");
    }
    #endregion
    #region PlayerShield Behavior
    //�߷�
    public override void Gravity()
    {
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.0f)
                {
                    anim.SetBool("isJumping", false);
                }

            }
        }
    }
    //�̵�
    public override void Move()
    {
        if (!isParrying && !isDefending)
        {
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxSpeed) // Right Max Speed
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxSpeed * (-1)) // Left Max Speed
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
            if(Input.GetAxisRaw("Horizontal") == 1)
            {
                spriteRenderer.flipX = true;
                shield.transform.localPosition = shieldPosition;
            }
            else
            {
                spriteRenderer.flipX = false;
                shield.transform.localPosition = shieldPosition * -1;
            }
        }

        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.5)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // ��ũ��Ʈ�� ��Ȱ��ȭ �Ǿ� ������ return
        if (!enabled) return;

        // �浹 ��ü�� Enemy
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("���� ĳ���� ü�� �ޱ�");
            healthScr.Damaged(1);
        }
        if (collider.gameObject.tag == "Bullet")
        {
            if (!defended)
            {
                healthScr.Damaged(1);
                Destroy(collider.gameObject);
            }
        }

    }
    private void HandleMouseInput()
    {
        // �Ͻ����� �޴� Ŭ���� �ÿ� �Ǵ°� ����
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                // �÷��̾� �ٲٸ鼭 �뽬
                ChangePlayer();
            }
            // ���콺 ��Ŭ�� �̺�Ʈ
            if (Input.GetMouseButtonDown(1))
            {
                if (isDefending)
                {
                    return;
                }
                if (!isParrying)
                {
                    StartCoroutine(Parrying());
                }

            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine(Defending());
            }
            if (isDefending)
            {
                if (Input.GetKeyDown(KeyCode.W))
                    isDefending = false;
            }
        }
    }
    #region PlayerShield Coroutines
    // �и�
    public IEnumerator Parrying()
    {
        anim.SetBool("isParrying", true);
        isParrying = true;
        audio.clip = swingSfx;
        audio.Play();
        shield.SetActive(true);

        yield return new WaitForSeconds(parryingDuration);

        isParrying = false;
        anim.SetBool("isParrying", false);
        shield.SetActive(false);
        if (defended)
            defended = false;
    }
    // ��� ���
    public IEnumerator Defending()
    {
        // ��� �ִϸ��̼� ���� �� ���� Ȱ��ȭ
        anim.SetBool("isDefending", true);
        isDefending = true;
        shield.SetActive(true);

        yield return new WaitUntil(() => isDefending == false);

        // ��� �ִϸ��̼� ���� �� ���� ��Ȱ��ȭ
        anim.SetBool("isDefending", false);
        isDefending = false;
        shield.SetActive(false);

    }
    #endregion
    //���� ĳ���ͷ� ����
    public void ChangePlayer()
    {
        Init();
        rigid.gravityScale = 8.0f;
        playerScr.ChangePlayer(PlayerType.PLAYERGHOST);
    }
    #endregion

}
