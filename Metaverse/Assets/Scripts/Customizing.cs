using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customizing : MonoBehaviour
{
    public List<Button> prefabbtn;
    public List<Sprite> customitem;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        // �������� ��������Ʈ ����
        int randomIndex = Random.Range(0, customitem.Count + 1); // 0���� customitem.Count���� ������ �ε���
        if (randomIndex == 0)
        {
            spriteRenderer.sprite = null;
        }
        else if (randomIndex <= customitem.Count)
        {
            spriteRenderer.sprite = customitem[randomIndex];
        }
        else
        {
            Debug.LogError("Invalid random index: " + randomIndex);
        }
    }

    public void ClickTap(int id)
    {
        if (id == 0)
        {
            spriteRenderer.sprite = null;
        }
        else if (id <= customitem.Count)
        {
            spriteRenderer.sprite = customitem[id];
        }
        else
        {
            Debug.LogError("Invalid id: " + id);
        }
    }
}
