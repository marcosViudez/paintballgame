using UnityEngine;
using System.Collections;

public class propiedadesGamev002 : MonoBehaviour {
	
	public GameObject scriptCamarasPantalla;
	public Canvas menuIntermedio;
	public AudioClip bocinaComienzo;
	public AudioClip cargarUno;
	public AudioClip cargarDos;
	public AudioClip cargarTres;
	// public AudioClip bandaSonora;

	public GameObject[] jugadores = new GameObject[5];					// lista de jugadores
	public GameObject[] enemigos = new GameObject[5];					// lista de enemigos
	public GameObject[] halosTeletransporte = new GameObject[5];		// lista de halos de transporte

	public Transform[] cargandoStand = new Transform[5];
	public Transform[] cargandoStandMalos = new Transform[5];
	public Transform[] muerteStandJugadores = new Transform[5];
	public Transform[] muerteStandEnemigos = new Transform[5];
	public Transform[] inicioStandJugadores = new Transform[5];
	public Transform[] inicioStandEnemigos = new Transform[5];
	public Transform[] waypointsVictorias = new Transform[5];
	public Transform[] waypointsDerrotas = new Transform[5];

	public bool[] jugadoresMuertos = new bool[5];
	public bool[] enemigosMuertos = new bool[5];
	public bool[] animandoJugadores = new bool[5];
	public bool[] animandoEnemigos = new bool[5];
	public bool[] halosTeletransporteCreados = new bool[5];
	public bool[] distanciaProhibidaJugadores = new bool[5];
	public bool[] enCasaMuerto = new bool[5];
	public bool[] malosTocados = new bool[5];
	public bool[] enemigoTeletransportado = new bool[5];
	public bool[] sonidoCargando = new bool[5];
	public bool[] sonidoCargandoEnemigos = new bool[5];

	public int[] frameAnimacion01 = new int[5];
	public int[] frameAnimacion01Malos = new int[5];

	public string[] animacionSeleccionada01 = new string[5];
	public string[] animacionSeleccionada01Malos = new string[5];

	public Texture2D juegoPausado;										// textura de pausa
	public Texture2D juegoFire;											// textura de fire !!!
	public Texture2D juegoWin;	
	public Texture2D juegoLose;	
	public Texture2D juegoEmpate;	

	public bool pausaActivada = false;
	public bool activarFire = false;
	public bool dispararImagen = false;
	public bool teclaEscapeActivada = false;
	public bool ganaste = false;
	public bool perdiste = false;
	public bool empate = false;
	public bool tocandoBocina = false;

	public int tiempoPausa;
	public int fasesDelJuego = 0;

	public Quaternion angulo;

	public float tiempoDescansando;

	public int[] ptosEquipoJugadores = new int[5];
	public int[] ptosEquipoEnemigos = new int[5];
	public int totalPtosJugadores;
	public int totalPtosEnemigos;

	public float[] animacionSonido = new float[5];
	public float[] animacionSonidoEnemigos = new float[5];

	// Use this for initialization
    void Awake () 
	{
		// GetComponent<AudioSource>().playOnAwake = bandaSonora;		// ejecuta la BSO todo el rato

		fasesDelJuego = 0;
		angulo.y = 1;
		tiempoDescansando = 0;
		pausaActivada = false;
		activarFire = false;
		dispararImagen = false;
		teclaEscapeActivada = false;
		ganaste = false;
		perdiste = false;
		empate = false;

		for(int i=0;i<5;i++)
		{
			animacionSeleccionada01[i] = "standCargandoCosas";
			animacionSeleccionada01Malos[i] = "standCargandoCosas";
			malosTocados[i] = false;
			enemigoTeletransportado[i] = false;
			frameAnimacion01[i] = 0;
			frameAnimacion01Malos[i] = 0;
			animandoJugadores[i] =false;
			animandoEnemigos[i] = false;
			codigoJugadorV002.tocadoJugador[i] = false;

			// iniciamos variables para cargar fase
			jugadoresMuertos[i] = false;
			enemigosMuertos[i] = false;
			enCasaMuerto[i] = false;
			malosTocados[i] = false;
			jugadores[i].tag = "player";

		}

	}

	// Update is called once per frame
	void Update () 
	{

		fasesPaintball();									// fases del juego
		teclaEscape();										// al pulsar tecla escape sale del juego
		pausaDelJuego();									// pausa del juego 
		activarPausa();										// metodo para activar la pausa del juego
		siJugadoresEstanMuertos();							// comprueba si los jugadores estan muertos
		siEnemigosEstanMuertos();							// comprueba si los emenigos estan muertos
		distanciaProhibida();								// distancia maxima prohibida de movimiento							
		puntosEquipos();

		if(!dispararImagen && fasesDelJuego == 2)
		{
			StartCoroutine(activarFireImagen());			    // activa imagen de fire! al comienzo
		}
		
		if(teclaEscapeActivada)
		{
			Time.timeScale = 0f;
			menuIntermedio.gameObject.SetActive(true);
		}

		for(int k = 0; k < 5; k++)
		{
			if(enemigos[k].tag == "muerto" && fasesDelJuego != 3)
			{
				enemigos[k].GetComponent<Animation>().CrossFade("reposo");
			}
		}
	}

	public void reanudarPartida()
	{
		Time.timeScale = 1f;
		menuIntermedio.gameObject.SetActive(false);			// reanudamos la partida desde donde pulsamos escape
		teclaEscapeActivada = false;
	}

	public void opcionesPartida()
	{
		// opciones de partida .... code ....desabilitado
	
	}

	public void terminarPartida()
	{
		Application.LoadLevel(1);	// terminamos la partida y salimos al menu inventario
	}

	IEnumerator activarFireImagen()
	{
		activarFire = true;
		yield return new WaitForSeconds(80.0f * Time.deltaTime);
		activarFire = false;
		dispararImagen = true;
	}

	void distanciaProhibida()
	{
		for(int i=0;i<5;i++)
		{
			if(jugadores[i].tag != "muerto")
			{
				if(Vector3.Distance(jugadores[i].transform.position ,jugadores[i].GetComponent<codigoJugadorV002>().colisionRayo.point) > 700)
				{
					GetComponent<cursoresSobrePantalla>().cursorElegido = 5;	// cursor de prohibido
					distanciaProhibidaJugadores[i] = true;
				}else{
					distanciaProhibidaJugadores[i] = false;
				}
			}
		}
	}

	/// <summary>
	/// cambio de las fases del juego cuando empiezas
	/// </summary>
	void fasesPaintball()
	{
		if(fasesDelJuego == 0)
		{
			standCargando ();
			standCargandoEnemigos();
			if(tiempoDescansando <= 5)
			{
				scriptCamarasPantalla.GetComponent<camarasEnPantalla>().activarStandBuenos();
			}
		}
		
		if(fasesDelJuego == 0)
		{
			tiempoDescansando  = tiempoDescansando + 0.02f;
		}
		
		if(tiempoDescansando > 5)
		{
			scriptCamarasPantalla.GetComponent<camarasEnPantalla>().activarStandMalos();
		}
		
		if(tiempoDescansando > 12 && fasesDelJuego == 0)
		{
			fasesDelJuego = 1;
			scriptCamarasPantalla.GetComponent<camarasEnPantalla>().activarFaseUno();
			tiempoDescansando = 0;
		}

		if(fasesDelJuego == 1)
		{
			tocadosJugadores ();
			StartCoroutine(metodosVivos ());
			inicioFaseDosJugadores();				// jugadores
			inicioFaseDosEnemigos();				// enemigos

			StartCoroutine(iniciarTransporteInicio());
			tocandoBocina = true;
		}

		if(fasesDelJuego == 2)
		{
			tocadosJugadores ();
			StartCoroutine(metodosVivos ());
			jugadoresEnCasaMuertos();

			StartCoroutine(teletransportarEnemigosMuertos());	// teletransporta a los enemigos muertos al stand de inicio
			transportadoStandEnemigos();
			comprobarFaseTres();
			tocarBocina();
		}

		if(fasesDelJuego == 3)
		{
			// se colocan los jugadores en los stands
			// y se animan derrota o vistoria segun ganen
			// una vez que se ven las animaciones se va al menu de inicio
			// si empatan se animan en reposo o cargando, wait 5sg

			GetComponent<camarasEnPantalla>().activarCamaraVictoria ();

			animandoFaseTres();		// animaciones de los jugadores de victoria o derrota

		}
	}

	/// <summary>
	/// bocina de inicio del juego
	/// </summary>
	void tocarBocina()
	{
		if(tocandoBocina)
		{
			GetComponent<AudioSource>().PlayOneShot(bocinaComienzo);		// toca la bocina de comienzo de juego
			tocandoBocina = false;
		}
	}

	void puntosEquipos()
	{
		for(int k = 0; k < 5; k++)
		{
			if(jugadoresMuertos[k] == true)
			{
				ptosEquipoJugadores[k] = 1;
			}
		}

		for(int j = 0; j < 5; j++)
		{
			if(enemigosMuertos[j] == true)
			{
				ptosEquipoEnemigos[j] = 1;
			}
		}

		totalPtosJugadores = ptosEquipoJugadores[0] + ptosEquipoJugadores[1] + ptosEquipoJugadores[2] + ptosEquipoJugadores[3] + ptosEquipoJugadores[4];
		totalPtosEnemigos = ptosEquipoEnemigos[0] + ptosEquipoEnemigos[1] + ptosEquipoEnemigos[2] + ptosEquipoEnemigos[3] + ptosEquipoEnemigos[4];
	}

	IEnumerator tempofaseTres()
	{
		yield return new WaitForSeconds(6.0f);

		// reiniciar juego para empezar otra vez !!!!!
		// ir a menu de inicio 


	}

	void animandoFaseTres()
	{

		GetComponent<camarasEnPantalla>().ocultarSeleccionado = false;
		StartCoroutine(tempofaseTres());

		for(int i = 0; i < 5; i++)
		{
			if(totalPtosEnemigos == totalPtosJugadores)
			{
				// empate de ptos
				enemigos[i].transform.position = waypointsDerrotas[i].position;
				jugadores[i].transform.position = waypointsVictorias[i].position;
				jugadores[i].GetComponent<Animation>().CrossFade("victoria01");
				enemigos[i].GetComponent<Animation>().CrossFade("victoria01");
				GetComponent<cursoresSobrePantalla>().roundsDerecha = 1;
				GetComponent<cursoresSobrePantalla>().roundsIzquierda = 1;
				empate = true;
			}
			
			if(totalPtosEnemigos < totalPtosJugadores)
			{
				// ptos para los enemigos
				jugadores[i].transform.position = waypointsDerrotas[i].position;
				enemigos[i].transform.position = waypointsVictorias[i].position;
				jugadores[i].GetComponent<Animation>().CrossFade("derrota01");
				enemigos[i].GetComponent<Animation>().CrossFade("victoria01");
				GetComponent<cursoresSobrePantalla>().roundsDerecha = 1;
				perdiste  = true;
			}
			
			if(totalPtosEnemigos > totalPtosJugadores)
			{
				// ptos para los jugadores
				enemigos[i].transform.position = waypointsDerrotas[i].position;
				jugadores[i].transform.position = waypointsVictorias[i].position;
				jugadores[i].GetComponent<Animation>().CrossFade("victoria01");
				enemigos[i].GetComponent<Animation>().CrossFade("derrota01");
				GetComponent<cursoresSobrePantalla>().roundsIzquierda = 1;
				ganaste = true;
			}
		}
	}

	void comprobarFaseTres()
	{

		if(jugadoresMuertos[0] == true && jugadoresMuertos[1] == true && jugadoresMuertos[2] == true && jugadoresMuertos[3] == true && jugadoresMuertos[4] == true)
		{
			fasesDelJuego = 3;			// si esta un equipo jugadores entero muerto pasa a la fase tres
		}

		if(enemigosMuertos[0] == true && enemigosMuertos[1] == true && enemigosMuertos[2] == true && enemigosMuertos[3] == true && enemigosMuertos[4] == true)
		{
			fasesDelJuego = 3;			// si esta un equipo enemigos entero muerto pasa a la fase tres
		}

		if(GetComponent<cursoresSobrePantalla>().relojParadoParcial = false)
		{
			fasesDelJuego = 3;
		}
	}

	void siJugadoresEstanMuertos()
	{
		for(int numeroJugadores = 0; numeroJugadores < 5; numeroJugadores++)
		{
			if(jugadoresMuertos[numeroJugadores] && enCasaMuerto[numeroJugadores])
			{
				jugadores[numeroJugadores].tag = "muerto";
				jugadores[numeroJugadores].GetComponent<codigoJugadorV002>().menuAccionesAbierto = false;
			}
		}
	}

	void siEnemigosEstanMuertos()
	{
		for(int numeroEnemigos = 0; numeroEnemigos < 5; numeroEnemigos++)
		{
			if(enemigos[numeroEnemigos].tag == "muerto")
			{
				malosTocados[numeroEnemigos] = true;
				enemigosMuertos[numeroEnemigos] = true;
			}
		}
	}

	IEnumerator teletransportarEnemigosMuertos()
	{
		for(int numeroEnemigos = 0; numeroEnemigos < 5; numeroEnemigos++)
		{
			if(malosTocados[numeroEnemigos] && !enemigoTeletransportado[numeroEnemigos])
			{
				Instantiate(halosTeletransporte[numeroEnemigos],enemigos[numeroEnemigos].transform.position, enemigos[numeroEnemigos].transform.rotation);
				enemigos[numeroEnemigos].transform.position = halosTeletransporte[numeroEnemigos].transform.position;
				yield return new WaitForSeconds(1.0f * Time.deltaTime);
				enemigoTeletransportado[numeroEnemigos] = true;
			}
		}
	}

	void transportadoStandEnemigos()
	{
		for(int numeroEnemigos = 0; numeroEnemigos < 5; numeroEnemigos++)
		{
			if(malosTocados[numeroEnemigos] && enemigoTeletransportado[numeroEnemigos])
			{
				enemigos[numeroEnemigos].transform.position = cargandoStandMalos[numeroEnemigos].position;
			}
		}
	}


	IEnumerator metodosVivos()
	{
		for(int numeroJugadores = 0; numeroJugadores < 5; numeroJugadores++)
		{
			if(!jugadoresMuertos[numeroJugadores] && !enCasaMuerto[numeroJugadores])
			{
				jugadores[numeroJugadores].GetComponent<codigoJugadorV002>().accionesVivo(numeroJugadores);
			}

			if(jugadoresMuertos[numeroJugadores] && !enCasaMuerto[numeroJugadores]){
				halosTeletransporte[numeroJugadores].GetComponent<animacionHalo>().activarHaloTransporte = true;

				if(halosTeletransporte[numeroJugadores].GetComponent<animacionHalo>().activarHaloTransporte && !halosTeletransporteCreados[numeroJugadores])
				{
					halosTeletransporteCreados[numeroJugadores] = true;
					Instantiate(halosTeletransporte[numeroJugadores],jugadores[numeroJugadores].transform.position, jugadores[numeroJugadores].transform.rotation);
					jugadores[numeroJugadores].transform.position = halosTeletransporte[numeroJugadores].transform.position;
					enCasaMuerto[numeroJugadores] = true;
				}
				yield return new WaitForSeconds(1.0f * Time.deltaTime);
			}
		}
	}

	void jugadoresEnCasaMuertos()
	{
		for(int numeroJugadores = 0; numeroJugadores < 5; numeroJugadores++)
		{
			if(enCasaMuerto[numeroJugadores])
			{
				jugadores[numeroJugadores].GetComponent<codigoJugadorV002>().cancelarAcciones();
				jugadores[numeroJugadores].transform.position = cargandoStand[numeroJugadores].position;
			}
		}
	}

	IEnumerator iniciarTransporteInicio()
	{
			// yield return new WaitForSeconds(1.0f);																				// esperamos 2 segundos y empezamos el teletranporte
			
			halosTeletransporte[0].GetComponent<animacionHalo>().activarHaloTransporte = true;								// activamos halos
			halosTeletransporte[1].GetComponent<animacionHalo>().activarHaloTransporte = true;
			halosTeletransporte[2].GetComponent<animacionHalo>().activarHaloTransporte = true;
			halosTeletransporte[3].GetComponent<animacionHalo>().activarHaloTransporte = true;
			halosTeletransporte[4].GetComponent<animacionHalo>().activarHaloTransporte = true;


		// transportamos a los jugadores y iniciamos fase 2
		if(halosTeletransporte[0].GetComponent<animacionHalo>().activarHaloTransporte && !halosTeletransporteCreados[0])
		{
			halosTeletransporteCreados[0] = true;
			Instantiate(halosTeletransporte[0],jugadores[0].transform.position, jugadores[0].transform.rotation);
			Instantiate(halosTeletransporte[0],enemigos[0].transform.position, jugadores[0].transform.rotation);
			yield return new WaitForSeconds(1.5f * Time.deltaTime);																			// esperamos un segundo mientras se transporta
			//jugadores[0].transform.position = inicioStandJugadores[0].position;												// reaparece en la posicion deseada
			//enemigos[0].transform.position = inicioStandEnemigos[0].position;
		}
	
		if(halosTeletransporte[1].GetComponent<animacionHalo>().activarHaloTransporte && !halosTeletransporteCreados[1])
		{
			halosTeletransporteCreados[1] = true;
			Instantiate(halosTeletransporte[1],jugadores[1].transform.position, jugadores[1].transform.rotation);
			Instantiate(halosTeletransporte[1],enemigos[1].transform.position, jugadores[1].transform.rotation);
			yield return new WaitForSeconds(1.5f * Time.deltaTime);																			// esperamos un segundo mientras se transporta
			//jugadores[1].transform.position = inicioStandJugadores[1].position;												// reaparece en la posicion deseada
			//enemigos[1].transform.position = inicioStandEnemigos[1].position;
		}

		if(halosTeletransporte[2].GetComponent<animacionHalo>().activarHaloTransporte && !halosTeletransporteCreados[2])
		{
			halosTeletransporteCreados[2] = true;
			Instantiate(halosTeletransporte[2],jugadores[2].transform.position, jugadores[2].transform.rotation);
			Instantiate(halosTeletransporte[2],enemigos[2].transform.position, jugadores[2].transform.rotation);
			yield return new WaitForSeconds(1.5f * Time.deltaTime);																			// esperamos un segundo mientras se transporta
			//jugadores[2].transform.position = inicioStandJugadores[2].position;												// reaparece en la posicion deseada
			//enemigos[2].transform.position = inicioStandEnemigos[2].position;
		}

		if(halosTeletransporte[3].GetComponent<animacionHalo>().activarHaloTransporte && !halosTeletransporteCreados[3])
		{
			halosTeletransporteCreados[3] = true;
			Instantiate(halosTeletransporte[3],jugadores[3].transform.position, jugadores[3].transform.rotation);
			Instantiate(halosTeletransporte[3],enemigos[3].transform.position, jugadores[3].transform.rotation);
			yield return new WaitForSeconds(1.5f * Time.deltaTime);																			// esperamos un segundo mientras se transporta
			//jugadores[3].transform.position = inicioStandJugadores[3].position;												// reaparece en la posicion deseada
			//enemigos[3].transform.position = inicioStandEnemigos[3].position;
		}

		if(halosTeletransporte[4].GetComponent<animacionHalo>().activarHaloTransporte && !halosTeletransporteCreados[4])
		{
			halosTeletransporteCreados[4] = true;
			Instantiate(halosTeletransporte[4],jugadores[4].transform.position, jugadores[4].transform.rotation);
			Instantiate(halosTeletransporte[4],enemigos[4].transform.position, jugadores[4].transform.rotation);
			yield return new WaitForSeconds(1.5f * Time.deltaTime);																			// esperamos un segundo mientras se transporta
			//jugadores[4].transform.position = inicioStandJugadores[4].position;												// reaparece en la posicion deseada
			//enemigos[4].transform.position = inicioStandEnemigos[4].position;
		}


		// si estamos todos teletransportados se inicia la fase dos
		if(halosTeletransporteCreados[0] && halosTeletransporteCreados[1] && halosTeletransporteCreados[2] && halosTeletransporteCreados[3] && halosTeletransporteCreados[4])
		{
			yield return new WaitForSeconds(50.0f * Time.deltaTime);		
			jugadores[0].transform.position = inicioStandJugadores[0].position;												// reaparece en la posicion deseada
			jugadores[1].transform.position = inicioStandJugadores[1].position;	
			jugadores[2].transform.position = inicioStandJugadores[2].position;	
			jugadores[3].transform.position = inicioStandJugadores[3].position;	
			jugadores[4].transform.position = inicioStandJugadores[4].position;	

			enemigos[0].transform.position = inicioStandEnemigos[0].position;
			enemigos[1].transform.position = inicioStandEnemigos[1].position;
			enemigos[2].transform.position = inicioStandEnemigos[2].position;
			enemigos[3].transform.position = inicioStandEnemigos[3].position;
			enemigos[4].transform.position = inicioStandEnemigos[4].position;
				
			halosTeletransporteCreados[0] = false;								// desactivamos halo
			halosTeletransporteCreados[1] = false;	
			halosTeletransporteCreados[2] = false;	
			halosTeletransporteCreados[3] = false;	
			halosTeletransporteCreados[4] = false;	

			fasesDelJuego = 2;
		}
	}

	void inicioFaseDosJugadores()
	{
		for(int numeroJugadores = 0; numeroJugadores < 5; numeroJugadores++)
		{
			jugadores[numeroJugadores].transform.position = muerteStandJugadores[numeroJugadores].position;
			jugadores[numeroJugadores].GetComponent<Animation>().CrossFade("reposo");
			angulo.y = 0;
			jugadores[numeroJugadores].transform.rotation = angulo;
		}
	}

	void inicioFaseDosEnemigos()
	{
		for(int numeroEnemigos = 0; numeroEnemigos < 5; numeroEnemigos++)
		{
			enemigos[numeroEnemigos].transform.position = muerteStandEnemigos[numeroEnemigos].position;
			enemigos[numeroEnemigos].GetComponent<Animation>().CrossFade("reposo");
			angulo.y = 0;
			enemigos[numeroEnemigos].transform.rotation = angulo;
		}
	}

	void tocadosJugadores()
	{
		for(int numeroJugadores = 0; numeroJugadores < 5; numeroJugadores++)
		{
			jugadoresMuertos[numeroJugadores] = codigoJugadorV002.tocadoJugador [numeroJugadores];
		}
	}

	IEnumerator animandoJugadoresStand()
	{

		for(int numeroJugadores = 0; numeroJugadores < 5; numeroJugadores++)
		{
			if(!animandoJugadores[numeroJugadores])
			{
				animandoJugadores[numeroJugadores] = true;
				jugadores[numeroJugadores].GetComponent<Animation>().CrossFade(animacionSeleccionada01[numeroJugadores]);
				yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
				jugadores[numeroJugadores].GetComponent<Animation> ().CrossFade ("reposo");
				sonidoCargando[numeroJugadores] = true;
				yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
				frameAnimacion01[numeroJugadores] = Random.Range (0, 50);
				animandoJugadores[numeroJugadores] = false;
			}

			if(sonidoCargando[numeroJugadores] && tiempoDescansando <= 5 && fasesDelJuego != 2)
			{
				animacionSonido[numeroJugadores] = Random.Range(0,75);

				if(animacionSonido[numeroJugadores] <= 25)
				{
					GetComponent<AudioSource>().PlayOneShot(cargarUno);
				}

				if(animacionSonido[numeroJugadores] > 25 && animacionSonido[numeroJugadores] <= 50)
				{
					GetComponent<AudioSource>().PlayOneShot(cargarDos);
				}

				if(animacionSonido[numeroJugadores] > 50 && animacionSonido[numeroJugadores] <= 75)
				{
					GetComponent<AudioSource>().PlayOneShot(cargarTres);
				}

				sonidoCargando[numeroJugadores] = false;
			}

			if(frameAnimacion01[numeroJugadores] >= 25)
			{
				animacionSeleccionada01[numeroJugadores] = "standCargandoCosas";
			}else if(frameAnimacion01[numeroJugadores] < 25){
				animacionSeleccionada01[numeroJugadores] = "standCogiendoCosas";
			}
		}
	}

	IEnumerator animandoEnemigosStand()
	{
		for(int numeroEnemigos = 0; numeroEnemigos < 5; numeroEnemigos++)
		{
			if(!animandoEnemigos[numeroEnemigos])
			{
				animandoEnemigos[numeroEnemigos] = true;
				enemigos[numeroEnemigos].GetComponent<Animation>().CrossFade(animacionSeleccionada01Malos[numeroEnemigos]);
				yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
				enemigos[numeroEnemigos].GetComponent<Animation>().CrossFade ("reposo");
				sonidoCargandoEnemigos[numeroEnemigos] = true;
				yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
				frameAnimacion01Malos[numeroEnemigos] = Random.Range (0, 50);
				animandoEnemigos[numeroEnemigos] = false;
			}

			if(sonidoCargandoEnemigos[numeroEnemigos]  && fasesDelJuego != 2 && tiempoDescansando > 5 && tiempoDescansando < 12)
			{
				animacionSonidoEnemigos[numeroEnemigos] = Random.Range(0,75);
				
				if(animacionSonidoEnemigos[numeroEnemigos] <= 25)
				{
					GetComponent<AudioSource>().PlayOneShot(cargarUno);
				}
				
				if(animacionSonidoEnemigos[numeroEnemigos] > 25 && animacionSonidoEnemigos[numeroEnemigos] <= 50)
				{
					GetComponent<AudioSource>().PlayOneShot(cargarDos);
				}
				
				if(animacionSonidoEnemigos[numeroEnemigos] > 50 && animacionSonidoEnemigos[numeroEnemigos] <= 75)
				{
					GetComponent<AudioSource>().PlayOneShot(cargarTres);
				}

				sonidoCargandoEnemigos[numeroEnemigos] = false;
			}
			
			if(frameAnimacion01Malos[numeroEnemigos] >= 25)
			{
				animacionSeleccionada01Malos[numeroEnemigos] = "standCargandoCosas";
			}else if(frameAnimacion01Malos[numeroEnemigos] < 25){
				animacionSeleccionada01Malos[numeroEnemigos] = "standCogiendoCosas";
			}
		}
	}

	void standCargando()
	{
		for(int numeroJugadores = 0; numeroJugadores < 5; numeroJugadores++)
		{
			jugadores[numeroJugadores].transform.position = cargandoStand[numeroJugadores].position;
			jugadores[numeroJugadores].transform.rotation = angulo;
			StartCoroutine(animandoJugadoresStand ());
		}
	}

	// malos --------------------
	void standCargandoEnemigos()
	{
		for(int numeroEnemigos = 0; numeroEnemigos < 5; numeroEnemigos++)
		{
			enemigos[numeroEnemigos].transform.position = cargandoStandMalos[numeroEnemigos].position;
			enemigos[numeroEnemigos].transform.rotation = angulo;
			StartCoroutine(animandoEnemigosStand ());
		}
	}

	void activarPausa()
	{
		tiempoPausa = tiempoPausa + 1;

		if(tiempoPausa > 100)
		{
			tiempoPausa = 100;
		}

		if(Input.GetKeyDown(KeyCode.P) && !pausaActivada && tiempoPausa > 5)
		{
			tiempoPausa = 0;
			pausaActivada = true;
		}

		if(Input.GetKeyDown(KeyCode.P) && pausaActivada && tiempoPausa > 5)
		{
			tiempoPausa = 0;
			pausaActivada = false;
		}

	}

	void pausaDelJuego()
	{
		if(pausaActivada)
		{
			Time.timeScale = 0f;	// pausa activada
		}else{
			Time.timeScale = 1f;	// pausa desactivada
		}
	}

	void teclaEscape()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			// Application.Quit();		// salir de juego al pulsar escape .... añadir codigo de cargar menu intermedio !!!!!!!!
			teclaEscapeActivada = true;
		}
	}

	void OnGUI()
	{
		if(pausaActivada)
		{
			GUI.DrawTexture(new Rect((Screen.width/2) - 248,Screen.height - 400,500,200),juegoPausado,ScaleMode.StretchToFill,true,1.0f);	// dibuja textura pausa en pantalla
		}

		if(fasesDelJuego == 2 && activarFire)
		{
			GUI.DrawTexture(new Rect((Screen.width/2) - 248,Screen.height - 400,500,200),juegoFire,ScaleMode.StretchToFill,true,1.0f);
		}

		if(ganaste)
		{
			GUI.DrawTexture(new Rect((Screen.width/2) - 248,Screen.height - 400,500,200),juegoWin,ScaleMode.StretchToFill,true,1.0f);
		}

		if(perdiste)
		{
			GUI.DrawTexture(new Rect((Screen.width/2) - 248,Screen.height - 400,500,200),juegoLose,ScaleMode.StretchToFill,true,1.0f);
		}

		if(empate)
		{
			GUI.DrawTexture(new Rect((Screen.width/2) - 248,Screen.height - 400,500,200),juegoEmpate,ScaleMode.StretchToFill,true,1.0f);
		}
	}
}
