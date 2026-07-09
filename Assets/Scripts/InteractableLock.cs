using UnityEngine;

public class InteractableLock : MonoBehaviour
{
    public GameObject marker;
    [SerializeField] private bool isPlayerNearby = false; // 인스펙터에서 실시간 확인용

    void Start()
    {
        if (marker != null)
        {
            marker.SetActive(false);
        }
    }

    void Update()
    {
        // 1. 매 프레임 E키를 누르는지 체크
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 2. E키를 눌렀을 때 플레이어가 근처에 있는지 체크
            if (isPlayerNearby)
            {
                Debug.Log("자물쇠 앞에서 E키 입력 감지 성공!");

                if (LockManager.Instance != null)
                {
                    Debug.Log("LockManager 인스턴스 발견! OpenLock 호출합니다.");
                    LockManager.Instance.OpenLock();
                }
                else
                {
                    Debug.LogError("에러: 씬에 LockManager 오브젝트가 없거나 스크립트가 안 붙어 있습니다!");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("플레이어 자물쇠 범위 진입 완료");
            if (marker != null)
            {
                marker.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("플레이어 자물쇠 범위 이탈 완료");
            if (marker != null)
            {
                marker.SetActive(false);
            }
            if (LockManager.Instance != null)
            {
                LockManager.Instance.CloseLock();
            }
        }
    }
}