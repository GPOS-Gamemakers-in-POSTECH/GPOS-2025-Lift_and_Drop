using UnityEngine;
using UnityEngine.UIElements;

public class Claw_Crane : MonoBehaviour
{
    public float maxLength = 5f;
    public float originalmaxLength;
    [SerializeField] private float speed = 3f;

    private float firstPositionY;

    public Vector2 inputVec;

    private Rigidbody2D _rigidbdoy;


    void Awake()
    {
        _rigidbdoy = GetComponent<Rigidbody2D>();
        originalmaxLength = maxLength;
        firstPositionY = _rigidbdoy.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.y = Input.GetAxisRaw("Vertical");
        if (firstPositionY - _rigidbdoy.position.y >= maxLength && inputVec.y < 0)
        {
            inputVec.y = 0;  // ÇÏÇâ ÀÌµ¿À» ¸ØÃã
        }
    }
    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        _rigidbdoy.MovePosition(_rigidbdoy.position + nextVec);
    }
}
