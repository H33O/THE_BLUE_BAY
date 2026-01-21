using UnityEngine;

public class SimpleCollectible : MonoBehaviour
{
    public static int count = 0;
    public static int totalNeeded = 3;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            count++;
            Debug.Log($"Collecté ! Total : {count}/{totalNeeded}");

            Destroy(gameObject);

            if (count >= totalNeeded)
            {
                Debug.Log("🎯 Tous les objets collectés !");

                if (QuestManager.Instance != null)
                {
                    QuestManager.Instance.CompleteQuest();
                }
            }
        }
    }
}
