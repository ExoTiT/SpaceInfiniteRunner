using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipControl : MonoBehaviour
{
    #region Exposed

    [Header("Moving variables")]
    [SerializeField]
    private float translationSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float dash;

    [Header("lateral wall position")]
    [SerializeField]
    GameObject leftWall;
    [SerializeField]
    GameObject rightWall;

    [Header("The player object")]
    [SerializeField]
    GameObject Ship;

    [Header("Script")]
    [SerializeField]
    GameManager gameManager;

    [Header("Timer values")]
    [SerializeField]
    private float _outlineTimeDelay;

    [Header("Cinemachine Camera")]
    [SerializeField]
    private CinemachineVirtualCamera _vCam;
    [SerializeField]
    [Range(0.05f,0.2f)]
    private float _distanceTolerance;
   
    #endregion

    #region Private and Protected

    //Object and component
    private Rigidbody _rigidbody;
    private ParticleSystem FlameG;
    private ParticleSystem FlameD;
    private ParticleSystem Explode;
    private ParticleSystem JumpBurst;


    //Int & float
    private float currentSpeed = 0;
    private float _horizontal;

    //bool
    private bool bAccelerate;
    private bool inLife;
    private bool isGrounded;
    private bool isJumping;

    //Animation death
    private int deathRotationX;
    private int deathRotationY;
    private int deathRotationZ;
    private float deathPositionY = 1.5f;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _outline = GetComponentInChildren<Outline>();
        _outline.enabled = false;

        _cineTransposer = _vCam.GetCinemachineComponent<CinemachineTransposer>();
    }

    void Start()
    {
        FlameG = GameObject.Find("FlameG").GetComponent<ParticleSystem>();
        FlameD = GameObject.Find("FlameD").GetComponent<ParticleSystem>();
        Explode = GameObject.Find("Explode").GetComponent<ParticleSystem>();
        JumpBurst = GameObject.Find("JumpParticule").GetComponent<ParticleSystem>();
    }


    void Update()
    {
      
        
        if (inLife)
        {
            
            //Create a visual groundChecker
            Debug.DrawLine(transform.position + new Vector3(0, 0.3f, 0), transform.position + new Vector3(0, -0.01f, 0), Color.blue);

            //Ground check
            if(Physics.Linecast(transform.position + new Vector3(0, 0.3f, 0), transform.position + new Vector3(0, -0.01f, 0)))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }

            //Jump
            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                isJumping = true;
                JumpBurst.Play();
            }

            //Déplacement horizontaux
            _horizontal = Input.GetAxisRaw("Horizontal") * translationSpeed * Time.deltaTime;
            

            //Dash latérale
            if (Input.GetButtonDown("LeftDash"))
            {
                Debug.Log("LEFT!!");
                if (transform.position.x > leftWall.transform.position.x)
                {
                    transform.position = new Vector3(transform.position.x - dash, transform.position.y, transform.position.z);
                }
            }
            if (transform.position.x < leftWall.transform.position.x) transform.position = new Vector3(leftWall.transform.position.x + 1.5f, transform.position.y, transform.position.z);

            if (Input.GetButtonDown("RightDash"))
            {
                Debug.Log("RIGHT!!");
                if (transform.position.x < rightWall.transform.position.x)
                {
                    transform.position = new Vector3(transform.position.x + dash, transform.position.y, transform.position.z);
                    
                }
            }
            if (transform.position.x > rightWall.transform.position.x) transform.position = new Vector3(rightWall.transform.position.x - 1.5f, transform.position.y, transform.position.z);
            //Accélération
            if (gameManager.isInGame)
            {
                if (!bAccelerate)
                {
                    bAccelerate = true;
                    ChangeFlame(FlameG, bAccelerate);
                    ChangeFlame(FlameD, bAccelerate);
                }
                transform.position += transform.forward * currentSpeed;
            }
        }

        if (!inLife)
        {
            Vector3 cameraPos = Camera.main.transform.position - Ship.transform.position;
            Debug.Log("Position de la camera : " + Vector3.Distance(cameraPos, _cineTransposer.m_FollowOffset));
            if (Vector3.Distance(cameraPos, _cineTransposer.m_FollowOffset) < _distanceTolerance)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    inLife = true;
                }
            }
        }

        //Disabled outline
        if (Time.timeSinceLevelLoad > _endTimeToOutline)
        {
            _outline.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.isInGame && inLife)
        {
            //Application de la vélocité pour les déplacement latéraux
            _rigidbody.velocity += new Vector3(_horizontal * Time.fixedDeltaTime, 0, 0);

            //Accelération
            _rigidbody.velocity += new Vector3(0, 0, speed * Time.fixedDeltaTime);

            //Jump
            if (isJumping)
            {
                _rigidbody.velocity += new Vector3(0, jumpForce * Time.fixedDeltaTime, 0);
                isJumping = false;
            }
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {

            gameManager.LostHealth -= 10;
            if (gameManager.Health <= 0 && inLife)
            {
                Death();
            }

            //Outline enabled and delay for disabled
            if (inLife)
            {
                _outline.enabled = true;
                _endTimeToOutline = Time.timeSinceLevelLoad + _outlineTimeDelay;
            }
        }
    }

    #endregion

    #region Method

    void ChangeFlame(ParticleSystem system, bool bAccelerate)
    {
        if (bAccelerate && inLife)
        {
            var main = system.main;
            main.startSpeed = 2;
            main.startSize = 1.5f;
        }
        else
        {
            var main = system.main;
            main.startSpeed = 1;
            main.startSize = 1;
        }
    }

    public void OnStartGame()
    {
        enabled = true;
        transform.position = new Vector3(0, 0.2f, -10);
        Ship.transform.position = transform.position;
        Ship.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void DamagedShip()
    {
        deathRotationX = Random.Range(-45, 45);
        deathRotationY = Random.Range(-45, 45);
        deathRotationZ = Random.Range(140, 215);
        Ship.transform.rotation = new Quaternion(deathRotationX, deathRotationY, deathRotationZ, 0);
        Ship.transform.position = new Vector3(transform.position.x, deathPositionY, transform.position.z);
    }

    public void Death()
    {
        inLife = false;
        currentSpeed = 0;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Invoke("DamagedShip", 0.2f);
        Explode.Play();
    }

    public void OnGameOver()
    {
        inLife = false;
        enabled = false;
    }

    #endregion

    #region Private & Protected

    private Outline _outline;

    private float _endTimeToOutline;

    private CinemachineTransposer _cineTransposer;
    #endregion

}
