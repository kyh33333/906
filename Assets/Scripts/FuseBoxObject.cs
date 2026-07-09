using UnityEngine;

public class FuseBoxObject : MonoBehaviour
{
    public GameObject marker;
    private bool isPlayerNearby = false;

    void Start()
    {
        if (marker != null) marker.SetActive(false);
    }

    void Update()
    {
        // 가까이 있고 + E키를 눌렀을 때
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (FuseBoxUI.Instance != null)
            {
                //  만약 UI 패널이 이미 켜져 있다면, 여기서 열기 코드를 실행하지 않음!
                // (UI 매니저가 Update에서 닫는 처리를 할 것이므로, 문 스크립트는 가만히 있어야 함)
                if (FuseBoxUI.Instance.uiPanel.activeSelf)
                {
                    return;
                }

                // UI 패널이 완전히 꺼져있을 때만 새로 열어줍니다.
                FuseBoxUI.Instance.OpenFuseBox();
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