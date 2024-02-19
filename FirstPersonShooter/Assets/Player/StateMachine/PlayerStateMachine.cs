using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerStateMachine : MonoBehaviour
{
    //State Variables
    private PlayerBaseState current_state;
    public PlayerGroundState ground_state = new PlayerGroundState();
    public PlayerAirState air_state = new PlayerAirState();

    //Player Variables
    [HideInInspector] public CharacterController character_controller;
    [HideInInspector] public Vector2 move_input = Vector2.zero;
    [HideInInspector] public Vector3 player_velocity = Vector3.zero;

    [HideInInspector] public bool jump_button_pressed = false;

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }

    public void GetJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) jump_button_pressed = true;
        if (context.phase == InputActionPhase.Canceled) jump_button_pressed = false;
    }

    void Start()
    {
        //Get Components
        character_controller = GetComponent<CharacterController>();

        //Set the current state.
        current_state = ground_state;

        //When player is first instantiated, it will call the enter state method.
        current_state.EnterState(this);
    }

    void Update()
    {
        current_state.UpdateState(this);
    }

    //Example to show of collision.
    //Since this script inherits from monobehvior it has all the functionality.
    //Just need to run the currents states method, pass in the reference to the manager and the collision.
    /*private void OnCollisionEnter(Collision collision)
    {
        current_state.OnCollisionEnter(this, collision);
    }*/

    public void SwitchState(PlayerBaseState cur_state, PlayerBaseState new_state)
    {
        cur_state.ExitState(this);
        current_state = new_state;
        current_state.EnterState(this);
    }

    public void Set_Velocity(float max_speed, float acceleration, float friction_multiplier)
    {
        //Accelerate the player.
        player_velocity += (transform.right * move_input.x + transform.forward * move_input.y) * acceleration;
        Vector2 xz_velocity = new Vector2(player_velocity.x, player_velocity.z);
        xz_velocity = Vector2.ClampMagnitude(xz_velocity, max_speed);

        //Friction
        if (move_input == Vector2.zero)
        {
            //Will use air resistance for in_air state.
            xz_velocity *= friction_multiplier;
        }

        //Reconstruct Player Velocity
        player_velocity = new Vector3(xz_velocity.x, player_velocity.y, xz_velocity.y);
    }

    public void Move()
    {
        //Move player
        character_controller.Move(player_velocity * Time.deltaTime);
    }

}
