using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;//이 네임스페이스를 사용해야 이벤트를 사용할 수 있다
public class PlayerHealth : MonoBehaviour
{
    //기능들이 바깥에서 등록할 수 있는 명단
    public UnityEvent onPlayerDead;
    private void Dead(){
        onPlayerDead.Invoke();
        Debug.Log("사망");
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other) {
        Dead();
    }
}
