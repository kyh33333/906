using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    [Header("이동할 인게임 씬 이름")]
    public string inGameSceneName = "InGame"; // 실제 게임 플레이 씬 이름을 적어주세요.

    void Start()
    {
        // 버튼과 함수 연결
        if (startButton != null) startButton.onClick.AddListener(OnClickStart);
        if (exitButton != null) exitButton.onClick.AddListener(OnClickExit);
    }

    void OnClickStart()
    {
        Debug.Log("게임 시작: 인게임 씬으로 이동합니다.");
        // 인게임 씬 로드
        SceneManager.LoadScene(inGameSceneName);
    }

    void OnClickExit()
    {
        Debug.Log("게임 종료: 어플리케이션을 종료합니다.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 테스트 시 멈춤
#else
            Application.Quit(); // 빌드된 게임에서 종료
#endif
    }
}