using UnityEngine;
using System.Collections;

public class MyComponent : MonoBehaviour {

    public int intVariable;
    public float floatVariable;
    public GameObject[] gameObjects;

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
            return intVariable;
        }
        set
        {
            intVariable = value;
        }
    }

    public void DoSomething()
    {
        intVariable += 1;
    }
}
