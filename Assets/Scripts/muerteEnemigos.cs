using UnityEngine;
using System.Collections;

public class muerteEnemigos : MonoBehaviour {

	private UnityEngine.AI.NavMeshAgent agent;

	public bool[] maloTocado = new bool[5];

	void Start()
	{
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();		// cogemos nuestra variable navegacion

		for(int i = 0; i < 5; i++)
		{
			maloTocado[i] = false;
		}
	}
	// Update is called once per frame
	void Update () 
	{

	}
	



}
