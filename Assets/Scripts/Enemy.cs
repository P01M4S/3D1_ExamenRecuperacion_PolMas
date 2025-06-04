using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int direction = 1;
    public float speed = 2.5f;
    public Animator _animator;
    public Rigidbody2D _rigidBody;
    public BoxCollider2D _boxCollider;
    public GameManager _gameManager;
    public List<GameObject> enemisOnScrean;
   
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(direction * speed, _rigidBody.velocity.y);
    }
void OnBecameVisible()
    {
        direction = 1;
        _gameManager.enemisOnScrean.Add(gameObject);
    }


     void OnBecameInvisible()
    {
        direction = 0;
        _gameManager.enemisOnScrean.Remove(gameObject);
    }


    public void Death()
    {
        direction = 0;
       
        _animator.SetTrigger("Dead");
        _boxCollider.enabled = false;
        Destroy(gameObject, 0.5f);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == 3)
        {
            direction *= -1;
        }


        if(collision.gameObject.CompareTag("Player"))
        {
           
            PlayerControler playerScript = collision.gameObject.GetComponent<PlayerControler>();
            playerScript.Death();
        }
    }
}
