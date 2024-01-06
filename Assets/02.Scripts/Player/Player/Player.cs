using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ���� ĳ���͸� ������ ����
    public GameObject playerGhost;

    // ���� ĳ���͸� ������ ����
    public GameObject playerShield;

    // ��� ĳ���͸� ������ ����
    public GameObject playerGoggles;

    // û�Һ� ĳ���͸� ������ ����
    public GameObject playerCleaner;

    // ��ũ��Ʈ ĳ��ó���� �� ����
    private PlayerGhostController playerGhostControllerScr;
    private PlayerShieldController playerShieldControllerScr;
    private PlayerGogglesController playerGogglesControllerScr;
    private PlayerCleanerController playerCleanerControllerScr;

    // �÷��̾� ���� ���¸� ǥ���� ����
    private bool isPossesing;
    // ���� �÷��̾� ĳ���� ���¸� ������ ����
    private PlayerType currentPlayer;

    // Start is called before the first frame update
    void Awake()
    {

        // ĳ�� ����
        CashScript();
        // ��ũ��Ʈ ����
        Init();


        isPossesing = false;
        // ���� Ÿ���� ���� Ÿ������ ����
        currentPlayer = PlayerType.PLAYERGHOST;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool IsPossesing
    {
        get
        {
            return isPossesing;
        }
        set
        {
            this.isPossesing = value;
        }
    }

    // ��ũ��Ʈ ĳ�� ó��
    public void CashScript()
    {
        playerGhostControllerScr = GetComponent <PlayerGhostController>();
        playerShieldControllerScr = GetComponent <PlayerShieldController>();
        playerGogglesControllerScr = GetComponent <PlayerGogglesController>();
        playerCleanerControllerScr = GetComponent <PlayerCleanerController>();
    }

    public void Init()
    {
        // ���� Ÿ�� ��ũ��Ʈ�� Ȱ��ȭ�ϰ� �������� ��Ȱ��ȭ.
        playerGhostControllerScr.enabled = true;
        playerShieldControllerScr.enabled = false;
        playerGogglesControllerScr.enabled = false;
        playerCleanerControllerScr.enabled = false;
    }
    // �÷��̾� ����
    public void ChangePlayer(PlayerType player) // ���ڷ� �ٲ� �÷��̾� Ÿ���� �޾ƿ´�.
    {
        // ���� �÷��̾� ĳ���� ������Ʈ ��Ȱ��ȭ
        inactiveCurrentGameObject(currentPlayer);
        switch (player)
        {
            // �ٲ� ĳ���Ͱ� ����ĳ������ ���
            case PlayerType.PLAYERGHOST:
                // ���� �÷��̾� Ÿ���� ���� ĳ���� Ÿ������ ���� �� ���� ���ӿ�����Ʈ�� ��ũ��Ʈ�� Ȱ��ȭ
                currentPlayer = PlayerType.PLAYERGHOST;
                playerGhost.SetActive(true);
                playerGhostControllerScr.enabled = true;
                playerGhostControllerScr.SetCashComponent();
                playerGhostControllerScr.ChangePlayerToGhost();
                break;
            // �ٲ� ĳ���Ͱ� ����ĳ������ ���
            case PlayerType.PLAYERSHIELD:
                // ���� �÷��̾� Ÿ���� ���� ĳ���� Ÿ������ ���� �� ���� ���ӿ�����Ʈ�� ��ũ��Ʈ�� Ȱ��ȭ
                currentPlayer = PlayerType.PLAYERSHIELD;
                playerShield.SetActive(true);
                playerShieldControllerScr.enabled = true;
                playerShieldControllerScr.SetCashComponent();
                break;            
            // �ٲ� ĳ���Ͱ� ��� ĳ������ ���
            case PlayerType.PLAYERGOGGLES:
                // ���� �÷��̾� Ÿ���� ��� ĳ���� Ÿ������ ���� �� ��� ���ӿ�����Ʈ�� ��ũ��Ʈ�� Ȱ��ȭ
                currentPlayer = PlayerType.PLAYERGOGGLES;
                playerGoggles.SetActive(true);
                playerGogglesControllerScr.enabled = true;
                playerGogglesControllerScr.SetCashComponent();
                break;            
            // �ٲ� ĳ���Ͱ� û�Һ� ĳ������ ���
            case PlayerType.PLAYERCLEANER:
                // ���� �÷��̾� Ÿ���� û�Һ� ĳ���� Ÿ������ ���� �� û�Һ� ���ӿ�����Ʈ�� ��ũ��Ʈ�� Ȱ��ȭ
                currentPlayer = PlayerType.PLAYERCLEANER;
                playerCleaner.SetActive(true);
                playerCleanerControllerScr.enabled = true;
                playerCleanerControllerScr.SetCashComponent();
                break;

        }
    }
    // ���� �÷��̾� ĳ���� ��Ȱ��ȭ �ϴ� �Լ�
    private void inactiveCurrentGameObject(PlayerType currPlayer) // ���ڷ� ���� �÷��̾� Ÿ���� �޾ƿ´�.
    {
        switch (currPlayer)
        {
            // ���� ĳ���Ͱ� ���� ĳ������ ���
            case PlayerType.PLAYERGHOST:
                // ���� ĳ���� ���� ������Ʈ�� ��ũ��Ʈ�� ��Ȱ��ȭ
                playerGhost.SetActive(false);
                playerGhostControllerScr.enabled = false;
                break;
            // ���� ĳ���Ͱ� ���� ĳ������ ���
            case PlayerType.PLAYERSHIELD:
                // ���� ĳ���� ���� ������Ʈ�� ��ũ��Ʈ�� ��Ȱ��ȭ
                playerShield.SetActive(false);
                playerShieldControllerScr.enabled = false;
                break;
            // ���� ĳ���Ͱ� ��� ĳ������ ���
            case PlayerType.PLAYERGOGGLES:
                // ��� ĳ���� ���� ������Ʈ�� ��ũ��Ʈ�� ��Ȱ��ȭ
                playerGoggles.SetActive(false);
                playerGogglesControllerScr.enabled = false;
                break;
            // ���� ĳ���Ͱ� û�Һ� ĳ������ ���
            case PlayerType.PLAYERCLEANER:
                // û�Һ� ĳ���� ���� ������Ʈ�� ��ũ��Ʈ�� ��Ȱ��ȭ
                playerCleaner.SetActive(false);
                playerCleanerControllerScr.enabled = false;
                break;

        }
    }

}
