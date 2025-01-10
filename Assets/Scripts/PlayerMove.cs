using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Entity
{
    // Start is called before the first frame update

    void Start()
    {
        RoomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
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

    public void tempAttack()//임시 공격, 나중에 삭제 예정
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
