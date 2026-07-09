using UnityEngine;
using UnityEngine.UI;

public class FuseSwitchButton : MonoBehaviour
{
    [Header("Switch Settings")]
    public int switchNumber; // 인스펙터에서 1, 2, 3, 4, 5 지정해줄 번호
    public bool isOn = false; // 기본값 꺼짐(false)

    [Header("Switch Sprites")]
    public Sprite switchOnSprite;
    public Sprite switchOffSprite;

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();

        // 중복 리스너 등록 방지 처리
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(OnClickSwitch);
        }

        UpdateVisual();
    }

    void OnClickSwitch()
    {
        isOn = !isOn;
        UpdateVisual();

        // [디버그] 클릭 시 현재 스위치 상태를 콘솔에 출력
        Debug.Log($"[스위치 클릭] 번호: {switchNumber}번 | 이름: {gameObject.name} | 현재 상태(IsOn): {isOn}");

        // 스위치가 바뀔 때마다 UI 매니저에게 정답 체크 요청
        if (FuseBoxUI.Instance != null)
        {
            FuseBoxUI.Instance.CheckPowerGrid();
        }
        else
        {
            Debug.LogError("하이어라키에 FuseBoxUI Instance가 존재하지 않습니다! UI 캔버스를 확인하세요.");
        }
    }

    void UpdateVisual()
    {
        if (buttonImage == null) buttonImage = GetComponent<Image>();
        if (buttonImage == null) return;

        if (isOn)
        {
            if (switchOnSprite != null) buttonImage.sprite = switchOnSprite;
        }
        else
        {
            if (switchOffSprite != null) buttonImage.sprite = switchOffSprite;
        }
    }
}