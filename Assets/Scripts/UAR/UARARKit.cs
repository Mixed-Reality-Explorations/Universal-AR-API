using UnityEngine;
using UnityEngine.XR.iOS;

namespace UAR 
{

    public class UARARKit : Api
    {
        public UARARKit(ScriptableObject imgCollection) : base(imgCollection)
        {
            Logger.log(Logger.Type.Info, "backend = {0}", GetType().Name);
            
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
            m.maximumNumberOfTrackedImages = m.detectionImages.referenceImages.Length;

            // add video component + material:
            UnityARVideo v = Camera.main.gameObject.AddComponent<UnityARVideo>();
            v.m_ClearMaterial = (Material)Resources.Load<Material>("YUVMaterial");

            // add near/far component:
            Camera.main.gameObject.AddComponent<UnityARCameraNearFar>();
        }

        private void WAnchorAdd(ARUserAnchor anchor)
        {
            Logger.log(Logger.Type.Info, "WAnchorAdd");

            if (!tracking)
            {
                Logger.log(Logger.Type.Info, "tracking paused. Ignore.");
            }       

            if (!anchors.Add(anchor.identifier))
            {
                Logger.log(Logger.Type.Warning, "anchor was already added: {0}", anchor.identifier);
                return;
            }

            if (WAnchorAdded == null)
            {
                Logger.log(Logger.Type.Info, "no delegates for this event.");
                return;
            }

            Logger.log(Logger.Type.Info, "dispatch event.");
            WAnchor a = new WAnchor(anchor.identifier, anchor.transform);
            WAnchorAdded(a);
        }

        private void WAnchorUpdate(ARUserAnchor anchor)
        {
            Logger.log(Logger.Type.Info, "WAnchorUpdate");

            if (!tracking)
            {
                Logger.log(Logger.Type.Info, "tracking paused. Ignore.");
                return;
            }

            if (!anchors.Contains(anchor.identifier))
            {
                Logger.log(Logger.Type.Warning, "=> WAnchorAdd (tracking was probably off and just turned on).");
                WAnchorAdd(anchor);
                return;
            }

            if (WAnchorUpdated == null)
            {
                Logger.log(Logger.Type.Info, "no delegates for this event.");
                return;
            }

            Logger.log(Logger.Type.Info, "dispatch event.");
            WAnchor a = new WAnchor(anchor.identifier, anchor.transform);
            WAnchorUpdated(a);
        }

        private void WAnchorRemove(ARUserAnchor anchor)
        {
            Logger.log(Logger.Type.Error, "WAnchorRemove not implemented");
        }

        private void IAnchorAdd(ARImageAnchor anchor)
        {
            Logger.log(Logger.Type.Info, "IAnchorAdd");

            if (!tracking)
            {
                Logger.log(Logger.Type.Info, "tracking paused. Ignore.");
            }

            if (!anchors.Add(anchor.identifier))
            {
                Logger.log(Logger.Type.Warning, "anchor was already added: {0}", anchor.identifier);
                return;
            }

            if (IAnchorAdded == null)
            {
                Logger.log(Logger.Type.Info, "no delegates for this event.");
                return;
            }

            Logger.log(Logger.Type.Info, "dispatch event.");
            IAnchor a = new IAnchor(anchor.identifier, anchor.transform, anchor.isTracked, anchor.referenceImageName);
            IAnchorAdded(a);
        }

        private void IAnchorUpdate(ARImageAnchor anchor)
        {
            Logger.log(Logger.Type.Info, "IAnchorUpdate");

            if (!tracking)
            {
                Logger.log(Logger.Type.Info, "tracking paused. Ignore.");
                return;
            }

            if (!anchors.Contains(anchor.identifier))
            {
                Logger.log(Logger.Type.Warning, "=> IAnchorAdd (tracking was probably off and just turned on).");
                IAnchorAdd(anchor);
                return;
            }

            if (IAnchorUpdated == null)
            {
                Logger.log(Logger.Type.Info, "no delegates for this event.");
                return;
            }

            Logger.log(Logger.Type.Info, "dispatch event.");
            IAnchor a = new IAnchor(anchor.identifier, anchor.transform, anchor.isTracked, anchor.referenceImageName);
            IAnchorUpdated(a);
        }

        private void IAnchorRemove(ARImageAnchor anchor)
        {
            Logger.log(Logger.Type.Error, "IAnchorRemove not implemented");
        }
    }
}
