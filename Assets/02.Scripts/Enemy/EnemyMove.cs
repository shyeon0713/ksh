using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;  //�ൿ��ǥ�� ������ ���� ���� -1 , ������ 1
    public float distace;
    public GameObject Player;
    public GameObject Enemy;
    public bool Player_detection = false; 

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        nextMove = 1; // ������ �̵�
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameObject.GetComponent<Enemy>().IsDied())
        {
            //Move
            distace = Vector2.Distance(Enemy.transform.position, Player.transform.position); //Player�� Enemy�� �Ÿ� ��� 
            if (distace < 3.0f)
            {
                Player_detection = true;
                nextMove += 1;
            }
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

            //Platform Check
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
            Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
            // DrawRay( ) : ������ �󿡼��� Ray�� �׷��ִ� �Լ�  (��ġ , ��� ���� ,���� �÷�(�ڵ忡�� �������))
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));  // (��ġ , ��� ���� ,�Ÿ�(���൵ �´�) , Layer����)
                                                                                                                //RayCastHit : Ray�� ���� ������Ʈ, GetMask : ���̾� �̸��� �ش��ϴ� �������� �����ϴ� ��
            if (rayHit.collider == null)
            {
                Invoke("Wait", 2);
                nextMove = nextMove * -1;

            }
        } 
    }

    private void LateUpdate()
    {
        rigid.velocity = new Vector2(0, 0);
    }
    private List<int> exclusionList = new List<int>() {0}; //������ ��
    void Wait()
    {
        nextMove = Random.Range(-1, 1);  //�ִ밪���� ����������

        while (exclusionList.Contains(nextMove))
        {
            nextMove = Random.Range(-1, 1);
        }

    }


}
//�⺻ ������