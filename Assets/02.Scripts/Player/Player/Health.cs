using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] 
    private int HP;
    [SerializeField] 
    private int numOfHearts;

    // ���� ���¿� �����ߴ��� Ȯ���ϴ� bool ����
    private bool isInvincible;
    // �����ð�
    public float invincibleTime;
    // �����̴� �ð�
    public float blinkingTime;

    public Image[] hearts;
    public Sprite fulltHeart;
    public Sprite emptyHeart;

    private SpriteRenderer playerSpr;
    // Start is called before the first frame update
    void Start()
    {
        isInvincible = false;
        HP = 4;
        numOfHearts = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP > numOfHearts)
        {
            HP = numOfHearts;
        }
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < HP)
            {
                hearts[i].sprite = fulltHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public int getHP()
    {
        return HP;
    }
    public void Damaged(int damage)
    {
        if (!isInvincible)
        {
            HP -= damage;
            StartCoroutine(Invincible());
        }

    }
    public void Healed(int health)
    {
        HP += health;
    }

    public IEnumerator Invincible()
    {
        isInvincible = true;
        StartCoroutine(Blinking());
        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;
    }

    public IEnumerator Blinking()
    {
        playerSpr = GetComponentInChildren<SpriteRenderer>();
        while (isInvincible)
        {
            playerSpr.color = new Color32(255, 255, 255, 20);

            yield return new WaitForSeconds(blinkingTime);

            playerSpr.color = new Color32(255, 255, 255, 255);

            yield return new WaitForSeconds(blinkingTime);
        }
    }
}
