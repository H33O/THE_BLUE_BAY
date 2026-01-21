using UnityEngine;
using TMPro;
using System.Collections;

public class RaceTimer : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timeFeedbackText;

    [Header("Configuration")]
    public bool startOnAwake = false;

    [Header("Couleurs (optionnel)")]
    public bool useColorCoding = false;
    public Color normalColor = Color.white;
    public Color penaltyColor = Color.red;
    public float colorFlashDuration = 1f;

    private float elapsedTime = 0f;
    private bool isRunning = false;
    private float colorFlashTimer = 0f;

    public bool IsRunning => isRunning;

    private void Start()
    {
        if (startOnAwake)
        {
            StartTimer();
        }
        else
        {
            UpdateDisplay();
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateDisplay();
        }

        if (useColorCoding && colorFlashTimer > 0f)
        {
            colorFlashTimer -= Time.deltaTime;

            if (colorFlashTimer <= 0f && timerText != null)
            {
                timerText.color = normalColor;
            }
        }
    }

    private void UpdateDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 100f) % 100f);

            timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
        }
    }

    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f;
        Debug.Log("⏱️ Timer démarré !");
    }

    public void StopTimer()
    {
        isRunning = false;
        Debug.Log($"⏱️ Timer arrêté ! Temps final : {GetFormattedTime()}");
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateDisplay();
    }

    public void AddTimePenalty(float seconds)
    {
        elapsedTime += seconds;
        Debug.Log($"⚠️ Pénalité de temps : +{seconds} secondes");

        if (useColorCoding && timerText != null)
        {
            timerText.color = penaltyColor;
            colorFlashTimer = colorFlashDuration;
        }

        if (timeFeedbackText != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowTimeFeedback($"+{seconds:F0}", new Color(1f, 0f, 0f, 1f)));
        }
    }

    public void RemoveTime(float seconds)
    {
        elapsedTime -= seconds;
        if (elapsedTime < 0f)
        {
            elapsedTime = 0f;
        }
        Debug.Log($"✓ Bonus de temps : -{seconds} secondes");

        if (timeFeedbackText != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowTimeFeedback($"-{seconds:F0}", new Color(0f, 1f, 0f, 1f)));
        }
    }

    public float GetTime()
    {
        return elapsedTime;
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100f) % 100f);

        return $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }

    public int GetMinutes()
    {
        return Mathf.FloorToInt(elapsedTime / 60f);
    }

    public int GetSeconds()
    {
        return Mathf.FloorToInt(elapsedTime % 60f);
    }

    public int GetMilliseconds()
    {
        return Mathf.FloorToInt((elapsedTime * 100f) % 100f);
    }

    private IEnumerator ShowTimeFeedback(string text, Color color)
    {
        timeFeedbackText.text = text;
        timeFeedbackText.color = color;

        float elapsed = 0f;
        float displayDuration = 1f;
        float fadeDuration = 0.5f;

        while (elapsed < displayDuration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            timeFeedbackText.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        timeFeedbackText.text = "";
        timeFeedbackText.color = new Color(color.r, color.g, color.b, 0f);
    }
}

