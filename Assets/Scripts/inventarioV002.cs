using UnityEngine;
using System.Collections;

public class inventarioV002 : MonoBehaviour {

	public Texture2D pistola;
	public Texture2D pila;
	public Texture2D bola;
	public Texture2D meco;
	private int totalHuecos = 9; 

	public int x;
	public int y;
	public int z;
	public int t;


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI()
	{
		/*
		GUI.BeginGroup (new Rect (10, 60, 90, 430));			// grupo de los jugadores
			GUI.Box (new Rect (0, 0, 90, 430), "");				// caja de los jugadores
				GUI.Button (new Rect (5, 5, 80, 80), "");		// boton jugador 01
				GUI.Button (new Rect (5, 90, 80, 80), "");		// boton jugador 01
				GUI.Button (new Rect (5, 175, 80, 80), "");		// boton jugador 01
				GUI.Button (new Rect (5, 260, 80, 80), "");		// boton jugador 01
				GUI.Button (new Rect (5, 345, 80, 80), "");		// boton jugador 01
		GUI.EndGroup ();
		*/
		GUI.BeginGroup (new Rect (105 - 80, 30, 500, 500));			// grupo del inventario
			GUI.Box (new Rect (0, 0, 500, 500), "");			// cajon del inventario
			GUI.Box (new Rect (5, 5, 490, 30), "");				// titulo del inventario
			GUI.Box (new Rect (395, 465, 100, 30), "");			// caja del dinero
			GUI.Label (new Rect (400, 470, 100, 30), " 0 $");	// dinero total del equipo
		GUI.EndGroup ();

		GUI.Box (new Rect (110 - 80, 70, 260, 180), pistola);		// fondo de pistola 
		GUI.Box (new Rect (120 - 80,140,74,30), "");					// caño
		GUI.Box (new Rect (172 - 80,92,75,38), "");					// portabolas
		GUI.Box (new Rect (201 - 80,125,22,18), "");					// acople
		GUI.Box (new Rect (194 - 80,140,90,70), "");					// marcadora
		GUI.Box (new Rect (194 - 80,210,22,20), "");					// codo
		GUI.Box (new Rect (214 - 80,210,35,20), "");					// latiguillo
		GUI.Box (new Rect (248 - 80,210,50,25), "");					// valvula
		GUI.Box (new Rect (295 - 80,196,71,48), "");					// gas
		GUI.Box (new Rect (275 - 80,84,85,50), "");					// gas extra
		GUI.Box (new Rect (118 - 80,185,30,30), bola);				// bolas
		GUI.Box (new Rect (155 - 80,185,30,30), pila);				// pila

		GUI.Box (new Rect (380 - 80, 70, 220, 180), meco);			// fondo de meco y cinturon 
		GUI.Box (new Rect (418 - 80,76,75,68), "");					// cabeza
		GUI.Box (new Rect (415 - 80,143,90,40), "");					// camisa
		GUI.Box (new Rect (426 - 80,180,70,40), "");					// pantalon
		GUI.Box (new Rect (388 - 80,150,28,26), "");					// mano izquierda
		GUI.Box (new Rect (412 - 80,217,42,20), "");					// pie izquierda
		GUI.Box (new Rect (500 - 80,146,28,26), "");					// mano derecha
		GUI.Box (new Rect (466 - 80,217,42,20), "");					// pie derecha

		GUI.Box (new Rect (528 - 80,96,65,20), "");					// pods 01
		GUI.Box (new Rect (528 - 80,117,65,20), "");					// pods 01
		GUI.Box (new Rect (528 - 80,138,65,20), "");					// pods 01
		GUI.Box (new Rect (528 - 80,159,65,20), "");					// pods 01
		GUI.Box (new Rect (528 - 80,180,65,20), "");					// pods 01
		GUI.Box (new Rect (528 - 80,201,65,20), "");					// pods 01

		GUI.BeginGroup (new Rect (110 - 80, 260, 490, 230));			// grupo mochila del jugador
			GUI.Box (new Rect (0,0,490,230), "");				// cajon mochila del jugador
		GUI.EndGroup ();

		GUI.Box (new Rect (110 - 80,495,384,30), "");				// mensajes de informacion


		// huecos del inventario de los jugadores
		for(int filas = 0; filas < totalHuecos; filas++)
		{
			if(totalHuecos < 10)
			{
				GUI.Box (new Rect (115 - 80 + (50 * filas) + (filas * 4),265,50,50), "");
				GUI.Box (new Rect (115 - 80 + (50 * filas) + (filas * 4),319,50,50), "");
				GUI.Box (new Rect (115 - 80 + (50 * filas) + (filas * 4),373,50,50), "");
				GUI.Box (new Rect (115 - 80 + (50 * filas) + (filas * 4),427,50,50), "");
			}
		}

		GUI.Button (new Rect (610 - 80, 210, 90, 40), "Comprar");	// boton de comprar objetos
		GUI.Button (new Rect (610 - 80, 260, 90, 40), "Vender");		// boton de vender objetos

		GUI.BeginGroup (new Rect (705 - 80, 30, 290, 500));
		GUI.Box (new Rect (0,0,290,500), "");
		GUI.EndGroup ();

	}
}
