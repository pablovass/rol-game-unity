using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int difficulty;
    public static GameManager Instance; 
    
    [SerializeField]int time = 30;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance=this;
        }
    }

    private void Start()
    {
        StartCoroutine(CountdownRoutine());
    }
    
    IEnumerator CountdownRoutine()
    {
        while (time>0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
    }
}
