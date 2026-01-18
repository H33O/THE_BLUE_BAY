using UnityEngine;
using TMPro;
using System.Collections;

public class QuizZoneTrigger : MonoBehaviour
{
    [Header("Question")]
    [TextArea(2, 4)]
    public string questionText;

    [Header("UI")]
    public GameObject questionPanel;
    public TextMeshProUGUI questionDisplayText;

    private bool isActive = false;
    private bool hasBeenAnswered = false;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        if (questionPanel != null)
        {
            questionPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive && !hasBeenAnswered && other.CompareTag("Player"))
        {
            ShowQuestion();
        }
    }

    private void ShowQuestion()
    {
        isActive = true;

        if (questionPanel != null)
        {
            questionPanel.SetActive(true);
        }

        if (questionDisplayText != null)
        {
            questionDisplayText.text = questionText;
        }
    }

    public void OnAnswerSelected(QuizGate gate, GameObject player)
    {
        if (hasBeenAnswered) return;

        hasBeenAnswered = true;

        if (gate.isCorrectAnswer)
        {
            Debug.Log("✓ Bonne réponse ! Boost de vitesse !");
            ApplySpeedBoost(player, gate.speedBoostMultiplier, gate.boostDuration);
        }
        else
        {
            Debug.Log($"✗ Mauvaise réponse ! +{gate.timePenalty} secondes de pénalité");
            ApplyTimePenalty(gate.timePenalty);
        }

        StartCoroutine(HideQuestionDelayed());
    }

    private void ApplySpeedBoost(GameObject player, float multiplier, float duration)
    {
        SpeedBoostController boost = player.GetComponent<SpeedBoostController>();
        if (boost == null)
        {
            boost = player.AddComponent<SpeedBoostController>();
        }
        boost.ApplyBoost(multiplier, duration);
    }

    private void ApplyTimePenalty(float penalty)
    {
        RaceTimer timer = FindFirstObjectByType<RaceTimer>();
        if (timer != null)
        {
            timer.AddTimePenalty(penalty);
        }
    }

    private IEnumerator HideQuestionDelayed()
    {
        yield return new WaitForSeconds(2f);

        if (questionPanel != null)
        {
            questionPanel.SetActive(false);
        }
    }
}
