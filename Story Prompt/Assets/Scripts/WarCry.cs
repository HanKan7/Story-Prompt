using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarCry : MonoBehaviour
{

    public TextMeshProUGUI dialogText;
    public float typingSpeed = 0.05f;
    public List<string> dialogs = new List<string>();
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintDialog());
    }
    private void Update()
    {
        if(dialogText.text == dialogs[index])
        {
            //NextSentence();
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
        if (index < dialogs.Count - 1)
        {
            index++;
            dialogText.text = "";
            StartCoroutine(PrintDialog());
        }
        else
        {
            dialogText.text = "";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        index = 0;
        dialogText.text = "";
    }
}
