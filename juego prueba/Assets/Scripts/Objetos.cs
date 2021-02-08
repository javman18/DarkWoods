using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetos : MonoBehaviour
{

    //script para las llaves y puertas
    //bool publico para que desde el inspector puedea activar si se va a ir al inventario si es verdadero
    public bool inventario;
    //para activar si la puerta se abre tiene que ser verdadero
    public bool openDoor;
    //para activar si la puerta esta cerrada tiene que ser verdadero
    public bool locked;
    //el objeto que se requiere para abrir la puerta es publico para agregarlo a la puerta y que
    //se elimine
    public GameObject llave;
    //para agregar la animacion a la puerta
    public Animator anim;
    //funcion que desactiva la llave al recogerla
    public void Recoger()
    {
        //recoger y poner en el inventario
        gameObject.SetActive(false);
    }

    //activa la animacion de la puerta
    public void Open()
    {
        anim.SetBool("open", true);
        
    }
    
}
