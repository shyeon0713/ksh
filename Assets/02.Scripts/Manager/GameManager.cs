using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * �̱����� Ȱ���� MonoBehaviour�� ��ӹ޴� ���ӸŴ��� ����
 * ����Ƽ �̱��� ���� ��Ʃ�� ��ũ: https://www.youtube.com/watch?v=-wzULWMvFu0
 * */
public class GameManager : MonoBehaviour
{
    // instance ������ �� �ϳ��� �����ؾ� �ϱ� ������ static���� ����
    private static GameManager instance;
    // instance property
    public static GameManager Instance
    {
        get
        {
            // instance�� ����ִ��� �˻�
            if(instance == null)
            {
                // �� �ȿ� GameManager ������Ʈ�� ������ �ִ� ������Ʈ�� �ִ��� �˻�.
                var obj = FindObjectOfType<GameManager>();
                // �����Ѵٸ� �� ��ü�� instance�� ����
                if(obj != null)
                {
                    instance = obj;
                }
                    // �������� �ʴ´ٸ� ���ο� ���ӿ�����Ʈ�� ������ �� ���ӿ�����Ʈ�� GameManger�� ���� �� instance�� ����
                else
                {
                    var newObj = new GameObject().AddComponent<GameManager>();
                    instance = newObj;
                }
            }
            // instance ��ȯ
            return instance;
        }
    }

    private void Awake()
    {
        // ���� ���� �Ŵ��� ������Ʈ�� ������ �ִ� ������Ʈ�� �� � �ִ��� �˻�
        var objs = FindObjectsOfType<GameManager>();
        // ���ӸŴ��� ������Ʈ�� ������ 1���� �ƴ϶��
        if(objs.Length != 1)
        {
            // �̹� instance�� ���ӸŴ����� �Ҵ�� ���̱⿡ ��� ������ ��ü �ı�
            Destroy(gameObject);
            return;
        }
        // ���� �Ѿ�� �ı����� �ʵ��� DontDestroyOnLoad ����
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {

    }

    public void EndGame()
    {

    }
}
