using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Monster : Entity
{

    public bool PlayerInsight= false, PlayerInsightTurn = false;//나중에 public 빼기, 플레이어를 봤어!, 본 턴이야(한턴쉬기)
    bool Turning = false,IsSeePlayer = false;
    int TargetX,  TargetY, Turningcount=0;//목적지랑 돈 횟수
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
        if(PlayerInsight&&PlayerInsightTurn)//플레이어를 봤었다면
            Chase();
        else if(PlayerInsight&&!PlayerInsightTurn)
            PlayerInsightTurn = true;
    }
    void Chase()//플레이어 추격
    {
        
        if(x==TargetX&&y==TargetY&&!Turning)//마지막 목격지 도착
        {
            Turning = true;
            Debug.Log(Turningcount);
            return;
        }
        if(Turning)//목적지 도착시 회전
        {
            Debug.Log("돈당");
            if(Turningcount==4)//한바퀴 돌아봄
            {
                Turningcount=0;
                Turning=false;
                PlayerInsight = false;
                PlayerInsightTurn = false;
            }
            else
            {
                TurnLeft();
                Turningcount++;
            }
            return;
        }
        Debug.Log("쫓는다.");
        if((Mathf.Abs(TargetX-x)+Mathf.Abs(TargetY-y)!=1)||!IsSeePlayer)//플레이어 바로 앞이 아니면
            Move();//이동
        else
            Debug.Log("공격");
    }
    void CheckSight()//플레이어 탐지
    {
        
        RaycastHit Hit;
        int layerMask = 1 << LayerMask.NameToLayer("Target");
        bool IsHit = Physics.Raycast(transform.position,transform.forward,out Hit, 3f);//target 마스크
        if(IsHit)//나 뭔가를 봤어!
        {
            if(Hit.collider.gameObject.tag=="Player")//그게 플레이어야.
            {
                PlayerInsight = true;
                Vector2 Temp = Hit.collider.gameObject.GetComponent<Entity>().GetXY();
                TargetX = (int)Temp.x;//목적지 설정
                TargetY = (int)Temp.y;
                Turning = false;
                IsSeePlayer = true; //플레이어를 봤어!
                Turningcount = 0;
            }
            else
                IsSeePlayer = false;//플레이어가 아니네
            Debug.Log(Hit.collider.gameObject.tag);
        }
        else
            IsSeePlayer = false;
    }
    private void OnDestroy() 
    {
        StayRoom.SetIsMob(false);
        Manager.DelMonster(gameObject);//퇴사
        StayRoom.CallNeiUpdate();
    }
}
