using UnityEngine;
using System.Collections;

public class inventario : MonoBehaviour {

	public GUISkin aspecto;

	public int posX;
	public int posY;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.skin = aspecto;

		GUI.Box (new Rect (0 + posX, 0 + posY, 178, 114), "Bolsa Personal");
		GUI.Button (new Rect (5 + posX, 30 + posY, 40, 40), "");
		GUI.Button (new Rect (47 + posX, 30 + posY, 40, 40), "");
		GUI.Button (new Rect (89 + posX, 30 + posY, 40, 40), "");
		GUI.Button (new Rect (131 + posX, 30 + posY, 40, 40), "");

		GUI.Button (new Rect (5 + posX, 72 + posY, 40, 40), "");
		GUI.Button (new Rect (47 + posX, 72 + posY, 40, 40), "");
		GUI.Button (new Rect (89 + posX, 72 + posY, 40, 40), "");
		GUI.Button (new Rect (131 + posX, 72 + posY, 40, 40), "");
	}
}
