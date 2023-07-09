using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEndStarter : MonoBehaviour
{
    [SerializeField] public UnityEvent onEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ControlManager player = collision.GetComponent<ControlManager>();
            if (player.activeController.NPCName == "Tilki")
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        onEnter?.Invoke();
    }
}
