using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    //Debug
    public TMP_Text debug_text;
    
    
    //camera variables
    public Camera cam;
    private Vector2 look_input = Vector2.zero;
    private float look_speed = 60;
    private float horizontal_look_angle = 0f;
    public bool invert_x = false;
    public bool invert_y = false;
    private int invert_factor_x = 1;
    private int invert_factor_y = 1;
    [Range(0.01f,1f)] public float sensitivity;

    //player Input
    private Vector2 move_input;
    private bool grounded;

    //Movement Vavriables
    private CharacterController character_controller;
    private Vector3 player_velocity;
    private Vector3 wish_dir = Vector3.zero;
    public float max_speed = 6;
    public float acceleration = 60;
    public float gravity = 20;
    public float stop_speed = 0.5f;
    public float jump_impulse = 10f;
    public float friction = 4;



    // Start is called before the first frame update
    void Start()
    {
        //Hide Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Invert Camera
        if (invert_x) invert_factor_x = -1;
        if (invert_y) invert_factor_y = -1;


        //Get Components
        character_controller = GetComponent<CharacterController>();



    }

    // Update is called once per frame
    void Update()
    {
        Look(); 
    }

    public void GetLookInput(InputAction.CallbackContext context)
    {
        look_input = context.ReadValue<Vector2>();
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        Jump();
    }


    private void Look()
    {
        //Left/Right
        transform.Rotate(Vector3.up, look_input.x * look_speed * Time.deltaTime * invert_factor_x * sensitivity);


        //Up/Down
        float angle = look_input.y * look_speed * Time.deltaTime * invert_factor_y * sensitivity;
        horizontal_look_angle -= angle;
        horizontal_look_angle = Mathf.Clamp(horizontal_look_angle, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(horizontal_look_angle, 0, 0);
    }

    private void Jump()
    {
        if(grounded)
        {
            //Do THIS LATER...
        }
    }

    private Vector3 Accelerate(Vector3 wish_dir, Vector3 current_velocity, float max_speed)
    {
        //Project current_velocity on the wish-dir
        float proj_speed = Vector3.Dot(current_velocity, wish_dir);
        float accel_speed = accel * Time.deltaTime;

        if(proj_speed + accel_speed > max_speed) 
            accel_speed = max_speed - proj_speed;

        
        return current_velocity + (wish_dir * accel_speed);
    }

    private Vector3 MoveGround(Vector3 wish_dir, Vector3 current_velocity)
    {
        Vector3 new_velocity = new Vector3(current_velocity.x, 0, current_velocity.z);
        float speed = new_velocity.magnitude;
        if(speed <= stop_speed)
        {
            new_velocity = Vector3.zero;
            speed = 0;
        }
        if(speed != 0)
        {
            float drop = speed * friction * Time.deltaTime;
            new_velocity *= Mathf.Max(speed - drop, 0) / speed;

        }
        new_velocity = new Vector3(new_velocity.x, current_velocity.y, new_velocity.z);


        return Accelerate(wish_dir, new_velocity, acceleration, max_speed);
    }


}
