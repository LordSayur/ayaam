﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (AwangBro))]
public class AwangBroUserController : MonoBehaviour
{
    // config variables
    [SerializeField] PlayerNumber playerNumber;

    // reference variables
    private AwangBro m_Character;
    private Transform m_Cam;

    // state variables
    private Vector3 m_CamForward;
    private Vector3 m_Move;
    private bool m_Jump;

    // constant
    public enum PlayerNumber
    {
        One = 0,
        Two,
        Three,
        Four
    }

    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<AwangBro>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            switch (playerNumber)
            {
                case PlayerNumber.One:
                    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump1");
                    if (CrossPlatformInputManager.GetButtonDown("Jump1") && m_Character.canJump)
                    {
                        m_Character.Jump();
                    }
                    break;
                case PlayerNumber.Two:
                    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump2");
                    if (CrossPlatformInputManager.GetButtonDown("Jump2") && m_Character.canJump)
                    {
                        m_Character.Jump();
                    }
                    break;
                default:
                    break;
            }
        }
        if (CrossPlatformInputManager.GetButtonDown("Dance"))
        {
            m_Character.Dance();
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h;
        float v;

        switch (playerNumber)
        {
            case PlayerNumber.One:
                h = CrossPlatformInputManager.GetAxis("Horizontal1");
                v = CrossPlatformInputManager.GetAxis("Vertical1");
                break;
            case PlayerNumber.Two:
                h = CrossPlatformInputManager.GetAxis("Horizontal2");
                v = CrossPlatformInputManager.GetAxis("Vertical2");
                break;
            default:
                h = 0;
                v = 0;
                break;
        }
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v*m_CamForward + h*m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v*Vector3.forward + h*Vector3.right;
        }
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;
    }
}