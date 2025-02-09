using System.Collections;
using UnityEngine;

public class Trap_Spinner : MonoBehaviour
{
    public bool IsMoving = false;
    public bool IsHorizontal = false;
    public int OppositeDir = -1;
    [SerializeField] private float movingSpeedTS = 6f;
    [SerializeField] private float rotationSpeedTS = 200f;
    private Vector2 moveDirection = Vector2.up;

    Rigidbody2D _rigidbody;
    Collider2D _collider;
    void Start()
    {

    }
    void Awake()
    {
        if (IsHorizontal)
        {
            moveDirection = Vector2.right;
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.MoveRotation(_rigidbody.rotation + rotationSpeedTS * Time.fixedDeltaTime);
        if (IsMoving)
        {
            MoveLinear();
        }
    }

    void MoveLinear()
    {
        _rigidbody.MovePosition(_rigidbody.position + moveDirection * movingSpeedTS * Time.fixedDeltaTime * OppositeDir);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tiles"))
        {
            OppositeDir *= -1;
        }
        else if (collision.gameObject.CompareTag("SwitchTile"))
        {
            OppositeDir *= -1;
        }
        else if (collision.gameObject.layer == 20 || collision.gameObject.layer == 21)
        {
            StartCoroutine(Crushed(collision));
        }
    }
    private IEnumerator Crushed(Collision2D collision)
    {
        moveDirection = (transform.position - collision.transform.position).normalized;
        movingSpeedTS = 40f;
        OppositeDir = 1;
        _collider.isTrigger = true;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
