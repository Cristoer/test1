using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;//BinaryFormatter�� �����Ƹ�ʽ���������л��ͷ����л�
public class ArchiveMgr 
{
    //����浵
   public  void SaveGame(Player player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        binaryFormatter.Serialize(file, player);
        file.Close();
    }
    //���ش浵
    public Player LoadGame()
    {
        Player player = new Player();
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                file.Seek(0, SeekOrigin.Begin);
                player = (Player)binaryFormatter.Deserialize(file);
                file.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError("��ȡʧ��----" + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("Player not exsit");
        }
        return player;
    }
}
