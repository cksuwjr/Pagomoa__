using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;

    private float direction;
    private bool jump;
    private bool isGround;

    public LayerMask whatIsGround;

    public Transform groundCheck;
        
    public float speed = 5f;
    public float jumpForce = 150f;
    // Start is called before the first frame update

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
            isGround = false;
        }
        direction = Input.GetAxisRaw("Horizontal");
        if(direction != 0)
            transform.localScale = new Vector3(direction, 1, 1);
    }
    void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
        if(collider != null)
            isGround = true;
        Vector2 v = Vector2.zero;
        m_rigidbody.velocity = Vector2.SmoothDamp(m_rigidbody.velocity, new Vector2(direction * speed, m_rigidbody.velocity.y), ref v, 0.06f);
        if (jump && isGround)
            m_rigidbody.AddForce(new Vector2(0, jumpForce));
        jump = false;
    }
}
