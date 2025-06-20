using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Room StartRoom;
    [SerializeField] RoomManager roomManager;
    void Start()
    {
        player = GameObject.Find("Player");
        StartRoom = RoomManager.roomInfor[RoomManager.startRoomNumber];
        Vector3 WorldStart = roomManager.TurnPixelToWorldPos(StartRoom.roomX, StartRoom.roomY, StartRoom.roomWidth, StartRoom.roomHeight);
        player.transform.position = WorldStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
