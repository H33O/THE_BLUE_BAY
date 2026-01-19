using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class QuizZoneTrigger : MonoBehaviour
{
    [Header("Question")]
    [TextArea(2, 4)]
    public string questionText = "Choisissez la bonne couleur !";

    [Header("UI")]
    public GameObject questionPanel;
    public TextMeshProUGUI questionDisplayText;
    public TextMeshProUGUI colorsDisplayText;

    [Header("Mode de Réponse")]
    public AnswerMode answerMode = AnswerMode.Manual;

    private bool isActive = false;
    private bool hasBeenAnswered = false;
    private List<QuizGate> gates = new List<QuizGate>();

    public enum AnswerMode
    {
        Manual,
        Random
    }

    private void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }

        if (questionPanel != null)
        {
            questionPanel.SetActive(false);
        }

        gates = GetComponentsInChildren<QuizGate>().ToList();

        if (answerMode == AnswerMode.Random && gates.Count > 0)
        {
            RandomizeCorrectAnswer();
        }
    }

    private void RandomizeCorrectAnswer()
    {
        foreach (var gate in gates)
        {
            gate.SetCorrectAnswer(false);
        }

        int randomIndex = Random.Range(0, gates.Count);
        gates[randomIndex].SetCorrectAnswer(true);

        Debug.Log($"🎲 Bonne réponse aléatoire : {gates[randomIndex].ringColor}");
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

        if (colorsDisplayText != null && gates.Count > 0)
        {
            string colors = string.Join(" ou ", gates.Select(g => g.ringColor));
            colorsDisplayText.text = colors + " ?";
        }
    }

    public void OnAnswerSelected(QuizGate gate)
    {
        if (hasBeenAnswered) return;

        hasBeenAnswered = true;

        if (gate.isCorrectAnswer)
        {
            Debug.Log($"✓ Bonne réponse ({gate.ringColor}) ! -{gate.timeBonus} secondes !");
            ApplyTimeBonus(gate.timeBonus);
        }
        else
        {
            Debug.Log($"✗ Mauvaise réponse ({gate.ringColor}) ! +{gate.timePenalty} secondes");
            ApplyTimePenalty(gate.timePenalty);
        }

        StartCoroutine(HideQuestionDelayed());
    }

    private void ApplyTimeBonus(float bonus)
    {
        RaceTimer timer = FindFirstObjectByType<RaceTimer>();
        if (timer != null)
        {
            timer.RemoveTime(bonus);
        }
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

