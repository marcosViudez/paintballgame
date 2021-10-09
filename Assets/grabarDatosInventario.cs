using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class grabarDatosInventario : MonoBehaviour {

	private StreamWriter fileSave;
	public GameObject botonJugar;

	[System.Serializable]
	public class inventariosDeJugadores
	{
		public GameObject inventario;
		public int dineroTotalTienda;
		public int totalBolasAmarillas;
		public int totalBolasRojas;
		public int totalBolasVerdes;
		public int totalBolasAzules;
		public int totalBolasVioletas;
		public int numeroColorBolas;
		public int numeroMascaraTienda;
		public int numeroPodsTienda;
		public int numeroGasTienda;
		public int numeroCargadorTienda;
	}

	public inventariosDeJugadores[] variablesJugadores = new inventariosDeJugadores[5];

	void Start()
	{
		Cursor.visible = true;
		botonJugar.GetComponent<Button>().interactable = false;
	}

	// Update is called once per frame
	void Update () 
	{
		for(int i = 0; i < 5; i++)
		{
			variablesJugadores[i].numeroMascaraTienda = variablesJugadores[i].inventario.GetComponent<inventarioV004>().numeroMascara;
			variablesJugadores[i].numeroColorBolas = variablesJugadores[i].inventario.GetComponent<inventarioV004>().numeroColor;
		}
	}

	public void volverMenuPrincipal()
	{
		Application.LoadLevel(0);		// volvemos al nivel principal
	}

	public void irMenuJugar()
	{
		Application.LoadLevel(2);		// vamos a la escena de jugar
	}

	public void grabandoDatos()
	{
		// grabamos los datos en un documento de texto
		fileSave = new StreamWriter("tiendaGuardado.txt");
		for(int i = 0; i < 5; i++)
		{
			fileSave.WriteLine(variablesJugadores[i].numeroMascaraTienda);
			fileSave.WriteLine(variablesJugadores[i].numeroColorBolas);
		}
		fileSave.Close();

		// hacer visible boton jugar
		botonJugar.GetComponent<Button>().interactable = true;
	}
}
