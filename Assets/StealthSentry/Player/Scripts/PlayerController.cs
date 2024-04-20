using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthSentry.Alarms;

namespace StealthSentry.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Tooltip("Player speed when running [SHIFT]")]
        [SerializeField] float RunSpeed = 5f;
        [Tooltip("Player speed when walking")]
        [SerializeField] float WalkSpeed = 3f;
        [Tooltip("Player speed when crouched [LCTRL]")]
        [SerializeField] float CrouchSpeed = 1.4f;

        public bool IsInStealth = false;
        public bool IsRunning = false;
        public bool IsMoving = false;

        [Tooltip("Horizontal camera sensitivity")]
        [SerializeField] float CameraSensitivityX = 180f;
        [Tooltip("Vertical camera sesitivity")]
        [SerializeField] float CameraSensitivityY = 180f;

        public Transform CameraOrientation;
        float CameraPitch = 0;
        float PlayerYaw = 0;


        // Local
        private bool isHidable = false;
        private static bool isHidden = false;
        Animator anim;
        Rigidbody rBody;
        CapsuleCollider Collider;
        [SerializeField] GameObject Body;

        public Camera playerCam;
        public Camera hidingCam;

        private GameObject pickup = null;


        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Get existing components
            anim = transform.GetComponentInChildren<Animator>();
            rBody = GetComponentInChildren<Rigidbody>();
            Collider = GetComponentInChildren<CapsuleCollider>();

            playerCam = GetComponentInChildren<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckIntereact();
            CheckStealth();

            // If true then the player is inside a hiding spot
            if (isHidden) return;

            Move();
            Rotate();
            if (!IsInStealth)
            {
                CheckRunning();
            }
        }

        #region Movement
        void CheckIntereact()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // If within range of a hiding spot
                if (isHidable)
                {
                    isHidden = !isHidden;
                    rBody.useGravity = !isHidden;
                    Collider.enabled = !isHidden;
                    Body.SetActive(!isHidden);
                    IsMoving = false;

                    if (hidingCam != null)
                    {
                        playerCam.enabled = !isHidden;
                        hidingCam.enabled = isHidden;
                    }
                }

                // If within range of a pickup
                if (pickup != null)
                {
                    pickup.GetComponent<PickupTrigger>().Collect();
                    pickup = null;
                }
            }
        }

        void Move()
        {
            float vMove = Input.GetAxis("Vertical");
            float hMove = Input.GetAxis("Horizontal");

            float MoveSpeed = WalkSpeed;
            if (IsInStealth)
            {
                MoveSpeed = CrouchSpeed;
            }
            else if (IsRunning)
            {
                MoveSpeed = RunSpeed;
            }

            // Move the player based on inputs handled prior
            Vector3 moveDirection = (Vector3.forward * vMove) + (Vector3.right * hMove);
            transform.Translate(moveDirection.normalized * MoveSpeed * Time.deltaTime, Space.Self);

            // If there is an animator component then pass it the correct data
            if (anim)
                anim.SetFloat("MoveSpeed", moveDirection.normalized.magnitude * MoveSpeed);

            // If moving in any axis then set false
            IsMoving = (vMove != 0) || (hMove != 0);
        }

        void Rotate()
        {
            // Get Mouse Input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * CameraSensitivityX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * CameraSensitivityY;

            PlayerYaw += mouseX;

            CameraPitch -= mouseY;
            CameraPitch = Mathf.Clamp(CameraPitch, -90f, 90f);


            // Rotate camera and player
            transform.Rotate(Vector3.up, mouseX);
            CameraOrientation.rotation = Quaternion.Euler(CameraPitch, PlayerYaw, 0);
        }

        void CheckStealth()
        {
            // On key down enter stealth
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                IsInStealth = true;
                CameraOrientation.Translate(0, -0.5f, 0);
            }
            // On key up exit stealth
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                IsInStealth = false;
                CameraOrientation.Translate(0, 0.5f, 0);
            }

            // If there is an animator component then pass it the correct data
            if (anim)
                anim.SetBool("IsInStealth", IsInStealth);
        }

        void CheckRunning()
        {
            // On key down start running
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                IsRunning = true;
            }
            // On key up Stop running
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                IsRunning = false;
            }

            // If there is an animator component then pass it the correct data
            if (anim)
                anim.SetBool("IsInStealth", IsRunning);
        }
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "HidingSpot")
            {
                isHidable = true;
                hidingCam = other.gameObject.GetComponent<Camera>();
            }
            else if (other.gameObject.tag == "Pickup")
            {
                pickup = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "HidingSpot")
            {
                isHidable = false;
                hidingCam = null;
            }
            else if (other.gameObject.tag == "Pickup")
            {
                pickup = null;
            }
        }
    }
}
