using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour {
    public ArcRaycaster arcRaycaster;

    Vector3 ControllerPos;
    Vector2 touch;
    Quaternion Orig;

    float zee;
    public static int ObjInx;
    public static int MenInx;
    public static bool DoneChose;

    public static Vector3 Pos;

    public GameObject[] _SelectedObject;
    public GameObject _canv;
    public GameObject _cam;
    public GameObject _ParLaser;
    public GameObject _PublicPar;

    public static bool _DisActive;
    public static bool _MoveMent;
    private bool _stopMov;
    public static bool _Rotate;
    private Vector3 OrigDis;
    private Quaternion OrigRot;

	// Use this for initialization
	void Start () {
        
        ObjInx = -1;
        MenInx = -1;
        _canv.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (DoneChose)
        {
            _canv.SetActive(true);
            _canv.transform.position = _SelectedObject[ObjInx].transform.position;
            OrigDis = _SelectedObject[ObjInx].transform.position;
            OrigRot = _SelectedObject[ObjInx].transform.rotation;
            _canv.transform.Translate(0,1f,0);
            _canv.transform.LookAt(_cam.transform.position);
           // _canv.transform.Rotate(0, 180, 0);

            DoneChose = false;
        }

        

        if (MenInx == 0)
        {
            _canv.SetActive(false);
            _SelectedObject[ObjInx].SetActive(false);
            _DisActive = true;
            _MoveMent = false;
            _Rotate = false;
            MenInx = -1;
        }else if(MenInx == 1)
        {
            _canv.SetActive(false);

            _SelectedObject[ObjInx].transform.position = Pos;
            
            _DisActive = false;
            _MoveMent = true;
            _Rotate = false;

        }
        else if(MenInx == 2)
        {
            _canv.SetActive(false);

           
            //if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad)) {
                _SelectedObject[ObjInx].transform.rotation = Quaternion.LookRotation(TouchpadDirection, arcRaycaster.Normal);
            
            _DisActive = false;
            _MoveMent = false;
            _Rotate = true;

        }


        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && _MoveMent)
        {
            _SelectedObject[ObjInx].transform.parent = _PublicPar.transform;
            MenInx = -1;
            
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && _Rotate)
        {
            MenInx = -1;

        }

        if (OVRInput.Get(OVRInput.Button.Back))
        {
            if (_DisActive)
            {
                _SelectedObject[ObjInx].SetActive(true);
                _DisActive = false;
            }

            if(_MoveMent)
            {
                _SelectedObject[ObjInx].transform.position = OrigDis;
                _SelectedObject[ObjInx].transform.parent = _PublicPar.transform;
                MenInx = -1;
                _MoveMent = false;
               
            }
            if (_Rotate)
            {
                _SelectedObject[ObjInx].transform.rotation = OrigRot;
                MenInx = -1;
                _Rotate = false;
            }

        }
	}
    OVRInput.Controller Controller
    {
        get
        {
            OVRInput.Controller controllers = OVRInput.GetConnectedControllers();
            if ((controllers & OVRInput.Controller.LTrackedRemote) == OVRInput.Controller.LTrackedRemote)
            {
                return OVRInput.Controller.LTrackedRemote;
            }
            if ((controllers & OVRInput.Controller.RTrackedRemote) == OVRInput.Controller.RTrackedRemote)
            {
                return OVRInput.Controller.RTrackedRemote;
            }
            return OVRInput.Controller.None;
        }
    }

    Matrix4x4 ControllerToWorldMatrix
    {
        get
        {
           
            Matrix4x4 localToWorld = arcRaycaster.trackingSpace.localToWorldMatrix;

            Quaternion orientation = OVRInput.GetLocalControllerRotation(Controller);
            Vector3 position = OVRInput.GetLocalControllerPosition(Controller);

            Matrix4x4 local = Matrix4x4.TRS(position, orientation, Vector3.one);

            Matrix4x4 world = local * localToWorld;

            return world;
        }
    }

    Vector3 TouchpadDirection
    {
        get
        {
             touch = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            Vector3 forward = new Vector3(touch.x, 0.0f, touch.y).normalized;
            forward = ControllerToWorldMatrix.MultiplyVector(forward);
            forward = Vector3.ProjectOnPlane(forward, Vector3.up);
            return forward.normalized;
        }
    }
    
}
