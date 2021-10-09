using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM_J01_head : MonoBehaviour {

		public GameObject jugador01;
		public int x,y,z;

	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
				transform.position = jugador01.transform.position + new Vector3 (x, y, z);


	}
}
