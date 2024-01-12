# (Unity) 인스턴스화 Instantiate

유니티에서 Instantiate, 인스턴스화는 어떠한 대상을 실존하는 무언가로 찍어낸다는 의미이다.
이미 만들어진 게임 오브젝트를 필요할 때마다 실시간으로 만드는데. 보통 총알이나 몬스터를 찍어낼 때 사용한다.

```cs
public class Spawner : MonoBehaviour{
    public GameObject target;

    void Start(){
        //Instantiate는 유니티에 내장되어 있는 함수
        Instantiate(target);
    }
}
```

아래처럼 Hierarchy창에 빈오브젝트인 GameObject에 Spawner 스크립트를 넣고

인스턴스화 시킬 3D 오브젝트인 Sphere를 생성한다. 그리고 생성한 Ball을 target에 넣어준다

![image1](Instantiate/images/image1.png)

그러면 게임을 시작할때 Ball이 하나 더 생성되서 Scene상에서는 2개의 공이 생기게된다

![image2](Instantiate/images/image2.png)

보통 Instantiate를 할 때는 Scene상에 이미 존재하는 게임 오브젝트를 대상으로 하진 않는다

아래처럼 게임 오브젝트를 미리 만들어 놓고 편할때마다 쓸 수 있는 프리팹을 사용하여 생성한다

![image3](Instantiate/images/image3.png)

# 인스턴스화 응용
앞서 작성한 코드를 수정할 수 있는데 Instantiate를 할 때 위치화 회전을 원하는 곳으로 옵션을 줄 수 있다
```cs
public class Spawner : MonoBehaviour{
    public Transform spawnPosition;
    public GameObject target;

    void Start(){
        Instantiate(target,spawnPosition.position,spawnPosition.rotation);
    }
}
```

Hierarchy창에서 새로 빈오브젝트인 SpawnPosition를 만들고 그 위치에 Ball을 옮기게 할 수 있다

![image4](Instantiate/images/image4.png)

Instantiate는 새로 찍어낸 게임 오브젝트를 리턴 값으로 전달하는데 이 `target`을 기반으로 생성한 클론을 = 통해서 가져올수도있다
```cs
public class Spawner : MonoBehaviour{
    public Transform spawnPosition;
    public GameObject target;

    void Start(){
        GameObject instance = Instantiate(target,
        spawnPosition.position,spawnPosition.rotation);
    }
}
```

그런데 인스턴스화 할때는 무조건 GameObject 타입이 아니라 RigidBody같은 타입으로도 생성이 가능하다

그런데 원본을 인스턴스화할 때 게임 오브젝트 자체는 위와 똑같이 동작하지만 리턴을 할때 RigidBody 타입으로서 리턴을 한다
```cs
public class Spawner : MonoBehaviour{
    public Transform spawnPosition;
    public RigidBody target;

    void Start(){
        RigidBody instance = Instantiate(target,
        spawnPosition.position,spawnPosition.rotation);
    }
}
```

`GameObject`로서 인스턴스화하고 RigidBody를 가져오려면 다시 GetComponent로 가져와야하지만
```cs
instance.GetComponent<Rigidbody>();
```
방금 새로 찍은 오브젝트의 RigidBody만 곧바로 쓰고 싶으면 위처럼 원본을 RIgidBody로서 생성하면된다.

```cs
void Start(){
    Rigidbody instance = Instantiate(target,
    spawnPosition.position,spawnPosition.rotation);

    instance.AddForce(0,1000,0);
}
```
이 코드처럼 RigidBody로서 생성하고 바로 RigidBody속성의 함수를 바로 사용하여 생성하자마자 위로 튀어오르게 할 수 있다.
