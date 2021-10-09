using UnityEngine;
using System.Collections;
using System.IO;
 
public class cargarDatosEscena001 : MonoBehaviour {

	public GameObject[] jugadores = new GameObject[5];
	public GameObject[] enemigos = new GameObject[5];

	public GameObject[] mascaraJugadores = new GameObject[5];		// mascaras de la malla 3D
	public GameObject[] mascaraEnemigos = new GameObject[5];		// mascaras de la malla 3D

	public Material[] matMascaras = new Material[8];

	// variables cargadas desde el archivo
	private int dineroTotalTiendaCargado;
	private int totalBolasAmarillasCargado;
	private int totalBolasRojasCargado;
	private int totalBolasVerdesCargado;
	private int totalBolasAzulesCargado;
	private int totalBolasVioletasCargado;
	private int[] numeroMascaraTiendaCargado = new int[5];
	private int[] numeroColorTiendaBolas = new int[5];
	private int numeroPodsTiendaCargado;
	private int numeroGasTiendaCargado;
	private int numeroCargadorTiendaCargado;

	public bool mascarasEnemigosAsignadas;
	public bool mascarasJugadoresAsignadas;

	private StreamReader fileLoad;

	// Use this for initialization
	void Start () 
	{
		// cargarDatos ();
		cargandoDatosInventarios();
		mascarasEnemigosAsignadas = false;
		mascarasJugadoresAsignadas = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		asignarVariables ();
		colocarMascarasEnemigos();
		colocarMascarasJugadores();
	}

	/// <summary>
	/// carga los datos de un archivo de txt de :
	/// mascara y el color de las bolas de pintura
	/// </summary>
	public void cargandoDatosInventarios()
	{
		fileLoad = new StreamReader("tiendaGuardado.txt");
		for(int i = 0; i<5; i++)
		{
			numeroMascaraTiendaCargado[i] = int.Parse(fileLoad.ReadLine());
			numeroColorTiendaBolas[i] = int.Parse(fileLoad.ReadLine());
		}
		fileLoad.Close();
	}

	/// <summary>
	/// activacion de las mascaras de los jugadores
	/// </summary>
	void colocarMascarasJugadores()
	{
		// colocamos mascaras aleatorias a los jugadores
		if(!mascarasJugadoresAsignadas)
		{
			for(int j = 0; j<5; j++)
			{
				if(numeroMascaraTiendaCargado[j] != 0)
				{
					mascaraJugadores [j].SetActive(true);	
					mascaraJugadores [j].GetComponent<Renderer> ().material = matMascaras[numeroMascaraTiendaCargado[j]];
					mascarasJugadoresAsignadas = true;
				}
			}
		}
	}

	/// <summary>
	/// activacion de las mascaras de los enemigos
	/// </summary>
	void colocarMascarasEnemigos()
	{
		// colocamos mascaras aleatorias a los enemigos
		if(!mascarasEnemigosAsignadas)
		{
			for(int i = 0; i<5; i++)
			{
				mascaraEnemigos [i].SetActive(true);	
				mascaraEnemigos [i].GetComponent<Renderer> ().material = matMascaras[Random.Range(1,6)];
				mascarasEnemigosAsignadas = true;
			}
		}
	}

	/// <summary>
	/// asigna las variables que has escogido en el invventario
	/// de los jugadores, color de bolas y mascaras
	/// </summary>
	void asignarVariables ()
	{
		for(int i=0; i < 5; i++)
		{
			if(numeroColorTiendaBolas[i] == 0)
			{
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaAmarilla = true;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaRoja = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaVerde = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaAzul = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaVioleta = false;
			}

			if(numeroColorTiendaBolas[i] == 1)
			{
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaAmarilla = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaRoja = true;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaVerde = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaAzul = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaVioleta = false;
			}

			if(numeroColorTiendaBolas[i] == 2)
			{
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaAmarilla = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaRoja = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaVerde = true;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaAzul = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaVioleta = false;
			}

			if(numeroColorTiendaBolas[i] == 3)
			{
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaAmarilla = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaRoja = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaVerde = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaAzul = true;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaVioleta = false;
			}

			if(numeroColorTiendaBolas[i] == 4)
			{
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaAmarilla = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaRoja = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaVerde = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaAzul = false;
				jugadores[i].GetComponent<codigoJugadorV002>().crearBolaVioleta = true;
			}
		}
	}

	void cargarDatos()
	{
		/*
		fileLoad = new StreamReader("tiendaGuardado.txt");
		dineroTotalTiendaCargado = int.Parse(fileLoad.ReadLine());
		totalBolasAmarillasCargado = int.Parse(fileLoad.ReadLine());
		totalBolasRojasCargado = int.Parse(fileLoad.ReadLine());
		totalBolasVerdesCargado = int.Parse(fileLoad.ReadLine());
		totalBolasAzulesCargado = int.Parse(fileLoad.ReadLine());
		totalBolasVioletasCargado = int.Parse(fileLoad.ReadLine());
		numeroMascaraTiendaCargado = int.Parse(fileLoad.ReadLine());
		numeroPodsTiendaCargado = int.Parse(fileLoad.ReadLine());
		numeroGasTiendaCargado = int.Parse(fileLoad.ReadLine());
		numeroCargadorTiendaCargado = int.Parse(fileLoad.ReadLine());
		fileLoad.Close();
		*/
	}

	void OnGUI()
	{
		/*
		if(GUI.Button(new Rect(20,80,150,50),"cargar variables") && File.Exists("tiendaGuardado.txt"))
		{
			cargarDatos ();
		}

		GUI.BeginGroup (new Rect (410, 20, 200, 400), "");
		GUI.Box (new Rect (0, 0, 200, 400), "datos cargados");
		GUI.Label (new Rect (10, 20, 200, 20),"dinero = " +  dineroTotalTiendaCargado.ToString());
		GUI.Label (new Rect (10, 50, 200, 20),"bolas amarillas = " +  totalBolasAmarillasCargado.ToString());
		GUI.Label (new Rect (10, 80, 200, 20),"bolas rojas = " +  totalBolasRojasCargado.ToString());
		GUI.Label (new Rect (10, 110, 200, 20),"bolas verdes = " +  totalBolasVerdesCargado.ToString());
		GUI.Label (new Rect (10, 140, 200, 20),"bolas azules = " +  totalBolasAzulesCargado.ToString());
		GUI.Label (new Rect (10, 170, 200, 20),"bolas violetas = " +  totalBolasVioletasCargado.ToString());
		GUI.Label (new Rect (10, 200, 200, 20),"mascara elegidas = " +  numeroMascaraTiendaCargado.ToString());
		GUI.Label (new Rect (10, 230, 200, 20),"numero de podss = " +  numeroPodsTiendaCargado.ToString());
		GUI.Label (new Rect (10, 260, 200, 20),"numero de botella = " +  numeroGasTiendaCargado.ToString());
		GUI.Label (new Rect (10, 290, 200, 20),"cargador elegido = " +  numeroCargadorTiendaCargado.ToString());
		GUI.EndGroup();
		*/
	}
}
