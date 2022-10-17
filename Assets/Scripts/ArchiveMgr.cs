using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;//BinaryFormatter类 二进制格式将对象序列化和反序列化
public class ArchiveMgr 
{
    //保存存档
   public  void SaveGame(Player player)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        binaryFormatter.Serialize(file, player);
        file.Close();
    }
    //加载存档
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
                Debug.LogError("读取失败----" + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("Player not exsit");
        }
        return player;
    }
}
