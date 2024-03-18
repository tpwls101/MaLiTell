using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviourPun
{
    private float moveSpeed = 3.0f;
    private Vector3 moveDirection;

    private Animator animator;
    private bool isSitting = false;
    private bool isSleeping = false;
    public GameObject mainScene;
    public TMP_Text nicknameText;
    private Rigidbody2D rb;
    public void Start()
    {
        mainScene = GameObject.Find("MainScene");
        rb = GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        if (photonView.IsMine)
        {
            nicknameText.text = PhotonNetwork.NickName;
            nicknameText.color = Color.white;
        }
        else
        {
            nicknameText.text = photonView.Owner.NickName;
            nicknameText.color = new Color(Random.value, Random.value, Random.value);
        }
        // Animator ������Ʈ
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (photonView.IsMine && mainScene)
        {
            // Left, a Ű�� ������ -1 / Right, d Ű�� ������ + 1 / �ƹ�Ű�� ������ ������ 0
            float moveX = Input.GetAxisRaw("Horizontal");
            // Down, s Ű�� ������ -1 / Up, w Ű�� ������ + 1 / �ƹ�Ű�� ������ ������ 0
            float moveY = Input.GetAxisRaw("Vertical");

            // ������ �ִϸ��̼� ���� �ѹ�
            float runNumber = 0.0f;

            // �׼� �ִϸ��̼� ���� �ѹ�
            float actionState = 0.0f;

            // ������ ����
            Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

            // Rigidbody 2D�� velocity �Ӽ��� ����Ͽ� �̵�
            rb.velocity = moveDirection * moveSpeed;

            // �������� ���� ��
            if (moveX != 0 || moveY != 0)
            {
                // �������� �̵��� �� ���� �ٶ󺸱�
                if (moveX < 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    // �÷��̾ ������ �ٶ� ��, �г��� �ؽ�Ʈ, ����ũ ��ư, �гθ� �������� ȸ���ϵ��� ����
                    nicknameText.rectTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }
                // ���������� �̵��� �� ������ �ٶ󺸱�
                else if (moveX > 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    // �÷��̾ �������� �ٶ� ��, �г���, ����ũ ��ư, �г� �ؽ�Ʈ�� ���������� ȸ���ϵ��� ����
                    nicknameText.rectTransform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                }
                // ���� ����Ʈ ���� �� runNumber ���� ����, moveSpeed ����
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // RunNumber = 1.0f �� �� Move ���� �׸� run2 �ִϸ��̼� ����
                    runNumber = 1.0f;
                    moveSpeed = 5.0f;
                }
                // �������� ���� �� Move �ִϸ��̼� ����
                animator.SetTrigger("Move");
                // Ű�� ���� RunNumber ������ runNumber�� ���� (�޸��� �ִϸ��̼�)
                animator.SetFloat("RunNumber", runNumber);
            }
            // �������� ���� ��
            else
            {
                // ������ �ִϸ��̼� �ʱ�ȭ �� ������ �ӵ� �ʱ�ȭ
                animator.ResetTrigger("Move");
                moveSpeed = 3.0f;
            }
            // Z Ű ������ ��
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // �⺻ ���� ���� false
                if (!isSitting)
                {
                    // ���� ���� true�� ���� �� SitDown �ִϸ��̼� ����
                    isSitting = true;
                    animator.SetTrigger("SitDown");
                }
                // ���� ���� �� ��
                else
                {
                    // ���� ���� false�� ���� �� SitUp �ִϸ��̼� ����
                    isSitting = false;
                    animator.SetTrigger("SitUp");
                }
            }
            // X Ű ������ ��
            if (Input.GetKeyDown(KeyCode.X))
            {
                // �⺻ ��� ���� false
                if (!isSleeping)
                {
                    // ��� ���� true�� ���� �� SleepDown �ִϸ��̼� ����
                    isSleeping = true;
                    animator.SetTrigger("SleepDown");
                }
                // ��� ������ ��
                else
                {
                    // ��� ���� false�� ���� �� SleepUp �ִϸ��̼� ����
                    isSleeping = false;
                    animator.SetTrigger("SleepUp");
                }
            }
            // �׼� �ִϸ��̼ǿ� ���� ó��
            // �����̽� �� ������ ��
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // ����2 �ִϸ��̼ǿ� �ش��ϴ� actionState 0.1f�� ���� �� �ִϸ��̼� ����
                actionState = 0.1f;
                animator.SetFloat("ActionState", actionState);
                animator.SetTrigger("Action");
            }
            // F Ű ������ �� Ű ������ ���� ���� �ִϸ��̼� �ݺ�
            else if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
            {
                // �λ�1 �ִϸ��̼ǿ� �ش��ϴ� actionState 0.2f�� ���� �� �ִϸ��̼� ����
                actionState = 0.2f;
                animator.SetFloat("ActionState", actionState);
                animator.SetTrigger("Action");
            }
            // G Ű ������ �� Ű ������ ���� ���� �ִϸ��̼� �ݺ�
            else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
            {
                // �λ�2 �ִϸ��̼ǿ� �ش��ϴ� actionState 0.3f�� ���� �� �ִϸ��̼� ����
                actionState = 0.3f;
                animator.SetFloat("ActionState", actionState);
                animator.SetTrigger("Action");
            }
            // H Ű ������ ��
            else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
            {
                // �λ�3 �ִϸ��̼ǿ� �ش��ϴ� actionState 0.3f�� ���� �� �ִϸ��̼� ����
                actionState = 0.4f;
                animator.SetFloat("ActionState", actionState);
                animator.SetTrigger("Action");
            }
        }
       
    }
}
