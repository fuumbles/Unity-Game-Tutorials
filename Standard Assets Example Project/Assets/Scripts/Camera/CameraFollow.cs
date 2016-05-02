using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;// the player
    public float smoothing = 5f;//lag for the camera movement - smooth movement
    Vector3 offset;

    // Use this for initialization
    void Start()
    {
        //assign the offset to the position of the camera  - the position of the camera
        offset = transform.position - target.position;
    }


    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
