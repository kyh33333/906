using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Door Settings")]
    public string roomNumber;
    public bool isLocked;
    public string targetSceneName;

    [Header("Visual Indicator")]
    public GameObject marker;

    private bool isPlayerNearby = false;

    void Start()
    {
        if (marker != null) marker.SetActive(false);
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
            // 콘솔이 아니라 화면 UI 매니저를 호출하여 "000호다." 출력
            if (TextWindowManager.Instance != null)
            {
                TextWindowManager.Instance.ShowMessage($"{roomNumber}호다.");
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneName);
            }
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