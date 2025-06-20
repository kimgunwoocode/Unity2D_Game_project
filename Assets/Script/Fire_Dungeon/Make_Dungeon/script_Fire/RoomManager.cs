using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    //!!!!!!타일맵 set할때 꼭 -mapsize/2 해야함!!!!!!
    //ㄴㅇㄱ 방 xy픽셀좌표가 방의 가장 왼쪽 하단 좌표를 뜻하는거였음!!!
    //슬래쉬 3개 쓰면 summary로 주석 달 수 있음!!
    //TurnPixelToWorldPos 로 바꾼 월드좌표는 중심을 의미함!

    public static Room[] roomInfor;//각 방을 담는 배열
    public static int startRoomNumber;
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] GameObject TriggerObject;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tilemap closemap;
    [SerializeField] Tile DebugTile;
    [SerializeField] Tile wallTile;
    [SerializeField] Tile roomTile;
    private int number;//방의 개수
    public static Vector2[] closeTilePos;//입장시 닫아야할 입구의 위치
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
        
        mapGenerator.OnlyRoomFillTest();//방만 타일 바꾸는 함수
        for (int i = 0; i < number; i++)
        {
            MakeTrigger(i);
        }
    }

    public static void RoomSet(int roomnumber, int x, int y, int width, int height)
    {//각 방의 위치,크기 설정하는 함수
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
        int xR = tilemap.WorldToCell(new Vector3(x, y, 0)).x; //임시 xy
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
        int halfmapSizeX = mapGenerator.mapSizeCopy.x/2; //맵사이즈 1/2
        int halfmapSizeY = mapGenerator.mapSizeCopy.y/2; //int가 가능한 이유는 mapsize를 내가 짝수로 해놨기 때문!!!
        
        if (isRandom)
        {
            Vector2[] save = new Vector2[number];
            for ( int  i = 0; i<number; i++)
            {
                
                int RandomX = Random.Range(x, x + w); //임시
                int RandomY = Random.Range(y , y + h);
                bool yesBreak = true;
                while (true)
                {
                    yesBreak = true;
                    if (RandomX % 1 == 0 && RandomY % 1 == 0)//정수인지 판별
                    {
                        
                        foreach(Vector2 v2 in save)//이전에 나왔던 랜덤값이랑 겹치지 않게 하기위함
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
                                //!!!중요 setTile에 넣을 좌표인지 그 전의 좌표인지 구분 잘 하기!!!!
                                if (vector2 == new Vector2(RandomX - halfmapSizeX, RandomY - halfmapSizeY))
                                {

                                    yesBreak = false;
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        Debug.Log("정수아님");
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
        else//!!!!!!타일맵 set할때 꼭 -mapsize/2 해야함!!!!!!
        {
            for (int i = x; i < x + w; i++)
            {
                for (int j = y; j < y + h; j++)
                {
                    if (!Null)//겹치지 않게 하기 위함
                    {
                        bool isSame = false;
                        foreach (Vector2 vector2 in DontFill)
                        {
                            //!!!중요 setTile에 넣을 좌표인지 그 전의 좌표인지 구분 잘 하기!!!!

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
        //한칸씩 띄우려면 w,h를 각각 2개씩 빼야하기 때문
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
        //왜그러는지 모르겠지만 홀수일때는 한칸띄우게 의도해도 2칸씩 띄워짐
        Vector2[] save = new Vector2[number];
        int RandomX;
        int RandomY;
        for (int i = 0; i < number; i++)
        {
            
            RandomX = Random.Range(Mathf.CeilToInt(worldPos.x - (w - minusX) / 2), Mathf.FloorToInt(worldPos.x + (w - minusX) / 2) + 1); 
            RandomY = Random.Range(Mathf.CeilToInt(worldPos.y - (h - minusY) / 2), Mathf.FloorToInt(worldPos.y + (h - minusY) / 2) + 1);
            //실수범위에서 정수난수를 생성하는 방법
            bool yesBreak = true;
            while (true)
            {
                yesBreak = true;
                foreach (Vector2 v2 in save)//이전에 나왔던 랜덤값이랑 겹치지 않게 하기위함
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
        Vector2[] FillArray= new Vector2[0];//채워야 하는 타일들의 좌표들을 모아둔 배열

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
            //벽을 만들지 않는다
        }

        if (roomInfor[roomInforNumber].roomTag==Room.RoomTag.StartRoom)
        {
            FillArray =  ChangeArrayToVector(size5_0, 5);
        }
        //Startroom에는 만들지 XXXXX빈  방!!

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
            //이 좌표를 넣었을 때 원하는 타일맵의 위치가 진짜로 바뀌는 변수
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
        //여기까지는 정상(Debug)
        
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


    public void enterDo(int roomInforNumber)//방에 들어왔을 때
    {
        
        PlayerEnterDungeon(roomInforNumber, roomInfor[roomInforNumber].roomX, roomInfor[roomInforNumber].roomY, roomInfor[roomInforNumber].roomWidth, roomInfor[roomInforNumber].roomHeight, false);
        
    }

    public void outDo(int roomInforNumber)//나갔을 때
    {
        PlayerEnterDungeon(roomInforNumber, roomInfor[roomInforNumber].roomX, roomInfor[roomInforNumber].roomY, roomInfor[roomInforNumber].roomWidth, roomInfor[roomInforNumber].roomHeight, true);
        
    }

    public   void PlayerEnterDungeon(int roomInforNumber, int x, int y, int w, int h,bool isOpen)
    {
        
        int count = 0;
        if (!isOpen)
        {
            
            int halfmapSizeX = mapGenerator.mapSizeCopy.x / 2; //맵사이즈 1/2
            int halfmapSizeY = mapGenerator.mapSizeCopy.y / 2; //int가 가능한 이유는 mapsize를 내가 짝수로 해놨기 때문!!!
            
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
    //방이 들어왔을 때 또는 나갔을 때 입구를 닫고 여는 역할


    public void FillRoomTag()//MakeRoomInfor 만든 이후 만들어져야 함
    {
        int[] DontMakeRoomTag = new int[number];//한 방에 중복된 기능을 구현하지 않기 위해
        int[] LeftRoomTag = new int[number]; // 특별한 방에 기능을 다 할당하고 남은 방들의 배열
        int DontMakeTag = 0;

        //B i >= 0 && i < number/4
        //A i >= number / 4 && i < number / 2
        //C i >= number / 2 && i < number /4*3
        //D i >= number / 4*3 && i < number


        int R;
        bool yesBreak = true;

        //1 start 생성(A중에서)
        R = Random.Range(number/4, number /2);
        roomInfor[R].roomTag = Room.RoomTag.StartRoom;
        Debug.Log(R);
        DontMakeRoomTag[DontMakeTag] = R;
        DontMakeTag++;

        startRoomNumber = R;


        //2 보스룸 생성(D,C중)
        R = Random.Range(number / 2, number-1);//-1이유는 R+1에 Fin 만들어야함
        roomInfor[R].roomTag = Room.RoomTag.BossRoom;
        DontMakeRoomTag[DontMakeTag] = R;
        DontMakeTag++;


        //3 보스룸 넘버 +1 에 Finish 생성
        R++;
        roomInfor[R].roomTag = Room.RoomTag.FinishRoom;
        DontMakeRoomTag[DontMakeTag] = R;
        DontMakeTag++;


        //4 준보스방 3개 배치 ( B,C,D)
        for(int k=0; k<3;k++)
        {
            while (true)
            {
                yesBreak = true;
                int R1 = Random.Range(0, 2);//B 끊어져서..
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


        //5 열쇠방 3개배치 (A,B,C,D)
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


        //6 나머지방 랜덤배치

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

        roomInfor[0].roomTag = Room.RoomTag.EnemyRoom;//인덱스 0 startRoom 되는 버그
        //7 워프방-->다른방이랑 겹침 가능,워프방50%확률 생성,2개생성(겹치지 X)


    }


}
