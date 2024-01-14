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
    StartCoroutine(Func());
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
코루틴은 대기시간을 삽입하는거 말고 또 다른 기능이 있는데 
