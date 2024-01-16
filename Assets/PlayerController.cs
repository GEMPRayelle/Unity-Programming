using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //애니메이터 변수를 통해서 애니메이터에게 어떠한 파라미터를 변경하라고 지시할 수 있다
    public Animator animator; 
    //애니메이션이 재생될때 바라보거나 손을 얹을 타겟
    public Transform target;

    void Start()
    {
    }

    void Update()
    {   
        //스페이스바를 누르면
        if(Input.GetButtonDown("Jump")){
            animator.SetTrigger("Jump");
        }
        //세로방향: 아무것도 안누를시 0 방향키 위쪽을 누르면 1
        float verticalInput = Input.GetAxis("Vertical");
        //가로방향: 왼쪽버튼 -1 오른쪽버튼 1
        float horizontalInput = Input.GetAxis("Horizontal");

        //애니메이터에 Speed라는 파라미터를 찾아가서 verticalInput을 넣게된다
        animator.SetFloat("Speed", verticalInput);
        animator.SetFloat("Horizontal", horizontalInput);
    }

    //애니메이터가 부착되어있는 오브젝트에서만 실행됨
    private void OnAnimatorIK(int layerIndex) {
        //SetWeight 계열 함수들은 ik에게 우선순위를 결정해준다
        //1.0이면 완전히 적용되고 0에 가까울수록 덜 적용된다
        //매개변수로 ik를 적용할 부위 
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        //왼손이 target에 위치로 이동하게된다
        animator.SetIKPosition(AvatarIKGoal.LeftHand, target.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, target.rotation);

        //타겟을따라 시선도 따라가게한다
        animator.SetLookAtWeight(1.0f);
        animator.SetLookAtPosition(target.position);
    }
}
