using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Sample : MonoBehaviour
{
    private const float copySpeed = 5;//分身の移動速度
    private const float jumpForce = 15;//1段目のジャンプ力
    private const float secondJumpForce = 11;//2段目のジャンプ力
    private const float maxJumpTime = 0.3f;//最大ジャンプの秒数
    private const float endSpeed = 1.0f;//指を離したときのスピード
    private const float reduceJumpSpeedRate = 0.3f;//ジャンプをやめたときのスピード減少率
    private const float maxFallingSpeed = -10f;//最大落下速度
    private const float moveSpeed = 6;//横移動速度

    [SerializeField]
    private GameObject[] groundCheckObjects;
    [SerializeField]
    private GameObject[] rightWallCheckObjects;//右の壁をチェックする
    [SerializeField]
    private GameObject rightDownWallCheckObject;



    private float m_MoveSpeed = 3;
    private Rigidbody2D m_RigidBody2D;
    private Transform m_Transform;
    private GameObject m_PlayerCopy;
    private SpriteRenderer m_SpriteRenderer;
    private bool m_DirectionLeft = false;//左を向いてればtrue
    private float m_JumpTimer;//�W�����v���Ă��鎞��
    private bool m_IsFirstJumping;//一段ジャンプ中true
    private bool m_IsSecondJumping;//2段ジャンプ中true
    private bool m_JumpEnd;
    private bool m_isGrounded;
    private bool m_isGroundedPrev;
    private bool m_CanLeftMove;
    private bool m_CanRightMove;
    private bool m_CanSecondJump;
    private bool m_PlayerFlosen;//trueなら操作を受け付けなくなる。KillPlayerの時などに呼ばれる
    private PlayerStart[] m_PS;


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
    public bool PlayerFlosen
    {
        set { m_PlayerFlosen = value; }
        get => m_PlayerFlosen;
    }
    public PlayerStart[] PS
    {
        get => m_PS;
        set { m_PS = value; }
    }



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 50;//フレームレートを50に
        m_RigidBody2D = this.GetComponent<Rigidbody2D>();
        m_Transform = this.transform;
        m_PlayerCopy = (GameObject)Resources.Load("Prefab/PlayerCopy");
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        int i = 0;
        PS = new PlayerStart[GameObject.FindGameObjectsWithTag("PlayerStart").Length];
        foreach(GameObject ps in GameObject.FindGameObjectsWithTag("PlayerStart"))
        {
            PS[i] = ps.GetComponent<PlayerStart>();
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerFlosen)
        {
            //�W�����v�J�n
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (IsGrounded)//1段ジャンプ
                {
                    IsFirstJumping = true;
                }
                else if (!IsSecondJumping && CanSecondJump)//2段ジャンプ
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
            if (Input.GetKeyDown(KeyCode.X) && GameObject.FindGameObjectsWithTag("Copy").Length == 0 && CanRightMove)
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
    }

    private void FixedUpdate()
    {
        if (!PlayerFlosen)
        {
            Jump();
            Move();
        }
    }

    //分身の位置まで移動する
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
            playerCopy.transform.position = m_Transform.position + Vector3.right * 0.3f;
            playerCopy.GetComponent<Rigidbody2D>().velocity = new Vector3(copySpeed, 0, 0);
        }

    }


    private void Move()
    {
        //移動速度計算
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        //壁に触れていないとき
        //移動する
        if (CanRightMove)
        {
            m_RigidBody2D.velocity = new Vector3(x, m_RigidBody2D.velocity.y);
        }
        else//壁に触れているとき
        {
            m_RigidBody2D.velocity = new Vector3(0, m_RigidBody2D.velocity.y);
        }

        //プレイヤーの向きの変更
        if (x > 0)
        {
            //大きいリフトに乗ったときのため
            if (null == m_Transform.parent)
            {
                m_Transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                Vector3 scale = transform.parent.lossyScale;
                m_Transform.localScale = new Vector3(1f / scale.x, 1f / scale.y, 1f / scale.z);
            }
            DirectionLeft = false;
        }
        else if (x < 0)
        {
            //大きいリフトに乗ったときのため
            if (null == m_Transform.parent)
            {
                m_Transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                Vector3 scale = transform.parent.lossyScale;
                m_Transform.localScale = new Vector3(-1f / scale.x, 1f / scale.y, 1f / scale.z);
            }
            DirectionLeft = true;
        }

    }

    private void Jump()
    {
        //1段目ジャンプ
        if (IsFirstJumping)
        {
            if (0 < JumpTimer && JumpTimer < maxJumpTime && !JumpEnd)//ジャンプ中
            {
                //m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, 0, 0);
                m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, jumpForce * (maxJumpTime - JumpTimer) / maxJumpTime + endSpeed, 0);
            }
            else//ジャンプをやめたとき
            {
                m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, m_RigidBody2D.velocity.y * reduceJumpSpeedRate, 0);
                JumpTimer = 0;
                JumpEnd = false;
                IsFirstJumping = false;
                return;
            }
        }
        else if (IsSecondJumping)//2段目ジャンプ
        {

            if (0 < JumpTimer && JumpTimer < maxJumpTime && !JumpEnd)//2段目ジャンプ中
            {
                //m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, 0, 0);
                m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, secondJumpForce * (maxJumpTime - JumpTimer) / maxJumpTime + endSpeed, 0);
            }
            else//2段目ジャンプ終了
            {
                m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, m_RigidBody2D.velocity.y * reduceJumpSpeedRate, 0);
                JumpTimer = 0;
                JumpEnd = false;
                IsSecondJumping = false;
                return;
            }
        }
        else if (JumpEnd)//落下中に手を離したとき
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
            if (groundCheckCollider[i] != null && (groundCheckCollider[i].isTrigger == false || groundCheckCollider[i].CompareTag("LiftRideCheck")))
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


    //壁の判定のチェック
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
            if (rightDownWallCheckCollider != null && rightDownWallCheckCollider.isTrigger == false)
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
            if (rightWallCheckCollider[i] != null && rightWallCheckCollider[i].isTrigger == false)
            {
                CanRightMove = false;
                return;
            }
        }
        //ここまで来ていると言うことは右に壁が無いということなのでtrue
        CanRightMove = true;
    }

    private void CheckFallingSpeed()
    {
        if(m_RigidBody2D.velocity.y < maxFallingSpeed)
        {
            m_RigidBody2D.velocity = new Vector3(m_RigidBody2D.velocity.x, maxFallingSpeed, 0);
        }
    }

    //Killer.csの付いたオブジェクトに触れると呼ばれる
    //プレイヤーを操作不能にし、プレイヤーを黒くする。その後プレイヤーを消去し、復活の処理をする。
    public IEnumerator KillPlayer()
    {
        //動けなくする
        m_RigidBody2D.gravityScale = 0;
        m_RigidBody2D.velocity = Vector3.zero;
        PlayerFlosen = true;

        //分身が居るなら分身を消す
        GameObject copy;
        if (copy = GameObject.FindGameObjectWithTag("Copy"))
        {
            Destroy(copy);
        }

        //色を黒くする
        int dc = 2;//decreaseColor 色の減少値
        Color color = m_SpriteRenderer.color;
        while (m_SpriteRenderer.color.g > 0)
        {
            float dcf = dc * Time.deltaTime;//decrease color float
            color = new Color(color.r, color.g - dcf, color.b - dcf, color.a);
            m_SpriteRenderer.color = color;
            yield return null;
        }
        m_SpriteRenderer.color = new Color(255, 0, 0, color.a);


        

        //新しいプレイヤーを出して自分は消える
        for(int i = 0;i < PS.Length; i++)
        {
            PS[i].CreatePlayer();
        }
        Destroy(this.gameObject);

        yield return null;
    }
}

