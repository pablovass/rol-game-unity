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
    [SerializeField]  float blinkRate;
    CameraController cameraController;
    [SerializeField] AudioClip powerUpClip;
   
   
    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
        UIManager.Instance.UpdateUIHelth(health);
    }

    void Update()
    {
        ReadInput();
        
        transform.position += moveDirection * Time.deltaTime * speed; 
        
        facingDirection= camera.ScreenToWorldPoint(Input.mousePosition)- transform.position;
        aim.position = transform.position + (Vector3)facingDirection.normalized;
        
        if (Input.GetMouseButton(0) && gunLoaded)
        {
            Shoot();
        }

       UpdatePlayerGraphics();
    
        
    }

    void UpdatePlayerGraphics()
    {
        anim.SetFloat("Speed",moveDirection.magnitude);
        if (aim.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }else if (aim.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    void ReadInput()
    {
        h=Input.GetAxis("Horizontal"); 
        v=Input.GetAxis("Vertical"); 
        moveDirection.x = h;
        moveDirection.y = v;

    }
    public int Health 
    {
        get => health;
        set
        {
            health = value;
            UIManager.Instance.UpdateUIHelth(health);
        } 
    }

    void Shoot()
    {
        
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Transform bulletClone=Instantiate(bulletPrefabs,transform.position,targetRotation);
            if (powerShotEnabled)
            {
                bulletClone.GetComponent<Bullet>().powerShot = true;
            }
            StartCoroutine(ReloadGun());
        
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
        fireRate = 1;
        powerShotEnabled = false;
        //cameraController.Shake();
        StartCoroutine(MakeVulnerableAgain());
        if (Health <= 0)
        {
           GameManager.Instance.gameOver = true;
           UIManager.Instance.ShowGameOverScreen();
        }
    }
    IEnumerator MakeVulnerableAgain()
    {
        StartCoroutine(BlinkRoutine());
        yield return new WaitForSeconds(invulnerableTime);
        invulnerable = false;
    }

    IEnumerator BlinkRoutine()
    {
        int t = 10;
        while (t > 0)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(t * blinkRate);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(t * blinkRate);
            t--;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            AudioSource.PlayClipAtPoint(powerUpClip,transform.position);
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