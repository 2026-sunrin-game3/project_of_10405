using System.Collections;
using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
    [Header("Warning UI")]
    public GameObject warningZonePrefab; 

    /// <summary>
    /// 지정된 위치에 원하는 크기로, 일정 시간 동안 장판을 표시합니다.
    /// </summary>
    /// <param name="position">장판의 중앙이 위치할 절대 좌표 (X, Y, Z)</param>
    /// <param name="size">장판의 전체 크기 (X: 너비, Y: 높이/두께, Z: 깊이/길이)</param>
    /// <param name="time">장판이 유지되는 시간 (초)</param>
    public IEnumerator ShowWarningSimple(Vector3 position, Vector3 size, float time)
    {
        // 1. 입력받은 위치에 장판 생성 (회전 없이 기본 상태)
        GameObject warningZone = Instantiate(warningZonePrefab, position, Quaternion.identity);

        // 2. 입력받은 크기(Scale) 그대로 적용
        warningZone.transform.localScale = size;

        // 3. 입력받은 시간만큼 대기
        yield return new WaitForSeconds(time);
        // 4. 대기 시간이 끝나면 장판 삭제
        Destroy(warningZone);
    }
}