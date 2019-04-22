using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UAR {
    public class UARInit : MonoBehaviour
    {
        public ScriptableObject imgCollection;
        public Api api;

        private void Awake()
        {
            RuntimePlatform platform = Application.platform;
            Debug.LogFormat("playform is {0}", platform);

            switch (platform)
            {
                case RuntimePlatform.WindowsEditor:
                    {
                        api = new UARARKit(imgCollection);
                        break;
                    }

                case RuntimePlatform.IPhonePlayer:
                    {
                        api = new UARARKit(imgCollection);
                        break;
                    }

                case RuntimePlatform.Android:
                    {
                        api = new UARARKit(imgCollection);
                        break;
                    }
            }
        }

    }
}
