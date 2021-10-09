using UnityEngine;
using System.Collections;

public class tiendaV001 : MonoBehaviour {

	public int posRanuraX = 858;
	public int posRanuraY;
	public int tamRanuraX = 50;
	public int tamRanuraY = 50;
	public int filas = 2;
	public int columnas = 4;
	public int totalHuecos;
	public int espaciadoHuecos = 4;
	public int posInventarioX;
	public int posInventarioY;

	public Vector2[] botones;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		totalHuecos = filas * columnas;
		botones = new Vector2[totalHuecos];
		crearRanuras ();
	}

	void crearRanuras()
	{
		int j = 0;
		for(int i=0; i < totalHuecos; i++)
		{
			if(i > columnas - 1)
			{
				if(i%columnas == 0)
				{
					j++;
				}
			}

			posRanuraX = (i % columnas * tamRanuraX) + ((i % columnas - 1) * espaciadoHuecos) + posInventarioX;
			posRanuraY = posInventarioY + espaciadoHuecos + (j * (tamRanuraY + espaciadoHuecos));

			botones[i].x = posRanuraX;
			botones[i].y = posRanuraY;
		}
	}

	void OnGUI()
	{
		for(int k = 0; k < totalHuecos; k++)
		{
			GUI.Button(new Rect(botones[k].x,botones[k].y,tamRanuraX,tamRanuraY),"");
		}
	}
}
