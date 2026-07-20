using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct AttackRange
{
    public Vector2 offset, size;
    public bool drawGizmos;
}
public class PlayerBattle : MonoBehaviour
{
    public EntityHealth health;
    public EntityStat stat;
    public PlayerMovement movement;
    public float atkCool, dashCool, skill1Cool;
    [SerializeField] DamageIndicator indicator;

    
    public PlayerAnimator animator;
    public AttackRange defaultAttack;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float dashPower, dashTime;
    public bool inDash;
    void Start()
    {
        health = GetComponent<EntityHealth>();
        stat = GetComponent<EntityStat>();
        animator = GetComponent<PlayerAnimator>();
        movement = GetComponent<PlayerMovement>();

        health.OnDamage(OnHurt);
    }

    void OnHurt(EntityHealth.Context ctx)
    {
        if(inDash)
        {
            ctx.canceled = true;
        }
        if(ctx.canceled)
        {
            return;
        }
        indicator.IndicateDamage(ctx.damage, transform.position + new Vector3(0,1), Color.skyBlue);
    }
    void Update()
    {
        if (atkCool > 0)
            atkCool -= Time.deltaTime * (1 + stat.GetResultValue("atkSpeed") / 100);
        if (dashCool > 0)
            dashCool -= Time.deltaTime * 2;
        if (skill1Cool > 0)
            skill1Cool -= Time.deltaTime;     
        defaultAttack.offset.x = animator.direction * 2;
        
    }
    public void Dash(int direction)
    {
        if (dashCool > 0)
            return;
        dashCool = 1f;
        StartCoroutine(dash_(direction));
    }
    IEnumerator dash_(int direction)
    {
        
        inDash = true;
        movement.SetVelocity(Vector2.right * direction * dashPower);

        yield return new WaitForSeconds(dashTime);

        movement.SetVelocity(Vector2.zero);
        inDash = false;
    }
    public void Attack()
    {
        if (atkCool > 0)
            return;
        atkCool = 1f;
        animator.Play("Attack1");
        var col = Physics2D.OverlapBoxAll((Vector2)transform.position + defaultAttack.offset, defaultAttack.size, 0, enemyMask);

        foreach (var target in col)
        {
            EntityHealth hp = target.GetComponent<EntityHealth>();
            if (hp != null)
            {
                hp.GetDamage(stat.GetResultValue("attackDamage"), health);
            }
        }
    }

    public void Skill1()
    {
        if (skill1Cool > 0)
            return;
        skill1Cool = 12f;
        StartCoroutine(skill1_());
    }

    IEnumerator skill1_()
    {
        var atkBuf = new EntityStat.Buf
        {
            Key = "attackDamage",
            mathType = MathType.Increase,
            Value = 50
        };
        var atkspeedBuf = new EntityStat.Buf
        {
            Key = "atkSpeed",
            mathType = MathType.Add,
            Value = 200
        };
        stat.bufs.Add(atkBuf);
        stat.bufs.Add(atkspeedBuf);
        stat.Calc("attackDamage");
        stat.Calc("atkSpeed");

        yield return new WaitForSeconds(5);

        stat.bufs.Remove(atkBuf);
        stat.bufs.Remove(atkspeedBuf);
        stat.Calc("attackDamage");
        stat.Calc("atkSpeed");

        var speedBuf = new EntityStat.Buf
        {
            Key = "moveSpeed",
            mathType = MathType.Decrease,
            Value = 50
        };

        stat.bufs.Add(speedBuf);
        stat.Calc("moveSpeed");

        yield return new WaitForSeconds(3);

        stat.bufs.Remove(speedBuf);
        stat.Calc("moveSpeed");       
    }
    
    void Draw(AttackRange range) 
    {
        if (!range.drawGizmos)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((Vector2)transform.position + range.offset, range.size);
    }

    void OnDrawGizmos() 
    {
        Draw(defaultAttack);
    }
}
