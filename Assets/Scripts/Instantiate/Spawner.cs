using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{
    public GameObject target;

    void Start(){
        //Instantiate는 유니티에 내장되어 있는 함수
        Instantiate(target);
    }
}