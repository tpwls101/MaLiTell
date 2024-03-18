using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoLobby : MonoBehaviour
{
    public GameObject mainScene;
    public GameObject loginScene;

    public void OnLobbyButtonClicked()
    {
        // mainScene�� Ȱ��ȭ�� ������ ��
        if (mainScene)
        {
            // �� ������
            PhotonNetwork.LeaveRoom();
            // mainScene�� ��Ȱ��ȭ�ϰ� loginScene�� Ȱ��ȭ
            mainScene.SetActive(false);
            loginScene.SetActive(true);
        }
    }
}
