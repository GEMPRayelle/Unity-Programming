using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementSystem : MonoBehaviour
{
    public Text achivementText;

    //플레이어가 떨어져 죽으면 뉴턴의 법칙이라는 도전과제를 달성하게 해준다
    public void UnlockAchivement(string title){
        Debug.Log("도전과제 해제 - " + title);
        achivementText.text = "도전과제 해제: " + title;
    }
}
