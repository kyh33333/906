using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleporter : MonoBehaviour
{
    [Header("이동할 씬 이름 (대소막자 일치)")]
    public string targetSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 무언가 문에 부딪혔을 때 콘솔에 출력
        Debug.Log($"[문 충돌 감지] 부딪힌 오브젝트: {collision.gameObject.name} | 태그: {collision.tag}");

        // 부딪힌 오브젝트의 태그가 "Player"인지 확인!
        if (collision.CompareTag("Player"))
        {
            Debug.Log("[플레이어 확인] 플레이어가 문에 닿았습니다. 씬 전환을 시도합니다.");

            if (!string.IsNullOrEmpty(targetSceneName))
            {
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                Debug.LogWarning("[경고] 이동할 targetSceneName이 비어있습니다!");
            }
        }
    }
}