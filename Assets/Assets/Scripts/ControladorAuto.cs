using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAuto : MonoBehaviour
{
    public GameObject autoGO;
    public float anguloDeGiro=45;
    //Velocidad de desplazamiento lateral
    public float velocidad=15;
    public bool habilitarMovimiento;


    // Start is called before the first frame update
    void Start()
    {
        //Busco objeto del tipo Auto y con el gameObject final lo devuelvo
        autoGO = GameObject.FindObjectOfType<Auto>().gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (habilitarMovimiento)
        {
        //Posición de giro mientras no se toque ninguna tecla
        float giroEnZ = 0;
        //Vector2 mueve solo derecha/izquierda mediante el axis "Horizontal" Ver en Edit...Input
        transform.Translate(Vector2.right*Input.GetAxis("Horizontal")*velocidad*Time.deltaTime);
        //El ángulo de giro es negativo por que sino parece que rota desde el eje trasero
        giroEnZ = Input.GetAxis("Horizontal") * (-anguloDeGiro);
        //Transformo valor en decimales en ángulo, mediante el Quaternion.Euler
        //Rota en Z únicamente
        autoGO.transform.rotation = Quaternion.Euler(0, 0, giroEnZ);
        }
    }
}
