using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 1.2f;
    public float gravity = -20f;
    public Transform cameraTransform;   

    CharacterController cc;
    float verticalVel;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (!cameraTransform && Camera.main) cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        
        float h = Input.GetAxisRaw("Horizontal");  // A/D
        float v = Input.GetAxisRaw("Vertical");    // W/S
        Vector3 input = new Vector3(h, 0f, v);
        input = Vector3.ClampMagnitude(input, 1f);

        Vector3 fwd = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector3 move = (fwd * input.z + right * input.x) * moveSpeed;

        if (cc.isGrounded && verticalVel < 0f) verticalVel = -2f;
        if (cc.isGrounded && Input.GetKeyDown(KeyCode.Space))
            verticalVel = Mathf.Sqrt(2f * jumpHeight * -gravity);

        verticalVel += gravity * Time.deltaTime;

     
        Vector3 velocity = move + Vector3.up * verticalVel;
        cc.Move(velocity * Time.deltaTime);

        Vector3 flat = new Vector3(move.x, 0f, move.z);
        if (flat.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(flat), 12f * Time.deltaTime);
    }
}
