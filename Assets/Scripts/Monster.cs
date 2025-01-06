using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Room StayRoom;
    public void SetStayRoom(Room Where)
    {
        StayRoom = Where;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy() 
    {
        StayRoom.SetIsMob(false);
        StayRoom.CallNeiUpdate();
    }
}
