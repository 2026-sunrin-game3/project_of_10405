
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Binah : Enemy
{
    [SerializeField] Slider bossbar;

    protected override void MobStart()
    {
        StartCoroutine(attackindicator.ShowWarningSimple(new Vector3(0,0,0),new Vector3(2,2,0),5f));
    }
    protected override void MobUpdate()
    {
        bossbar.value = health.health / health.maxHealth;
    }

    protected override void DrawGizmos()
    {

    }
}