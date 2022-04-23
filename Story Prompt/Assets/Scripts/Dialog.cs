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

    PlayerController playerController;
    public float enablePlayerControllerTimer = 0.5f;
    public float enableColliderTimer = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(PrintDialog());
            GetComponent<CapsuleCollider2D>().enabled = false;
            playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.moveVector = Vector3.zero;
            playerController.projectileMoveVector = Vector3.zero;
            playerController.playerAnim.SetBool("Dialog", true);
            playerController.enabled = false;
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
