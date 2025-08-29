using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMFreelookSetting : MonoBehaviour
{
    void Awake()
    {
        CinemachineCore.GetInputAxis = clickControl;
    }

    public float clickControl(string axis)
    {
        if (Input.GetMouseButton(1))
            return UnityEngine.Input.GetAxis(axis);

        return 0;
    }
}
