using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthSentry.Alarms
{
    public class HitboxTrigger : MonoBehaviour
    {
        // Local variables
        Alarm alarm;

        void Start()
        {
            alarm = GetComponent<Alarm>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                alarm.TriggerAlarm();
            }
        }
    }
}
