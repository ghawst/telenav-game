using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    float yaw; 
    float pitch;
    public float minPitch;
    public float maxPitch;
    public float coSpeed;
    public float rotationSpeed;
    public Transform player;
    public Vector3 defaultCameraOffset;
    Vector3 cameraOffset;
    Vector3 targetCameraOffset;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //acumulam deplasamentul mouselui la unghiurile facute de sys coord local cu axele lumii:
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        //limitam rotatia sus-jos a.i. sa nu se dea peste cap:
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        //aplicam rotatia:
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        targetCameraOffset = defaultCameraOffset;
        cameraOffset = Vector3.Lerp(cameraOffset, targetCameraOffset, Time.deltaTime * coSpeed);
        //trecem deplasamentul camerei relativ la personaj din spatiul local in spatiul lume
        Vector3 worldSpaceOffset = transform.TransformDirection(cameraOffset);
        //calculam pozitia camerei:
        transform.position = player.position + worldSpaceOffset;
    }
}