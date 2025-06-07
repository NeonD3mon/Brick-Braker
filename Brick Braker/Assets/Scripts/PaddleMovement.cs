using UnityEngine;

public class PaddleMovement : MonoBehaviour
{

    public float Speed = 5.0f;

    public float LimitXLeft = -6.56f;
    public float LimitXRight = 0.2f;

    public KeyCode LeftDirection;
    public KeyCode RightDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movement = 0.0f;

        if (Input.GetKey(LeftDirection))
        {
            movement -= Speed;
        }

        if (Input.GetKey(RightDirection))
        {
            movement += Speed;
        }

        Vector3 newPosition = transform.position + new Vector3(movement, 0.0f, 0.0f) * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, min: LimitXLeft, max: LimitXRight);

        transform.position = newPosition;
    }
}
