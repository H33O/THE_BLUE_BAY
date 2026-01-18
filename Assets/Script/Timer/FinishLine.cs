using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    [Header("UI")]
    public GameObject victoryPanel;
    public TextMeshProUGUI finalTimeText;

    private bool raceFinished = false;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!raceFinished && other.CompareTag("Player"))
        {
            raceFinished = true;

            RaceTimer timer = FindFirstObjectByType<RaceTimer>();
            if (timer != null)
            {
                timer.StopTimer();

                float finalTime = timer.GetTime();
                int minutes = Mathf.FloorToInt(finalTime / 60f);
                int seconds = Mathf.FloorToInt(finalTime % 60f);
                int milliseconds = Mathf.FloorToInt((finalTime * 100f) % 100f);

                string timeString = $"{minutes:00}:{seconds:00}:{milliseconds:00}";

                Debug.Log($"🏁 COURSE TERMINÉE ! Temps final : {timeString}");

                if (victoryPanel != null)
                {
                    victoryPanel.SetActive(true);

                    if (finalTimeText != null)
                    {
                        finalTimeText.text = $"Temps final :\n{timeString}";
                    }
                }
            }
        }
    }
}
