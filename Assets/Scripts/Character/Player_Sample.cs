using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sample : MonoBehaviour
{
    private const float copySpeed = 3;//コピーの移動速度

    private Rigidbody2D m_RigidBody2D;
    private Transform m_Transform;
    private GameObject m_PlayerCopy;
    private SpriteRenderer m_SpriteRenderer;
    private bool m_DirectionLeft=false;//左を向いているかどうか


    public bool DirectionLeft
    {
        set { m_DirectionLeft = value; }
        get => m_DirectionLeft;
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
        Move();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CreateCopy();
        }


    }

    //分身から指定された座標にワープする
    public void WarpToCopy(Vector3 pos)
    {
        m_Transform.position = pos;
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
            DirectionLeft = false;
        }
        else if (x < 0)
        {
            DirectionLeft = true;
        }

        m_SpriteRenderer.flipX= DirectionLeft;
    }
        
}
