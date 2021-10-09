using UnityEngine;
using System.Collections;

/* CLASE CONTROL DISPARO
 * ------------------------------------------------
 * METODOS PARA CONTROL DE DISPARO (descripcion)
 * ------------------------------------------------
 * Modifica frecuencia de disparo. 
 * Rango mayor => menos posibilidades de que numero predefinido coincida (Menos disparos por rafaga) JUGADOR NO DESCUBIERTO
 * Rango menor => mayor posibilidades de que numero predefinido coincida (Mas disparos por rafaga) JUGADOR DESCUBIERTO
 * Temporizador para rafaga aleatorio
 * * 	cuentaReinicios (Rango)Impar false (No dispara)
 * * 	cuentaReinicios (Rango)Par true (dispara)
 * Calcula aleatorios
 * * 	Disparos a intervalos aleatorios, dispara si variable igual a numero predefinido
 * * 	Modifica rango de aleatorio --> Mayor o menor posibilidades de coincidencia de numero
*/

public class ControlDisparo : MonoBehaviour {

	// ------------------------------------------------
	// VARIABLES
	// ------------------------------------------------
	
    public GameObject scriptFases;
	public AudioClip sonidoDisparo;
	public GameObject particulaGasMalos;

    // Mascara de obstaculos
    public LayerMask mascara = 1 << 8;
	// VARIABLES DE LA PISTOLA
	public Transform bocaDisparo;				// Punto de salida de la Bola ** Cambia en Proyecto Marcos
	public Rigidbody bola;						// Bola de disparo ** Cambia en proyecto Marcos
	private int fuerza = 24;					// Velocidad de la Bola en el disparo
	// VARIABLES CONTROL BOLAS
	public int totalBolas;
	public int cuentaBolas;
	// NUMERO DE BOLAS POR CADA RAFAGA *** MODIFICA CANTIDAD DE BOLAS EN CADA RAFAGA
	// ------------------------------------------------
	private int aleatorioBolas;					// *** MODIFICA NIVELES DE JUEGO
	private int minBolasDisparo = 10;		    // Numero aleatorio de bolas en cada rafaga
	private int maxBolasDisparo = 22;		
	// VARIABLES DE CONTROL
	public bool bColisionaConObstaculo = false;	// Detiene disparo si colisiona con Obstaculo									
	public bool bDescubiertoJugador = false;	// Muestra booleana true si descubierto jugador
	private Rigidbody clone;					// Variable clonar bola ** Cambia en Proyecto Marcos
	// VARIABLES DE DISPARO *** MODIFICA VELOCIDAD DE RAFAGAS
	// ------------------------------------------------
	private int aleatorioDisparo;				// Aleatorio para iniciar disparo			
	public int rangoFrecuenciaDisparo;		   	// *** MODIFICA NIVELES DE JUEGO en metodo descubiertoJugador
	// VARIABLES DE TEMPORIZADOR
	// Realiza pausas aleatorias entre refagas 
	private float tiempo = 0;
	private int temporizadorReinicios;
	private float cuentaReinicios = 0;
	public bool bTemporizador;					// Indica si temporizador esta a cero	
    private int minTiempo;
	private int maxTiempo;						
	private int minContReinicios;
	private int maxContReinicios;
	private int aleatorioTemporizador;			// Detiene disparo si temporizador = 0

    public GameObject objetivoJugadorSiObstaculo;
    public float distJugadorProxObstaculo;
	private int radioFuegoEnemigo = 10;
	private int rafagaMax = 4;
	private int rangFrecDisparo;

	// ------------------------------------------------
	// INICIALIZA
	// ------------------------------------------------
	void Awake() {
		// Destruye bolas
		// Autodestruir destruyeBolas = Bola.GetComponent<Autodestruir>();
		// Inicializa Total Bolas
		totalBolas = 200;
		cuentaBolas = 0;
		bTemporizador = false;
		rangFrecDisparo = Random.Range(30,60);
	}

	// ------------------------------------------------
	// GETTERS Y SETTERS BOOLEAN
	// ------------------------------------------------
	public void setBTemporizador (bool bTemp) {
		this.bTemporizador = bTemp;
	}
	
	public bool getBTemporizador() {
		return bTemporizador;
	}

	public void controlDisparo() {
		modificaRangosTemporizador ();
		contadorReinicios ();
	}

	// ------------------------------------------------
	// METODO COLISION BOLA
	// Detiene disparo si existe colision con Obstaculo en el que se oculta el Meco
	// ------------------------------------------------
	public bool colisionBolaConObstaculoProximo() {
        // Rayo de colision
        RaycastHit hit;
        // devuelve true si existe colision de Meco (Boca disparo) con obstaculo - NO DISPARA
        if (Physics.Raycast(bocaDisparo.transform.position, transform.forward, out hit, GetComponent<ObjetoProximo>().distIniciaDisparo, mascara))
        {
            if (hit.transform.gameObject == GetComponent<ObjetoProximo>().ordenaObstaculos()) 
               // && hit.transform.gameObject != GameObject.Find("player"))
            {
                bColisionaConObstaculo = true;
                rangoFrecuenciaDisparo = 400;
            }
            else
                if (GetComponent<ObjetoProximo>().distJugadorCercano < GetComponent<ObjetoProximo>().distIniciaDisparo)
                {
                    // INICIA DISPARO
                    bColisionaConObstaculo = false;
                    GetComponent<ObjetoProximo>().bDistIniciaDisparo = true;
                    bDescubiertoJugador = true;
					rangoFrecuenciaDisparo = rangFrecDisparo;

                }
                else
                {
                    bDescubiertoJugador = false;
                    rangoFrecuenciaDisparo = 400;
                }
        }
        return bColisionaConObstaculo;
	}

	// ------------------------------------------------
	// METODO COLISION JUGADOR
	// Aumenta o disminuye frecuencia de disparo si el Jugador es descubierto y dentro de rango definido
	// ------------------------------------------------
	public void descubiertoJugador() {
		// Rayo de colision
		RaycastHit hit;
        if (Physics.Raycast(bocaDisparo.transform.position, transform.forward, out hit, GetComponent<ObjetoProximo>().distIniciaDisparo, mascara)) {
			bDescubiertoJugador = false;
			rangoFrecuenciaDisparo = 400;			// Aumenta rango de intervalo entre disparo *** MODIFICA NIVELES DE JUEGO
													// Disminuye la frecuencia de disparo, menor probabilidad de coincidencia con numero aleatorio MENOS DISPAROS
		} else {
			bDescubiertoJugador = true;
			//bTemporizadorPausa = false;
			rangoFrecuenciaDisparo = rangFrecDisparo;			// Disminuye rango de intervalo entre disparo *** MODIFICA NIVELES DE JUEGO
													// Aumenta la frecuencia de disparo, mayor probabilidad de coincidencia con numero aleatorio MAS DISPAROS
		}
	}

	// ------------------------------------------------
	// METODO DISPARA RAFAGA
	// Dispara si Jugador se encuentra dentro de una distancia minima (en un rango aleatorio)
	// ------------------------------------------------
	public void disparo() {
        //float indiceJugador;
        if (!colisionBolaConObstaculoProximo())
        {
            if (GetComponent<ObjetoProximo>().bDistIniciaDisparo == true
                && aleatorioDisparo == 6 && !GameObject.Find("enemigo")) {
                if (getBTemporizador()) 
				{
					// code marcos vario la linea de disparo para que varie y no vayan al mismo pto todas las bolas
					for(int i=0;i<rafagaMax;i++){
					clone = Instantiate(bola, bocaDisparo.position + new Vector3(Random.Range(0,radioFuegoEnemigo),Random.Range(0,radioFuegoEnemigo),Random.Range(0,radioFuegoEnemigo)), bocaDisparo.rotation) as Rigidbody;	// Instancia Bolas
												GetComponent<AudioSource>().PlayOneShot(sonidoDisparo);
												Instantiate(particulaGasMalos,bocaDisparo.position,bocaDisparo.rotation);
					// AudioSource.PlayClipAtPoint(sonidoDisparo,transform.position);
					
												// GetComponent<AudioSource>().PlayOneShot(sonidoDisparo);
					GetComponent<CargadoresMalos>().restaBolas();
					}
                }
            }
        } 
	}

	// CALCULO DE VALORES ALEATORIOS
	// ------------------------------------------------
	private void calculoAleatorios() {
		// MECO DISPARA
		aleatorioDisparo = Random.Range (1, rangoFrecuenciaDisparo); // *** MODIFICA NIVELES DE JUEGO
		// NUMERO DE BOLAS 
		aleatorioBolas = Random.Range (minBolasDisparo, maxBolasDisparo);
		// NUMERO DE PAUSAS ENTRE RAFAGA
		aleatorioTemporizador = Random.Range (minContReinicios, maxContReinicios);

	}

    // ------------------------------------------------
    // METODO MODIFICA VALORES RANGO TEMPORIZADOR
    // ------------------------------------------------
    public void modificaRangosTemporizador()
    {
        calculoAleatorios();
        if (bDescubiertoJugador)
        {
            minContReinicios = 0;
            maxContReinicios = 0;
            minTiempo = 0;
            maxTiempo = 0;
        }
        else
        {
            minContReinicios = 4;
            maxContReinicios = 10;
            minTiempo = 6;
            maxTiempo = 18;
        }
    }

	// CONTADOR DE REINICIOS
	// Tiempo de espera entre rafagas
	// ------------------------------------------------
	private void contadorReinicios() {
		// Cuenta las veces que temporizador =0 para realizar pausas en disaparo de Meco
		// Solo activo cuando el Jugador este detras de un Obstaculo
		if(temporizadorReinicios == 0) cuentaReinicios = cuentaReinicios + 1.0f * Time.deltaTime;
		if (cuentaReinicios > aleatorioTemporizador * 2) {
			setBTemporizador (true);
			//setBDetieneBolas(false);
			cuentaReinicios = 0;
		} else {
			setBTemporizador (false);
		}
	}

	// CONTADOR ESPERA
	// ------------------------------------------------
	private int temporizador() {
		//El valor de Tiempo se va sumando por Time.deltaTime.
		tiempo = tiempo +1 * Time.deltaTime; 
		//Si el valor de Tiempo supera o es igual a 1.0 (que es igual a 1 segundo) se activara el bucle if.
		if (tiempo >= 1.0) {
			temporizadorReinicios -= 1;
			tiempo = 0; 		//Reseteamos el valor de Tiempo para que vuelva a crearse otro segundo.
		}
		if (temporizadorReinicios <= -1) temporizadorReinicios = Random.Range(minTiempo, maxTiempo);	// Determina el valor inicial de Contador (Aleatorio)
		return temporizadorReinicios;
	}

	// ------------------------------------------------
	// ACTUALIZA
	// ------------------------------------------------
	void Update() {
        temporizador();
         
				if (scriptFases.GetComponent<propiedadesGamev002>().fasesDelJuego == 2 && gameObject.tag != "muerto") {
            disparo();
            controlDisparo();
        }
        //print("fase del juego" +  scriptFases.GetComponent<PropiedadesGame>().fasesDelJuego);
	}
}
