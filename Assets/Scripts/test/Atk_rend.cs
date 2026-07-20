using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image cooldownFill;

    public PlayerBattle playerBattle;
    private float maxCooldown = 1f; 

    void Update()
    {
        if (playerBattle == null || cooldownFill == null) return;

        if (playerBattle.atkCool > 0)
        {
            cooldownFill.fillAmount = playerBattle.atkCool / maxCooldown;
        }
        else
        {
            cooldownFill.fillAmount = 0; 
        }
    }
}