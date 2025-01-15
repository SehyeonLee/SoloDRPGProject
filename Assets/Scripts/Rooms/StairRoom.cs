using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairRoom : Room
{
    // Start is called before the first frame update
    public bool GoUpDown = false; //True는 위로, False는 밑으로

    // Update is called once per frame
    new private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Player")
        {
            StateUpdate();//주변 상황 체크
            other.gameObject.GetComponent<Entity>().UpdateCanGo(CanMove,x,y,f);//플레이어한테 이동 가능 방향과 주소 알려주기
            other.gameObject.GetComponent<Entity>().SetStayRoom(GetComponent<Room>());//주소 알려주기+
            SetStay(other.gameObject);//나 플레이어 왔다고 기록
            MManager.CallFind();//몬스터 업데이트
            for(int i = 0 ; i<2;i++)
                other.gameObject.GetComponent<Entity>().TurnLeft();
            other.gameObject.GetComponent<Entity>().Move();
            Vector3 TempVecter = other.transform.position;
            if(GoUpDown)
                other.transform.position = new Vector3(TempVecter.x,TempVecter.y+1.2f,TempVecter.z);
            if(!GoUpDown)
                other.transform.position = new Vector3(TempVecter.x,TempVecter.y-1.2f,TempVecter.z);
        }
        if(other.gameObject.tag=="Monster")
        {
            IsMob=true;//나 몹 있음 체크
            other.gameObject.GetComponent<Entity>().UpdateCanGo(CanMove,x,y,f);//몹한테 이동 가능 방향과 주소 알려주기
            other.gameObject.GetComponent<Entity>().SetStayRoom(GetComponent<Room>());//몹한테 주소 알려주기
            SetStay(other.gameObject);//나 몹 갖고 있다고 기록
            CallNeiUpdate();//업뎃 있어요!
        }
    }
}
