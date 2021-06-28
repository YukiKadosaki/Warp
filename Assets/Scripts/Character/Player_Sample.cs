using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sample : MonoBehaviour
{
    private const float copySpeed = 5;//�R�s�[�̈ړ����x
    private const float jumpForce = 14;//1�i�ڃW�����v�̋���
    private const float secondJumpForce = 10;//2�i�ڃW�����v�̋���
    private const float maxJumpTime = 0.3f;//�ő�W�����v�̎���
    private const float endSpeed = 1.0f;//�W�����v�I�����̑��x
    private const float reduceJumpSpeedRate = 0.3f;//��𗣂����Ƃ��̃W�����v�͂̌�������
    private const float maxFallingSpeed = -10f;//�ő嗎�����x
    private const float moveSpeed = 4;//�������ړ����x

    [SerializeField]
    private GameObject[] groundCheckObjects;
    [SerializeField]
    private GameObject[] rightWallCheckObjects;//�E�ɐi�߂邩�ǂ����𒲂ׂ�
    [SerializeField]
    private GameObject rightDownWallCheckObject;



    private float m_MoveSpeed = 3;
    private Rigidbody2D m_RigidBody2D;
    private Transform m_Transform;
    private GameObject m_PlayerCopy;
    private SpriteRenderer m_SpriteRenderer;
    private bool m_DirectionLeft = false;//���������Ă��邩�ǂ���
    private float m_JumpTimer;//�W�����v���Ă��鎞��
    private bool m_IsFirstJumping;//��i�ڃW�����v�{�^�������������Ă����true
    private bool m_IsSecondJumping;//��i�ڃW�����v�{�^�������������Ă����true
    private bool m_JumpEnd;
    private bool m_isGrounded;
    private bool m_isGroundedPrev;
    private bool m_CanLeftMove;
    private bool m_CanRightMove;
    private bool m_CanSecondJump;


    public float MoveSpeed
    {
        set
        {
            m_MoveSpeed = value;
            if (m_MoveSpeed < 0)
            {
                m_MoveSpeed = 0;
            }
        }
        get => m_MoveSpeed;
    }

    public bool DirectionLeft
    {
        set { m_DirectionLeft = value; }
        get => m_DirectionLeft;
    }

    public float JumpTimer
    {
        set
        {
            m_JumpTimer = value;
            if (m_JumpTimer < 0)
            {
                m_JumpTimer = 0;
            }
        }
        get => m_JumpTimer;
    }
    public bool IsFirstJumping
    {
        set { m_IsFirstJumping = value; }
        get => m_IsFirstJumping;
    }

    public bool IsSecondJumping
    {
        set { m_IsSecondJumping = value; }
        get => m_IsSecondJumping;
    }
    public bool JumpEnd
    {
        set { m_JumpEnd = value; }
        get => m_JumpEnd;
    }

    public bool IsGrounded
    {
        set { m_isGrounded = value; }
        get => m_isGrounded;
    }

    public bool IsGroundedPrev
    {
        set { m_isGroundedPrev = value; }
        get => m_isGroundedPrev;
    }

    public bool CanLeftMove
    {
        set { m_CanLeftMove = value; }
        get => m_CanLeftMove;
    }

    public bool CanRightMove
    {
        set { m_CanRightMove = value; }
        get => m_CanRightMove;
    }
    public bool CanSecondJump
    {
        set { m_CanSecondJump = value; }
        get => m_CanSecondJump;
    }



    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody2D = this.GetComponent<Rigidbody2D>();
        m_Transform = this.transform;
        m_PlayerCopy = (GameObject)Resources.Load("Prefab/PlayerCopy");
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Vel:" + m_RigidBody2D.velocity.y);

        //�W�����v�J�n
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (IsGrounded)//1�i�ڃW�����v
            {
                IsFirstJumping = true;
            }
            else if (!IsSecondJumping && CanSecondJump)//�󒆃W�����v
            {
                IsSecondJumping = true;
                CanSecondJump = false;
            }
        }



        //�W�����v�I��
        if (Input.GetKeyUp(KeyCode.Z))
        {
            JumpEnd = true;
        }

        CalculateJumpTime();

        //���g�����
        if (Input.GetKeyDown(KeyCode.X) && GameObject.FindGameObjectsWithTag("Copy").Length == 0)
        {
            CreateCopy();
        }

        //���n������v�Z
        GroundCheck();
        //�ǂ̔�����v�Z
        RightWallCheck();
        //�����X�s�[�h�͍ő�7
        CheckFallingSpeed();
    }

    private void FixedUpdate()
    {
        Jump();
        Move();
    }

    //���g����w�肳�ꂽ���W�Ƀ��[�v����
    public void WarpToCopy(Vector3 pos)
    {
        m_Transform.position = pos;
        m_RigidBody2D.velocity = Vector3.zero;
        CanSecondJump = true;
    }

    private void CreateCopy()
    {

        GameObject playerCopy = Instantiate(m_PlayerCopy);


        if (DirectionLeft)
        {
            playerCopy.transform.position = m_Transform.position + Vector3.left * 0.3f;

            playerCopy.GetComponent<Rigidbody2D>().velocity = new Vector3(-copySpeed, 0, 0);
            playerCopy.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            playerCopy.transform.position = m_Transform.position + Vector3.right * 0.3
                f;
            playerCopy.GetComponent<Rigidbody2D>().velocity = new Vector3(copySpeed, 0, 0);
        }

    }


    private void Move()
    {
        //�ړ������Ƒ��x��\��
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        //�E�ɕǂ��Ȃ��ꍇ�ړ�����B
        //���ɍs���Ƃ��̓v���C���[�̑傫����-1�{����֌W�ŉE�𒲂ׂ邾���ŗǂ�
        if (CanRightMove)
        {
            m_RigidBody2D.velocity = new Vector3(x, m_RigidBody2D.velocity.y);
        }
        else//�E�ɕǂ�����ꍇ�ړ��ł��Ȃ�
        {
            m_RigidBody2D.velocity = new Vector3(0, m_RigidBody2D.velocity.y);
        }

        //�v���C���[�̌�����ς���
        if (x > 0)
        {
            m_Transform.localScale = new Vector3(1, 1, 1);
            DirectionLeft = false;
        }
        else if (x < 0)
        {
            m_Transform.localScale = new Vector3(-1, 1, 1);
            DirectionLeft = true;
        }

    }

    private void Jump()
    {
        //1�i�ڃW�����v
        if (IsFirstJumping)
        {
            if (0 < JumpTimer && JumpTimer < maxJumpTime && !JumpEnd)//1�i�W�����v���͏�ɓ���
            {
                //m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, 0, 0);
                m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, jumpForce * (maxJumpTime - JumpTimer) / maxJumpTime + endSpeed, 0);
            }
            else//X�������ꂽ�������ԃW�����v�����Ƃ��W�����v����߂�
            {
                m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, m_RigidBody2D.velocity.y * reduceJumpSpeedRate, 0);
                JumpTimer = 0;
                JumpEnd = false;
                IsFirstJumping = false;
                return;
            }
        }
        else if (IsSecondJumping)//2�i�W�����v��
        {

            if (0 < JumpTimer && JumpTimer < maxJumpTime && !JumpEnd)//2�i�W�����v���͏�ɓ����A�W�����v���Ԃ͔���
            {
                //m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, 0, 0);
                m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, secondJumpForce * (maxJumpTime - JumpTimer) / maxJumpTime + endSpeed, 0);
            }
            else//X�������ꂽ�������ԃW�����v�����Ƃ��W�����v����߂�
            {
                m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, m_RigidBody2D.velocity.y * reduceJumpSpeedRate, 0);
                JumpTimer = 0;
                JumpEnd = false;
                IsSecondJumping = false;
                return;
            }
        }
        else if (JumpEnd)//�W�����v�͏I��������ǎ�𗣂��ꂽ�Ƃ��̏���
        {
            JumpTimer = 0;
            JumpEnd = false;
        }



    }

    //���b�ԃW�����v�������v��
    private void CalculateJumpTime()
    {

        if (IsFirstJumping || IsSecondJumping)
        {
            JumpTimer += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            JumpTimer = 0;
        }


    }

    //���n����(2�i�ڃW�����v����)
    //JumpEnd��false��
    private void GroundCheck()
    {
        IsGroundedPrev = IsGrounded;
        Collider2D[] groundCheckCollider = new Collider2D[groundCheckObjects.Length];
        //�ڒn����I�u�W�F�N�g�������ɏd�Ȃ��Ă��邩�ǂ������`�F�b�N
        for (int i = 0; i < groundCheckObjects.Length; i++)
        {
            groundCheckCollider[i] = Physics2D.OverlapPoint(groundCheckObjects[i].transform.position);
            //�ڒn����I�u�W�F�N�g�̂����A1�ł������ɏd�Ȃ��Ă�����ڒn���Ă�����̂Ƃ��ďI��
            if (groundCheckCollider[i] != null)
            {
                IsGrounded = true;
                CanSecondJump = true;
                JumpEnd = false;
                return;
            }
        }
        //�����܂ł����Ƃ������Ƃ͉����d�Ȃ��Ă��Ȃ��Ƃ������ƂȂ̂ŁA�ڒn���Ă��Ȃ��Ɣ��f����
        IsGrounded = false;
    }


    //�ǂɂ߂荞�܂�Ȃ��悤�ɂ��鏈��
    private void RightWallCheck()
    {
        //�E���̕ǃ`�F�b�N
        //�W�����v�Ńu���b�N�ɂԂ���Ȃ���o��Ƃ��ɉ��̕�������������̂�h�~���邽��
        //�ڒn���Ă���Ƃ��͊֌W�Ȃ�
        if (!IsGrounded)
        {
            Collider2D rightDownWallCheckCollider = new Collider2D();

            rightDownWallCheckCollider = Physics2D.OverlapPoint(rightDownWallCheckObject.transform.position);

            //�u���b�N�ɐG��Ă���ΉE�ɍs���Ȃ��B
            if (rightDownWallCheckCollider != null)
            {
                CanRightMove = false;
                return;
            }
        }

        //�E���̓u���b�N�ɐG��Ă��Ȃ��̂ő��𒲂ׂ�

        Collider2D[] rightWallCheckCollider = new Collider2D[rightWallCheckObjects.Length];
        //�ǔ���I�u�W�F�N�g�������ɏd�Ȃ��Ă��邩�ǂ������`�F�b�N
        for (int i = 0; i < rightWallCheckObjects.Length; i++)
        {
            rightWallCheckCollider[i] = Physics2D.OverlapPoint(rightWallCheckObjects[i].transform.position);
            //�ǔ���I�u�W�F�N�g�̂����A1�ł������ɏd�Ȃ��Ă�����ǂɐG��Ă�����̂Ƃ��ďI��
            if (rightWallCheckCollider[i] != null)
            {
                CanRightMove = false;
                return;
            }
        }
        //�����܂ł����Ƃ������Ƃ͉E�ɕǂ��Ȃ��Ƃ������ƂȂ̂�true
        CanRightMove = true;
    }

    private void CheckFallingSpeed()
    {
        if(m_RigidBody2D.velocity.y < maxFallingSpeed)
        {
            m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, maxFallingSpeed, 0);
        }
    }
}

