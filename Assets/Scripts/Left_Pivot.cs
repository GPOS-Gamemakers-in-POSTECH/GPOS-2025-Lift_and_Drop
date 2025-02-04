using UnityEngine;

public class Left_Pivot : MonoBehaviour
{
    public float rotationSpeed = 1f; // ���� �ö󰡴� �ӵ�
    private bool isMovingUp = false; // ���԰� �ö󰡴� �������� Ȯ���ϴ� ����
    public float maxRotation = 60f; // �ִ� ȸ�� ���� (60��)
    private void Update()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        if (Input.GetKey(KeyCode.Space))
        {
            // �����̽��ٸ� ������ ���԰� �ö�
            isMovingUp = true;
        }
        else
        {
            // �����̽��ٸ� ���� ���԰� ������
            isMovingUp = false;
        }

        // ���԰� �ö󰡴� ���¶��
        if (isMovingUp)
        {
            if (currentRotation < maxRotation)
            {
                transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            };
        }
        else
        {
            if (currentRotation >= 0)
            {
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
