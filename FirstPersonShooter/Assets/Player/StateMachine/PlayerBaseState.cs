using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    //Other states will inherit from this state.

    //Dont need mono behavior since it is not used.

    //Abstract, will not instance this state but its children.
    //Provide a definition for abstract, think i have it in the inheritance lesson plan.

    //Player has two states. OnGround and InAir.
    //Create new scripts for the states.

    //Create Methods that will run in the State Manager's functions.
    //Functions will be defined in needed states.
    //Each function will have access to the state manager so that it can call it to switch states.
    public abstract void EnterState(PlayerStateMachine state_machine);

    public abstract void ExitState(PlayerStateMachine state_machine);

    public abstract void UpdateState(PlayerStateMachine state_machine);

    //State manager inherits from monobehavior so can just run Oncollision enter while calling the state's oncollision enter function.
    //public abstract void OnCollisionEnter(PlayerStateManager state_manager,  Collision collision);
}
