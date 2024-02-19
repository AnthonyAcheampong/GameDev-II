using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    //Ground Variables
    public float ground_speed_max = 10f;
    public float ground_acceleration = 5f;
    public float ground_friction_multiplier = 0.9f;

    public float jump_power = 10f;

    public override void EnterState(PlayerStateMachine state_machine)
    {
        //Test to see if enter state is working.
        //Debug.Log("Ground State Enter");
    }

    public override void ExitState(PlayerStateMachine state_machine)
    {
        //Test to see if exit state is working.
        //Debug.Log("Ground State Exit");
    }

    public override void UpdateState(PlayerStateMachine state_machine)
    {
        //Debug.Log("Ground State Update");
        state_machine.Set_Velocity(ground_speed_max, ground_acceleration, ground_friction_multiplier);
        state_machine.Move();

        //Switch States
        if (!state_machine.character_controller.isGrounded) state_machine.SwitchState(this, state_machine.air_state);
        if (state_machine.jump_button_pressed)
        {
            state_machine.player_velocity.y = jump_power;
            state_machine.SwitchState(this, state_machine.air_state);
        }
    }
}