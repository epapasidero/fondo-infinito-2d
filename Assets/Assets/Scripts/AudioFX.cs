using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour
{
    //Creo array de audioclips para almacenar todos los sonidos necesarios
    public AudioClip[] fxs;
    AudioSource audioSrc;
    //0 = Choque
    //1 = Game Music

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    public void fxSonidoChoque() {
        //Seteo clip y reproduzco sonido
        audioSrc.clip = fxs[0];
        audioSrc.Play();
    } 

    public void fxGameMusic()
    {
        audioSrc.clip = fxs[1];
        audioSrc.Play();
    }

    public void fxSonidoPowerUp()
    {
        //Seteo clip y reproduzco sonido
        audioSrc.clip = fxs[2];
        audioSrc.Play();
    }

}
