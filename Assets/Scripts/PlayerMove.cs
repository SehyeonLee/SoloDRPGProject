using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    Transform TF;
    int Dir = 0;
    GameObject StayRoom;
    public void SetRoom(GameObject GObj)
    {
        StayRoom = GObj;
    }
    public void TurnLeft()
    {
        Dir-=1;
        if(Dir<0)
            Dir=3;
    }
    public void TurnRight()
    {
        Dir+=1;
        if(Dir>3)
            Dir=0;
    }
    Vector3[] GoDirs = {new Vector3(0,0,1),new Vector3(1,0,0),new Vector3(0,0,-1),new Vector3(-1,0,0)};
    Vector3 GetLookDir()
    {
        return new Vector3(0,90*Dir,0);
    }
    
    bool[] CanGo = {false,false,false,false};
    public void UpdateCanGo(bool[] UpBool)
    {
        CanGo = UpBool;
    }
    void Start()
    {
        TF = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move()
    {
        if(CanGo[Dir])
            TF.position+=GoDirs[Dir];
    }
}
