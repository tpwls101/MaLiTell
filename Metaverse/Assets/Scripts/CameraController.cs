using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraspeed = 3.0f;

    private GameObject player;

    private void Start()
    {
        // Player�� ã�Ƽ� �Ҵ�
        FindPlayer();
    }

    private void Update()
    {
        if (player != null)
        {
            // �÷��̾� ��ġ �ľ�
            Vector3 dir = player.transform.position - transform.position;
            // �÷��̾� ��ġ�� ���� ī�޶� ��ġ ����
            Vector3 moveVector = new Vector3(dir.x * cameraspeed * Time.deltaTime, dir.y * cameraspeed * Time.deltaTime, 0.0f);
            transform.Translate(moveVector);
        }
        else
        {
            // �÷��̾ ���ٸ� ���ο� �÷��̾� ã�� �õ�
            FindPlayer();
        }
    }

    private void FindPlayer()
    {
        // Player�� ã�Ƽ� �Ҵ�
        player = GameObject.Find("Player(Clone)");

        if (player == null)
        {
            Debug.LogError("Player(Clone) not found!");
        }
    }
}
