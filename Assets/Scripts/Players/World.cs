//�f�[�^�̃��[�h�Ȃǂ�����

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
        //Escape�L�[�ŃQ�[���I��
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // ���͂��ꂽ�������ƂɃZ�[�u�f�[�^���쐬
    private PlayerSaveData CreateSavePlayerData(int fn, double pt, int dc)
    {
        PlayerSaveData player = new PlayerSaveData();
        player.floorNumber = fn;
        player.playTime = pt;
        player.deathCount = dc;
        return player;
    }

    //�j���[�Q�[�������Ƃ��̏���
    public void NewGame()
    {
        // �Z�[�u�f�[�^�쐬
        PlayerSaveData player = CreateSavePlayerData(0, 0, 0);
        // �o�C�i���`���ŃV���A����
        BinaryFormatter bf = new BinaryFormatter();
        // �w�肵���p�X�Ƀt�@�C�����쐬
        FileStream file = File.Create(SaveFilePath);
        // Close���m���ɌĂ΂��悤�ɗ�O������p����
        try
        {
            // �w�肵���I�u�W�F�N�g����ō쐬�����X�g���[���ɃV���A��������
            bf.Serialize(file, player);
        }
        finally
        {
            // �t�@�C������ɂ͖����I�Ȕj�����K�v�ł��BClose��Y��Ȃ��悤�ɁB
            if (file != null)
                file.Close();
        }
    }


    public PlayerSaveData LoadGame()
    {
        if (File.Exists(SaveFilePath))
        {
            // �o�C�i���`���Ńf�V���A���C�Y
            BinaryFormatter bf = new BinaryFormatter();
            // �w�肵���p�X�̃t�@�C���X�g���[�����J��
            FileStream file = File.Open(SaveFilePath, FileMode.Open);
            //���^�[���p�̃Z�[�u�f�[�^
            PlayerSaveData saveData;
            try
            {
                // �w�肵���t�@�C���X�g���[�����I�u�W�F�N�g�Ƀf�V���A���C�Y�B
                saveData = (PlayerSaveData)bf.Deserialize(file);
                return saveData;
            }
            finally
            {
                // �t�@�C������ɂ͖����I�Ȕj�����K�v�ł��BClose��Y��Ȃ��悤�ɁB
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
        // �Z�[�u�f�[�^�쐬
        PlayerSaveData player = CreateSavePlayerData(fn, pt, dc);
        // �o�C�i���`���ŃV���A����
        BinaryFormatter bf = new BinaryFormatter();
        // �w�肵���p�X�Ƀt�@�C�����쐬
        FileStream file = File.Create(SaveFilePath);
        // Close���m���ɌĂ΂��悤�ɗ�O������p����
        try
        {
            // �w�肵���I�u�W�F�N�g����ō쐬�����X�g���[���ɃV���A��������
            bf.Serialize(file, player);
        }
        finally
        {
            // �t�@�C������ɂ͖����I�Ȕj�����K�v�ł��BClose��Y��Ȃ��悤�ɁB
            if (file != null)
                file.Close();
        }
    }
}
