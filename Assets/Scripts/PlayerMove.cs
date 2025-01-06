using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    Transform TF;
    int Dir = 0;
    GameObject StayRoom;
    RoomManager RoomManager;
    int x,y;
    public void SetRoom(GameObject GObj)
    {
        StayRoom = GObj;
    }
    public void TurnLeft()
    {
        Dir-=1;
        if(Dir<0)
            Dir=3;
        Turn();
    }
    public void TurnRight()
    {
        Dir+=1;
        if(Dir>3)
            Dir=0;
        Turn();
    }
    Vector3[] GoDirs = {new Vector3(0,0,1),new Vector3(1,0,0),new Vector3(0,0,-1),new Vector3(-1,0,0)};
    float GetLookDir()
    {
        return 90*Dir;
    }
    void Turn()
    {
        transform.rotation = Quaternion.Euler(0,GetLookDir(),0);
    }
    bool[] CanGo = {true,false,false,false};
    public void UpdateCanGo(bool[] UpBool,int nx,int ny)
    {
        CanGo = UpBool;
        x=nx;
        y=ny;
    }
    void Start()
    {
        RoomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
        TF = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
            Move();
        if(Input.GetKeyDown(KeyCode.A))
            TurnLeft();
        if(Input.GetKeyDown(KeyCode.D))
            TurnRight();
        if(Input.GetKeyDown(KeyCode.G))
            tempAttack(); 
    }
    public void Move()
    {
        if(CanGo[Dir])
            TF.position+=GoDirs[Dir];
    }

    public void tempAttack()
    {
        int tx = x,ty = y;
        switch(Dir)
        {
            case 0:
                ty++;
                break;
            case 1:
                tx++;
                break;
            case 2:
                ty--;
                break;
            case 3:
                tx--;
                break;
        }
        if(RoomManager.IsValuableSpot(tx,ty) ==false)
            return;
        GameObject TargetRoom = RoomManager.GetRoom(tx,ty);
        if(TargetRoom.GetComponent<Room>().GetIsMob())
        {
            Destroy(TargetRoom.GetComponent<Room>().GetStay());
        }
    }
}
