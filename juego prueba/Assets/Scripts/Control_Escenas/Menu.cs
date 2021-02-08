using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Animator anim;
   public void Jugar()
    {
        Puntos.puntos = 0;
        SceneManager.LoadScene(2);
        

    }
    public void Salir()
    {
        Application.Quit();

    }
}
