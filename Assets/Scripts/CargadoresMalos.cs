using UnityEngine;
using System.Collections;

// este script detecta las bolas gastadas y si se
// queda a cero, tira un cargador de su espalda
// y asi hasta su maximo de cargadores

public class CargadoresMalos : MonoBehaviour {

	public const int bolasMax = 200;
	public int bolasCargador;
	public int numeroPods;
	public int gasRestante;
	public int bucleBolas;

	public GameObject[] podsMochila = new GameObject[6];		// gameobjects de la espalda
	public GameObject podCreate;								// gameobject vacio donde creamos pods nuevos	
	public Rigidbody podTirado;									// nuevo pods creado prefab

	// Use this for initialization
	void Start () 
	{
		bolasCargador = bolasMax;
		numeroPods = 6;
		gasRestante = 54;
	}
	
	// Update is called once per frame
	void Update () 
	{
		limitesCargadores ();
		restandoGasMalos ();
	}

	public void restandoGasMalos()
	{
		if(bucleBolas > 15)		// conversion de bolas a resta de gas
		{						// cada 15 bolas resta 1gr de gas
			bucleBolas = 0;
			gasRestante --;
		}
	}

	public void crearPodMalo()
	{
		Instantiate(podTirado, podCreate.transform.position, podCreate.transform.rotation);
		podTirado.AddForce(new Vector3(0,0,10));
		podTirado.AddTorque(10,50,20);
		podsMochila [numeroPods].SetActive (false);
	}

	public void limitesCargadores()
	{
		if(bolasCargador <= 0 && numeroPods > 0)
		{
			bolasCargador = bolasMax;
			numeroPods --;
			crearPodMalo();
		}
		
		if(numeroPods == 0 && bolasCargador <= 0)
		{
			print ("acabaste con la municion");
		}

		if(gasRestante < 0)
		{
			gasRestante = 0;
		}
	}

	public void restaBolas()
	{
		if(numeroPods >= 0 && bolasCargador > 0 && gasRestante > 0)
		{
			bolasCargador --;
			bucleBolas ++;
		}
	}
}
