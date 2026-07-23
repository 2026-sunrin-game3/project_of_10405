using UnityEngine;
using UnityEngine.UI;

public class LoadHealth : MonoBehaviour
{
    public EntityHealth plh;
    public EntityHealth bo;
    public PlayerBattle pl;
    void Awake()
    {
        bo.health = DataManager.savedBossHealth;
        plh.health = DataManager.savedPlayerHealth;

        pl.atkCool = DataManager.atkCool;
        pl.dashCool = DataManager.dashCool;
        pl.skill1Cool = DataManager.skill1Cool;
        pl.skill2Cool = DataManager.skill2Cool;
        pl.skill3Cool = DataManager.skill3Cool;
    }
}
