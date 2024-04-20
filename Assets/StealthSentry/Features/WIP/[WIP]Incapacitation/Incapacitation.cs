using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incapacitation : MonoBehaviour
{
    // Lethal variables
    [Tooltip("Can the Guard be lethaly incapacitated")]
    [SerializeField] private bool IsLethalable;
    [Tooltip("How much damage is required to incapacitate them")]
    [SerializeField] private int Health;

    // Non-lethal Variables
    [Tooltip("Can the Guard be non-lethaly incapacitated")]
    [SerializeField] private bool IsNonLethalable;
    [Tooltip("Can the guard be woken by other guards")]
    [SerializeField] private bool IsWakeable;
    [Tooltip("How long should it be until they wake up themselves. Leave as 0 for an indefinite period")]
    [SerializeField] private int TimeUntilWakeSelf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
