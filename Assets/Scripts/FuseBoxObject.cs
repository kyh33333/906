using UnityEngine;

public class FuseBoxObject : MonoBehaviour
{
    public GameObject marker; // 포인트 마커 (작은 빛)
    private bool isPlayerNearby = false;

    void Start()
    {
        if (marker != null) marker.SetActive(false);
    }

    void Update()
    {
        // 가까이서 E키 누르면 큰 화면 띄우기
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            FuseBoxUI.Instance.OpenFuseBox();
        }
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