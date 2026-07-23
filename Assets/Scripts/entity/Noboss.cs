using UnityEngine;
using UnityEngine.UI;

public class Noboss : Enemy
{
    [SerializeField] Slider bossbar;
    protected override void MobUpdate()
    {
        bossbar.value = health.health / health.maxHealth;
    }
}
