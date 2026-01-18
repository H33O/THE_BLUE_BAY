using UnityEngine;
using TMPro;

public class QuizGate : MonoBehaviour
{
    [Header("Configuration")]
    public string answerText;
    public bool isCorrectAnswer = false;

    [Header("Récompenses/Pénalités")]
    public float speedBoostMultiplier = 1.5f;
    public float boostDuration = 3f;
    public float timePenalty = 5f;

    [Header("UI (optionnel)")]
    public TextMeshPro labelText;

    private bool hasBeenUsed = false;
    private QuizZoneTrigger parentQuiz;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        if (labelText != null)
        {
            labelText.text = answerText;
        }

        parentQuiz = GetComponentInParent<QuizZoneTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenUsed || parentQuiz == null) return;

        if (other.CompareTag("Player"))
        {
            hasBeenUsed = true;
            parentQuiz.OnAnswerSelected(this, other.gameObject);
        }
    }
}
