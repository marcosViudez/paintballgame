using UnityEngine;
using System.Collections;

public class PropiedadesGame : MonoBehaviour {

	public GameObject jugador1;
	public GameObject jugador2;
	public GameObject jugador3;
	public GameObject jugador4;
	public GameObject jugador5;

	public GameObject[] halosTransporte = new GameObject[5];
	public bool[] halosTransporteCreados = new bool[5];

	public GameObject enemigo1;
	public GameObject enemigo2;
	public GameObject enemigo3;
	public GameObject enemigo4;
	public GameObject enemigo5;

	public GameObject scriptCamarasPantalla;

	public Texture2D juegoPausado;

	public Transform inicioStand01;
	public Transform inicioStand02;
	public Transform inicioStand03;
	public Transform inicioStand04;
	public Transform inicioStand05;
	
	public Transform muerteStand01;
	public Transform muerteStand02;
	public Transform muerteStand03;
	public Transform muerteStand04;
	public Transform muerteStand05;
	
	public Transform cargandoStand01;
	public Transform cargandoStand02;
	public Transform cargandoStand03;
	public Transform cargandoStand04;
	public Transform cargandoStand05;

	public bool jugadorMuerto01 = false;
	public bool jugadorMuerto02 = false;
	public bool jugadorMuerto03 = false;
	public bool jugadorMuerto04 = false;
	public bool jugadorMuerto05 = false;
	public bool pausaActivada;

	public int tiempoPausa;
	public int fasesDelJuego = 0;
	public float tiempoDescansando;
	public int distanciaMuerte = 600;

	public Quaternion angulo;
	
	public int numeroAnime01;
	public int numeroAnime02;
	public int numeroAnime03;
	public int numeroAnime04;
	public int numeroAnime05;

	public bool animando01 = false;
	public bool animando02 = false;
	public bool animando03 = false;
	public bool animando04 = false;
	public bool animando05 = false;
	
	public bool paradoInicio01 = false;
	public bool paradoInicio02 = false;
	public bool paradoInicio03 = false;
	public bool paradoInicio04 = false;
	public bool paradoInicio05 = false;
	
	public string animacionSeleccionada01 = "standCargandoCosas";
	public string animacionSeleccionada02 = "standCogiendoCosas";
	public string animacionSeleccionada03 = "standCargandoCosas";
	public string animacionSeleccionada04 = "standCargandoCosas";
	public string animacionSeleccionada05 = "standCogiendoCosas";


	// gameObjects malos variables de los enemigos
	public Transform maloInicioStand01;
	public Transform maloInicioStand02;
	public Transform maloInicioStand03;
	public Transform maloInicioStand04;
	public Transform maloInicioStand05;

	public Transform muerteStand01Malo;
	public Transform muerteStand02Malo;
	public Transform muerteStand03Malo;
	public Transform muerteStand04Malo;
	public Transform muerteStand05Malo;

	public Transform maloCargandoStand01;
	public Transform maloCargandoStand02;
	public Transform maloCargandoStand03;
	public Transform maloCargandoStand04;
	public Transform maloCargandoStand05;

	public int numeroAnime01Malo;
	public int numeroAnime02Malo;
	public int numeroAnime03Malo;
	public int numeroAnime04Malo;
	public int numeroAnime05Malo;

	public bool animando01Malo = false;
	public bool animando02Malo = false;
	public bool animando03Malo = false;
	public bool animando04Malo = false;
	public bool animando05Malo = false;

	public bool maloParadoInicio01 = false;
	public bool maloParadoInicio02 = false;
	public bool maloParadoInicio03 = false;
	public bool maloParadoInicio04 = false;
	public bool maloParadoInicio05 = false;

	public string animacionSeleccionada01Malo = "standCogiendoCosas";
	public string animacionSeleccionada02Malo = "standCogiendoCosas";
	public string animacionSeleccionada03Malo = "standCogiendoCosas";
	public string animacionSeleccionada04Malo = "standCogiendoCosas";
	public string animacionSeleccionada05Malo = "standCogiendoCosas";

	void Start()
	{
		angulo.y = 1;
		// posicionesInicioStand ();
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();				// sale del juego al pulsar escape
		}

		// print (Vector3.Distance(jugador1.transform.position ,muerteStand01.position));

		if(pausaActivada)
		{
			Time.timeScale = 0f;
		}else{
			Time.timeScale = 1f;
		}

		activarPausa ();
		// enemigosMuertos ();

		if(fasesDelJuego == 0)
		{
			standCargando ();
			standCargandoEnemigos();
			scriptCamarasPantalla.GetComponent<camarasEnPantalla>().activarStandBuenos();
		}

		if(fasesDelJuego == 0)
		{
			tiempoDescansando = 0;
			tiempoDescansando = ((int)Time.fixedTime);
		}

		if(tiempoDescansando > 5)
		{
			scriptCamarasPantalla.GetComponent<camarasEnPantalla>().activarStandMalos();
		}

		if(tiempoDescansando > 12)
		{
			fasesDelJuego = 1;
			scriptCamarasPantalla.GetComponent<camarasEnPantalla>().activarFaseUno();
			tiempoDescansando = 0;
		}

		if(fasesDelJuego == 1)
		{
			tocadosJugadores ();
			StartCoroutine(metodosVivos ());
			inicioFaseDos();

			inicioFaseDosMalos();		// malos 
		}

		if(fasesDelJuego == 2)
		{
			tocadosJugadores ();
			StartCoroutine(metodosVivos ());
		}

		if(jugadorMuerto01)
		{
			jugador1.tag = "muerto";
			jugador1.GetComponent<codigoJugadorV002>().menuAccionesAbierto = false;
		}

		if(jugadorMuerto02)
		{
			jugador2.tag = "muerto";
			jugador2.GetComponent<codigoJugadorV002>().menuAccionesAbierto = false;
		}

		if(jugadorMuerto03)
		{
			jugador3.tag = "muerto";
			jugador3.GetComponent<codigoJugadorV002>().menuAccionesAbierto = false;
		}

		if(jugadorMuerto04)
		{
			jugador4.tag = "muerto";
			jugador4.GetComponent<codigoJugadorV002>().menuAccionesAbierto = false;
		}

		if(jugadorMuerto05)
		{
			jugador5.tag = "muerto";
			jugador5.GetComponent<codigoJugadorV002>().menuAccionesAbierto = false;
		}
	}

	/*void enemigosMuertos()
	{
		if(enemigo1.tag == "muerto")
		{
			enemigo1.GetComponent<muerteEnemigos>().maloTocado = true;
		}

		if(enemigo2.tag == "muerto")
		{
			enemigo2.GetComponent<muerteEnemigos>().maloTocado = true;
		}

		if(enemigo3.tag == "muerto")
		{
			enemigo3.GetComponent<muerteEnemigos>().maloTocado = true;
		}

		if(enemigo4.tag == "muerto")
		{
			enemigo4.GetComponent<muerteEnemigos>().maloTocado = true;
		}

		if(enemigo5.tag == "muerto")
		{
			enemigo5.GetComponent<muerteEnemigos>().maloTocado = true;
		}
	}*/

	void activarPausa()
	{
		tiempoPausa = (tiempoPausa + 1);

		if(tiempoPausa > 100)
		{
			tiempoPausa = 100;
		}

		if(Input.GetKey(KeyCode.P) && !pausaActivada && tiempoPausa > 5)
		{
			tiempoPausa = 0;
			pausaActivada = true;
		}

		if(Input.GetKey(KeyCode.P) && pausaActivada && tiempoPausa > 5)
		{
			tiempoPausa = 0;
			pausaActivada = false;
		}
	}

	// malos codigos ----------------
	void inicioFaseDosMalos()
	{
		if(!maloParadoInicio01)
		{
			enemigo1.GetComponent<ControlMovimiento>().moverseWaypointInicio(maloInicioStand01.transform.position);
			enemigo1.GetComponent<Animation> ().CrossFade ("correrNormal");
		}

		if(Vector3.Distance(enemigo1.transform.position, maloInicioStand01.position) < 10)
		{
			enemigo1.GetComponent<Animation> ().CrossFade ("reposo");
			maloParadoInicio01 = true;
		}

		if(!maloParadoInicio02)
		{
			enemigo2.GetComponent<ControlMovimiento>().moverseWaypointInicio(maloInicioStand02.transform.position);
			enemigo2.GetComponent<Animation> ().CrossFade ("correrNormal");
		}
		
		if(Vector3.Distance(enemigo2.transform.position, maloInicioStand02.position) < 10)
		{
			enemigo2.GetComponent<Animation> ().CrossFade ("reposo");
			maloParadoInicio02 = true;
		}

		if(!maloParadoInicio03)
		{
			enemigo3.GetComponent<ControlMovimiento>().moverseWaypointInicio(maloInicioStand03.transform.position);
			enemigo3.GetComponent<Animation> ().CrossFade ("correrNormal");
		}
		
		if(Vector3.Distance(enemigo3.transform.position, maloInicioStand03.position) < 10)
		{
			enemigo3.GetComponent<Animation> ().CrossFade ("reposo");
			maloParadoInicio03 = true;
		}

		if(!maloParadoInicio04)
		{
			enemigo4.GetComponent<ControlMovimiento>().moverseWaypointInicio(maloInicioStand04.transform.position);
			enemigo4.GetComponent<Animation> ().CrossFade ("correrNormal");
		}
		
		if(Vector3.Distance(enemigo4.transform.position, maloInicioStand04.position) < 10)
		{
			enemigo4.GetComponent<Animation> ().CrossFade ("reposo");
			maloParadoInicio04 = true;
		}

		if(!maloParadoInicio05)
		{
			enemigo5.GetComponent<ControlMovimiento>().moverseWaypointInicio(maloInicioStand05.transform.position);
			enemigo5.GetComponent<Animation> ().CrossFade ("correrNormal");
		}
		
		if(Vector3.Distance(enemigo5.transform.position, maloInicioStand05.position) < 10)
		{
			enemigo5.GetComponent<Animation> ().CrossFade ("reposo");
			maloParadoInicio05 = true;
		}

	}

	void inicioFaseDos()
	{
		if(!paradoInicio01)
		{
			jugador1.GetComponent<codigoJugadorV002> ().ejecutandoAccion = true;
			jugador1.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = inicioStand01.position;
			jugador1.GetComponent<codigoJugadorV002> ().moviendoseWaypoint = true;
			jugador1.GetComponent<Animation> ().CrossFade ("correrNormal");
		}

		if(!paradoInicio02)
		{
			jugador2.GetComponent<codigoJugadorV002> ().ejecutandoAccion = true;
			jugador2.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = inicioStand02.position;
			jugador2.GetComponent<codigoJugadorV002> ().moviendoseWaypoint = true;
			jugador2.GetComponent<Animation> ().CrossFade ("correrNormal");
		}

		if(!paradoInicio03)
		{
			jugador3.GetComponent<codigoJugadorV002> ().ejecutandoAccion = true;
			jugador3.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = inicioStand03.position;
			jugador3.GetComponent<codigoJugadorV002> ().moviendoseWaypoint = true;
			jugador3.GetComponent<Animation> ().CrossFade ("correrNormal");
		}

		if(!paradoInicio04)
		{
			jugador4.GetComponent<codigoJugadorV002> ().ejecutandoAccion = true;
			jugador4.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = inicioStand04.position;
			jugador4.GetComponent<codigoJugadorV002> ().moviendoseWaypoint = true;
			jugador4.GetComponent<Animation> ().CrossFade ("correrNormal");
		}

		if(!paradoInicio05)
		{
			jugador5.GetComponent<codigoJugadorV002> ().ejecutandoAccion = true;
			jugador5.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = inicioStand05.position;
			jugador5.GetComponent<codigoJugadorV002> ().moviendoseWaypoint = true;
			jugador5.GetComponent<Animation> ().CrossFade ("correrNormal");
		}

		if(Vector3.Distance(jugador1.transform.position, inicioStand01.position) < 10)
		{
			jugador1.GetComponent<codigoJugadorV002> ().ejecutandoAccion = false;
			jugador1.GetComponent<Animation> ().CrossFade ("reposo");
			paradoInicio01 = true;
		}

		if(Vector3.Distance(jugador2.transform.position, inicioStand02.position) < 10)
		{
			jugador2.GetComponent<codigoJugadorV002> ().ejecutandoAccion = false;
			jugador2.GetComponent<Animation> ().CrossFade ("reposo");
			paradoInicio02 = true;
		}

		if(Vector3.Distance(jugador3.transform.position, inicioStand03.position) < 10)
		{
			jugador3.GetComponent<codigoJugadorV002> ().ejecutandoAccion = false;
			jugador3.GetComponent<Animation> ().CrossFade ("reposo");
			paradoInicio03 = true;
		}

		if(Vector3.Distance(jugador4.transform.position, inicioStand04.position) < 10)
	    {
			jugador4.GetComponent<codigoJugadorV002> ().ejecutandoAccion = false;
			jugador4.GetComponent<Animation> ().CrossFade ("reposo");
			paradoInicio04 = true;
		}

		if(Vector3.Distance(jugador5.transform.position, inicioStand05.position) < 10)
		{
			jugador5.GetComponent<codigoJugadorV002> ().ejecutandoAccion = false;
			jugador5.GetComponent<Animation> ().CrossFade ("reposo");
			paradoInicio05 = true;
		}

		if(paradoInicio01 && paradoInicio02 && paradoInicio03 && paradoInicio04 && paradoInicio05)
		{
			fasesDelJuego = 2;
		}
	}

	void standCargando()
	{
		jugador1.transform.position = cargandoStand01.position;
		jugador1.transform.rotation = angulo;
		StartCoroutine(anime01 ());

		jugador2.transform.position = cargandoStand02.position;
		jugador2.transform.rotation = angulo;
		StartCoroutine(anime02 ());

		jugador3.transform.position = cargandoStand03.position;
		jugador3.transform.rotation = angulo;
		StartCoroutine(anime03 ());

		jugador4.transform.position = cargandoStand04.position;
		jugador4.transform.rotation = angulo;
		StartCoroutine(anime04 ());

		jugador5.transform.position = cargandoStand05.position;
		jugador5.transform.rotation = angulo;
		StartCoroutine(anime05 ());
	}


	// malos --------------------
	void standCargandoEnemigos()
	{
		enemigo1.transform.position = maloCargandoStand01.position;
		enemigo1.transform.rotation = angulo;
		StartCoroutine(anime01Malo ());

		enemigo2.transform.position = maloCargandoStand02.position;
		enemigo2.transform.rotation = angulo;
		StartCoroutine(anime01Malo ());

		enemigo3.transform.position = maloCargandoStand03.position;
		enemigo3.transform.rotation = angulo;
		StartCoroutine(anime01Malo ());

		enemigo4.transform.position = maloCargandoStand04.position;
		enemigo4.transform.rotation = angulo;
		StartCoroutine(anime01Malo ());

		enemigo5.transform.position = maloCargandoStand05.position;
		enemigo5.transform.rotation = angulo;
		StartCoroutine(anime01Malo ());
	}

	IEnumerator anime01Malo()
	{
		if(!animando01Malo)
		{
			animando01Malo = true;
			enemigo1.GetComponent<Animation> ().CrossFade (animacionSeleccionada01Malo);
			yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
			enemigo1.GetComponent<Animation> ().CrossFade ("reposo");
			yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
			numeroAnime01Malo = Random.Range (0, 50);
			animando01Malo = false;
		}
		
		
		if(numeroAnime01 >= 25)
		{
			animacionSeleccionada01Malo = "standCargandoCosas";
		}else if(numeroAnime01Malo < 25){
			animacionSeleccionada01Malo = "standCogiendoCosas";
		}

		if(!animando02Malo)
		{
			animando02Malo = true;
			enemigo2.GetComponent<Animation> ().CrossFade (animacionSeleccionada02Malo);
			yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
			enemigo2.GetComponent<Animation> ().CrossFade ("reposo");
			yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
			numeroAnime02Malo = Random.Range (0, 50);
			animando02Malo = false;
		}
		
		
		if(numeroAnime02 >= 25)
		{
			animacionSeleccionada02Malo = "standCargandoCosas";
		}else if(numeroAnime02Malo < 25){
			animacionSeleccionada02Malo = "standCogiendoCosas";
		}

		if(!animando03Malo)
		{
			animando03Malo = true;
			enemigo3.GetComponent<Animation> ().CrossFade (animacionSeleccionada03Malo);
			yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
			enemigo3.GetComponent<Animation> ().CrossFade ("reposo");
			yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
			numeroAnime03Malo = Random.Range (0, 50);
			animando03Malo = false;
		}
		
		
		if(numeroAnime03 >= 25)
		{
			animacionSeleccionada03Malo = "standCargandoCosas";
		}else if(numeroAnime03Malo < 25){
			animacionSeleccionada03Malo = "standCogiendoCosas";
		}

		if(!animando04Malo)
		{
			animando04Malo = true;
			enemigo4.GetComponent<Animation> ().CrossFade (animacionSeleccionada04Malo);
			yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
			enemigo4.GetComponent<Animation> ().CrossFade ("reposo");
			yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
			numeroAnime04Malo = Random.Range (0, 50);
			animando04Malo = false;
		}
		
		
		if(numeroAnime04 >= 25)
		{
			animacionSeleccionada04Malo = "standCargandoCosas";
		}else if(numeroAnime04Malo < 25){
			animacionSeleccionada04Malo = "standCogiendoCosas";
		}

		if(!animando05Malo)
		{
			animando05Malo = true;
			enemigo5.GetComponent<Animation> ().CrossFade (animacionSeleccionada05Malo);
			yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
			enemigo5.GetComponent<Animation> ().CrossFade ("reposo");
			yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
			numeroAnime05Malo = Random.Range (0, 50);
			animando05Malo = false;
		}
		
		
		if(numeroAnime05 >= 25)
		{
			animacionSeleccionada05Malo = "standCargandoCosas";
		}else if(numeroAnime05Malo < 25){
			animacionSeleccionada05Malo = "standCogiendoCosas";
		}
	}

	IEnumerator anime01()
	{
		if(!animando01)
		{
			animando01 = true;
			jugador1.GetComponent<Animation> ().CrossFade (animacionSeleccionada01);
			yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
			jugador1.GetComponent<Animation> ().CrossFade ("reposo");
			yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
			numeroAnime01 = Random.Range (0, 50);
			animando01 = false;
		}


		if(numeroAnime01 >= 25)
		{
			animacionSeleccionada01 = "standCargandoCosas";
		}else if(numeroAnime01 < 25){
			animacionSeleccionada01 = "standCogiendoCosas";
		}
	}

	IEnumerator anime02()
	{
		if(!animando02)
		{
			animando02 = true;
			jugador2.GetComponent<Animation> ().CrossFade (animacionSeleccionada02);
			yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
			jugador2.GetComponent<Animation> ().CrossFade ("reposo");
			yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
			numeroAnime02 = Random.Range (0, 50);
			animando02 = false;
		}
		
		
		if(numeroAnime02 >= 25)
		{
			animacionSeleccionada02 = "standCargandoCosas";
		}else if(numeroAnime02 < 25){
			animacionSeleccionada02 = "standCogiendoCosas";
		}
	}

	IEnumerator anime03()
	{
		if(!animando03)
		{
			animando03 = true;
			jugador3.GetComponent<Animation> ().CrossFade (animacionSeleccionada03);
			yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
			jugador3.GetComponent<Animation> ().CrossFade ("reposo");
			yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
			numeroAnime03 = Random.Range (0, 50);
			animando03 = false;
		}
		
		
		if(numeroAnime03 >= 25)
		{
			animacionSeleccionada03 = "standCargandoCosas";
		}else if(numeroAnime03 < 25){
			animacionSeleccionada03 = "standCogiendoCosas";
		}
	}

	IEnumerator anime04()
	{
		if(!animando04)
		{
			animando04 = true;
			jugador4.GetComponent<Animation> ().CrossFade (animacionSeleccionada04);
			yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
			jugador4.GetComponent<Animation> ().CrossFade ("reposo");
			yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
			numeroAnime04 = Random.Range (0, 50);
			animando04 = false;
		}
		
		
		if(numeroAnime04 >= 25)
		{
			animacionSeleccionada04 = "standCargandoCosas";
		}else if(numeroAnime04 < 25){
			animacionSeleccionada04 = "standCogiendoCosas";
		}
	}

	IEnumerator anime05()
	{
		if(!animando05)
		{
			animando05 = true;
			jugador5.GetComponent<Animation> ().CrossFade (animacionSeleccionada05);
			yield return new WaitForSeconds (Random.Range(20.0f,60.0f) * Time.deltaTime);
			jugador5.GetComponent<Animation> ().CrossFade ("reposo");
			yield return new WaitForSeconds (Random.Range(100.0f,160.0f) * Time.deltaTime);
			numeroAnime05 = Random.Range (0, 50);
			animando05 = false;
		}
		
		
		if(numeroAnime05 >= 25)
		{
			animacionSeleccionada05 = "standCargandoCosas";
		}else if(numeroAnime05 < 25){
			animacionSeleccionada05 = "standCogiendoCosas";
		}
	}

	void posicionesInicioStand()
	{
		jugador1.transform.position = inicioStand01.position;
		jugador2.transform.position = inicioStand02.position;
		jugador3.transform.position = inicioStand03.position;
		jugador4.transform.position = inicioStand04.position;
		jugador5.transform.position = inicioStand05.position;
	}

	void tocadosJugadores()
	{
		jugadorMuerto01 = codigoJugadorV002.tocadoJugador [0];
		jugadorMuerto02 = codigoJugadorV002.tocadoJugador [1];
		jugadorMuerto03 = codigoJugadorV002.tocadoJugador [2];
		jugadorMuerto04 = codigoJugadorV002.tocadoJugador [3];
		jugadorMuerto05 = codigoJugadorV002.tocadoJugador [4];
	}

	IEnumerator metodosVivos()
	{
		if(!jugadorMuerto01)
		{
			jugador1.GetComponent<codigoJugadorV002>().accionesVivo(0);
		}else{
			halosTransporte[0].GetComponent<animacionHalo>().activarHaloTransporte = true;

			if(halosTransporte[0].GetComponent<animacionHalo>().activarHaloTransporte && !halosTransporteCreados[0])
			{
				halosTransporteCreados[0] = true;
				Instantiate(halosTransporte[0],jugador1.transform.position,jugador1.transform.rotation);
				jugador1.transform.position = halosTransporte[0].transform.position;
			}

			yield return new WaitForSeconds(1.0f);
			jugador1.transform.position = cargandoStand01.position;
			jugador1.transform.rotation = Quaternion.Slerp(jugador1.transform.rotation, Quaternion.LookRotation(new Vector3(0,0,0)),3.0f * Time.deltaTime);
		}

		if(!jugadorMuerto02)
		{
			jugador2.GetComponent<codigoJugadorV002>().accionesVivo(1);
		}else{
			if(Vector3.Distance(jugador2.transform.position ,muerteStand02.position) < distanciaMuerte)
			{
				jugador2.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = muerteStand02.position;	// modificar muerte jugador
			}
			
			if(Vector3.Distance(jugador2.transform.position, muerteStand02.position) >= distanciaMuerte)
			{
				jugador2.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = muerteStand02Malo.position;	// modificar muerte jugador
			}

			jugador2.GetComponent<codigoJugadorV002> ().accionesMuerto (1);
			if(jugador2.GetComponent<codigoJugadorV002> ().faseTocado == 2)
			{
				jugador2.transform.position = cargandoStand02.position;
				jugador2.transform.rotation = Quaternion.Slerp(jugador2.transform.rotation, Quaternion.LookRotation(new Vector3(0,0,0)),3.0f * Time.deltaTime);
			}
		}

		if(!jugadorMuerto03)
		{
			jugador3.GetComponent<codigoJugadorV002>().accionesVivo(2);
		}else{
			if(Vector3.Distance(jugador3.transform.position ,muerteStand03.position) < distanciaMuerte)
			{
				jugador3.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = muerteStand03.position;	// modificar muerte jugador
			}
			
			if(Vector3.Distance(jugador3.transform.position, muerteStand03.position) >= distanciaMuerte)
			{
				jugador3.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = muerteStand03Malo.position;	// modificar muerte jugador
			}

			jugador3.GetComponent<codigoJugadorV002> ().accionesMuerto (2);
			if(jugador3.GetComponent<codigoJugadorV002> ().faseTocado == 2)
			{
				jugador3.transform.position = cargandoStand03.position;
				jugador3.transform.rotation = Quaternion.Slerp(jugador3.transform.rotation, Quaternion.LookRotation(new Vector3(0,0,0)),3.0f * Time.deltaTime);
			}
		}

		if(!jugadorMuerto04)
		{
			jugador4.GetComponent<codigoJugadorV002>().accionesVivo(3);
		}else{
			if(Vector3.Distance(jugador4.transform.position ,muerteStand04.position) < distanciaMuerte)
			{
				jugador4.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = muerteStand04.position;	// modificar muerte jugador
			}
			
			if(Vector3.Distance(jugador4.transform.position, muerteStand04.position) >= distanciaMuerte)
			{
				jugador4.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = muerteStand04.position;	// modificar muerte jugador
			}
			jugador4.GetComponent<codigoJugadorV002> ().accionesMuerto (3);
			if(jugador4.GetComponent<codigoJugadorV002> ().faseTocado == 2)
			{
				jugador4.transform.position = cargandoStand04.position;
				jugador4.transform.rotation = Quaternion.Slerp(jugador4.transform.rotation, Quaternion.LookRotation(new Vector3(0,0,0)),3.0f * Time.deltaTime);
			}
		}

		if(!jugadorMuerto05)
		{
			jugador5.GetComponent<codigoJugadorV002>().accionesVivo(4);
		}else{
			if(Vector3.Distance(jugador5.transform.position ,muerteStand05.position) < distanciaMuerte)
			{
				jugador5.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = muerteStand05.position;	// modificar muerte jugador
			}
			
			if(Vector3.Distance(jugador5.transform.position, muerteStand05.position) >= distanciaMuerte)
			{
				jugador5.GetComponent<codigoJugadorV002> ().puntoDesplazamiento = muerteStand05.position;	// modificar muerte jugador
			}
			jugador5.GetComponent<codigoJugadorV002> ().accionesMuerto (4);
			if(jugador5.GetComponent<codigoJugadorV002> ().faseTocado == 2)
			{
				jugador5.transform.position = cargandoStand05.position;
				jugador5.transform.rotation = Quaternion.Slerp(jugador5.transform.rotation, Quaternion.LookRotation(new Vector3(0,0,0)),3.0f * Time.deltaTime);
			}
		}
	}	

	void OnGUI()
	{
		if(pausaActivada)
		{
			GUI.DrawTexture(new Rect((Screen.width/2) - 248,Screen.height - 400,500,200),juegoPausado,ScaleMode.StretchToFill,true,1.0f);
		}

		// boton de quitar intro
		if(GUI.Button(new Rect(20,60,100,40),"saltar intro"))
		{
			// jugadores 
			fasesDelJuego = 2;
			tiempoDescansando = 0;
			scriptCamarasPantalla.GetComponent<camarasEnPantalla>().activarFaseUno();
			scriptCamarasPantalla.GetComponent<camarasEnPantalla>().activarFaseDos();

			jugador1.transform.position = inicioStand01.position;
			jugador2.transform.position = inicioStand02.position;
			jugador3.transform.position = inicioStand03.position;
			jugador4.transform.position = inicioStand04.position;
			jugador5.transform.position = inicioStand05.position;
			paradoInicio01 = true;
			paradoInicio02 = true;
			paradoInicio03 = true;
			paradoInicio04 = true;
			paradoInicio05 = true;
			jugador1.GetComponent<Animation> ().CrossFade ("reposo");
			jugador2.GetComponent<Animation> ().CrossFade ("reposo");
			jugador3.GetComponent<Animation> ().CrossFade ("reposo");
			jugador4.GetComponent<Animation> ().CrossFade ("reposo");
			jugador5.GetComponent<Animation> ().CrossFade ("reposo");

			// enemigos
			enemigo1.transform.position = maloInicioStand01.position;
			enemigo2.transform.position = maloInicioStand02.position;
			enemigo3.transform.position = maloInicioStand03.position;
			enemigo4.transform.position = maloInicioStand04.position;
			enemigo5.transform.position = maloInicioStand05.position;
			maloParadoInicio01 = true;
			maloParadoInicio02 = true;
			maloParadoInicio03 = true;
			maloParadoInicio04 = true;
			maloParadoInicio05 = true;
			enemigo1.GetComponent<Animation> ().CrossFade ("reposo");
			enemigo2.GetComponent<Animation> ().CrossFade ("reposo");
			enemigo3.GetComponent<Animation> ().CrossFade ("reposo");
			enemigo4.GetComponent<Animation> ().CrossFade ("reposo");
			enemigo5.GetComponent<Animation> ().CrossFade ("reposo");
		}
	}
}
