using UnityEngine;

public class Right_Pivot : MonoBehaviour
{
    public float rotationSpeed = 1f; // ���� �ö󰡴� �ӵ�
    private bool isMovingUp = false; // ���԰� �ö󰡴� �������� Ȯ���ϴ� ����

    private void Update()
    {
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
            transform.Rotate(0, 0, +rotationSpeed * Time.deltaTime);
        }
        else
        {
            // �������� ���¶��
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
    }
}
