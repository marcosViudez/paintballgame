using UnityEngine;
using System.Collections;

public class crearBolaMalosAmarilla : MonoBehaviour {
	
	// variables 
	public GameObject scriptMalosBolas;
	public GameObject impactoPinturaMalos;
	public GameObject particulasImpactoMalos;
	public float velocidadBolaPinturaMalos = 200f;
	public int tiempoVidaBolaMalos = 20;
	public string capaColisionMalos = "obstaculo";
	public RaycastHit hitMalos;
	
	// metodos 
	void Update () 
	{
		moverseMalos();						  	 // movemos la bola disparada
		Destroy(gameObject);
	}

	/// <summary>
	/// movimiento de los enemigos
	/// </summary>
	void moverseMalos()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * velocidadBolaPinturaMalos);
		
		if(Physics.Raycast(transform.position,transform.forward ,out hitMalos))
		{
			if(hitMalos.transform.tag != capaColisionMalos && hitMalos.transform.tag == "suelo" && hitMalos.transform.tag == "player")
			{
				Destroy(gameObject,tiempoVidaBolaMalos);		// se destruye a los 4 segundos de ser creado
			}
			
			if(hitMalos.transform.tag == capaColisionMalos || hitMalos.transform.tag == "suelo")
			{
				// creamos manchas si caen al suelo o chocan con un obstaculo
				Destroy(gameObject);
				Quaternion rotaMalos = Quaternion.FromToRotation(Vector3.up, hitMalos.normal);
								Transform particulasPinturaMalos = Instantiate(particulasImpactoMalos.transform,hitMalos.point,rotaMalos) as Transform;	// instanciamos el objeto particulas
								Transform manchaPinturaMalos = Instantiate(impactoPinturaMalos.transform,hitMalos.point,rotaMalos) as Transform;		// instanciamos el objeto recal imagen
				// manchaPintura.transform.parent = hit.transform;
			}

			// si el meco esta tocado vuelve al waypoint indicado
			if(hitMalos.transform.tag == "player")
			{
				Destroy(gameObject);
				Quaternion rotaMalos = Quaternion.FromToRotation(Vector3.up, hitMalos.normal);
								Transform particulasPinturaMalos = Instantiate(particulasImpactoMalos.transform,hitMalos.point,rotaMalos) as Transform;	// instanciamos el objeto particulas
				
				if(hitMalos.transform.name == "nuevoJugador01")
				{
					// print ("eliminado 01");
					codigoJugadorV002.tocadoJugador[0] = true;
				}
				
				if(hitMalos.transform.name == "nuevoJugador02")
				{
					// print ("eliminado 02");
					codigoJugadorV002.tocadoJugador[1] = true;
				}
				
				if(hitMalos.transform.name == "nuevoJugador03")
				{
					// print ("eliminado 02");
					codigoJugadorV002.tocadoJugador[2] = true;
				}
				
				if(hitMalos.transform.name == "nuevoJugador04")
				{
					// print ("eliminado 02");
					codigoJugadorV002.tocadoJugador[3] = true;
				}
				
				if(hitMalos.transform.name == "nuevoJugador05")
				{
					// print ("eliminado 02");
					codigoJugadorV002.tocadoJugador[4] = true;
				}
				
			}
		}		
	}
	
	
}