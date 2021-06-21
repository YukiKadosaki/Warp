/*ï™êgÉNÉâÉX*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copy : MonoBehaviour
{
    private Player_Sample m_Player;
    private Transform m_Transform;

    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Sample>();
        m_Transform = this.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall")){
            m_Player.WarpToCopy(m_Transform.position);
            Destroy(this.gameObject);
        }
        
    }
}
