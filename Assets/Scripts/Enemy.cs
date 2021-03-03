using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    
     [SerializeField] float speed = 6;
     Transform player;
     [SerializeField] int scorePoint = 100;
     [SerializeField] int health = 5;
     [SerializeField] AudioClip impactClip;

     void Start()
     {
       player = FindObjectOfType<Player>().transform;
       GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
       int randomSpawnPoint = Random.Range(0,spawnPoint.Length);
       transform.position = spawnPoint[randomSpawnPoint].transform.position;
     }

     private void Update()
     {
         Vector2 direction = player.position - transform.position;
         transform.position += (Vector3) direction.normalized * Time.deltaTime*speed;
     }
     private void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.CompareTag("Player"))
         
             collision.GetComponent<Player>().TakeDamage();   
       
     }

     public void TakeDamage()
     {
         health--;
         AudioSource.PlayClipAtPoint(impactClip,transform.position);
         if (health <= 0)
         {
             GameManager.Instance.Score += scorePoint;
             Destroy(gameObject,0.1f);
         }
     }        
       
     }
        
       
     

