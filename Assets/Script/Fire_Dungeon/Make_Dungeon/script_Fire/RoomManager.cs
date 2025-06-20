using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    //!!!!!!Ÿ�ϸ� set�Ҷ� �� -mapsize/2 �ؾ���!!!!!!
    //������ �� xy�ȼ���ǥ�� ���� ���� ���� �ϴ� ��ǥ�� ���ϴ°ſ���!!!
    //������ 3�� ���� summary�� �ּ� �� �� ����!!
    //TurnPixelToWorldPos �� �ٲ� ������ǥ�� �߽��� �ǹ���!

    public static Room[] roomInfor;//�� ���� ��� �迭
    public static int startRoomNumber;
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] GameObject TriggerObject;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tilemap closemap;
    [SerializeField] Tile DebugTile;
    [SerializeField] Tile wallTile;
    [SerializeField] Tile roomTile;
    private int number;//���� ����
    public static Vector2[] closeTilePos;//����� �ݾƾ��� �Ա��� ��ġ
    private int closeTileCount = 0;
    bool[,] size5_0 = new bool[5, 5] {
        { false, false, false, false, false },
        { false, false, false, false, false },
        { false, false, false, false, false },
        { false, false, false, false, false },
        { false, false, false, false, false } };
    bool[,] size5_1 = new bool[5, 5] {
        { false, false, true , false, false },
        { false, false, true , false, false },
        { true , true , true , true , true  },
        { false, false, true , false, false },
        { false, false, true , false, false } };
    bool[,] size5_2 = new bool[5, 5] {
        { false, true , false, false, true  },
        { false, true , false, false, true  },
        { false, true , false, false, true  },
        { false, true , false, false, true  },
        { false, true , false, false, true  } };
    bool[,] size5_3 = new bool[5, 5] {
        { false, false, false, false, false },
        { true , true , true , true , true  },
        { false, false, false, false, false },
        { false, false, false, false, false },
        { true , true , true , true , true  }, };
    bool[,] size5_4 = new bool[5, 5] {
        { false, false, true , false, false },
        { false, false, true , false, false },
        { false, false, true , false, false },
        { false, false, true , false, false },
        { false, false, true , false, false } };
    bool[,] size5_5 = new bool[5, 5] {
        { false, false, false, false, false },
        { false, false, false, false, false },
        { true , true , true , true , true  },
        { false, false, false, false, false },
        { false, false, false, false, false } };
    bool[,] size5_6 = new bool[5, 5] {
        { false, false, false, false, true  },
        { false, false, false, true , false },
        { false, false, true , false, false },
        { false, true , false, false, false },
        { true , false, false, false, false } };
    bool[,] size5_7 = new bool[5, 5] {
        { false, true , true , true , true  },
        { false, false, false, false, true  },
        { false, false, false, false, true  },
        { false, false, false, false, true  },
        { false, true , true , true , true  } };

    bool[,] size8_0 = new bool[8, 8] {
        { false, false, false, false, false,false, false, false },
        { false, false, false, false, false,false, false, false },
        { false, false, false, false, false,false, false, false },
        { false, false, false, false, false,false, false, false },
        { false, false, false, false, false,false, false, false },
        { false, false, false, false, false,false, false, false },
        { false, false, false, false, false,false, false, false },
        { false, false, false, false, false,false, false, false } };
    bool[,] size8_1 = new bool[8, 8] {
        { false, false, true , true , true ,true , false, false },
        { false, false, true , true , true ,true , false, false },
        { false, false, true , true , true ,true , false, false },
        { false, false, true , true , true ,true , false, false },
        { false, false, true , true , true ,true , false, false },
        { false, false, true , true , true ,true , false, false },
        { false, false, true , true , true ,true , false, false },
        { false, false, true , true , true ,true , false, false } };
    bool[,] size8_2 = new bool[8, 8] {
        { false, true , true , true , true ,false, false, true  },
        { true , false, false, false, true ,false, false, true  },
        { true , false, false, false, true ,false, false, true  },
        { true , false, false, true , true ,false, false, true  },
        { true , false, false, true , true ,false, false, true  },
        { true , false, false, false, false,false, false, true  },
        { true , false, false, false, false,false, false, true  },
        { true , true , true , true , true ,true , true , true  } };
    bool[,] size8_3 = new bool[8, 8] {
        { false, true , true , true , true ,true , true , true  },
        { false, false, false, true , true ,false, false, false },
        { false, false, false, true , true ,false, false, false },
        { false, false, false, true , true ,false, false, false },
        { false, false, false, true , true ,false, false, false },
        { false, false, false, true , true ,false, false, false },
        { false, false, false, true , true ,false, false, false },
        { false, true , true , true , true ,true , true , true  } };
    bool[,] size8_4 = new bool[8, 8] {
        { false, false, false, true , true ,false, false, false },
        { false, false, false, true , true ,false, false, false },
        { false, false, false, true , true ,false, false, false },
        { true , true , true , true , true ,true , true , true  },
        { true , true , true , true , true ,true , true , true  },
        { false, false, false, true , true ,false, false, false },
        { false, false, false, true , true ,false, false, false },
        { false, false, false, true , true ,false, false, false } };
    bool[,] size8_5 = new bool[8, 8] {
        { false, false, true , false, false,true , false, false },
        { false, false, true , false, false,true , false, false },
        { true , true , true , false, false,true , true , true  },
        { false, false, false, false, false,false, false, false },
        { false, false, false, false, false,false, false, false },
        { true , true , true , false, false,true , true , true  },
        { false, false, false, false, false,true , false, false },
        { false, false, true , false, false,true , false, false } };

    bool[,] size15_0 = new bool[15, 15] {
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false }};
    bool[,] size15_1 = new bool[15, 15] {
        { false, false, false, true , false,false, false, false,false, false,false, true , false, false, false },
        { false, false, false, true , false,false, false, false,false, false,false, true , false, false, false },
        { false, false, false, true , false,false, false, false,false, false,false, true , false, false, false },
        { true , true , true , true , false,false, false, false,false, false,false, true , true , true , true  },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { true , true , true , true , false,false, false, false,false, false,false, true , true , true , true  },
        { false, false, false, true , false,false, false, false,false, false,false, true , false, false, false },
        { false, false, false, true , false,false, false, false,false, false,false, true , false, false, false },
        { false, false, false, true , false,false, false, false,false, false,false, true , false, false, false }};
    bool[,] size15_2 = new bool[15, 15] {
        { false, false, false, false, false,false, false, true ,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, true ,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, true ,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, true ,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, true ,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { true , true , true , true , true ,false, false, false,false, false,true , true , true , true , true  },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, false,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, true ,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, true ,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, true ,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, true ,false, false,false, false, false, false, false },
        { false, false, false, false, false,false, false, true ,false, false,false, false, false, false, false }};

    private void Awake()
    {
        closeTilePos = new Vector2[160];
    }
    void Start()
    {
        
        mapGenerator.OnlyRoomFillTest();//�游 Ÿ�� �ٲٴ� �Լ�
        for (int i = 0; i < number; i++)
        {
            MakeTrigger(i);
        }
    }

    public static void RoomSet(int roomnumber, int x, int y, int width, int height)
    {//�� ���� ��ġ,ũ�� �����ϴ� �Լ�
        if (roomInfor == null)
        {
            Debug.Log("errer");
            return;
        }


        roomInfor[roomnumber].roomX = x;
        roomInfor[roomnumber].roomY = y;
        roomInfor[roomnumber].roomWidth = width;
        roomInfor[roomnumber].roomHeight = height;

    }

    public void MakeRoomInfor()
    {
        number = mapGenerator.roomNumber;
        //Debug.Log(number);
        roomInfor = new Room[number];
        for (int i = 0; i<number; i++)
        {
            roomInfor[i] = new Room();
            if (i >= 0 && i < number/4)
            {
                roomInfor[i].roomType = Room.RoomType.B;
            }
            else if (i >= number / 4 && i < number / 2)
            {
                roomInfor[i].roomType = Room.RoomType.A;
            }
            else if (i >= number / 2 && i < number /4*3)
            {
                roomInfor[i].roomType = Room.RoomType.C;
            }
            else  if (i >= number / 4*3 && i < number )
            {
                roomInfor[i].roomType = Room.RoomType.D;
            }
        }


        
    }

    public void Test()//Test
    {
        
    }

    public void MakeTrigger(int numberM)
    {
        Vector2 MakePos = TurnPixelToWorldPos(roomInfor[numberM].roomX, roomInfor[numberM].roomY, roomInfor[numberM].roomWidth, roomInfor[numberM].roomHeight);
        Vector2 MakeScale = new Vector2(roomInfor[numberM].roomWidth, roomInfor[numberM].roomHeight);

        

        GameObject TriggerObject_obj = Instantiate(TriggerObject);
        TriggerObject_obj.transform.position = new Vector2(0, 0);
        TriggerObject_obj.transform.localScale = new Vector2(0, 0);
        TriggerObject_obj.transform.position = MakePos;
        TriggerObject_obj.transform.localScale = MakeScale;
        
        RoomTrigger roomTrigger;
        roomTrigger = TriggerObject_obj.GetComponent<RoomTrigger>();
        roomTrigger.roomTrigerNumber = numberM;


        //test
        SpriteRenderer thisColor;
         thisColor = TriggerObject_obj.GetComponent<SpriteRenderer>();
        switch (roomInfor[numberM].roomTag)
        {

            case Room.RoomTag.StartRoom:
                {
                    TriggerObject_obj.AddComponent<BossRoomRoom>();
                    thisColor.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0.3f);
                    break;
                }
            case Room.RoomTag.BossRoom:
                thisColor.color = new Color(147/255f,0 / 255f, 6 / 255f,0.3f);
                
                break;
            case Room.RoomTag.EnemyRoom:
                TriggerObject_obj.AddComponent<EnemyRoom>();
                thisColor.color = new Color(255 / 255f, 109 / 255f, 38 / 255f, 0.3f);
                break;
            case Room.RoomTag.FinishRoom:
                thisColor.color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 0.3f);
                break;
            case Room.RoomTag.HpRoom:
                thisColor.color = new Color(0 / 255f, 255 / 255f, 0 / 255f, 0.3f);
                break;
            case Room.RoomTag.KeyRoom:
                thisColor.color = new Color(255 / 255f, 255 / 255f, 0 / 255f, 0.3f);
                break;
            case Room.RoomTag.MidBossRoom:
                thisColor.color = new Color(195 / 255f, 10 / 255f, 18 / 255f, 0.3f);
                break;
            case Room.RoomTag.UpgradeRoom:
                thisColor.color = new Color(0 / 255f, 255 / 255f, 255 / 255f, 0.3f);
                break;
            default:
                Debug.Log("Dont Have Tag");
                break;
        }
        //test

        
        
        
    }

    private void DivideRoomType()
    {
        int index = Random.Range(0, mapGenerator.roomNumber);

    }


    

    public  Vector2 TurnPixelToWorldPos(int x, int y,int w , int h)
    {
        float worldPosX;
        float worldPosY;
        int xR = tilemap.WorldToCell(new Vector3(x, y, 0)).x; //�ӽ� xy
        int yR = tilemap.WorldToCell(new Vector3(x, y, 0)).y;

        if (w % 2 == 0)
        {
            worldPosX = (xR + (w / 2)) - mapGenerator.mapSizeCopy.x / 2;
        }
        else
        {
            worldPosX = (xR + (w / 2)) + 0.5f - mapGenerator.mapSizeCopy.x / 2;
        }

        if (h % 2 == 0)
        {
            worldPosY = (yR + (h / 2)) - mapGenerator.mapSizeCopy.y / 2;
        }
        else
        {
            worldPosY = (yR + (h / 2)) + 0.5f - mapGenerator.mapSizeCopy.y / 2;
        }

        return new Vector2(worldPosX,worldPosY);
    }

    public void FillTilemap(bool isRandom, Tile tile,int number,int x, int y, int w, int h,Vector2[] DontFill,bool Null)
    {
        int halfmapSizeX = mapGenerator.mapSizeCopy.x/2; //�ʻ����� 1/2
        int halfmapSizeY = mapGenerator.mapSizeCopy.y/2; //int�� ������ ������ mapsize�� ���� ¦���� �س��� ����!!!
        
        if (isRandom)
        {
            Vector2[] save = new Vector2[number];
            for ( int  i = 0; i<number; i++)
            {
                
                int RandomX = Random.Range(x, x + w); //�ӽ�
                int RandomY = Random.Range(y , y + h);
                bool yesBreak = true;
                while (true)
                {
                    yesBreak = true;
                    if (RandomX % 1 == 0 && RandomY % 1 == 0)//�������� �Ǻ�
                    {
                        
                        foreach(Vector2 v2 in save)//������ ���Դ� �������̶� ��ġ�� �ʰ� �ϱ�����
                        {
                            
                            if (v2 == new Vector2(RandomX, RandomY)&& i!=0)
                            {
                                
                                yesBreak = false;
                            }
                        }
                        if(DontFill != null)
                        {
                            foreach (Vector2 vector2 in DontFill)
                            {
                                //!!!�߿� setTile�� ���� ��ǥ���� �� ���� ��ǥ���� ���� �� �ϱ�!!!!
                                if (vector2 == new Vector2(RandomX - halfmapSizeX, RandomY - halfmapSizeY))
                                {

                                    yesBreak = false;
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        Debug.Log("�����ƴ�");
                        RandomX = Random.Range(x, x + w); 
                        RandomY = Random.Range(y, y + h);
                    }

                    if (yesBreak)
                    {
                        tilemap.SetTile(new Vector3Int(RandomX - halfmapSizeX, RandomY - halfmapSizeY, 0), tile);
                        save[i] = new Vector2(RandomX, RandomY);
                        break;
                    }
                    else
                    {
                        RandomX = Random.Range(x, x + w);
                        RandomY = Random.Range(y, y + h);
                    }
                }
            }
            
           
        }
        else//!!!!!!Ÿ�ϸ� set�Ҷ� �� -mapsize/2 �ؾ���!!!!!!
        {
            for (int i = x; i < x + w; i++)
            {
                for (int j = y; j < y + h; j++)
                {
                    if (!Null)//��ġ�� �ʰ� �ϱ� ����
                    {
                        bool isSame = false;
                        foreach (Vector2 vector2 in DontFill)
                        {
                            //!!!�߿� setTile�� ���� ��ǥ���� �� ���� ��ǥ���� ���� �� �ϱ�!!!!

                            if (vector2 == new Vector2(i - halfmapSizeX, j - halfmapSizeY))
                            {
                                isSame = true;
                                break;
                            }
                        }
                        if (!isSame)
                        {
                            tilemap.SetTile(new Vector3Int(i - halfmapSizeX, j - halfmapSizeY, 0), tile);
                        }
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(i - halfmapSizeX, j - halfmapSizeY, 0), tile);
                    }
                    
                }
            }
        }
    }

    public GameObject[] MakePrefebInRange(GameObject prefeb,int number, int x, int y, int w, int h,int minusR)
    {
        
        GameObject[] returnPrefeb;
        returnPrefeb = new GameObject[number];
        
        Vector2 worldPos = TurnPixelToWorldPos(x, y, w, h);
        int minusX;
        int minusY;
        //��ĭ�� ������ w,h�� ���� 2���� �����ϱ� ����
        if (w % 2 == 0)
        {
            minusX = minusR * 2;
        }
        else
        {
            minusX = minusR ;
        }

        if (h % 2 == 0)
        {
            minusY = minusR * 2;
        }
        else
        {
            minusY = minusR;
        }
        //�ֱ׷����� �𸣰����� Ȧ���϶��� ��ĭ���� �ǵ��ص� 2ĭ�� �����
        Vector2[] save = new Vector2[number];
        int RandomX;
        int RandomY;
        for (int i = 0; i < number; i++)
        {
            
            RandomX = Random.Range(Mathf.CeilToInt(worldPos.x - (w - minusX) / 2), Mathf.FloorToInt(worldPos.x + (w - minusX) / 2) + 1); 
            RandomY = Random.Range(Mathf.CeilToInt(worldPos.y - (h - minusY) / 2), Mathf.FloorToInt(worldPos.y + (h - minusY) / 2) + 1);
            //�Ǽ��������� ���������� �����ϴ� ���
            bool yesBreak = true;
            while (true)
            {
                yesBreak = true;
                foreach (Vector2 v2 in save)//������ ���Դ� �������̶� ��ġ�� �ʰ� �ϱ�����
                {

                    if (v2 == new Vector2(RandomX, RandomY) && i != 0)
                    {

                        yesBreak = false;
                    }
                }
                if (yesBreak)
                {
                    GameObject makeObj = Instantiate(prefeb);
                    makeObj.transform.position = new Vector3(RandomX, RandomY);
                    save[i] = new Vector2(RandomX, RandomY);
                    returnPrefeb[i] = makeObj;

                    break;
                }
                else
                {
                    RandomX = Random.Range(Mathf.CeilToInt(worldPos.x - (w - minusX) / 2), Mathf.FloorToInt(worldPos.x + (w - minusX) / 2) + 1);
                    RandomY = Random.Range(Mathf.CeilToInt(worldPos.y - (h - minusY) / 2), Mathf.FloorToInt(worldPos.y + (h - minusY) / 2) + 1);
                }
            }
        }
        return returnPrefeb;
        
    }

    public void MakeWallInRange(Tile tile, int roomInforNumber,int x, int y, int w, int h)
    {
        int spaceX;
        int spaceY;
        int size=0;
        Vector2[] FillArray= new Vector2[0];//ä���� �ϴ� Ÿ�ϵ��� ��ǥ���� ��Ƶ� �迭

        if (w >= 19 && h >= 19)
        {//size15
            size = 15;
            Vector2[] FillWall = new Vector2[size * size];
            switch (Random.Range(0, 2))
            {
                case 0:
                    FillWall = ChangeArrayToVector(size15_0, 15);
                    break;
                case 1:
                    FillWall = ChangeArrayToVector(size15_1, 15);
                    break;
                case 2:
                    FillWall = ChangeArrayToVector(size15_2, 15);
                    break;
                default:
                    break;
            }
            FillArray = FillWall;
        }
        else if((w >= 12 && h >= 12))
        {//size8
            size = 8;
            Vector2[] FillWall = new Vector2[size * size];
            switch (Random.Range(0, 5))
            {
                case 0:
                    FillWall = ChangeArrayToVector(size8_0, 8);
                    break;
                case 1:
                    FillWall = ChangeArrayToVector(size8_1, 8);
                    break;
                case 2:
                    FillWall = ChangeArrayToVector(size8_2, 8);
                    break;
                case 3:
                    FillWall = ChangeArrayToVector(size8_3, 8);
                    break;
                case 4:
                    FillWall = ChangeArrayToVector(size8_4, 8);
                    break;
                case 5:
                    FillWall = ChangeArrayToVector(size8_5, 8);
                    break;
                default:
                    break;

            }
            FillArray = FillWall;
        }
        else if ((w >= 9 && h >= 9))
        {//size5
            size = 5;
            Vector2[] FillWall = new Vector2[size * size];
            switch (Random.Range(0, 7))
            {
                case 0:
                    FillWall = ChangeArrayToVector(size5_0, 5);
                    break;
                case 1:
                    FillWall = ChangeArrayToVector(size5_1, 5);
                    break;
                case 2:
                    FillWall = ChangeArrayToVector(size5_2, 5);
                    break;
                case 3:
                    FillWall = ChangeArrayToVector(size5_3, 5);
                    break;
                case 4:
                    FillWall = ChangeArrayToVector(size5_4, 5);
                    break;
                case 5:
                    FillWall = ChangeArrayToVector(size5_5, 5);
                    break;
                case 6:
                    FillWall = ChangeArrayToVector(size5_6, 5);
                    break;
                case 7:
                    FillWall = ChangeArrayToVector(size5_7, 5);
                    break;
                default:
                    break;

            }
            FillArray = FillWall;
        }
        else
        {
            //���� ������ �ʴ´�
        }

        if (roomInfor[roomInforNumber].roomTag==Room.RoomTag.StartRoom)
        {
            FillArray =  ChangeArrayToVector(size5_0, 5);
        }
        //Startroom���� ������ XXXXX��  ��!!

        if ((w - size) % 2 == 0)
        {
            spaceX = (w - size) / 2;
        }
        else
        {
            spaceX = (w - size - 1) / 2;
        }

        if ((h - size) % 2 == 0)
        {
            spaceY = (h - size) / 2;
        }
        else
        {
            spaceY = (h - size - 1) / 2;
        }

        int i = 0;
        foreach (Vector2 vector in FillArray)
        {
            //�� ��ǥ�� �־��� �� ���ϴ� Ÿ�ϸ��� ��ġ�� ��¥�� �ٲ�� ����
            int RealFillPosX = (int)vector.x + spaceX + x - mapGenerator.mapSizeCopy.x / 2;
            int RealFillPosY = (int)vector.y + spaceY + y - mapGenerator.mapSizeCopy.y / 2;
            if (new Vector2(0, 0) != vector)
            {
                tilemap.SetTile(new Vector3Int(RealFillPosX, RealFillPosY, 0), tile);
                FillArray[i].x = RealFillPosX;
                FillArray[i].y = RealFillPosY;
            }
            i++;
        }
        
        roomInfor[roomInforNumber].DontFillTile = FillArray;
        //��������� ����(Debug)
        
    }

    public Vector2[] ChangeArrayToVector(bool[,] Array ,int size)
    {
        Vector2[] save = new Vector2[size*size];
        //Array = new bool[size, size];
        int k = 0;
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (Array[i, j])
                {
                    save[k] = new Vector2(i, j);
                    k++;
                }

            }
        }


        return save;
    }


    public void enterDo(int roomInforNumber)//�濡 ������ ��
    {
        
        PlayerEnterDungeon(roomInforNumber, roomInfor[roomInforNumber].roomX, roomInfor[roomInforNumber].roomY, roomInfor[roomInforNumber].roomWidth, roomInfor[roomInforNumber].roomHeight, false);
        
    }

    public void outDo(int roomInforNumber)//������ ��
    {
        PlayerEnterDungeon(roomInforNumber, roomInfor[roomInforNumber].roomX, roomInfor[roomInforNumber].roomY, roomInfor[roomInforNumber].roomWidth, roomInfor[roomInforNumber].roomHeight, true);
        
    }

    public   void PlayerEnterDungeon(int roomInforNumber, int x, int y, int w, int h,bool isOpen)
    {
        
        int count = 0;
        if (!isOpen)
        {
            
            int halfmapSizeX = mapGenerator.mapSizeCopy.x / 2; //�ʻ����� 1/2
            int halfmapSizeY = mapGenerator.mapSizeCopy.y / 2; //int�� ������ ������ mapsize�� ���� ¦���� �س��� ����!!!
            
            Vector2[] outside = new Vector2[(w + 2) * (h + 2)];
            int y1 = y-1;
            int x1 = x - 1;
            int x2 = x +w;
            int y2 =  y +h;
            for (int i = x - 1; i < x - 1 + w + 2; i++)
            {
                for (int j = y - 1; j < y - 1 + h + 2; j++)
                {
                    if(i==x1 || i==x2 || j==y1 || j == y2)
                    {
                        outside[count] = new Vector2(i - halfmapSizeX, j - halfmapSizeY);
                        //test
                        //tilemap.SetTile(new Vector3Int((int)outside[count].x, (int)outside[count].y), DebugTile);
                        count++;
                    }
                    
                }
            }

            
            closeTilePos = outside;
        }

        


        if (!isOpen)
        {
            
            foreach (Vector2 v in closeTilePos)
            {
                closemap.SetTile(new Vector3Int((int)v.x, (int)v.y), wallTile);
            }
        }
        else
        {
            
            foreach (Vector2 v in closeTilePos)
            {
                
                closemap.SetTile(new Vector3Int((int)v.x, (int)v.y), null);
            }
        }

    }
    //���� ������ �� �Ǵ� ������ �� �Ա��� �ݰ� ���� ����


    public void FillRoomTag()//MakeRoomInfor ���� ���� ��������� ��
    {
        int[] DontMakeRoomTag = new int[number];//�� �濡 �ߺ��� ����� �������� �ʱ� ����
        int[] LeftRoomTag = new int[number]; // Ư���� �濡 ����� �� �Ҵ��ϰ� ���� ����� �迭
        int DontMakeTag = 0;

        //B i >= 0 && i < number/4
        //A i >= number / 4 && i < number / 2
        //C i >= number / 2 && i < number /4*3
        //D i >= number / 4*3 && i < number


        int R;
        bool yesBreak = true;

        //1 start ����(A�߿���)
        R = Random.Range(number/4, number /2);
        roomInfor[R].roomTag = Room.RoomTag.StartRoom;
        Debug.Log(R);
        DontMakeRoomTag[DontMakeTag] = R;
        DontMakeTag++;

        startRoomNumber = R;


        //2 ������ ����(D,C��)
        R = Random.Range(number / 2, number-1);//-1������ R+1�� Fin ��������
        roomInfor[R].roomTag = Room.RoomTag.BossRoom;
        DontMakeRoomTag[DontMakeTag] = R;
        DontMakeTag++;


        //3 ������ �ѹ� +1 �� Finish ����
        R++;
        roomInfor[R].roomTag = Room.RoomTag.FinishRoom;
        DontMakeRoomTag[DontMakeTag] = R;
        DontMakeTag++;


        //4 �غ����� 3�� ��ġ ( B,C,D)
        for(int k=0; k<3;k++)
        {
            while (true)
            {
                yesBreak = true;
                int R1 = Random.Range(0, 2);//B ��������..
                if (R1 == 0)
                {
                    R = Random.Range(number / 2, number);
                }
                else
                {
                    R = Random.Range(0, number / 4);
                }

                foreach (int i in DontMakeRoomTag)
                {
                    if (R == i)
                    {
                        yesBreak = false;
                        break;
                    }
                }

                if (yesBreak)
                {
                    break;
                }

            }
            roomInfor[R].roomTag = Room.RoomTag.MidBossRoom;
            DontMakeRoomTag[DontMakeTag] = R;
            DontMakeTag++;
        }


        //5 ����� 3����ġ (A,B,C,D)
        for (int k = 0; k < 3; k++)
        {
            while (true)
            {
                yesBreak = true;
                R = Random.Range(0, number);
                

                foreach (int i in DontMakeRoomTag)
                {
                    if (R == i)
                    {
                        yesBreak = false;
                        break;
                    }
                }

                if (yesBreak)
                {
                    break;
                }

            }
            roomInfor[R].roomTag = Room.RoomTag.KeyRoom;
            DontMakeRoomTag[DontMakeTag] = R;
            DontMakeTag++;
        }


        //6 �������� ������ġ

        //hp
        for (int k = 0; k < 2; k++)//hp
        {
            while (true)
            {
                yesBreak = true;
                R = Random.Range(0, number/2);


                foreach (int i in DontMakeRoomTag)
                {
                    if (R == i)
                    {
                        yesBreak = false;
                        break;
                    }
                }

                if (yesBreak)
                {
                    break;
                }

            }
            roomInfor[R].roomTag = Room.RoomTag.HpRoom;
            DontMakeRoomTag[DontMakeTag] = R;
            DontMakeTag++;
        }


        //upgrade
        while (true)
        {
            yesBreak = true;
            R = Random.Range(0, number / 2);


            foreach (int i in DontMakeRoomTag)
            {
                if (R == i)
                {
                    yesBreak = false;
                    break;
                }
            }

            if (yesBreak)
            {
                break;
            }

        }
        roomInfor[R].roomTag = Room.RoomTag.UpgradeRoom;
        DontMakeRoomTag[DontMakeTag] = R;
        DontMakeTag++;

        while (true)
        {
            yesBreak = true;
            R = Random.Range(0/2, number);


            foreach (int i in DontMakeRoomTag)
            {
                if (R == i)
                {
                    yesBreak = false;
                    break;
                }
            }

            if (yesBreak)
            {
                break;
            }

        }
        roomInfor[R].roomTag = Room.RoomTag.UpgradeRoom;
        DontMakeRoomTag[DontMakeTag] = R;
        DontMakeTag++;


        //Left
        for (int i = 0; i < number; i++)
        {
            LeftRoomTag[i] = 1;
        }
        foreach (int i in DontMakeRoomTag)
        {
            LeftRoomTag[i] = 0;
        }
        for (int i = 0; i < number; i++)
        {
            if (LeftRoomTag[i] == 1)
            {
                roomInfor[i].roomTag = Room.RoomTag.EnemyRoom;
            }
        }

        roomInfor[0].roomTag = Room.RoomTag.EnemyRoom;//�ε��� 0 startRoom �Ǵ� ����
        //7 ������-->�ٸ����̶� ��ħ ����,������50%Ȯ�� ����,2������(��ġ�� X)


    }


}
