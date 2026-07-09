using UnityEngine;

public class FuseBoxUI : MonoBehaviour
{
    public static FuseBoxUI Instance;

    public GameObject uiPanel;
    private PlayerMovement playerMovement;

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
        // UI 창이 열려있을 때 오직 E 키를 누를 때만 정상적으로 닫힘 (ESC 제거)
        if (uiPanel != null && uiPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CloseFuseBox();
            }
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
}