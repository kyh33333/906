using UnityEngine;
using UnityEngine.UI;

public class FuseSwitchButton : MonoBehaviour
{
    public bool isOn = true;

    [Header("Switch Sprites")]
    public Sprite switchOnSprite;
    public Sprite switchOffSprite;

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();

        // 버튼 클릭 이벤트 연결
        GetComponent<Button>().onClick.AddListener(OnClickSwitch);

        UpdateVisual();
    }

    void OnClickSwitch()
    {
        isOn = !isOn;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        // 인스펙터에 등록된 스프라이트로 교체
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