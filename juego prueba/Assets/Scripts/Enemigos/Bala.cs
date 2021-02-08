using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    PlayerController objetivo;
    Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objetivo = GameObject.FindObjectOfType<PlayerController>();
        dir = (objetivo.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(dir.x, dir.y);
        Destroy(gameObject, 3f);

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Destroy(gameObject);
            collision.GetComponent<PlayerController>().HurtPlayer(5);
            BarraVida.salud -= 5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
