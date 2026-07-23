using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Binah : Enemy
{
    public PlayerBattle pl;
    BoxCollider2D coll;
    [SerializeField] Slider bossbar;
    public GameObject ex;
    public GameObject mi;

    [SerializeField] AttackRange attack1_1;
    [SerializeField] AttackRange attack1_2;
    [SerializeField] AttackRange attack1_3;
    [SerializeField] AttackRange attack2_1;


    protected override void MobStart()
    {
        health.OnDamage(OnHurt);
        coll = GetComponent<BoxCollider2D>();
        StartCoroutine(Loop());
    }
    IEnumerator Loop()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.5f);   
            yield return StartCoroutine(Pattern1());
            if(health.health <= 800)
            {
                break;
            }
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(Pattern2());
            if(health.health <= 800)
            {
                break;
            }
        }
        while(true)
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(Pattern3());
            if(health.health <= 500)
            {
                De();
                break;
            }
            yield return new WaitForSeconds(0.5f);   
            yield return StartCoroutine(Pattern1());
            if(health.health <= 500)
            {
                De();
                break;
            }
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(Pattern2());
            if(health.health <= 500)
            {
                De();
                break;
            }
        }
        DataManager.savedPlayerHealth = plhealth.health;
        DataManager.savedBossHealth = health.health;

        DataManager.atkCool = pl.atkCool;
        DataManager.dashCool = pl.dashCool;
        DataManager.skill1Cool = pl.skill1Cool;
        DataManager.skill2Cool = pl.skill2Cool;
        DataManager.skill3Cool = pl.skill3Cool;

        yield return StartCoroutine(Back());
        SceneManager.LoadScene("Phase2");
    }
    protected override void MobUpdate()
    {
        bossbar.value = health.health / health.maxHealth;
    }
    void OnHurt(EntityHealth.Context ctx)
    {
        indicator.IndicateDamage(ctx.damage, transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 1) + new Vector3(-6,0,0), Color.orange);
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
    protected override void DrawGizmos()
    {
        Draw(attack1_1);
        Draw(attack1_2);
        Draw(attack1_3);
        Draw(attack2_1);
    }
    IEnumerator Pattern1()
    {
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack1_1.offset,attack1_1.size,2f));
        yield return new WaitForSeconds(0.5f);
        if(health.health <= 500)
        {
            De();
        }
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack1_2.offset,attack1_2.size,2f));
        yield return new WaitForSeconds(0.5f);
        if(health.health <= 500)
        {
            De();
        }
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack1_3.offset,attack1_3.size,2f));
        yield return new WaitForSeconds(1f);
        if(health.health <= 500)
        {
            De();
        }
        Instantiate(ex,(Vector2)transform.position + attack1_1.offset + new Vector2(0,-2),Quaternion.identity);
        Attack(0f,attack1_1,transform.position,1.5f);
        yield return new WaitForSeconds(0.5f);
        if(health.health <= 500)
        {
            De();
        }
        Instantiate(ex,(Vector2)transform.position + attack1_2.offset + new Vector2(0,-2),Quaternion.identity);
        Attack(0f,attack1_2,transform.position,1.5f);
        yield return new WaitForSeconds(0.5f);
        if(health.health <= 500)
        {
            De();
        }
        Instantiate(ex,(Vector2)transform.position + attack1_3.offset + new Vector2(0,-2),Quaternion.identity);
        Attack(0f,attack1_3,transform.position,1.5f);
    }
    IEnumerator Pattern2()
    {
        StartCoroutine(attackindicator.ShowWarningSimple((Vector2)transform.position + attack2_1.offset,attack2_1.size,2f));
        yield return new WaitForSeconds(2f);
        if(health.health <= 500)
        {
            De();
        }
        Attack(0f,attack2_1,transform.position,3.5f);
        coll.isTrigger = true;
        transform.position = new Vector2(transform.position.x - 5f,transform.position.y);
        yield return new WaitForSeconds(0.2f);
        if(health.health <= 500)
        {
            De();
        }
        transform.position = new Vector2(transform.position.x + 5f,1.38f);
        coll.isTrigger = false;
    }
    IEnumerator Pattern3()
    {
        float time = 2f;
        while(time > 0)
        {
            StartCoroutine(attackindicator.ShowWarningSimple(new Vector2(player.transform.position.x,-1.5f),new Vector2(3f,1.75f),Time.deltaTime));
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            if(health.health <= 500)
            {
                De();
            }
        }
        float save = player.transform.position.x;
        StartCoroutine(attackindicator.ShowWarningSimple(new Vector2(save,-2.125f),new Vector2(3f,1.75f),1f));
        yield return new WaitForSeconds(1f);
        if(health.health <= 500)
        {
            De();
        }
        Instantiate(mi,new Vector2(save,5),Quaternion.identity);
    }
    IEnumerator Back()
    {
        plhealth.isDeath = true;
        health.isDeath = true;

        Vector2 savePos = player.transform.position; 
        
        float time = 2f;
        
        while(time > 0)
        {
            player.transform.position = savePos; 
            Move(new Vector2(1, 0));
            
            time -= Time.deltaTime;

            yield return null; 
        }
    }
    protected override void OnDeath(EntityHealth.Context ctx)
    {
        SceneManager.LoadScene("Win");
    }
}