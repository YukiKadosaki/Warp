//プレイ時間、死亡回数などを記録

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private float m_ClimbingTime;
    private bool m_Timekeeping;
    private int m_DeathCount;
    private float m_countTime;


    public float ClimbingTime
    {
        get => m_ClimbingTime;
        set { m_ClimbingTime = value; }
    }
    public bool Timekeeping
    {
        get => m_Timekeeping;
        set { m_Timekeeping = value; }
    }
    public int DeathCount
    {
        get => m_DeathCount;
        set { m_DeathCount = value; }
    }
    public float CountTime
    {
        get => m_countTime;
        set { m_countTime = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        Timekeeping = true;
        //DeathCountは死亡時にプレイヤーが増やす
        DeathCount = PlayerPrefs.GetInt("DeathCount", 0);
        CountTime = 0;
        Timekeeping = true;
        ClimbingTime = PlayerPrefs.GetFloat("PlayTime", 0);
    }

    // Update is called once per frame
    void Update()
    {
        TryTimeKeep();
        CountTime += Time.deltaTime;
        if(CountTime > 1)
        {
            PlayerPrefs.SetFloat("PlayTime", ClimbingTime);
            PlayerPrefs.Save();
            CountTime = 0;
        }
    }

    private void TryTimeKeep()
    {
        if (Timekeeping)
        {
            ClimbingTime += Time.deltaTime;
        }
    }

}
