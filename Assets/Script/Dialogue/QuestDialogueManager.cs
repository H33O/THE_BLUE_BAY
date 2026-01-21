using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class QuestDialogueManager : MonoBehaviour
{
    [Header("Dialogues Automatiques")]
    [TextArea(2, 4)]
    public List<string> questDialogues = new List<string>
    {
        "Cherchez bien autour de vous...",
        "Les piles doivent être dans les environs.",
        "Elles brillent légèrement dans l'obscurité.",
        "N'abandonnez pas !",
        "Vous vous rapprochez du but.",
        "Encore quelques efforts !",
        "Le phare compte sur vous !"
    };

    [Header("Configuration")]
    public float timeBetweenDialogues = 5f;
    public float dialogueDisplayDuration = 3f;

    [Header("UI Références")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private bool isQuestActive = false;
    private int currentDialogueIndex = 0;

    public void StartQuestDialogues()
    {
        if (!isQuestActive)
        {
            isQuestActive = true;
            currentDialogueIndex = 0;
            StartCoroutine(ShowQuestDialoguesSequence());
            Debug.Log("🎯 Dialogues de quête démarrés");
        }
    }

    public void StopQuestDialogues()
    {
        isQuestActive = false;
        StopAllCoroutines();

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        Debug.Log("🛑 Dialogues de quête arrêtés");
    }

    private IEnumerator ShowQuestDialoguesSequence()
    {
        while (isQuestActive && currentDialogueIndex < questDialogues.Count)
        {
            yield return new WaitForSeconds(timeBetweenDialogues);

            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(true);
            }

            if (dialogueText != null)
            {
                dialogueText.text = questDialogues[currentDialogueIndex];
            }

            Debug.Log($"💬 Dialogue {currentDialogueIndex + 1}/{questDialogues.Count}: {questDialogues[currentDialogueIndex]}");

            yield return new WaitForSeconds(dialogueDisplayDuration);

            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }

            currentDialogueIndex++;
        }

        isQuestActive = false;
        Debug.Log("✓ Tous les dialogues de quête affichés");
    }
}
