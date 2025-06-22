using System.Globalization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public bool DidHitPaddleLast = true;

    [SerializeField] float _launchForce = 5.0f;

    [SerializeField] float _paddleInfluence = 0.4f;

    [SerializeField] float _ballSpeedIncrement = 1.1f;

    float GetNonZeroRandomFloat(float min = -1.0f, float max = 1.0f)
    {
        float num;
        do
        {
            num = Random.Range(min, max);
        } while (Mathf.Approximately(num, 0.0f));
        {
            return num;
        }
    }

    private Rigidbody2D _rb;

    void ResetBall()
    {
        _rb.linearVelocity = Vector2.zero;
        transform.position = new Vector3(-3.06f, 1.62f, 0.0f);
        Vector2 Direction = new Vector2(
            GetNonZeroRandomFloat(),
            GetNonZeroRandomFloat()
        ).normalized;

        _rb.AddForce(Direction * _launchForce, ForceMode2D.Impulse);

    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        ResetBall();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (!Mathf.Approximately(collision.rigidbody.linearVelocityX, 0.0f))
            {
                Vector2 direction = _rb.linearVelocity * (1 - _paddleInfluence)
                                    + collision.rigidbody.linearVelocity * _paddleInfluence;

                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized * _ballSpeedIncrement;
            }
            _rb.linearVelocity *= _ballSpeedIncrement;
            DidHitPaddleLast = true;
        }

        else if (collision.gameObject.CompareTag("Border"))
        {
            _rb.linearVelocity *= _ballSpeedIncrement;
        }

        else if (collision.gameObject.CompareTag("Brick"))
        {
            _rb.linearVelocity *= _ballSpeedIncrement;
            DidHitPaddleLast = false;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LoseArea"))
        {
            GameBehavior.Instance.YouLose();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    
    
}
