using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 250f;
    private float rotationSpeed = 2f;
    private float waitTime = 5f;
    private float actionTime = 30f;
    private bool started = false;
    
    private bool waiting = false;
    private float startTime;
    public int counter = 0;
    private float lastRotationY = 0f;

    private bool rotatingRight = false;
    private bool rotatingLeft = false;

    public bool goBack = false;

    private bool paused = false;

    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stopPlayer();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !started)
        {
            started = true;
            moveForwardPlayer();
        }

        if(started)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (paused)
                {
                    Time.timeScale = 1;
                    paused = false;
                }
                else
                {
                    Time.timeScale = 0;
                    paused = true;
                }
            }

            if (rotatingRight && !paused)
            {
                Debug.Log("RIGHT");
                rotatePlayerRight();
            }
            else if (rotatingLeft && !paused)
            {
                Debug.Log("LEFT");
                rotatePlayerLeft();
            }
            else if (counter == 6 && !paused)
            {
                stopPlayer();
            }
            else if (!rotatingLeft && !rotatingRight && goBack && !paused)
            {
                moveBackPlayer();
            }
            else if(!rotatingLeft && !rotatingRight && counter != 5 && !paused)
            {
                moveForwardPlayer();
            }
            
        }
    }

    private void rotatePlayerLeft()
    {
        Quaternion target = Quaternion.Euler(0, transform.rotation.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * rotationSpeed);
    }

    private void rotatePlayerRight()
    {
        Quaternion target = Quaternion.Euler(0, transform.rotation.y + 90, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * rotationSpeed);
    }

    private void stopPlayer()
    {
        rb.velocity = Vector3.zero;
    }

    private void moveForwardPlayer()
    {
        Debug.Log("XXX");
        if (transform.rotation.y >= 0.7f)
        {
            Debug.Log("XX1:");
            rb.velocity = new Vector3(speed, 0f, 0f);
        }
        else if (transform.rotation.y >= 0f && transform.rotation.y < 0.7f)
        {
            Debug.Log("XX2:");
            rb.velocity = new Vector3(0f, 0f, speed);
        }
    }

    private void moveBackPlayer()
    {
        if (transform.rotation.y >= 0.7f)
        {
            Debug.Log("XB1:");
            rb.velocity = new Vector3(-1 * speed, 0f, 0f);
        }
        else if (transform.rotation.y >= 0f && transform.rotation.y < 0.7f)
        {
            Debug.Log("XB2:" + transform.rotation.y);
            rb.velocity = new Vector3(0f, 0f, -1 * speed);
        }
    }

    private IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);

        rotatingLeft = false;
        rotatingRight = false;

        if (counter == 5) goBack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision: " + counter);
        if(counter == 0)
        {
            stopPlayer();
            rotatingRight = true;
            StartCoroutine("wait");
 
        }
        else if(counter == 1)
        {
            stopPlayer();
            rotatingLeft = true;
            StartCoroutine("wait");

        }
        else if (counter == 2)
        {
            stopPlayer();
            rotatingRight = true;
            StartCoroutine("wait");
        }
        else if (counter == 3)
        {
            stopPlayer();
            rotatingLeft = true;
            StartCoroutine("wait");
        }
        else if (counter == 4)
        {
            stopPlayer();
            StartCoroutine("wait");
        }
        else if (counter == 5)
        {
            stopPlayer();
        }

        counter++;
    }
}
