using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int x,y;
    public bool[] CanMove = {false,false,false,false};//앞 오 뒤 왼
    GameObject[] NeiRooms = new GameObject[4];
    public void SetRoom(int i,GameObject GObj)
    {
        NeiRooms[i] = GObj;
    }
    bool IsMob = false;
    public void SetIsMob(bool How)
    {
        IsMob = How;
    }
    public bool GetIsMob()
    {
        return IsMob;
    }
    RoomManager Manager;
    GameObject Stay;
    public void SetStay(GameObject Who)
    {
        Stay = Who;
    }
    public GameObject GetStay()
    {
        return Stay;
    }
    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();
        Manager.SetRoom(gameObject,x,y);
        StartCoroutine("Search");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StateUpdate()
    {
        for(int i = 0;i<4;i++)
            if(ReferenceEquals(NeiRooms[i],null)==false)
            {
                Room NeiRoom =NeiRooms[i].GetComponent<Room>();
                int ti = i+2;
                if(ti >3){ti-=4;}
                if(NeiRoom.CanMove[ti])
                    CanMove[i]=true;
                if(NeiRoom.IsMob)
                    CanMove[i]=false;
            }
    }
    public void CallNeiUpdate()
    {
        for(int i = 0;i<4;i++)
            if(ReferenceEquals(NeiRooms[i],null)==false)
            {
                NeiRooms[i].GetComponent<Room>().StateUpdate();
            }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Player")
        {
            StateUpdate();
            other.gameObject.GetComponent<PlayerMove>().UpdateCanGo(CanMove,x,y);
            SetStay(other.gameObject);
        }
        if(other.gameObject.tag=="Monster")
        {
            IsMob=true;
            other.gameObject.GetComponent<Monster>().SetStayRoom(GetComponent<Room>());
            SetStay(other.gameObject);
        }
    }
    IEnumerator Search()
    {
        for(int i =0;i<10;i++)
            yield return null;
        Manager.GetNei(GetComponent<Room>(),x,y);
    }
}
