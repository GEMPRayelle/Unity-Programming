using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    void Start(){
        StartCoroutine("HelloUnity");
        StartCoroutine("HelloCsharp");
        Debug.Log("END");
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)) StopCoroutine("HelloUnity");
    }

    IEnumerator HelloUnity()
    {
        while(true){
            yield return null;
            Debug.Log("Unity");
        }
    }

    IEnumerator HelloCsharp(){
        Debug.Log("Hi");
        yield return new WaitForSeconds(5f);
        Debug.Log("Csharp");
    }
}
