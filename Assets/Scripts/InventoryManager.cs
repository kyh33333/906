using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI Slots")]
    public Image[] slots;

    [Header("Item Sprites Inventory Icon")]
    public Sprite img8FKey;
    public Sprite img810Key;
    public Sprite imgSpade;
    public Sprite imgHeart;
    public Sprite imgDiamond;

    [Header("Item Big Sprites Enlarged View")]
    [SerializeField] private Sprite big8FKey;
    [SerializeField] private Sprite big810Key;
    [SerializeField] private Sprite bigSpade;   // 오타 수정완료
    [SerializeField] private Sprite bigHeart;   // 오타 수정완료
    [SerializeField] private Sprite bigDiamond; // 오타 수정완료

    [Header("Item Descriptions Text")]
    [TextArea(3, 5)] public string desc8FKey;
    [TextArea(3, 5)] public string desc810Key;
    [TextArea(3, 5)] public string descSpade;
    [TextArea(3, 5)] public string descHeart;
    [TextArea(3, 5)] public string descDiamond;

    private int currentIndex = 0;
    private string[] slotItemNames = new string[4];

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ClearInventory();
    }

    public void AddItem(string itemName)
    {
        if (currentIndex >= slots.Length) return;

        Sprite targetSprite = null;

        switch (itemName)
        {
            case "8FKey": targetSprite = img8FKey; break;
            case "810Key": targetSprite = img810Key; break;
            case "Spade": targetSprite = imgSpade; break;
            case "Heart": targetSprite = imgHeart; break;
            case "Diamond": targetSprite = imgDiamond; break;
        }

        if (targetSprite == null) return;

        slots[currentIndex].sprite = targetSprite;
        slots[currentIndex].color = Color.white;

        slotItemNames[currentIndex] = itemName;
        currentIndex++;
    }

    public void ShowItemDetail(int slotIndex)
    {
        if (slotIndex >= currentIndex || string.IsNullOrEmpty(slotItemNames[slotIndex])) return;

        string itemName = slotItemNames[slotIndex];
        Sprite bigSprite = null;
        string detailText = "";

        switch (itemName)
        {
            case "8FKey": bigSprite = big8FKey; detailText = desc8FKey; break;
            case "810Key": bigSprite = big810Key; detailText = desc810Key; break;
            case "Spade": bigSprite = bigSpade; detailText = descSpade; break;
            case "Heart": bigSprite = bigHeart; detailText = descHeart; break;
            case "Diamond": bigSprite = bigDiamond; detailText = descDiamond; break;
        }

        if (NoteUiManager.Instance != null)
        {
            NoteUiManager.Instance.ShowContent(bigSprite, detailText);
        }
    }

    private void ClearInventory()
    {
        currentIndex = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = null;
            slots[i].color = new Color(1f, 1f, 1f, 0f);
            slotItemNames[i] = "";
        }
    }
}