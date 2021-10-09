using UnityEngine;
using System.Collections;

public class pruebasReloj : MonoBehaviour {

	public GUISkin skinFuenteMarcador;

	private float tiempoPrueba;
	private int parcialTime;
	private int minutesParciales;
	private int secondsParciales;
	private int parcialSegundosAtras;
	private int parcialMinutosAtras;
	private string contadorParcial;
	private bool relojParado;

	// Update is called once per frame
	void Update () 
	{
		formatoRelojParcial ();

		if(!relojParado)
		{
			contandoRelojParcial ();
		}else{
			resetearParciales();
			parcialMinutosAtras = 5;
			parcialSegundosAtras = 0;
		}
	}

	void formatoRelojParcial ()
	{
		minutesParciales = Mathf.FloorToInt(parcialTime % 3600) / 60;
		secondsParciales = Mathf.FloorToInt(parcialTime % 3600) % 60;

		parcialMinutosAtras = 4 - minutesParciales;
		parcialSegundosAtras = 59 - secondsParciales;
	}

	void contandoRelojParcial()
	{
		tiempoPrueba += 1.0f * Time.deltaTime;
		parcialTime = (int)tiempoPrueba;
	}

	void resetearParciales()
	{
		parcialTime = 0;
		tiempoPrueba = 0.0f;
	}

	void OnGUI()
	{
		GUI.skin = skinFuenteMarcador;

		if(GUI.Button(new Rect(200,0,120,30)," reset parar "))
		{
			relojParado = true;
		}

		if(GUI.Button(new Rect(200,40,120,30)," reset marcha "))
		{
			parcialMinutosAtras = 4;
			parcialSegundosAtras = 59;
			relojParado = false;
		}

		GUI.Label(new Rect(0, 30, 500, 40),string.Format("{0:00}:{1:00}", parcialMinutosAtras, parcialSegundosAtras),"relojCustom");
	}
}
