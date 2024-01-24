# (Unity) UGUI

UGUI는 유티니의 공식 UI 시스템이다. 이 시스템이 존재하기전까지는 Legacy UI가 존재해서 업데이트문 안에서 매 프레임마다 UI를
새로 그리고 지우고를 코드를 통해서 제어를 했다.

> 코드를 통해서 제어를 했기에 플레이 버튼을 눌러서 테스하기 전까지 UI를 확인할 방법이없었다.

그래서 새로나온 UI시스템은 UI요소를 게임 오브젝트나 컴포넌트로서 편집하게 해준다.  
플레이어에게 RigidBody를 붙이듯이 UI시스템을 게임 오브젝트 Hierarchy 인스펙에서 편집이 가능하기때문에  
UI시스템을 마치 기존에 게임오브젝트를 편집하듯이 쉽게 편집할 수 있다

# (UGUI) 캔버스 
캔버스는 모든 UI 게임 오브젝트들이 들어가 있어야 하는 게임 오브젝트이다  
UI게임 오브젝트들에게 좌표계를 제공하기때문에 모든 UI 게임 오브젝트들은 캔버스 자식으로 있어야한다 

![image1](images/image1.png)

기본적으로 UI 이미지 오브젝트를 만들면 캔버스와 이미지 EventSystem이 생성되고  자동으로 캔버스가 같이 생긴다.  
`EventSystem`은 게임 오브젝트 중에서도 UI게임 오브젝트들이 터치나 클릭 혹은  
개발자가 직접만든 이벤트를 감지할 수 있도록 만들어주는 오브젝트이고 자동으로 동작한다  

`Canvas`는 모든 UI 오브젝들을 가지고있는 스크린과 같은 오브젝트다

![image2](images/image2.png)

유니티 씬 패널에서 2D를 체크하고 캔버스를 더블클릭하면 캔버스를 전체화면으로 볼 수 있는데  
캔버스가 크게 보이는 이유는 3D게임 세상의 좌표계가 아닌 유저의 게임 화면(해상도)에 1:1로 대응하고 동시에 기본 값으로는 Canvas의 1px길이는 유니티 세상의 1m에 대응하기 때문이다
그리고 캔버스는 모든 UI 게임 오브젝트를 위한 스크린 좌표를 제공해주는데 캔버스 자체가 UI 오브젝트들이 배치되기 위한 하나의 프레임을 마련해주는샘이다

![image3](images/image3.png)

그리고 캔버스는 3가지의 Render 모드를 가지는데
* Screen Space - Overlay  
  UI요소들이 스크린 상에만 존재하고 모든 3D오브젝트들이 그려진 다음에 마지막으로 UI요소를 덮어쓰는 방식이다

* Screen Space - Camera  
  이 방식도 위와 마찬가지로 스크린 좌표계에 대응한다. 두 방식다 화면상에 대응을 하지만 이 방식같은 경우는
  Space Camera같은 경우는 3D좌표상에서 위치를 가지게 된다 

![image4](images/image4.png)

인스펙터창에는 초기에는 이런 상태이지만 특정 카메라를 지정하게 되면 

![image5](images/image5.png)

Render Camera에 지정을 해주면 Plane Distance가 생기게되는데  
Plane Distance는 메인 카메라로부터 100m 떨어진 거리에 UI요소가 배치되게 한다

![image6](images/image6.png)

씬에서 2D 모드를 해제하고 캔버스를 보면 이제 좌표를 가지게된다.  
캔버스는 원래 좌표계랑 상관없이 동작하는데 이 방식을 사용하면 카메라를 기준으로 Plane Distance 거리만큼 떨어진곳에 배치가 된다.  
카메라가 이동하거나 회전하게되면 똑같이 캔버스도 따라 움직이는데 보이는 화면은 위에 방식이랑 다를게 없다  

하지만 대상이 되는 카메라에서 평행하게 떨어져있는 방식으로 구현이 되기 때문에 카메라랑 캔버스사이에 3D 오브젝트가 배치가 될 수 있는데  
그러면 아래처럼 UI요소가 사라지게 보일 수 있다.

![image7](images/image7.png)

* World Space  
  이 방식은 캔버스 자체가 3D 상에서 좌표랑 회전을 다 가지게 되는 방식이다  
  원래 인스펙터창에서 Rect Transform이 회색으로 처리되어 수정이 불가능한데  
  World Space로 설정하면 수정이 가능하고, 캔버스 자체를 마음대로 움직일 수 있게 된다  
 
![image8](images/image8.png)

  그래서 다른 방식과달리 3D 오브젝트로 취급을 하기에 UI요소를 마음대로 회전하고 배치할 수 있다
  이 방식은 증강현실 UI를 구현할 때 사용할 수 있다