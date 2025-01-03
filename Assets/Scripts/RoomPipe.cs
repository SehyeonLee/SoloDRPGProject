using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomPipe : MonoBehaviour
{
    GameObject Room;
    int Dir = 0;
    Vector3[] GoDirs = {new Vector3(0,0,1),new Vector3(1,0,0),new Vector3(0,0,-1),new Vector3(-1,0,0)};
    new Vector3 StartPosition;
    // Start is called before the first frame update
    void Start()
    {
        Room = gameObject.transform.parent.gameObject;
        StartPosition = transform.position;
        StartCoroutine("Search");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Search()
    {
        for(int i = 0;i<10;i++)
            yield return null;
        for(int i =0;i<4;i++)
        {
            transform.position+=GoDirs[Dir];
            for(int j = 0;j<3;j++)
                yield return null;
            Dir++;
            transform.position = StartPosition;
            for(int j = 0;j<2;j++)
                yield return null;
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag=="Room"&&other.gameObject!=Room)
        {
            Room.GetComponent<Room>().SetRoom(Dir,other.gameObject);
        }
    }
}
