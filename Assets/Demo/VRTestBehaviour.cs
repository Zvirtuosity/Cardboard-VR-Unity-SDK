﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MobfishCardboard;

namespace MobfishCardboardDemo
{
    public class VRTestBehaviour: MonoBehaviour
    {
        public Text debugText;
        public Text debugTextStatic;
        public MeshFilter testEyeMeshLeft;
        public MeshFilter testEyeMeshRight;

        // Start is called before the first frame update
        void Start()
        {
            CardboardManager.deviceParamsChangeEvent += DeviceParamsChanged;
        }

        private void DeviceParamsChanged()
        {
            debugTextStatic.text = string.Format("eyefromhead=\r\n{0}", CardboardManager.eyeFromHeadMatrixLeft);
        }

        private void OnEnable()
        {
            CardboardManager.deviceParamsChangeEvent += RefreshCameraProperty;
        }

        private void OnDisable()
        {
            CardboardManager.deviceParamsChangeEvent -= RefreshCameraProperty;
        }

        // Update is called once per frame
        void Update()
        {
            debugText.text = string.Format("device rot={0}, \r\nUnity rot={1}",
                CardboardHeadTracker.trackerRawRotation.eulerAngles,
                CardboardHeadTracker.trackerUnityRotation.eulerAngles);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Debug.Log("OnApplicationFocus called, hasFocus=" + hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Debug.Log("OnApplicationPause called, pauseStatus=" + pauseStatus);
        }

        private void RefreshCameraProperty()
        {
            testEyeMeshLeft.mesh = CardboardManager.viewMeshLeft;
            testEyeMeshRight.mesh = CardboardManager.viewMeshRight;

            if (!Application.isEditor)
            {
                NativeDataExtract.Save_MeshJson(CardboardManager.viewMeshLeftRaw);
                NativeDataExtract.Save_MeshJson(CardboardManager.viewMeshRightRaw);
            }

            // (byte[], int) paramDetailVar = CardboardQrCode.GetDeviceParamsByte();
            // NativeDataExtract.Save_EncodedParam(paramDetailVar.Item1, paramDetailVar.Item2);
        }
    }


}