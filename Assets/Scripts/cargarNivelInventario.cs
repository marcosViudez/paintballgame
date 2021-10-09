using UnityEngine;
using System.Collections;

public class cargarNivelInventario : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.visible = true;
	}

	public void cargandoLevelJuego()
	{
		Application.LoadLevel(1);
	}

	public void cargandoLevelMenu()
	{
		Application.LoadLevel(0);
	}
}
