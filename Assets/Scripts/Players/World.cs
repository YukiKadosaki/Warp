//データのロードなどをする

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private PlayerStart[] playerStart;
    private NextMapTrigger[] nextMapTrigger;

    private string SaveFilePath;


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SaveFilePath = Application.persistentDataPath + "/savedPlayer.save";

    }

    private void Update()
    {
        //Escapeキーでゲーム終了
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // 入力された情報をもとにセーブデータを作成
    private PlayerSaveData CreateSavePlayerData(int fn, double pt, int dc)
    {
        PlayerSaveData player = new PlayerSaveData();
        player.floorNumber = fn;
        player.playTime = pt;
        player.deathCount = dc;
        return player;
    }

    //ニューゲームしたときの処理
    public void NewGame()
    {
        // セーブデータ作成
        PlayerSaveData player = CreateSavePlayerData(0, 0, 0);
        // バイナリ形式でシリアル化
        BinaryFormatter bf = new BinaryFormatter();
        // 指定したパスにファイルを作成
        FileStream file = File.Create(SaveFilePath);
        // Closeが確実に呼ばれるように例外処理を用いる
        try
        {
            // 指定したオブジェクトを上で作成したストリームにシリアル化する
            bf.Serialize(file, player);
        }
        finally
        {
            // ファイル操作には明示的な破棄が必要です。Closeを忘れないように。
            if (file != null)
                file.Close();
        }
    }


    public PlayerSaveData LoadGame()
    {
        if (File.Exists(SaveFilePath))
        {
            // バイナリ形式でデシリアライズ
            BinaryFormatter bf = new BinaryFormatter();
            // 指定したパスのファイルストリームを開く
            FileStream file = File.Open(SaveFilePath, FileMode.Open);
            //リターン用のセーブデータ
            PlayerSaveData saveData;
            try
            {
                // 指定したファイルストリームをオブジェクトにデシリアライズ。
                saveData = (PlayerSaveData)bf.Deserialize(file);
                return saveData;
            }
            finally
            {
                // ファイル操作には明示的な破棄が必要です。Closeを忘れないように。
                if (file != null)
                    file.Close();
            }
        }
        else
        {
            Debug.Log("no load file");
            return null;
        }
    }

    //fn=>floorNumber
    //pt=>playTime
    //dc=>deathCount
    public void SaveGame(int fn, double pt, int dc)
    {
        // セーブデータ作成
        PlayerSaveData player = CreateSavePlayerData(fn, pt, dc);
        // バイナリ形式でシリアル化
        BinaryFormatter bf = new BinaryFormatter();
        // 指定したパスにファイルを作成
        FileStream file = File.Create(SaveFilePath);
        // Closeが確実に呼ばれるように例外処理を用いる
        try
        {
            // 指定したオブジェクトを上で作成したストリームにシリアル化する
            bf.Serialize(file, player);
        }
        finally
        {
            // ファイル操作には明示的な破棄が必要です。Closeを忘れないように。
            if (file != null)
                file.Close();
        }
    }
}
