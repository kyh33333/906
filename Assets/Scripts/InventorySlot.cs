using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int slotIndex; // 인스펙터에서 각 슬롯에 맞게 0, 1, 2, 3 지정
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnSlotClick);
        }
    }

    void OnSlotClick()
    {
        // 인벤토리 매니저에게 몇 번째 슬롯이 클릭되었는지 전달
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ShowItemDetail(slotIndex);
        }
    }
}