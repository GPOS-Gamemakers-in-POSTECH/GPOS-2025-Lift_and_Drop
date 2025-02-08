using UnityEngine;

public class Pivot : MonoBehaviour
{
    public float inputX;
    public float nextX;

    [SerializeField] private bool IsClockwise = false;

    [SerializeField] private float rotationSpeedCW = 60f;
    [SerializeField] private float returnSpeedCW = 50f;

    [SerializeField] private float maxRotation = 60f;
    [SerializeField] private float minRotation = 0;

    Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        if (inputX > 0) // 스페이스바를 누르면 올라감
        {
            nextX = rotationSpeedCW * Time.fixedDeltaTime * (IsClockwise ? -1 : 1);
        }
        else // 스페이스바를 떼면 자동으로 내려감
        {
            nextX = -returnSpeedCW * Time.fixedDeltaTime * (IsClockwise ? -1 : 1); // 아래 방향으로 회전
        }

        float newRotation = Mathf.Clamp(_rigidbody.rotation + nextX, minRotation, maxRotation);
        if (IsClockwise)
        {
            newRotation = Mathf.Clamp(_rigidbody.rotation + nextX, -maxRotation, minRotation);
        }
        _rigidbody.MoveRotation(newRotation);
    }
}
