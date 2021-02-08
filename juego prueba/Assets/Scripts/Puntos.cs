using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntos : MonoBehaviour
{

    public static int puntos = 0;
    Text textoPuntos;
    // Start is called before the first frame update
    void Start()
    {
        textoPuntos = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textoPuntos.text = "Orbes: " + puntos;
    }
}
