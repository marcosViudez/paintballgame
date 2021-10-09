using UnityEngine;
using System.Collections;

public class item : MonoBehaviour {

	public enum ItemType {bolas, mascaras};

	public ItemType type;
	public Sprite spriteNeutral;
	public Sprite spriteNeutralHighlighted;
	public int maxSize;

	public GameObject inventary;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void addItemBolsa()
	{
		inventary.GetComponent<inventarioV003>().addItem(GetComponent<item>());
	}
}
