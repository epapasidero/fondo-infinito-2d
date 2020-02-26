using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Traigo framework para cambiar de escena
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fundido : MonoBehaviour
{
    public Image fundido;
    public string[] escenas;

    void Start()
    {
        //Hago un fundido de color sólido a transparente
        fundido.CrossFadeAlpha(0,0.5f,false);   
    }
    //Transparencia 0 a color sólido
    public void fadeOut(int s) {
        fundido.CrossFadeAlpha(1, 0.5f, false);
        StartCoroutine(cambioEscena(escenas[s]));
    }

    IEnumerator cambioEscena(string escena) {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(escena);
    }


}
