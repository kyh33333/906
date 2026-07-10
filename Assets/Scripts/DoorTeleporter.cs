using System.Collections;
using UnityEngine;

public class DoorTeleporter : MonoBehaviour
{
    [Header("Door Settings")]
    public string roomNumber;       // 중요! 방 번호 (예: 805, 810, 701 등 문자열 일치 필수)
    public bool isLocked;
    public string targetSceneName;  // 이동할 씬 이름

    [Header("Dialogue Settings")]
    [TextArea(3, 5)]
    public string lockedMessage;
    [TextArea(3, 5)]
    public string unlockedMessage;

    [Header("Puzzle Settings")]
    public bool unlockBy8FFuseBox = false;

    [Header("Visual Indicator")]
    public GameObject marker;

    private bool isPlayerNearby = false;
    private Coroutine textClearCoroutine;

    void Start()
    {
        if (marker != null) marker.SetActive(false);

        // 805호와 810호 두꺼비집 퍼즐이 둘 다 풀렸다면 자동으로 문 잠금 해제
        if (unlockBy8FFuseBox)
        {
            if (FuseBoxUI.Is805Cleared && FuseBoxUI.Is810Cleared)
            {
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

            // [핵심] 복도로 나가기 직전, 이 문에 적힌 방 번호를 매니저 메모리에 기록!
            PlayerSpawnManager.lastRoomExited = this.roomNumber;

            if (!string.IsNullOrEmpty(targetSceneName))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneName);
            }
        }
    }

    private void StartTextClearTimer()
    {
        if (textClearCoroutine != null)
        {
            StopCoroutine(textClearCoroutine);
        }
        textClearCoroutine = StartCoroutine(ClearTextAfterSeconds(5.0f));
    }

    private IEnumerator ClearTextAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (TextWindowManager.Instance != null)
        {
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
        }
    }
}