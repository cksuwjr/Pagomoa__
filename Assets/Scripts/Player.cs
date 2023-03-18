using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] Status status;
    [SerializeField] public Move move;
    [SerializeField] public Transform[] HitPointDown;
    [SerializeField] public float GroundHeight = 0;

    public LayerMask WhatisGround;
    public bool Digging;
    public UnityEvent oxygen_alter;
    public UnityEvent hungry_alter;
    public UnityEvent direction_alter;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    // Update is called once per frame
    private void Awake()
    {
        if (oxygen_alter == null)
            oxygen_alter = new UnityEvent();
        if (hungry_alter == null)
            hungry_alter = new UnityEvent();
    }
    void Update()
    {

        float declineSpeed = 0.05f;
        
        if (transform.position.y < GroundHeight && status.oxygen >= status.min_oxygen)
        {
            status.oxygen -= Mathf.Abs(transform.position.y) * declineSpeed;
        }
        else
        {
            if(status.oxygen < status.max_oxygen)
                status.oxygen += Mathf.Abs(transform.position.y) * declineSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Digging = true;
        }
            oxygen_alter.Invoke();
        //if (Digging)
        //    Dig();
    }

    void FixedUpdate()
    {
        if (Digging)
        {
            Dig();
            Digging = false;
        }
    }
    public void SetDirection(float direction)
    {
        if (direction != 0)
        {
            Vector3 Direction = new Vector3(direction, 1, 1);
            transform.localScale = Direction;
            direction_alter.Invoke();
        }
    }
    public void Dig()
    {
        bool CheckDig = false; 
        for (int i = 0; i < HitPointDown.Length; i++)
        {
            Collider2D overCollider2d = Physics2D.OverlapCircle(HitPointDown[i].position, 0.1f, WhatisGround);
            if (overCollider2d != null)
            {
                overCollider2d.transform.GetComponent<Brick>().MakeDot(HitPointDown[i].position);
                CheckDig = true;
            }
        }
        if (CheckDig)
        {
            float hungrydeclineSpeed = 5;
            status.hungry -= hungrydeclineSpeed;
        }
        hungry_alter.Invoke();
        Debug.Log(status.hungry);
    }
}
