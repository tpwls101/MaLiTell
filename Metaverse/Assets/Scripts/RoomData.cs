using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomData : MonoBehaviour
{
    private TMP_Text RoomInfoText;
    private RoomInfo _roomInfo;

    public TMP_InputField userIdText; // �ν����Ϳ��� ���� �Ҵ�

    public RoomInfo RoomInfo
    {
        get
        {
            return _roomInfo;
        }
        set
        {
            _roomInfo = value;
            string roomDisplayText = _roomInfo.IsOpen ? $"{_roomInfo.Name} ({_roomInfo.PlayerCount}/{_roomInfo.MaxPlayers})" : $"{_roomInfo.Name} (��й�ȣ �Է� �ʿ�)";
            RoomInfoText.text = roomDisplayText;
            // ��ư�� Ŭ�� �̺�Ʈ�� �Լ��� ����
            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(_roomInfo.Name));
        }
    }

    private void Awake()
    {
        RoomInfoText = GetComponentInChildren<TMP_Text>();

        if (RoomInfoText == null)
        {
            Debug.LogError("RoomInfoText not found!");
        }

        // ��� �������� userIdText�� ã���� �õ�
        userIdText = GameObject.Find("ID").GetComponent<TMP_InputField>(); // ������ ��η� �����ؾ� �մϴ�.

        if (userIdText == null)
        {
            Debug.LogError("userIdText not found!");
        }
    }

    void OnEnterRoom(string roomName)
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 10;

        if (userIdText != null && !string.IsNullOrEmpty(userIdText.text))
        {
            PhotonNetwork.NickName = userIdText.text;
        }
        else
        {
            // �г����� ��� �ִ� ��� ���� ���̵� �ο�
            PhotonNetwork.NickName = $"User_{Random.Range(0, 100):00}";
        }

        PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);
    }
}
