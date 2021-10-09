using UnityEngine;
using System.Collections;

public class moveEscenario : MonoBehaviour {

	float velocidad = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.W))
		{
			// moverse hacia adelante
			transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
		}	
		
		if(Input.GetKey(KeyCode.S))
		{
			// moverse hacia atras
			transform.Translate(Vector3.forward * -velocidad * Time.deltaTime);
		}
		
		if(Input.GetKey(KeyCode.A))
		{
			// moverse hacia izquierda
			transform.Translate(Vector3.right * -velocidad * Time.deltaTime);
		}
		
		if(Input.GetKey(KeyCode.D))
		{
			// moverse hacia derecha
			transform.Translate(Vector3.right * velocidad * Time.deltaTime);
		}
	}
}
