using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] float speed=6;
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
            Destroy(gameObject);
        }
        
       
    }
}
