using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorCarretera : MonoBehaviour
{
    public GameObject contenedorCallesGO;
    public GameObject[] contenedorCallesArray;
    public GameObject calleAnterior;
    public GameObject callePosterior;
    public Vector3 medidaLimitePantalla;
    //La regla para acceder a componentes externos es:
    //1) Buscar GameObject 2)A través de esa referencia accedo al componente que necesito
    public GameObject mCamGO;
    public Camera mCamComp;
    public float velocidad;
    public float alturaCalle;
    public bool inicioJuego;
    public bool juegoTerminado;
    public bool calleAnteriorSalioDePantalla;
    public int contadorCalles=0;
    public int numeroSelectorCalles;
    public GameObject autoGO;
    public GameObject audioFXGO;
    public AudioFX audioFXScript;
    public GameObject bgFinalGO;
    
   
    void Start()
    {
        iniciarJuego();
    }

    void iniciarJuego()
    {
        contenedorCallesGO = GameObject.Find("ContenedorCalles");
        //Busco la cámara por su nombre en el editor
        mCamGO = GameObject.Find("MainCamera");
        //Consigo el componente Camera presente dentro de la Main Camera
        mCamComp = mCamGO.GetComponent<Camera>();
        bgFinalGO = GameObject.Find("PanelGameOver");
        bgFinalGO.SetActive(false);
        audioFXGO = GameObject.Find("AudioFX");
        audioFXScript = audioFXGO.GetComponent<AudioFX>();
        autoGO = GameObject.FindObjectOfType<Auto>().gameObject;
        velocidadMotorCarretera();
        medirPantalla();
        buscarCalles();
    }

    public void juegoTerminadoEstados() {
        autoGO.GetComponent<AudioSource>().Stop();
        audioFXScript.fxGameMusic();
        bgFinalGO.SetActive(true);
    }

    void velocidadMotorCarretera()
    {
        velocidad = 18;
    }

    void buscarCalles()
    {
        contenedorCallesArray = GameObject.FindGameObjectsWithTag("Calle");
        for(int i = 0; i < contenedorCallesArray.Length; i++)
        {
            contenedorCallesArray[i].gameObject.transform.parent = contenedorCallesGO.transform;//Hace hijo a la calle del contenedor
            contenedorCallesArray[i].gameObject.SetActive(false);//Desactiva la calle
            contenedorCallesArray[i].gameObject.name = "CalleOFF_" + i;//Le cambia el nombre por CalleOFF
        }
        crearCalles();

    }

    void crearCalles()
    {
        contadorCalles++;
        numeroSelectorCalles = Random.Range(0,contenedorCallesArray.Length);//Genera nro random entre 0 (incluyéndolo) y el tamaño del array (sin incluirlo)
        GameObject calle = Instantiate(contenedorCallesArray[numeroSelectorCalles]);//Creo una copia exactamente igual y la instancio de contenedorCallesArray[numeroSelectorCalles]
        calle.SetActive(true);//Activo la nueva calle
        calle.name = "Calle" + contadorCalles; //Cambio el nombre a la nueva calle
        calle.transform.parent = gameObject.transform;//Hago hija a la nueva calle del motor de carreteras
        posicionarCalles();
    }

    void posicionarCalles()
    {
        calleAnterior = GameObject.Find("Calle"+(contadorCalles-1));//Busco calle anterior a la que acabo de crear
        callePosterior = GameObject.Find("Calle" + contadorCalles);
        medirCalle();
        //Después de medir la calle anterior, posiciono la nueva tomando la posición en x de la calle anterior
        //Posicionamos la nueva calle sobre la anterior teniendo en cuenta la posición de la anterior en Y, y sumando la altura de la calle anterior
        callePosterior.transform.position = new Vector3(calleAnterior.transform.position.x, calleAnterior.transform.position.y + alturaCalle-0.5f, 0);
        calleAnteriorSalioDePantalla = false;
    }

    void medirCalle()
    {   
        //Entro a la calle anterior y cuento la cantidad de hijos que tiene
        for(int i = 0; i < calleAnterior.transform.childCount; i++)
        {
            //Incluye en la operación a todos las piezas que tengan el script "Pieza" incorporado
            if(calleAnterior.transform.GetChild(i).gameObject.GetComponent<Pieza>() != null) {
                //Mediante el SpriteRenderer, consigo el tamaño en Y de la pieza
                float alturaDePieza = calleAnterior.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
                //Lo sumo al acumulador de la altura de la calle
                alturaCalle = alturaCalle + alturaDePieza;
            }
        }
    }

    void medirPantalla() {
        //Consigo la posición en Y debido a que la cámara se ajusta a los dispositivos
        //ScreenToWorldPoint transforma la medida en píxeles de la pantalla y la transforma a vector, agrego media unidad más para tener una tolerancia
        //cuando la calle sale de la pantalla. ScreenToWorldPoint(Vector3).y devuelve el 0 de el eje Y
        medidaLimitePantalla = new Vector3(0, mCamComp.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 0.5f, 0);
    }
    
    void Update()
    {
        if(inicioJuego == true && juegoTerminado == false){ 
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
            //Compruebo si la calle anterior salió de la pantalla si el juego está funcionando
            if (calleAnterior.transform.position.y + alturaCalle < medidaLimitePantalla.y && calleAnteriorSalioDePantalla == false)
            {
                calleAnteriorSalioDePantalla = true;
                //Destruyo la calle anterior
                destruirCalle();
            }
        }
    }

    void destruirCalle() {
        Destroy(calleAnterior);
        alturaCalle = 0;
        calleAnterior = null;
        crearCalles();
    }
}
