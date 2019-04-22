using UnityEngine;
using UnityEngine.XR.iOS;

namespace UAR 
{

    public class UARARKit : UAR
    {
        public static void init(ScriptableObject imgCollection, GameObject camera)
        {
            Logger.log(Logger.Type.Info, "backend = ARKit");
            
            if (imgCollection == null)
            {
                Logger.log(Logger.Type.Error, "no image collection!");
                return;
            }

            // world anchor events:
            UnityARSessionNativeInterface.ARUserAnchorAddedEvent += WAnchorAdd;
            UnityARSessionNativeInterface.ARUserAnchorUpdatedEvent += WAnchorUpdate;
            UnityARSessionNativeInterface.ARUserAnchorRemovedEvent += WAnchorRemove;

            // image anchor events:
            UnityARSessionNativeInterface.ARImageAnchorAddedEvent += IAnchorAdd;
            UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += IAnchorUpdate;
            UnityARSessionNativeInterface.ARImageAnchorRemovedEvent += IAnchorRemove;

            var manager = camera.GetComponent<UnityARCameraManager>();
            manager.detectionImages = (ARReferenceImagesSet)imgCollection;
            manager.maximumNumberOfTrackedImages = manager.detectionImages.referenceImages.Length;
            camera.SetActive(true);
        }

        private static void WAnchorAdd(ARUserAnchor anchor)
        {
            Logger.log(Logger.Type.Info, "WAnchorAdd");

            if (!tracking)
            {
                Logger.log(Logger.Type.Info, "tracking paused. Ignore.");
                return;
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
            WAnchor a = new WAnchor(anchor.identifier, UnityARMatrixOps.GetPose(anchor.transform));
            WAnchorAdded(a);
        }

        private static void WAnchorUpdate(ARUserAnchor anchor)
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
            WAnchor a = new WAnchor(anchor.identifier, UnityARMatrixOps.GetPose(anchor.transform));
            WAnchorUpdated(a);
        }

        private static void WAnchorRemove(ARUserAnchor anchor)
        {
            Logger.log(Logger.Type.Error, "WAnchorRemove not implemented");
        }

        private static void IAnchorAdd(ARImageAnchor anchor)
        {
            Logger.log(Logger.Type.Info, "IAnchorAdd");

            if (!tracking)
            {
                Logger.log(Logger.Type.Info, "tracking paused. Ignore.");
                return;
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
            IAnchor a = new IAnchor(anchor.identifier, UnityARMatrixOps.GetPose(anchor.transform), anchor.isTracked, anchor.referenceImageName);
            IAnchorAdded(a);
        }

        private static void IAnchorUpdate(ARImageAnchor anchor)
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
            IAnchor a = new IAnchor(anchor.identifier, UnityARMatrixOps.GetPose(anchor.transform), anchor.isTracked, anchor.referenceImageName);
            IAnchorUpdated(a);
        }

        private static void IAnchorRemove(ARImageAnchor anchor)
        {
            Logger.log(Logger.Type.Error, "IAnchorRemove not implemented");
        }
    }
}
