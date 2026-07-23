using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer; // 인스펙터에서 플레이어 레이어를 선택할 수 있게 해줍니다.
    public EntityHealth plh;
    public PlayerBattle pl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 내 몸에 닿은 오브젝트의 레이어가 targetLayer에 체크된 레이어와 일치하는지 확인하는 공식입니다.
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            DataManager.savedPlayerHealth = plh.health;

            DataManager.atkCool = pl.atkCool;
            DataManager.dashCool = pl.dashCool;
            DataManager.skill1Cool = pl.skill1Cool;
            DataManager.skill2Cool = pl.skill2Cool;
            DataManager.skill3Cool = pl.skill3Cool;
            SceneManager.LoadScene("Phase3");
        }
    }
}