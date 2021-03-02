using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] float speed=6;
    [SerializeField] int health = 3;
    public bool powerShot;
    
    private void Start()
    {
        Destroy(gameObject,5);
    }

    void Update()
    {     transform.position += transform.right * Time.deltaTime * speed;
        
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage();  
            if(!powerShot)
                Destroy(gameObject);

            health--;
          
            if(health <=0)
                 Destroy(gameObject);
        }
        
       
    }
}
