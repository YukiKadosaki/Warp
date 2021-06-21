using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sample : MonoBehaviour
{
    private const float copySpeed = 3;//�R�s�[�̈ړ����x
    private const float jumpForce = 8;//�W�����v�̋���
    private const float maxJumpTime = 0.3f;//�ő�W�����v�̎���

    [SerializeField]
    private GameObject[] groundCheckObjects;


    private float m_MoveSpeed = 3;
    private Rigidbody2D m_RigidBody2D;
    private Transform m_Transform;
    private GameObject m_PlayerCopy;
    private SpriteRenderer m_SpriteRenderer;
    private bool m_DirectionLeft=false;//���������Ă��邩�ǂ���
    private float m_JumpTimer;//�W�����v���Ă��鎞��
    private bool m_IsFirstJumping;//��i�ڃW�����v�{�^�������������Ă����true
    private bool m_IsSecondJumping;//��i�ڃW�����v�{�^�������������Ă����true
    private bool m_Jumping;//�W�����v�{�^�������������Ă����true
    private bool m_JumpEnd;
    private bool m_isGrounded;
    private bool m_isGroundedPrev;


    public float MoveSpeed
    {
        set 
        { 
            m_MoveSpeed = value;
            if(m_MoveSpeed < 0)
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
            if(m_JumpTimer < 0)
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

    public bool Jumping
    {
        set { m_Jumping = value; }
        get => m_Jumping;
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

        //�W�����v�J�n
        if (IsGrounded && Input.GetKeyDown(KeyCode.Z))//��i��
        {
            IsFirstJumping = true;
            Jumping = true;
        }
        else if (!IsGrounded && !IsSecondJumping)//��i��
        {
            IsSecondJumping = true;
        }

        //�W�����v�I��
        if (Input.GetKeyUp(KeyCode.Z) && Jumping)
        {
            JumpEnd = true;
        }
        
        CalculateJumpTime();

        //���g�����
        if (Input.GetKeyDown(KeyCode.X) && GameObject.FindGameObjectsWithTag("Copy").Length  < 1)
        {
            CreateCopy();
        }

        //���n������v�Z
        GroundCheck();
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
        Jumping = false;
    }

    private void CreateCopy()
    {

        GameObject playerCopy = Instantiate(m_PlayerCopy);
        playerCopy.transform.position = m_Transform.position;

        if (DirectionLeft)
        {
            playerCopy.GetComponent<Rigidbody2D>().velocity = new Vector3(-copySpeed, 0, 0);
            playerCopy.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            playerCopy.GetComponent<Rigidbody2D>().velocity = new Vector3(copySpeed, 0, 0);
        }
        
    }


    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * 3;
        m_RigidBody2D.velocity = new Vector3(x, m_RigidBody2D.velocity.y);
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
        //�W�����v�^�C�}�[�̃`�F�b�N�͏I��

        //X�������ꂽ�u��
        if (0 < JumpTimer && JumpTimer < maxJumpTime && Jumping)
        {
            m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, 0, 0);
            m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, jumpForce, 0); 
            IsFirstJumping = true;
        }
        if (JumpEnd || JumpTimer > maxJumpTime)//X�������ꂽ�������ԃW�����v�����Ƃ�
        {
            m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, m_RigidBody2D.velocity.y / 2, 0);
            JumpTimer = 0;
            JumpEnd = false;
            IsFirstJumping = false;
            Jumping = false;
        }
        

        
    }

    //���b�ԃW�����v�������v��
    private void CalculateJumpTime()
    {
        
        if (Jumping)
        {
            JumpTimer += Time.deltaTime;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            JumpTimer = 0;
        }


    }

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
                return;
            }
        }
        //�����܂ł����Ƃ������Ƃ͉����d�Ȃ��Ă��Ȃ��Ƃ������ƂȂ̂ŁA�ڒn���Ă��Ȃ��Ɣ��f����
        IsGrounded = false;
    }
}
        
