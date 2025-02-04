using UnityEngine;

public class Claw_Controller : MonoBehaviour
{
    public Transform leftClaw;   // 왼쪽 집게
    public Transform rightClaw;  // 오른쪽 집게
    public float openSpeed = 100f;  // 벌어지는 속도
    public float closeSpeed = 100f; // 닫히는 속도
    public float maxAngle = 30f;    // 최대 벌어질 각도

    private bool isOpening = false;

    void Update()
    {
        isOpening = Input.GetKey(KeyCode.Space);
        UpdateClaw();
    }

    void UpdateClaw()
    {
        float angleChange = (isOpening ? openSpeed : -closeSpeed) * Time.deltaTime;

        // 왼쪽 집게 회전 (Z축 기준)
        float newLeftAngle = Mathf.Clamp(leftClaw.localEulerAngles.z + angleChange, 0, maxAngle);
        leftClaw.localEulerAngles = new Vector3(0, 0, newLeftAngle);

        // 오른쪽 집게 회전 (반대 방향)
        float newRightAngle = Mathf.Clamp(rightClaw.localEulerAngles.z - angleChange, -maxAngle, 0);
        rightClaw.localEulerAngles = new Vector3(0, 0, newRightAngle);
    }
}
