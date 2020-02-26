using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuentaAtras : MonoBehaviour
{
    //Almaceno el motor de carretera y su script
    public GameObject motorCarreteraGO;
    public MotorCarretera motorCarreteraScript;
    //Almaceno las cuatro imágenes de inicio (3,2,1,Go!)
    //cambiar en el editor el size de este array, asignando cada imagen a cada elemento
    public Sprite[] numeros;
    //Almaceno el contador de números y su componente SpriteRenderer
    public GameObject contadorNumerosGO;
    public SpriteRenderer contadorNumerosComp;
    //Almaceno el controlador del auto y el auto
    public GameObject controladorAutoGO;
    public ControladorAuto controladorAutoScript;
    public GameObject autoGO;

    void Start()
    {
        inicioComponentes();
    }

    void inicioComponentes() {
        //Ubico el motor de carretera y el script dentro de él
        motorCarreteraGO = GameObject.Find("MotorCarretera");
        motorCarreteraScript = motorCarreteraGO.GetComponent<MotorCarretera>();
        //Ubico el contador de números y el SpriteRenderer dentro de él
        contadorNumerosGO = GameObject.Find("ContadorNumeros");
        contadorNumerosComp = contadorNumerosGO.GetComponent<SpriteRenderer>();
        //Ubico el auto y el controlador del auto
        autoGO = GameObject.Find("Auto");
        controladorAutoGO = GameObject.Find("ControladorAuto");
        controladorAutoScript = controladorAutoGO.GetComponent<ControladorAuto>();
        inicioCuentaAtras();
    }

    void inicioCuentaAtras() {
        StartCoroutine(contando());
    }

    IEnumerator contando() {
        //Reproduzco clip de audio de arranque de motor en controladorAuto
        controladorAutoGO.GetComponent<AudioSource>().Play();
        //Espero 2 segundos
        yield return new WaitForSeconds(2);
        //Cambio el sprite en el contador de números por el número 2
        contadorNumerosComp.sprite = numeros[1];
        //Reproduzco clip de audio dentro de CuentaAtras
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        contadorNumerosComp.sprite = numeros[2];
        //Reproduzco clip de audio dentro de CuentaAtras
        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        //Muestro último elemento en el contador (Go!)
        contadorNumerosComp.sprite = numeros[3];
        //Inicio el juego cambiando el valor del bool inicioJuego
        //presente en el script del motor de carretera
        motorCarreteraScript.inicioJuego = true;
        controladorAutoScript.habilitarMovimiento = true;
        //Reproduzco clip de audio de sirena en ContadorNumeros
        contadorNumerosGO.GetComponent<AudioSource>().Play();
        //Reproduzco clip de audio de motor funcionando en el auto
        autoGO.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        //Quito el contador de números
        contadorNumerosGO.SetActive(false);
        
        
    }
}
