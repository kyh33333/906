using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public enum ItemType
    {
        EightFloorKey,
        Eight10RoomKey,
        SpadeCard,
        HeartCard,
        DiamondCard
    }

    [Header("Item Setting")]
    public ItemType itemType;
    public GameObject marker;

    private bool isPlayerNearby = false;

    void Start()
    {
        if (marker != null) marker.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        switch (itemType)
        {
            case ItemType.EightFloorKey:
                GameManager.Instance.has8FKey = true;
                InventoryManager.Instance.AddItem("8FKey");
                break;

            case ItemType.Eight10RoomKey:
                GameManager.Instance.has810Key = true;
                InventoryManager.Instance.AddItem("810Key");
                break;

            case ItemType.SpadeCard:
                GameManager.Instance.hasSpade = true;
                InventoryManager.Instance.AddItem("Spade");
                break;

            case ItemType.HeartCard:
                GameManager.Instance.hasHeart = true;
                InventoryManager.Instance.AddItem("Heart");
                break;

            case ItemType.DiamondCard:
                GameManager.Instance.hasDiamond = true;
                InventoryManager.Instance.AddItem("Diamond");
                break;
        }

        gameObject.SetActive(false);
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