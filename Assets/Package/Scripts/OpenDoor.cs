using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Menu
{
    // This script is for loading scenes from the main menu.
    // Each 'button' will be a rendering showing the scene
    // that will be loaded and use the SelectionRadial.
    public class OpenDoor : MonoBehaviour
    {
        public event Action<OpenDoor> OnButtonSelected;                   // This event is triggered when the selection of the button has finished.

        
        [SerializeField] private SelectionRadial m_SelectionRadial;         // This controls when the selection is complete.
        [SerializeField] private VRInteractiveItem m_InteractiveItem;       // The interactive item for where the user should click to load the level.

        public AudioClip[] _AudioDoor;
        public AudioSource Src;
        private Vector3 new_axis = new Vector3(0, -1, 0);
        private Vector3 new_axis1 = new Vector3(0, 1, 0);
        private int _Time = 0;
        public HingeJoint _openDoor;
        private bool m_GazeOver;                                            // Whether the user is looking at the VRInteractiveItem currently.

       

        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_SelectionRadial.OnSelectionComplete += HandleSelectionComplete;
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_SelectionRadial.OnSelectionComplete -= HandleSelectionComplete;
        }


        private void HandleOver()
        {
            // When the user looks at the rendering of the scene, show the radial.
            m_SelectionRadial.Show();

            m_GazeOver = true;

        }


        private void HandleOut()
        {
            // When the user looks away from the rendering of the scene, hide the radial.
            m_SelectionRadial.Hide();

            m_GazeOver = false;

        }


        public void HandleSelectionComplete()
        {
            Debug.Log("Amir");
            if (_Time % 2 == 0)
            {

               
                _openDoor.axis = new_axis1;

                JointSpring hingeSpring = _openDoor.spring;
                hingeSpring.spring = 500;
                hingeSpring.damper = 600;
                hingeSpring.targetPosition = 90;
                _openDoor.spring = hingeSpring;

                _openDoor.useSpring = true;

                Src.clip = _AudioDoor[0];
                Src.Play();

            }
            else
            {
               

                _openDoor.axis = new_axis;

                JointSpring hingeSpring = _openDoor.spring;
                hingeSpring.spring = 500;
                hingeSpring.damper = 600;
                hingeSpring.targetPosition = 90;
                _openDoor.spring = hingeSpring;

                _openDoor.useSpring = true;

                Src.clip = _AudioDoor[1];
                Src.Play();
            }
            _Time++;

            m_GazeOver = false;
        }

    }


}