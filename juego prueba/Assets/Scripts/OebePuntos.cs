using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OebePuntos : MonoBehaviour
{
    
    
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.gameObject.tag=="Player")
        {
            
            Destroy(gameObject);
            Puntos.puntos++;
            PlayerController.vidaActual = PlayerController.vidaMaxima;
            BarraVida.salud = PlayerController.vidaMaxima;
            
           
        }

        
    }
}
