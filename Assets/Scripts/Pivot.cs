using UnityEngine;

public class Pivot : MonoBehaviour
{
    public float inputX;
    public float nextX;

    [SerializeField] private float rotationSpeedCW = 0;
    [SerializeField] private float returnSpeedCW = 0;

    [SerializeField] private float maxRotation = 0f;
    [SerializeField] private float minRotation = 0;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Claw_Up");
    }

    private void FixedUpdate()
    {
        if (inputX > 0)
            nextX = rotationSpeedCW * Time.fixedDeltaTime;
        else
            nextX = returnSpeedCW * Time.fixedDeltaTime;

        float newRotation = Mathf.Clamp(_rigidbody.rotation + nextX, minRotation, maxRotation);

        _rigidbody.MoveRotation(newRotation);
    }
}