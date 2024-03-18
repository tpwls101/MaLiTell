using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;

public class MicBtn : MonoBehaviourPun
{
    public Sprite[] sprites;
    public bool isClicked;
    public Recorder recorder;

    private Image micButtonImage;

    private void Start()
    {
        micButtonImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            // "M" Ű�� ������ �� ����
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMic();
            }
        }
    }

    public void BtnClick()
    {
        if (photonView.IsMine)
        {
            ToggleMic();
        }
    }

    private void ToggleMic()
    {
        isClicked = !isClicked;

        // ��ư �̹��� �� ���� ���� ���� ����
        if (isClicked)
        {
            micButtonImage.sprite = sprites[0];
            recorder.TransmitEnabled = true;
        }
        else
        {
            micButtonImage.sprite = sprites[1];
            recorder.TransmitEnabled = false;
        }
    }
}
