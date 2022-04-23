using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{

    public TextMeshProUGUI dialogText;
    public GameObject nextLine;
    public float typingSpeed = 0.2f;
    public List<string> dialogs = new List<string>();
    public GameObject continueButton;
    int index = 0;

    PlayerController playerController;
    public float enablePlayerControllerTimer = 0.5f;
    public float enableColliderTimer = 5f;

    bool canDialogue = false;
    bool dialoguing = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {/*
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(PrintDialog());
            GetComponent<CapsuleCollider2D>().enabled = false;
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.moveVector = Vector3.zero;
            playerController.playerAnim.SetBool("Dialog", true);
            playerController.enabled = false;
        }*/
        playerController = collision.gameObject.GetComponent<PlayerController>();
        canDialogue = true;
    }

    private void Update()
    {
        /*if(dialogText.text == dialogs[index])
        {
            continueButton.SetActive(true);
        }*/

        if (canDialogue && !dialoguing && Input.GetKeyDown(KeyCode.Space)) {
            nextLine.SetActive(true);
            StartCoroutine(PrintDialog());
            GetComponent<CapsuleCollider2D>().enabled = false;
            //playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.moveVector = Vector3.zero;
            playerController.playerAnim.SetBool("Dialog", true);
            playerController.enabled = false;
        }

        if (dialoguing && Input.GetKeyDown(KeyCode.Space)) {
            NextSentence();
        }
    }

    IEnumerator PrintDialog()
    {
        foreach (char letter in dialogs[index].ToCharArray())
        {
            dialogText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }
        dialoguing = true;
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
            dialoguing = false;
            canDialogue = false;
            nextLine.SetActive(false);
            StartCoroutine(EnablePlayerController());
            StartCoroutine(EnableCollider());
        }
    }
    IEnumerator EnablePlayerController()
    {
        yield return new WaitForSeconds(enablePlayerControllerTimer);
        playerController.enabled = true;
        playerController.playerAnim.SetBool("Dialog", false);
    }


    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(enableColliderTimer);
        GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
