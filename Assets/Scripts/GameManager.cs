using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Item Data")]
    public bool has8FKey = false;
    public bool has810Key = false;
    public bool hasSpade = false;
    public bool hasHeart = false;
    public bool hasDiamond = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 넘어가도 데이터가 유지되도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }
}