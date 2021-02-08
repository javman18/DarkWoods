using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
//aplica herencia 
public class Sombra : MonoBehaviour
{
    public float empuje;
    public AudioClip clip;
    public float vol;
    AudioSource audio;
    public bool alreadyPlayed=false;
    GameObject[] sombras;
   //la posicion del jugador
    public Transform objetivo;
    //deteccion
    public float radio;
    public float radioAtaque;
    //ataque
    public int danoAtack = 5;
    //vida de la sombra
    public int vidaMaxima = 200;
    public int vidaActual;
    //velocidad de la sombra
    public float vel;
    public Transform posInicial;
    private bool viendoDer;
    //efecto de sombra cuando la hieren
    private bool flashActive;
    public float duracionFlash;
    public float flashC;
    public SpriteRenderer enemigoSprite;
    
   
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        sombras = GameObject.FindGameObjectsWithTag("Enemy");
        enemigoSprite = GetComponent<SpriteRenderer>();
        //la vida actual del enemigo es la vida maxima
        vidaActual = vidaMaxima;
        //define el objetivo del enemigo 
        objetivo = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //define el parpadeo de la sombra
        if(flashActive)
        {
            if(flashC>duracionFlash *.99f)
            {
                enemigoSprite.color = new Color(enemigoSprite.color.r, enemigoSprite.color.g, enemigoSprite.color.b, 0f);
            }else if(flashC > duracionFlash * .82f)
            {
                enemigoSprite.color = new Color(enemigoSprite.color.r, enemigoSprite.color.g, enemigoSprite.color.b, 1f);
            }
            else if (flashC > duracionFlash * .66f)
            {
                enemigoSprite.color = new Color(enemigoSprite.color.r, enemigoSprite.color.g, enemigoSprite.color.b, 0f);
            }
            else if (flashC > duracionFlash * .49f)
            {
                enemigoSprite.color = new Color(enemigoSprite.color.r, enemigoSprite.color.g, enemigoSprite.color.b, 1f);
            }
            else if (flashC > duracionFlash * .33f)
            {
                enemigoSprite.color = new Color(enemigoSprite.color.r, enemigoSprite.color.g, enemigoSprite.color.b, 0f);
            }
            else if (flashC > duracionFlash * 16f)
            {
                enemigoSprite.color = new Color(enemigoSprite.color.r, enemigoSprite.color.g, enemigoSprite.color.b, 1f);
            }
            else if (flashC > duracionFlash * 0f)
            {
                enemigoSprite.color = new Color(enemigoSprite.color.r, enemigoSprite.color.g, enemigoSprite.color.b, 0f);
            }
            else
            {
                enemigoSprite.color = new Color(enemigoSprite.color.r, enemigoSprite.color.g, enemigoSprite.color.b, 1f);
                flashActive = false;
            }
            flashC -= Time.deltaTime;
        
        }
        
        Distancia();
        flip();
    }
    //funcion para detectar si el personaje esta en rango
    void Distancia()
    {
        //detecta al jugador a una distancia especifica y lo persigue si la posision del jugador
        //es menor o igual a la sombra y le agrega el radio del jugador para no ponerse encima
        if (Vector3.Distance(transform.position, objetivo.position) <= 
            radio && Vector3.Distance(transform.position, objetivo.position) > radioAtaque)
        {
            //se dirige hacia el jugador
            transform.position = Vector3.MoveTowards(transform.position, objetivo.position, vel * Time.deltaTime);
            audio.PlayOneShot(clip, vol);
        }

    }

    public void DanoAtaque(int dano)
    {
        //el ataque del jugador a la sombra
        vidaActual -= dano;
        //activa el parpadeo
        flashActive = true;
        flashC = duracionFlash;
        Debug.Log("Sombra " + vidaActual);

        if(vidaActual <= 0)
        {
            Muere();
        }
    }
    void Muere()
    {
        transform.position = new Vector3(100, 100, 100);
       
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
    //hiere al jugador si detecta colision
    void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Player")
       {
            //llamar al RB de la sombra
            Rigidbody2D sombra = GetComponent<Rigidbody2D>();
            
            collision.GetComponent<PlayerController>().HurtPlayer(danoAtack);
            BarraVida.salud -= danoAtack;
            //empezar courutina cada vez que choca con el jjugador
            StartCoroutine(Retroceso(sombra));
            
            
       }
    }

    private IEnumerator Retroceso(Rigidbody2D sombra)
    {
        //establecer direccion de empuje de la sombra
        Vector2 dir = sombra.transform.position - objetivo.transform.position;
        //establecer la fuerza de retroceso al chocar con el jugador
        Vector2 fuerza = dir.normalized * empuje;
        //establecer que la fuerza es la velocidad del RB de la sombra
        sombra.velocity = fuerza;
        //esperar .3 segundos 
        yield return new WaitForSeconds(.3f);
        //la velocidad es un nuevo vector 2
        sombra.velocity = new Vector2();
    }

      


}
