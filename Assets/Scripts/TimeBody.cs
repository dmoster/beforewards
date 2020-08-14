﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.InputSystem;

public class TimeBody : MonoBehaviour
{
    public PlayerMovement playerMovement;
    PlayerControls controls;

    bool isRewinding = false;

    public float recordTime = 5f;

    List<PointInTime> pointsInTime;
    Rigidbody2D rb;

    private void Awake()
    {
        controls = playerMovement.controls;

        controls.Gameplay.Rewind.performed += ctx => StartRewind();
        controls.Gameplay.Rewind.canceled += ctx => StopRewind();
    }

    // Start is called before the first frame update
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody2D>();
    }
/*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
            StartRewind();
        if (Input.GetButtonUp("Fire2"))
            StopRewind();
    }*/

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }

    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation)); ;
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;
    }
}
