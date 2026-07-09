using System.Collections;
using UnityEngine;

public class FuseBoxObject : MonoBehaviour
{
    public GameObject marker;
    private bool isPlayerNearby = false;
    private bool isCooldown = false; //  딜레이 중인지 체크할 변수

    void Start()
    {
        if (marker != null) marker.SetActive(false);
    }

    void Update()
    {
        // 가까이 있고 + E키를 눌렀고 + 딜레이 중이 아닐 때만 실행
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isCooldown)
        {
            if (FuseBoxUI.Instance != null)
            {
                // UI가 이미 켜져 있다면 여기서는 아무것도 안 함 (FuseBoxUI가 닫을 수 있게 양보)
                if (FuseBoxUI.Instance.uiPanel.activeSelf)
                {
                    return;
                }

                // UI가 꺼져있을 때만 새로 열기 요청 후 1초 딜레이 시작
                FuseBoxUI.Instance.OpenFuseBox();
                StartCoroutine(OpenCooldownRoutine());
            }
        }
    }

    // 1초 동안 열기 기능을 잠그는 코루틴
    private IEnumerator OpenCooldownRoutine()
    {
        isCooldown = true;
        yield return new WaitForSeconds(1.0f); // 1초 동안 대기
        isCooldown = false;
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