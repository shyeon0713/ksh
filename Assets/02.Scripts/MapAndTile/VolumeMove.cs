using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class VolumeMove : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public GameObject targetObject;
    public GameObject targetObject1;//Ȱ��ȭ ���� �˻�
    public GameObject targetObject2;
    public GameObject volumeObj;
    
    Volume volume;
    Vignette vignette;

  
    private void Start()
    {
       
        volume = GetComponent<Volume>();
        if (volume.profile.TryGet(out vignette))
        {
            // ������ �� �ʱ� Center ���� ����
            vignette.center.value = new Vector2(0.5f, 0.5f);
        }

        
    }

    private void Update()
    {
      //  string currentSceneName = SceneManager.GetActiveScene().name;
        VolumeTarget();
        /* if (currentSceneName == "Map2")
         {
             GameObject playerObject = GameObject.FindWithTag("Player");
             if (playerObject != null)
             {
                // Debug.Log(SceneManager.GetActiveScene().name);
                 volumeObj.SetActive(true);
                 VolumeTarget();
             }


         }
         else
         {

             Debug.Log(SceneManager.GetActiveScene().name);
             volumeObj.SetActive(false);

         }*/
    }

    public void VolumeTarget()
    {
        if (targetObject.activeSelf)
        {
            GameObject playerObject = GameObject.Find("Player");

            if (playerObject != null)
            {
                //�ڽ� ������Ʈ�� ã��
                Transform playerGhost = playerObject.transform.Find("PlayerGhost");

                if (playerGhost != null)
                {
                    // ã�� "PlayerGhost"�� ���ο� player���� ����
                    player = playerGhost;
                }
            }
        }
        if (targetObject1.activeSelf)
        {
            GameObject playerObject = GameObject.Find("Player");

            if (playerObject != null)
            {
                //�ڽ� ������Ʈ�� ã��
                Transform playerShield = playerObject.transform.Find("PlayerShield");

                if (playerShield != null)
                {
                    // ã�� "PlayerShield"�� ���ο� player���� ����
                    player = playerShield;
                }
            }
        }
        if (targetObject2.activeSelf)
        {
            GameObject playerObject = GameObject.Find("Player");

            if (playerObject != null)
            {
                //�ڽ� ������Ʈ�� ã��
                Transform playerGoggles = playerObject.transform.Find("PlayerGoggles");

                if (playerGoggles != null)
                {
                    // ã�� "PlayerGoggles"�� ���ο� player���� ����
                    player = playerGoggles;
                }
            }
        }


        if (player != null && vignette != null)
        {
            // �÷��̾� ��ġ�� ���� Vignette�� Center ���� ������Ʈ
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(player.position);
            vignette.center.value = new Vector2(viewportPos.x, viewportPos.y);
        }
    }
}
