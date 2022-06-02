using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private bool isMoveable;
    private bool isMovingForward;
    private bool isMovingBack;
    private bool isTurningRight;
    private bool isTurningLeft;

    private CharacterController _charController;
    private Vector3 v_movement;
    private Vector3 v_velocity;
    private float moveSpeed;

    [SerializeField] private TMP_Text number;
    [SerializeField] private TMP_Text info;

    private bool generate = true;

    Rigidbody rb;

    private void Start()
    {
        isMoveable = true;
        //rb = GetComponent<Rigidbody>();
        _charController = GetComponent<CharacterController>();
        moveSpeed = 250;
        v_velocity.y = 0f;
    }

    private void Update()
    {
        if (generate && isMoveable) StartCoroutine("generateRandom");

        if (isMoveable && Input.GetKey(KeyCode.UpArrow))
        {
            isMovingForward = true;
            isMovingBack = false;
            isTurningRight = false;
            isTurningLeft = false;
            StartCoroutine("wait");
        }
        else if (isMoveable && Input.GetKey(KeyCode.DownArrow))
        {
            isMovingForward = false;
            isMovingBack = true;
            isTurningRight = false;
            isTurningLeft = false;
            StartCoroutine("wait");
        }
        else if (isMoveable && Input.GetKey(KeyCode.RightArrow))
        {
            isMovingForward = false;
            isMovingBack = false;
            isTurningRight = true;
            isTurningLeft = false;
            StartCoroutine("wait");
        }
        else if (isMoveable && Input.GetKey(KeyCode.LeftArrow))
        {
            isMovingForward = false;
            isMovingBack = false;
            isTurningRight = false;
            isTurningLeft = true;
            StartCoroutine("wait");
        }
    }

    void FixedUpdate()
    {       
        if (isMovingForward)
        {
            //transform.position += new Vector3(0f, 0f, speed);
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
            v_movement = _charController.transform.forward;
            _charController.Move(v_movement * moveSpeed * Time.deltaTime);
            _charController.Move(v_velocity);
        }
        else if (isMovingBack)
        {
            //transform.position -= new Vector3(0f, 0f, speed);
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -speed);
            v_movement = -1 * _charController.transform.forward;
            _charController.Move(v_movement * moveSpeed * Time.deltaTime);
            _charController.Move(v_velocity);
            _charController.Move(v_velocity);
        }
        else if (isTurningRight)
        {
            //Quaternion goal = Quaternion.AngleAxis(1f, new Vector3(0f, rotationSpeed, 0f));
            //transform.rotation = goal * transform.rotation;
            _charController.transform.Rotate(Vector3.up * 50 * Time.deltaTime);
            _charController.Move(v_velocity);
        }
        else if (isTurningLeft)
        {
            //transform.Rotate(new Vector3(0f, -rotationSpeed, 0f) * Time.deltaTime);
            _charController.transform.Rotate(-1 * Vector3.up * 50 * Time.deltaTime);
            _charController.Move(v_velocity);
        }

        //v_movement = _charController.transform.forward;
        //_charController.transform.Rotate(Vector3.up * 100 * Time.deltaTime);

        //_charController.Move(v_movement * moveSpeed * Time.deltaTime);

    }

    private float getRandom()
    {
        float rnd = Random.Range(-10.0f, 10.0f);
        return rnd;
    }

    private IEnumerator wait()
    {
        isMoveable = false;
        yield return new WaitForSeconds(0.35f);
        isMoveable = true;
    }

    private IEnumerator generateRandom()
    {
        generate = false;

        float rndForward = Random.Range(0.0f, 32.0f);
        float rndBack = Random.Range(0.0f, 400.0f);
        float rndRight = Random.Range(0.0f, 100.0f);
        float rndLeft = Random.Range(0.0f, 50.0f);

        float absoluteForward = Mathf.Abs(rndForward - 4.69921875f);
        float absoluteBack = Mathf.Abs(rndBack - 163.015625f);
        float absoluteRight = Mathf.Abs(rndRight - 70.9375f);
        float absoluteLeft = Mathf.Abs(rndLeft - 21.4921875f);

        float minimum = Mathf.Min(Mathf.Min(absoluteForward, absoluteBack), Mathf.Min(absoluteRight, absoluteLeft));

        if (minimum == absoluteForward)
        {
            number.text = "EEG VALUE: " + rndForward;
            info.text = "MOVING FORWARD";
            isMovingForward = true;
            isMovingBack = false;
            isTurningRight = false;
            isTurningLeft = false;
        }
        else if (minimum == absoluteBack)
        {
            number.text = "EEG VALUE: " + rndBack;
            info.text = "MOVING BACK";
            isMovingForward = false;
            isMovingBack = true;
            isTurningRight = false;
            isTurningLeft = false;
        }
        else if (minimum == absoluteRight)
        {
            number.text = "EEG VALUE: " + rndRight;
            info.text = "TURNING RIGHT";
            isMovingForward = false;
            isMovingBack = false;
            isTurningRight = true;
            isTurningLeft = false;
        }
        else if (minimum == absoluteLeft)
        {
            number.text = "EEG VALUE: " + rndLeft;
            info.text = "TURNING LEFT";
            isMovingForward = false;
            isMovingBack = false;
            isTurningRight = false;
            isTurningLeft = true;
        }

        yield return new WaitForSeconds(2.5f);
        generate = true;

    }
}
