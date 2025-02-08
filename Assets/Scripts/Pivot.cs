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
        if (inputX > 0) // �����̽��ٸ� ������ �ö�
        {
            nextX = rotationSpeedCW * Time.fixedDeltaTime * (IsClockwise ? -1 : 1);
        }
        else // �����̽��ٸ� ���� �ڵ����� ������
        {
            nextX = -returnSpeedCW * Time.fixedDeltaTime * (IsClockwise ? -1 : 1); // �Ʒ� �������� ȸ��
        }

        float newRotation = Mathf.Clamp(_rigidbody.rotation + nextX, minRotation, maxRotation);
        if (IsClockwise)
        {
            newRotation = Mathf.Clamp(_rigidbody.rotation + nextX, -maxRotation, minRotation);
        }
        _rigidbody.MoveRotation(newRotation);
    }
}
