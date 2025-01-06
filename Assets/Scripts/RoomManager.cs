using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int MaxSize = 8;
    GameObject[,] Rooms = new GameObject[8,8];
    public void SetRoom(GameObject N, int x,int y)
    {
        Rooms[x,y]=N;
    }
    public void GetNei(Room R, int x,int y)
    {
        if(y<MaxSize-1)
            if(ReferenceEquals(Rooms[x,y+1],null)==false)
                R.SetRoom(0,Rooms[x,y+1]);
        if(x<MaxSize-1)
            if(ReferenceEquals(Rooms[x+1,y],null)==false)
                R.SetRoom(1,Rooms[x+1,y]);
        if(y>0)
            if(ReferenceEquals(Rooms[x,y-1],null)==false)
                R.SetRoom(2,Rooms[x,y-1]);
        if(x>0)
            if(ReferenceEquals(Rooms[x-1,y],null)==false)
                R.SetRoom(3,Rooms[x-1,y]);
    }
    public GameObject GetRoom(int x,int y)
    {
        return Rooms[x,y];
    }
    public bool IsValuableSpot(int x,int y)
    {
        return x>=0&&y>=0&&x<MaxSize&&y<MaxSize;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        Rooms = new GameObject[MaxSize,MaxSize];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
