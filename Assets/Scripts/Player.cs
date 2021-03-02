using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    float h;
    float v;
    private Vector3 moveDirection;
    public float speed = 10;
    [SerializeField] Transform aim;
    [SerializeField]  Camera camera; 
    Vector2 facingDirection;
    [SerializeField] Transform bulletPrefabs;
    bool gunLoaded = true; 
    [SerializeField]float fireRate; 
    [SerializeField]int health = 5;
     bool powerShotEnabled;
     bool invulnerable;
    [SerializeField] float invulnerableTime=3;
    [SerializeField] Animator anim ;
    [SerializeField]  SpriteRenderer spriteRenderer;

public int Health { get=>health;
         set
         {
             health = value;
             UIManager.Instance.UpdateUIHelth(health);
         } }
    void Start()
    {
        
    }

    void Update()
    {
        h=Input.GetAxis("Horizontal"); 
        v=Input.GetAxis("Vertical"); 
        moveDirection.x = h;
        moveDirection.y = v;
        
        transform.position += moveDirection * Time.deltaTime * speed; 
        //la mira
        facingDirection= camera.ScreenToWorldPoint(Input.mousePosition)- transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized;
        
         if (Input.GetMouseButton(0)&& gunLoaded)
         { 
             gunLoaded = false;
             float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
             Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
             // hacer que las balas que disparemos sean diferentes
             Transform bulletClone=Instantiate(bulletPrefabs,transform.position,targetRotation);
             //condicional que nos va a dar que tipo de vala va ser 
             if (powerShotEnabled)
             {
                 bulletClone.GetComponent<Bullet>().powerShot = true;
             }
             StartCoroutine(ReloadGun());
         }
        //Contro la animacion
         anim.SetFloat("Speed",moveDirection.magnitude);
         if (aim.position.x > transform.position.x)
         {
             spriteRenderer.flipX = true;
         }else if (aim.position.x < transform.position.x)
         {
             spriteRenderer.flipX = false;
         }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1/fireRate);  
        gunLoaded = true;
    }
   
    public void TakeDamage()
    {
        if (invulnerable)
            return;
        
        Health--;  
        invulnerable = true;
        StartCoroutine(MakeVulnerableAgain());
        if (Health <= 0)
        {
           GameManager.Instance.gameOver = true;
           UIManager.Instance.ShowGameOverScreen();
        }
    }
    IEnumerator MakeVulnerableAgain()
    {
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }
 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            switch (collision.GetComponent<PowerUp>().powerUpType)
            {
                case PowerUp.PowerUpType.FireRateIncrease:
                    fireRate++;
                    break;
                case PowerUp.PowerUpType.PowerShot:
                    powerShotEnabled = true;
                     break;
            }
            Destroy(collision.gameObject, 0.1f);
            
           
        }
    }
}
