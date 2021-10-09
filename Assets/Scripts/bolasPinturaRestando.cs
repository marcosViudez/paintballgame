using UnityEngine;
using System.Collections;

public class bolasPinturaRestando : MonoBehaviour {

	public GUISkin botonesInterface;	

	public GameObject scriptMeco;
	public GameObject scriptMarcador;

	public Texture2D seleccionInterface;
	public Texture2D seleccionInterfaceEliminado;
	public Texture2D dineroTotal;

	private int bolasEnPods = 20;					// bolas que tenemos en los pods de la espalda
	private int bolasIniciales = 200;				// bolas max que tenemos en el cargador de la marcadora
	private int bolasInicialesEnMarcadora = 50;		// bolas iniciales en marcadora

	public int numero;
	public int jugador;
	public int monedasTotal; 					
	public int incrementoY;					
	public int coorX;
	public int coorY;

	public bool metodoCargadores;
	public bool vidaDesplegada = true;
	public bool[] interfaceSeleccionada = new bool[5];
	public int[] numerobolas = new int[5];
	
	public Rigidbody podsTirados;
	
	[System.Serializable]
	public class cosas
	{
	public string nombreJugador;
	public bool estoyEliminado;
	public bool tirarPods = false;
	public bool tiroMiPod;

	public GameObject[] podsEspalda = new GameObject[6];
	public GameObject podCreate;

	public Texture2D texturaFoto;					// imagen de interface de vidas
	public Texture2D barraVerdeCO2;
	public Texture2D barraRojaCO2;
	public Texture2D barraAmarillaBolas;
	public Texture2D barraNegraBolas; 
	public Texture2D cloneColor;
	
	public int cantidadPods = 5; // pods espalda
	public int numeroBolasCargador;
	public int numeroPods;
	public int[] bolasCargador = new int[5];
	public int numeroCargadorUsado = 4;
	public int numeroBolasAmarillas;
	public int numeroBolasRojas;
	public int numeroBolasAzules;
	public int numeroBolasVerdes;
	public int numeroBolasVioletas;
	public int bolasBucle;
	public int gasBucle;
	public int maxCO2 = 54;		// barras de energia
	public int maxBolas = -32;	// barras de energia
	}

	public int x;
	public int y;
	public int z;
	public int t;

	public cosas[] menuVidaSegunda = new cosas[5];
	

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i<5; i++)
		{
			for(int j = 0; j<6; j++)
			{
				menuVidaSegunda[i].numeroBolasCargador = bolasInicialesEnMarcadora;
				menuVidaSegunda[i].bolasCargador[j] = bolasEnPods;
				menuVidaSegunda[i].numeroPods = 6;
				menuVidaSegunda[i].maxCO2 = 54;
				menuVidaSegunda[i].maxBolas = -32;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		StartCoroutine(escondiendoVidas());
		limites ();
		creandoPods ();

		if(metodoCargadores)
		{
			comprobarPodsCargadores();
		}

		menuVidaSegunda[0].maxBolas = -(menuVidaSegunda [0].numeroBolasCargador/6);
		menuVidaSegunda[1].maxBolas = -(menuVidaSegunda [1].numeroBolasCargador/6);
		menuVidaSegunda[2].maxBolas = -(menuVidaSegunda [2].numeroBolasCargador/6);
		menuVidaSegunda[3].maxBolas = -(menuVidaSegunda [3].numeroBolasCargador/6);
		menuVidaSegunda[4].maxBolas = -(menuVidaSegunda [4].numeroBolasCargador/6);
	}

	/// <summary>
	/// Resta las bolas de pintura de la interface
	/// </summary>
	/// <param name="numeroInterface"> el numero de la interface del cada jugador</param>
    public void restarBolas(int numeroInterface)
	{
		if(menuVidaSegunda[numeroInterface].numeroBolasCargador > 0)
		{
			menuVidaSegunda [numeroInterface].numeroBolasCargador--;
			menuVidaSegunda[numeroInterface].bolasBucle = menuVidaSegunda[numeroInterface].bolasBucle + 1;
			menuVidaSegunda[numeroInterface].gasBucle = menuVidaSegunda[numeroInterface].gasBucle + 1;
		}
	}

	/// <summary>
	/// Comprueba las bolas que hay en los pods y segun tenga suma, añade
	/// si esta lleno no te suma, si le queda poco para llenar lo completa.
	/// </summary>
	void comprobarPodsCargadores()
	{
		metodoCargadores = false;

		// el 0 es el jugador hay que cambiarlo por el "numero"
		if(menuVidaSegunda[jugador].bolasCargador[menuVidaSegunda[jugador].numeroPods-1] > 0)
		{
			var suma = menuVidaSegunda[jugador].numeroBolasCargador + menuVidaSegunda[jugador].bolasCargador[menuVidaSegunda[jugador].numeroCargadorUsado];
			
			if(suma > bolasIniciales)
			{	
				menuVidaSegunda[jugador].bolasCargador[menuVidaSegunda[jugador].numeroCargadorUsado] = menuVidaSegunda[jugador].bolasCargador[menuVidaSegunda[jugador].numeroCargadorUsado] - (bolasIniciales - menuVidaSegunda[jugador].numeroBolasCargador);
				menuVidaSegunda[jugador].numeroBolasCargador = (bolasIniciales - menuVidaSegunda[jugador].numeroBolasCargador) + menuVidaSegunda[jugador].numeroBolasCargador;
			}
			
			if(suma <= bolasIniciales)
			{
				menuVidaSegunda[jugador].numeroBolasCargador = menuVidaSegunda[jugador].numeroBolasCargador + menuVidaSegunda[jugador].bolasCargador[menuVidaSegunda[jugador].numeroCargadorUsado];
				menuVidaSegunda[jugador].bolasCargador[menuVidaSegunda[jugador].numeroCargadorUsado] = 0;
				menuVidaSegunda[jugador].tirarPods = true;
				menuVidaSegunda[jugador].numeroCargadorUsado--;
				menuVidaSegunda[jugador].numeroPods--;
				// print(menuVidaSegunda[jugador].numeroCargadorUsado);
			}
		}
	}

	/// <summary>
	/// Crea un pods instaciando prefab cuando cargas y lo tira al suelo
	/// </summary>
	void creandoPods()
	{	
		// introducir una variable de eliminacion de pods
		if(menuVidaSegunda[jugador].tirarPods && menuVidaSegunda[jugador].cantidadPods != 0)
		{
			Instantiate(podsTirados, menuVidaSegunda[jugador].podCreate.transform.position, menuVidaSegunda[jugador].podCreate.transform.rotation);
			menuVidaSegunda[jugador].tirarPods = false;
			podsTirados.AddForce(new Vector3(0,0,10));
			podsTirados.AddTorque(10,50,20);
			if(menuVidaSegunda[jugador].cantidadPods > 0 && !menuVidaSegunda[jugador].tirarPods)
			{
				menuVidaSegunda[jugador].cantidadPods = menuVidaSegunda[jugador].cantidadPods - 1;
			}
			// estadosYCursores.GetComponent<bolasPinturaRestando>().menuVidaSegunda[jugadorSeleccionadoPods].podsEspalda[cantidadPods].SetActive(false);
			menuVidaSegunda[jugador].podsEspalda[menuVidaSegunda[jugador].cantidadPods].SetActive(false);
			//portaPods[cantidadPods].SetActive(false);		// eliminamos un pod solo de la espalda	
			menuVidaSegunda[jugador].tiroMiPod = false;
		}	
	}

	/// <summary>
	/// Limites de las interfaces de los jugadores, barras de vida etc.
	/// Si se pasan de un valor determinado se mantienen o vuelven a cero
	/// </summary>
	void limites()
	{	
		for(int j = 0; j<5; j++)
		{	
			if(menuVidaSegunda[j].numeroBolasCargador < 1 && menuVidaSegunda[j].numeroPods >= 0)
			{
				// cuando no hay mas cargadores
				menuVidaSegunda[j].numeroBolasCargador = 0;
			}
			
			// control del co2 barra azul
			if(menuVidaSegunda[j].maxCO2 < 0)
			{
				menuVidaSegunda[j].maxCO2 = 0;
			}
			
			if(menuVidaSegunda[j].maxCO2 > 54)
			{
				menuVidaSegunda[j].maxCO2 = 54;
			}
			
			if(menuVidaSegunda[j].gasBucle > 15 && menuVidaSegunda[j].maxCO2 > 0)
			{
				menuVidaSegunda[j].gasBucle = 0;
				menuVidaSegunda[j].maxCO2 = menuVidaSegunda[j].maxCO2 - 1;
			}
			
			// control de la barra de bolas amarilla
			if(menuVidaSegunda[j].maxBolas > 0)
			{
				menuVidaSegunda[j].maxBolas = 0;
			}
			// conversion de bolas y gas para ir restando
			if(menuVidaSegunda[j].bolasBucle > 6 && menuVidaSegunda[j].maxBolas < 0)
			{
				menuVidaSegunda[j].bolasBucle = 0;
				menuVidaSegunda[j].maxBolas = menuVidaSegunda[j].maxBolas + 1;
			}
		}
	}

	/// <summary>
	/// Con la tecla espacio ocultamos o mostramos las interfaces de los jugadores
	/// </summary>
	IEnumerator escondiendoVidas()
	{
		if(Input.GetKeyDown("space") && !vidaDesplegada)
		{
			incrementoY = 0;	// esconde las interfaces de vida de los jugadores
			yield return new WaitForSeconds(0.1f * Time.deltaTime);
			vidaDesplegada = true;
			// scriptMarcador.GetComponent<cursoresSobrePantalla>().marcadorActivado = true;
		}
		
		if(Input.GetKeyDown("space") && vidaDesplegada)
		{
			incrementoY = 85;	// esconde las interfaces de vida de los jugadores
			yield return new WaitForSeconds(0.1f * Time.deltaTime);
			vidaDesplegada = false;
			// scriptMarcador.GetComponent<cursoresSobrePantalla>().marcadorActivado = false;
		}
	}
	
	/// <summary>
	/// Colocacion de la interface de los jugadores
	/// Posicionado de los elementos de la interface
	/// </summary>
	void OnGUI()
	{

		GUI.skin = botonesInterface;			// estilo de los botones de la interface
		
		// GUI.Label(Rect(0,0,50,50),dineroTotal);			// dinero total del equipo icono
		// GUI.Label(Rect(40,14,30,30),monedasTotal.ToString(),"monedas");  // dinero total del equipo en digitos
		
		// jugador 00
		GUI.DrawTexture(new Rect((Screen.width/2) - 232,Screen.height - 32 + incrementoY,54,27),menuVidaSegunda[0].barraRojaCO2,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) - 232,Screen.height - 32 + incrementoY,menuVidaSegunda[0].maxCO2,27),menuVidaSegunda[0].barraVerdeCO2,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) - 232,Screen.height - 40 + incrementoY,54,-32),menuVidaSegunda[0].barraNegraBolas,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) - 232,Screen.height - 40 + incrementoY,54,menuVidaSegunda[0].maxBolas),menuVidaSegunda[0].barraAmarillaBolas,ScaleMode.StretchToFill,true,1.0f);
		GUI.Label(new Rect((Screen.width/2) - 272.5f, Screen.height - 105 + incrementoY,105,105),menuVidaSegunda[0].texturaFoto);
		GUI.Label(new Rect((Screen.width/2) - 272.5f, Screen.height - 104 + incrementoY,99,76),menuVidaSegunda[0].cloneColor);
		GUI.Label(new Rect((Screen.width/2) - 272.5f, Screen.height - 104 + incrementoY,99,76)," clone 01 ");
		GUI.Label(new Rect((Screen.width/2) - 201,Screen.height - 43 + incrementoY,30,30),menuVidaSegunda[0].numeroBolasCargador.ToString(),"numeroBolasTexto");    
		GUI.Label(new Rect((Screen.width/2) - 252,Screen.height - 49 + incrementoY,30,30),"x" + menuVidaSegunda[0].numeroPods.ToString(),"numeroPods"); 
		
		// jugador 01
		GUI.DrawTexture(new Rect((Screen.width/2) - 123,Screen.height - 32 + incrementoY,54,27),menuVidaSegunda[1].barraRojaCO2,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) - 123,Screen.height - 32 + incrementoY,menuVidaSegunda[1].maxCO2,27),menuVidaSegunda[1].barraVerdeCO2,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) - 123,Screen.height - 40 + incrementoY,54,-32),menuVidaSegunda[1].barraNegraBolas,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) - 123,Screen.height - 40 + incrementoY,54,menuVidaSegunda[1].maxBolas),menuVidaSegunda[1].barraAmarillaBolas,ScaleMode.StretchToFill,true,1.0f);
		GUI.Label(new Rect((Screen.width/2) - 162.5f, Screen.height - 105 + incrementoY,105,105),menuVidaSegunda[1].texturaFoto);
		GUI.Label(new Rect((Screen.width/2) - 162.5f, Screen.height - 104 + incrementoY,99,76),menuVidaSegunda[1].cloneColor);
		GUI.Label(new Rect((Screen.width/2) - 162.5f, Screen.height - 104 + incrementoY,99,76)," clone 02 ");
		GUI.Label(new Rect((Screen.width/2) - 91,Screen.height - 43 + incrementoY,30,30),menuVidaSegunda[1].numeroBolasCargador.ToString(),"numeroBolasTexto");    
		GUI.Label(new Rect((Screen.width/2) - 141,Screen.height - 49 + incrementoY,30,30),"x" + menuVidaSegunda[1].numeroPods.ToString(),"numeroPods"); 
		
		// jugador 02
		GUI.DrawTexture(new Rect((Screen.width/2) - 14,Screen.height - 32 + incrementoY,54,27),menuVidaSegunda[2].barraRojaCO2,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) - 14,Screen.height - 32 + incrementoY,menuVidaSegunda[2].maxCO2,27),menuVidaSegunda[2].barraVerdeCO2,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) - 14,Screen.height - 40 + incrementoY,54,-32),menuVidaSegunda[2].barraNegraBolas,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) - 14,Screen.height - 40 + incrementoY,54,menuVidaSegunda[2].maxBolas),menuVidaSegunda[2].barraAmarillaBolas,ScaleMode.StretchToFill,true,1.0f);
		GUI.Label(new Rect((Screen.width/2) - 52.5f, Screen.height - 105 + incrementoY,105,105),menuVidaSegunda[2].texturaFoto);
		GUI.Label(new Rect((Screen.width/2) - 52.5f, Screen.height - 104 + incrementoY,99,76),menuVidaSegunda[2].cloneColor);
		GUI.Label(new Rect((Screen.width/2) - 52.5f, Screen.height - 104 + incrementoY,99,76)," clone 03 ");
		GUI.Label(new Rect((Screen.width/2) + 18,Screen.height - 43 + incrementoY,30,30),menuVidaSegunda[2].numeroBolasCargador.ToString(),"numeroBolasTexto");    
		GUI.Label(new Rect((Screen.width/2) - 32,Screen.height - 49 + incrementoY,30,30),"x" + menuVidaSegunda[2].numeroPods.ToString(),"numeroPods"); 
		
		// jugador 03
		GUI.DrawTexture(new Rect((Screen.width/2) + 96,Screen.height - 32 + incrementoY,54,27),menuVidaSegunda[3].barraRojaCO2,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) + 96,Screen.height - 32 + incrementoY,menuVidaSegunda[3].maxCO2,27),menuVidaSegunda[3].barraVerdeCO2,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) + 96,Screen.height - 40 + incrementoY,54,-32),menuVidaSegunda[3].barraNegraBolas,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) + 96,Screen.height - 40 + incrementoY,54,menuVidaSegunda[3].maxBolas),menuVidaSegunda[3].barraAmarillaBolas,ScaleMode.StretchToFill,true,1.0f);
		GUI.Label(new Rect((Screen.width/2) + 57.5f, Screen.height - 105 + incrementoY,105,105),menuVidaSegunda[3].texturaFoto);
		GUI.Label(new Rect((Screen.width/2) + 57.5f, Screen.height - 104 + incrementoY,99,76),menuVidaSegunda[3].cloneColor);
		GUI.Label(new Rect((Screen.width/2) + 57.5f, Screen.height - 104 + incrementoY,99,76)," clone 04 ");
		GUI.Label(new Rect((Screen.width/2) + 128,Screen.height - 43 + incrementoY,30,30),menuVidaSegunda[3].numeroBolasCargador.ToString(),"numeroBolasTexto");    
		GUI.Label(new Rect((Screen.width/2) + 78,Screen.height - 49 + incrementoY,30,30),"x" + menuVidaSegunda[3].numeroPods.ToString(),"numeroPods"); 
		
		// jugador 04
		GUI.DrawTexture(new Rect((Screen.width/2) + 207,Screen.height - 32 + incrementoY,54,27),menuVidaSegunda[4].barraRojaCO2,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) + 207,Screen.height - 32 + incrementoY,menuVidaSegunda[4].maxCO2,27),menuVidaSegunda[4].barraVerdeCO2,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) + 207,Screen.height - 40 + incrementoY,54,-32),menuVidaSegunda[4].barraNegraBolas,ScaleMode.StretchToFill,true,1.0f);
		GUI.DrawTexture(new Rect((Screen.width/2) + 207,Screen.height - 40 + incrementoY,54,menuVidaSegunda[4].maxBolas),menuVidaSegunda[4].barraAmarillaBolas,ScaleMode.StretchToFill,true,1.0f);
		GUI.Label(new Rect((Screen.width/2) + 167.5f, Screen.height - 105 + incrementoY,105,105),menuVidaSegunda[4].texturaFoto);
		GUI.Label(new Rect((Screen.width/2) + 167.5f, Screen.height - 104 + incrementoY,99,76),menuVidaSegunda[4].cloneColor);
		GUI.Label(new Rect((Screen.width/2) + 167.5f, Screen.height - 104 + incrementoY,99,76)," clone 05 ");
		GUI.Label(new Rect((Screen.width/2) + 239,Screen.height - 43 + incrementoY,30,30),menuVidaSegunda[4].numeroBolasCargador.ToString(),"numeroBolasTexto");    
		GUI.Label(new Rect((Screen.width/2) + 188,Screen.height - 49 + incrementoY,30,30),"x" + menuVidaSegunda[4].numeroPods.ToString(),"numeroPods"); 

		// seleccion interfaces cuadro
		if(interfaceSeleccionada[0] == true && scriptMeco.GetComponent<codigoJugadorV002>().estoyVivo)
		{
			GUI.Label(new Rect((Screen.width/2) - 277, Screen.height - 111 + incrementoY,115,115),seleccionInterface);
			jugador = 0;
		}
		if(interfaceSeleccionada[1] == true && scriptMeco.GetComponent<codigoJugadorV002>().estoyVivo)
		{
			GUI.Label(new Rect((Screen.width/2) - 167, Screen.height - 111 + incrementoY,115,115),seleccionInterface);
			jugador = 1;
		}
		if(interfaceSeleccionada[2] == true && scriptMeco.GetComponent<codigoJugadorV002>().estoyVivo)
		{
			GUI.Label(new Rect((Screen.width/2) - 57, Screen.height - 111 + incrementoY,115,115),seleccionInterface);
			jugador = 2;
		}
		if(interfaceSeleccionada[3] == true && scriptMeco.GetComponent<codigoJugadorV002>().estoyVivo)
		{
			GUI.Label(new Rect((Screen.width/2) + 53, Screen.height - 111 + incrementoY,115,115),seleccionInterface);
			jugador = 3;
		}
		if(interfaceSeleccionada[4] == true && scriptMeco.GetComponent<codigoJugadorV002>().estoyVivo)
		{
			GUI.Label(new Rect((Screen.width/2) + 163, Screen.height - 111 + incrementoY,115,115),seleccionInterface);
			jugador = 4;
		}

		// tachando menus de interfaces 
		if(codigoJugadorV002.tocadoJugador[0])
		{
			GUI.Label(new Rect((Screen.width/2) - 277, Screen.height - 111 + incrementoY,115,115),seleccionInterfaceEliminado);
		}
		if(codigoJugadorV002.tocadoJugador[1])
		{
			GUI.Label(new Rect((Screen.width/2) - 167, Screen.height - 111 + incrementoY,115,115),seleccionInterfaceEliminado);
		}
		if(codigoJugadorV002.tocadoJugador[2])
		{
			GUI.Label(new Rect((Screen.width/2) - 57, Screen.height - 111 + incrementoY,115,115),seleccionInterfaceEliminado);
		}
		if(codigoJugadorV002.tocadoJugador[3])
		{
			GUI.Label(new Rect((Screen.width/2) + 53, Screen.height - 111 + incrementoY,115,115),seleccionInterfaceEliminado);
		}
		if(codigoJugadorV002.tocadoJugador[4])
		{
			GUI.Label(new Rect((Screen.width/2) + 163, Screen.height - 111 + incrementoY,115,115),seleccionInterfaceEliminado);
		}
	
	}
}

