using UnityEngine;
using System.Collections;

public class codigoJugadorV002 : MonoBehaviour {
	// declaracion de variables
	public GUISkin botonesInterface;
	public UnityEngine.AI.NavMeshAgent agent;
	public AudioClip audioDisparo;
	public AudioClip audioCargar;

	public Texture2D interfaceDerecha;
	public Texture2D interfaceIzquierda;

	public bool estoyVivo;
	private bool seleccionado;
	private bool seleccionadoClick;
	private bool haloActivado;
	public bool menuAccionesAbierto;
	public bool ejecutandoAccion;
	// variables para todas las acciones de los botones
	private bool reposo = true;
	private bool cargarArma = false;
	private bool arrodillarse = false;
	private bool tumbarse = false;
	private bool moverseAgachado = false;
	private bool moverseCorriendo = false;
	private bool disparar = false;
	private bool dispararLadoDerecho = false;
	private bool dispararLadoIzquierdo = false;
	private bool moverseAgachadoDisparando = false;
	private bool moverseCorriendoDisparando = false;
	private bool estoyArrodillado =  false;
	private bool estoyTumbado =  false;
	public bool moviendoseWaypoint;
	private bool enemigoDetectado;
	private bool miraProhibidoActivada;
	private bool disparandoRafagas = false;
	private bool bolaCreada = false;
	public bool crearBolaAmarilla = true;
	public bool crearBolaRoja = false;
	public bool crearBolaAzul = false;
	public bool crearBolaVerde = false;
	public bool crearBolaVioleta = false;
	public bool buenosVictoria = false;
	public bool buenosDerrota = false;
	private bool buenoTocado = false;

	public GameObject haloDeSeleccion;
	public GameObject jugador;
	public GameObject estadosYCursores;
	public GameObject bolaPinturaAmarilla;
	public GameObject bolaPinturaRoja;
	public GameObject bolaPinturaAzul;
	public GameObject bolaPinturaVerde;
	public GameObject bolaPinturaVioleta;
	public GameObject particulaGasBueno;

	public GameObject prefabHaloOcultar01;
	public GameObject prefabHaloOcultar02;
	public GameObject prefabHaloOcultar03;
	public GameObject prefabHaloOcultar04;
	public GameObject prefabHaloOcultar05;

	public Transform cannoDisparar;

	private string nombreColision;
	private string aspectoBotonAccion;
	private string aspectoBotonCargar;
	
	private int tamx = 100;
	private int tamy = 200;
	private int alturaPersonaje = 0;
	private int numeroBotonPulsado;
	private int destinoWaypoint = 10;
	private int radioFuego = 10;
	private int numeroBolasTotal = 0;
	private int numeroJugadorResta;
	private int nombreJugador;
	public int faseTocado;
	private int jugadorSeleccionadoPods = 5;
	public int cantidadRafaga = 5;
	private int contador = 0;

	public RaycastHit colisionRayo;
	private float longitudRayo = 10000.0f;	
	private float velocidadDisparoBolas = 1.0f;

	public Vector3 puntoDesplazamiento;
	private Vector3 CambioCoordenadaRatonY;
	private Vector3 puntoDisparo;
	
	public Texture2D[] accionesTextura = new Texture2D[10];
	public Texture2D[] accionesTexturaRojos = new Texture2D[6];
	public Rect[] botonesAcciones = new Rect[10];
	public int[] podsCodigo = new int[5];
	public int[] bolasCodigo = new int[5];
	public int[] gasCodigo = new int[5];

	static public bool[] tocadoJugador = new bool[5];
	public bool[] tocadoJugadorPublica = new bool[5];

	// Use this for initialization
	void Awake () 
	{	
		estoyVivo = true;
		Cursor.visible = false;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();		// cogemos nuestra variable navegacion
		estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 0;
		posicionesBotones ();

		for(int i=0; i<5; i++)
		{
			tocadoJugador[i] = false;
			tocadoJugadorPublica[i] = false;
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		for(int i=0; i<5 ; i++)
		{
			tocadoJugadorPublica[i] = tocadoJugador [i];
		}
		halosColores();
	}

    public void accionesVivo(int j)
	{
			// mirar codigo
			if(!tocadoJugadorPublica[j])
			{
				seleccionandoJugador ();		// iniciamos la seleccion de jugadores
				pulsandoBotonesMenuAcciones (); // pulsado de botones de la interface jugador
				desactivarHalo ();
				seleccionandoInterfaceJugadores ();
				botonesRojos ();
				// comprobacion de animaciones
				estasEnReposo ();
				StartCoroutine (comprobandoMoverseAgachado ());
				StartCoroutine (comprobandoMoverseCorriendo ());
				comprobandoSiEstasAgachado ();
				comprobandoSiEstasAcostado ();
				comprobandoMoviendoseWaypoint ();
				StartCoroutine (comprobandoSiEstasDisparando ());
				StartCoroutine (comprobandoSiEstasDisparandoLadeadoDerecha());
				StartCoroutine (comprobandoSiEstasDisparandoLadeadoIzquierda());
				StartCoroutine (comprobandoDisparandoAgachado());
				StartCoroutine (comprobandoDisparandoCorriendo());
				StartCoroutine (comprobandoSiEstasCargando ());
			 	estadosBuenos();
				
				if(Input.GetMouseButtonDown(1))
				{
					estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 0;
					cancelarAcciones();
				}
			}
	}

	public void accionesMuerto(int j)
	{
		if(tocadoJugadorPublica[j])
		{
			cancelarAcciones();
			reposo = true;
			buenoTocado = true;
			estadosBuenos();
		}
	}

	public void cancelarAcciones()
	{
		// reseteamos al pulsar el boton secundario raton
		if(!moviendoseWaypoint)
		{
			jugadorDeseleccionado();
			haloDeSeleccion.SetActive(false);
			agent.Stop();
			moviendoseWaypoint = false;
			disparar = false;
			ejecutandoAccion = false;
			numeroBotonPulsado = 10;
			moverseAgachado = false;
			moverseAgachadoDisparando = false;
			moverseCorriendo = false;
			moverseCorriendoDisparando = false;
			dispararLadoDerecho = false;
			dispararLadoIzquierdo = false;
			reposo = true;
			GetComponent<Animation>().CrossFade ("reposo");	
		}
	}

	void estadosBuenos()
	{
		// print(Vector3.Distance(puntoDesplazamiento, transform.position));
		// estados de victoria , derrota y tocado
		if(buenosVictoria)
		{
			reposo = false;
			GetComponent<Animation>().CrossFade ("victoria01");
		}
		
		if(buenosDerrota)
		{
			reposo = false;
			GetComponent<Animation>().CrossFade ("derrota01");
		}

		/*
		if(buenoTocado)
		{
			reposo = false;
			if(faseTocado == 0)
			{
				GetComponent<Animation>().CrossFade ("tocado01");
				yield return new WaitForSeconds(1f);
				faseTocado = 1;
			}
			
			if(faseTocado == 1)
			{
				GetComponent<Animation>().CrossFade ("tocado01Andar");
				faseTocado = 2;
				// moviendoseWaypoint = true;
				// moverseWaypoint();										// segundo metodo de waypoint
			}

			if(Vector3.Distance(puntoDesplazamiento, transform.position) < 31)
			{
				faseTocado = 2;
			}
			
			if(faseTocado == 2)
			{
				agent.Stop();					// detengo el desplazamiento del jugador
				// moviendoseWaypoint = false;		// waypoint false
				reposo = true;					// reposo anime true
				buenoTocado = false;
			}

		}*/
	}

	void seleccionandoJugador()
	{
		if(estadosYCursores.GetComponent<propiedadesGamev002>().fasesDelJuego == 2)
		{
			if (GetComponent<Renderer> ().isVisible && !ejecutandoAccion && Input.GetMouseButton (0)) 
			{
				if(seleccionadoClick)
				{
					StartCoroutine(jugadorSeleccionado());	// seleccionamos jugadores metodo con temporizador
				}else{
					StartCoroutine(jugadorDeseleccionado());	// deseleccionamos jugador seleccionado metodo
				}
			}
		}
	}

	/// <summary>
	/// Acciones cuando un jugador es seleccionado
	/// </summary>
	IEnumerator jugadorSeleccionado()
	{
		haloActivado = true;						// activamos halo de seleccion
		menuAccionesAbierto = false;				// menu botones acciones oculto
		yield return new WaitForSeconds(0.01f * Time.deltaTime);		// espero un tiempo para cambio de estado
		menuAccionesAbierto = true;					// muestro el menu botones acciones
	}

	/// <summary>
	/// Acciones cuando un jugador es deseleccionado
	/// </summary>
	/// <returns></returns>
	IEnumerator jugadorDeseleccionado()
	{

		haloActivado = false;	// desactivamos halo de seleccion
		seleccionado = false;
		yield return new WaitForSeconds(0.01f * Time.deltaTime);		// espero un tiempo para cambio de estado
		menuAccionesAbierto = false;

		if(!haloActivado)
		{
			jugadorSeleccionadoPods = 5;
		}
		estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[0] = false;
		estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[1] = false;
		estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[2] = false;
		estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[3] = false;
		estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[4] = false;
	}

	/// <summary>
	/// Desactiva el halo que rodea al jugador cuando
	/// esta seleccionado
	/// </summary>
	void desactivarHalo()
	{
		if (haloActivado) 
		{
			haloDeSeleccion.SetActive(true);	// activamos halo de seleccion
		}else{
			haloDeSeleccion.SetActive(false);	// desactivamos halo de seleccion
		}
	}

	/// <summary>
	///  activaciones de los halos de los jugadores cuando
	///  pulsas el boton de ver halos de la interface
	/// </summary>
	void halosColores()
	{
		if(estadosYCursores.GetComponent<camarasEnPantalla> ().ocultarSeleccionado == true)
		{
			prefabHaloOcultar01.SetActive(true);
			prefabHaloOcultar02.SetActive(true);
			prefabHaloOcultar03.SetActive(true);
			prefabHaloOcultar04.SetActive(true);
			prefabHaloOcultar05.SetActive(true);
		}else{
			prefabHaloOcultar01.SetActive(false);
			prefabHaloOcultar02.SetActive(false);
			prefabHaloOcultar03.SetActive(false);
			prefabHaloOcultar04.SetActive(false);
			prefabHaloOcultar05.SetActive(false);
		}
	}

	/// <summary>
	/// interfaces de acciones de los jugadores segun cual
	/// tengas seleccionado
	/// </summary>
	void seleccionandoInterfaceJugadores()
	{	
		if(seleccionado)
		{
			if(transform.name == "nuevoJugador01")
			{
				// print("jugador01");
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[0] = true;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[1] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[2] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[3] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[4] = false;
			}
			if(transform.name == "nuevoJugador02")
			{
				// print("jugador02");
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[0] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[1] = true;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[2] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[3] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[4] = false;
			}
			if(transform.name == "nuevoJugador03")
			{
				// print("jugador03");
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[0] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[1] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[2] = true;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[3] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[4] = false;
			}
			if(transform.name == "nuevoJugador04")
			{
				// print("jugador04");
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[0] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[1] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[2] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[3] = true;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[4] = false;
			}
			if(transform.name == "nuevoJugador05")
			{
				// print("jugador05");
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[0] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[1] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[2] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[3] = false;
				estadosYCursores.GetComponent<bolasPinturaRestando> ().interfaceSeleccionada[4] = true;
			}
		}
	}

	/// <summary>
	/// Raycast para las acciones tanto de disparo como seleccionar donde
	/// vas a ir caminando
	/// </summary>
	/// <param name="objetoColisionador"></param>
	/// <returns></returns>
	IEnumerator crearRayoPantalla(string objetoColisionador)
	{
		Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(rayo,out colisionRayo,longitudRayo))
		{
			//colisiona con el gameObject que queremos mover, su tag
			if(colisionRayo.collider.tag == "obstaculo")
			{
				miraProhibidoActivada = true;
			}else{
				miraProhibidoActivada = false;
			}
			
			if(colisionRayo.collider.tag == "enemigo")
			{
				enemigoDetectado = true;
			}else{
				enemigoDetectado = false;
			}

			if(colisionRayo.collider.tag == objetoColisionador)
			{
				if(Input.GetButtonDown("Fire1") && !estadosYCursores.GetComponent<propiedadesGamev002>().distanciaProhibidaJugadores[0] && !estadosYCursores.GetComponent<propiedadesGamev002>().distanciaProhibidaJugadores[1]
				   && !estadosYCursores.GetComponent<propiedadesGamev002>().distanciaProhibidaJugadores[2] && !estadosYCursores.GetComponent<propiedadesGamev002>().distanciaProhibidaJugadores[3]
				   && !estadosYCursores.GetComponent<propiedadesGamev002>().distanciaProhibidaJugadores[4])
				{
					switch(numeroBotonPulsado)
					{
					case 0:
						// cargar arma
						break;
					case 1:
						// arrodillarme
						break;
					case 2:
						// tumbarme
						break;
					case 3:
						// moverse agachado
						agent.speed = 30;
						agent.acceleration = 30;
						reposo = false;
						if(!reposo){
							GetComponent<Animation>().CrossFade ("caminarAgachado");
						}
						// waypoint a donde se va a desplazar el jugador
						puntoDesplazamiento = new Vector3(colisionRayo.point.x, alturaPersonaje, colisionRayo.point.z);		
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 2;		// cursor de moverse cerrado
						ejecutandoAccion = false;
						moverseAgachado = false;
						yield return new WaitForSeconds(0.3f * Time.deltaTime);						// pequeño retardo ver cambio de cursor
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 0;		// vuelvo cursor flecha
						moviendoseWaypoint = true;						// ejecuto metodo para moverme hacia ptoDesplazamiento
						numeroBotonPulsado = 10;						// numero pulsado nulo	
						break;
					case 4:
						// moverse corriendo
						agent.speed = 60;
						agent.acceleration = 60;
						reposo = false;
						if(!reposo){
							GetComponent<Animation>().CrossFade ("correrNormal");
						}
						// waypoint a donde se va a desplazar el jugador
						puntoDesplazamiento = new Vector3(colisionRayo.point.x, alturaPersonaje, colisionRayo.point.z);		
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 2;		// cursor de moverse cerrado
						ejecutandoAccion = false;
						moverseCorriendo = false;
						yield return new  WaitForSeconds(0.3f * Time.deltaTime);						// pequeño retardo ver cambio de cursor
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 0;		// vuelvo cursor flecha
						moviendoseWaypoint = true;						// ejecuto metodo para moverme hacia ptoDesplazamiento
						numeroBotonPulsado = 10;						// numero pulsado nulo
						break;
					case 5:
						// disparar
						if(!enemigoDetectado)	
						{
							puntoDisparo = colisionRayo.point;			// cogemos la posicion del mouse si es verde puntero
						}else{
							puntoDisparo = colisionRayo.point;			// cogemos la posicion del mouse si es rojo puntero
						}
						reposo = false;
						girarHaciaWaypoint(puntoDisparo);				// giro hacia el waypoint
						// yield WaitForSeconds(1);						// espero para disparar un segundo
						StartCoroutine(dispararBolas(cantidadRafaga,velocidadDisparoBolas));			// disparo rafaga a cierta velocidad (rafaga,velocidad)
						ejecutandoAccion = false;
						disparar = false;
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 0;	// vuelvo cursor flecha
						numeroBotonPulsado = 10;						// numero pulsado nulo
						break;
					case 6:
						// dispararLadoDerecho
						if(!enemigoDetectado)	
						{
							puntoDisparo = colisionRayo.point;			// cogemos la posicion del mouse si es verde puntero
						}else{
							puntoDisparo = colisionRayo.point;			// cogemos la posicion del mouse si es rojo puntero
						}
						reposo = false;
						if(!reposo){
							GetComponent<Animation>().CrossFade ("ladearDerPie");
						}
						girarHaciaWaypoint(puntoDisparo);				// giro hacia el waypoint
						// yield WaitForSeconds(1);						// espero para disparar un segundo
						StartCoroutine(dispararBolas(cantidadRafaga,velocidadDisparoBolas));				// disparo rafaga a cierta velocidad (rafaga,velocidad)
						ejecutandoAccion = false;
						dispararLadoDerecho = false;
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 0;	// vuelvo cursor flecha
						numeroBotonPulsado = 10;						// numero pulsado nulo
						if(!reposo){
							yield return new WaitForSeconds(3f * Time.deltaTime);
							GetComponent<Animation>().CrossFade ("ladearDerPieVolver");
							// reposo = true;
						}
						break;
					case 7:
						// dispararLadoIzquierdo
						if(!enemigoDetectado)	
						{
							puntoDisparo = colisionRayo.point;			// cogemos la posicion del mouse si es verde puntero
						}else{
							puntoDisparo = colisionRayo.point;			// cogemos la posicion del mouse si es rojo puntero
						}
						reposo = false;
						if(!reposo){
							GetComponent<Animation>().CrossFade ("ladearIzqPie");
						}
						girarHaciaWaypoint(puntoDisparo);				// giro hacia el waypoint
						// yield WaitForSeconds(1);						// espero para disparar un segundo
						StartCoroutine(dispararBolas(cantidadRafaga,velocidadDisparoBolas));		// disparo rafaga a cierta velocidad (rafaga,velocidad)
						ejecutandoAccion = false;
						dispararLadoIzquierdo = false;
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 0;	// vuelvo cursor flecha
						numeroBotonPulsado = 10;						// numero pulsado nulo
						if(!reposo){
							yield return new WaitForSeconds(3f * Time.deltaTime);
							GetComponent<Animation>().CrossFade ("ladearIzqPieVolver");
							// reposo = true;
						}
						break;
					case 8:
						// moverseAgachadoDisparando
						agent.speed = 30;
						agent.acceleration = 30;
						reposo = false;
						if(!reposo){
							GetComponent<Animation>().CrossFade ("caminarAgachado");		
						}
						// waypoint a donde se va a desplazar el jugador
						puntoDesplazamiento = new Vector3(colisionRayo.point.x, alturaPersonaje, colisionRayo.point.z);	
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 7;		// cursor de moverse cerrado
						StartCoroutine(dispararBolas(cantidadRafaga,velocidadDisparoBolas));			// disparo rafaga a cierta velocidad (rafaga,velocidad)
						disparar = false;
						ejecutandoAccion = false;
						moverseAgachadoDisparando = false;
						yield return new WaitForSeconds(5.0f * Time.deltaTime);						// pequeño retardo ver cambio de cursor
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 0;		// vuelvo cursor flecha
						moviendoseWaypoint = true;						// ejecuto metodo para moverme hacia ptoDesplazamiento
						numeroBotonPulsado = 10;						// numero pulsado nulo	
						break;
					case 9:
						// moverseCorriendoDisparando
						agent.speed = 60;
						agent.acceleration = 60;
						reposo = false;
						if(!reposo){
							GetComponent<Animation>().CrossFade ("correrNormal");		
						}
						// waypoint a donde se va a desplazar el jugador
						puntoDesplazamiento = new Vector3(colisionRayo.point.x, alturaPersonaje, colisionRayo.point.z);		
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 7;		// cursor de moverse cerrado
						StartCoroutine(dispararBolas(cantidadRafaga,velocidadDisparoBolas));			// disparo rafaga a cierta velocidad (rafaga,velocidad)
						disparar = false;
						ejecutandoAccion = false;
						moverseCorriendoDisparando = false;
						yield return new WaitForSeconds(5.0f * Time.deltaTime);						// pequeño retardo ver cambio de cursor
						estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 0;		// vuelvo cursor flecha
						moviendoseWaypoint = true;						// ejecuto metodo para moverme hacia ptoDesplazamiento
						numeroBotonPulsado = 10;						// numero pulsado nulo	
						break;
					default:
						break;
					}
				}
			}
		}
		Debug.DrawRay(rayo.origin, rayo.direction * longitudRayo, Color.yellow);	// mostramos el rayo
	}

	/// <summary>
	/// Cambiamos la coordenada en el eje Y para los botones de interface
	/// </summary>
	void cambioCoordY()
	{
		CambioCoordenadaRatonY = new Vector3(Input.mousePosition.x,Screen.height - Input.mousePosition.y,Input.mousePosition.z);
	}

	/// <summary>
	/// Pulsaciones de los botones de la interface de acciones por jugador
	/// </summary>
	void pulsandoBotonesMenuAcciones()
	{
		// esto me cambia la coordenada del mouse para igualarla a la coordenada de GUI
		cambioCoordY();
		
		for(int boton = 0; boton < 10; boton++)
		{
			if(menuAccionesAbierto && botonesAcciones[boton].Contains(CambioCoordenadaRatonY))
			{
				// cambiar color al boton
				if(Input.GetMouseButtonDown(0))
				{
					switch(boton)
					{
					case 0:
						if(podsCodigo[nombreJugador] > 0 && gasCodigo[nombreJugador] > 0)
						{
							cargarArma = true;
							numeroBotonPulsado = 0;
						}
						break;

					case 1:
						arrodillarse = true;
						numeroBotonPulsado = 1;
						break;

					case 2:
						tumbarse = true;
						numeroBotonPulsado = 2;
						break;

					case 3:
						moverseAgachado = true;
						numeroBotonPulsado = 3;
						break;

					case 4:
						moverseCorriendo = true;
						numeroBotonPulsado = 4;
						break;

					case 5:
						if(bolasCodigo[nombreJugador] > 0 && gasCodigo[nombreJugador] > 0)
						{
							disparar = true;
							numeroBotonPulsado = 5;
						}
						break;

					case 6:
						if(bolasCodigo[nombreJugador] > 0 && gasCodigo[nombreJugador] > 0)
						{	
							dispararLadoDerecho = true;
							numeroBotonPulsado = 6;
						}
						break;

					case 7:
						if(bolasCodigo[nombreJugador] > 0 && gasCodigo[nombreJugador] > 0)
						{
							dispararLadoIzquierdo = true;
							numeroBotonPulsado = 7;
						}
						break;

					case 8:
						if(bolasCodigo[nombreJugador] > 0 && gasCodigo[nombreJugador] > 0)
						{
							moverseAgachadoDisparando = true;
							numeroBotonPulsado = 8;
						}
						break;

					case 9:
						if(bolasCodigo[nombreJugador] > 0 && gasCodigo[nombreJugador] > 0)
						{
							moverseCorriendoDisparando = true;
							numeroBotonPulsado = 9;
						}
						break;
					default:
						break;
					}
				}
			}
		}
	}

	/// <summary>
	/// botones rojos cuando no tienes algo
	/// </summary>
	void botonesRojos()
	{	
		if(transform.name == "nuevoJugador01")
		{
			nombreJugador = 0;
		}
		if(transform.name == "nuevoJugador02")
		{
			nombreJugador = 1;
		}
		if(transform.name == "nuevoJugador03")
		{
			nombreJugador = 2;
		}
		if(transform.name == "nuevoJugador04")
		{
			nombreJugador = 3;
		}
		if(transform.name == "nuevoJugador05")
		{
			nombreJugador = 4;
		}

		podsCodigo[nombreJugador] = estadosYCursores.GetComponent<bolasPinturaRestando> ().menuVidaSegunda[nombreJugador].numeroPods;
		bolasCodigo[nombreJugador] = estadosYCursores.GetComponent<bolasPinturaRestando> ().menuVidaSegunda[nombreJugador].numeroBolasCargador;
		gasCodigo [nombreJugador] = estadosYCursores.GetComponent<bolasPinturaRestando> ().menuVidaSegunda [nombreJugador].maxCO2;

		if(bolasCodigo[nombreJugador] > 20)
		{
			aspectoBotonAccion = "botonesDeInterface";
		}
		if(bolasCodigo[nombreJugador] <= 20 && bolasCodigo[nombreJugador] > 1)
		{
			aspectoBotonAccion = "botonesInterfaceNaranja";
		}
		if(bolasCodigo[nombreJugador] <= 0)
		{
			aspectoBotonAccion = "botonesInterfaceRojo";
		}
		
		if(podsCodigo[nombreJugador] > 1)
		{
			aspectoBotonCargar = "botonesDeInterface";
		}
		if(podsCodigo[nombreJugador] == 1)
		{
			aspectoBotonCargar = "botonesInterfaceNaranja";
		}
		if(podsCodigo[nombreJugador] == 0)
		{
			aspectoBotonCargar = "botonesInterfaceRojo";
		}
		
		
	}

	void girarHaciaWaypoint(Vector3 punto)
	{
		Vector3 relativaPos = punto - transform.position;								// calculo la posicion al objetivo
		transform.rotation = Quaternion.LookRotation(relativaPos);							// giro hacia su posicion
	}


	void moverseWaypoint()
	{
		// navegacion por navmesh 
		agent.ResetPath();							// reseteamos camino arregla el seleccion lejana
		agent.SetDestination(puntoDesplazamiento);		// desplazo al jugador	
		arrodillarse = false;
		tumbarse = false;
		estoyTumbado = false;
		estoyArrodillado = false;

		if(Vector3.Distance(puntoDesplazamiento, transform.position) < destinoWaypoint)
		{
			agent.Stop();					// detengo el desplazamiento del jugador
			moviendoseWaypoint = false;		// waypoint false
			reposo = true;					// reposo anime true
		}
	}

	void comprobandoMoviendoseWaypoint()
	{
		if(moviendoseWaypoint)
		{
			moverseWaypoint();
		}
	}

	void estasEnReposo()
	{
		if(reposo)
		{
			GetComponent<Animation>().CrossFade ("reposo");	// activa animacion de reposo
		}
	}

	IEnumerator comprobandoSiEstasCargando()
	{
		if(!moviendoseWaypoint && !estoyTumbado && cargarArma)
		{
			cargarArma = false;
			reposo = false;
			if(!reposo && !estoyArrodillado)
			{
				GetComponent<Animation>().CrossFade ("cargarDePie");		// cargo arma de pie
				GetComponent<AudioSource>().PlayOneShot(audioCargar);
				estadosYCursores.GetComponent<bolasPinturaRestando> ().metodoCargadores = true;
				// tirarPods = true;											// tiramos un pod al suelo
			}
			
			if(!reposo && estoyArrodillado)
			{
				GetComponent<Animation>().CrossFade ("cargarArrodillado");	// cargo arma si estoy arrodillado
				GetComponent<AudioSource>().PlayOneShot(audioCargar);
				estadosYCursores.GetComponent<bolasPinturaRestando> ().metodoCargadores = true;
				// tirarPods = true;											// tiramos un pod al suelo
			}
			yield return new WaitForSeconds(2f * Time.deltaTime);
			cargarArma = false;
		}
		
		if(!moviendoseWaypoint && estoyTumbado && cargarArma)
		{
			cargarArma = false;
		}
		
	}

	IEnumerator comprobandoMoverseAgachado()
	{
		if(moverseAgachado)
		{	
			ejecutandoAccion = true;
			estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 1;	// cursor de moverse abierto
			yield return new WaitForSeconds(0.1f * Time.deltaTime);			// esperamos medio segundo cambio de pantalla interface
			menuAccionesAbierto = false;		// cerramos el menu de botones
			StartCoroutine(crearRayoPantalla("suelo"));			// creamos el rayo de colision con suelo	
		}
	}

	IEnumerator comprobandoMoverseCorriendo()
	{
		if(moverseCorriendo)
		{	
			ejecutandoAccion = true;
			estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 1;	// cursor de moverse abierto
			yield return new  WaitForSeconds(0.1f * Time.deltaTime);			// esperamos medio segundo cambio de pantalla interface
			menuAccionesAbierto = false;		// cerramos el menu de botones
			StartCoroutine(crearRayoPantalla("suelo"));			// creamos el rayo de colision con suelo	
		}
	}

	void comprobandoSiEstasAgachado()
	{
		if(!moviendoseWaypoint && arrodillarse && !estoyArrodillado && !cargarArma)
		{
			reposo = false;
			if(!reposo)
			{
				GetComponent<Animation>().CrossFade ("arrodillarse");		
			}
			arrodillarse = false;
			estoyArrodillado = true;
			estoyTumbado = false;
			cargarArma = false;
		}
		
		if(!moviendoseWaypoint && arrodillarse && estoyArrodillado && !cargarArma)
		{
			reposo = false;
			if(!reposo)
			{
				GetComponent<Animation>().CrossFade ("levantarseArrodillado");		
			}
			arrodillarse = false;
			estoyArrodillado = false;
			estoyTumbado = false;
			cargarArma = false;
		}
	}

	void comprobandoSiEstasAcostado()
	{
		if(!moviendoseWaypoint && tumbarse && !estoyTumbado && !cargarArma)
		{
			reposo = false;
			if(!reposo)
			{
				GetComponent<Animation>().CrossFade ("acostarse");		
			}
			tumbarse = false;
			estoyTumbado = true;
			estoyArrodillado = false;
			cargarArma = false;
		}
		
		if(!moviendoseWaypoint && tumbarse && estoyTumbado && !cargarArma)
		{
			reposo = false;
			if(!reposo)
			{
				GetComponent<Animation>().CrossFade ("levantarseAcostado");		
			}
			tumbarse = false;
			estoyTumbado = false;
			estoyArrodillado = false;
			cargarArma = false;
		}
	}

	IEnumerator comprobandoSiEstasDisparando()
	{	
		if(disparar)
		{
			ejecutandoAccion = true;
			if(!enemigoDetectado && !miraProhibidoActivada)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 3;	// cursor de pto mira verde	
				nombreColision = "suelo";	
			}
			
			if(enemigoDetectado && !miraProhibidoActivada)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 4;	// cursor de pto mira roja	
				nombreColision = "enemigo";	
			}
			
			if(miraProhibidoActivada && !enemigoDetectado)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido =  5;
			}
			
			yield return new WaitForSeconds(0.1f * Time.deltaTime);				// esperamos medio segundo cambio de pantalla interface
			menuAccionesAbierto = false;						// cerramos el menu de botones
			StartCoroutine(crearRayoPantalla(nombreColision));	// creamos el rayo de colision con suelo			
		}
	}

	IEnumerator comprobandoSiEstasDisparandoLadeadoDerecha()
	{
		if(dispararLadoDerecho)
		{
			ejecutandoAccion = true;
			if(!enemigoDetectado && !miraProhibidoActivada)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido =  3;	// cursor de pto mira verde	
				nombreColision = "suelo";	
			}
			
			if(enemigoDetectado && !miraProhibidoActivada)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido =  4;	// cursor de pto mira roja	
				nombreColision = "enemigo";	
			}
			
			if(miraProhibidoActivada && !enemigoDetectado)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido =  5;
			}
			
			yield return new WaitForSeconds(0.1f * Time.deltaTime);					// esperamos medio segundo cambio de pantalla interface
			menuAccionesAbierto = false;							// cerramos el menu de botones
			StartCoroutine(crearRayoPantalla(nombreColision));		// creamos el rayo de colision con suelo			
		}
	}

	IEnumerator comprobandoSiEstasDisparandoLadeadoIzquierda()
	{
		if(dispararLadoIzquierdo)
		{
			ejecutandoAccion = true;
			if(!enemigoDetectado && !miraProhibidoActivada)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido =  3;	// cursor de pto mira verde	
				nombreColision = "suelo";	
			}
			
			if(enemigoDetectado && !miraProhibidoActivada)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido =  4;	// cursor de pto mira roja	
				nombreColision = "enemigo";	
			}
			
			if(miraProhibidoActivada && !enemigoDetectado)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido =  5;
			}
			
			yield return new WaitForSeconds(0.1f * Time.deltaTime);				// esperamos medio segundo cambio de pantalla interface
			menuAccionesAbierto = false;						// cerramos el menu de botones
			StartCoroutine(crearRayoPantalla(nombreColision));	// creamos el rayo de colision con suelo			
		}
	}

	IEnumerator comprobandoDisparandoAgachado()
	{
		if(moverseAgachadoDisparando)
		{	
			ejecutandoAccion = true;
			if(!enemigoDetectado && !miraProhibidoActivada)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido =  6;	// cursor de moverse abierto
			}
			
			if(miraProhibidoActivada || enemigoDetectado)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido = 5;	// cursor de prohibido
			}
			yield return new WaitForSeconds(0.1f * Time.deltaTime);			// esperamos medio segundo cambio de pantalla interface
			menuAccionesAbierto = false;					// cerramos el menu de botones
			StartCoroutine(crearRayoPantalla("suelo"));		// creamos el rayo de colision con suelo	
		}
	}

	IEnumerator comprobandoDisparandoCorriendo()
	{
		if(moverseCorriendoDisparando)
		{	
			ejecutandoAccion = true;		
			if(!enemigoDetectado && !miraProhibidoActivada)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido =  6;	// cursor de moverse abierto
			}
			
			if(miraProhibidoActivada || enemigoDetectado)
			{
				estadosYCursores.GetComponent<cursoresSobrePantalla> ().cursorElegido =  5;	// cursor de prohibido
			}
			yield return new WaitForSeconds(0.1f * Time.deltaTime);			// esperamos medio segundo cambio de pantalla interface
			menuAccionesAbierto = false;					// cerramos el menu de botones
			StartCoroutine(crearRayoPantalla("suelo"));		// creamos el rayo de colision con suelo	
		}
	}

	IEnumerator dispararBolas(int rafagaBolas,float velocidadBolas)
	{
		yield return new WaitForSeconds(0.5f * Time.deltaTime);		// tiempo de giro del jugador									
		disparandoRafagas = true;				    // rafaga disparando

		// creo una rafaga de bolas segun me indica el metodo
		for(int i = 0; i < rafagaBolas; i++)
		{
			if(!bolaCreada && bolasCodigo[nombreJugador] > 0)
			{
				// con un random numero para la estabilidad de disparo o carencia de disparo
				if(crearBolaAmarilla)
				{
					Instantiate(bolaPinturaAmarilla, cannoDisparar.position + new Vector3(Random.Range(0,radioFuego),Random.Range(0,radioFuego),Random.Range(0,radioFuego)), cannoDisparar.rotation);	// instancio nuestra municion
				}
				if(crearBolaRoja)
				{
					Instantiate(bolaPinturaRoja, cannoDisparar.position + new Vector3(Random.Range(0,radioFuego),Random.Range(0,radioFuego),Random.Range(0,radioFuego)), cannoDisparar.rotation);	// instancio nuestra municion
				}
				if(crearBolaAzul)
				{
					Instantiate(bolaPinturaAzul, cannoDisparar.position + new Vector3(Random.Range(0,radioFuego),Random.Range(0,radioFuego),Random.Range(0,radioFuego)), cannoDisparar.rotation);	// instancio nuestra municion
				}
				if(crearBolaVerde)
				{
					Instantiate(bolaPinturaVerde, cannoDisparar.position + new Vector3(Random.Range(0,radioFuego),Random.Range(0,radioFuego),Random.Range(0,radioFuego)), cannoDisparar.rotation);	// instancio nuestra municion
				}
				if(crearBolaVioleta)
				{
					Instantiate(bolaPinturaVioleta, cannoDisparar.position + new Vector3(Random.Range(0,radioFuego),Random.Range(0,radioFuego),Random.Range(0,radioFuego)), cannoDisparar.rotation);	// instancio nuestra municion
				}
				Instantiate(particulaGasBueno,cannoDisparar.position,cannoDisparar.rotation);	// instancio particulas de gas
				bolaCreada = true;																// bola esta creada												
				GetComponent<AudioSource>().PlayOneShot(audioDisparo);						// ejecuta sonido
				// AudioSource.PlayClipAtPoint(audioDisparo,transform.position);					// ejecuta sonido
				yield return new WaitForSeconds(velocidadBolas  * Time.deltaTime);				// espero a lanzar la siguiente
				bolaCreada = false;																// bola creada lanzada
				contador++;																		// aumento en uno el contador 
				numeroBolasTotal++;																// sumamos bolas disparadas
			

				// scriptInterface.GetComponent(InterfaceEnergias).restarBolas(0);				// restamos bolas del cargador
				if(transform.name == "nuevoJugador01")
				{
					numeroJugadorResta = 0;
					estadosYCursores.GetComponent<bolasPinturaRestando> ().restarBolas(numeroJugadorResta);
				}
				
				if(transform.name == "nuevoJugador02")
				{
					numeroJugadorResta = 1;
					estadosYCursores.GetComponent<bolasPinturaRestando> ().restarBolas(numeroJugadorResta);
				}
				
				if(transform.name == "nuevoJugador03")
				{
					numeroJugadorResta = 2;
					estadosYCursores.GetComponent<bolasPinturaRestando> ().restarBolas(numeroJugadorResta);
				}
				
				if(transform.name == "nuevoJugador04")
				{
					numeroJugadorResta = 3;
					estadosYCursores.GetComponent<bolasPinturaRestando> ().restarBolas(numeroJugadorResta);
				}
				
				if(transform.name == "nuevoJugador05")
				{
					numeroJugadorResta = 4;
					estadosYCursores.GetComponent<bolasPinturaRestando> ().restarBolas(numeroJugadorResta);
				}
			}
		}
		
		if(contador == cantidadRafaga)
		{
			disparandoRafagas = false;	
			contador = 0;
		}
	}


	void OnMouseDown()
	{
		if(Input.GetMouseButtonDown(0))
		{
			seleccionadoClick = true;
			seleccionado = true;
		}
	}

	void OnMouseUp()
	{
		if (seleccionadoClick) 
		{
			seleccionado = true;
			seleccionadoClick = false;
		}
	}

	void posicionesBotones()
	{
		botonesAcciones[0] = new Rect(Screen.width/2 - 142,Screen.height/2 - 130,40,40); 	// primer boton posicion
		botonesAcciones[1] = new Rect(Screen.width/2 - 171,Screen.height/2 - 80,40,40);		// segundo boton posicion
		botonesAcciones[2] = new Rect(Screen.width/2 - 175,Screen.height/2 - 26,40,40); 	// tercer boton posicion
		botonesAcciones[3] = new Rect(Screen.width/2 - 171,Screen.height/2 + 28,40,40); 	// cuarto boton posicion
		botonesAcciones[4] = new Rect(Screen.width/2 - 142,Screen.height/2 + 80,40,40); 	// quinto boton posicion
		
		botonesAcciones[5] = new Rect(Screen.width/2 + 102,Screen.height/2 - 130,40,40); 	// sexto boton posicion
		botonesAcciones[6] = new Rect(Screen.width/2 + 131,Screen.height/2 - 80,40,40); 	// septimo boton posicion
		botonesAcciones[7] = new Rect(Screen.width/2 + 136,Screen.height/2 - 26,40,40); 	// octavo boton posicion
		botonesAcciones[8] = new Rect(Screen.width/2 + 131,Screen.height/2 + 28,40,40); 	// noveno boton posicion
		botonesAcciones[9] = new Rect(Screen.width/2 + 102,Screen.height/2 + 80,40,40); 	// decimo boton posicion
	}

	void OnGUI()
	{
		GUI.skin = botonesInterface;	// mi estilo de botones

		GUI.depth = 2;

		if(menuAccionesAbierto)
		{
			// dibujamos las dos interfaces de juego
			GUI.DrawTexture(new Rect((Screen.width/2-tamx/2) + 100,Screen.height/2-tamy/2,tamx,tamy),interfaceDerecha,ScaleMode.StretchToFill,true,1.0f);
			GUI.DrawTexture(new Rect((Screen.width/2-tamx/2) - 100,Screen.height/2-tamy/2,tamx,tamy),interfaceIzquierda,ScaleMode.StretchToFill,true,1.0f);
			
			// botones de interface de la parte izquierda
			// GUI.Button(Rect(posicion_x , posicion_y, tamaño_x, tamaño_y), texto, aspecto)
			GUI.Button(new Rect(Screen.width/2 - 142,Screen.height/2 - 130,40,40),accionesTextura[0],aspectoBotonCargar);
			GUI.Button(new Rect(Screen.width/2 - 171,Screen.height/2 - 80,40,40),accionesTextura[1],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 - 175,Screen.height/2 - 26,40,40),accionesTextura[2],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 - 171,Screen.height/2 + 28,40,40),accionesTextura[3],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 - 142,Screen.height/2 + 80,40,40),accionesTextura[4],"botonesDeInterface");
			// botones de interface de la parte derecha
			GUI.Button(new Rect(Screen.width/2 + 102,Screen.height/2 - 130,40,40),accionesTextura[5],aspectoBotonAccion);
			GUI.Button(new Rect(Screen.width/2 + 131,Screen.height/2 - 80,40,40),accionesTextura[6],aspectoBotonAccion);
			GUI.Button(new Rect(Screen.width/2 + 136,Screen.height/2 - 26,40,40),accionesTextura[7],aspectoBotonAccion);
			GUI.Button(new Rect(Screen.width/2 + 131,Screen.height/2 + 28,40,40),accionesTextura[8],aspectoBotonAccion);
			GUI.Button(new Rect(Screen.width/2 + 102,Screen.height/2 + 80,40,40),accionesTextura[9],aspectoBotonAccion);
		}
	}
}
