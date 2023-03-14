using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] protected float _oxygen = 100;
    [SerializeField] protected float _max_oxygen = 100;
    public float oxygen { get { return _oxygen; } set { _oxygen = value; } }
    public float max_oxygen { get { return _max_oxygen; } set { _max_oxygen = value; } }
}
