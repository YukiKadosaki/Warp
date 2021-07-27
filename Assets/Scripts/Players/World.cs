//データのロードなどをする

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public int deathCount;
    public float time;
    public int psNumber;
    private PlayerStart[] playerStart;
    private NextMapTrigger[] nextMapTrigger;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        psNumber = PlayerPrefs.GetInt("ReturnPSNum", 0);
        deathCount = PlayerPrefs.GetInt("DeathCount", 0);
        time = PlayerPrefs.GetFloat("PlayTime", 0);
    }
}
