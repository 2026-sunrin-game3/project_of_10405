using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Missile : Enemy
{
    public Fire fire;
    [SerializeField] AttackRange defaultAttack;
    protected override void MobUpdate()
    {
        if(OnGround())
        {
            rigid.gravityScale = 0f;
            rigid.linearVelocity = new Vector2(0,0);
            Attack(0f, defaultAttack, transform.position);
            Instantiate(fire,new Vector2(transform.position.x,-3),quaternion.identity);
            Destroy(gameObject);
        }
    }
    protected override void DrawGizmos()
    {
        Draw(defaultAttack);
    }
}