using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player2 : MonoBehaviour
{

    private Player2Control controls;
   [SerializeField] Rigidbody player;
    public bool finished = false;

    // wheel collider
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] float speed = 500f;
    [SerializeField] float maxTurnAngle = 15f;

    // wheel mesh
    [SerializeField] Transform frontRightWheelMesh;
    [SerializeField] Transform frontLeftWheelMesh;
    [SerializeField] Transform backRightWheelMesh;
    [SerializeField] Transform backLeftWheelMesh;

    float currentSpeed = 0;
    float currentTurnAngle = 0;

    private int laps = -1;

    void Awake()
    {
        controls = new Player2Control();
        player.GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    IEnumerator SpeedBoost()
    {
        speed = speed * 2;
        yield return new WaitForSeconds(5);
        speed = 700f;
    }

    void FixedUpdate()
    {
        currentSpeed = speed * controls.Player.Move.ReadValue<Vector2>().y;

        frontRight.motorTorque = currentSpeed;
        frontLeft.motorTorque = currentSpeed;

        currentTurnAngle = maxTurnAngle * controls.Player.Move.ReadValue<Vector2>().x;
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        SetWheel(frontRight, frontRightWheelMesh);
        SetWheel(frontLeft, frontLeftWheelMesh);
        SetWheel(backRight, backRightWheelMesh);
        SetWheel(backLeft, backLeftWheelMesh);
    }

    void SetWheel(WheelCollider wheelCol, Transform wheelMesh)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCol.GetWorldPose(out pos, out rot);

        wheelMesh.position = pos;
        wheelMesh.rotation = rot;
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name.StartsWith("Muffin"))
        {
            StartCoroutine(SpeedBoost());
            Destroy(col.gameObject);
        }

        if (col.gameObject == GameObject.Find("FinishLine"))
        {
            laps++;
            if (laps >= 1)
            {
                finished = true;
            }

        }
    }
}
