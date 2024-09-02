using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TankController : MonoBehaviour
{
    Rigidbody2D rB2D;
    public PlayerInput PlayerInput;
    private InputAction move;
    private InputAction fire;
    private InputAction restart;

    public GameObject Tank;
    public GameObject Bullet;
    public GameObject Explosion;
    private bool isTankMoving;
    private bool isTankFiring;
    private bool canFire;
    public float Speed = 10;
    private float moveDirection;
    public TMP_Text PointsText;
    public TMP_Text EndGameText;
    public TMP_Text RestartText;
    public TMP_Text LaunchText;
    public int Points;

    /// <summary>
    /// Enables the ActionMap and all inputs, sets isTankMoving to false and canFire to true
    /// </summary>
    void Start()
    {

        PlayerInput.currentActionMap.Enable(); //has to be enabled, or nothing else will work
        move = PlayerInput.currentActionMap.FindAction("Move");
        fire = PlayerInput.currentActionMap.FindAction("Fire");
        restart = PlayerInput.currentActionMap.FindAction("Restart");

        move.started += Move_started;  
        move.canceled += Move_canceled; 
        fire.started += Fire_started;
        fire.canceled += Fire_canceled;
        restart.performed += Restart_performed;
        isTankMoving = false;
        canFire = true;

    }

    /// <summary>
    /// Turns off inputs when the tank is Destroyed
    /// </summary>
    public void OnDestroy() //When the level is reloaded, everything in it is automatically destroyed
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        fire.started -= Fire_started;
        fire.canceled -= Fire_canceled;
        restart.performed -= Restart_performed;
    }

    /// <summary>
    /// Sets isTankMoving to false when the key is released
    /// </summary>
    /// <param name="obj"></param>

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isTankMoving = false;
    }

    /// <summary>
    /// Sets isTankMoving to true when the key is pressed
    /// </summary>
    /// <param name="obj"></param>
    private void Move_started(InputAction.CallbackContext obj) 
    {
        isTankMoving = true;
    }

    /// <summary>
    /// Sets isTankFiring to true and turns on the Explosion object when the key is pressed
    /// </summary>
    /// <param name="obj"></param>
    private void Fire_started(InputAction.CallbackContext obj)
    {
        isTankFiring = true;
        Explosion.gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets isTankFiring to false and turns off the Explosion object when the key is pressed
    /// </summary>
    /// <param name="obj"></param>
    private void Fire_canceled(InputAction.CallbackContext obj)
    {
        isTankFiring = false;
        Explosion.gameObject.SetActive(false);
    }

    /// <summary>
    /// Restarts the scene when the R key is hit
    /// </summary>
    /// <param name="obj"></param>
    private void Restart_performed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0); 
    }

    /// <summary>
    /// When isTankMoving is true, moves the tank in the appropriate direction. When false, stops the tank
    /// When isTankFiring and canFire are both true, Invokes CreateBullet, sets canFire to false, and calls 
    /// the FireAgain Coroutine
    /// </summary>
    private void FixedUpdate() //modified update, is more consistent
    {
        if (isTankMoving)
        {
            Tank.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Speed * moveDirection); 
        }
        else
        {
            Tank.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (isTankFiring && canFire)
        {
            Invoke("CreateBullet", .1f);
            canFire = false;
            StartCoroutine(FireAgain());
        }
    }

    /// <summary>
    /// If isTankMoving = true, finds the moveDirection
    /// </summary>
    void Update()
    {
        if (isTankMoving)
        {
            moveDirection = move.ReadValue<float>(); //makes it move left or right
        }
    }

    /// <summary>
    /// After .5 seconds, sets canFire to true
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireAgain()
    {
        yield return new WaitForSeconds(.5f);
        canFire = true;
    }

    /// <summary>
    /// Finds the position of the Tank, then Instantiates a bullet slightly to the right of that point
    /// </summary>
    void CreateBullet()
    {
        transform.position = new Vector3(Tank.GetComponent<Rigidbody2D>().position.x +1, Tank.GetComponent<Rigidbody2D>().position.y);

        Instantiate(Bullet, (transform.position), Quaternion.identity);
    }

}
