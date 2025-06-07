using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public Vector2 speed = new Vector2(5.0f, 5.0f);
    public float LimitYMax = 4.51f;
    public float LimitYMin = -4.72f;
    public float LimitXMax = 1.16f;
    public float LimitXMin = -7.47f;

    public float BoundryProtection = 0.1f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed.x *= Random.value < 0.5 ? -1 : 1;
        speed.y *= Random.value < 0.5 ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPosition = transform.position + (Vector3)speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, min: LimitXMin, max: LimitXMax);
        newPosition.y = Mathf.Clamp(newPosition.y, min: LimitYMin, max: LimitYMax);

        if ((newPosition.x == LimitXMax) || (newPosition.x == LimitXMin))
        {
            speed.x *= -1.0f;
             newPosition.x = Mathf.Clamp(newPosition.x + (speed.x > 0 ? BoundryProtection : -1 * BoundryProtection),
             min: LimitXMin,
             max: LimitXMax);
        }

        if ((newPosition.y == LimitYMax) || (newPosition.y == LimitYMin))
        {
            speed.y *= -1.0f;
            newPosition.y = Mathf.Clamp(newPosition.y + (speed.x > 0 ? BoundryProtection : -1 * BoundryProtection),
             min: LimitYMin,
             max: LimitYMax);
        }

        transform.position = newPosition;
        
    }
}
