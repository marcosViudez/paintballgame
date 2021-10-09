using UnityEngine;
using System.Collections;
using System.IO;

public class salvarguardar : MonoBehaviour {

	public Vector3 posicionMeco;
	public int vidaMeco;
	public int cargadores;
	public bool estoyVivo;
	public int vivo;

	public int vidaMecoCargado;
	public int cargadoresCargado;
	public bool estoyVivoCargado;
	public int vivoCargado;

	// Use this for initialization
	void Start () 
	{
		estoyVivo = true;
		cargadores = 10;
		vidaMeco = 100;
		posicionMeco = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		posicionMeco = transform.position;
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(20,20,100,40),"Guardar"))
		{
			// salvarDatos();
			salvarFile();
		}

		if(GUI.Button(new Rect(20,80,100,40),"Cargar"))
		{
			// cargarDatos();
			cargarFile();
		}
	}

	void salvarFile()
	{
		StreamWriter file = new StreamWriter(Application.dataPath + "perro.txt");
		file.WriteLine (vidaMeco);
		file.WriteLine (estoyVivo);
		file.WriteLine (cargadores);
		file.Close ();
	}

	void cargarFile()
	{
		StreamReader file =  new StreamReader("Assetsperro.txt");
		vidaMecoCargado = int.Parse(file.ReadLine ());
		estoyVivoCargado = bool.Parse(file.ReadLine ());
		cargadoresCargado = int.Parse(file.ReadLine ());
		file.Close ();
	}
	/*
	void salvarDatos()
	{
		PlayerPrefs.SetFloat ("posicionx", posicionMeco.x);
		PlayerPrefs.SetFloat ("posiciony", posicionMeco.y);
		PlayerPrefs.SetFloat ("posicionz", posicionMeco.z);
		if(estoyVivo)
		{
			vivo = 1;
			PlayerPrefs.SetInt("estoyVivo", vivo);
		}else{
			vivo = 0;
			PlayerPrefs.SetInt("estoyVivo", vivo);
		}
		PlayerPrefs.SetInt("vida", vidaMeco);
	}

	void cargarDatos()
	{
		if(PlayerPrefs.GetInt("estoyVivo") == 1)
		{
			estoyVivo = true;
		}else{
			estoyVivo = false;
		}

		vidaMeco = PlayerPrefs.GetInt("vida");

		posicionMeco.x = PlayerPrefs.GetFloat ("posicionx");
		posicionMeco.y = PlayerPrefs.GetFloat ("posiciony");
		posicionMeco.z = PlayerPrefs.GetFloat ("posicionz");
		Vector3 miposicion = new Vector3 (posicionMeco.x, posicionMeco.y, posicionMeco.z);
		transform.position = miposicion;

	}
	*/
}
