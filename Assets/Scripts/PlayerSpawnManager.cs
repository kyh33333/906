using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager Instance;

    // 직전에 탈출한 방 번호를 저장할 전역 메모리 공간 (예: "805", "810", "701" 등)
    public static string lastRoomExited = "";

    [Header("방 내부 씬 이름 등록")]
    [Tooltip("플레이어가 탐색할 모든 '방 내부 씬'들의 이름을 인스펙터에서 등록해 주세요.")]
    public string[] roomSceneNames;

    void Awake()
    {
        // 씬 전환 시 파괴되지 않는 싱글톤 구조 세팅
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 새로운 씬이 로드될 때 실행될 이벤트 함수 연결
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("[스폰 매니저] 시스템이 정상적으로 초기화되었습니다. (DontDestroyOnLoad)");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // 새로운 씬이 로드 완료되면 자동으로 발동하는 함수
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"<color=cyan>[씬 전환 감지]</color> 현재 로드된 씬: <b>{scene.name}</b> | 기억된 이전 탈출 방: <b>{(string.IsNullOrEmpty(lastRoomExited) ? "없음" : lastRoomExited)}</b>");

        // 탈출 기록(방 번호)이 없으면 스폰 로직을 실행하지 않음
        if (string.IsNullOrEmpty(lastRoomExited))
        {
            Debug.Log("[스폰 매니저] 저장된 탈출 방 번호가 없으므로 스폰 위치 조정을 스킵합니다.");
            return;
        }

        // 현재 들어온 씬이 '방 내부' 인지 체크
        bool isRoomScene = false;
        foreach (string roomName in roomSceneNames)
        {
            if (scene.name == roomName)
            {
                isRoomScene = true;
                break;
            }
        }

        // 현재 로드된 씬이 '방 내부 씬'이 아닐 때 (즉, 복도로 나왔을 때) 스폰 처리 진행
        if (!isRoomScene)
        {
            Debug.Log($"<color=yellow>[복도 복귀 확인]</color> 방 내부가 아닌 씬(복도)에 진입했습니다. '{lastRoomExited}'호 문을 탐색합니다.");

            // 복도 씬에 존재하는 모든 'DoorTeleporter' 컴포넌트를 싹 다 찾아냄
            DoorTeleporter[] doors = FindObjectsOfType<DoorTeleporter>();
            Debug.Log($"[맵 전수조사] 현재 복도 씬에서 발견된 총 문의 개수: {doors.Length}개");

            bool spawnSuccess = false;

            foreach (DoorTeleporter door in doors)
            {
                // 디버깅용: 발견한 모든 문의 배치 정보 로그 출력
                Debug.Log($"  -> 복도 문 오브젝트명: <color=white>[{door.gameObject.name}]</color> | 설정된 Room Number: <color=orange>'{door.roomNumber}'</color>");

                // 문이 가진 방 번호와 내가 직전에 탈출했던 방 번호가 일치한다면?
                if (door.roomNumber == lastRoomExited)
                {
                    Debug.Log($"<color=green>[일치하는 문 발견]</color> 오브젝트 <b>[{door.gameObject.name}]</b>이 '{lastRoomExited}'호 문인 것을 확인했습니다.");

                    // 플레이어 오브젝트를 찾음
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        // 해당 복도 문의 위치에서 약간 아래쪽(Y축 -1.2f)으로 플레이어 위치를 강제 이동!
                        Vector3 targetPosition = door.transform.position + new Vector3(0, -1.2f, 0);
                        player.transform.position = targetPosition;

                        Debug.Log($"<color=lime><b>[스폰 성공]</b></color> 플레이어를 [{door.gameObject.name}] 문 앞 좌표({targetPosition})로 성공적으로 텔레포트 시켰습니다.");
                        spawnSuccess = true;
                        break;
                    }
                    else
                    {
                        Debug.LogError("<color=red><b>[스폰 실패]</b></color> 하이어라키에서 'Player' 태그를 가진 오브젝트를 찾을 수 없습니다! 주인공의 Tag를 확인하세요.");
                    }
                }
            }

            if (!spawnSuccess)
            {
                Debug.LogWarning($"<color=orange>[스폰 실패]</color> 복도에서 Room Number가 <b>'{lastRoomExited}'</b>와 일치하는 문 오브젝트를 찾지 못했습니다. 복도 문의 인스펙터를 확인하세요.");
            }

            // 위치 이동 처리가 끝났다면 다음 스폰을 위해 메모리 초기화
            if (spawnSuccess)
            {
                lastRoomExited = "";
                Debug.Log("[스폰 매니저] 다음 스폰 처리를 위해 임시 저장된 방 번호를 비웠습니다.");
            }
        }
        else
        {
            Debug.Log($"[스폰 매니저] 현재 씬은 등록된 방 내부 씬({scene.name})이므로 복도 문 스폰 처리를 대기합니다.");
        }
    }
}