using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthSentry
{
    public class DistractionStatic : MonoBehaviour
    {
        // Audio components and attraction
        [Tooltip("Audio Source to play an alarm from")]
        [SerializeField] private AudioSource AudioSource;
        [Tooltip("Audio clip to play when the alarm is triggered")]
        [SerializeField] private AudioClip AudioClip;
        [Tooltip("Delay before the sound distraction is triggered")]
        [SerializeField] private int Delay;
        [Tooltip("How close a guard will need to be to be alerted")]
        [SerializeField] private int AttractionRange;

        // Interaction
        [Tooltip("Can it be triggered by throwing something at it")]
        [SerializeField] private bool ByThrowable;
        [Tooltip("Can it be triggered by shooting at it")]
        [SerializeField] private bool ByShot;
        [Tooltip("Can it be triggered interacting directly with it")]
        [SerializeField] private bool ByInteraction;

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