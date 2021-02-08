using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    // Start is called before the first frame update
    private float largo, inicio;
    public GameObject cam;
    public float parallax;
    void Start()
    {
        largo = GetComponent<SpriteRenderer>().bounds.size.x;
        inicio = transform.position.x;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float temp = (cam.transform.position.x * (1 - parallax));
        float dist = (cam.transform.position.x * parallax);
        transform.position = new Vector3(inicio + dist, transform.position.y, transform.position.z);
        if(temp > inicio+largo)
        {
            inicio += largo;

        }
        else if(temp < inicio-largo)
        {
            inicio -= largo;

        }
        
    }
}
