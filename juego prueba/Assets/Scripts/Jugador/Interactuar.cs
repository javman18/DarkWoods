using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuar : MonoBehaviour
{
    //para poder acceder a los objetos, es null por defecto, o sea que no esta en el juego al inicio
    // y se activa con la colision
    public Objetos objeto = null;  

    public GameObject interactuable = null;  //hacerlo un gameobject null para que se active con la colision

    //para aceder al inventario y agregarlo en el inspector
    public Inventario inventario;

    void Update()
    {
        //recoge la llave si presion Q y esta en el rango del objeto
        if(Input.GetKeyDown(KeyCode.Q) && interactuable)
        {
            //verificar si la variable booleana del objete es verdader para agregar al inventario
            if (objeto.inventario)
            {
                inventario.AgregaObj(interactuable);
            }
            //verificar si el objeto puede ser abierto
            if(objeto.openDoor)
            {
                //verificar si esta cerrado
                if(objeto.locked)
                {
                    //verificar se el objeto esta en el inventaro
                    //si esta en el inventario, desboquea la puerta
                    if(inventario.EncuentraObj(objeto.llave))
                    {
                        objeto.locked = false;
                        Debug.Log(interactuable.name + " desbloqueada");
                    }
                   
                }
                else
                {
                    //desbloquea la puerta y elimina la llave del array
                    objeto.Open();
                    inventario.EliminaObj(objeto.llave);
                }
            }
        }
    }
    //define el rango del jugador y la llave siendo trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Llave"))
        {
            Debug.Log("llave");
            //detecta a la llave
            interactuable = other.gameObject;
            //agrega el script a la llave si hay colision
            objeto = interactuable.GetComponent<Objetos>();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Llave"))
        {
            //eliminar la llave si sale de la colision, para que no la recoja
            if (other.gameObject == interactuable)
            {
                interactuable = null;
            }
        }
    }
}
