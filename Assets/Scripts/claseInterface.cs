using UnityEngine;
using System.Collections;

public class claseInterface : MonoBehaviour {
	
	public string nombreJugador;
	public bool estoyEliminado;
	
	public Texture2D texturaFoto;					// imagen de interface de vidas
	public Texture2D barraVerdeCO2;
	public Texture2D barraRojaCO2;
	public Texture2D barraAmarillaBolas;
	public Texture2D barraNegraBolas; 
	
	public int numeroBolasCargador;
	public int numeroPods;
	public int[] bolasCargador = new int[5];
	public int numeroCargadorUsado = 4;
	public int numeroBolasAmarillas;
	public int numeroBolasRojas;
	public int numeroBolasAzules;
	public int numeroBolasVerdes;
	public int numeroBolasVioletas;
	public int bolasBucle;
	public int gasBucle;
	public int maxCO2 = 54;		// barras de energia
	public int maxBolas = -32;	// barras de energia

	public claseInterface()
	{

	}
}
