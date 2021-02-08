using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SombraDisp : MonoBehaviour
{
    public Transform objetivo;
    private bool viendoDer;
    public GameObject bala;
    public float tiempoDisp;
    public float sigDisp;
    public float radio;
    

    // Start is called before the first frame update
    void Start()
    {
        tiempoDisp = 1f;
        sigDisp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Distancia();
        flip();
    }

    void DisparoSiguiente()
    {
        if(Time.time > sigDisp)
        {
            Instantiate(bala, transform.position, Quaternion.identity);
            sigDisp = Time.time + tiempoDisp;
        }
    }
    void Distancia()
    {
        //detecta al jugador a una distancia especifica y lo persigue si la posision del jugador
        //es menor o igual a la sombra y le agrega el radio del jugador para no ponerse encima
        if (Vector3.Distance(transform.position, objetivo.position) <=
            radio )
        {
            //se dirige hacia el jugador
            DisparoSiguiente();
        }
    }
    void flip()
    {
        if (objetivo.position.x > transform.position.x || objetivo.position.x < transform.position.x)
        {
            viendoDer = !viendoDer;
            // la escala es 1 en x, para ver a la izquierda tiene que ser -1 por lo que se multiplica x por -1
            Vector2 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
}
