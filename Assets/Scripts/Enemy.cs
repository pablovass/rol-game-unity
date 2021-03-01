using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // para controlar la velocidad de la persecucion 
    [SerializeField] float speed = 6;
    //REFERENCIA NUESTRO PLAYER 
     Transform player;
     // la salud d la vida 
     [SerializeField]int health = 5;


     void Start()
     {
       player = FindObjectOfType<Player>().transform;
     }

     private void Update()
     {
         Vector2 direction = player.position - transform.position;
         transform.position += (Vector3) direction * Time.deltaTime*speed;
     }
     private void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.CompareTag("Player"))
         
             collision.GetComponent<Player>().TakeDamage();   
       
     }

     public void TakeDamage()
     {
         health--;
         if (health <= 0)
         {
             Destroy(gameObject);
         }
     }        
       
     }
        
       
     

