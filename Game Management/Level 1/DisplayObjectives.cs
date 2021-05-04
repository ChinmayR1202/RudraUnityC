using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayObjectives : MonoBehaviour
{
    private Queue<string> objectives;
    public ObjectiveManager readObjective;
    public Text disObjective;

    // Start is called before the first frame update
    void Start()
    {
        objectives = new Queue<string>();
        StartReading(readObjective);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            DisplayNextSentence();
        }

    }


    public void StartReading(ObjectiveManager objectiveManager)
    {

        objectives.Clear();

        foreach (string sentence in objectiveManager.sentences)
        {
            objectives.Enqueue(sentence);
        }

        DisplayNextSentence();

    }


    public void DisplayNextSentence()
    {
        if (objectives.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = objectives.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        disObjective.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            disObjective.text += letter;
            yield return null;
        }
    }


    void EndDialogue()
    {

    }
}
