using UnityEngine;
using System.Collections;

public class animacionHalo : MonoBehaviour {

	public Light luz;
	private int potenciaLuz;
	private int velocidadDestello;
	private bool transportar;
	public bool activarHaloTransporte;

	// Use this for initialization
	void Start () 
	{
		potenciaLuz = 0;
		velocidadDestello = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		luz.intensity = potenciaLuz;

		if(activarHaloTransporte && !transportar)
		{
			GetComponent<Animation>().CrossFade ("desplegarHalo");
			StartCoroutine(flash());									
		}

		destello();

	}

	IEnumerator flash()
	{
		yield return new WaitForSeconds(1f * Time.deltaTime);		// espera un segundo a realizar animacion
		transportar = true;							// y teletransporta destello
	}

	void destello()
	{
		if(transportar)
		{
			potenciaLuz = potenciaLuz + velocidadDestello;	// aumenta intensidad de luz
		}

		if (potenciaLuz >= 8)
		{
			potenciaLuz = 0;
			transportar = false;
			Destroy(gameObject,1.0f);
		}
	}
}
