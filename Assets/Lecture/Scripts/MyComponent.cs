using UnityEngine;
using System.Collections;

[AddComponentMenu("My Menu/MyComponent Script")]
public class MyComponent : MonoBehaviour {

    public int intVariable;
    public float floatVariable;
    public GameObject[] gameObjects;

    // 이 변수는 private 변수지만 Serialize로 사용하겠다 라고 선언
    // 캐쉬(cach)에 저장하여 씬을 넘어가도 유지되도록 할 수 있다.
    [SerializeField]
    private int _intvar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int intvar
    {
        get
        {
            return _intvar;
        }
        set
        {
            _intvar = value;
        }
    }

    public void DoSomething()
    {
        intVariable += 1;
    }
}
