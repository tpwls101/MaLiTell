using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "v1.0";
    private string userId = "";

    // ����Ƽ ������ �ν����Ϳ��� �Ҵ�Ǵ� ������
    public TMP_InputField userIdText;  // �г��� �Է� �ʵ�
    public TMP_InputField roomNameText;  // �� �̸� �Է� �ʵ�
    public TMP_InputField pwdNameText; // ��й�ȣ �̸� �Է� �ʵ�
    public TMP_Text ConnectionStatus;  // ���� ���� ǥ�� �ؽ�Ʈ

    // �� ����� ������ ��ųʸ�
    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();
    // ���� ǥ���� ������
    public GameObject roomPrefab;
    // �� �������� ǥ���� �θ� ������Ʈ
    public Transform scrollContent;
    // �÷��̾� ������
    public GameObject playerPrefab;

    public GameObject loginScene;
    public GameObject mainScene;

    private void Awake()
    {
        // ������ ȥ�� ���� �ε��ϸ�, ������ ������� �ڵ����� ��ũ��
        PhotonNetwork.AutomaticallySyncScene = true;

        // ���� ���� ����
        PhotonNetwork.GameVersion = gameVersion;

        // ���� ������ ����
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Start()
    {
        // �ʱ�ȭ �޽��� ǥ��
        ConnectionStatus.text = "���� �Ŵ��� ����";

        // PlayerPrefs�� ����Ͽ� ����� ���� ���̵� �ҷ�����, ������ ���� ���̵� ����
        userId = PlayerPrefs.GetString("User_ID", $"USER_{Random.Range(0, 100):00}");

        // UI�� �г��� ǥ��
        userIdText.text = userId;

        // ���� ��Ʈ��ũ �г��� ����
        PhotonNetwork.NickName = userId;
        // �ʱ� ���¿��� �α��� ���� Ȱ��ȭ�ǰ� ���� ���� ��Ȱ��ȭ ���·� ����
        loginScene.SetActive(true);
        mainScene.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        // ���� ���� ǥ�� ������Ʈ
        ConnectionStatus.text = "���� ������ ����";

        // �κ� ����
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        // ���� ���� ǥ�� ������Ʈ
        ConnectionStatus.text = "�κ� ����";
    }
   
    public override void OnCreatedRoom()
    {
        // ���� ���� ǥ�� ������Ʈ
        ConnectionStatus.text = "�� ���� �Ϸ�";
    }

    public override void OnJoinedRoom()
    {
        // �濡 �������� �� �α��� ���� ��Ȱ��ȭ�ϰ� ���� ���� Ȱ��ȭ
        loginScene.SetActive(false);
        mainScene.SetActive(true);
        // ���� ���� ǥ�� ������Ʈ
        ConnectionStatus.text = "�� ���� �Ϸ�";
        // PhotonNetwork.Instantiate�� ����Ͽ� �÷��̾� ������ ����
        GameObject playerObj = PhotonNetwork.Instantiate(playerPrefab.name, Vector2.zero, Quaternion.identity);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;

        foreach (var room in roomList)
        {
            // ���� ������ ���
            if (room.RemovedFromList == true)
            {
                // ��ųʸ����� �ش� �� ����
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            // �� ������ ����(����)�� ���
            else
            {
                // ���� ó�� ������ ���
                if (roomDict.ContainsKey(room.Name) == false)
                {
                    // �� �������� �����Ͽ� ��ũ�� �信 �߰�
                    GameObject _room = Instantiate(roomPrefab, scrollContent);
                    _room.GetComponent<RoomData>().RoomInfo = room;
                    roomDict.Add(room.Name, _room);
                }
                // �� ������ �����ϴ� ���
                else
                {
                    // ��ųʸ����� �ش� �� ã�� ����
                    roomDict.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = room;
                }
            }
        }
    }

    #region UI_BUTTON_CALLBACK
    // Room ��ư Ŭ�� �� ȣ�� (�� ����)
    public void OnMakeRoomClick()
    {
        // �� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 10;

        // ��ǲ�ʵ尡 ��� ������
        if (string.IsNullOrEmpty(roomNameText.text))
        {
            // ���� �� �̸� �ο�
            roomNameText.text = $"Room_{Random.Range(1, 100):000}";
        }

        // �� ���� �õ�
        PhotonNetwork.CreateRoom(roomNameText.text, ro);
    }
    #endregion
}
