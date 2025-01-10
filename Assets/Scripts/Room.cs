using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int x,y;//매니저에 신고할 자기 좌표
    public bool[] Passage= {false,false,false,false};//앞 오 뒤 왼, 길이 있냐.
    bool[] CanMove = {false,false,false,false};//앞 오 뒤 왼, 갈 수 있을지 반환
    GameObject[] NeiRooms = new GameObject[4];//이웃 방들
    public void SetRoom(int i,GameObject GObj)//입주신고
    {
        NeiRooms[i] = GObj;
    }
    bool IsMob = false;//나 몹 갖고있니
    public void SetIsMob(bool How)//갖고 있을지 세팅
    {
        IsMob = How;
    }
    public bool GetIsMob()//내가 갖고 있는지 반환
    {
        return IsMob;
    }
    RoomManager Manager;//매니저님 연락처
    MonsterManager MManager; //옆집 매니저 연락처
    GameObject Stay;//이 방에 있는 몹/플레이어(아직은 그 둘)
    public void SetStay(GameObject Who)//누가 있는지 세팅
    {
        Stay = Who;
    }
    public GameObject GetStay()//갖고 있는애 반환
    {
        return Stay;
    }
    bool IsLocked = false; //잠긴방 구현
    public bool StartLock = false; //게임 시작시 잠긴방 세팅팅
    public void SetIsLocked(bool How) //잠겼나 세팅
    {
        IsLocked =How;
    }
    public bool GetIsLocked() //잠겼나 반환
    {
        return IsLocked;
    }
    // Start is called before the first frame update
    void Start()
    {
        IsLocked = StartLock; //시작시 잠겨야함 잠그기기
        Manager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>();//매니저님 컨택
        MManager = GameObject.FindGameObjectWithTag("MonsterManager").GetComponent<MonsterManager>();//매니저님 컨택
        Manager.SetRoom(gameObject,x,y);//입주신고
        CanMove = Passage; //길 파악
        StartCoroutine("Search");//이웃 파악
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StateUpdate()//내 상태 업뎃
    {
        for(int i = 0;i<4;i++)
            if(ReferenceEquals(NeiRooms[i],null)==false)//옆방이 있나 확인, 널체크
            {
                Room NeiRoom =NeiRooms[i].GetComponent<Room>();//옆방 확인 최적화
                CanMove[i]=Passage[i];//길 뚫려있던가 확인
                int ti = i+2;//나한테는 왼쪽이 쟤한테는 오른쪽이니까
                if(ti >3){ti-=4;}
                if(NeiRoom.CanMove[ti])//서로 오갈 수 있는지 확인
                    CanMove[i]=true;
                if(NeiRoom.GetIsMob())//옆방에 몹 있는지 확인
                    CanMove[i]=false;
                if(NeiRoom.GetIsLocked())//옆방이 잠겨있나 확인인
                    CanMove[i]=false;
            }
    }
    public void CallNeiUpdate()//주변한테 나 변경된거 있으니 상황 최신화 하라고 소리치기
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
            StateUpdate();//주변 상황 체크
            other.gameObject.GetComponent<Entity>().UpdateCanGo(CanMove,x,y);//플레이어한테 이동 가능 방향과 주소 알려주기
            other.gameObject.GetComponent<Entity>().SetStayRoom(GetComponent<Room>());//주소 알려주기+
            SetStay(other.gameObject);//나 플레이어 왔다고 기록
            MManager.CallFind();//몬스터 업데이트
        }
        if(other.gameObject.tag=="Monster")
        {
            IsMob=true;//나 몹 있음 체크
            other.gameObject.GetComponent<Entity>().UpdateCanGo(CanMove,x,y);//몹한테 이동 가능 방향과 주소 알려주기
            other.gameObject.GetComponent<Entity>().SetStayRoom(GetComponent<Room>());//몹한테 주소 알려주기
            SetStay(other.gameObject);//나 몹 갖고 있다고 기록
            CallNeiUpdate();//업뎃 있어요!
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag=="Monster")
        {
            IsMob=false;//나 몹 없음 체크
            SetStay(null);//나 몹 없다고 기록
            CallNeiUpdate();//업뎃 있어요!
        }
    }
    IEnumerator Search()//이웃 찾기
    {
        for(int i =0;i<10;i++)//다른방 신고 기다리기
            yield return null;
        Manager.GetNei(GetComponent<Room>(),x,y);//매니저님 나 이웃 있어요?
        StateUpdate();//이웃 찾았으니 업뎃
    }
}
