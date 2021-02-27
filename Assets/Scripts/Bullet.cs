using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //instancia de la velocidad de la vala
    [SerializeField] float speed=6;
    // Update is called once per frame
    void Update()
    {   //le decimos que valla a derechar 
        //con delta time y con la variable de speed le regulamos la vecidad.
        
        transform.position += transform.right * Time.deltaTime * speed;
        
    }
}
