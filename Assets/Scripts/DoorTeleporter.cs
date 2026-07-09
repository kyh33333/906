using System.Collections; // 코루틴(IEnumerator) 사용을 위해 필수!
using UnityEngine;

public class DoorTeleporter : MonoBehaviour
{
    [Header("Door Settings")]
    public string roomNumber;
    public bool isLocked;
    public string targetSceneName;

    [Header("Dialogue Settings")]
    [TextArea(3, 5)]
    public string lockedMessage;    // 문이 잠겨있을 때 출력할 대사
    [TextArea(3, 5)]
    public string unlockedMessage;  // 문이 열려있을 때 출력할 대사 (선택사항)

    [Header("Puzzle Settings")]
    public bool unlockBy8FFuseBox = false; // 8층 두꺼비집과 연동된 문인가?

    [Header("Visual Indicator")]
    public GameObject marker;

    private bool isPlayerNearby = false;
    private Coroutine textClearCoroutine; // 글자 지우기 타이머를 관리할 변수

    void Start()
    {
        if (marker != null) marker.SetActive(false);

        // 805호와 810호 전력이 '둘 다' 복구되었을 때만 자동으로 문 잠금 해제!
        if (unlockBy8FFuseBox)
        {
            if (FuseBoxUI.Is805Cleared && FuseBoxUI.Is810Cleared)
            {
                Debug.Log($"{roomNumber}호 문의 잠금이 해제되었습니다 (805, 810 전력 복구 완료).");
                isLocked = false;
            }
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithDoor();
        }
    }

    void InteractWithDoor()
    {
        if (isLocked)
        {
            if (TextWindowManager.Instance != null)
            {
                if (!string.IsNullOrEmpty(lockedMessage))
                {
                    TextWindowManager.Instance.ShowMessage(lockedMessage);
                    // 5초 뒤 지우는 타이머 시작
                    StartTextClearTimer();
                }
                else
                {
                    TextWindowManager.Instance.ShowMessage($"{roomNumber}호 문은 굳게 잠겨 있다.");
                    StartTextClearTimer();
                }
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(unlockedMessage))
            {
                if (TextWindowManager.Instance != null)
                {
                    TextWindowManager.Instance.ShowMessage(unlockedMessage);
                    StartTextClearTimer();
                }
            }

            if (!string.IsNullOrEmpty(targetSceneName))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneName);
            }
        }
    }

    // 이미 돌고 있는 타이머가 있다면 겹치지 않게 취소하고, 새로 5초 타이머를 시작하는 함수
    private void StartTextClearTimer()
    {
        if (textClearCoroutine != null)
        {
            StopCoroutine(textClearCoroutine);
        }
        textClearCoroutine = StartCoroutine(ClearTextAfterSeconds(5.0f));
    }

    // 5초 동안 기다렸다가 글자를 지워주는 코루틴
    private IEnumerator ClearTextAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (TextWindowManager.Instance != null)
        {
            // 빈 문자열을 보내서 화면에서 글자가 안 보이게 처리 (또는 지우는 전용 함수가 있다면 대체 가능)
            TextWindowManager.Instance.ShowMessage("");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (marker != null) marker.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (marker != null) marker.SetActive(false);

            // 플레이어가 문에서 멀어질 때도 글자를 즉시 지우고 싶다면 아래 주석을 해제해줘!
            // if (TextWindowManager.Instance != null) TextWindowManager.Instance.ShowMessage("");
        }
    }
}