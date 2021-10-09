using UnityEngine;
using System.Collections;

public class animas_Jugador : MonoBehaviour {

	public string animaCurso;

	void OnGUI()
	{
		if (GUI.Button (new Rect (0, 0,140, 30), "caminarNormal")) 
		{
			GetComponent<Animation>().CrossFade ("caminarNormal");
		}
		if (GUI.Button (new Rect (0, 30,140, 30), "reposo")) 
		{
			GetComponent<Animation>().CrossFade ("reposo");
		}
		if (GUI.Button (new Rect (0, 60,140, 30), "caminarAgachado")) 
		{
			GetComponent<Animation>().CrossFade ("caminarAgachado");
		}
		if (GUI.Button (new Rect (0, 90,140, 30), "correrNormal")) 
		{
			GetComponent<Animation>().CrossFade ("correrNormal");
		}
		if (GUI.Button (new Rect (0, 120,140, 30), "correrAgachado")) 
		{
			GetComponent<Animation>().CrossFade ("correrAgachado");
		}
		if (GUI.Button (new Rect (0, 150,140, 30), "agacharse")) 
		{
			GetComponent<Animation>().CrossFade ("agacharse");
		}
		if (GUI.Button (new Rect (0, 180,140, 30), "levantarseAgachado")) 
		{
			GetComponent<Animation>().CrossFade ("levantarseAgachado");
		}
		if (GUI.Button (new Rect (0, 210,140, 30), "arrodillarse")) 
		{
			GetComponent<Animation>().CrossFade ("arrodillarse");
		}
		if (GUI.Button (new Rect (0, 240,140, 30), "levantarseArrodillado")) 
		{
			GetComponent<Animation>().CrossFade ("levantarseArrodillado");
		}
		if (GUI.Button (new Rect (0, 270,140, 30), "acostarse")) 
		{
			GetComponent<Animation>().CrossFade ("acostarse");
		}
		if (GUI.Button (new Rect (0, 300,140, 30), "levantarseAcostado")) 
		{
			GetComponent<Animation>().CrossFade ("levantarseAcostado");
		}
		if (GUI.Button (new Rect (0, 330,140, 30), "cargarDePie")) 
		{
			GetComponent<Animation>().CrossFade ("cargarDePie");
		}
		if (GUI.Button (new Rect (0, 360,140, 30), "cargarArrodillado")) 
		{
			GetComponent<Animation>().CrossFade ("cargarArrodillado");
		}
		if (GUI.Button (new Rect (0, 390,140, 30), "cargarAgachado")) 
		{
			GetComponent<Animation>().CrossFade ("cargarAgachado");
		}
		if (GUI.Button (new Rect (0, 420,140, 30), "standCogiendoCosas")) 
		{
			GetComponent<Animation>().CrossFade ("standCogiendoCosas");
		}
		if (GUI.Button (new Rect (0, 450,140, 30), "tocado01")) 
		{
			GetComponent<Animation>().CrossFade ("tocado01");
		}

		if (GUI.Button (new Rect (160, 0,180, 30), "tocado01Andar")) 
		{
			GetComponent<Animation>().CrossFade ("tocado01Andar");
		}
		if (GUI.Button (new Rect (160, 30,180, 30), "tocado01Inicio")) 
		{
			GetComponent<Animation>().CrossFade ("tocado01Inicio");
		}
		if (GUI.Button (new Rect (160, 60,180, 30), "gestoParoPie")) 
		{
			GetComponent<Animation>().CrossFade ("gestoParoPie");
		}
		if (GUI.Button (new Rect (160, 90,180, 30), "gestoParoPieVolver")) 
		{
			GetComponent<Animation>().CrossFade ("gestoParoPieVolver");
		}
		if (GUI.Button (new Rect (160, 120,180, 30), "gestoParoAgachado")) 
		{
			GetComponent<Animation>().CrossFade ("gestoParoAgachado");
		}
		if (GUI.Button (new Rect (160, 150,180, 30), "gestoParoAgachadoVolver")) 
		{
			GetComponent<Animation>().CrossFade ("gestoParoAgachadoVolver");
		}
		if (GUI.Button (new Rect (160, 180,180, 30), "gestoParoArrodillado")) 
		{
			GetComponent<Animation>().CrossFade ("gestoParoArrodillado");
		}
		if (GUI.Button (new Rect (160, 210,180, 30), "gestoParoArrodilladoVolver")) 
		{
			GetComponent<Animation>().CrossFade ("gestoParoArrodilladoVolver");
		}
		if (GUI.Button (new Rect (160, 240,180, 30), "gestoAvanzarPie")) 
		{
			GetComponent<Animation>().CrossFade ("gestoAvanzarPie");
		}
		if (GUI.Button (new Rect (160, 270,180, 30), "gestoAvanzarAgachado")) 
		{
			GetComponent<Animation>().CrossFade ("gestoAvanzarAgachado");
		}
		if (GUI.Button (new Rect (160, 300,180, 30), "gestoAvanzarArrodillado")) 
		{
			GetComponent<Animation>().CrossFade ("gestoAvanzarArrodillado");
		}
		if (GUI.Button (new Rect (160, 330,180, 30), "ladearDerPie")) 
		{
			GetComponent<Animation>().CrossFade ("ladearDerPie");
		}
		if (GUI.Button (new Rect (160, 360,180, 30), "ladearDerPieVolver")) 
		{
			GetComponent<Animation>().CrossFade ("ladearDerPieVolver");
		}
		if (GUI.Button (new Rect (160, 390,180, 30), "ladearDerArrodillado")) 
		{
			GetComponent<Animation>().CrossFade ("ladearDerArrodillado");
		}
		if (GUI.Button (new Rect (160, 420,180, 30), "ladearDerArrodilladoVolver")) 
		{
			GetComponent<Animation>().CrossFade ("ladearDerArrodilladoVolver");
		}
		if (GUI.Button (new Rect (160, 450,180, 30), "ladearIzqPie")) 
		{
			GetComponent<Animation>().CrossFade ("ladearIzqPie");
		}

		if (GUI.Button (new Rect (360, 0,180, 30), "ladearIzqPieVolver")) 
		{
			GetComponent<Animation>().CrossFade ("ladearIzqPieVolver");
		}
		if (GUI.Button (new Rect (360, 30,180, 30), "ladearIzqArrodillado")) 
		{
			GetComponent<Animation>().CrossFade ("ladearIzqArrodillado");
		}
		if (GUI.Button (new Rect (360, 60,180, 30), "ladearIzqArrodilladoVolver")) 
		{
			GetComponent<Animation>().CrossFade ("ladearIzqArrodilladoVolver");
		}
		if (GUI.Button (new Rect (360, 90,180, 30), "victoria01")) 
		{
			GetComponent<Animation>().CrossFade ("victoria01");
		}
		if (GUI.Button (new Rect (360, 120,180, 30), "victoria01Andar")) 
		{
			GetComponent<Animation>().CrossFade ("victoria01Andar");
		}
		if (GUI.Button (new Rect (360, 150,180, 30), "derrota01")) 
		{
			GetComponent<Animation>().CrossFade ("derrota01");
		}
		if (GUI.Button (new Rect (360, 180,180, 30), "derrota01Andar")) 
		{
			GetComponent<Animation>().CrossFade ("derrota01Andar");
		}

	}
}
