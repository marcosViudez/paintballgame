using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class slot : MonoBehaviour {

	private Stack<item> items;
	public Text stackTxt;
	public Sprite slotEmpty;
	public Sprite slotHighLight;

	public bool isEmpty
	{
		get { return items.Count == 0; }
	}

	public bool IsAvaiable
	{
		get { return CurrentItem.maxSize >items.Count; }
	}

	public item CurrentItem
	{
		get { return items.Peek (); }
	}

	// Use this for initialization
	void Start () 
	{
		items = new Stack<item>();
		RectTransform slotRect = GetComponent<RectTransform>();
		RectTransform txtRect = stackTxt.GetComponent<RectTransform>();

		int txtScaleFactor  = (int)(slotRect.sizeDelta.x * 0.60);
		stackTxt.resizeTextMaxSize = txtScaleFactor;
		stackTxt.resizeTextMinSize = txtScaleFactor;

		txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
		txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
	}

	public void addItem(item item)
	{
		items.Push(item);

		if(items.Count > 1)
		{
			stackTxt.text = items.Count.ToString(); 
		}

		changeSprite(item.spriteNeutral, item.spriteNeutralHighlighted);
	}

	private void changeSprite(Sprite neutral, Sprite highLight)
	{
		GetComponent<Image>().sprite = neutral;

		SpriteState st = new SpriteState();
		st.highlightedSprite = highLight;
		st.pressedSprite = neutral;

		GetComponent<Button>().spriteState = st;
	}
}
