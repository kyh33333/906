using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("--- 걷기 애니메이션 클립 (Animation Clips) ---")]
    public AnimationClip walkDown;
    public AnimationClip walkUp;
    public AnimationClip walkLeft;
    public AnimationClip walkRight;

    [Header("--- 서 있기 애니메이션 클립 (Animation Clips) ---")]
    public AnimationClip idleDown;
    public AnimationClip idleUp;
    public AnimationClip idleLeft;
    public AnimationClip idleRight;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;

    // 마지막으로 바라보던 방향 (기본값: 아래)
    private string lastDirection = "Down";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (moveInput.sqrMagnitude > 0)
        {
            moveInput = moveInput.normalized;

            // 이동 방향에 따라 인스펙터에 등록된 클립을 재생
            if (moveInput.x > 0)
            {
                PlayAnimationClip(walkRight);
                lastDirection = "Right";
            }
            else if (moveInput.x < 0)
            {
                PlayAnimationClip(walkLeft);
                lastDirection = "Left";
            }
            else if (moveInput.y > 0)
            {
                PlayAnimationClip(walkUp);
                lastDirection = "Up";
            }
            else if (moveInput.y < 0)
            {
                PlayAnimationClip(walkDown);
                lastDirection = "Down";
            }
        }
        else
        {
            // 가만히 멈췄을 때 마지막 방향의 서 있기 클립 재생
            if (lastDirection == "Right") PlayAnimationClip(idleRight);
            else if (lastDirection == "Left") PlayAnimationClip(idleLeft);
            else if (lastDirection == "Up") PlayAnimationClip(idleUp);
            else if (lastDirection == "Down") PlayAnimationClip(idleDown);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    // 클립 자체를 받아서 안전하게 틀어주는 핵심 함수
    void PlayAnimationClip(AnimationClip clip)
    {
        if (clip != null)
        {
            // 애니메이터가 관리하는 상자 이름 대신, 클립 파일 이름 자체를 찾아 재생함
            anim.Play(clip.name);
        }
    }
}