using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Sample : MonoBehaviour
{
    private Rigidbody2D m_RigidBody2D;
    private Transform m_Transform;
    private GameObject m_PlayerCopy;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody2D = this.GetComponent<Rigidbody2D>();
        m_Transform = this.transform;
        m_PlayerCopy = (GameObject)Resources.Load("Prefab/PlayerCopy");
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * 3;

        m_RigidBody2D.velocity = new Vector3(x, m_RigidBody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject playerCopy = Instantiate(m_PlayerCopy);
            playerCopy.transform.position = m_Transform.position;
            playerCopy.GetComponent<Rigidbody2D>().velocity = new Vector3(3, 0, 0);
        }

    }

    //分身から指定された座標にワープする
    public void WarpToCopy(Vector3 pos)
    {
        m_Transform.position = pos;
    }
}
