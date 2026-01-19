using UnityEngine;
using TMPro;

public class QuizGate : MonoBehaviour
{
    [Header("Configuration de l'Anneau")]
    public string ringColor = "Vert";
    public bool isCorrectAnswer = false;

    [Header("Récompenses/Pénalités")]
    public float timeBonus = 5f;
    public float timePenalty = 5f;

    private bool hasBeenUsed = false;
    private QuizZoneTrigger parentQuiz;

    private void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }

        parentQuiz = GetComponentInParent<QuizZoneTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenUsed || parentQuiz == null) return;

        if (other.CompareTag("Player"))
        {
            hasBeenUsed = true;
            parentQuiz.OnAnswerSelected(this);
        }
    }

    public void SetCorrectAnswer(bool correct)
    {
        isCorrectAnswer = correct;
    }
}

