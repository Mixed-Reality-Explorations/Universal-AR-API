using UnityEngine;
using UnityEngine.XR.iOS;
using System.Diagnostics;

namespace UAR 
{

    public class UARARKit : Api
    {
        public UARARKit(ScriptableObject imgCollection) : base(imgCollection)
        {
            UnityEngine.Debug.LogFormat("UA: using ** {0} **", GetType().Name);

            // world anchor events:
            UnityARSessionNativeInterface.ARUserAnchorAddedEvent += WAnchorAdd;
            UnityARSessionNativeInterface.ARUserAnchorUpdatedEvent += WAnchorUpdate;
            UnityARSessionNativeInterface.ARUserAnchorRemovedEvent += WAnchorRemove;

            // image anchor events:
            UnityARSessionNativeInterface.ARImageAnchorAddedEvent += IAnchorAdd;
            UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += IAnchorUpdate;
            UnityARSessionNativeInterface.ARImageAnchorRemovedEvent += IAnchorRemove;
            
            // add camera manager:
            UnityARCameraManager m = Camera.main.gameObject.AddComponent<UnityARCameraManager>();
            m.m_camera = Camera.main;

            // configuration:
            m.startAlignment = UnityARAlignment.UnityARAlignmentGravity;
            m.planeDetection = UnityARPlaneDetection.None;
            m.getPointCloud = true;
            m.enableLightEstimation = true;
            m.enableAutoFocus = true;
            m.environmentTexturing = UnityAREnvironmentTexturing.UnityAREnvironmentTexturingNone;
            m.detectionImages = (ARReferenceImagesSet)imgCollection;

            // add video component + material:
            UnityARVideo v = Camera.main.gameObject.AddComponent<UnityARVideo>();
            v.m_ClearMaterial = (Material)Resources.Load<Material>("YUVMaterial");

            // add near/far component:
            Camera.main.gameObject.AddComponent<UnityARCameraNearFar>();
        }

        private void WAnchorAdd(ARUserAnchor anchor)
        {
            if (tracking)
            {
                if (anchors.Add(anchor.identifier))
                {
                    WAnchor a = new WAnchor(anchor.identifier, anchor.transform);
                    WAnchorAdded(a);
                }
            }
        }

        private void WAnchorUpdate(ARUserAnchor anchor)
        {
            if (tracking)
            {
                WAnchor a = new WAnchor(anchor.identifier, anchor.transform);

                if (anchors.Add(anchor.identifier))
                {
                    WAnchorAdded(a);
                }
                else
                {
                    WAnchorUpdated(a);
                }
            }
        }

        private void WAnchorRemove(ARUserAnchor anchor)
        {
            var s = new StackTrace();
            var f = s.GetFrame(0);
            throw new UnityException(string.Format("{0}: [Not Implemented!]", f.GetMethod()));
        }

        private void IAnchorAdd(ARImageAnchor anchor)
        {
            if (tracking)
            {
                if (anchors.Add(anchor.identifier))
                {
                    IAnchor a = new IAnchor(anchor.identifier, anchor.transform, anchor.isTracked, anchor.referenceImageName);
                    WAnchorAdded(a);
                }
            }
        }

        private void IAnchorUpdate(ARImageAnchor anchor)
        {
            if (tracking)
            {
                IAnchor a = new IAnchor(anchor.identifier, anchor.transform, anchor.isTracked, anchor.referenceImageName);

                if (anchors.Add(anchor.identifier))
                {
                    WAnchorAdded(a);
                }
                else
                {
                    WAnchorUpdated(a);
                }
            }
        }

        private void IAnchorRemove(ARImageAnchor anchor)
        {
            var s = new StackTrace();
            var f = s.GetFrame(0);
            throw new UnityException(string.Format("{0}: [Not Implemented!]", f.GetMethod()));
        }
    }
}
