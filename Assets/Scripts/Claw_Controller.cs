using UnityEngine;

public class Claw_Controller : MonoBehaviour
{
    public Transform leftClaw;   // ���� ����
    public Transform rightClaw;  // ������ ����
    public float openSpeed = 100f;  // �������� �ӵ�
    public float closeSpeed = 100f; // ������ �ӵ�
    public float maxAngle = 30f;    // �ִ� ������ ����

    private bool isOpening = false;

    void Update()
    {
        isOpening = Input.GetKey(KeyCode.Space);
        UpdateClaw();
    }

    void UpdateClaw()
    {
        float angleChange = (isOpening ? openSpeed : -closeSpeed) * Time.deltaTime;

        // ���� ���� ȸ�� (Z�� ����)
        float newLeftAngle = Mathf.Clamp(leftClaw.localEulerAngles.z + angleChange, 0, maxAngle);
        leftClaw.localEulerAngles = new Vector3(0, 0, newLeftAngle);

        // ������ ���� ȸ�� (�ݴ� ����)
        float newRightAngle = Mathf.Clamp(rightClaw.localEulerAngles.z - angleChange, -maxAngle, 0);
        rightClaw.localEulerAngles = new Vector3(0, 0, newRightAngle);
    }
}
