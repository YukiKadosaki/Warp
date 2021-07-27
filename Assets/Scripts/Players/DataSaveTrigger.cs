//ƒf[ƒ^‚Ì‰Šú‰»

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveTrigger : MonoBehaviour
{
    private World m_World;

    private void Start()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        m_World = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //‰Šú‰»
        if(collision.CompareTag("Player") || collision.CompareTag("Copy"))
        {
            Debug.Log("HIT");
            PlayerPrefs.SetInt("ReturnPSNum", 0);
            PlayerPrefs.SetInt("DeathCount", 0);
            PlayerPrefs.SetFloat("PlayTime", 0);
            PlayerPrefs.Save();
            m_World.psNumber = 0;
            m_World.deathCount = 0;
            m_World.time = 0;
        }
    }
}
