using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ControlLuz : MonoBehaviour
{
    public AudioClip clip;
    public float vol;
    AudioSource audio;
    
    GameObject[] plat;
    GameObject [] sombra;
    GameObject[] plat2;
    public float coolDown = 2f;
    public float prendeLuz = 0f;
    Light2D lt;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        plat2 = GameObject.FindGameObjectsWithTag("Plataforma2");
        plat = GameObject.FindGameObjectsWithTag("Plataforma");
        sombra = GameObject.FindGameObjectsWithTag("Enemy");
        lt = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PrendeLuz();
        if(lt.intensity<=.5f)
        {
            
            foreach (GameObject pl in plat)
            {

                pl.SetActive(false);
            }
            foreach (GameObject enemy in sombra)
            {
                
                enemy.SetActive(false);
            }
            foreach (GameObject pl in plat2)
            {

                pl.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject enemy in sombra)
            {
                enemy.SetActive(true);
            }

            foreach (GameObject pl in plat)
            {

                pl.SetActive(true);
            }
            foreach (GameObject pl in plat2)
            {

                pl.SetActive(false);
            }
        }

    }
    void PrendeLuz()
    {
        
            
           if(lt.intensity==1f)
           {
            if (Input.GetKeyDown(KeyCode.X))
            {
                audio.Play();
                lt.intensity = .5f;
                lt.pointLightOuterRadius = 1f;
                lt.pointLightInnerRadius = 0f;
            }
           }else if (lt.intensity==.5f)
           {
            if (Input.GetKeyDown(KeyCode.X))
            {
                audio.Play();
                lt.intensity = 1f;
                lt.pointLightOuterRadius = 6f;
                lt.pointLightInnerRadius = 4.6f;
            }

           }


    }
}
