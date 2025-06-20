using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData_Manager : MonoBehaviour
{
    [Header("Player Static Data")]
    public static string Player_Name = "PlayerName_";//�÷��̾� �̸�

    public static float Player_StandardSpeed = 3f;//�÷��̾��� �⺻ �ӷ�
    public static float moveSpeed = 4f;//�÷��̾� ���� �̵� �ӷ�

    public static int Player_WeaponType = 1;//����ִ� ���� ����,  1 : ��, 2 : Ȱ
    public static float Player_ArrowCharge = 0.8f;//�÷��̾��� Ȱ ������ �ʿ��� �ð�
    public static float PlayerAttack_cool = 0.6f;//�÷��̾� ���� �ӵ� (���� ��Ÿ��)

    public static int PlayerDamage = 10;//�÷��̾� ���ݷ�

    public static int Player_Level = 0;//�÷��̾� ����

    public static int Player_MaxHP = 100;//�÷��̾��� �ִ� ü��
    public static int Player_PresentHP = 100;//�÷��̾��� ���� ü��

    public static bool Player_Die = false;

    //public static bool[] Item_index;//������ ����, �ع� ����

    public static bool[] dungeon_clear = new bool[4] { false, false, false, false };

    public static Dictionary<int, int> Inventory = new Dictionary<int, int>();//���� �κ��丮 �ȿ� �ִ� ������ ����, [������ ������ȣ(Ű��), ������ ����] ������ ����Ʈ
                                                                              //public static int Sword, Bow, Artifact;



    public static string FilePath;
    private void Awake()
    {
        FilePath = Path.Combine(Application.dataPath, "data.dat");//����ũž ���� ���
        //FilePath = Path.Combine(Application.persistentDataPath, "data.dat");//����� ���� ���


        Load_Data();
    }
    public void Save_Data()
    {
        
        List<int> Inventory_keys = new List<int>(Inventory.Keys);
        List<int> Inventory_values = new List<int>(Inventory.Values);
        PlayerDB playerData = new PlayerDB();
        playerData.Player_Name_DATA = Player_Name;
        //playerData.Player_Level_DATA = Player_Level;
        //playerData.Item_index_DATA = Item_index;
        playerData.Inventory_keys_DATA = Inventory_keys;
        playerData.Inventory_values_DATA = Inventory_values;
        //playerData.Inventory_DATA = Inventory;
        //playerData.Sword_DATA = Sword;
        //playerData.Bow_DATA = Bow;
        //playerData.Artifact_DATA = Artifact;
        playerData.treasure_enable_DATA = destroy_interaction.treasure_enable;
        playerData.count_rock_DATA = rockinteraction.count;
        playerData.trigger_human_DATA = humantreasureinteraction.trigger_;

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

            Player_Name = LoadPlayerData.Player_Name_DATA;
            //Player_Level = LoadPlayerData.Player_Level_DATA;
            //Item_index = LoadPlayerData.Item_index_DATA;
            List<int> Inventory_keys = LoadPlayerData.Inventory_keys_DATA;
            List<int> Inventory_values = LoadPlayerData.Inventory_values_DATA;
            for (int i = 0; i < Inventory_keys.Count; i++)
            {
                Inventory.Add(Inventory_keys[i], Inventory_values[i]);
            }
            //Inventory = LoadPlayerData.Inventory_DATA;
            //Sword = LoadPlayerData.Sword_DATA;
            //Bow = LoadPlayerData.Bow_DATA;
            //Artifact = LoadPlayerData.Artifact_DATA;


            destroy_interaction.treasure_enable = LoadPlayerData.treasure_enable_DATA;
            rockinteraction.count = LoadPlayerData.count_rock_DATA;
            humantreasureinteraction.trigger_ = LoadPlayerData.trigger_human_DATA;
        }
        
    }

    private void OnApplicationPause(bool pause)//���� ���� ��
    {
        if (pause)
        {
            //��������!!!
            Save_Data();
        }
    }

}
