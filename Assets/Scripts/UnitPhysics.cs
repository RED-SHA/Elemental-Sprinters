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
        unitStatus.SetUnitStatus(2f, 0f, "Unit", 0f, "", false, 1, 1f);
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (unitStatus.IsRunable)
        {
            rigidbody.velocity = new Vector3(unitStatus.velocity, 0f, 0f);
        }
    }
}

