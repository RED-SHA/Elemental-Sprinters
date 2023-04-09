using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public Animator animator;
    public float BuffDuration;
    public static PlayerEnum crnEnum;
    public  PlayerEnum PrevEnum;
    public UnitCamController Cam;
    public UnitStatus Status;
    [SerializeField] private UnitPhysics unitPhysics;

/*    public delegate void EnterDelegate();
    public delegate void UpdateDelegate();
    public delegate void ExitDelegate();

    List<EnterDelegate> EnterDel = new();
    List<UpdateDelegate> UpdateDel = new();
    List<ExitDelegate> ExitDel = new();*/

    private FSM fsm;

    private void Awake()
    {
        fsm = new();
        AddFSMStates();
    }
    private void Update()
    {
        fsm.Update();
        //print(crnEnum);
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
        fsm.AddState(PlayerEnum.Sprint, Sprint_Enter, Sprint_Exit, Sprint_Update);
        fsm.AddState(PlayerEnum.Death, Death_Enter, Death_Exit, Death_Update);

        // Set the initial state to "Idle":
        fsm.SetInitialState(PlayerEnum.Idle);
    }

    #region FSM state functions:
    // FSM state functions:

    #region EnterPhase
    private void Idle_Enter()
    {
        unitPhysics.StopRun();
        animator.SetBool("bRun", false);
    }

    private void Run_Enter()
    {
        unitPhysics.StartRun();
        animator.SetBool("bRun", true);
    }

    private void Hit_Enter()
    {
        unitPhysics.DecreaseSpeed();
        animator.SetTrigger("tHit");
    }

    private void Roll_Front_Enter()
    {
        unitPhysics.DecreaseSpeed();
        animator.SetTrigger("tRollForward");
    }

    private void Roll_Back_Enter()
    {
        unitPhysics.StopRun();
        unitPhysics.DecreaseSpeed();
        animator.SetTrigger("tRollBackward");
    }

    private void Block_Enter()
    {
        unitPhysics.DecreaseSpeed();
        animator.SetTrigger("tBlock");
        animator.SetBool("bBlock", true);
    }

    private void Start_Enter()
    {
        animator.SetTrigger("tStart");
        animator.SetBool("bRun", true);
    }

    private void Buff_Enter()
    {
        unitPhysics.StopRun();
        animator.SetTrigger("tBuff");
        unitPhysics.StartBuff();
    }
    private void Sprint_Enter()
    {
        animator.SetBool("bSprint", true);
        BuffDuration = unitPhysics.SprintTime();
        unitPhysics.StartRun();
    }
    private void Death_Enter()
    {
        unitPhysics.StopRun();
        animator.SetTrigger("tDeath");
        animator.SetBool("bDeath", true);
    }
    #endregion

    #region UpdatePhase

    private void Idle_Update()
    {
        //print("Im Idle!");
    }
    private void Run_Update()
    {
    }
    private void Hit_Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            StartState(PrevEnum);
        }
    }

    private void Roll_Front_Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            StartState(PrevEnum);
        }
    }

    private void Roll_Back_Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            StartState(PrevEnum);
        }
    }

    private void Block_Update()
    {
    }

    private void Start_Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            StartState(PlayerEnum.Run);
        }
    }

    private void Buff_Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            StartState(PlayerEnum.Sprint);
        }
    }
    private void Sprint_Update()
    {
        if (BuffDuration > 0)
        {
            BuffDuration -= Time.deltaTime;
        }
        else
        {
            StartState(PlayerEnum.Run);
        }
    }
    private void Death_Update()
    {
    }
    #endregion

    #region ExitPhase

    private void Idle_Exit()
    {
        PrevEnum = PlayerEnum.Idle;
    }
    private void Run_Exit()
    {
        PrevEnum = PlayerEnum.Run;
        unitPhysics.StopRun();
        animator.SetBool("bRun", false);
    }
    private void Hit_Exit()
    {
    }

    private void Roll_Front_Exit()
    {
    }

    private void Roll_Back_Exit()
    {
        unitPhysics.StartRun();
    }

    private void Block_Exit()
    {
        animator.SetBool("bBlock", false);
    }

    private void Start_Exit()
    {
        unitPhysics.StartRun();
    }

    private void Buff_Exit()
    {
        animator.SetBool("bSprint", true);
    }
    private void Sprint_Exit()
    {
        unitPhysics.StopBuff();
        BuffDuration = 0;
        animator.SetBool("bSprint", false);
    }
    private void Death_Exit()
    {
        animator.SetBool("bDeath", false);
    }

    #endregion

    #endregion


    #region FSM Triggers :

    public void StartState(PlayerEnum state)
    {
        switch (state)
        {
            case PlayerEnum.Idle:
                StartIdle();
                break;
            case PlayerEnum.Run:
                StartRunning();
                break;
            case PlayerEnum.Hit:
                StartHit();
                break;
            case PlayerEnum.Roll_Front:
                StartRoll_Front();
                break;
            case PlayerEnum.Roll_Back:
                StartRoll_Back();
                break;
            case PlayerEnum.Block:
                StartBlocking();
                break;
            case PlayerEnum.Start:
                StartStart();
                break;
            case PlayerEnum.Buff:
                StartBuff();
                break;
            case PlayerEnum.Sprint:
                StartSprint();
                break;
            case PlayerEnum.Death:
                StartDeath();
                break;
            default:
                break;
        }

    }

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
        fsm.ChangeState(PlayerEnum.Block);
    }
    public void StartStart()
    {
        fsm.ChangeState(PlayerEnum.Start);
    }
    public void StartBuff()
    {
        fsm.ChangeState(PlayerEnum.Buff);
    }
    public void StartSprint()
    {
        fsm.ChangeState(PlayerEnum.Sprint);
    }
    public void StartDeath()
    {
        fsm.ChangeState(PlayerEnum.Death);
    }

    #endregion
}
