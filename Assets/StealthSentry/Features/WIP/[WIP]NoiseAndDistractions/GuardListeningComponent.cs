using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthSentry
{
    public class GuardListeningComponent : MonoBehaviour
    {
        // Guard variables
        [Tooltip("How close should a sound be to investigate")]
        [SerializeField] private int ListenRange;
        [Tooltip("How closely should this guard investigate sounds. Scale of 1-100")]
        [SerializeField] private int SearchIntesnsity;
        [Tooltip("Does this guard increase in suspicion with repeated distractions")]
        [SerializeField] private bool IncreaseSuspicion;

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