using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthSentry.Alarms
{
    public class PickupTrigger : MonoBehaviour
    {
        [Tooltip("Set this to cause the intereacted with item to disappear with use.")]
        [SerializeField] GameObject VisualComponent;

        // Local Variables
        Alarm Alarm;
        bool Collected = false;


        void Start()
        {
            Alarm = GetComponent<Alarm>();
        }

        public void Collect()
        {
            if (Collected) return;

            Collected = true;

            if (VisualComponent)
            {
                VisualComponent.SetActive(false);
            }

            Alarm.TriggerAlarm();
        }
    }
}
