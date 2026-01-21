using UnityEngine;
using TMPro;
using System.Collections;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Respawn")]
    public Transform respawnPoint;
    public GameObject player;

    [Header("UI Dialogue")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("UI Quête")]
    public GameObject questWidget;
    public GameObject collectionUI;

    [Header("Dialogues de Fin")]
    [TextArea(2, 4)]
    public string completionMessage = "Merci infiniment ! Grâce à vous, mon phare brillera à nouveau !";
    public float completionDialogueDuration = 4f;

    [Header("Transition Scène")]
    public string nextSceneName = "Quiz course";
    public float delayBeforeTransition = 1f;

    [Header("Gestionnaire de Dialogues")]
    public QuestDialogueManager questDialogueManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteQuest()
    {
        StartCoroutine(QuestCompletionSequence());
    }

    private IEnumerator QuestCompletionSequence()
    {
        Debug.Log("🎉 Quête terminée ! Téléportation au phare...");

        if (questDialogueManager != null)
        {
            questDialogueManager.StopQuestDialogues();
        }

        if (questWidget != null)
        {
            questWidget.SetActive(false);
        }

        if (collectionUI != null)
        {
            collectionUI.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);

        if (player != null && respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation;

            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            Debug.Log("✓ Joueur téléporté au phare");
        }

        yield return new WaitForSeconds(1f);

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }

        if (dialogueText != null)
        {
            dialogueText.text = completionMessage;
        }

        Debug.Log($"💬 {completionMessage}");

        yield return new WaitForSeconds(completionDialogueDuration);

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        yield return new WaitForSeconds(delayBeforeTransition);

        Debug.Log("🎬 Transition vers la scène suivante...");

        if (SceneTransition.Instance != null)
        {
            SceneTransition.Instance.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("❌ SceneTransition.Instance introuvable !");
        }
    }
}
