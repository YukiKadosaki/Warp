//プレイ時間、死亡回数などを記録


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerManager : MonoBehaviour
{
    private PlayerSaveData m_SaveData;
    private bool m_Timekeeping;
    private World m_World;
    private PlayerStart[] m_PS;


    public bool Timekeeping
    {
        get => m_Timekeeping;
        set { m_Timekeeping = value; }
    }
    public PlayerSaveData SaveData
    {
        get => m_SaveData;
        set { m_SaveData = value; }
    }
    public PlayerStart[] PS
    {
        get => m_PS;
        set { m_PS = value; }
    }


    // Start is called before the first frame update
    void Awake()
    {
        m_World = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        //データのロード
        m_SaveData = m_World.LoadGame();

        Timekeeping = true;
        //DeathCountは死亡時にプレイヤーが増やす

        int i = 0;
        //PlayerStartを取得
        PS = new PlayerStart[GameObject.FindGameObjectsWithTag("PlayerStart").Length];
        foreach (GameObject m_PlayerSprite in GameObject.FindGameObjectsWithTag("PlayerStart"))
        {
            PS[i] = m_PlayerSprite.GetComponent<PlayerStart>();
            i++;
        }

        //プレイヤーを生成
        for (int j = 0;j < PS.Length; j++)
        {
            if(PS[j].PSNumber == SaveData.floorNumber)
            {
                PS[j].CreatePlayerVoid();
            }
        }
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
            SaveData.playTime += Time.deltaTime;
        }
    }

    //ゲームをやめたときの処理
    private void OnApplicationQuit()
    {
        //セーブする
        m_World.SaveGame(SaveData.floorNumber, SaveData.playTime, SaveData.deathCount);
    }

}
