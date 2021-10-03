using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesCamp : MonoBehaviour
{
    public Transform WayPoint => wayPoint;
    public CampStatus Status => status;

    public UnityEvent OnCampCleared;

    [SerializeField] private Transform wayPoint;
    [SerializeField] private List<Enemy> campEnemies;

    private CampStatus status = CampStatus.notCleared;

    private void Awake()
    {
        if (campEnemies.Count == 0)
        {
            status = CampStatus.cleared;
            return;
        }

        foreach (var enemy in campEnemies)
        {
            enemy.OnDeath.AddListener(delegate
            {
                campEnemies.Remove(enemy);

                if (campEnemies.Count == 0)
                {
                    OnCampCleared?.Invoke();
                    status = CampStatus.cleared;
                }
            });
        }
    }

    public enum CampStatus
    {
        cleared,
        notCleared,
    }
}
