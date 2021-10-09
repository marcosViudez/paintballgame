using UnityEngine;
using System.Collections;


/* CLASE PROXIMO OBJETO
 * ------------------------------------------------
 * Crea una lista ordenada de Objetos
 * Retorna el objeto mas cercano
 * Calcula distancia de disparo en rafaga
 * Retorna valor booleano de disparo
 ------------------------------------------------
*/

public class ObjetoProximo : MonoBehaviour, IComparer {

	// ------------------------------------------------
	// VARIABLES
	// ------------------------------------------------
    public GameObject scriptFases;
	// Variables Personajes
	public GameObject mecoMalo;
	//public GameObject sueloScript;

	public GameObject obstaculoProximo;			    // Obstaculo mas cercano
	public GameObject jugadorProximo;			    // Jugador mas cercano
	public GameObject waypointProximo;			    // Waypoint mas cercano

	// ------------------------------------------------
	// *** VARIABLES DE DITANCIA A JUGADOR Y GIRO DE DISPARO
	// ------------------------------------------------
	// VARIABLES DISTANCIA Y GIRO DE DISPARO
	public float distJugadorCercano;			    // Distancia entre Enemigo y Jugador
	public float velocidadRotacion;			        // *** MODIFICA NIVELES DE JUEGO
	// VARIABLES CONTROL DE DISPARO 
	// Inicia disparo dentro de rango aleatorio de distancia entre Meco y Jugador
	public float distIniciaDisparo;
	// Rango aleatorio a la que detecta jugador
    private int aleatorioDistIniDispMin = 800;      // *** MODIFICA NIVELES DE JUEGO
	private int aleatorioDistIniDispMax = 1300;	    // *** MODIFICA NIVELES DE JUEGO
	public bool bDistIniciaDisparo;				    // Indica si el Meco esta disparando *** DISTANCIA INICIA DISPARO
	// LISTAS DE OBJETOS
	// Listas de Objetos (Arrays y ArrayList)
	public GameObject[] listaJugadores;			    // Lista ordenada de Jugadores
	public GameObject[] listaObstaculos;		    // Lista ordenada de Obstaculos
	public GameObject[] listaWaypoints;			    // Lista ordenada de Waypoints
	public ArrayList listaObjetos;
    // CAMBIA JUGADOR
    public float distJugadorObstaculo;
    private float distMinJugadorObstaculo = 150f;
    public int indexJugadores;

 

	// ------------------------------------------------
	// INICIALIZA
	// ------------------------------------------------
	void Start() {
		// Aleatorio para inicio de disparo, entre un rango determinado
		distIniciaDisparo = Random.Range (aleatorioDistIniDispMin, aleatorioDistIniDispMax);
		velocidadRotacion = Random.Range(3,15);;
		bDistIniciaDisparo = false;
	}

	// ------------------------------------------------
	// METODO de la clase IComparer
	// ------------------------------------------------
	
	public int Compare(object a, object b) {
		GameObject aObject = (GameObject)a;
		GameObject bObject = (GameObject)b;
		
		Vector3 wPos = mecoMalo.transform.position;
		Vector3 aPos = aObject.transform.position;
		Vector3 bPos = bObject.transform.position;
		
		float aDist = (wPos - aPos).magnitude;
		float bDist = (wPos - bPos).magnitude;
		
		if (aDist > bDist) { 
			return 1;
		} else if (aDist < bDist) {
			return -1;
		} else
			return 0;
	}
	
	// ------------------------------------------------
	// METODO ORDENA OBJETOS
	// Inserta objetos en la lista y los ordena por posicion
	// ------------------------------------------------
	
	public GameObject ordenaJugadores() {
		
		listaObjetos = new ArrayList ();										// Crea un ArrayList

		GameObject[] Objetos = GameObject.FindGameObjectsWithTag ("player");	// Busca Objetos con la etiqueta Player

		// Inserta objetos en la Lista
		foreach(GameObject go in Objetos) {
			listaObjetos.Add(go);
		}

		ObjetoProximo cDistancia = new ObjetoProximo ();						// Crea un objeto de la clase ObjetoProximo
        cDistancia.mecoMalo = this.mecoMalo;									// Asigna GameObject instancia de clase
		listaObjetos.Sort (cDistancia);											// Ordena los objetos con respecto a Meco
		
		listaJugadores = new GameObject[listaObjetos.Count];					// Crea un nuevo Array con el numero de jugadores
		listaObjetos.CopyTo (listaJugadores);									// Rellena lista de Objetos con la lista de Jugadores

        if (listaJugadores.Length >= 0)
            return jugadorProximo = (GameObject)listaObjetos[indexJugadores];				// Devuelve el primer GameObjet del Array (El mas cercano)
        return this.gameObject;
	}
	
	// ------------------------------------------------
	// DISTANCIA JUGADOR MECO PROXIMO
	// Calcula la distancia entre el jugador y el enemigo
	// ------------------------------------------------
	
	public float distanciaJugadorProximo(GameObject prox) {
		distJugadorCercano = Vector3.Distance (mecoMalo.transform.position, prox.transform.position);
		return distJugadorCercano;
	}

    // ------------------------------------------------
    // DISTANCIA JUGADOR OBSTACULO PROXIMO
    // Calcula la distancia entre el jugador y el obstaculo cercano a meco
    // ------------------------------------------------

    public float distanciaJugadorObstaculo(GameObject obj) {
        distJugadorObstaculo = Vector3.Distance(obj.transform.position, ordenaObstaculos().transform.position);
        return distJugadorObstaculo;
    }

    // ------------------------------------------------
    // CAMBIA JUGADOR
    // Cambia jugador cuando este esta cerca de obstaculo de meco
    // ------------------------------------------------

    public void calculaDistanciaJugadorObstaculo() {
        if (listaJugadores.Length > 0)
        if (distanciaJugadorObstaculo(listaJugadores[0]) < distMinJugadorObstaculo)
            indexJugadores = 1;
        else indexJugadores = 0;
        //GetComponent<ControlDisparo>().bColisionaConObstaculo = true;
    }
	
	// ------------------------------------------------
	// METODO ORDENA OBJETOS
	// Inserta objetos en la lista y los ordena por posicion
	// ------------------------------------------------
	
	public GameObject ordenaObstaculos() {
		listaObjetos = new ArrayList ();											// Crea un ArrayList
		
		GameObject[] Objetos = GameObject.FindGameObjectsWithTag ("obstaculo");		// Busca Objetos con la etiqueta Obstacle
		
		foreach(GameObject go in Objetos) {
			listaObjetos.Add(go);
		}
		ObjetoProximo cDistancia = new ObjetoProximo ();							// Crea un objeto de la clase ObjetoProximo
		cDistancia.mecoMalo = this.mecoMalo;										// Asigna GameObject instancia de clase
		listaObjetos.Sort (cDistancia);												// Ordena los objetos con respecto a Meco
		
		listaObstaculos = new GameObject[listaObjetos.Count];						// Crea un nuevo Array con el numero de obstaculos
		listaObjetos.CopyTo (listaObstaculos);										// Rellena lista de Objetos con la lista de Obstaculos
		
		return obstaculoProximo = (GameObject)listaObjetos [0];						// Devuelve el primer GameObjet del Array
	}
	
	// ------------------------------------------------
	// METODO ORDENA OBJETOS
	// Inserta objetos en la lista y los ordena por posicion
	// ------------------------------------------------
	
	public GameObject ordenaWaypoints() {
		listaObjetos = new ArrayList ();											// Crea un ArrayList
		
		GameObject[] Objetos = GameObject.FindGameObjectsWithTag ("waypoint");		// Busca Objetos con la etiqueta Waypoint
		
		foreach(GameObject go in Objetos) {
			listaObjetos.Add(go);
		}
		ObjetoProximo cDistancia = new ObjetoProximo ();							// Crea un objeto de la clase ObjetoProximo
        cDistancia.mecoMalo = this.mecoMalo;										// Asigna GameObject instancia de clase
		listaObjetos.Sort (cDistancia);												// Ordena los objetos con respecto a Meco
		
		listaWaypoints = new GameObject[listaObjetos.Count];						// Crea un nuevo Array con el numero de waypoints
		listaObjetos.CopyTo (listaWaypoints);										// Rellena lista de Objetos con la lista de Waypoint
		
		return waypointProximo = (GameObject)listaObjetos [Random.Range(6,40)];						// Devuelve el primer GameObjet del Array
	}

	// ------------------------------------------------
	// METODO DISPARO (Apunta y gira)
	// 1. Decide si dispara rafaga segun distancia determinada
	// 2. Gira y apunta al objetivo
	// ------------------------------------------------

    public void bDisparoJugadorProximo(float dist)
    {
        // Reposo: Jugador fuera de la distancia establecida (Aleatorio)
        // Disparo: Jugador dentro de los limites de disparo (Aleatorio)
        if (dist < distIniciaDisparo)
        {
            bDistIniciaDisparo = true;
            giraMeco(ordenaJugadores());

        }
        else {
            bDistIniciaDisparo = false;
        }
    }

    // GIRA EL MECO HACIA JUGADOR

    public void giraMeco(GameObject obj) {
        Vector3 relativePos = obj.transform.position - mecoMalo.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, velocidadRotacion * Time.deltaTime);
    }

	void Update () {

		if (scriptFases.GetComponent<propiedadesGamev002>().fasesDelJuego == 2)
		{
        	if (listaJugadores.Length >= 0)
        	bDisparoJugadorProximo (distanciaJugadorProximo (ordenaJugadores()));
			ordenaObstaculos ();
			ordenaWaypoints ();
        	calculaDistanciaJugadorObstaculo();
		}
		// Inicia Movimiento
		/*
		if (scriptFases.GetComponent<propiedadesGamev002>().fasesDelJuego != 2)
        {
            GameObject goInicio = GameObject.Find("inicio_enemigo");
            transform.LookAt(goInicio.transform.position);
        }*/
	}
}
