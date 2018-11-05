using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchControl : MonoBehaviour {
    public GameObject _Laser;
    public GameObject _Retricle;
    public GameObject _Teleport;
    int c = 0;
    bool dd;
	// Use this for initialization
	void Start () {
        _Teleport.SetActive(true);
        _Retricle.SetActive(false);
        _Laser.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.One)){

            if (c % 2 == 0)
            {
                _Teleport.SetActive(false);
                _Retricle.SetActive(true);
                _Laser.SetActive(true);
            }
            else
            {
                _Teleport.SetActive(true);
                _Retricle.SetActive(false);
                _Laser.SetActive(false);
            }

            c++;
        }
       

    }

}
