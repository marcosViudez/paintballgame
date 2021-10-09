using UnityEngine;
using System.Collections;

public class crearBolaPintura : MonoBehaviour {

	// variables 
	public GameObject scriptJugadores;
	public GameObject impactoPintura;
	public GameObject particulasImpacto;
	public float velocidadBolaPintura = 200f;
	public int tiempoVidaBola = 20;
	public string capaColision = "obstaculo";
	public RaycastHit hit;

	// metodos 
	void Update () 
	{
		moverse();						  	 // movemos la bola disparada
		Destroy(gameObject);
	}

	/// <summary>
	/// movimiento de las bolas de pintura disparadas
	/// </summary>
	void moverse()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * velocidadBolaPintura);
		
		if(Physics.Raycast(transform.position,transform.forward ,out hit))
		{
			if(hit.transform.tag != capaColision && hit.transform.tag == "suelo" && hit.transform.tag == "enemigo")
			{
				Destroy(gameObject,tiempoVidaBola);		// se destruye a los 4 segundos de ser creado
			}

			if(hit.transform.tag == capaColision || hit.transform.tag == "suelo")
			{
				// creamos manchas si caen al suelo o chocan con un obstaculo
				Destroy(gameObject);
				Quaternion rota = Quaternion.FromToRotation(Vector3.up, hit.normal);
								Transform particulasPintura = Instantiate(particulasImpacto.transform,hit.point,rota) as Transform;	// instanciamos el objeto particulas
								Transform manchaPintura = Instantiate(impactoPintura.transform,hit.point,rota) as Transform;		// instanciamos el objeto recal imagen
				// manchaPintura.transform.parent = hit.transform;
			}

			if(hit.transform.tag == "enemigo")
			{
				if(hit.transform.name == "enemigo01")
				{
					// accion matar enemigo
					GameObject malo01 = GameObject.Find("enemigo01");
					malo01.tag = "muerto";
				}

				if(hit.transform.name == "enemigo02")
				{
					// accion matar enemigo
					GameObject malo02 = GameObject.Find("enemigo02");
					malo02.tag = "muerto";
				}

				if(hit.transform.name == "enemigo03")
				{
					// accion matar enemigo
					GameObject malo03 = GameObject.Find("enemigo03");
					malo03.tag = "muerto";
				}

				if(hit.transform.name == "enemigo04")
				{
					// accion matar enemigo
					GameObject malo04 = GameObject.Find("enemigo04");
					malo04.tag = "muerto";
				}

				if(hit.transform.name == "enemigo05")
				{
					// accion matar enemigo
					GameObject malo05 = GameObject.Find("enemigo05");
					malo05.tag = "muerto";
				}
			}
			/*
			// si el meco esta tocado vuelve al waypoint indicado
			if(hit.transform.tag == "player")
			{
				Destroy(gameObject);
				Quaternion rota = Quaternion.FromToRotation(Vector3.up, hit.normal);
				Transform particulasPintura = Instantiate(particulasImpacto,hit.point,rota) as Transform;	// instanciamos el objeto particulas

				if(hit.transform.name == "nuevoJugador01")
				{
					// print ("eliminado 01");
					codigoJugadorV002.tocadoJugador[0] = true;
				}

				if(hit.transform.name == "nuevoJugador02")
				{
					// print ("eliminado 02");
					codigoJugadorV002.tocadoJugador[1] = true;
				}

				if(hit.transform.name == "nuevoJugador03")
				{
					// print ("eliminado 02");
					codigoJugadorV002.tocadoJugador[2] = true;
				}

				if(hit.transform.name == "nuevoJugador04")
				{
					// print ("eliminado 02");
					codigoJugadorV002.tocadoJugador[3] = true;
				}

				if(hit.transform.name == "nuevoJugador05")
				{
					// print ("eliminado 02");
					codigoJugadorV002.tocadoJugador[4] = true;
				}

			}
			*/
		}		
	}
	

}