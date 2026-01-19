using UnityEngine;

public class QuizGate : MonoBehaviour
{
    [Header("Configuration")]
    public bool isCorrectAnswer = false;

    [Header("R�compenses/P�nalit�s")]
    public float timeBonus = 5f;

    private bool hasBeenUsed = false;

    private void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenUsed) return;

        if (other.CompareTag("Player"))
        {
            hasBeenUsed = true;

            QuizWidgetManager manager = FindFirstObjectByType<QuizWidgetManager>();
            if (manager != null)
            {
                manager.OnRingPassed(gameObject, isCorrectAnswer, timeBonus);
            }
        }
    }

    public void SetCorrectAnswer(bool correct)
    {
        isCorrectAnswer = correct;
    }
}

