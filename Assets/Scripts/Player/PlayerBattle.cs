using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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
    public float atkCool, dashCool, skill1Cool, skill2Cool, skill3Cool;
    [SerializeField] DamageIndicator indicator;

    
    public PlayerAnimator animator;
    public AttackRange defaultAttack;
    public AttackRange superAttack;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float dashPower, dashTime;
    public bool inDash;
    [SerializeField] Slider healthbar;
    public GameObject bull;
    public GameObject ex;
    public GameObject sh;
    void Start()
    {
        health = GetComponent<EntityHealth>();
        stat = GetComponent<EntityStat>();
        animator = GetComponent<PlayerAnimator>();
        movement = GetComponent<PlayerMovement>();

        health.OnDamage(OnHurt);
        health.OnDeath(OnDeath);
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
        healthbar.value = health.health / health.maxHealth;
        if (atkCool > 0)
            atkCool -= Time.deltaTime * (1 + stat.GetResultValue("atkSpeed") / 100) * 2f;
        if (dashCool > 0)
            dashCool -= Time.deltaTime * 2.5f;
        if (skill1Cool > 0)
            skill1Cool -= Time.deltaTime;
        if (skill2Cool > 0)
            skill2Cool -= Time.deltaTime;     
        if (skill3Cool > 0)
            skill3Cool -= Time.deltaTime;     
        defaultAttack.offset.x = animator.direction * 2;
        superAttack.offset.x = animator.direction;
        
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
        GameObject b = Instantiate(bull, transform.position + new Vector3(0,0.65f,0), Quaternion.identity);
        Bullet bc = b.GetComponent<Bullet>();
        bc.x = animator.direction;
        GameObject b1 = Instantiate(bull, transform.position + new Vector3(0,0.8f,0), Quaternion.identity);
        Bullet b1c = b1.GetComponent<Bullet>();
        b1c.y = 1.25f;
        b1c.x = animator.direction;
        GameObject b2 = Instantiate(bull, transform.position + new Vector3(0,0.5f,0), Quaternion.identity);
        Bullet b2c = b2.GetComponent<Bullet>();
        b2c.y = -1.25f;
        b2c.x = animator.direction;
        GameObject b3 = Instantiate(bull, transform.position + new Vector3(0,0.95f,0), Quaternion.identity);
        Bullet b3c = b3.GetComponent<Bullet>();
        b3c.y = 2.5f;
        b3c.x = animator.direction;
        GameObject b4 = Instantiate(bull, transform.position + new Vector3(0,0.35f,0), Quaternion.identity);
        Bullet b4c = b4.GetComponent<Bullet>();
        b4c.y = -2.5f;
        b4c.x = animator.direction;
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
        skill1Cool = 6f;
        movement.SetVelocity(Vector2.up * 8f);
    }

    public void Skill2()
    {
        if (skill2Cool > 0)
            return;
        skill2Cool = 12f;
        StartCoroutine(skill2_());
    }

    IEnumerator skill2_()
    {
        var hurtdamageBuf = new EntityStat.Buf
        {
            Key = "hurtDamage",
            mathType = MathType.Remove,
            Value = 50
        };

        var speedBuf = new EntityStat.Buf
        {
            Key = "moveSpeed",
            mathType = MathType.Decrease,
            Value = 50
        };

        sh.SetActive(true);
        stat.bufs.Add(hurtdamageBuf);
        stat.Calc("hurtDamage");
        stat.bufs.Add(speedBuf);
        stat.Calc("moveSpeed");

        yield return new WaitForSeconds(3);

        sh.SetActive(false);
        stat.bufs.Remove(hurtdamageBuf);
        stat.Calc("hurtDamage");
        stat.bufs.Remove(speedBuf);
        stat.Calc("moveSpeed");       
    }

    public void Skill3()
    {
        if (skill3Cool > 0)
            return;
        skill3Cool = 6f;
        animator.Play("Attack1");
        StartCoroutine(Skill3_());
    }
    IEnumerator Skill3_()
    {
        animator.Play("Attack1");
        GameObject e1 = Instantiate(ex, transform.position + new Vector3(1,0.65f,0), Quaternion.identity);
        Explode e1c = e1.GetComponent<Explode>();
        e1c.si = 0.25f; e1c.time = 0.1f;
        var col = Physics2D.OverlapBoxAll((Vector2)transform.position + superAttack.offset, superAttack.size, 0, enemyMask);

        foreach (var target in col)
        {
            EntityHealth hp = target.GetComponent<EntityHealth>();
            if (hp != null)
            {
                hp.GetDamage(stat.GetResultValue("attackDamage") * 2f, health);
            }
        }
        movement.SetVelocity(new Vector2(animator.direction * -1,0) * 30f);
        yield return new WaitForSeconds(0.05f);
        movement.SetVelocity(Vector2.zero);
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
        Draw(superAttack);
    }
    void OnDeath(EntityHealth.Context ctx)
    {
        SceneManager.LoadScene("Die");
    }
}
