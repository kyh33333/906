using System.Collections;
using UnityEngine;

public class InteractableNote : MonoBehaviour
{
    [Header("Note Image Setting")]
    public Sprite noteSprite;

    public GameObject marker;
    private bool isPlayerNearby = false;
    private bool isCooldown = false;

    void Start()
    {
        if (marker != null)
        {
            marker.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (NoteUiManager.Instance != null)
            {
                if (NoteUiManager.Instance.notePanel.activeSelf)
                {
                    if (isCooldown) return;

                    NoteUiManager.Instance.CloseNote();
                }
                else
                {
                    // 원래 ShowImageNote였던 부분을 ShowContent로 수정
                    // 쪽지는 설명 글이 없으므로 텍스트 칸에는 빈값""을 전달
                    NoteUiManager.Instance.ShowContent(noteSprite, "");
                    StartCoroutine(OpenCooldownRoutine());
                }
            }
        }
    }

    private IEnumerator OpenCooldownRoutine()
    {
        isCooldown = true;
        yield return new WaitForSeconds(1.0f);
        isCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (marker != null)
            {
                marker.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (marker != null)
            {
                marker.SetActive(false);
            }

            if (NoteUiManager.Instance != null && NoteUiManager.Instance.notePanel.activeSelf)
            {
                NoteUiManager.Instance.CloseNote();
            }
            isCooldown = false;
        }
    }
}