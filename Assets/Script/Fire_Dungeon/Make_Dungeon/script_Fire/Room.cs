using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    public  int roomX=0;
    public  int roomY=0;
    public  int roomWidth=0;
    public  int roomHeight=0;
    public Vector2[] DontFillTile;
    public enum RoomType {A,B,C,D};
    public RoomType roomType;
    public enum RoomTag {
        StartRoom,FinishRoom,EnemyRoom,
        MidBossRoom,BossRoom,HpRoom,
        UpgradeRoom,KeyRoom};
    public RoomTag roomTag;
    //KeyRoom 은 EnemyToom 상속함!!
}
