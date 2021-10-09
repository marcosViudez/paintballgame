using UnityEngine;
using System.Collections;

public class escuchaSonidos : MonoBehaviour {

	public GameObject camarasScript;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(camarasScript.GetComponent<camarasEnPantalla>().camaraUno.enabled == true)
		{
			transform.position = camarasScript.GetComponent<camarasEnPantalla>().camaraUno.transform.position;
		}

		if(camarasScript.GetComponent<camarasEnPantalla>().camaraDos.enabled == true)
		{
			transform.position = camarasScript.GetComponent<camarasEnPantalla>().camaraDos.transform.position;
		}

		if(camarasScript.GetComponent<camarasEnPantalla>().camaraTres.enabled == true)
		{
			transform.position = camarasScript.GetComponent<camarasEnPantalla>().camaraTres.transform.position;
		}

		if(camarasScript.GetComponent<camarasEnPantalla>().camaraCuatro.enabled == true)
		{
			transform.position = camarasScript.GetComponent<camarasEnPantalla>().camaraCuatro.transform.position;
		}

		if(camarasScript.GetComponent<camarasEnPantalla>().camaraCinco.enabled == true)
		{
			transform.position = camarasScript.GetComponent<camarasEnPantalla>().camaraCinco.transform.position;
		}

		if(camarasScript.GetComponent<camarasEnPantalla>().camaraSeis.enabled == true)
		{
			transform.position = camarasScript.GetComponent<camarasEnPantalla>().camaraSeis.transform.position;
		}

		if(camarasScript.GetComponent<camarasEnPantalla>().camaraSiete.enabled == true)
		{
			transform.position = camarasScript.GetComponent<camarasEnPantalla>().camaraSiete.transform.position;
		}

		if(camarasScript.GetComponent<camarasEnPantalla>().camaraOcho.enabled == true)
		{
			transform.position = camarasScript.GetComponent<camarasEnPantalla>().camaraOcho.transform.position;
		}
	}
}
