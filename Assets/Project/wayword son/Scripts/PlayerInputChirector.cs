using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInputChirector : MonoBehaviour {


    private ChirectorCtrl_WWS m_Character;    // A reference to the Character on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;
	private bool m_Aim;                        // the world-relative desired move direction, calculated from the camForward and user input.



	// Use this for initialization
	void Start () {
		// get the transform of the main camera
		m_Move = new Vector3();
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
        m_Character = GetComponent<ChirectorCtrl_WWS>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!m_Jump)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}
		//if (!m_Aim)
		//{
			m_Aim = CrossPlatformInputManager.GetButton("AimTrigger");
		//}
	}

    private void FixedUpdate()
    {
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");

		// calculate move direction to pass to character
		if (!m_Aim)
		{
			if (m_Cam != null)
			{
				// calculate camera relative direction to move:
				m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
				m_Move = v * m_CamForward + h * m_Cam.right;
			}
			else
			{
				// we use world-relative directions in the case of no main camera
				m_Move = v * Vector3.forward + h * Vector3.right;
			}
		}
		else
		{
			m_Move.x = h;
			m_Move.z = v;
		}

        // pass all parameters to the character control script
        m_Character.Move(m_Move, m_Jump, m_Aim);
        m_Jump = false;
    }
}
