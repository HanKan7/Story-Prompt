using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{

    public TextMeshProUGUI dialogText;
    public float typingSpeed = 0.2f;
    public List<string> dialogs = new List<string>();
    public GameObject continueButton;
    int index = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(PrintDialog());
            GetComponent<CapsuleCollider2D>().enabled = false;
            //collision.gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    private void Update()
    {
        if(dialogText.text == dialogs[index])
        {
            continueButton.SetActive(true);
        } 
    }

    IEnumerator PrintDialog()
    {
        foreach (char letter in dialogs[index].ToCharArray())
        {
            dialogText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if(index < dialogs.Count - 1)
        {
            index++;
            dialogText.text = "";
            StartCoroutine(PrintDialog());
        }
        else
        {
            dialogText.text = "";
            continueButton.SetActive(false);
            index = 0;
            GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
}
