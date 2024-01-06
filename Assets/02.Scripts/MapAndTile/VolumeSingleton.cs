using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VolumeSingleton : MonoBehaviour
{
    
    private GameObject globalVolumeObj;

    private static VolumeSingleton instance=null;
    
    public static VolumeSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<VolumeSingleton>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(VolumeSingleton).Name);
                    instance = singletonObject.AddComponent<VolumeSingleton>();
                }
            }
            return instance;
        }
    }
    
    void Awake()
     {

        if (instance == null )
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // ���� ���� ������Ʈ�� �ν��Ͻ��� �����ϰ� �ı����� �ʵ��� ����
        else
        {
            Destroy(this.gameObject);
        }
    }
  

}
