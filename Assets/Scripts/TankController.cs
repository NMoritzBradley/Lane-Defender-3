using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

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

    // Start is called before the first frame update
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

    public void OnDestroy() //When the level is reloaded, everything in it is automatically destroyed
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        fire.started -= Fire_started;
        fire.canceled -= Fire_canceled;
        restart.performed -= Restart_performed;
    }


    private void Move_canceled(InputAction.CallbackContext obj)
    {
        //there was an exception here, but it was unecessary so it was deleted
        isTankMoving = false;
    }

    private void Move_started(InputAction.CallbackContext obj) //This function is automatically made when move.started is written above
    {
        isTankMoving = true;
    }

    private void Fire_started(InputAction.CallbackContext obj)
    {
        isTankFiring = true;
        Explosion.gameObject.SetActive(true);
    }

    private void Fire_canceled(InputAction.CallbackContext obj)
    {
        isTankFiring = false;
        Explosion.gameObject.SetActive(false);
    }

    private void Restart_performed(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0); 
    }

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

    // Update is called once per frame
    void Update()
    {
        if (isTankMoving)
        {
            moveDirection = move.ReadValue<float>(); //makes it move left or right
        }

        /*if (EndGameText.text != "") //to turn off the movement ability when the game ends. It's probably not the most efficient way, but it functions
        {
            move.started -= Move_started;
            move.canceled -= Move_canceled;
        }*/
    }

    public void UpdateScore()
    {
        Points += 1;
        PointsText.text = Points.ToString(); //Sometimes you might have to use Score.ToString()
    }

    private IEnumerator FireAgain()
    {
        yield return new WaitForSeconds(.5f);
        canFire = true;
    }

    void CreateBullet()
    {
        transform.position = new Vector3(Tank.GetComponent<Rigidbody2D>().position.x +1, Tank.GetComponent<Rigidbody2D>().position.y);

        Instantiate(Bullet, (transform.position), Quaternion.identity);
    }

}
