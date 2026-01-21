using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MultiDialogueQuestTrigger : MonoBehaviour
{
    [Header("Dialogues Successifs (en bas)")]
    [TextArea(2, 4)]
    public List<string> dialogues = new List<string>
    {
        "Bonjour voyageur ! J'ai besoin de votre aide.",
        "Des objets précieux ont été perdus dans la région.",
        "Pouvez-vous les retrouver pour moi ?"
    };
    public float dialogueDuration = 3f;

    [Header("UI - Dialogue en Bas")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("UI - Widget de Quête en Haut à Gauche")]
    public GameObject questWidget;
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;
    public string questTitle = "Collecte d'Objets";
    [TextArea(2, 3)]
    public string questDescription = "Collectez 3 objets précieux";

    [Header("Objets à Activer")]
    public GameObject[] collectiblesToActivate;
    public GameObject collectionUI;

    [Header("Gestionnaire de Dialogues")]
    public QuestDialogueManager questDialogueManager;

    private bool hasTriggered = false;

    private void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if (questWidget != null)
        {
            questWidget.SetActive(false);
        }

        foreach (GameObject collectible in collectiblesToActivate)
        {
            if (collectible != null)
            {
                collectible.SetActive(false);
            }
        }

        if (collectionUI != null)
        {
            collectionUI.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(ShowDialoguesSequence());
        }
    }

    private IEnumerator ShowDialoguesSequence()
    {
        foreach (string dialogue in dialogues)
        {
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(true);
            }

            if (dialogueText != null)
            {
                dialogueText.text = dialogue;
            }

            Debug.Log($"💬 Dialogue : {dialogue}");

            yield return new WaitForSeconds(dialogueDuration);
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);

        ShowQuestWidget();
        ShowCollectionUI();
        ActivateCollectibles();

        if (questDialogueManager != null)
        {
            questDialogueManager.StartQuestDialogues();
        }
    }

    private void ShowQuestWidget()
    {
        if (questWidget != null)
        {
            questWidget.SetActive(true);
        }

        if (questTitleText != null)
        {
            questTitleText.text = questTitle;
        }

        if (questDescriptionText != null)
        {
            questDescriptionText.text = questDescription;
        }

        Debug.Log($"📋 Quête activée : {questTitle}");
    }

    private void ActivateCollectibles()
    {
        foreach (GameObject collectible in collectiblesToActivate)
        {
            if (collectible != null)
            {
                collectible.SetActive(true);
            }
        }

        SimpleCollectible.count = 0;

        Debug.Log($"✓ {collectiblesToActivate.Length} collectibles activés !");
    }

    private void ShowCollectionUI()
    {
        if (collectionUI != null)
        {
            collectionUI.SetActive(true);
            Debug.Log("📊 Compteur de collection affiché : 0/3");
        }
    }
}

