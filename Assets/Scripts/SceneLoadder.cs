using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadder : MonoBehaviour
{
    public void loadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
    public void QuitScene()
    {
        Application.Quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
    }

}
