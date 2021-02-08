using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject circulo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //la camara sigue al personaje
        transform.position = new Vector3(circulo.transform.position.x, circulo.transform.position.y +1 , transform.position.z);
    }
}
