using UnityEngine;
using System.Collections;

public class interfaceDeBotones : MonoBehaviour {

	public GUISkin botonesInterface;
	
	public Texture2D interfaceDerecha;
	public Texture2D interfaceIzquierda;

	public bool menuAccionesAbiertoCodigo;
	
	private int tamx = 100;
	private int tamy = 200;
	
	public Texture2D[] accionesTextura = new Texture2D[10];
	public Texture2D[] accionesTexturaRojos = new Texture2D[6];
	public Rect[] botonesAcciones = new Rect[10];

	void OnGUI()
	{
		GUI.skin = botonesInterface;	// mi estilo de botones
		
		if(menuAccionesAbiertoCodigo)
		{
			GUI.depth = 2;
			// dibujamos las dos interfaces de juego
			GUI.DrawTexture(new Rect((Screen.width/2-tamx/2) + 100,Screen.height/2-tamy/2,tamx,tamy),interfaceDerecha,ScaleMode.StretchToFill,true,1.0f);
			GUI.DrawTexture(new Rect((Screen.width/2-tamx/2) - 100,Screen.height/2-tamy/2,tamx,tamy),interfaceIzquierda,ScaleMode.StretchToFill,true,1.0f);
			
			// botones de interface de la parte izquierda
			// GUI.Button(Rect(posicion_x , posicion_y, tamaño_x, tamaño_y), texto, aspecto)
			GUI.Button(new Rect(Screen.width/2 - 142,Screen.height/2 - 130,40,40),accionesTextura[0],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 - 171,Screen.height/2 - 80,40,40),accionesTextura[1],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 - 175,Screen.height/2 - 26,40,40),accionesTextura[2],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 - 171,Screen.height/2 + 28,40,40),accionesTextura[3],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 - 142,Screen.height/2 + 80,40,40),accionesTextura[4],"botonesDeInterface");
			// botones de interface de la parte derecha
			GUI.Button(new Rect(Screen.width/2 + 102,Screen.height/2 - 130,40,40),accionesTextura[5],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 + 131,Screen.height/2 - 80,40,40),accionesTextura[6],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 + 136,Screen.height/2 - 26,40,40),accionesTextura[7],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 + 131,Screen.height/2 + 28,40,40),accionesTextura[8],"botonesDeInterface");
			GUI.Button(new Rect(Screen.width/2 + 102,Screen.height/2 + 80,40,40),accionesTextura[9],"botonesDeInterface");
		}
	}
}
