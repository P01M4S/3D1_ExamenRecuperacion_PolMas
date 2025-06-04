using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variable para la velocidad de movimiento
    public float playerSpeed = 5.5f;
    //Variable para la fuerza del salto
    public float jumpForce = 3f;

    //Variable para acceder al SpriteRenderer
    private SpriteRenderer spriteRenderer;
    //Variable para acceder al RigidBody2D
    private Rigidbody2D rBody;
    //Variable para acceder al GroundSensor
    public int direction = 1;
    private float inputHorizontal;
    public GroundSensor groundSensor;
    public Animator _animator;
    public AudioSource _audio;
    public AudioClip jumpSXF;
    public AudioClip deathSFX;
    public BoxCollider2D _boxCollider;
    public GameManager _gameManager;

    //Variable para almacenar el input de movimiento
    float horizontal;

    GameManager gameManager;

    void Awake()
    {
        //Asignamos la variables del SpriteRender con el componente que tiene este objeto
        spriteRenderer = GetComponent<SpriteRenderer>();
        //Asignamos la variable del Rigidbody2D con el componente que tiene este objeto
        rBody = GetComponent<Rigidbody2D>();
        //Buscamos un Objeto por su nombre, cojemos el Componente GroundSensor de este objeto y lo asignamos a la variable
        groundSensor = GameObject.Find("GroundSensor").GetComponent<GroundSensor>();
        //Buscamos el objeto del GameManager y SFXManager lo asignamos a las variables
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();     
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver == true)
        {
            return;
        }    
        
        horizontal = Input.GetAxis("Horizontal");

        if(horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if(horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if(Input.GetButtonDown("Jump") && groundSensor.isGrounded)
        {
            rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }        

        Movement();


        _animator.SetBool("IsJump", !groundSensor.isGrounded);
    }

    void FixedUpdate()
    {
        rBody.velocity = new Vector2(horizontal * playerSpeed, rBody.velocity.y);
    }

    public void Die()
    {
        gameManager.GameOver();
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Coin")
        {
            gameManager.AddCoin();
            Destroy(collider.gameObject);
        }
    }

    void Movement()
    {
        if(inputHorizontal > 0)
        {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _animator.SetBool("IsRunning", true);
        }
        else if(inputHorizontal < 0)
        {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        _animator.SetBool("IsRunning", true);
        }


        else if (inputHorizontal == 0)
        {
            _animator.SetBool("IsRunning", false);
        }
    }

    void Jump()
    {
        rBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);


        _animator.SetBool("IsJump", true);

    }


    public void Death()
    {
        _animator.SetTrigger("IsDead");


        _boxCollider.enabled = false;
        Destroy(groundSensor.gameObject);


        rBody.AddForce(Vector2.up * jumpForce / 4, ForceMode2D.Impulse);


        inputHorizontal = 0;
        rBody.velocity = Vector2.zero;
        _gameManager.isPlaying = false;


        Destroy(gameObject, 5);
    }



}
