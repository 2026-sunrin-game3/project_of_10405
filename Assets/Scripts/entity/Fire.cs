
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fire : Enemy
{
    [SerializeField] AttackRange defaultAttack;
    protected override void MobUpdate()
    {
        Attack(0.2f, defaultAttack, transform.position);
        health.GetDamage(Time.deltaTime + 100);
    }
    protected override void DrawGizmos()
    {
        Draw(defaultAttack);
    }
}