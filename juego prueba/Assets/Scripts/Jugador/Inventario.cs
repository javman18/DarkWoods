using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    //array para crear el inventario
    public GameObject[] llaves = new GameObject[10];
    
    public void AgregaObj (GameObject obj)
    {
        //al principio no hay objetos en el inventario 
        bool objEnInventario = false;
        //encontrar el primer lugar libre en el inventario
        for(int i=0; i<llaves.Length; i++)
        {
            if(llaves[i] == null)
            {
                //las llaves son el objeto que se tiene que almacenar en el array
                llaves[i] = obj;
                Debug.Log(obj.name+" agregada");
                //es verdadero al estar en el inventario
                objEnInventario = true;
                //llama a la funcion de los objetos en ObjetosInteract
                obj.SendMessage("Recoger");
                //break para hacerlo solo 1 vez
                break;
            }
            if (!objEnInventario) 
            {
                Debug.Log("inventario lleno");
            }
        }
    }

    public bool EncuentraObj(GameObject llave)
    {
        for(int i=0; i < llaves.Length; i++)
        {
            if(llaves[i] == llave)
            {
                //objeto en inventario
                return true;
            }
        }
        //no se encuentra en el inventario
        return false;
    }

    public void EliminaObj(GameObject llave)
    {
        for(int i = 0; i<llaves.Length; i++)
        {
            if(llaves[i] == llave)
            {
                Debug.Log("llave eliminada");
                llaves[i] = null;
                //elimina solo 1 objeto
                break;
            }
        }
    }
}
