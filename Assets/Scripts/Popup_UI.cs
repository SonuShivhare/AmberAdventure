using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_UI : MonoBehaviour
{
    public GameObject dialogueBox;
    public Vector3 distance;

    private bool isActive = false;
    
    private void Update()
    {
        if(isActive)
            dialogueBox.transform.position = transform.position + distance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isActive = true;
            Debug.Log(other.gameObject.name);
            dialogueBox.SetActive(true);
        } 
    }
   
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isActive = false;
            dialogueBox.SetActive(false);
        } 
    }
}
