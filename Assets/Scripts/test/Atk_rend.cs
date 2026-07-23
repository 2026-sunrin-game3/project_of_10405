using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public Image cooldownFill1;
    public Image cooldownFill2;
    public Image cooldownFill3;
    public Image cooldownFill4;
    public Image cooldownFill5;

    public PlayerBattle playerBattle;

    void Update()
    {
        if (playerBattle.atkCool > 0)
        {
            cooldownFill1.fillAmount = playerBattle.atkCool / 1f;
        }
        else
        {
            cooldownFill1.fillAmount = 0; 
        }

        if (playerBattle.dashCool > 0)
        {
            cooldownFill2.fillAmount = playerBattle.dashCool / 1f;
        }
        else
        {
            cooldownFill2.fillAmount = 0; 
        }

        if (playerBattle.skill3Cool > 0)
        {
            cooldownFill3.fillAmount = playerBattle.skill3Cool / 6f;
        }
        else
        {
            cooldownFill3.fillAmount = 0; 
        }

        if (playerBattle.skill1Cool > 0)
        {
            cooldownFill4.fillAmount = playerBattle.skill1Cool / 6f;
        }
        else
        {
            cooldownFill4.fillAmount = 0; 
        }

        if (playerBattle.skill2Cool > 0)
        {
            cooldownFill5.fillAmount = playerBattle.skill2Cool / 12f;
        }
        else
        {
            cooldownFill5.fillAmount = 0;
        }
    }
}