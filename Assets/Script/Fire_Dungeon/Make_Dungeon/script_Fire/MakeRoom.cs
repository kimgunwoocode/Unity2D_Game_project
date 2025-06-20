using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeRoom : MonoBehaviour
{
    //Trigger 오브젝트에 컴포넌트로써 추가
    /*
    [SerializeField] GameObject Enemy1;
    [SerializeField] GameObject Enemy2;
    [SerializeField] GameObject Enemy3;
    [SerializeField] RoomManager roomManager;
    private int x;
    private int y;
    private int w;
    private int h;
    private int roomCount;
    int number;
    private RoomTrigger roomTrigger;
    */
    private void Awake()
    {
        /*
        roomCount = 0;
        roomTrigger = GetComponent<RoomTrigger>();
        number = roomTrigger.roomTrigerNumber;

        x = RoomManager.roomInfor[number].roomX;
        y = RoomManager.roomInfor[number].roomY;
        w = RoomManager.roomInfor[number].roomWidth;
        h = RoomManager.roomInfor[number].roomHeight;
        */
    }
    public virtual void MakingRoom()
    {

    }
    public virtual void DestroyRoom()
    {

    }
}
