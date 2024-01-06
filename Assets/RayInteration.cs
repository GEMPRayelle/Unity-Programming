using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayInteraction : MonoBehaviour
{
    //RayCast = 기본적으로 어떤 위치에서 광선을 발사해서 그 광선에 닿는 물체가 있는지 검사하는 방식
    //보통 정확한 물리처리를 요구할 때 사용함
    
    //LayerMask를 통해서 대응하고싶은 레이어를 지정해줌
    public LayerMask whatIsTarget;
    //카메라에 정중앙에서 앞쪽으로 광선을 쏘기위해서 메인카메라의 위치를 알아야함
    private Camera playerCam;
    //광선이 날아가는 거리, 100미터 앞쪽으로 광선을 날림
    public float distance = 100f;
    //충돌한 상대방을 이 변수에 넣어 계속 시선에 따라 이동시키도록 할 변수
    private Transform moveTarget;
    //처음상대방과 나 사이의 거리, 그 간격을 유지한채로 옮길계획
    private float targetDistance;

    void Start(){
        //현재활성화된 메인 카메라를 넣어준다
        playerCam = Camera.main;
    }

    void Update()
    {   
        //광선이 날아갈 원점을 지정함
        //playerCam에 trasform.position을해도 상관없음
        //ViewportToWorldPoint()는 카메라 상에 위치를 찍어주면 그 위치가 실제 게임상에서 어떤 위치인지 알려줌
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0f));//x(가로방향)으로 0.5는 50%지점인 정중앙이고 y또한 마찬가지 z는 카메라의 깊이이다 0으로함 
        //화면상에 위치를 찍어주면 그 화면 상의 점이 world position으로 어딘지 알려줌
        //카메라로 보고있는 화면상에 정중앙지점이 -> 게임세상 기준으로 어떤 지점인지를 rayOrigin에 저장해줌
        
        //광선을 어느 방향으로 쏘는지 정함
        //카메라에 앞쪽 방향으로함
        Vector3 rayDirection = playerCam.transform.forward;

        //raycast는 다양한 형태로할 수 있는데 Ray를 사용하여 정보를 담아준뒤
        //이 ray데이터를 사용해서 아래처럼 다양한 변수로 사용할 수 있다
        Ray ray = new Ray(rayOrigin, rayDirection);

        //보통 raycast는 눈에보이지않는 광선을 발사해 검사하는 처리방식이라 눈으로 확인할 수 없음
        //개발자가 확인할 수 있도록 레이를 그려주는 함수
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        
        //왼쪽 마우스버튼을 눌렀을때
        if(Input.GetMouseButtonDown(0)){
            //raycast에 의해 충돌했을때의 정보를 담아주는 정보 컨테이너
            RaycastHit hit;

            //raycast처리는 물리기반 처리라 Physics클래스안에 함수가 있다
            
            /* [whatIsTartget을 사용한 이유]
            기본적으로 물리처리를 할때 layer를 기반으로 필터링을 하게되서 
            다른 오브젝트와 상호작용을 하려면 그 오브젝트도 layer설정을 해줘야한다*/

            //if(Physics.Raycast(rayOrigin, rayDirection, out hit, distance,whatIsTarget)) -> rayOrigin과 rayDirection을 나눠서 넣어줄 필요없이 ray만 넣어줘도 됨
            if(Physics.Raycast(ray, out hit, distance,whatIsTarget)){//광선에 어떤 collider가 걸리면 true
            //out은 입력으로 들어간 값이 내부에서 어떤 값을 챙겨 빠져나오는걸 의미

                
                //hit은 다양한 정보들을 가지고있다
                //hit.point; 충돌한 위치
                //hit.normal 충돌각, 충돌한 상대방의 평면이 보고 있는 방향
                //hit.distance 충돌한 위치까지의 거리
                GameObject hitTarget = hit.collider.gameObject;//충돌한 상대방
                hitTarget.GetComponent<Renderer>().material.color = Color.red;//상대방의 색을 바꿈
                moveTarget = hitTarget.transform;
                targetDistance = hit.distance;

                

                Debug.Log(hit.collider.gameObject.name);//충돌한 상태 오브젝트의 이름을 출력
                Debug.Log("뭔가 감지됨");
            }
        }
        //마우스 버튼을 해제하면
        if(Input.GetMouseButtonUp(0)){
            if(moveTarget != null){
                moveTarget.GetComponent<Renderer>().material.color = Color.white;//색도 원래대로 돌려놓음
            }
            moveTarget = null;
        }
        //움직일 상대방이 존재하면
        if(moveTarget != null){
            //상대방을 시선에 따라 움직임 
            //ray.origin = 광선이 시작되는 위치 = 카메라의 위치
            moveTarget.position = ray.origin + ray.direction * targetDistance;//처음 기록한 간격을 유지한채로 옮김
        }
    }
}
