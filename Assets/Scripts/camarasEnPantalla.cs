using UnityEngine;
using System.Collections;

public class camarasEnPantalla : MonoBehaviour {

	public GameObject scriptCamaras;

	public GUISkin botonesInterface;

	public Texture2D botonCamara;
	public Texture2D selecCamaras;
	public Texture2D botonOcultarMecos;

	public Camera camaraUno;
	public Camera camaraDos;
	public Camera camaraTres;
	public Camera camaraCuatro;
	public Camera camaraCinco;
	public Camera camaraSeis;
	public Camera camaraSiete;
	public Camera camaraOcho;

	public Camera camaraBuenosStand;
	public Camera camaraMalosStand;

	public Camera camaraVictoriaDerrota;

	private string nombreCamara;

	public int camaraY;
	public float tiempoMiBoton;

	public bool camaraInicial;
	public bool ocultarSeleccionado;

	// Use this for initialization
	void Awake () 
	{

		camaraUno.enabled = false;
		camaraDos.enabled = false;
		camaraTres.enabled = false;
		camaraCuatro.enabled = false;
		camaraCinco.enabled = false;
		camaraSeis.enabled = false;
		camaraSiete.enabled = true;
		camaraOcho.enabled = false;

		camaraBuenosStand.enabled = false;
		camaraMalosStand.enabled = false;

		camaraVictoriaDerrota.enabled = false;

		scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraSeleccionada = 6;

	}
	
	// Update is called once per frame
	void Update () 
	{
			// print (Screen.height);
			// print("Camara en uso : " + nombreCamara);
		tiempoBoton ();
	}

	public void tiempoBoton()
	{
		tiempoMiBoton = tiempoMiBoton + 1 * Time.deltaTime;

		if(tiempoMiBoton > 5)
		{
			tiempoMiBoton = 5;
		}
	}
	
	public void activarFaseUno()
	{
		camaraUno.enabled = false;
		camaraDos.enabled = false;
		camaraTres.enabled = false;
		camaraCuatro.enabled = false;
		camaraCinco.enabled = false;
		camaraSeis.enabled = false;
		camaraSiete.enabled = true;
		camaraOcho.enabled = false;
		
		camaraBuenosStand.enabled = false;
		camaraMalosStand.enabled = false;
		camaraVictoriaDerrota.enabled = false;
		
		scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraSeleccionada = 6;
	}

	/// <summary>
	/// activar fase de juego
	/// </summary>
	public void activarFaseDos()
	{
		if(!camaraInicial)
		{
			camaraInicial = true;
			camaraY = 192;
		}
	}

	/// <summary>
	/// se activa la camara del stand de los jugadores
	/// </summary>
	public void activarStandBuenos()
	{
		camaraUno.enabled = false;
		camaraDos.enabled = false;
		camaraTres.enabled = false;
		camaraCuatro.enabled = false;
		camaraCinco.enabled = false;
		camaraSeis.enabled = false;
		camaraSiete.enabled = false;
		camaraOcho.enabled = false;
		camaraBuenosStand.enabled = true;
		camaraMalosStand.enabled = false;
		camaraVictoriaDerrota.enabled = false;
	}

	/// <summary>
	/// se activa la camara del stand de los enemigos
	/// </summary>
	public void activarStandMalos()
	{
		camaraUno.enabled = false;
		camaraDos.enabled = false;
		camaraTres.enabled = false;
		camaraCuatro.enabled = false;
		camaraCinco.enabled = false;
		camaraSeis.enabled = false;
		camaraSiete.enabled = false;
		camaraOcho.enabled = false;
		camaraBuenosStand.enabled = false;
		camaraMalosStand.enabled = true;
		camaraVictoriaDerrota.enabled = false;
	}

	/// <summary>
	/// se activa la camara de la vistoria
	/// </summary>
	public void activarCamaraVictoria()
	{
		camaraUno.enabled = false;
		camaraDos.enabled = false;
		camaraTres.enabled = false;
		camaraCuatro.enabled = false;
		camaraCinco.enabled = false;
		camaraSeis.enabled = false;
		camaraSiete.enabled = false;
		camaraOcho.enabled = false;
		camaraBuenosStand.enabled = false;
		camaraMalosStand.enabled = false;
		camaraVictoriaDerrota.enabled = true;
	}


	/// <summary>
	/// interface de botones
	/// botones de las camaras que segun pulses se activa la camara
	/// </summary>
	void OnGUI()
	{
		GUI.skin = botonesInterface;	// mi estilo de botones

		if(scriptCamaras.GetComponent<propiedadesGamev002>().fasesDelJuego == 2 && !scriptCamaras.GetComponent<propiedadesGamev002>().pausaActivada)
		{
			activarFaseDos();

		// ocultar los halos de  colores de los jugadores
		if(GUI.Button(new Rect(5,5,30,30),botonOcultarMecos,"ocultarHalos"))
		{
			if(ocultarSeleccionado == false && tiempoMiBoton > 0.25)
			{
				tiempoMiBoton = 0;
				ocultarSeleccionado = true;
			}

			if(ocultarSeleccionado == true && tiempoMiBoton > 0.25)
			{
				tiempoMiBoton = 0;
				ocultarSeleccionado = false;
			}
		}

		if(ocultarSeleccionado)
		{
			GUI.Label(new Rect(3,1,40,40),selecCamaras);
		}

		if(GUI.Button(new Rect(Screen.width - 34,Screen.height - (Screen.height - 5),30,30),botonCamara,"botonesInterfaceCamaras") || Input.GetKey(KeyCode.Alpha1))
		{
			nombreCamara = camaraUno.name;
			camaraY = 0;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraSeleccionada = 0;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZ[0] = scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZiniciales[0];
			camaraUno.enabled = true;
			camaraDos.enabled = false;
			camaraTres.enabled = false;
			camaraCuatro.enabled = false;
			camaraCinco.enabled = false;
			camaraSeis.enabled = false;
			camaraSiete.enabled = false;
			camaraOcho.enabled = false;
		}
		GUI.Label(new Rect(Screen.width - 33,Screen.height - (Screen.height - 22),50,50),"x1","numeroCamaras");

		if(GUI.Button(new Rect(Screen.width - 34,Screen.height - (Screen.height - 37),30,30),botonCamara,"botonesInterfaceCamaras") || Input.GetKey(KeyCode.Alpha2))
		{
			nombreCamara = camaraDos.name;
			camaraY = 32;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraSeleccionada = 1;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZ[1] = scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZiniciales[1];
			camaraUno.enabled = false;
			camaraDos.enabled = true;
			camaraTres.enabled = false;
			camaraCuatro.enabled = false;
			camaraCinco.enabled = false;
			camaraSeis.enabled = false;
			camaraSiete.enabled = false;
			camaraOcho.enabled = false;
		}
		GUI.Label(new Rect(Screen.width - 33,Screen.height - (Screen.height - 54),50,50),"x2","numeroCamaras");

		if(GUI.Button(new Rect(Screen.width - 34,Screen.height - (Screen.height - 69),30,30),botonCamara,"botonesInterfaceCamaras") || Input.GetKey(KeyCode.Alpha3))
		{
			nombreCamara = camaraTres.name;
			camaraY = 64;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraSeleccionada = 2;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZ[2] = scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZiniciales[2];
			camaraUno.enabled = false;
			camaraDos.enabled = false;
			camaraTres.enabled = true;
			camaraCuatro.enabled = false;
			camaraCinco.enabled = false;
			camaraSeis.enabled = false;
			camaraSiete.enabled = false;
			camaraOcho.enabled = false;
		}
		GUI.Label(new Rect(Screen.width - 33,Screen.height - (Screen.height - 86),50,50),"x3","numeroCamaras");

		if(GUI.Button(new Rect(Screen.width - 34,Screen.height - (Screen.height - 101),30,30),botonCamara,"botonesInterfaceCamaras") || Input.GetKey(KeyCode.Alpha4))
		{
			nombreCamara = camaraCuatro.name;
			camaraY = 96;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraSeleccionada = 3;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZ[3] = scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZiniciales[3];
			camaraUno.enabled = false;
			camaraDos.enabled = false;
			camaraTres.enabled = false;
			camaraCuatro.enabled = true;
			camaraCinco.enabled = false;
			camaraSeis.enabled = false;
			camaraSiete.enabled = false;
			camaraOcho.enabled = false;
		}
		GUI.Label(new Rect(Screen.width - 33,Screen.height - (Screen.height - 118),50,50),"x4","numeroCamaras");

		if(GUI.Button(new Rect(Screen.width - 34,Screen.height - (Screen.height - 133),30,30),botonCamara,"botonesInterfaceCamaras") || Input.GetKey(KeyCode.Alpha5))
		{
			nombreCamara = camaraCinco.name;
			camaraY = 128;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraSeleccionada = 4;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZ[4] = scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZiniciales[4];
			camaraUno.enabled = false;
			camaraDos.enabled = false;
			camaraTres.enabled = false;
			camaraCuatro.enabled = false;
			camaraCinco.enabled = true;
			camaraSeis.enabled = false;
			camaraSiete.enabled = false;
			camaraOcho.enabled = false;
		}
		GUI.Label(new Rect(Screen.width - 33,Screen.height - (Screen.height - 150),50,50),"x5","numeroCamaras");

		if(GUI.Button(new Rect(Screen.width - 34,Screen.height - (Screen.height - 165),30,30),botonCamara,"botonesInterfaceCamaras") || Input.GetKey(KeyCode.Alpha6))
		{
			nombreCamara = camaraSeis.name;
			camaraY = 160;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraSeleccionada = 5;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZ[5] = scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZiniciales[5];
			camaraUno.enabled = false;
			camaraDos.enabled = false;
			camaraTres.enabled = false;
			camaraCuatro.enabled = false;
			camaraCinco.enabled = false;
			camaraSeis.enabled = true;
			camaraSiete.enabled = false;
			camaraOcho.enabled = false;
		}
		GUI.Label(new Rect(Screen.width - 33,Screen.height - (Screen.height - 182),50,50),"x6","numeroCamaras");

		if(GUI.Button(new Rect(Screen.width - 34,Screen.height - (Screen.height - 197),30,30),botonCamara,"botonesInterfaceCamaras") || Input.GetKey(KeyCode.Alpha7))
		{
			nombreCamara = camaraSiete.name;
			camaraY = 192;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraSeleccionada = 6;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZ[6] = scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZiniciales[6];
			camaraUno.enabled = false;
			camaraDos.enabled = false;
			camaraTres.enabled = false;
			camaraCuatro.enabled = false;
			camaraCinco.enabled = false;
			camaraSeis.enabled = false;
			camaraSiete.enabled = true;
			camaraOcho.enabled = false;
		}
		GUI.Label(new Rect(Screen.width - 33,Screen.height - (Screen.height - 214),50,50),"x7","numeroCamaras");

		if(GUI.Button(new Rect(Screen.width - 34,Screen.height - (Screen.height - 229),30,30),botonCamara,"botonesInterfaceCamaras") || Input.GetKey(KeyCode.Alpha8))
		{
			nombreCamara = camaraSiete.name;
			camaraY = 224;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraSeleccionada = 7;
			scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZ[7] = scriptCamaras.GetComponent<moverEscenarioCamara> ().camaraXYZiniciales[7];
			camaraUno.enabled = false;
			camaraDos.enabled = false;
			camaraTres.enabled = false;
			camaraCuatro.enabled = false;
			camaraCinco.enabled = false;
			camaraSeis.enabled = false;
			camaraSiete.enabled = false;
			camaraOcho.enabled = true;
		}
		GUI.Label(new Rect(Screen.width - 33,Screen.height - (Screen.height - 246),50,50),"x8","numeroCamaras");

		GUI.Label(new Rect(Screen.width - 36,Screen.height - (Screen.height - camaraY),40,40),selecCamaras);

		}
	}
}
