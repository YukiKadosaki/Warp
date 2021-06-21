using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sample : MonoBehaviour
{
    private const float copySpeed = 3;//コピーの移動速度
    private const float jumpForce = 8;//ジャンプの強さ
    private const float maxJumpTime = 0.3f;//最大ジャンプの時間

    [SerializeField]
    private GameObject[] groundCheckObjects;


    private float m_MoveSpeed = 3;
    private Rigidbody2D m_RigidBody2D;
    private Transform m_Transform;
    private GameObject m_PlayerCopy;
    private SpriteRenderer m_SpriteRenderer;
    private bool m_DirectionLeft=false;//左を向いているかどうか
    private float m_JumpTimer;//ジャンプしている時間
    private bool m_IsFirstJumping;//一段目ジャンプボタンを押し続けている間true
    private bool m_IsSecondJumping;//二段目ジャンプボタンを押し続けている間true
    private bool m_Jumping;//ジャンプボタンを押し続けている間true
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

        //ジャンプ開始
        if (IsGrounded && Input.GetKeyDown(KeyCode.Z))//一段目
        {
            IsFirstJumping = true;
            Jumping = true;
        }
        else if (!IsGrounded && !IsSecondJumping)//二段目
        {
            IsSecondJumping = true;
        }

        //ジャンプ終了
        if (Input.GetKeyUp(KeyCode.Z) && Jumping)
        {
            JumpEnd = true;
        }
        
        CalculateJumpTime();

        //分身を作る
        if (Input.GetKeyDown(KeyCode.X) && GameObject.FindGameObjectsWithTag("Copy").Length  < 1)
        {
            CreateCopy();
        }

        //着地判定を計算
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Jump();
        Move();
    }

    //分身から指定された座標にワープする
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
        //ジャンプタイマーのチェックは終了

        //Xが押された瞬間
        if (0 < JumpTimer && JumpTimer < maxJumpTime && Jumping)
        {
            m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, 0, 0);
            m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, jumpForce, 0); 
            IsFirstJumping = true;
        }
        if (JumpEnd || JumpTimer > maxJumpTime)//Xが離されたか長時間ジャンプしたとき
        {
            m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, m_RigidBody2D.velocity.y / 2, 0);
            JumpTimer = 0;
            JumpEnd = false;
            IsFirstJumping = false;
            Jumping = false;
        }
        

        
    }

    //何秒間ジャンプ中かを計る
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
        //接地判定オブジェクトが何かに重なっているかどうかをチェック
        for (int i = 0; i < groundCheckObjects.Length; i++)
        {
            groundCheckCollider[i] = Physics2D.OverlapPoint(groundCheckObjects[i].transform.position);
            //接地判定オブジェクトのうち、1つでも何かに重なっていたら接地しているものとして終了
            if (groundCheckCollider[i] != null)
            {
                IsGrounded = true;
                return;
            }
        }
        //ここまできたということは何も重なっていないということなので、接地していないと判断する
        IsGrounded = false;
    }
}
        
