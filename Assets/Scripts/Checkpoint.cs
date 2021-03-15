using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int addedTime = 10;
    [SerializeField] private AudioClip checkPointClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(checkPointClip,transform.position);
            GameManager.Instance.time += addedTime;
            Destroy(gameObject, 0.1f);
        }
    }
}
