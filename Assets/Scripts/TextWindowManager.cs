using UnityEngine;
using UnityEngine.UI;

public class TextWindowManager : MonoBehaviour
{
    public static TextWindowManager Instance;

    public GameObject textWindowPanel; // 텍스트를 담은 UI 패널
    public Text messageText;           // 화면에 보여줄 텍스트 컴포넌트

    private bool isWindowOpen = false;
    private PlayerMovement playerMovement;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        // 시작할 때는 화면에서 텍스트 창 숨기기
        if (textWindowPanel != null) textWindowPanel.SetActive(false);
    }

    void Update()
    {
        // 메시지 창이 열려있을 때 E나 ESC를 누르면 닫기
        if (isWindowOpen && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            CloseWindow();
        }
    }

    public void ShowMessage(string message)
    {
        if (textWindowPanel == null || messageText == null) return;

        messageText.text = message;
        textWindowPanel.SetActive(true);
        isWindowOpen = true;

        // 메시지 창이 떠 있는 동안 플레이어 이동 잠금
        if (playerMovement != null) playerMovement.enabled = false;
    }

    public void CloseWindow()
    {
        textWindowPanel.SetActive(false);
        isWindowOpen = false;

        // 메시지 창이 닫히면 플레이어 이동 해제
        if (playerMovement != null) playerMovement.enabled = true;
    }
}