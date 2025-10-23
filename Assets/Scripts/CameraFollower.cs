using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;                       
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float followSmooth = 10f;
    public bool rotateWithTarget = true;           

    void LateUpdate()
    {
        if (!target) return;

        
        Vector3 desiredPos = target.position + (rotateWithTarget ? target.rotation * offset : offset);
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSmooth * Time.deltaTime);

        
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.LookRotation(target.position - transform.position, Vector3.up),
            followSmooth * Time.deltaTime
        );
    }
}
