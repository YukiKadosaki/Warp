//ƒvƒŒƒCŽžŠÔAŽ€–S‰ñ”‚È‚Ç‚ð‹L˜^

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private double m_ClimbingTime;
    private bool m_Timekeeping;
    private int m_DeathCount;


    public double ClimbingTime
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


    // Start is called before the first frame update
    void Start()
    {
        Timekeeping = true;
        DeathCount = 0;
        Timekeeping = true;
    }

    // Update is called once per frame
    void Update()
    {
        TryTimeKeep();
    }

    private void TryTimeKeep()
    {
        if (Timekeeping)
        {
            ClimbingTime += Time.deltaTime;
        }
    }

}
