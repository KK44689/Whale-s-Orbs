using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // declare player's speed
    private float m_speed = 10f;

    public float speed
    {
        get
        {
            return m_speed;
        }
        set
        {
            if (value < 0)
            {
                Debug.LogError("You can't set negative player's speed !");
            }
            else
            {
                m_speed = value;
            }
        }
    }

    // rigidbody variables
    Rigidbody playerRb;

    // player's input variables
    private float horizontalInput;

    private float verticalInput;

    // private Vector3 direction;
    private Vector3 facingDirection;

    // player directions
    private static Quaternion forwardDirection = Quaternion.Euler(-90, 0, 0);

    private static Quaternion rightDirection = Quaternion.Euler(-90, 90, 0);

    private static Quaternion leftDirection = Quaternion.Euler(-90, -90, 0);

    private static Quaternion backDirection = Quaternion.Euler(-90, 0, 180);

    // enemy
    private GameObject enemyPrefab;

    // enemy force
    public float collideForce = 5f;

    // check collide with enemy
    private bool enemyCollided = false;

    // player exp
    private int m_maxExp = 5;

    public int maxExp
    {
        get
        {
            return m_maxExp;
        }
        set
        {
            if (value < 0)
            {
                Debug.LogError("You can't set negative player's max EXP !");
            }
            else
            {
                m_maxExp = value;
            }
        }
    }

    private int m_exp;

    public int exp
    {
        get
        {
            return m_exp;
        }
        set
        {
            if (value < 0)
            {
                Debug.LogError("You can't set negative player's EXP !");
            }
            else
            {
                m_exp = value;
            }
        }
    }

    public ExpBar expBar;

    // player hp
    private int m_maxHp = 5;

    public int maxHp
    {
        get
        {
            return m_maxHp;
        }
        set
        {
            if (value < 0)
            {
                Debug.LogError("You can't set negative player's max HP !");
            }
            else
            {
                m_maxHp = value;
            }
        }
    }

    private int m_hp = 5;

    public int hp
    {
        get
        {
            return m_hp;
        }
        set
        {
            if (value < 0)
            {
                Debug.LogError("You can't set negative player's HP !");
            }
            else
            {
                m_hp = value;
            }
        }
    }

    // public Animator fullHelthAlertText;
    public HpBar hpBar;

    //player attack
    private bool isAttacked;

    // player boundary
    private float boundaryX = 22;

    private float boundaryZ = 12;

    // gameover
    public bool isGameActive { get; private set; }

    public GameObject GameOverScreen;

    // animation
    public Animator playerAnim;

    // sound
    private AudioSource playerAudio;

    public AudioClip orbsSoundFX;

    public AudioClip redOrbsSoundFX;

    public AudioClip shipAttackSoundFX;

    public AudioClip shipDestroyedSoundFX;

    public AudioClip coralDestroySoundFX;

    // Start is called before the first frame update
    void Start()
    {
        // get player's rigidbody
        playerRb = GetComponent<Rigidbody>();

        // set start exp
        expBar.SetMaxExp (m_maxExp);
        m_exp = 0;

        isAttacked = false;

        // set start hp
        hpBar.SetMaxHp (m_maxHp);
        m_hp = 5;

        // set gameactive bool
        isGameActive = true;

        // gameover
        GameOverScreen.SetActive(false);

        // get audiosource
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGameActive)
        {
            PlayerMove();
            // PlayerAttack();
        }
    }

    void Update()
    {
        PlayerAttack();
        CheckGameOver();
        if (isGameActive)
        {
            CheckExp();
            PlayerBoundary();
        }
        if (!isGameActive)
        {
            GameOverScreen.SetActive(true);
        }
    }

    void PlayerBoundary()
    {
        if (transform.position.x > boundaryX)
        {
            transform.position =
                new Vector3(boundaryX,
                    transform.position.y,
                    transform.position.z);
        }
        if (transform.position.z > boundaryZ)
        {
            transform.position =
                new Vector3(transform.position.x,
                    transform.position.y,
                    boundaryZ);
        }
        if (transform.position.x < -boundaryX)
        {
            transform.position =
                new Vector3(-boundaryX,
                    transform.position.y,
                    transform.position.z);
        }
        if (transform.position.z < -boundaryZ)
        {
            transform.position =
                new Vector3(transform.position.x,
                    transform.position.y,
                    -boundaryZ);
        }
    }

    void PlayerMove()
    {
        // get player's input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // transform.Translate(direction * speed * Time.deltaTime);
        // set direction left / right
        if (horizontalInput > 0)
        {
            transform.rotation = rightDirection;
            facingDirection = Vector3.right;

            // // player move
            transform
                .Translate(Vector3.down *
                Time.deltaTime *
                m_speed *
                horizontalInput);
        }
        else if (horizontalInput < 0)
        {
            // direction = new Vector3(-1, 0, 0);
            transform.rotation = leftDirection;
            facingDirection = Vector3.left;

            // // player move
            transform
                .Translate(Vector3.up *
                Time.deltaTime *
                m_speed *
                horizontalInput);
        } // set direction forward/backward
        else if (verticalInput > 0)
        {
            // direction = new Vector3(0, 1, 0);
            transform.rotation = forwardDirection;
            facingDirection = Vector3.forward;

            // // player move
            transform
                .Translate(Vector3.down *
                Time.deltaTime *
                m_speed *
                verticalInput);
        }
        else if (verticalInput < 0)
        {
            // direction = new Vector3(0, -1, -0);
            transform.rotation = backDirection;
            facingDirection = Vector3.back;

            // // player move
            transform
                .Translate(Vector3.up *
                Time.deltaTime *
                m_speed *
                verticalInput);
        }

        // check collide with enemy
        // if (enemyCollided)
        // {
        //     Debug.Log("enemy");
        //     enemyPrefab = GameObject.FindWithTag("Enemy");
        //     Vector3 awayFromEnemy =
        //         (transform.position - enemyPrefab.transform.position)
        //             .normalized;
        //     playerRb.AddForce(awayFromEnemy * collideForce, ForceMode.Impulse);
        //     enemyCollided = false;
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        // fullHelthAlertText.SetTrigger("Idle");
        if (other.gameObject.CompareTag("Orb"))
        {
            playerAudio.PlayOneShot (orbsSoundFX);
            Destroy(other.gameObject);
            m_exp++;
            expBar.SetExp (m_exp);
        }
        if (other.gameObject.CompareTag("RedOrb"))
        {
            playerAudio.PlayOneShot (redOrbsSoundFX);
            Destroy(other.gameObject);
            m_exp += 2;
            expBar.SetExp (m_exp);
            // player already have full health
            // else
            // {
            //     Destroy(other.gameObject);
            //     fullHelthAlertText.SetTrigger("fullHealth");
            // }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!isAttacked)
            {
                playerAudio.PlayOneShot (shipAttackSoundFX);
                m_exp--;
                expBar.SetExp (m_exp);
                m_hp--;
                hpBar.SetHp (m_hp);
                // enemyPrefab = GameObject.FindWithTag("Enemy");
                // Vector3 awayFromEnemy =
                //     (enemyPrefab.transform.position - transform.position)
                //         .normalized;
                // playerRb
                //     .AddForce(awayFromEnemy * collideForce, ForceMode.Impulse);
            }
            else if (isAttacked && m_maxExp >= 35)
            {
                playerAudio.PlayOneShot (shipDestroyedSoundFX);
                Destroy(other.gameObject);
                Debug.Log("player attack ship");
            }
        }
        if (other.gameObject.CompareTag("Coral"))
        {
            if (isAttacked && m_maxExp >= 20)
            {
                playerAudio.PlayOneShot (coralDestroySoundFX);
                Destroy(other.gameObject);
                Debug.Log("player attack coral");
            }
        }
    }

    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttacked = true;
            playerAnim.SetTrigger("Attack");
            playerRb.AddForce(facingDirection * 3 * m_speed, ForceMode.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isAttacked = false;
            playerRb.velocity = Vector3.zero;
            playerAnim.SetTrigger("Swim1");
        }
    }

    void CheckExp()
    {
        if (m_exp < 0)
        {
            m_exp = 0;
        }
    }

    void CheckGameOver()
    {
        if (m_hp == 0)
        {
            isGameActive = false;
            playerAnim.SetBool("Dead", true);
        }
    }
}
