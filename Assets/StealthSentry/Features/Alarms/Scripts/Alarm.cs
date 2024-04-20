using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthSentry.Alarms {
    using Enemies;

    public class Alarm : MonoBehaviour
    {
        // Audio components and attraction
        [Tooltip("Audio Source to play an alarm from")]
        [SerializeField] private AudioSource AudioSource;
        [Tooltip("Audio clip to play when the alarm is triggered")]
        [SerializeField] private AudioClip AudioClip;
        [Tooltip("How close a guard will need to be to be alerted")]
        [SerializeField] private int AttractionRange = 30;
        [Tooltip("How long the alarm will last in seconds. Leave as 0 to have it continue indefinitely")]
        [SerializeField] private float Duration = 10;

        /*
        // Disarmable options
        [Tooltip("Can the alarm be disarmed")]
        [SerializeField] private bool Disarmable;
        [Tooltip("Is it disarmed remotely. Leave empty for it to be disarmable directly")]
        [SerializeField] private GameObject DisarmedFrom;
        */

        // Local
        private float AlarmTimer;
        private bool Triggered;

        // Update is called once per frame
        void Update()
        {
            // If the alarm has been triggered
            if (Triggered)
            {
                // Update timer
                AlarmTimer += Time.deltaTime;

                // If the alarm has continued for the total duraion
                if (AlarmTimer > Duration && Duration != 0)
                {
                    StopAlarm();
                }
                else
                {
                    AlertEneimes();
                }
            } 
        }

        public void TriggerAlarm()
        {
            // Reset the timer
            AlarmTimer = 0;

            // If an audio component is attached
            if (AudioSource)
            {
                AudioSource.clip = AudioClip;
                AudioSource.Play();
            }

            Triggered = true;
        }

        private void StopAlarm()
        {
            // If an audio component is attached
            if (AudioSource)
                AudioSource.Stop();

            Triggered = false;
        }

        private void AlertEneimes()
        {
            // Find all enemies
            List<GameObject> enemies = new List<GameObject>();
            GameObject.FindGameObjectsWithTag("Enemy", enemies);

            foreach (GameObject enemy in enemies)
            {
                // If there is an audio component then attract based on distance to that
                if (AudioSource)
                {
                    if (Vector3.Distance(AudioSource.transform.position, enemy.transform.position) < AttractionRange)
                        {
                            enemy.GetComponentInChildren<EnemyController>().AlertEnemy(transform.position);
                        }
                }
                // Otherwise attract based on distance to this object
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < AttractionRange)
                        {
                            enemy.GetComponentInChildren<EnemyController>().AlertEnemy(transform.position);
                        }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Draw a wireframe sphere around the object that attracts the enemies
            Gizmos.color = Color.yellow;
            if (AudioSource)
            {
                Gizmos.DrawWireSphere(AudioSource.transform.position, AttractionRange);

                // Draw a line to the attached audio component
                Gizmos.color = Color.grey;
                Gizmos.DrawLine(transform.position, AudioSource.transform.position);
            }
            else
            {
                Gizmos.DrawWireSphere(transform.position, AttractionRange);
            }
        }
    }
}