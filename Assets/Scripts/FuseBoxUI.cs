using System.Collections;
using UnityEngine;

public class FuseBoxUI : MonoBehaviour
{
    public static FuseBoxUI Instance;

    public GameObject uiPanel;
    private PlayerMovement playerMovement;

    public float closeDelay = 0.5f;
    private bool canClose = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (uiPanel != null) uiPanel.SetActive(false);
    }

    void Update()
    {
        // 1단계 확인: UI 패널이 켜져있을 때 Update가 돌고 있는지 체크
        if (uiPanel != null && uiPanel.activeSelf)
        {
            // 2단계 확인: E키를 누르는 순간을 유니티가 감지하는지 체크
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log($"[디버깅] E키 입력 감지됨! 현재 canClose 상태: {canClose}");

                if (canClose)
                {
                    Debug.Log("[디버깅] 조건 만족! CloseFuseBox()를 호출합니다.");
                    CloseFuseBox();
                }
                else
                {
                    Debug.Log("[디버깅] 딜레이 시간이 안 지나서 닫기가 거부되었습니다.");
                }
            }

            // ESC는 무조건 닫히는지 확인용
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("[디버깅] ESC키로 닫기 시도");
                CloseFuseBox();
            }
        }
    }

    public void OpenFuseBox()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
            Debug.Log("[디버깅] OpenFuseBox() 호출됨. 패널 활성화.");
            StartCoroutine(EnableCloseDelay());
        }
    }

    public void CloseFuseBox()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
            Debug.Log("[디버깅] uiPanel.SetActive(false) 실행 완료.");
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        canClose = false;
    }

    private IEnumerator EnableCloseDelay()
    {
        canClose = false;
        yield return new WaitForSeconds(closeDelay);
        canClose = true;
        Debug.Log("[디버깅] 딜레이 타임 종료! 이제 닫을 수 있습니다.");
    }
}