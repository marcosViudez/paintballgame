using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class inventarioV004 : MonoBehaviour {

	public int ranuras = 10;
	public int ranurasVacias;
	public GameObject ranuraFoto;
	public GameObject ranuraBola;

	public GameObject[] todasRanuras = new GameObject[10];

	public Sprite[] mascaras = new Sprite[8];
	public Sprite[] pinturaColor = new Sprite[5];

	public int numeroMascara = 0;
	public int numeroColor = 0;
	
	// Use this for initialization
	void Start () 
	{
		crearInvetarioPanel();
		ranuraBola.GetComponent<Image>().sprite = pinturaColor[0];
	}
	
	// Update is called once per frame
	void Update () 
	{
		limitesMascaras();
		codigoMascaras();
	}

	public void cambiarColoresBolas()
	{
		numeroColor++;

		if(numeroColor > 4)
		{
			numeroColor = 0;
		}

		ranuraBola.GetComponent<Image>().sprite = pinturaColor[numeroColor];

	}

	public void codigoMascaras()
	{
		for(int i = 0; i < 8; i++)
		{
			if(numeroMascara == i)
			{
				ranuraFoto.GetComponent<Image>().sprite = mascaras[i];
			}
		}
	}

	public void limitesMascaras()
	{
		if(numeroMascara == 0)
		{
			todasRanuras[0] = null;
			ranuraFoto.GetComponent<Image>().sprite = mascaras[0];
		}
		
		if(numeroMascara < 0)
		{
			numeroMascara = 0;
		}
		
		if(numeroMascara > 7)
		{
			numeroMascara = 7;
		}
	}

	public void cambiarMascaraUp()
	{
		numeroMascara--;
	}

	public void cambiarMascaraDown()
	{
		numeroMascara++;
	}

	private void crearInvetarioPanel()
	{
		ranurasVacias = ranuras; 
	}

	public void estadoRanura()
	{
		for(int i = 0; i < 10; i++)
		{
			if(todasRanuras[i].tag == "ranuraVacia")
			{

			}
		}
	}


}
