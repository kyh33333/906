using UnityEngine;

public class FuseBoxUI : MonoBehaviour
{
    public static FuseBoxUI Instance; // 어디서나 쉽게 열고 닫을 수 있게 싱글톤 세팅

    public GameObject uiPanel; // 인스펙터에서 크게 만든 두꺼비집 UI 패널 연결
    private PlayerMovement playerMovement; // 플레이어 이동을 멈추기 위함

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        uiPanel.SetActive(false); // 시작할 때는 큰 화면 숨기기
    }

    void Update()
    {
        // 두꺼비집 창이 열려있을 때 ESC나 E를 누르면 닫기
        if (uiPanel.activeSelf && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)))
        {
            CloseFuseBox();
        }
    }

    public void OpenFuseBox()
    {
        uiPanel.SetActive(true);
        if (playerMovement != null) playerMovement.enabled = false; // 플레이어 조작 잠금
    }

    public void CloseFuseBox()
    {
        uiPanel.SetActive(false);
        if (playerMovement != null) playerMovement.enabled = true;  // 플레이어 조작 해제
    }
}