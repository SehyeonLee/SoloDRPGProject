using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Start is called before the first frame update
    protected Room StayRoom;//내가 지금 있는 방
    protected RoomManager RoomManager;//방 매니저
    public void SetStayRoom(Room Where)//있는 곳 받기
    {
        StayRoom = Where;
    }
    protected int x,y,f;//내 좌표
    public Vector2 GetXY()//내 좌표 포장해서 배출
    {
        return new Vector3(x,y,f);
    }
    protected int Dir = 0;//보는 방향, 0앞 1오 2뒤 3왼
    public void TurnLeft()//좌로 돌아
    {
        Dir-=1;
        if(Dir<0)
            Dir=3;
        Turn();
    }
    public void TurnRight()//우로 돌아
    {
        Dir+=1;
        if(Dir>3)
            Dir=0;
        Turn();
    }
    protected Vector3[] GoDirs = {new Vector3(0,0,1),new Vector3(1,0,0),new Vector3(0,0,-1),new Vector3(-1,0,0)};//이동에 쓰이는 좌표, Dir에 맞춰 세팅팅
    protected float GetLookDir()//회전값 얻기
    {
        return 90*Dir;
    }
    protected void CalcLookDir()//회전값 얻기
    {
        Dir = (int)(transform.rotation.eulerAngles.y/90);
    }
    protected void Turn()//Dir값으로 돌기ㅇㅇ
    {
        transform.rotation = Quaternion.Euler(0,GetLookDir(),0);
    }
    protected bool[] CanGo = {true,false,false,false};//갈 수 있는지 판별
    public void UpdateCanGo(bool[] UpBool,int nx,int ny, int nf)//방에 들가서 정보 얻기
    {
        CanGo = UpBool;
        x=nx;
        y=ny;
        f = nf;
    }
    void Start()
    {
        RoomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
    }
    public void Move()
    {
        if(CanGo[Dir])
            transform.position+=GoDirs[Dir];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
