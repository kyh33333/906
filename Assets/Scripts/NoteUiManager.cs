using UnityEngine;
using UnityEngine.UI;

public class NoteUiManager : MonoBehaviour
{
    public static NoteUiManager Instance;

    public GameObject notePanel;
    public Image noteImage;
    public Text noteText;

    private PlayerMovement playerMovement;

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

        if (notePanel != null)
        {
            notePanel.SetActive(false);
        }
    }

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        // 만약 확대 창이 켜져 있는 상태라면
        if (notePanel != null && notePanel.activeSelf)
        {
            // ESC 키나 E 키를 누르면 창이 닫힙니다.
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                CloseNote();
            }
        }
    }

    public void ShowContent(Sprite imageSprite, string textContent)
    {
        if (notePanel == null || noteImage == null || noteText == null) return;

        noteImage.gameObject.SetActive(imageSprite != null);
        noteText.gameObject.SetActive(!string.IsNullOrEmpty(textContent));

        noteImage.sprite = imageSprite;
        noteText.text = textContent;

        if (imageSprite != null)
        {
            noteImage.color = Color.white;
        }

        notePanel.SetActive(true);

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }

    public void CloseNote()
    {
        if (notePanel == null) return;

        notePanel.SetActive(false);
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }
}