#pragma strict

public var pantallaInicio : Texture2D;
public var aspectoMenu : GUISkin;

public var sonidoMancha : AudioClip;

public var nuevaPulsada : boolean = false;
public var cargarPulsada : boolean = false;
public var opcionesPulsada : boolean = false;
public var salirPulsada : boolean = false;

public var disparandoNueva : boolean = false;
public var disparandoCargar : boolean = false;
public var disparandoOpciones : boolean = false;
public var disparandoSalir : boolean = false;

public var tiempo : float = 0;
public var conversionAncho : float;
public var conversionAlto : float;

public var manchas = new Texture2D[5];	

public var x : int = 0;
public var y : int = 0;

function Update () 
{
	codigosTiempo();
	
	conversionAncho = Screen.width / 10.24 / 100;
	conversionAlto = Screen.height / 7.68 / 100;
	
	// print(conversionAncho + " x " + conversionAlto);
	
	if(nuevaPulsada)
	{	
		tiempo = 0;
		nuevaPulsada = false;
		disparandoNueva = true;
	}
	if(cargarPulsada)
	{
		tiempo = 0;
		cargarPulsada = false;
		disparandoCargar = true;
	}
	if(opcionesPulsada)
	{
		tiempo = 0;
		opcionesPulsada = false;
		disparandoOpciones = true;

	}
	if(salirPulsada)
	{
		tiempo = 0;
		salirPulsada = false;
		disparandoSalir = true;
	}
}

function codigosTiempo()
{
	if(disparandoNueva || disparandoCargar || disparandoOpciones || disparandoSalir)
	{
		tiempo = tiempo + 1;
	}
	
	if(tiempo > 12)
	{
		tiempo = 15;
	}
	
	if(disparandoNueva)
	{
		disparandoCargar = false;
		disparandoOpciones = false;
		disparandoSalir = false;
		yield WaitForSeconds(40.0f * Time.deltaTime);
		tiempo = 0;
		disparandoNueva = false;
	}
	
	if(cargarPulsada)
	{
		disparandoNueva = false;
		disparandoOpciones = false;
		disparandoSalir = false;
		yield WaitForSeconds(40.0f * Time.deltaTime);
		tiempo = 0;
		disparandoCargar = false;
	}
	
	if(opcionesPulsada)
	{
		disparandoNueva = false;
		disparandoCargar = false;
		disparandoSalir = false;
		yield WaitForSeconds(40.0f * Time.deltaTime);
		tiempo = 0;
		disparandoOpciones = false;
	}
	
	if(salirPulsada)
	{
		disparandoNueva = false;
		disparandoCargar = false;
		disparandoOpciones = false;
		yield WaitForSeconds(40.0f * Time.deltaTime);
		tiempo = 0;
		disparandoSalir = false;
	}
}

function OnGUI()
{
	GUI.skin = aspectoMenu;				// aspecto del menu y botones
	
	GUI.Box(new Rect(0,0,1024*conversionAncho,768*conversionAncho),pantallaInicio);		// pantalla de inicio fondo
	// botones del menu principal 
	if(GUI.Button(new Rect(80*conversionAncho,220*conversionAncho,200*conversionAncho,50*conversionAncho),"","botonNueva") && tiempo == 0)
	{
		nuevaPulsada = true;
		cargarPulsada = false;
		opcionesPulsada = false;
		salirPulsada = false; 
	}
	if(GUI.Button(new Rect(80*conversionAncho,280*conversionAncho,200*conversionAncho,50*conversionAncho),"","botonCargar") && tiempo == 0)
	{
		nuevaPulsada = false;
		cargarPulsada = true;
		opcionesPulsada = false;
		salirPulsada = false;
	}
	if(GUI.Button(new Rect(80*conversionAncho,340*conversionAncho,200*conversionAncho,50*conversionAncho),"","botonOpciones") && tiempo == 0)
	{
		nuevaPulsada = false;
		cargarPulsada = false;
		opcionesPulsada = true;
		salirPulsada = false;
	}
	if(GUI.Button(new Rect(80*conversionAncho,400*conversionAncho,200*conversionAncho,50*conversionAncho),"","botonSalir") && tiempo == 0)
	{
		nuevaPulsada = false;
		cargarPulsada = false;
		opcionesPulsada = false;
		salirPulsada = true;
	}
	
	if(disparandoNueva)
	{
		if(tiempo>1)
		{
			GUI.DrawTexture(Rect(80*conversionAncho,210*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[2]);
			if(tiempo>1 && tiempo<3)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>3)
		{
			GUI.DrawTexture(Rect(67*conversionAncho,220*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[3]);
			if(tiempo>3 && tiempo<5)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>6)
		{
			GUI.DrawTexture(Rect(100*conversionAncho,236*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[0]);
			if(tiempo>6 && tiempo<8)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>9)
		{
			GUI.DrawTexture(Rect(150*conversionAncho,205*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[1]);
			if(tiempo>9 && tiempo<11)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>12)
		{
			GUI.DrawTexture(Rect(200*conversionAncho,215*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[2]);
			if(tiempo>12 && tiempo<14)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
	}
	
	if(disparandoCargar)
	{
		if(tiempo>1)
		{
			GUI.DrawTexture(Rect(111*conversionAncho,262*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[0]);
			if(tiempo>1 && tiempo<3)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>3)
		{
			GUI.DrawTexture(Rect(210*conversionAncho,285*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[2]);
			if(tiempo>3 && tiempo<5)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>6)
		{
			GUI.DrawTexture(Rect(130*conversionAncho,278*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[3]);
			if(tiempo>6 && tiempo<8)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>9)
		{
			GUI.DrawTexture(Rect(95*conversionAncho,290*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[1]);
			if(tiempo>9 && tiempo<11)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>12)
		{
			GUI.DrawTexture(Rect(74*conversionAncho,284*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[1]);
			if(tiempo>12 && tiempo<14)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
	}
	
	if(disparandoOpciones)
	{
		if(tiempo>1)
		{
			GUI.DrawTexture(Rect(76*conversionAncho,333*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[1]);
			if(tiempo>1 && tiempo<3)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>3)
		{
			GUI.DrawTexture(Rect(200*conversionAncho,354*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[2]);
			if(tiempo>3 && tiempo<5)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>6)
		{
			GUI.DrawTexture(Rect(165*conversionAncho,345*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[1]);
			if(tiempo>6 && tiempo<8)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>9)
		{
			GUI.DrawTexture(Rect(125*conversionAncho,330*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[0]);
			if(tiempo>9 && tiempo<11)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>12)
		{
			GUI.DrawTexture(Rect(220*conversionAncho,348*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[0]);
			if(tiempo>12 && tiempo<14)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
	}
	
	if(disparandoSalir)
	{
		if(tiempo>1)
		{
			GUI.DrawTexture(Rect(85*conversionAncho,387*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[0]);
			if(tiempo>1 && tiempo<3)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>3)
		{
			GUI.DrawTexture(Rect(154*conversionAncho,390*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[3]);
			if(tiempo>3 && tiempo<5)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>6)
		{
			GUI.DrawTexture(Rect(210*conversionAncho,400*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[0]);
			if(tiempo>6 && tiempo<8)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>9)
		{
			GUI.DrawTexture(Rect(110*conversionAncho,416*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[2]);
			if(tiempo>9 && tiempo<11)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
		if(tiempo>12)
		{
			GUI.DrawTexture(Rect(190*conversionAncho,392*conversionAncho,50*conversionAncho,50*conversionAncho),manchas[3]);
			if(tiempo>12 && tiempo<14)
			{
				GetComponent.<AudioSource>().PlayOneShot(sonidoMancha);
			}
		}
	}
	

}