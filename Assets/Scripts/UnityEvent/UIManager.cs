using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text playerStateText;

    //플레이어가 죽었을때 텍스트 내용물을 덮어쓰게한다
    public void OnPlayerDead(){
        playerStateText.text = "You Die";
    }
}
