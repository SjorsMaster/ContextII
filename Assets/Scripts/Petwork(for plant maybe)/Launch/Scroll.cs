using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used for the moving background in the start and shop
/// upon further inspection I could've used the animator instead if anything.
/// </summary>
[ExecuteAlways]
public class Scroll : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform startT;

    private void Start()
    {
        startT = transform;
    }
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position,new Vector2(startT.position.x * Mathf.Sin(Time.time), startT.position.y * Mathf.Sin(Time.time)), speed);
        transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(startT.localScale.x + Mathf.Sin(Time.time)/100, startT.localScale.y + Mathf.Sin(Time.time) /100), speed/4);
    }
}
