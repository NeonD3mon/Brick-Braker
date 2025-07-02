using UnityEngine;
using UnityEngine.UIElements;

public class BrickBehavior : MonoBehaviour
{

    private Rigidbody2D _rb;
    int BrickLife;
    //[SerializeField] AudioSource _source;
    
    
    [SerializeField] AudioClip _scoreClip;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (BrickLife == 0)
            {
                GetComponent<AudioSource>().PlayOneShot(_scoreClip);
                Debug.Log("Playing _scoreClip");
                    GameBehavior.Instance.ScorePoint();
                    Destroy(gameObject, 0.2f);
                
               

            }

            else
                BrickLife -= 1;
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                spriteRenderer.color = Utilities.Colors[BrickLife];

             Debug.Log(BrickLife);
            // gameObject represents the GameObject that has this script.


        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        //_source = GetComponent<AudioSource>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int index = Random.Range(0, GameBehavior.BrickLevel);
        spriteRenderer.color = Utilities.Colors[index];
        BrickLife = index;
        Debug.Log(BrickLife);
    }

    

    // Update is called once per frame
    void Update()
{
    
}
}
