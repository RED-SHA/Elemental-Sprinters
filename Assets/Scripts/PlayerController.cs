using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    private FSM fsm;

    private void Awake()
    {
        AddFSMStates();
    }
    private void Update()
    {
        // Check for input to switch back to run state:
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (fsm.GetCurrentStateName() == PlayerEnum.Run)
            {
                fsm.ChangeState(PlayerEnum.Idle);
            }
            else
            {
                fsm.ChangeState(PlayerEnum.Run);
            }
        }
        fsm.Update();
        
    }

    private void AddFSMStates()
    {
        // Initialize the FSM with the following states:
        fsm = new FSM();
        fsm.AddState(PlayerEnum.Idle, Idle_Enter, Idle_Exit, Idle_Update);
        fsm.AddState(PlayerEnum.Run, Run_Enter, Run_Exit, Run_Update);
        fsm.AddState(PlayerEnum.Hit, Hit_Enter, Hit_Exit , Hit_Update);
        fsm.AddState(PlayerEnum.Roll_Front, Roll_Front_Enter, Roll_Front_Exit, Roll_Front_Update);
        fsm.AddState(PlayerEnum.Roll_Back, Roll_Back_Enter, Roll_Back_Exit, Roll_Back_Update);
        fsm.AddState(PlayerEnum.Block, Block_Enter, Block_Exit, Block_Update) ;
        fsm.AddState(PlayerEnum.Start, Start_Enter, Start_Exit , Start_Update);
        fsm.AddState(PlayerEnum.Buff, Buff_Enter, Buff_Exit, Buff_Update);
        fsm.AddState(PlayerEnum.Death, Death_Enter, Death_Exit, Death_Update);

        // Set the initial state to "Idle":
        fsm.SetInitialState(PlayerEnum.Idle);
    }


    #region FSM state functions:
    // FSM state functions:

    #region EnterPhase
    private void Idle_Enter()
    {
        animator.SetBool("bRun", false);
    }

    private void Run_Enter()
    {
        animator.SetBool("bRun", true);
    }

    private void Hit_Enter()
    {
        animator.SetTrigger("tHit");
    }

    private void Roll_Front_Enter()
    {
        animator.SetTrigger("tRollForward");
    }

    private void Roll_Back_Enter()
    {
        animator.SetTrigger("tRollBackWard");
    }

    private void Block_Enter()
    {
        animator.SetTrigger("tBlock");
        animator.SetBool("bBlcok", true);
    }

    private void Start_Enter()
    {
        animator.SetTrigger("tStart");
    }

    private void Buff_Enter()
    {
        animator.SetTrigger("tBuff");
        animator.SetBool("bSprint", true);
    }

    private void Death_Enter()
    {
        animator.SetTrigger("tDeath");
        animator.SetBool("bDeath", true);
    }
    #endregion

    #region UpdatePhase

    private void Idle_Update()
    {
    }
    private void Run_Update()
    {
    }
    private void Hit_Update()
    {
    }

    private void Roll_Front_Update()
    {
    }

    private void Roll_Back_Update()
    {
    }

    private void Block_Update()
    {
    }

    private void Start_Update()
    {
    }

    private void Buff_Update()
    {
    }

    private void Death_Update()
    {
    }
    #endregion

    #region ExitPhase

    private void Idle_Exit()
    {
    }
    private void Run_Exit()
    {
    }
    private void Hit_Exit()
    {
    }

    private void Roll_Front_Exit()
    {
    }

    private void Roll_Back_Exit()
    {
    }

    private void Block_Exit()
    {
    }

    private void Start_Exit()
    {
    }

    private void Buff_Exit()
    {
    }

    private void Death_Exit()
    {
    }

    #endregion


    #endregion


    #region triggering FSM transitions:
    public void StartIdle()
    {
        fsm.ChangeState(PlayerEnum.Idle);
    }
    public void StartRunning()
    {
        fsm.ChangeState(PlayerEnum.Run);
    }
    public void StartHit()
    {
        fsm.ChangeState(PlayerEnum.Hit);
    }
    public void StartRoll_Front()
    {
        fsm.ChangeState(PlayerEnum.Roll_Front);
    }
    public void StartRoll_Back()
    {
        fsm.ChangeState(PlayerEnum.Roll_Back);
    }
    public void StartBlocking()
    {
        fsm.ChangeState(PlayerEnum.Run);
    }
    public void StartStart()
    {
        fsm.ChangeState(PlayerEnum.Start);
    }
    public void StartBuff()
    {
        fsm.ChangeState(PlayerEnum.Buff);
    }

    public void StartDeath()
    {
        fsm.ChangeState(PlayerEnum.Death);
    }

    #endregion
}
