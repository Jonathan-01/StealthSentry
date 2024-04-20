using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthSentry
{
    public class PatrolRoute : MonoBehaviour
    {
        private enum PatrolType
        {
            RoundRobin,
            ReverseOnFinish
        }

        [Tooltip("List of nodes in the order you want to visit them")]
        [SerializeField] private List<GameObject> PatrolList;
        [Tooltip("The pattern you want to follow the nodes in once you reach the end of listed nodes")]
        [SerializeField] private PatrolType PatrolPattern;

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