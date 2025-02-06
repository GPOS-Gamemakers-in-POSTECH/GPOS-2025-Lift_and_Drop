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
        if (inputX > 0) // �����̽��ٸ� ������ �ö�
        {
            nextX = -Time.fixedDeltaTime * rotationSpeed;
        }
        else // �����̽��ٸ� ���� �ڵ����� ������
        {
            nextX = +returnSpeed * Time.fixedDeltaTime; // �Ʒ� �������� ȸ��
        }
        float newRotation = Mathf.Clamp(rb.rotation + nextX, minRotation, maxRotation);
        rb.MoveRotation(newRotation);
    }
}
