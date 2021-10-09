using UnityEngine;
using System.Collections;

public class menuCanvas001 : MonoBehaviour {

	public AudioClip sonidoMancha;

	public bool nuevaPulsada;
	public bool cargarPulsada;
	public bool opcionesPulsada;
	public bool salirPulsada;

	public bool disparandoNueva;
	public bool disparandoCargar;
	public bool disparandoOpciones;
	public bool disparandoSalir;

	public float tiempo = 0;

	public Texture2D manchas;
	public GameObject teclas;

	public int faseLevel = 0;


	void start()
	{
	
	}

	// Update is called once per frame
	void Update () 
	{
		// codigosTiempo();
		Cursor.visible = true;
	}

	void codigosTiempo()
	{
		if(disparandoNueva || disparandoCargar || disparandoOpciones || disparandoSalir)
		{
			tiempo = tiempo + 1;
		}

		if(tiempo > 12)
		{
			tiempo = 15;
		}
	}

	public void teclasVisible()
	{
		teclas.SetActive(true);
	}

	public void teclasNoVisible()
	{
		teclas.SetActive(false);
	}

	public void pulsarNueva()
	{
		Application.LoadLevel(1);		// cargamos el nivel de juego
	}

	public void pulsarCargar()
	{
		// desactivado	
	}

	public void pulsarOpciones()
	{
		// desactivado
	}

	public void pulsarSalir()
	{
		Application.Quit();								// salimos de la aplicacion
	}
}
