using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    //definir la imagen de la barra
    Image barra;
    //esta salud no es la del jugador pero cumple el mismo proposito
    float saludMaxima = 100f;
    public static float salud;
    // Start is called before the first frame update
    void Start()
    {
        //defino que la barra es la imagen
        barra = GetComponent<Image>();
        salud = saludMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        // le quito la salud cuando la sombra toca al personaje
        barra.fillAmount = salud / saludMaxima;
    }
}
