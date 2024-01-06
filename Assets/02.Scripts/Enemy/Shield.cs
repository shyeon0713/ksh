using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;  //�ൿ��ǥ�� ������ ���� ���� -1 , ������ 1
    public float distace;
    public GameObject Player;
    public GameObject Enemy;
    public bool Detection = false;
    public bool Collison = false;
    public float speed = 1.5f;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        nextMove = -1; // ���� �̵�
        GetComponent<SpriteRenderer>().flipX = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameObject.GetComponent<Enemy>().IsDied())
        {
         
            distace = Vector2.Distance(Enemy.transform.position, Player.transform.position); //Player�� Enemy�� �Ÿ� ��� 
            if (distace < 3.0f)
            {
                Detection = true;
                nextMove += 1 * (int)speed;
            }


           //rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

            //Platform Check
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            // DrawRay( ) : ������ �󿡼��� Ray�� �׷��ִ� �Լ�  (��ġ , ��� ���� ,���� �÷�(�ڵ忡�� �������))
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));  // (��ġ , ��� ���� ,�Ÿ�(���൵ �´�) , Layer����)
                                                                                                                //RayCastHit : Ray�� ���� ������Ʈ, GetMask : ���̾� �̸��� �ش��ϴ� �������� �����ϴ� ��
            if (rayHit.collider == null)
            { 
                nextMove = nextMove * -1;
                GetComponent<SpriteRenderer>().flipX = true;

            }
        }
    }
   


}
//�⺻ ������