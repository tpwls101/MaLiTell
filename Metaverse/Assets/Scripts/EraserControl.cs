using Photon.Voice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EraserControl : MonoBehaviour
{
    public Button eraserButton; // ���찳 ��ư
    public Texture2D eraser; // ���찳 ���콺 Ŀ��
    public Sprite[] sprites;
    public Image eraserimage;

    private EdgeCollider2D collider2D;
    private bool isEraserMode = false; // ���찳 ��� ����

    // ���찳 ��带 ��ȯ�ϴ� �޼���
    public void ToggleEraserMode()
    {
        isEraserMode = !isEraserMode;

        // ���찳 ����� ��� ���콺 Ŀ�� �̹��� ����
        if (isEraserMode)
        {
            Cursor.SetCursor(eraser, Vector2.zero, CursorMode.Auto);
        }
    }

    public void CancleEraserMode()
    {
        isEraserMode = false;
        if (!isEraserMode)
        {
            // ���찳 ��尡 �ƴ� ��� ���� ���콺 Ŀ�� �̹����� ����
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseEnter()
    {
        eraserimage.sprite = sprites[0];
    }

    private void OnMouseExit()
    {
        eraserimage.sprite = sprites[1];
    }
}
