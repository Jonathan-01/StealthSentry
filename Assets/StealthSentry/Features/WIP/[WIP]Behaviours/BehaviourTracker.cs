using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthSentry
{
    public class BehaviourTracker : MonoBehaviour
    {
        private enum GuardState
        {
            Patrolling,
            Cautious,
            Searching,
            Combative
        }

        [Tooltip("The behaviour graph this guard will follow")]
        [SerializeField] private ScriptableObject Behaviour;
        [Tooltip("The current state of the guard")]
        [SerializeField] private GuardState State;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}