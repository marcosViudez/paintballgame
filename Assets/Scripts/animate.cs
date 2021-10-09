using UnityEngine;
using System.Collections;

public class animate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<Animation>().CrossFade ("sentarse");
	}


}
