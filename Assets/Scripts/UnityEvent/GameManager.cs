using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //플레이어가 죽고 5초뒤에 시작한다
    public void OnPlayerDead(){
        //어떤 함수를 명시해주고 지연시간을 명시해주면 그 시간만큼 흐른후 함수를 실행한다
        Invoke("Start",5f);
    }
    private void Restart(){
        //지금 보고 있는 씬을 다시 로드 한다
        SceneManager.LoadScene(0);
    }
}
