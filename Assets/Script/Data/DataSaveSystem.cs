using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataSaveSystem  
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter Bformatter = new BinaryFormatter();//cho doi tuong duoi dang nhi phan de luu tru 
        string path = Application.persistentDataPath + "/Player.save";//duong dan de choi tren cac ne tang khac nhau
        FileStream stream = new FileStream(path, FileMode.Create);//neu tep chua ton tai thi tao tep moi hoac gi de(overwrite) vo tep

        PlayerData data = new PlayerData(player);//tao doi tuong playerdata dua tren Player,Playerdata chua thong tin cua Player
        
        Bformatter.Serialize(stream, data);//dung Bfor de chuyen thanh nhi phan doi tuong PlayerData, sau do ghi du lieu thong qua stream
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Player.save";

        if (File.Exists(path))
        {
            BinaryFormatter Bformatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);//tao r thi open no ra

            PlayerData data = Bformatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save path not found :" + path);
            return null;
        }
    }

}
