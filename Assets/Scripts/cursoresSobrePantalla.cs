using UnityEngine;
using System.Collections;

public class cursoresSobrePantalla : MonoBehaviour {

	public int cursorElegido;

	public Texture2D cursorDefault;
	public Texture2D cursorMoverseAbierto;
	public Texture2D cursorMoverseCerrado;
	public Texture2D cursorMiraVerde;
	public Texture2D cursorMiraRoja;
	public Texture2D cursorProhibido;
	public Texture2D cursorMueveDisparoAbierto;
	public Texture2D cursorMueveDisparoCerrado;
	public Texture2D marcador;
	public Texture2D buenosMecos;
	public Texture2D malosMecos;
	public Texture2D manchasMecos;


	public int x;

	public bool marcadorActivado = false;		// marcador de puntuacion
		
	public int roundsDerecha = 0;
	public int roundsIzquierda = 0;
	public string equipoIzquierda = "";
	public string equipoDerecha = "";
	
	public string coloresTextos = "relojCustom";

	// reloj parcial variables
	private string contadorParcial;
	private float tiempoPruebaParcial;
	private int parcialTime;
	private int minutesParciales;
	private int secondsParciales;
	private int parcialSegundosAtras;
	private int parcialMinutosAtras;
	public bool relojParadoParcial = true;

	// reloj total variables
	private string contadorTotal;
	private float tiempoPruebaTotal;
	private int totalTime;
	private int minutesTotales;
	private int secondsTotales;
	private int totalSegundosAtras;
	private int totalMinutosAtras;
	private bool relojParadoTotal = true;
	
	public GUISkin skinFuenteMarcador;

	// Use this for initialization
	void Start () 
	{
		valoresRelojInicialParcial ();
		valoresRelojInicialTotal ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		marcadorTiempos ();

		if(!relojParadoParcial)
		{
			contandoRelojParcial ();
		}

		limites();
	}

	void limites()
	{
		// detenemos el reloj para que suene la bocina de final de partida
		if(parcialMinutosAtras <= 0 && parcialSegundosAtras <= 0)
		{
			parcialMinutosAtras = 0;
			parcialSegundosAtras = 0;
			minutesParciales = 0;
			secondsParciales = 0;
		}
	}

	void valoresRelojInicialParcial()
	{
		parcialMinutosAtras = 5;
		parcialSegundosAtras = 0;
	}

	void valoresRelojInicialTotal()
	{
		totalMinutosAtras = 20;
		totalSegundosAtras = 0;
	}

	void formatoRelojTotal ()
	{
		minutesTotales = Mathf.FloorToInt(totalTime % 3600) / 60;
		secondsTotales = Mathf.FloorToInt(totalTime % 3600) % 60;
		
		totalMinutosAtras = 19 - minutesTotales;
		totalSegundosAtras = 59 - secondsTotales;
	}

	void formatoRelojParcial ()
	{
		minutesParciales = Mathf.FloorToInt(parcialTime % 3600) / 60;
		secondsParciales = Mathf.FloorToInt(parcialTime % 3600) % 60;
		
		parcialMinutosAtras = 4 - minutesParciales;
		parcialSegundosAtras = 59 - secondsParciales;
	}

	void contandoRelojTotal()
	{
		tiempoPruebaTotal += 1.0f * Time.deltaTime;
		totalTime = (int)tiempoPruebaTotal;
	}

	void contandoRelojParcial()
	{
		tiempoPruebaParcial += 1.0f * Time.deltaTime;
		parcialTime = (int)tiempoPruebaParcial;
	}

	void resetearTotales()
	{
		totalTime = 0;
		tiempoPruebaTotal = 0.0f;
	}

	void resetearParciales()
	{
		parcialTime = 0;
		tiempoPruebaParcial = 0.0f;
	}

	void marcadorTiempos()
	{
		if(GetComponent<propiedadesGamev002>().fasesDelJuego == 2)
		{
			// arranca tiempo total y parcial, sonando primero la bocina
			if(parcialMinutosAtras <=0 && parcialSegundosAtras <=0)
			{
				relojParadoParcial = true;   // quitar cuando se pare el juego
				GetComponent<propiedadesGamev002>().fasesDelJuego = 3;
			}else{
				relojParadoParcial = false;
			}
			formatoRelojParcial ();
			// formatoRelojTotal ();		// desactivado en la demo
		}
	}

	/// <summary>
	/// tipo de cursores segun donde estes en la interface
	/// </summary>
	/// <param name="tipo"></param>
	void cambioDeCursores(int tipo)
	{
		switch(tipo)
		{
		case 0:
			cursorElegido = 0;		// cursor flecha
			break;
		case 1:
			cursorElegido = 1;		// cursor abierto verde
			break;
		case 2:
			cursorElegido = 2;		// cursor cerrado verde
			break;
		case 3:
			cursorElegido = 3;		// cursor pto mira verde
			break;
		case 4:
			cursorElegido = 4;		// cursor pto mira rojo
			break;
		case 5:
			cursorElegido = 5;		// cursor prohibido
			break;
		case 6:
			cursorElegido = 6;		// cursor moverse disparo abierto
			break;
		case 7:
			cursorElegido = 7;		// cursor moverse disparo cerrado
			break;
		default:
			break;
		}
	}

	void OnGUI()
	{
		GUI.skin = skinFuenteMarcador;
		GUI.depth = -1;						// profundidad del raton para pasar por encima de los botones

		if(marcadorActivado == true)
		{

			// mecos tocados durante el encuentro
			GUI.DrawTexture(new Rect((Screen.width/2) - 200,0,30,30),buenosMecos,ScaleMode.StretchToFill,true,1.0f);		// mecos buenos
			GUI.DrawTexture(new Rect((Screen.width/2) - 170,0,30,30),buenosMecos,ScaleMode.StretchToFill,true,1.0f);		// mecos buenos
			GUI.DrawTexture(new Rect((Screen.width/2) - 140,0,30,30),buenosMecos,ScaleMode.StretchToFill,true,1.0f);		// mecos buenos
			GUI.DrawTexture(new Rect((Screen.width/2) - 110,0,30,30),buenosMecos,ScaleMode.StretchToFill,true,1.0f);		// mecos buenos
			GUI.DrawTexture(new Rect((Screen.width/2) - 80,0,30,30),buenosMecos,ScaleMode.StretchToFill,true,1.0f);		// mecos buenos
			
			GUI.DrawTexture(new Rect((Screen.width/2) + 34,0,30,30),malosMecos,ScaleMode.StretchToFill,true,1.0f);		// mecos malos
			GUI.DrawTexture(new Rect((Screen.width/2) + 64,0,30,30),malosMecos,ScaleMode.StretchToFill,true,1.0f);		// mecos malos
			GUI.DrawTexture(new Rect((Screen.width/2) + 94,0,30,30),malosMecos,ScaleMode.StretchToFill,true,1.0f);		// mecos malos
			GUI.DrawTexture(new Rect((Screen.width/2) + 124,0,30,30),malosMecos,ScaleMode.StretchToFill,true,1.0f);		// mecos malos
			GUI.DrawTexture(new Rect((Screen.width/2) + 154,0,30,30),malosMecos,ScaleMode.StretchToFill,true,1.0f);		// mecos malos

			if(GetComponent<propiedadesGamev002>().jugadores[0].tag == "muerto")
			{
				GUI.DrawTexture(new Rect((Screen.width/2) - 200,0,30,30),manchasMecos,ScaleMode.StretchToFill,true,1.0f);	// mecos buenos
			}

			if(GetComponent<propiedadesGamev002>().jugadores[1].tag == "muerto")
			{
				GUI.DrawTexture(new Rect((Screen.width/2) - 170,0,30,30),manchasMecos,ScaleMode.StretchToFill,true,1.0f);	// mecos buenos
			}

			if(GetComponent<propiedadesGamev002>().jugadores[2].tag == "muerto")
			{
				GUI.DrawTexture(new Rect((Screen.width/2) - 140,0,30,30),manchasMecos,ScaleMode.StretchToFill,true,1.0f);	// mecos buenos
			}

			if(GetComponent<propiedadesGamev002>().jugadores[3].tag == "muerto")
			{
				GUI.DrawTexture(new Rect((Screen.width/2) - 110,0,30,30),manchasMecos,ScaleMode.StretchToFill,true,1.0f);	// mecos buenos
			}

			if(GetComponent<propiedadesGamev002>().jugadores[4].tag == "muerto")
			{
				GUI.DrawTexture(new Rect((Screen.width/2) - 80,0,30,30),manchasMecos,ScaleMode.StretchToFill,true,1.0f);	// mecos buenos
			}

			// malos mancha
			if(GetComponent<propiedadesGamev002>().enemigos[0].tag == "muerto")
			{
				GUI.DrawTexture(new Rect((Screen.width/2) + 36,0,30,30),manchasMecos,ScaleMode.StretchToFill,true,1.0f);	// mecos malos
			}
				
			if(GetComponent<propiedadesGamev002>().enemigos[1].tag == "muerto")
			{
				GUI.DrawTexture(new Rect((Screen.width/2) + 66,0,30,30),manchasMecos,ScaleMode.StretchToFill,true,1.0f);	// mecos malos
			}

			if(GetComponent<propiedadesGamev002>().enemigos[2].tag == "muerto")
			{
				GUI.DrawTexture(new Rect((Screen.width/2) + 96,0,30,30),manchasMecos,ScaleMode.StretchToFill,true,1.0f);	// mecos malos
			}

			if(GetComponent<propiedadesGamev002>().enemigos[3].tag == "muerto")
			{
				GUI.DrawTexture(new Rect((Screen.width/2) + 126,0,30,30),manchasMecos,ScaleMode.StretchToFill,true,1.0f);	// mecos malos
			}

			if(GetComponent<propiedadesGamev002>().enemigos[4].tag == "muerto")
			{
				GUI.DrawTexture(new Rect((Screen.width/2) + 156,0,30,30),manchasMecos,ScaleMode.StretchToFill,true,1.0f);	// mecos malos
			}


			GUI.DrawTexture(new Rect((Screen.width/2)-300,5,600,100),marcador,ScaleMode.StretchToFill,true,1.0f);
			GUI.Label(new Rect(Screen.width/2 + 154,37,30,30),roundsDerecha.ToString(),"labelCustom");
			GUI.Label(new Rect(Screen.width/2 - 183,37,30,30),roundsIzquierda.ToString(),"labelCustom");
			
			GUI.Label(new Rect(Screen.width/2 - 149,45,30,30),equipoIzquierda.ToString(),"textoCustom");
			GUI.Label(new Rect(Screen.width/2 + 30,45,30,30),equipoDerecha.ToString(),"textoCustom");

			// reloj parcial
			GUI.Label(new Rect((Screen.width/2) - 25, 16, 20, 20),string.Format("{0:00}:{1:00}", parcialMinutosAtras, parcialSegundosAtras),"relojCustom");
			// reloj total
			GUI.Label(new Rect((Screen.width/2) - 25, 72, 20, 20),string.Format("{0:00}:{1:00}", totalMinutosAtras, totalSegundosAtras),"relojCustom");
		}
		
		if(cursorElegido == 0)
		{
			GUI.DrawTexture(new Rect(Input.mousePosition.x - 6,Screen.height - Input.mousePosition.y - 4,16,16),cursorDefault,ScaleMode.StretchToFill,true,1.0f);
		}
		if(cursorElegido == 1)
		{
			GUI.DrawTexture(new Rect(Input.mousePosition.x - 6,Screen.height - Input.mousePosition.y - 4,16,16),cursorMoverseAbierto,ScaleMode.StretchToFill,true,1.0f);
		}
		if(cursorElegido == 2)
		{
			GUI.DrawTexture(new Rect(Input.mousePosition.x - 6,Screen.height - Input.mousePosition.y - 4,16,16),cursorMoverseCerrado,ScaleMode.StretchToFill,true,1.0f);
		}
		if(cursorElegido == 3)
		{
			GUI.DrawTexture(new Rect(Input.mousePosition.x - 6,Screen.height - Input.mousePosition.y - 4,16,16),cursorMiraVerde,ScaleMode.StretchToFill,true,1.0f);
		}
		if(cursorElegido == 4)
		{
			GUI.DrawTexture(new Rect(Input.mousePosition.x - 6,Screen.height - Input.mousePosition.y - 4,16,16),cursorMiraRoja,ScaleMode.StretchToFill,true,1.0f);
		}
		if(cursorElegido == 5)
		{
			GUI.DrawTexture(new Rect(Input.mousePosition.x - 6,Screen.height - Input.mousePosition.y - 4,16,16),cursorProhibido,ScaleMode.StretchToFill,true,1.0f);
		}
		if(cursorElegido == 6)
		{
			GUI.DrawTexture(new Rect(Input.mousePosition.x - 6,Screen.height - Input.mousePosition.y - 4,16,16),cursorMueveDisparoAbierto,ScaleMode.StretchToFill,true,1.0f);
		}
		if(cursorElegido == 7)
		{
			GUI.DrawTexture(new Rect(Input.mousePosition.x - 6,Screen.height - Input.mousePosition.y - 4,16,16),cursorMueveDisparoCerrado,ScaleMode.StretchToFill,true,1.0f);
		}
	}
}
