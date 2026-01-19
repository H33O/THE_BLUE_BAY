using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuizWidgetManager : MonoBehaviour
{
    [Header("Widget UI")]
    public GameObject widgetPanel;
    public Image widgetBackground;
    public TextMeshProUGUI questionText;

    [Header("Answer Feedback")]
    public TextMeshProUGUI greenText;
    public TextMeshProUGUI blueText;

    [Header("Texture")]
    public Sprite talkBoxTexture;

    [Header("Questions")]
    public List<string> questions = new List<string>();

    [Header("Feedback Messages")]
    public List<string> greenMessages = new List<string>();
    public List<string> blueMessages = new List<string>();

    private int currentQuestionIndex = 0;
    private bool isWidgetActive = false;
    private HashSet<GameObject> usedRings = new HashSet<GameObject>();

    private static QuizWidgetManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (widgetPanel != null)
        {
            widgetPanel.SetActive(false);
        }

        if (widgetBackground != null && talkBoxTexture != null)
        {
            widgetBackground.sprite = talkBoxTexture;
        }

        if (greenText != null)
        {
            greenText.text = "";
        }

        if (blueText != null)
        {
            blueText.text = "";
        }
    }

    public void ShowWidget()
    {
        if (!isWidgetActive && widgetPanel != null)
        {
            isWidgetActive = true;
            widgetPanel.SetActive(true);
            currentQuestionIndex = 0;
            DisplayCurrentQuestion();
            Debug.Log("📝 Widget de quiz affiché");
        }
    }

    public void HideWidget()
    {
        if (isWidgetActive && widgetPanel != null)
        {
            isWidgetActive = false;
            widgetPanel.SetActive(false);
            usedRings.Clear();
            Debug.Log("✅ Widget de quiz masqué");
        }
    }

    public void OnRingPassed(GameObject ring, bool isCorrect, float timeValue)
    {
        if (!isWidgetActive) return;

        if (usedRings.Contains(ring))
        {
            return;
        }

        usedRings.Add(ring);

        RaceTimer timer = FindFirstObjectByType<RaceTimer>();
        if (timer != null)
        {
            if (isCorrect)
            {
                timer.RemoveTime(timeValue);
                Debug.Log($"✓ Bonne réponse ! -{timeValue}s");
            }
            else
            {
                timer.AddTimePenalty(timeValue);
                Debug.Log($"✗ Mauvaise réponse ! +{timeValue}s");
            }
        }

        UpdateFeedbackTexts();

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            DisplayCurrentQuestion();
        }
    }

    private void DisplayCurrentQuestion()
    {
        if (questionText != null && currentQuestionIndex < questions.Count)
        {
            questionText.text = questions[currentQuestionIndex];
        }

        UpdateFeedbackTexts();
    }

    private void UpdateFeedbackTexts()
    {
        if (greenText != null && currentQuestionIndex < greenMessages.Count)
        {
            greenText.text = greenMessages[currentQuestionIndex];
        }

        if (blueText != null && currentQuestionIndex < blueMessages.Count)
        {
            blueText.text = blueMessages[currentQuestionIndex];
        }
    }

    public static QuizWidgetManager Instance => instance;
}
