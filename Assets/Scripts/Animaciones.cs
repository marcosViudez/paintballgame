using UnityEngine;
using System.Collections;

public class Animaciones : MonoBehaviour
{

    // --------------------------------------------------------------------------------
    // Variables
    // --------------------------------------------------------------------------------

    public enum AnimacionTipo {
        Reposo,
        Corriendo,
        Agachado,
        Acostado
    };

    public AnimacionTipo animaTipo = AnimacionTipo.Reposo;

    public void agachadoSiWaypointAlto() 
	{
        if (GetComponent<ControlMovimiento>().bColisionaWaypoint)
        {
            if (GetComponent<ObjetoProximo>().ordenaObstaculos().name.Substring(GetComponent<ObjetoProximo>().ordenaObstaculos().name.Length - 4, 4).Equals("bajo"))
            {
                GetComponent<Animation>().CrossFade("acostarse");
				animaTipo = AnimacionTipo.Acostado;
            }
            else if (GetComponent<ObjetoProximo>().ordenaObstaculos().name.Substring(GetComponent<ObjetoProximo>().ordenaObstaculos().name.Length - 4, 4).Equals("alto"))
                GetComponent<Animation>().CrossFade("agacharse");
				animaTipo = AnimacionTipo.Agachado;
        }
        else GetComponent<Animation>().CrossFade("correrNormal");
		animaTipo = AnimacionTipo.Corriendo;
    }

    void Update() 
	{
		if(GetComponent<ControlMovimiento>().scriptFases.GetComponent<propiedadesGamev002>().fasesDelJuego == 2)
		{
        	agachadoSiWaypointAlto();
		}
    }

}
