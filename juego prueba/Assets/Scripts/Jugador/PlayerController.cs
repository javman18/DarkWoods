using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int puntosG = 0;
    public Text puntosGema;
    // Start is called before the first frame update
    public float alto;
    // primero agrego un flotante de velocidad
    public float speed;
    //llamo al rigidbody del jugador
    private Rigidbody2D rb;
    //dash
    public float coolDown = 2f;
    public float usoDeHabilidad = 0f;
    public float velDash;
    private float tiempoDash;
    public float iniciaDash;
    public int direccion;
    // el movimiento del jugador 
    private Vector2 moveVel;
    public float movimiento;
    //cambio de direccion
    private bool viendoDer;
    // para saber si toca tierra
    private bool grounded;
    //velocidad de la caida 
    public float cae = 2.5f;
    private bool jumping;
    //saltos extra
    private int extraJumps;
    public int extraJumpsValue;
    // limites del salto    
    public float saltomax;
    public float saltomenor;
    //ataque
    public Transform puntoAtaque;
    public LayerMask Enemy;
    public int danoAtack = 100;
    public float rangoAtaque = 0.5f;
    //animaciones
    public Animator anim;
    //vida del personaje
    public static float vidaActual;
    public static float vidaMaxima=100f;

    void Start()
    {
        
        // llama a su Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        vidaActual = vidaMaxima;
        tiempoDash = iniciaDash;
    }
    
    // Update is called once per frame
    void Update()
    {
        puntosGema.text = "Gemas: " + puntosG;
        //el movimiento
        movimiento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movimiento * speed, rb.velocity.y);
        
        anim.SetFloat("Speed", Mathf.Abs(movimiento));
        if (direccion == 0)
        {
            if (Time.time > usoDeHabilidad)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    usoDeHabilidad = Time.time + coolDown;
                    if (movimiento < 0)
                    {
                        anim.SetBool("Dash", true);
                        direccion = 1;
                    }
                    else if (movimiento > 0)
                    {
                        anim.SetBool("Dash", true);
                        direccion = 2;
                    }
                }
            }
           
        }
        else
        {
            if (tiempoDash >= 0)
            {
                tiempoDash -= Time.deltaTime;
                anim.SetBool("Dash", true);
                if (direccion == 1)
                {
                    rb.velocity = Vector2.left * velDash;
                }
                else if (direccion == 2)
                {

                    rb.velocity = Vector2.right * velDash;
                }
               
            }
            else if (tiempoDash <=0)
            {

                anim.SetBool("Dash", false);
                direccion = 0;
                tiempoDash = iniciaDash;
                rb.velocity = Vector2.zero;
            }

           
        }


        // salto del personaje si esta tocando tierra
        if(grounded==true)
        {
            extraJumps = extraJumpsValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps>0)
        {
            jumping = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * saltomax;
            extraJumps--;
            // cuando salta deja de tocar tierra 
            grounded = false;
        }else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && grounded==true)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * saltomax;
        }
        //animaciones del personaje de salto
        else if (grounded == false)
        {
            if (rb.velocity.y > 0)
            {
                anim.SetBool("Grounded", false);
                anim.SetBool("Jump", true);
                anim.SetBool("Falling", false);
            }
            else if (rb.velocity.y < 0)
            {

                anim.SetBool("Grounded", false);
                anim.SetBool("Falling", true);
                anim.SetBool("Jump", false);
            }
        }
        else if (grounded == true)
        {
            anim.SetBool("Grounded", true);
            anim.SetBool("Jump", false);
            anim.SetBool("Falling", false);
        }
        //ver en la direccion que avanzas 
        if (movimiento > 0)
        {
            //bala.rb.velocity = transform.right * bala.vel * -1;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (movimiento < 0)
        {
            //    bala.rb.velocity = transform.right * bala.vel;
            transform.eulerAngles = new Vector3(0, 180, 0); ;
        }

                //aplica gravedad si la velocidad del Rigidbody en y es menor a 0 y lo multiplica por la velocidad de caida 
        if (rb.velocity.y < 0)
        {

            rb.velocity += Vector2.up * Physics.gravity.y * cae * Time.deltaTime;
        }
        //salta mas alto si mantiene espacio y la velocidad del Rigidbody es mayor a 0, el saltomenor es el limite
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity.y * saltomenor * Time.deltaTime;
        }

        //el ataque es con ctrl derecho o boton del mouse izquerdo
        if(Input.GetButtonDown("Fire1"))
        {
            Ataque();
        }
        if(transform.position.y < alto)
        {
            Debug.Log("caiste");
            SceneManager.LoadScene(1);
            Puntos.puntos = 0;
        }

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //detecta si toca piso
        if (collision.gameObject.tag == "Piso" || collision.gameObject.tag == "Plataforma"|| collision.gameObject.tag == "Plataforma2")
        {
            grounded = true;
        }
    }

    void Ataque()
    {
        anim.SetTrigger("Ataque");
        //crea un circulo invisible que se activa al atacar
        Collider2D[] detectaE=Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, Enemy);
        //solo afecta a los enemigos
        foreach(Collider2D enemigo in detectaE)
        {
            
            enemigo.GetComponent<Sombra>().DanoAtaque(danoAtack);
        }
    }
   
    
    public void HurtPlayer (int damage)
    {
        vidaActual -= damage;
        Debug.Log("jugador" + vidaActual);
        if (vidaActual <= 0)
        {
            Muere();
            SceneManager.LoadScene(1);
            Puntos.puntos = 0;
        }
    }

    void Muere()
    {
        Debug.Log("Moriste!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gema")
        {
            Destroy(collision.gameObject);
            puntosG++;
            
           
        }
    }




}
