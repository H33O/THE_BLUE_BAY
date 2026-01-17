using UnityEngine;
using TMPro;

public class CollectionUI : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI counterText;

    [Header("Configuration")]
    public int totalToCollect = 3;

    private void Update()
    {
        if (counterText != null)
        {
            counterText.text = $"{SimpleCollectible.count}/{totalToCollect}";
        }
    }
}


