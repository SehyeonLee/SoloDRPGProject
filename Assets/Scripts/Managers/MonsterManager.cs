using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    List<GameObject> Monsters = new List<GameObject>();//직원 명단
    public void SetMonster(GameObject N)//입사받기
    {
        Monsters.Add(N);
    }
    public void DelMonster(GameObject N)//사표받기
    {
        Monsters.Remove(N);
    }
    // Start is called before the first frame update
    public void CallFind()
    {
        foreach (GameObject M in Monsters)
        {
            M.GetComponent<Monster>().DoWork();//일시키기
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
