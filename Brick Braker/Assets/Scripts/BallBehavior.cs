using System.Globalization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public bool DidHitPaddleLast = true;
    private Vector2 _prevBallVelocity;
    private bool _isPaused = false;

    [SerializeField] float _launchForce = 5.0f;

    [SerializeField] float _paddleInfluence = 0.4f;

    public static float _ballSpeedIncrement = 1.001f;

    [SerializeField] AudioClip _wallHitClip;
    [SerializeField] AudioClip _paddleHitClip;
    [SerializeField] AudioClip _brickHitClip;
    private AudioSource _source;


    private Rigidbody2D _rb;

    void ResetBall()
    {
        _rb.linearVelocity = Vector2.zero;
        transform.position = new Vector3(-3.06f, 1.62f, 0.0f);
        Vector2 Direction = new Vector2(
            Utilities.GetNonZeroRandomFloat(-0.2f, 0.2f),
            Utilities.GetNonZeroRandomFloat()
        ).normalized;

        _rb.AddForce(Direction * _launchForce, ForceMode2D.Impulse);

    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();

        ResetBall();

    }

    void PlaySound(AudioClip clip, float pitchMin = 0.8f, float pitchmax = 1.2f)
    {
        _source.clip = clip;
        _source.pitch = Random.Range(pitchMin, pitchmax);
        _source.Play();
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
           
            DidHitPaddleLast = true;
            _rb.linearVelocity *= _ballSpeedIncrement;
            PlaySound(_paddleHitClip);
        }

        else if (collision.gameObject.CompareTag("Border"))
        {
            PlaySound(_wallHitClip);

        }

        else if (collision.gameObject.CompareTag("Brick"))
        {
            
            DidHitPaddleLast = false;
            PlaySound(_brickHitClip);
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
        if (GameBehavior.Instance.CurrentState == Utilities.GameState.Pause)
        {
            if (!_isPaused)
            {
                _isPaused = true;
                _prevBallVelocity = _rb.linearVelocity;
                _rb.linearVelocity = Vector2.zero;
            }

        }
        else
        {
            if (_isPaused)
                _rb.linearVelocity = _prevBallVelocity;
            _isPaused = false;
        }

    } 
}
    
    
    

