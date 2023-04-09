using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitPhysics : MonoBehaviour
{
    [SerializeField]private UnitStatus unitStatus;

    private Rigidbody rigidbody;

    private void Start()
    {
        // Initialize the UnitStatus object
        unitStatus.SetUnitStatus(2f, 0f, "Unit", 5f, "", false, 1, 1f);
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (unitStatus.IsRunable)
        {
            rigidbody.velocity = new Vector3(unitStatus.velocity, 0f, 0f);
        }
    }

    public void StartRun() => unitStatus.Runable();
    public void StopRun() => unitStatus.Unrunable();
    public void DecreaseSpeed(int decreaseValue = 5)
    {
        unitStatus.DecreaseVelocity(decreaseValue);
    }
    public void StartBuff()
    {
        unitStatus.BuffOn();
    }
    public void StopBuff()
    {
        unitStatus.BuffOff();
    }
    public float SprintTime()
    {
        return unitStatus.buffRemainTime;
    }
}

