using UnityEngine;
using System.Collections;

public class VortexParticle : MonoBehaviour {

    public GameObject Parent;

	// Use this for initialization
	void Start () {
        if (Parent != null)
        {
            this.transform.SetParent(Parent.transform);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Parent != null)
        {
            this.transform.position = Parent.transform.position;
        }
	}
}
