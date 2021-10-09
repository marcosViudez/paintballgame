#pragma strict

	public var pantallaInventario : Texture2D;

	public var conversionAncho : float;
	
	public var x :float;
	public var y : float;
	public var tamx : float = 100;
	public var tamy : float = 100;
	
	public var botonesX : int = 7;
	public var botonesY : int = 5;
	
	public var espacioBotonesX : float = 5;
	public var espacioEmpieceX : float = 6;
	public var espacioBotonesY : float = 5;
	public var espacioEmpieceY : float = 6;
	public var tamanoBoton : float = 40;
	
function Update () 
{
	if(Input.GetKey(KeyCode.Escape))
	{
		Application.Quit();
	}
	
	conversionAncho = Screen.width / 10.24 / 100;
}

function OnGUI()
{
	GUI.BeginGroup(Rect(0,0,Screen.width,Screen.height));
		GUI.DrawTexture(Rect(0,0,Screen.width,Screen.height),pantallaInventario);
		
		GUI.BeginGroup(Rect(48 * conversionAncho,283 * conversionAncho,400 * conversionAncho,400* conversionAncho));
			GUI.Box(Rect(0 * conversionAncho,0 * conversionAncho,165 * conversionAncho,118 + y * conversionAncho),"");
				/*GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 0) + (espacioBotonesX * 0),6,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 1) + (espacioBotonesX * 1),6,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 2) + (espacioBotonesX * 2),6,tamanoBoton,tamanoBoton),"");
				
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 0) + (espacioBotonesX * 0),52,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 1) + (espacioBotonesX * 1),52,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 2) + (espacioBotonesX * 2),52,tamanoBoton,tamanoBoton),"");*/
		GUI.EndGroup();
		
		/*
		GUI.BeginGroup(Rect(35,425,142,100),"");
			GUI.Box(Rect(0,0,142,100),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 0) + (espacioBotonesX * 0),6,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 1) + (espacioBotonesX * 1),6,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 2) + (espacioBotonesX * 2),6,tamanoBoton,tamanoBoton),"");
				
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 0) + (espacioBotonesX * 0),52,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 1) + (espacioBotonesX * 1),52,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 2) + (espacioBotonesX * 2),52,tamanoBoton,tamanoBoton),"");
		GUI.EndGroup();
		
		GUI.BeginGroup(Rect(238,425,142,100),"");
			GUI.Box(Rect(0,0,142,100),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 0) + (espacioBotonesX * 0),6,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 1) + (espacioBotonesX * 1),6,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 2) + (espacioBotonesX * 2),6,tamanoBoton,tamanoBoton),"");
				
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 0) + (espacioBotonesX * 0),52,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 1) + (espacioBotonesX * 1),52,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 2) + (espacioBotonesX * 2),52,tamanoBoton,tamanoBoton),"");
		GUI.EndGroup();
		
		GUI.BeginGroup(Rect(238,265,142,100),"");
			GUI.Box(Rect(0,0,142,100),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 0) + (espacioBotonesX * 0),6,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 1) + (espacioBotonesX * 1),6,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 2) + (espacioBotonesX * 2),6,tamanoBoton,tamanoBoton),"");
				
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 0) + (espacioBotonesX * 0),52,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 1) + (espacioBotonesX * 1),52,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 2) + (espacioBotonesX * 2),52,tamanoBoton,tamanoBoton),"");
		GUI.EndGroup();
		
		GUI.BeginGroup(Rect(238,105,142,100),"");
			GUI.Box(Rect(0,0,142,100),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 0) + (espacioBotonesX * 0),6,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 1) + (espacioBotonesX * 1),6,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 2) + (espacioBotonesX * 2),6,tamanoBoton,tamanoBoton),"");
				
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 0) + (espacioBotonesX * 0),52,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 1) + (espacioBotonesX * 1),52,tamanoBoton,tamanoBoton),"");
				GUI.Button(Rect(espacioEmpieceX + (tamanoBoton * 2) + (espacioBotonesX * 2),52,tamanoBoton,tamanoBoton),"");
		GUI.EndGroup();
		
		
		GUI.BeginGroup(Rect(515,147,344,232),"");
		GUI.Box(Rect(0,0,344,232),"");
			for(var i : int = 0; i<botonesX; i++)
			{ 
				for(var j : int = 0; j<botonesY; j++)
				{
					if(GUI.Button(Rect(espacioEmpieceX + 12 + (tamanoBoton * i) + (espacioBotonesX * i),espacioEmpieceY + (tamanoBoton * j) + (espacioBotonesY * j),tamanoBoton,tamanoBoton),""))
					{
						print(i + " x " + j);
					}
				}
			}
		GUI.EndGroup();
		*/
	GUI.EndGroup();
	
}