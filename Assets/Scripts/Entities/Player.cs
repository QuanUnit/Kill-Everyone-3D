using UnityEngine;
using UnityEngine.AI;

public class Player : Entity
{
    public PlayerState State { get; set; } = PlayerState.Idle;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Gun gun;

    public void MoveTo(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    public void Shot(Vector3 target)
    {
        gun.Shot(target);
    }

    private void OnAnimatorMove()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public enum PlayerState
    {
        Idle,
        Fight,
        Dislocation,
    }
}
