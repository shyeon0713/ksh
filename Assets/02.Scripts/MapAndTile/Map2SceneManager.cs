using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class Map2SceneManager : MonoBehaviour
{

    private GameObject globalvolumeObj;
    public GameObject objectToDisable;
    void Update()
    {
        SceneState();
    }
    void SceneState()
     {
       
        string currentSceneName = SceneManager.GetActiveScene().name;
       
         if (globalvolumeObj! == null)
        {
             // ���� ���� "Map2"�̰� Player ������Ʈ�� �ִ� ��쿡�� �۾��� ����
             if (currentSceneName == "Map2")
             {
                objectToDisable.SetActive(true);
                //Debug.Log("���� �� �̸�: " + currentSceneName);
 
             }
             else
            {
              
                objectToDisable.SetActive(false);
                //Debug.Log("���� �� �̸�: " + currentSceneName);
              
            }
         }
     }
    
    

}
