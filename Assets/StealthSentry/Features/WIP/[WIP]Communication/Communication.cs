using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthSentry
{
    public class Communication : MonoBehaviour
    {
        [Tooltip("Should this actor be able to comminicate regardless of distance?")]
        [SerializeField] private bool IsGlobal;
        [Tooltip("How close should this actor be to another to exchange information")]
        [SerializeField] private int CommRange;
        [Tooltip("Set a dedicated site to spread information")]
        [SerializeField] private GameObject CommSite;

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