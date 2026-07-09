using UnityEngine;

public class FuseBoxUI : MonoBehaviour
{
    public static FuseBoxUI Instance;

    // 씬을 이동해도 전력 복구 상태를 기억하는 Static 변수
    public static bool Is805Cleared = false;
    public static bool Is810Cleared = false;

    public GameObject uiPanel;
    private PlayerMovement playerMovement;

    [Header("스위치 오브젝트를 1번부터 5번까지 순서대로 꼭 연결하세요!")]
    public FuseSwitchButton switch1;
    public FuseSwitchButton switch2;
    public FuseSwitchButton switch3;
    public FuseSwitchButton switch4;
    public FuseSwitchButton switch5;

    private bool isCleared = false;

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
        if (uiPanel != null && uiPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E)) CloseFuseBox();
        }
    }

    public void OpenFuseBox()
    {
        if (uiPanel != null) uiPanel.SetActive(true);
        if (playerMovement != null) playerMovement.enabled = false;
    }

    public void CloseFuseBox()
    {
        if (uiPanel != null) uiPanel.SetActive(false);
        if (playerMovement != null) playerMovement.enabled = true;
    }

    public void CheckPowerGrid()
    {
        if (isCleared) return;

        // 스위치 연결 누락 확인 방어 코드
        if (switch1 == null || switch2 == null || switch3 == null || switch4 == null || switch5 == null)
        {
            Debug.LogError($"[{gameObject.name}] 인스펙터 창에 Switch 1~5 중 비어있는 칸이 있습니다!");
            return;
        }

        // 콘솔 창에서 5개 스위치 상태를 실시간 모니터링하기 위한 로그
        Debug.Log($"==== [{gameObject.name} 실시간 전수조사] ====\n" +
                  $"1번({switch1.gameObject.name}): {switch1.isOn}\n" +
                  $"2번({switch2.gameObject.name}): {switch2.isOn}\n" +
                  $"3번({switch3.gameObject.name}): {switch3.isOn}\n" +
                  $"4번({switch4.gameObject.name}): {switch4.isOn}\n" +
                  $"5번({switch5.gameObject.name}): {switch5.isOn}");

        // 이미지 반전 현상을 수정한 최종 정답 공식:
        // 눈에 보이는 2번, 4번 스위치만 켜진 상태(ON 모양)일 때 완벽히 성공 처리!
        bool isCorrect = (switch1.isOn == true) &&
                         (switch2.isOn == false) &&
                         (switch3.isOn == true) &&
                         (switch4.isOn == false) &&
                         (switch5.isOn == true);

        if (isCorrect)
        {
            Debug.LogWarning($"★ [성공] {gameObject.name} 퍼즐이 해결되었습니다! ★");
            isCleared = true;

            if (gameObject.name.Contains("805")) Is805Cleared = true;
            else if (gameObject.name.Contains("810")) Is810Cleared = true;
            else
            {
                Is805Cleared = true;
                Is810Cleared = true;
            }

            CloseFuseBox();
        }
    }
}