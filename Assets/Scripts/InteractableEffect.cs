using UnityEngine;

public class InteractableEffect : MonoBehaviour
{
    [Header("Visual Effect Object")]
    [Tooltip("오브젝트 위에 표시될 E키 아이콘이나 하이라이트 효과를 드래그해서 넣어주세요.")]
    public GameObject interactEffect; // 우리가 나타나게 할 '오브젝트 하나'

    private void Start()
    {
        // 시작할 때는 효과를 숨겨둡니다.
        if (interactEffect != null)
        {
            interactEffect.SetActive(false);
        }
    }

    // 플레이어가 콜라이더 범위(Trigger) 안으로 들어왔을 때 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 들어온 오브젝트의 태그가 "Player"인지 확인
        if (collision.CompareTag("Player"))
        {
            // 상호작용 효과를 나타나게 합니다.
            if (interactEffect != null)
            {
                interactEffect.SetActive(true);
            }
        }
    }

    // 플레이어가 콜라이더 범위(Trigger) 밖으로 나갔을 때 실행
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 나간 오브젝트가 "Player"라면
        if (collision.CompareTag("Player"))
        {
            // 상호작용 효과를 다시 숨깁니다.
            if (interactEffect != null)
            {
                interactEffect.SetActive(false);
            }
        }
    }
}