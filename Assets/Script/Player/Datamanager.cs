using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Datamanager : MonoBehaviour
{
    public PlayerDB playerDB;

    public static string FilePath;
    private void Awake()
    {
        FilePath = Path.Combine(Application.dataPath, "data.dat");//데스크탑 저장 경로
        //FilePath = Path.Combine(Application.persistentDataPath, "data.dat");//모바일 저장 경로


        Load_Data();
    }
    public void Save_Data()
    {
        List<int> Inventory_keys = new List<int>(PlayerData_Manager.Inventory.Keys);
        List<int> Inventory_values = new List<int>(PlayerData_Manager.Inventory.Values);
        PlayerDB playerData = new PlayerDB();
        playerData.Player_Name_DATA = PlayerData_Manager.Player_Name;
        //playerData.Player_Level_DATA = Player_Level;
        //playerData.Item_index_DATA = Item_index;
        playerData.Inventory_keys_DATA = Inventory_keys;
        playerData.Inventory_values_DATA = Inventory_values;
        //playerData.Inventory_DATA = Inventory;
        //playerData.Sword_DATA = Sword;
        //playerData.Bow_DATA = Bow;
        //playerData.Artifact_DATA = Artifact;

        Stream stream = new FileStream(FilePath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(stream, playerData);
        stream.Close();

    }

    public void Load_Data()
    {

        if (File.Exists(FilePath))
        {
            Stream stream = new FileStream(FilePath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            PlayerDB LoadPlayerData = (PlayerDB)binaryFormatter.Deserialize(stream);
            stream.Close();

            PlayerData_Manager.Player_Name = LoadPlayerData.Player_Name_DATA;
            //Player_Level = LoadPlayerData.Player_Level_DATA;
            //Item_index = LoadPlayerData.Item_index_DATA;
            List<int> Inventory_keys = LoadPlayerData.Inventory_keys_DATA;
            List<int> Inventory_values = LoadPlayerData.Inventory_values_DATA;
            for (int i = 0; i < Inventory_keys.Count; i++)
            {
                PlayerData_Manager.Inventory.Add(Inventory_keys[i], Inventory_values[i]);
            }
            //Inventory = LoadPlayerData.Inventory_DATA;
            //Sword = LoadPlayerData.Sword_DATA;
            //Bow = LoadPlayerData.Bow_DATA;
            //Artifact = LoadPlayerData.Artifact_DATA;
        }

    }

    private void OnApplicationPause(bool pause)//강제 종료 시
    {
        if (pause)
        {
            //게임저장!!!
            Save_Data();
        }
    }
}
