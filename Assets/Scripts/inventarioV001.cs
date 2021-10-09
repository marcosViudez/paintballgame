using UnityEngine;
using System.Collections;
using System.IO;

public class inventarioV001 : MonoBehaviour {

	public GUISkin celdasV001;

	public Texture2D fondoPantalla;
	public Texture2D casillaVacia;
	public Texture2D dineroTextura;
	public Texture2D cajasBolasAmarillas;
	public Texture2D cajasBolasRojas;
	public Texture2D cajasBolasVerdes;
	public Texture2D cajasBolasAzules;
	public Texture2D cajasBolasVioletas;

	public Texture2D[] mascarasTienda = new Texture2D[6]; 

	// variables a guardar con playerprefs o archivo .dat
	private int dineroTotalTienda;
	private int totalBolasAmarillas;
	private int totalBolasRojas;
	private int totalBolasVerdes;
	private int totalBolasAzules;
	private int totalBolasVioletas;
	private int numeroMascaraTienda;
	private int numeroPodsTienda;
	private int numeroGasTienda;
	private int numeroCargadorTienda;

	// variables cargadas desde el archivo
	private int dineroTotalTiendaCargado;
	private int totalBolasAmarillasCargado;
	private int totalBolasRojasCargado;
	private int totalBolasVerdesCargado;
	private int totalBolasAzulesCargado;
	private int totalBolasVioletasCargado;
	private int numeroMascaraTiendaCargado;
	private int numeroPodsTiendaCargado;
	private int numeroGasTiendaCargado;
	private int numeroCargadorTiendaCargado;
	// ---------------------------------------------------

	private StreamWriter fileSave;
	private StreamReader fileLoad;

	public int x;

	private Vector2 scrollPosition = Vector2.zero;

	// Use this for initialization
	void Start () 
	{
		dineroTotalTienda = 0;
		totalBolasAmarillas = 0;
		totalBolasRojas = 0;
		totalBolasVerdes = 0;
		totalBolasAzules = 200;
		totalBolasVioletas = 0;
		numeroMascaraTienda = 0;
		numeroPodsTienda = 0;
		numeroGasTienda = 0;
		numeroCargadorTienda = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI()
	{
		GUI.skin = celdasV001;	// mi estilo de botones

		if(GUI.Button(new Rect(20,20,150,50),"guardar variables"))
		{
			fileSave = new StreamWriter("tiendaGuardado.txt");
				fileSave.WriteLine(dineroTotalTienda);
				fileSave.WriteLine(totalBolasAmarillas);
				fileSave.WriteLine(totalBolasRojas);
				fileSave.WriteLine(totalBolasVerdes);
				fileSave.WriteLine(totalBolasAzules);
				fileSave.WriteLine(totalBolasVioletas);
				fileSave.WriteLine(numeroMascaraTienda);
				fileSave.WriteLine(numeroPodsTienda);
				fileSave.WriteLine(numeroGasTienda);
				fileSave.WriteLine(numeroCargadorTienda);
			fileSave.Close();
		}

		if(GUI.Button(new Rect(20,80,150,50),"cargar variables") && File.Exists("tiendaGuardado.txt"))
		{
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
		}

		if(!File.Exists("tiendaGuardado.txt"))
		{
			print ("no hay partida guardada");
		}

		// columna de botones para inventario
		GUI.Box (new Rect (700, 300, 120, 100),"");
		scrollPosition = GUI.BeginScrollView (new Rect (700,300, 116, 100), scrollPosition, new Rect (700, 300, 60, 500));
			GUI.Button (new Rect (710, 300, 80, 80),cajasBolasAmarillas);		// botones con textura
			GUI.Button (new Rect (710, 390, 80, 80),cajasBolasRojas);
			GUI.Button (new Rect (710, 480, 80, 80),cajasBolasVerdes);
			GUI.Button (new Rect (710, 570, 80, 80),cajasBolasAzules);
			GUI.Button (new Rect (710, 660, 80, 80),cajasBolasVioletas);
		GUI.EndScrollView ();

		GUI.BeginGroup (new Rect (200, 20, 200, 400), "");
			GUI.Box (new Rect (0, 0, 200, 400), "datos a guardar");
			GUI.Label (new Rect (10, 20, 200, 20),"dinero = " +  dineroTotalTienda.ToString());
			GUI.Label (new Rect (10, 50, 200, 20),"bolas amarillas = " +  totalBolasAmarillas.ToString());
			GUI.Label (new Rect (10, 80, 200, 20),"bolas rojas = " +  totalBolasRojas.ToString());
			GUI.Label (new Rect (10, 110, 200, 20),"bolas verdes = " +  totalBolasVerdes.ToString());
			GUI.Label (new Rect (10, 140, 200, 20),"bolas azules = " +  totalBolasAzules.ToString());
			GUI.Label (new Rect (10, 170, 200, 20),"bolas violetas = " +  totalBolasVioletas.ToString());
			GUI.Label (new Rect (10, 200, 200, 20),"mascara elegida = " +  numeroMascaraTienda.ToString());
			GUI.Label (new Rect (10, 230, 200, 20),"numero de pods = " +  numeroPodsTienda.ToString());
			GUI.Label (new Rect (10, 260, 200, 20),"numero de botella = " +  numeroGasTienda.ToString());
			GUI.Label (new Rect (10, 290, 200, 20),"cargador elegido = " +  numeroCargadorTienda.ToString());
		GUI.EndGroup();

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
	}
}
