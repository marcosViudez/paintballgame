#pragma strict

// variables para mover el cuadro del inventario
public var posxInventario : int = 0;
public var posyInventario : int = 0;

// tamaño de caja de inventario para una bolsa 4x2
public var tamanox : int = 110;
public var tamanoy : int = 140;
public var tamCuadroInventariox : int;
public var tamCuadroInventarioy : int;

// variables de formato de las ranuras Inventario
public var posRanurax : int;
public var posRanuray : int;
public var tamRanurax : int  = 40; 
public var tamBRanuray : int  = 40;
public var espaciadoRanuras : int = 10;
public var espacioVerticalTitulo : int = 40;
public var ampliableCuadro : int = 50;
public var totalRanuras : int;
public var textoRanura : Texture2D = null;
public var ranuraCreada : int = 0;
// variables del tamaño del inventario
public var filas : int = 2;
public var columnas : int = 4;

public var crear : boolean;

//structura para mi array de ranuras
class ranuras
{
	var nRanura : int;
	//var texturaRanura : Texture2D;
	var posicionRanura : Vector2;
	//var tamanoRanura : Vector2;
}

public var ListaRanuras = new ranuras [totalRanuras];

function Update () 
{
    // formula del total de ranuras por bolsa de inventario
	totalRanuras = filas * columnas;
	// ListaRanuras = new ranuras [totalRanuras];
	// formatoRanuras();
	
}

function nuevoArray()
{
	if(!crear)
	{
		ListaRanuras = new ranuras [totalRanuras];
		crear = true;
		formatoRanuras();
	}
}

function formatoRanuras()
{	
	var j : int = 0;
	for(var i : int = 0; i < totalRanuras ; i++)
	{
		// condicion para el salto de fila
		if(i > columnas-1)
		{
			if((i%columnas) == 0)
			{
				j++;
			}
		}
		// algoritmo de tamaño de cuadro inventario para agrandar 
		// la caja que los contiene
		tamCuadroInventariox = tamanox + ((i%columnas-1) * ampliableCuadro);
		tamCuadroInventarioy = tamanoy + ((j%filas-1) * ampliableCuadro);
		// algoritmo de posicion de ranuras
		// el operando % es para calcular el resto de la division
		posRanurax = (i%columnas * tamRanurax) + ((i%columnas+1) * espaciadoRanuras) + posxInventario;
		posRanuray = posyInventario + espacioVerticalTitulo + (j * (tamBRanuray + espaciadoRanuras));
		
	}
}

function OnGUI()
{
	// array para colocacion de ranuras de inventario
	// segun las filas y columnas deseadas
	GUI.Box(Rect(posxInventario,posyInventario,tamCuadroInventariox,tamCuadroInventarioy),"Inventario Personaje");
	
		if(GUI.Button(Rect(300,300,40,40),"new"))
		{
			crear = false;
			nuevoArray();
		}
		
		GUI.Button(Rect(posRanurax ,posRanuray,tamRanurax,tamBRanuray),textoRanura);
		
		// inserto en mi array de ranuras cada vez que creo un boton
		
	/*GUI.BeginGroup(Rect(0,0,tamanox,tamanoy));
			
	GUI.EndGroup();*/
	
	
}