using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//(i - mapSize.x / 2, j - mapSize.y / 2)�� ���� ���������� �����ϴ� Ÿ���� ��ġ�� ����!!-->���� �Ʒ��ڵ��� �ݺ��� �ȿ�����!!
//GetTile,SetTile�� Vector3�� ����








public class MapGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int mapSize;//�� ¦��������!!!!
    public Vector2Int mapSizeCopy;
    [SerializeField] float minimumDevideRate; //������ �������� �ּ� ����
    [SerializeField] float maximumDivideRate; //������ �������� �ִ� ����
    [SerializeField] private GameObject line; //lineRenderer�� ����ؼ� ������ �������� ���������� �����ֱ� ����
    [SerializeField] private GameObject map; //lineRenderer�� ����ؼ� ù ���� ����� �����ֱ� ����
    [SerializeField] private GameObject roomLine; //lineRenderer�� ����ؼ� ���� ����� �����ֱ� ����
    [SerializeField] private int maximumDepth; //Ʈ���� ����, ���� ���� ���� �� �ڼ��� ������ ��
    [SerializeField] Tilemap tileMap;
    [SerializeField] Tile roomTile; //���� �����ϴ� Ÿ��
    [SerializeField] Tile wallTile; //��� �ܺθ� ���������� �� Ÿ��
    [SerializeField] Tile outTile; //�� �ܺ��� Ÿ��

    [SerializeField] Tile testTile;
    [SerializeField] GameObject TestObject;
    [SerializeField] private int roomCount = 0;//roomInfor �ε��� 1�� �ø��µ� ���
    public  int roomNumber ;//���� ���� ���µ� ���
    [SerializeField] RoomManager roomManager;


    private void Awake()//�̾��� ��� �Լ����� awake�϶� �̷���� == �� ���� �ڵ����� awakw�϶� ����
    {
        mapSizeCopy = mapSize;
        roomNumber = 1;
        for (int i = 1; i < maximumDepth + 1; i++)
        {
            roomNumber = roomNumber * 2;
        }
        roomCount = 0;
        roomManager.MakeRoomInfor();

        FillBackground();//�� �ε� �� ���δ� �ٱ�Ÿ�Ϸ� ����
        Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y));
        Divide(root, 0);
        GenerateRoom(root, 0);
        GenerateLoad(root, 0);
        FillWall(); //�ٱ��� ���� ������ ������ ������ ĥ���ִ� �Լ�
    }
    void Start()
    {
        
    }
    
    public void OnlyRoomFillTest()
    {
        //room Type ������
        roomManager.FillRoomTag();
        //startroom�� �� ���� ����� ���� ���� �ؾ���!!

        int k=0;//¦���Ǻ�
        foreach (Room room in RoomManager.roomInfor)//�游 �ٸ�Ÿ�� ä��� �׽�Ʈ
        {
            
                roomManager.MakeWallInRange(wallTile, k, room.roomX, room.roomY, room.roomWidth, room.roomHeight);
            
            //������ �Լ� ���

            if (k % 2 == 0)
            {
                //roomManager.FillTilemap(true, testTile, 25, room.roomX, room.roomY, room.roomWidth, room.roomHeight,room.DontFillTile,false);
            }
            else
            {
                //roomManager.FillTilemap(false, testTile, 0, room.roomX, room.roomY, room.roomWidth, room.roomHeight, room.DontFillTile,false);
            }
            
            k++;


            //roomManager.MakePrefebInRange(TestObject, 25, room.roomX, room.roomY, room.roomWidth, room.roomHeight,1);
            //���ӿ�����Ʈ ������ ������ ����
            //���� �Ⱥ��̱� ���� ��ĭ�� ���°���

        }

        
        roomManager.Test();
    }
    

    void Divide(Node tree, int n)
    {
        if (n == maximumDepth) return; //���� ���ϴ� ���̿� �����ϸ� �� �������� �ʴ´�.
                                       //�� ���� ��쿡��

        int maxLength = Mathf.Max(tree.nodeRect.width, tree.nodeRect.height);
        //���ο� ������ �� ����� ������, ���ΰ� ��ٸ� �� ��, ��� ���ΰ� �� ��ٸ� ��, �Ʒ��� �����ְ� �� ���̴�.
        int split = Mathf.RoundToInt(Random.Range(maxLength * minimumDevideRate, maxLength * maximumDivideRate));
        //���� �� �ִ� �ִ� ���̿� �ּ� �����߿��� �������� �� ���� ����
        if (tree.nodeRect.width >= tree.nodeRect.height) //���ΰ� �� ����� ��쿡�� �� ��� ������ �� ���̸�, �� ��쿡�� ���� ���̴� ������ �ʴ´�.
        {

            tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, split, tree.nodeRect.height));
            //���� ��忡 ���� ������ 
            //��ġ�� ���� �ϴ� �����̹Ƿ� ������ ������, ���� ���̴� ������ ���� �������� �־��ش�.
            tree.rightNode = new Node(new RectInt(tree.nodeRect.x + split, tree.nodeRect.y, tree.nodeRect.width - split, tree.nodeRect.height));
            //���� ��忡 ���� ������ 
            //��ġ�� ���� �ϴܿ��� ���������� ���� ���̸�ŭ �̵��� ��ġ�̸�, ���� ���̴� ���� ���α��̿��� ���� ���� ���ΰ��� �� ������ �κ��� �ȴ�. 
        }
        else
        {

            tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width, split));
            tree.rightNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split, tree.nodeRect.width, tree.nodeRect.height - split));
            //DrawLine(new Vector2(tree.nodeRect.x , tree.nodeRect.y+ split), new Vector2(tree.nodeRect.x + tree.nodeRect.width, tree.nodeRect.y  + split));
        }
        tree.leftNode.parNode = tree; //�ڽĳ����� �θ��带 �������� ���� ����
        tree.rightNode.parNode = tree;
        Divide(tree.leftNode, n + 1); //����, ������ �ڽ� ���鵵 �����ش�.
        Divide(tree.rightNode, n + 1);//����, ������ �ڽ� ���鵵 �����ش�.
    }
    private RectInt GenerateRoom(Node tree, int n)
    {
        RectInt rect;
        if (n == maximumDepth) //�ش� ��尡 ��������� ���� ����� �� ���̴�.
        {
            rect = tree.nodeRect;
            
            int width = Random.Range(rect.width / 2, rect.width - 1);
            //���� ���� �ּ� ũ��� ����� ���α����� ����, �ִ� ũ��� ���α��̺��� 1 �۰� ������ �� �� ���� ���� ������ ���� �����ش�.
            int height = Random.Range(rect.height / 2, rect.height - 1);
            //���̵� ���� ����.
            int x = rect.x + Random.Range(1, rect.width - width);
            //���� x��ǥ�̴�. ���� 0�� �ȴٸ� �پ� �ִ� ��� �������� ������,�ּڰ��� 1�� ���ְ�, �ִ��� ���� ����� ���ο��� ���� ���α��̸� �� �� ���̴�.
            int y = rect.y + Random.Range(1, rect.height - height);
            //y��ǥ�� ���� ����.
            rect = new RectInt(x, y, width, height);
            FillRoom(rect);
            RoomManager.RoomSet(roomCount, x, y, width, height);
            roomCount++;

        }
        else
        {
            tree.leftNode.roomRect = GenerateRoom(tree.leftNode, n + 1);
            tree.rightNode.roomRect = GenerateRoom(tree.rightNode, n + 1);
            rect = tree.leftNode.roomRect;
        }
        return rect;
    }
    private void GenerateLoad(Node tree, int n)
    {
        if (n == maximumDepth) //���� ����� ���� �ڽ��� ����.
            return;
        Vector2Int leftNodeCenter = tree.leftNode.center;
        Vector2Int rightNodeCenter = tree.rightNode.center;

        for (int i = Mathf.Min(leftNodeCenter.x, rightNodeCenter.x); i <= Mathf.Max(leftNodeCenter.x, rightNodeCenter.x); i++)
        {
            tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, leftNodeCenter.y - mapSize.y / 2, 0), roomTile);
        }

        for (int j = Mathf.Min(leftNodeCenter.y, rightNodeCenter.y); j <= Mathf.Max(leftNodeCenter.y, rightNodeCenter.y); j++)
        {
            tileMap.SetTile(new Vector3Int(rightNodeCenter.x - mapSize.x / 2, j - mapSize.y / 2, 0), roomTile);
        }
        //���� �����ÿ��� ������ ������� �κ��� room tile�� ä��� ����
        GenerateLoad(tree.leftNode, n + 1); //�ڽ� ���鵵 Ž��
        GenerateLoad(tree.rightNode, n + 1);
    }

    void FillBackground() //����� ä��� �Լ�, �� load�� ���� ���� ���ش�.
    {
        for (int i = -10; i < mapSize.x + 10; i++) //�ٱ�Ÿ���� �� �����ڸ��� ���� ������� �ʰ�
        //�� ũ�⺸�� �а� ä���ش�.
        {
            for (int j = -10; j < mapSize.y + 10; j++)
            {
                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), outTile);
            }
        }
    }
    void FillWall() //�� Ÿ�ϰ� �ٱ� Ÿ���� ������ �κ�
    {
        for (int i = 0; i < mapSize.x; i++) //Ÿ�� ��ü�� ��ȸ
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0)) == outTile)//(i - mapSize.x / 2, j - mapSize.y / 2)�� ���� ���������� �����ϴ� Ÿ���� ��ġ�� ����!!
                {
                    //�ٱ�Ÿ�� �� ���, 
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {//�� �ٱ�Ÿ���߿� 
                            if (x == 0 && y == 0) continue;//�ٱ� Ÿ�� ���� 8������ Ž���ؼ� room tile�� �ִٸ� wall tile�� �ٲ��ش�.//�߰��κ������ϴ°� ���� ���ǹ�
                            if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2 + x, j - mapSize.y / 2 + y, 0)) == roomTile)//(i - mapSize.x / 2 + x, j - mapSize.y / 2 + y, 0)�� ����Ÿ�� ��ġ���� ���Ʒ��� 8ĭ�� ��ġ�� ��Ÿ��!!
                            {//GetTile,SetTile�� Vector3�� ����
                                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), wallTile);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
    private void FillRoom(RectInt rect)
    { //room�� rect������ �޾Ƽ� tile�� set���ִ� �Լ�

        roomManager.FillTilemap(false, roomTile, 0, rect.x, rect.y, rect.width, rect.height, new Vector2[0],true);
        
    }

}