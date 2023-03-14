using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] Status status;

    [SerializeField] public float GroundHeight = 0;

    public UnityEvent oxygen_alter;
    public UnityEvent direction_alter;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    // Update is called once per frame
    private void Awake()
    {
        if (oxygen_alter == null)
            oxygen_alter = new UnityEvent();
    }
    void Update()
    {

        float declineSpeed = 0.05f;
        if(transform.position.y < GroundHeight)
        {
            status.oxygen -= Mathf.Abs(transform.position.y) * declineSpeed;
        }
        else
        {
            if(status.oxygen < status.max_oxygen)
                status.oxygen += Mathf.Abs(transform.position.y) * declineSpeed;
        }

        oxygen_alter.Invoke();

        Debug.Log(status.oxygen);
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
}
