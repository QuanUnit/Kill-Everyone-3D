using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    protected override void Die()
    {
        animator.enabled = false;
        base.Die();
    }
}
