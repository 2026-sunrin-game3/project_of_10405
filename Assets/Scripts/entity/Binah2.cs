using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Binah2 : Enemy
{
    BoxCollider2D coll;
    [SerializeField] Slider bossbar;
    public GameObject ex;
    public GameObject mi;
    private bool is_ = true;

    [SerializeField] AttackRange attack1_1;
    [SerializeField] AttackRange attack1_2;
    [SerializeField] AttackRange attack1_3;
    [SerializeField] AttackRange attack2_1;
    [SerializeField] AttackRange attack4_1;


    protected override void MobStart()
    {
        health.OnDamage(OnHurt);
        health.OnDamage(is_cut);
        coll = GetComponent<BoxCollider2D>();
        StartCoroutine(Loop());
    }
    IEnumerator Loop()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Pattern3());
            yield return new WaitForSeconds(1.75f);
            StartCoroutine(Pattern2());
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(Pattern1());
            yield return new WaitForSeconds(1.75f);
            yield return StartCoroutine(Pattern2());
            if(health.health <= 150)
            {
                break;
            }
            yield return new WaitForSeconds(0.5f);
            if(health.health <= 150)
            {
                break;
            }
            yield return StartCoroutine(Pattern1_1());
            if(health.health <= 150)
            {
                break;
            }
            yield return new WaitForSeconds(1.5f);
            if(health.health <= 150)
            {
                break;
            }
        }
        yield return StartCoroutine(Pattern4());
        stat.Calc("hurtDamage");
        yield return new WaitForSeconds(1f);
        while(true)
        {
            if(health.health <= 0)
            {
                break;
            }
            StartCoroutine(Pattern3());
            yield return new WaitForSeconds(0.2f);
        }
    }
    void De()
    {
        var hurtdamageBuf = new EntityStat.Buf
        {
            Key = "hurtDamage",
            mathType = MathType.Remove,
            Value = 100
        };

        stat.bufs.Add(hurtdamageBuf);
        stat.Calc("hurtDamage");
        stat.bufs.Remove(hurtdamageBuf);
    }
    void is_cut(EntityHealth.Context ctx)
    {
        if(health.health <= 150)
            {
                if(is_)
                {
                    De();
                }
                is_ = false;
            }
    }
    protected override void MobUpdate()
    {
        bossbar.value = health.health / health.maxHealth;
    }
    void OnHurt(EntityHealth.Context ctx)
    {
        indicator.IndicateDamage(ctx.damage, transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 1) + new Vector3(-6,0,0), Color.orange);
    }
    protected override void DrawGizmos()
    {
        Draw(attack1_1);
        Draw(attack1_2);
        Draw(attack1_3);
        Draw(attack2_1);
    }
    IEnumerator Pattern1()
    {
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack1_1.offset,attack1_1.size,1.1f));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack1_2.offset,attack1_2.size,1.1f));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack1_3.offset + new Vector2(5,0),attack1_3.size,1.1f));
        yield return new WaitForSeconds(0.5f);
        Instantiate(ex,(Vector2)transform.position + attack1_1.offset + new Vector2(0,-2),Quaternion.identity);
        Attack(0f,attack1_1,transform.position,2f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(ex,(Vector2)transform.position + attack1_2.offset + new Vector2(0,-2),Quaternion.identity);
        Attack(0f,attack1_2,transform.position,2f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(ex,(Vector2)transform.position + attack1_3.offset + new Vector2(0,-2),Quaternion.identity);
        Attack(0f,attack1_3,transform.position,2f);
    }
    IEnumerator Pattern1_1()
    {
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack1_1.offset,attack1_1.size,1.1f));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack1_2.offset,attack1_2.size,1.1f));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack1_3.offset,attack1_3.size,1.1f));
        yield return new WaitForSeconds(0.5f);
        Instantiate(ex,(Vector2)transform.position + attack1_1.offset + new Vector2(0,-2),Quaternion.identity);
        Attack(0f,attack1_1,transform.position,2f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(ex,(Vector2)transform.position + attack1_2.offset + new Vector2(0,-2),Quaternion.identity);
        Attack(0f,attack1_2,transform.position,2f);
        yield return new WaitForSeconds(0.3f);
        Instantiate(ex,(Vector2)transform.position + attack1_3.offset + new Vector2(0,-2),Quaternion.identity);
        Attack(0f,attack1_3,transform.position,2f);
    }
    IEnumerator Pattern2()
    {
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack2_1.offset,attack2_1.size,0.5f));
        yield return new WaitForSeconds(0.75f);
        Attack(0f,attack2_1,transform.position,5f);
        coll.isTrigger = true;
        transform.position = new Vector2(transform.position.x - 5f,transform.position.y);
        yield return new WaitForSeconds(0.2f);
        transform.position = new Vector2(transform.position.x + 5f,1.38f);
        coll.isTrigger = false;
    }
    IEnumerator Pattern3()
    {
        float time = 0.5f;
        while(time > 0)
        {
            StartCoroutine(attackindicator.ShowWarningSimple(new Vector2(player.transform.position.x,-1.5f),new Vector2(3f,1.75f),Time.deltaTime));
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        float save1 = player.transform.position.x;
        StartCoroutine(attackindicator.ShowWarningSimple(new Vector2(save1,-2.125f),new Vector2(3f,1.75f),0.5f));
        time = 0.5f;
        while(time > 0)
        {
            StartCoroutine(attackindicator.ShowWarningSimple(new Vector2(player.transform.position.x,-1.5f),new Vector2(3f,1.75f),Time.deltaTime));
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Instantiate(mi,new Vector2(save1,5),Quaternion.identity);
        float save2 = player.transform.position.x;
        StartCoroutine(attackindicator.ShowWarningSimple(new Vector2(save2,-2.125f),new Vector2(3f,1.75f),0.5f));
        time = 0.5f;
        while(time > 0)
        {
            StartCoroutine(attackindicator.ShowWarningSimple(new Vector2(player.transform.position.x,-1.5f),new Vector2(3f,1.75f),Time.deltaTime));
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Instantiate(mi,new Vector2(save2,5),Quaternion.identity);
        float save3 = player.transform.position.x;
        StartCoroutine(attackindicator.ShowWarningSimple(new Vector2(save3,-2.125f),new Vector2(3f,1.75f),0.75f));
        yield return new WaitForSeconds(0.5f);
        Instantiate(mi,new Vector2(save3,5),Quaternion.identity);
    }
    IEnumerator Pattern4()
    {
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack4_1.offset,attack4_1.size,2f));
        yield return new WaitForSeconds(2f);
        Attack(0f,attack2_1,transform.position,1000f);
        transform.position = new Vector2(transform.position.x - 8f,transform.position.y);
        yield return new WaitForSeconds(0.2f);
    }
    protected override void OnDeath(EntityHealth.Context ctx)
    {
        SceneManager.LoadScene("Win");
    }
}