using UnityEngine;
using TMPro;
using System.Collections;

public class QuestDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue")]
    [TextArea(3, 6)]
    public string dialogueText = "Bienvenue au phare ! Collectez 3 objets pour compléter la quête.";
    public float dialogueDuration = 4f;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueTextUI;

    [Header("Objets à Activer après le Dialogue")]
    public GameObject[] collectiblesToActivate;
    public GameObject collectionUI;

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
            StartCoroutine(ShowDialogueAndStartQuest());
        }
    }

    private IEnumerator ShowDialogueAndStartQuest()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }

        if (dialogueTextUI != null)
        {
            dialogueTextUI.text = dialogueText;
        }

        Debug.Log($"📜 Dialogue : {dialogueText}");

        yield return new WaitForSeconds(dialogueDuration);

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        ActivateQuest();
    }

    private void ActivateQuest()
    {
        Debug.Log("✓ Quête activée ! Les collectibles apparaissent.");

        foreach (GameObject collectible in collectiblesToActivate)
        {
            if (collectible != null)
            {
                collectible.SetActive(true);
            }
        }

        if (collectionUI != null)
        {
            collectionUI.SetActive(true);
        }

        SimpleCollectible.count = 0;
    }
}
