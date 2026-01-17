using UnityEngine;

public class SimpleCollectible : MonoBehaviour
{
    public static int count = 0;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            count++;
            Debug.Log($"Collecté ! Total : {count}");
            Destroy(gameObject);
        }
    }
}
