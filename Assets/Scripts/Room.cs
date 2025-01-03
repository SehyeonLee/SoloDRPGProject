using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool[] CanMove = {false,false,false,false};
    GameObject[] Rooms = new GameObject[4];
    public void SetRoom(int i,GameObject GObj)
    {
        Rooms[i] = GObj;
    }
    bool IsMob = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Player")
        {
            other.gameObject.GetComponent<PlayerMove>().UpdateCanGo(CanMove);
        }
    }
}
