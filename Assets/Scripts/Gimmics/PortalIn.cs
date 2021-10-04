using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortalIn : MonoBehaviour
{
    [SerializeField]
    private int m_PortalNumber;
    private PortalOut m_PortalOut; 


    void Start()
    {
        //ワープ先のポータルを取得
        GameObject[] obj = GameObject.FindGameObjectsWithTag("PortalOut");
        foreach (GameObject o in obj)
        {
            if(o.GetComponent<PortalOut>().PortalNumber == m_PortalNumber)
            {
                m_PortalOut = o.GetComponent<PortalOut>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = m_PortalOut.transform.position;
    }
}
