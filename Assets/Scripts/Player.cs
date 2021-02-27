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
    [SerializeField]float speed = 10;
    [SerializeField] Transform aim;
    [SerializeField]  Camera camera; 
    Vector2 facingDirection;
    [SerializeField] Transform bulletPrefabs;
    bool gunLoaded = true; 

    [SerializeField]float fireRate; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
             Instantiate(bulletPrefabs,transform.position,targetRotation);
             StartCoroutine(ReloadGun());
         }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1/fireRate);  
        gunLoaded = true;
    }
}
