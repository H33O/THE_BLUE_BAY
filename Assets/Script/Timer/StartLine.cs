using UnityEngine;

public class StartLine : MonoBehaviour
{
    // Variable pour s'assurer que le timer ne démarre qu'une seule fois
    private bool hasStarted = false;

    private void Start()
    {
        // Configure le collider en mode "trigger" pour détecter le passage
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le timer n'a pas déjà démarré ET si c'est le joueur qui entre
        if (!hasStarted && other.CompareTag("Player"))
        {
            // Trouve le RaceTimer dans la scène
            RaceTimer timer = FindFirstObjectByType<RaceTimer>();
            if (timer != null)
            {
                // Démarre le chronomètre
                timer.StartTimer();
                hasStarted = true;
                Debug.Log("Course démarrée !");
            }
        }
    }
}
