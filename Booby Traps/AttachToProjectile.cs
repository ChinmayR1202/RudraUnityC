using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToProjectile : MonoBehaviour
{

    [SerializeField] private GameObject explosion;
    private GameObject e;
    private SpriteRenderer projectile;
    private Rigidbody2D rb;
    private float timer;

    public bool shoot;

    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        shoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ledge") || collision.CompareTag("Pillar"))
        {
            e = Instantiate(explosion) as GameObject;
            e.transform.position = transform.position;
            projectile.color = new Color(1, 1, 1, 0);        
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Ledge") || collision.CompareTag("Pillar"))
        {
            timer += Time.deltaTime;
            if(timer >= 2)
            {
                
            }

            Destroy(this.gameObject);
            Destroy(e);
        }
    }
}
