using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonolithScriptManager : MonoBehaviour
{
    private Queue<string> sentences;

    public Text script;
    public Text continueText;
    public Button continueButton;

    public bool startedReading;

    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private Image dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        script.enabled = false;
        dialogueBox.enabled = false;
        continueText.enabled = false;
        continueButton.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && startedReading)
        {
            DisplayNextSentence();
        }

    }


    public void StartReading(ReadMonolith readMonolith)
    {
        startedReading = true;
        dialogueBox.enabled = true;
        playerMove.isMovementLocked = true;
        script.enabled = true;
        continueText.enabled = true;
        continueButton.enabled = true;
        sentences.Clear();

        foreach( string sentence in readMonolith.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

  
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
            
    }

    IEnumerator TypeSentence(string sentence)
    {
        script.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            script.text += letter;
            yield return null;
        }
    }


    void EndDialogue()
    {
        startedReading = false;
        playerMove.isMovementLocked = false;
        dialogueBox.enabled = false;
        script.enabled = false;
        continueButton.enabled = false;
        continueText.enabled = false;
    }

}
