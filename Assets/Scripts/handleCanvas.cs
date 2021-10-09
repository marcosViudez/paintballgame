using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class handleCanvas : MonoBehaviour {

	private CanvasScaler escala;

	// Use this for initialization
	void Start ()
	{
		escala = GetComponent<CanvasScaler>();

		escala.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
	}
	

}
