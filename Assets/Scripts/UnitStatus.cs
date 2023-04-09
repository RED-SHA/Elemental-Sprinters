using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatus : MonoBehaviour
{
    public bool IsRunable { get; private set; }
    private bool IsBuff;
    private float maxVelocity;
    public float velocity { get; private set; }
    private string name;
    private float buffRemainTime;
    private string preInputValue;
    private float endurance;
    private float strength;

    public void Runable() => IsRunable = true;
    public void Unrunable() => IsRunable = false;
    public void ResetVelocity() => velocity = 0;
    public void BuffOn() => IsBuff = true;
    public void BuffOff() => IsBuff = false;

    public void SetUnitStatus(float maxVelocity, float velocity, string name, float buffRemainTime, string preInputValue, bool IsRunable, float endurance ,float strength, bool IsBuff = false)
    {
        this.maxVelocity = maxVelocity;
        this.velocity = velocity;
        this.name = name;
        this.buffRemainTime = buffRemainTime;
        this.preInputValue = preInputValue;
        this.IsRunable = IsRunable;
        this.endurance = endurance;
        this.strength = strength;
        this.IsBuff = false;
    }

    private void Update()
    {
        if (IsRunable)
        {
            if (IsBuff)
            {
                velocity += Time.deltaTime * strength;
                return;
            }

            if (velocity < maxVelocity)
            {
                velocity += Time.deltaTime * strength;
            }
            else if (velocity > maxVelocity)
            {
                velocity -= Time.deltaTime * 1f / endurance;
            }
        }
        else
        {
            velocity = 0f;
        }
    }

    public void DecreaseVelocity(int value)
    {
        if (velocity - value <= 0)
        {
            velocity = 0f;
        }

        velocity -= value;
    }

}
