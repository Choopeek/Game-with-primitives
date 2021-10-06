using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;

    private Queue<string> sentences;
    [SerializeField] Level2PlayerController playerControls;

    [SerializeField] Animator animator;
    [SerializeField] float textSpeedAnimation = 0.1f;

    [SerializeField] Level2GameManager gameManager;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsTalking", true);
        playerControls.inDialogue = true;
        gameManager.StartExitDialogueState("start");
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
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
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeedAnimation);
        }
    }

    public void EndDialogue()
    {
        gameManager.StartExitDialogueState("exit");
        playerControls.inDialogue = false;
        animator.SetBool("IsTalking", false);
        
    }
}
