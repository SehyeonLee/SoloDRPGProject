using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{

    public bool PlayerInsight= false;
    int TargetX,  TargetY;
    MonsterManager Manager; //매니저 연락처
    
    // Start is called before the first frame update
    void Start()
    {
        CalcLookDir();
        Manager = GameObject.FindGameObjectWithTag("MonsterManager").GetComponent<MonsterManager>();//매니저님 컨택
        Manager.SetMonster(gameObject);//입사
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoWork()//몬스터 상태 업데이트 호출
    {
        Debug.Log("일한당");
        CheckSight();//플레이어 탐지
        if(PlayerInsight)//플레이어를 봤었다면
            Chase();
    }
    void Chase()//플레이어 추격
    {
        
        if(x==TargetX&&y==TargetY)//마지막 목격지 도착
        {
            PlayerInsight = false;
            return;
        }
        Debug.Log("쫓는다.");
        Move();//이동
    }
    void CheckSight()//플레이어 탐지
    {
        
        RaycastHit Hit;
        int layerMask = 1 << LayerMask.NameToLayer("Target");
        bool IsHit = Physics.Raycast(transform.position,transform.forward,out Hit, 3f,layerMask);//target 마스크
        if(IsHit)//나 뭔가를 봤어!
        {
            if(Hit.collider.gameObject.tag=="Player")//그게 플레이어야.
            {
                PlayerInsight = true;
                Vector2 Temp = Hit.collider.gameObject.GetComponent<Entity>().GetXY();
                TargetX = (int)Temp.x;//목적지 설정
                TargetY = (int)Temp.y;
            }
            Debug.Log(Hit.collider.gameObject.tag);
        }
    }
    private void OnDestroy() 
    {
        StayRoom.SetIsMob(false);
        Manager.DelMonster(gameObject);//퇴사
        StayRoom.CallNeiUpdate();
    }
}
