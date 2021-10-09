using UnityEngine;
using System.Collections;

public class moverEscenarioCamara : MonoBehaviour {

	public GameObject scriptCamaras;

	public Transform[] miCamaraActiva = new Transform[8];

	public float[] camaraX = new float[8];
	public float[] camaraY = new float[8];
	public float[] camaraZ = new float[8];

	public Vector3[] camaraXYZ = new Vector3[8];
	public Vector3[] camaraXYZiniciales = new Vector3[8];

	public int camaraSeleccionada;
	public Vector3 increXZ;

	// Use this for initialization
	void Awake () 
	{

		for(int i=0; i<8; i++)
		{
			camaraXYZiniciales[i] = miCamaraActiva[i].transform.position;
			camaraXYZ[i] = miCamaraActiva[i].transform.position;
			increXZ = new Vector3 (6f ,6f ,6f);		// estaba el error 
		}

	}
	
	// Update is called once per frame
	void Update () 
	{

		if(!scriptCamaras.GetComponent<propiedadesGamev002>().pausaActivada)
		{
			miCamaraActiva[camaraSeleccionada].transform.position = camaraXYZ[camaraSeleccionada];
			moverCamara ();
			limitesXZ ();

			for(int i=0; i<8; i++)
			{
				if(camaraXYZ[i].y < 200)
				{
					camaraXYZ[i].y = 200;
				}

				if(camaraSeleccionada != 2)
				{
					if(camaraXYZ[i].y > 600)
					{
						camaraXYZ[i].y = 600;
					}
				}else{

					if(camaraXYZ[i].y < 500)
					{
						camaraXYZ[i].y = 500;
					}

					if(camaraXYZ[i].y > 4500)
					{
					camaraXYZ[i].y = 4500;
					}
				}
			}
		}
	}

	void limitesXZ()
	{
		// limites camara 01
		if(camaraXYZ[0].x < 1000)
		{
			camaraXYZ[0].x = 1000;
		}
		if(camaraXYZ[0].x > 2440)
		{
			camaraXYZ[0].x = 2440;
		}
		if(camaraXYZ[0].z < 222)
		{
			camaraXYZ[0].z = 222;
		}
		if(camaraXYZ[0].z > 762)
		{
			camaraXYZ[0].z = 762;
		}
		// limites camara 02
		if(camaraXYZ[1].x < -649)
		{
			camaraXYZ[1].x = -649;
		}
		if(camaraXYZ[1].x > 600)
		{
			camaraXYZ[1].x = 600;
		}
		if(camaraXYZ[1].z < 222)
		{
			camaraXYZ[1].z = 222;
		}
		if(camaraXYZ[1].z > 762)
		{
			camaraXYZ[1].z = 762;
		}
		// limites camara 03
		if(camaraXYZ[2].x < 398)
		{
			camaraXYZ[2].x = 398;
		}
		if(camaraXYZ[2].x > 1358)
		{
			camaraXYZ[2].x = 1358;
		}
		if(camaraXYZ[2].z < 243)
		{
			camaraXYZ[2].z = 243;
		}
		if(camaraXYZ[2].z > 883)
		{
			camaraXYZ[2].z = 883;
		}
		// limites camara 04
		if(camaraXYZ[3].x < 500)
		{
			camaraXYZ[3].x = 500;
		}
		if(camaraXYZ[3].x > 1930)
		{
			camaraXYZ[3].x = 1930;
		}
		if(camaraXYZ[3].z < 197)
		{
			camaraXYZ[3].z = 197;
		}
		if(camaraXYZ[3].z > 883)
		{
			camaraXYZ[3].z = 883;
		}
		// limites camara 05
		if(camaraXYZ[4].x < -115)
		{
			camaraXYZ[4].x = -115;
		}
		if(camaraXYZ[4].x > 1200)
		{
			camaraXYZ[4].x = 1200;
		}
		if(camaraXYZ[4].z < 185)
		{
			camaraXYZ[4].z = 185;
		}
		if(camaraXYZ[4].z > 815)
		{
			camaraXYZ[4].z = 815;
		}
		// limites camara 06
		if(camaraXYZ[5].x < 400)
		{
			camaraXYZ[5].x = 400;
		}
		if(camaraXYZ[5].x > 1900)
		{
			camaraXYZ[5].x = 1900;
		}
		if(camaraXYZ[5].z < 300)
		{
			camaraXYZ[5].z = 300;
		}
		if(camaraXYZ[5].z > 1090)
		{
			camaraXYZ[5].z = 1090;
		}
		// limites camara 07
		if(camaraXYZ[6].x < -113)
		{
			camaraXYZ[6].x = -113;
		}
		if(camaraXYZ[6].x > 1200)
		{
			camaraXYZ[6].x = 1200;
		}
		if(camaraXYZ[6].z < 300)
		{
			camaraXYZ[6].z = 300;
		}
		if(camaraXYZ[6].z > 1150)
		{
			camaraXYZ[6].z = 1150;
		}
		if(camaraXYZ[7].x < 116)
		{
			camaraXYZ[7].x = 116;
		}
		if(camaraXYZ[7].x > 1700)
		{
			camaraXYZ[7].x = 1700;
		}
		if(camaraXYZ[7].z < -270)
		{
			camaraXYZ[7].z = -270;
		}
		if(camaraXYZ[7].z > 350)
		{
			camaraXYZ[7].z = 350;
		}
	}

	void moverCamara()
	{
		if(scriptCamaras.GetComponent<propiedadesGamev002>().fasesDelJuego == 2)
		{

		if(camaraSeleccionada == 0 || camaraSeleccionada == 5 || camaraSeleccionada == 3)
		{
			if(Input.GetKey(KeyCode.S))
			{
				camaraXYZ[camaraSeleccionada].x = camaraXYZ[camaraSeleccionada].x + increXZ.x;
			}
	
			if(Input.GetKey(KeyCode.W))
			{
				camaraXYZ[camaraSeleccionada].x = camaraXYZ[camaraSeleccionada].x - increXZ.x;
			}

			if(Input.GetKey(KeyCode.D))
			{
				camaraXYZ[camaraSeleccionada].z = camaraXYZ[camaraSeleccionada].z + increXZ.z;
			}

			if(Input.GetKey(KeyCode.A))
			{
				camaraXYZ[camaraSeleccionada].z = camaraXYZ[camaraSeleccionada].z - increXZ.z;
			}
		}

		if(camaraSeleccionada == 1 || camaraSeleccionada == 4 || camaraSeleccionada == 6)
		{
			if(Input.GetKey(KeyCode.W))
			{
				camaraXYZ[camaraSeleccionada].x = camaraXYZ[camaraSeleccionada].x + increXZ.x;
			}
			
			if(Input.GetKey(KeyCode.S))
			{
				camaraXYZ[camaraSeleccionada].x = camaraXYZ[camaraSeleccionada].x - increXZ.x;
			}
			
			if(Input.GetKey(KeyCode.A))
			{
				camaraXYZ[camaraSeleccionada].z = camaraXYZ[camaraSeleccionada].z + increXZ.z;
			}
			
			if(Input.GetKey(KeyCode.D))
			{
				camaraXYZ[camaraSeleccionada].z = camaraXYZ[camaraSeleccionada].z - increXZ.z;
			}
		}

		if(camaraSeleccionada == 2 || camaraSeleccionada == 7)
		{
			if(Input.GetKey(KeyCode.D))
			{
				camaraXYZ[camaraSeleccionada].x = camaraXYZ[camaraSeleccionada].x + increXZ.x;
			}
			
			if(Input.GetKey(KeyCode.A))
			{
				camaraXYZ[camaraSeleccionada].x = camaraXYZ[camaraSeleccionada].x - increXZ.x;
			}
			
			if(Input.GetKey(KeyCode.W))
			{
				camaraXYZ[camaraSeleccionada].z = camaraXYZ[camaraSeleccionada].z + increXZ.z;
			}
			
			if(Input.GetKey(KeyCode.S))
			{
				camaraXYZ[camaraSeleccionada].z = camaraXYZ[camaraSeleccionada].z - increXZ.z;
			}
		}

		// subir y bajar la coordenada y de las camaras
		if(Input.GetKey(KeyCode.R))
		{
			camaraXYZ[camaraSeleccionada].y = camaraXYZ[camaraSeleccionada].y + increXZ.y;
		}
		
		if(Input.GetKey(KeyCode.F))
		{
			camaraXYZ[camaraSeleccionada].y = camaraXYZ[camaraSeleccionada].y - increXZ.y;
		}
	}
	}
}
