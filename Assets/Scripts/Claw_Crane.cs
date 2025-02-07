using UnityEngine;

public class Claw_Crane : MonoBehaviour
{
    public float maxLength = 5f;

    [SerializeField] private float speed = 3f;

    private float firstPostionY;
    private float inputY;

    private Rigidbody2D _rigidbdoy;
    private SliderJoint2D _joint;

    private void Awake()
    {
        _rigidbdoy = GetComponent<Rigidbody2D>();
        _joint = GetComponent<SliderJoint2D>();

        firstPostionY = _rigidbdoy.position.y;
        inputY = 0;
    }

    private void Update()
    {
        inputY = Input.GetAxisRaw("Vertical");
        if (firstPostionY - _rigidbdoy.position.y >= maxLength && inputY < 0)
        {
            inputY = 0;  // ÇÏÇâ ÀÌµ¿À» ¸ØÃã
        }
    }

    private void FixedUpdate()
    {
        JointMotor2D motor = _joint.motor;
        motor.motorSpeed = speed * inputY;
        _joint.motor = motor;
    }
}