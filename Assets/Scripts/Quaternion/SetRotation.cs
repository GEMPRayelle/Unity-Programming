using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour{
    public Transform targetTransform;
    void Start(){
        //Vector3를 거치지않고 Quaternion끼리 바로 각도를 증가시킬 수 있다
        Quaternion originRotation = Quaternion.Euler(new Vector3(45,0,0));
        Quaternion plusRotation = Quaternion.Euler(new Vector3(30,0,0));

        //두 쿼터니언끼리 곱하면 값이 더해진다
        Quaternion targetRotation = originRotation * plusRotation;
        transform.rotation = targetRotation;//75도
    }
}
