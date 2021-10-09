using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class inventarioV003 : MonoBehaviour {

	private RectTransform inventarioRect;
	private float anchoInventario;
	private float altoInventario;
	public int ranuras;
	public int filas;
	public int columnas;
	public float espacioIzquierdo;
	public float espacioArriba;
	public float tamanoRanuras;
	public GameObject prefabRanuras;

	private float inventarioPosX;
	private float inventarioPosY;

	private List<GameObject> totalSlots;

	private int emptySlot;

	// Use this for initialization
	void Start () 
	{
		crearInvetarioPanel();
	}

	private void crearInvetarioPanel()
	{
		totalSlots = new List<GameObject>();

		emptySlot = ranuras;

		// tamaño del fondo del inventario
		anchoInventario = (ranuras/filas) * (tamanoRanuras + espacioIzquierdo) + espacioIzquierdo;
		altoInventario = filas * (tamanoRanuras + espacioArriba) + espacioArriba;

		//anchoInventario = 200;
		//altoInventario = 55;

		inventarioRect = GetComponent<RectTransform>();

		// colocamos las anclas de los slots creados
		inventarioRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, anchoInventario);
		inventarioRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, altoInventario);

		columnas = ranuras / filas;

		for(int y = 0; y < filas; y++)
		{
			for(int x = 0; x < columnas; x++)
			{
				GameObject nuevaRanura = (GameObject)Instantiate(prefabRanuras);
				RectTransform ranuraRect = nuevaRanura.GetComponent<RectTransform>();
				nuevaRanura.name = "ranura";
				// nuevaRanura.tag = "ranuraVacia";
				nuevaRanura.transform.SetParent(this.transform.parent);

				inventarioPosX = espacioIzquierdo * (x + 1) + (tamanoRanuras * x);
				inventarioPosY = -espacioArriba * (y + 1) - (tamanoRanuras * y);

				ranuraRect.localPosition = inventarioRect.localPosition + new Vector3(inventarioPosX, inventarioPosY);
				ranuraRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, tamanoRanuras);
				ranuraRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, tamanoRanuras);

				totalSlots.Add (nuevaRanura);
			}
		}

	}

	public bool addItem(item item)
	{
		if(item.maxSize == 1)
		{
			placeEmpty(item);
			return true;
		}
		else 
		{
			foreach(GameObject slot in totalSlots)
			{
				slot tmp = slot.GetComponent<slot>();

				if(!tmp.isEmpty)
				{
					if(tmp.CurrentItem.type == item.type && tmp.IsAvaiable) 
					{
						tmp.addItem(item);
						// emptySlot--;
						return true;
					}
				}
			}

			if(emptySlot > 0)
			{
				placeEmpty(item);
			}
		}

		return false;
	}

	private bool placeEmpty(item item)
	{
		if(emptySlot > 0)
		{
			foreach(GameObject slot in totalSlots)
			{
				slot tmp = slot.GetComponent<slot>();
				if(tmp.isEmpty)
				{
					tmp.addItem(item);
					emptySlot--;
					return true;
				}
			}
		}
		return false;
	}
}
