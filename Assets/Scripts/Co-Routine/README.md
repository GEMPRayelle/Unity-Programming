# (Unity) 코루틴
코루틴은 유니티에서 지원하는 기능으로 함수의 작업 과정 사이에 대기시간을 삽입할 수 있다
> 언제 사용하는데?

FadeIn, FadeOut처럼 서서히 변화는 기능을 만들때 코루틴을 사용한다  
뭔가를 변화시켜주기위해 for문을 사용하여 loop를 실행하면 코드를 보기에는 천천히 변하는 것 처럼 보이지만  
컴퓨터는 너무 빨라 사람이 인지하지 못할 시간에 반목문을 끝내고 결과를 보여준다

### 이런 상황에서 코루틴을 사용한다


```cs
void Start(){
    Func();
}

void Func()
{
    //일반적으로 함수는 함수가 시작된 다음에 함수가 종료될 때까지 일직선상으로 쭉 내려간다
    ..
}
```
일반적인 함수는 함수가 발동되었을때 첫번째 라인에서 마지막 라인까지 도달해야 함수가 종료된다  

```cs
IEnumerator Coroutine(){
    //A

    yield return new WaitForSeconds(0.1f);

    //B
}
```
하지만 코루틴은 IEnumerator라는 타입을 반환하는 함수로서 선언하게 되는데 
코루틴은 중간에 `yield return`문을 사용해서 뒤에 대기시간을 지정해주면 
A처리를 하고 yield를 만나면 대기시간동안 코드밖에 나가서 대기를 하고 다시 `yield`로 돌아와 B처리를 실행한다 

# 코루틴 사용방법

코루틴은 `IEnumerator`라는 서식을 사용하여 사용한다 
참고로 유니티의 고유 기능이라 정식문법이 아니기에 문법에 크게 신경쓸 필요는 없다

```cs
void Start(){
    StartCoroutine(Func());//함수로서 넣으면 성능은 더 좋지만 인위적으로 멈출 수는 없다
    StartCoroutine("Func");//문자열로서 넣으면 코루틴을 시작한 다음에 인위적으로 멈출 수 있다
}

IEnumerator Func(){
    yield return new WaitForSeconds(0.1f);
}
```
코루틴을 사용하기 위해선 `StartCoroutine`함수를 사용하여 매개변수로 함수를 전달시켜야 한다.  
코루틴 함수는 반드시 `IEnumerator`타입을 반환해야하고 `yield return`을 포함해야 한다  
return 뒤에는 `new WaitForSeconds`로 대기시간에 대한 데이터 타입을 명시해주면된다.
WaitForSeconds는 코루틴에서 대기하는 시간을 지정해주는 클래스이다

# 코루틴의 또 다른 기능
코루틴은 대기시간을 삽입하는거 말고 또 다른 기능이 있는데 비동기 방식을 구현하는 방법중 하나로 사용할 수 있다
>비동기 방식이란?

비동기 방식이란 함수 호출과 그 결과가 동시에 일어나지 않는걸 의미한다  

동기 방식은 A함수가 실행하고 끝나면 끝난거에 맞춰서 sync가 맞춰져서 그 다음 B가 실행되고 B가 끝나면 B가 끝난 시점에 sync가 맞춰서 C가 실행되는 방식이다

비동기 방식은 함수를 호출하더라도 그 함수의 결과를 기다리지 않고 계속해서 다음 코드를 읽는다  
즉, 자기 자신이 끝나는 지점에 동기화를 해서 다음 작업이 실행되는게 아닌 그냥 바로 A가 시작되자마자 다음 줄로 넘긴다  
Async라고도 부르는데 이런 방식은 동시에 여러 함수를 돌릴 수 있다는 이점이 있다
> 어떤 시점에 여러 작업이 동시에 처리되는 "멀티스레드 프로그래밍"을 구현할 수 있게된다  


아래는 비동기 방식의 예제코드이다
```cs
public class Fade : MonoBehaviour
{
    void Start(){
        StartCoroutine("HelloUnity");
        StartCoroutine("HelloCsharp");
        Debug.Log("END");
    }

    IEnumerator HelloUnity()
    {
        Debug.Log("Hello");
        yield return new WaitForSeconds(3f);
        Debug.Log("Unity");
    }

    IEnumerator HelloCsharp(){
        Debug.Log("Hi");
        yield return new WaitForSeconds(5f);
        Debug.Log("Csharp");
    }
}
```
시작하자마자 StartCoroutine("HelloUnity")에서 StartCoroutine("HelloCsharp")으로 넘어가는데  
위에 코루틴이 끝나기를 기다리지않는다 StartCoroutine("HelloCsharp")도 마찬가지로 코루틴을 시작하면  
시작한다는것을 알리고 바로 밑에 줄로 넘긴다.  
Debug.Log("END")도 사실상 게임이 시작되자마자 바로 찍히게되는것

코드를 직관적으로 봤을때는 8초가 걸릴거같지만 사실상 총 소요시간은 5초다

코루틴은 또 무한루프에서도 사용할 수 있다
```cs
IEnumerator HelloUnity()
{
    while(true){
        Debug.Log("Hello");
        yield return new WaitForSeconds(3f);
        Debug.Log("Unity");
    }
}
```
이런 방식으로 무한루프를 효과적으로 제어할 수 있는데 끝도 없이 반복을하는게 아닌 대기시간을 설정하기에 컴퓨터에 무리가 덜하다.  
그리고 무한루프가 끝나지않아 StartCoroutine("HelloCsharp")을 실행할 수 없을거같지만 코루틴을 사용하면 그거에 상관없이 실행이된다

* `StopCoroutine()` : `StopCoroutine`을 사용하면 코루틴을 중간에 강제로 중단시킬 수 있게 된다  

단 문자열을 사용하는 방식으로 코루틴을 시작해야한다
```cs
void Update(){
    if(Input.GetMouseButtonDown(0)) StopCoroutine("HelloUnity");
}
```

* `yield null` : return 뒤에는 대기시간을 지정할 수 있는데 대기 시간에 대한 데이터를 주지 않아도 된다

null이라고 하면 그 코루틴은 한 프레임을 쉬게 된다 (1/60)
```cs
IEnumerator HelloUnity(){
    while(true){
        //무한반복시 로그가 초당 60번 계속찍히게된다
        yield return null;
        Debug.Log("Unity");
    }
}
```
