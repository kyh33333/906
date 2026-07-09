using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // [추가] 씬 전환을 위해 필수!

public class LockManager : MonoBehaviour
{
    public static LockManager Instance;

    [Header("Lock UI Panel")]
    public GameObject lockPanel;

    [Header("Current Input Text Slots")]
    public Text[] inputTexts;

    [Header("Answer Settings")]
    public int[] correctAnswer = { 1, 3, 3 };

    // [추가] 유니티 에디터에 있는 엔딩 씬의 정확한 이름을 적어줄 변수
    [Header("Ending Settings")]
    public string endingSceneName = "EndingScene";

    private int[] currentInput = { 0, 0, 0 };
    private PlayerMovement playerMovement;
    private bool canCloseWithKey = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }

        if (lockPanel != null)
        {
            lockPanel.SetActive(false);
        }
    }

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        UpdateLockUi();
    }

    void Update()
    {
        if (lockPanel != null && lockPanel.activeSelf && canCloseWithKey)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                CloseLock();
            }
        }
    }

    public void OpenLock()
    {
        if (lockPanel == null || lockPanel.activeSelf) return;

        lockPanel.SetActive(true);
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        InteractableLock[] interlocks = FindObjectsOfType<InteractableLock>();
        foreach (InteractableLock lockScript in interlocks)
        {
            lockScript.enabled = false;
        }

        StartCoroutine(EnableCloseDelay());
    }

    private IEnumerator EnableCloseDelay()
    {
        canCloseWithKey = false;
        yield return new WaitForSeconds(0.15f);
        canCloseWithKey = true;
    }

    public void CloseLock()
    {
        if (lockPanel == null) return;

        lockPanel.SetActive(false);
        canCloseWithKey = false;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        InteractableLock[] interlocks = FindObjectsOfType<InteractableLock>();
        foreach (InteractableLock lockScript in interlocks)
        {
            lockScript.enabled = true;
        }
    }

    public void ChangeNumber(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= currentInput.Length) return;

        currentInput[slotIndex] = (currentInput[slotIndex] + 1) % 10;
        UpdateLockUi();
    }

    private void UpdateLockUi()
    {
        for (int i = 0; i < inputTexts.Length; i++)
        {
            if (inputTexts[i] != null)
            {
                inputTexts[i].text = currentInput[i].ToString();
            }
        }
    }

    public void OnClickEnterButton()
    {
        Debug.Log($"[데이터 검사] 슬롯0: {currentInput[0]} | 슬롯1: {currentInput[1]} | 슬롯2: {currentInput[2]}");

        bool isCorrect = true;
        for (int i = 0; i < correctAnswer.Length; i++)
        {
            if (currentInput[i] != correctAnswer[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("Lock Unlocked! 자물쇠가 풀렸습니다. 엔딩으로 이동합니다.");
            OnUnlockSuccess();
        }
        else
        {
            Debug.Log("Wrong Password! 비밀번호가 일치하지 않습니다.");
        }
    }

    private void OnUnlockSuccess()
    {
        // [수정] 자물쇠 창을 닫은 직후, 지정한 엔딩 씬을 강제로 로드합니다.
        CloseLock();
        SceneManager.LoadScene(endingSceneName);
    }
}