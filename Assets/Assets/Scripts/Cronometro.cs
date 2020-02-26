using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cronometro : MonoBehaviour
{
    public GameObject motorCarreteraGO;
    public MotorCarretera motorCarreteraScript;
    public GameObject controladorAutoGO;
    public ControladorAuto controladorAutoScript;
    public float tiempo;
    public float distancia;
    public Text txtTiempo;
    public Text txtDistancia;
    public Text txtDistanciaFinal;

    void Start()
    {
        motorCarreteraGO = GameObject.Find("MotorCarretera");
        motorCarreteraScript = motorCarreteraGO.GetComponent<MotorCarretera>();
        controladorAutoGO = GameObject.Find("ControladorAuto");
        controladorAutoScript = controladorAutoGO.GetComponent<ControladorAuto>();
        txtTiempo.text = "2:00";
        txtDistancia.text = "0";
        tiempo = 120;
    }

    void calculoTiempoDistancia()
    {
        //Distancia = tiempo * velocidad
        distancia += Time.deltaTime * motorCarreteraScript.velocidad;
        txtDistancia.text = ((int)distancia).ToString();
        //El tiempo comienza en cuenta regresiva
        tiempo -= Time.deltaTime;
        //El tiempo/60 da los minutos
        int minutos = (int)tiempo / 60;
        //El resto de la división del tiempo/60 da los segundos
        int segundos = (int)tiempo % 60;
        //Con PadLeft dejo con dos ceros los segundos 
        txtTiempo.text = minutos.ToString() + ":" + segundos.ToString().PadLeft(2, '0');

    }

    
    void Update()
    {
        if (motorCarreteraScript.inicioJuego == true && motorCarreteraScript.juegoTerminado == false) { 
            calculoTiempoDistancia();
        }
        //Condición para terminar el juego
        if((tiempo<=0) && (motorCarreteraScript.juegoTerminado == false))
        {
            motorCarreteraScript.juegoTerminado = true;
            motorCarreteraScript.juegoTerminadoEstados();
            txtDistanciaFinal.text = ((int)distancia).ToString()+" mts";
            txtTiempo.text = "0:00";
            controladorAutoScript.habilitarMovimiento = false;
        }
    }
}
