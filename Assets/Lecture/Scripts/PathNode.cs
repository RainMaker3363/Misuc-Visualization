using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        //Debug.Log("gizmo");

        //Gizmos.color = new Color(1, 0, 0, 0.5F);
        //Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));

        Vector3 forwardVec = transform.forward;
        Vector3 fromVec = transform.position + forwardVec * 3.0f;
        Vector3 toVec = transform.position + forwardVec * -3.0f;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(fromVec, toVec);
    }
}
