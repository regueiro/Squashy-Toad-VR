using UnityEngine;
using System.Collections;

public class FireWall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var player = GameObject.Find("Player");
	    var delta = player.transform.position - transform.position;
	    var projectedDelta = Vector3.Project(delta, Vector3.left);

	    transform.position += projectedDelta;
	}
}
