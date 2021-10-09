using UnityEngine;
using System.Collections;

public class inicioJuego : MonoBehaviour {

	public Camera camaraBuenos;
	public Camera camaraMalos;

	public GameObject jugador1;
	public GameObject jugador2;
	public GameObject jugador3;
	public GameObject jugador4;
	public GameObject jugador5;

	public Transform stand01;
	public Transform stand02;
	public Transform stand03;
	public Transform stand04;
	public Transform stand05;

	// Use this for initialization
	void Start () 
	{
		jugador1.transform.position = stand01.position;
		jugador2.transform.position = stand02.position;
		jugador3.transform.position = stand03.position;
		jugador4.transform.position = stand04.position;
		jugador5.transform.position = stand05.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
