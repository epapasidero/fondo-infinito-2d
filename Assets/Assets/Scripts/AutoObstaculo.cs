using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoObstaculo : MonoBehaviour
{
    //Se obtiene el cronómetro y su script correspondiente
    public GameObject cronometroGO;
    public Cronometro cronometroScript;
    //Se obtiene el GameObject que triggea los dos clips de audio
    //y su script
    public GameObject audioFXGO;
    public AudioFX audioFXScript;

    void Start()
    {
        cronometroGO = GameObject.FindObjectOfType<Cronometro>().gameObject;
        cronometroScript = cronometroGO.GetComponent<Cronometro>();
        audioFXGO = GameObject.FindObjectOfType<AudioFX>().gameObject;
        audioFXScript = audioFXGO.GetComponent<AudioFX>();
    }


    //Cuando colisiona el jugador contra el obstáculo
    void OnTriggerEnter2D(Collider2D other)
    {
        //Si choca con el gameobject que tiene un componente Auto
        if (other.GetComponent<Auto>() != null) {
            audioFXScript.fxSonidoChoque();
            cronometroScript.tiempo = cronometroScript.tiempo - 20;
            //Destruyo el auto obstáculo
            Destroy(this.gameObject);
        }    
    }

}
