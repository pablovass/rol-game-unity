using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartController : MonoBehaviour
{
    [SerializeField]  AudioClip buttonClip;

    public void LoadGame()
    {
        AudioSource.PlayClipAtPoint(buttonClip, transform.position);
        Invoke("LoadGameScene",0.5f);
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
