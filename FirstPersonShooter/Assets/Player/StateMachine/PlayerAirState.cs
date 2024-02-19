using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public float air_speed_max = 10f;
    public float air_acceleration = 5f;
    public float air_friction_multiplier = 0.9f;
    public float gravity = -0.2f;

    public override void EnterState(PlayerStateMachine state_machine)
    {
        //Debug.Log("Air State Enter");
    }

    public override void ExitState(PlayerStateMachine state_machine)
    {
        //Debug.Log("Air State Exit");
        state_machine.player_velocity.y = -2; //-2 so the palyer can detect that it is on the ground.
    }

    public override void UpdateState(PlayerStateMachine state_machine)
    {
        //Debug.Log("Air State Update");

        //Set Velocity
        state_machine.Set_Velocity(air_speed_max, air_acceleration, air_friction_multiplier);

        //Gravity
        state_machine.player_velocity.y += gravity;

        //Move player
        state_machine.Move();

        //Switch States
        if (state_machine.character_controller.isGrounded) state_machine.SwitchState(this, state_machine.ground_state);
    }
}