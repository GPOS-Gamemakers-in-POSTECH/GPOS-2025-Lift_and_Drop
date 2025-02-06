using UnityEngine;

public class Left_Pivot : MonoBehaviour
{
    public float inputX;
    float rotationSpeed = 50f;
    float returnSpeed = 50f;
    public float nextX;
    float maxRotation = 0f;
    float minRotation = -60f;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        inputX = Input.GetAxisRaw("Claw_Up");
    }
    void FixedUpdate()
    {
        LiftClaw();
    }
    void LiftClaw()
    {
        float nextX;
        if (inputX > 0) // 스페이스바를 누르면 올라감
        {
            nextX = -Time.fixedDeltaTime * rotationSpeed;
        }
        else // 스페이스바를 떼면 자동으로 내려감
        {
            nextX = +returnSpeed * Time.fixedDeltaTime; // 아래 방향으로 회전
        }
        float newRotation = Mathf.Clamp(rb.rotation + nextX, minRotation, maxRotation);
        rb.MoveRotation(newRotation);
    }
}
