using UnityEngine;

public class StartLine : MonoBehaviour
{
    private bool hasStarted = false;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasStarted && other.CompareTag("Player"))
        {
            RaceTimer timer = FindFirstObjectByType<RaceTimer>();
            if (timer != null)
            {
                timer.StartTimer();
                hasStarted = true;
            }

            QuizWidgetManager quizManager = FindFirstObjectByType<QuizWidgetManager>();
            if (quizManager != null)
            {
                quizManager.ShowWidget();
            }
        }
    }
}
