using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region Instance Properties
    // EffectManager�� �� �ϳ��� �����ؾ� �ϹǷ� static���� ����
    private static ObjectPooler instance;
    // instance property
    public static ObjectPooler Instance
    {
        get
        {
            // instance�� ����ִ��� �˻�
            if(instance == null)
            {
                // �� �ȿ� EffectManager ������Ʈ�� ������ �ִ� ������Ʈ�� �ִ��� �˻�.
                var obj = FindObjectOfType<ObjectPooler>();
                // �����Ѵٸ� �� ��ü�� instance�� ����
                if(obj != null)
                {
                    instance = obj;
                }
                // �������� �ʴ´ٸ� ���ο� ���ӿ�����Ʈ�� ������ �� ���ӿ�����Ʈ�� EffectManager�� ���� �� instance�� ����
                else
                {
                    var newObj = new GameObject().AddComponent<ObjectPooler>();
                    instance = newObj;
                }
            }
            // instance ��ȯ
            return instance;
        }
    }
    #endregion

    #region Effect ObjectPool
    // ����Ʈ ���ӿ�����Ʈ ����
    [SerializeField] private uint effectPoolSize;
    public uint EffectPoolSize => effectPoolSize;

    [SerializeField] public GameObject hitEffect;

    private Stack<GameObject> hitEffectPool;

    private void SetUpEffectPool()
    {
        if(hitEffect == null)
        {
            return;
        }
        hitEffectPool = new Stack<GameObject>();

        GameObject instance = null;

        for(int i = 0; i < effectPoolSize; i++)
        {
            instance = Instantiate(hitEffect);
            instance.SetActive(false);
            hitEffectPool.Push(instance);
        }
    }

    public GameObject GetEffectObject()
    {
        if(hitEffectPool == null)
        {
            return null;       
        }
        if(hitEffectPool.Count == 0)
        {
            GameObject newInstance = Instantiate(hitEffect);
            return newInstance;
        }

        GameObject nextInstance = hitEffectPool.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }
    #endregion
    // SingleTon Awake
    private void Awake()
    {
        // ���� EffectManager ������Ʈ�� ������ �ִ� ������Ʈ�� �� � �ִ��� Ž��
        var objs = FindObjectsOfType<ObjectPooler>();
        // ���� ������Ʈ�� ���� 1�� �ƴ϶��
        if(objs.Length != 1)
        {
            // �̹� instance�� EffectManager�� �Ҵ�� ���̱⿡ ��� ������ ��ü �ı�
            Destroy(gameObject);
            return;
        }
        // ���� �Ѿ�� �ı����� �ʵ��� DontDestroyOnLoad ����
        DontDestroyOnLoad(gameObject);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
