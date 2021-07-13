using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMapTrigger : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_Destination;
    private MoveCamera m_Camera;

    public Vector3 Destination
    {
        get => m_Destination;
        set { m_Destination = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Call");
            StartCoroutine(m_Camera.MoveToDestination(Destination));
            Debug.Log("CallEnd");
        }

        m_Camera.DebugLog();
    }
}
