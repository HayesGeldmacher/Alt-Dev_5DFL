using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //the below variables check wether the player is touching the ground or not!
    [Header("Grounded Variables")]
    private bool _grounded;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundDistance;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _gravity;

    [Header("Walking Variables")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _walkSpeed;


    //The below region just creates a reference of this specific controller that we can call from other scripts quickly
    #region Singleton

    public static PlayerController instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of playercontroller present!! NOT GOOD!");
            return;
        }

        instance = this;
    }

    #endregion


    private void Update()
    {
        //This line checks if the player is touching the ground, or in the air
        _grounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        //takes the raw player input to move character 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Stores that input in a variable to be used later in function
        Vector3 _move = transform.right * x + transform.forward * z;

       //Constantly adding a downward force to the player so they fall when not standing on something
        _velocity.y += _gravity * Time.deltaTime;

       //controller.move is how the character actually moves - always multiply by Time.deltaTime so physics work correctly!
        _controller.Move(_move * _walkSpeed * Time.deltaTime);
        _controller.Move(_velocity * Time.deltaTime);

    }
}
