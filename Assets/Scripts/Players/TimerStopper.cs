using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStopper : MonoBehaviour
{
    private TowerManager m_TowerManager;

    // Start is called before the first frame update
    void Start()
    {
        m_TowerManager = GameObject.FindGameObjectWithTag("TowerManager").GetComponent<TowerManager>();
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    //プレイヤーが触れたら計測終了
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_TowerManager.Timekeeping = false;
        }
    }
}
