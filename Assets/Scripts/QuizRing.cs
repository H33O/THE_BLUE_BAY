using UnityEngine;

public class QuizRing : MonoBehaviour
{
    [Header("Configuration")]
    public bool isCorrectAnswer = false;

    [Header("Temps (secondes)")]
    public float timeValue = 5f;

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

            QuizWidgetManager manager = QuizWidgetManager.Instance;
            if (manager != null)
            {
                manager.OnRingPassed(gameObject, isCorrectAnswer, timeValue);
            }
        }
    }
}
