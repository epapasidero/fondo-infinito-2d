using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject cronometroGO;
    public Cronometro cronometroScript;
    public GameObject audioFXGO;
    public AudioFX audioFXScript;

    void Start()
    {
        cronometroGO = GameObject.FindObjectOfType<Cronometro>().gameObject;
        cronometroScript = cronometroGO.GetComponent<Cronometro>();
        audioFXGO = GameObject.FindObjectOfType<AudioFX>().gameObject;
        audioFXScript = audioFXGO.GetComponent<AudioFX>();

    }


    //Cuando colisiona el jugador contra el power up
    void OnTriggerEnter2D(Collider2D other)
    {
        //Si choca con el gameobject que tiene un componente Auto
        if (other.GetComponent<Auto>() != null)
        {
            audioFXScript.fxSonidoPowerUp();
            cronometroScript.tiempo = cronometroScript.tiempo + 20;
            //Destruyo el auto obstáculo
            Destroy(this.gameObject);
        }
    }
}
