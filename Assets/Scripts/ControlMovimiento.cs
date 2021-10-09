using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * CLASE CONTROL MOVIMIENTO
 * ---------------------------------------------------
 * Movimiento del meco segun condiciones
 * Busca waypoint aleatorio:	
 * Segun posicion de Jugador - Descubierto / No descubierto 
 * Realiza pausas aleatorias en Waypoints
 * 
 * Para modificar indices:
 *      indexPrimeroJD
 *      indexUltimoJD 
 *      indexPrimeroRestaJND
 *      indexUltimoRestaJND
*/

public class ControlMovimiento : MonoBehaviour {

	// ------------------------------------------------
	// VARIABLES
	// ------------------------------------------------
    // Fases de juego
	public GameObject scriptFases;

    // Movimiento
	private GameObject inicioWaypoint;		    // Waypoint para inicio de movimiento *** ASIGNADO DENTRO DEL CODIGO
	public GameObject siguienteWaypoint;	    // Waypoint para movimiento (Actualiza por cada movimiento)	
    public GameObject waypointTemp; 
	public bool bMoviendoMeco = false;             
    // Waypoints                                            
    public int numeroWaypoints;
    private int restaIndexPrimeroJND = 60; 	    // Resta del numero total de Waypoints. Indica el elemento de la lista a partir del cual inicia segundo movimiento - NO DESCUBIERTO
    private int restaindexUltimoJND = 46;		// Resta del numero total de Waypoints. Indica el ultimo elemento posible de la lista hacia el que se dirije el Meco  - NO DESCUBIERTO
    public int indexPrimeroJND;
    public int indexUltimoJND;
    private int indexWaypoint;                   // Indice que almacena el elemento de la lista de Waypoints al que se movera el Meco         
	public GameObject[] misWaypoints;                                                                            
    public bool bColisionaWaypoint;             // ** SOLUCIONAR En caso de colision con Waypoint, no podra mover otro Meco a esa posicion
	// Temporizador
	private float tiempo = 0;
    public int aleatorioTemporizador;
    public float cuentaReinicios = 0;
    // Contador de tiempo
	private int temporizadorCero;
	private int minTiempo = 4;                  
	private int maxTiempo = 10;
	private int minContReinicios = 3;		// el contador no llega a 3 es imposible que sea cierto el condicional !!!!!
	private int maxContReinicios = 40;
    // Animaciones y Navegacion
    private UnityEngine.AI.NavMeshAgent agent;
	private Animation anim;

	public bool meEstoyMoviendo;			// marcos bool variable para saber si me muevo
	public int comoMeMuevo;
	public bool calculoMover;

	// ------------------------------------------------
	// INICIALIZA
	// ------------------------------------------------

	void Awake () 
	{
		bColisionaWaypoint = false;
		inicioWaypoint = GetComponent<ObjetoProximo> ().ordenaWaypoints ();	    // Almacena Waypoint inicial al que se mueve Meco en fase 2
 
		misWaypoints = GetComponent<ObjetoProximo> ().listaWaypoints;           // Lista ordenada inicial de Waypints
        numeroWaypoints = GetComponent<ObjetoProximo>().listaWaypoints.Length;  // Numero de waypoints
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();								    // NavMesh Agent para movimientos por escena
		anim = GetComponent<Animation>();                                       // Animaciones de Mecos
	}

	void Start()
	{
		buscaSiguiente(inicioWaypoint);										    // Inicializa busca waypoint al primero de la lista de waypointss
		// agent.SetDestination(misWaypoints[Random.Range(0,10)].transform.position);
	}

	// ------------------------------------------------
	// METODOS DE COLISIONES CON WAYPOINTS
	// ------------------------------------------------
	// ENTRA EN COLISION
	void OnTriggerEnter(Collider tr_Colision) 
	{
	
        indexPrimeroJND = GetComponent<ObjetoProximo>().listaWaypoints.Length - restaIndexPrimeroJND;
        indexUltimoJND = GetComponent<ObjetoProximo> ().listaWaypoints.Length - restaindexUltimoJND;

		foreach (GameObject go in misWaypoints) 
		{
            if (tr_Colision.tag == "waypoint"  && scriptFases.GetComponent<propiedadesGamev002>().fasesDelJuego == 2 && GetComponent<ObjetoProximo>().listaWaypoints.Length > 0) 
			{
				waypointTemp = GetComponent<ObjetoProximo>().waypointProximo;

				// MOVIMIENTO A WAYPOINTS - JUGADOR NO DESCUBIERTO
                indexWaypoint = Random.Range(indexPrimeroJND, indexUltimoJND);
                bColisionaWaypoint = true; 
			}
             
		}
		meEstoyMoviendo = false;				// code marcos
        tr_Colision.tag = "ocupado";
		entraAnimaSiObstaculo();

	}

	//SALE DE COLISION
	void OnTriggerExit(Collider tr_Colision) 
	{
		foreach (GameObject go in misWaypoints) 
		{
			bColisionaWaypoint = false;
            tr_Colision.gameObject.SetActive(true);
		}
       	// anim.CrossFade("correrNormal");
	    bMoviendoMeco = true;
		meEstoyMoviendo = true;
        tr_Colision.tag = "waypoint";
        saleAnimaSiObstaculo(); 
	}

    // ANIMACIONES **************************************************

    private void entraAnimaSiObstaculo() 
	{
		if (!meEstoyMoviendo && GetComponent<ObjetoProximo>().ordenaObstaculos().name.Substring(GetComponent<ObjetoProximo>().ordenaObstaculos().name.Length - 4, 4).Equals("bajo"))
		{
            // anim.CrossFade("acostarse");
			GetComponent<Animation>().CrossFade ("acostarse");
		}else{
			// anim.CrossFade("arrodillarse");
			GetComponent<Animation>().CrossFade ("arrodillarse");
		}
    }

    private void saleAnimaSiObstaculo()
    {
		if (!meEstoyMoviendo && GetComponent<ObjetoProximo>().ordenaObstaculos().name.Substring(GetComponent<ObjetoProximo>().ordenaObstaculos().name.Length - 4, 4).Equals("bajo"))
		{
            anim.CrossFade("levantarseAcostado");
		}else {
			anim.CrossFade("levantarseArrodillado");
		}
    }

    public void animaSiWaypointDcha() 
	{ 
		if(GetComponent<ControlMovimiento>().waypointTemp.name.Substring(GetComponent<ControlMovimiento>().waypointTemp.name.Length -4, 4).Equals("dcha"))
            anim.CrossFade("ladearDerArrodillado");
    }

    public void animaSiWaypointIzda() 
	{ 
		if(GetComponent<ControlMovimiento>().waypointTemp.name.Substring(GetComponent<ControlMovimiento>().waypointTemp.name.Length -4, 4).Equals("izda"))
            anim.CrossFade("ladearIzqArrodillado");
    }
    
    //***************************************************************


    // ------------------------------------------------
    // LISTA DE WAYPOINTS Almacena siguiente punto al que se mueve Meco
    // Index: parámetro que almacena el indice del array de Waypoints (JD - JND)
    //      indexPrimeroJD
    //      indexUltimoJD 
    //      indexPrimeroRestaJND
    //      indexUltimoRestaJND
    // ------------------------------------------------
    public void guardaSiguiente(int index) 
	{
		numeroWaypoints = GetComponent<ObjetoProximo>().listaWaypoints.Length;  // Numero de waypoints si la activo !!!!
        foreach (GameObject go in misWaypoints) 
		{
            if (go == GetComponent<ObjetoProximo>().listaWaypoints[0])
			{
                siguienteWaypoint = GetComponent<ObjetoProximo>().listaWaypoints[index];				// error de linea index out range!!!
            }
        }
    }

    // ------------------------------------------------
    // METODO ACTUALIZA SIGUIENTE WAYPOINT
    // ------------------------------------------------
    public void buscaSiguiente(GameObject wp) {

        int indexPrimeroJD = Random.Range(0,10); // Indica el elemento de la lista a partir del cual inicia movimiento

		int indexUltimoJD = Random.Range(15,25);  // Indica el ultimo elemento posible de la lista hacia el que se dirije el Meco
        if (GetComponent<ObjetoProximo>().distanciaJugadorProximo(GetComponent<ObjetoProximo>().ordenaJugadores())
            < GetComponent<ObjetoProximo>().distIniciaDisparo
		    && GetComponent<ObjetoProximo>().listaJugadores.Length > 0)
        {
            // Comprueba si existen waypoints y asigna al indice de la lista valores entre los primeros elementos
            indexWaypoint = Random.Range(indexPrimeroJD, indexUltimoJD);

        }
        // Mueve Meco a siguiente waypoint
        if (bMoviendoMeco) 
		{
            agent.SetDestination(wp.transform.position);
			meEstoyMoviendo = true;									// inserto marcos code
			calculoMover = true;
		}
    }

    public void moverseWaypointInicio(Vector3 ptodesplazamiento) 
	{
        if(!GetComponent<ObjetoProximo>().ordenaWaypoints().tag.Equals("ocupado"))
		{
        	agent.SetDestination(ptodesplazamiento);
		}
    }

	// ------------------------------------------------
	// METODOS DETIENE MOVIMIENTO PAUSAS ALEATORIAS
	// ------------------------------------------------
	private void contadorReinicios() 
	{
		aleatorioTemporizador = Random.Range (minContReinicios, maxContReinicios);
		// Cuenta las veces que temporizador =0 para realizar pausas en movimiento de Meco
		if(temporizadorCero == 0) cuentaReinicios = cuentaReinicios + 1.0f * Time.deltaTime;

		if (cuentaReinicios >= aleatorioTemporizador) {
			bMoviendoMeco = true;
			cuentaReinicios = 0;
		} else
			bMoviendoMeco = false;
	}

	public int temporizador() 
	{
		//El valor de Tiempo se va sumando por Time.deltaTime.
		tiempo = tiempo +1 * Time.deltaTime; 
		//Si el valor de Tiempo supera o es igual a 1.0 (que es igual a 1 segundo) se activara el bucle if.
		if (tiempo >= 1.0) {
			temporizadorCero -= 1;
			tiempo = 0; 		//Reseteamos el valor de Tiempo para que vuelva a crearse otro segundo.
		}
		if (temporizadorCero <= -1) temporizadorCero = Random.Range(minTiempo, maxTiempo);	// Determina el valor inicial del Temporizador (Aleatorio)
		return temporizadorCero;
	}

	// ------------------------------------------------
	// ACTUALIZA
	// ------------------------------------------------
	void Update () 
	{
		if (scriptFases.GetComponent<propiedadesGamev002>().fasesDelJuego == 2)  // quite || fase=1  marcos
        {
            temporizador();
            contadorReinicios();

			if(calculoMover) 
			{
				// random por si corren o andan
				calculoMover = false;
				comoMeMuevo = Random.Range(0,50);
			}

			if(meEstoyMoviendo && comoMeMuevo <= 25)
			{
				anim.CrossFade("caminarNormal");
				// print ("caminar normal");
				agent.speed = 30;
			}

			if(meEstoyMoviendo && comoMeMuevo > 25)
			{
				anim.CrossFade("correrNormal");
				// print ("correr normal");
				agent.speed = 60;
			}


            
            if (!GetComponent<ObjetoProximo>().ordenaWaypoints().tag.Equals("ocupado"))
            {
                guardaSiguiente(indexWaypoint);
                buscaSiguiente(siguienteWaypoint);
            }
		}
	}
}