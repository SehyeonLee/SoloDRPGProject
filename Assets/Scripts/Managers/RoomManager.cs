using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int MaxSize = 8;//스테이지 사이즈
    public int MaxFloor = 4;//스테이지 층
    GameObject[,,] Rooms = new GameObject[8,8,4];//입주자 명단 가로,세로,층 커질수록 지하하
    public void SetRoom(GameObject N, int x,int y,int f)//입주받기
    {
        Rooms[x,y,f]=N;
    }
    public void GetNei(Room R, int x,int y,int f)//이웃 알려주기
    {
        if(y<MaxSize-1)
            if(ReferenceEquals(Rooms[x,y+1,f],null)==false)
                R.SetRoom(0,Rooms[x,y+1,f]);
        if(x<MaxSize-1)
            if(ReferenceEquals(Rooms[x+1,y,f],null)==false)
                R.SetRoom(1,Rooms[x+1,y,f]);
        if(y>0)
            if(ReferenceEquals(Rooms[x,y-1,f],null)==false)
                R.SetRoom(2,Rooms[x,y-1,f]);
        if(x>0)
            if(ReferenceEquals(Rooms[x-1,y,f],null)==false)
                R.SetRoom(3,Rooms[x-1,y,f]);
    }
    public GameObject GetRoom(int x,int y,int f)//특정 방 알려주기
    {
        return Rooms[x,y,f];
    }
    public bool IsValuableSpot(int x,int y,int f)//스테이지 내부인지 판별
    {
        return x>=0&&y>=0&&x<MaxSize&&y<MaxSize&&f>=0&&f<MaxFloor;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Rooms = new GameObject[MaxSize,MaxSize,MaxFloor];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
