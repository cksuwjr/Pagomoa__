using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Move : MonoBehaviour
{
    Player player;
    private Rigidbody2D m_rigidbody;

    private float direction;
    private bool jump;
    private bool crawlUp;
    private bool isGround;

    public LayerMask whatIsGround;

    public Transform groundCheck;
        
    public float speed = 5f;
    public float jumpForce = 150f;
    public float crawlSpeed = 5f;
    // Start is called before the first frame update

    void Awake()
    {
        player = GetComponent<Player>();
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
        if (Input.GetKey(KeyCode.C))
        {
            crawlUp = true;
        }
        direction = Input.GetAxisRaw("Horizontal");
        player.SetDirection(direction);
        
    }
    void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
        if(collider != null)
            isGround = true;


        Vector2 TargetVelocity;
        TargetVelocity = crawlUp && player.GroundHeight >= transform.position.y ? new Vector2(direction * speed, crawlSpeed) : new Vector2(direction * speed, m_rigidbody.velocity.y);
        Vector2 v = Vector2.zero;

        m_rigidbody.velocity = Vector2.SmoothDamp(m_rigidbody.velocity, TargetVelocity, ref v, 0.06f);

        if (jump && isGround)
            m_rigidbody.AddForce(new Vector2(0, jumpForce));

        jump = false;
        crawlUp = false;
    }
}
