using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParallax : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody2D rb;
    private GameManager _gm;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-_gm.parallaxSpeed * Time.deltaTime * 250f * speed, 0);
    }
}
