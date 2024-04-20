using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthSentry
{
    public class DistractionThrowable : MonoBehaviour
    {
        // Audio components and attraction
        [Tooltip("Audio Source to play an alarm from")]
        [SerializeField] private AudioSource AudioSource;
        [Tooltip("Audio clip to play when the alarm is triggered")]
        [SerializeField] private AudioClip AudioClip;
        [Tooltip("How close a guard will need to be to be alerted")]
        [SerializeField] private int AttractionRange;

        // Throwable variables
        [Tooltip("The max amount of this throwable")]
        [SerializeField] private int AmmoMax;
        [Tooltip("How strongly this can be thrown")]
        [SerializeField] private int Force;

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