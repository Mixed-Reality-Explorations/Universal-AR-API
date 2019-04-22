using UnityEngine;
using GoogleARCore;
using System.Collections.Generic;

namespace UAR
{

    public class UARARCore : UAR
    {
        private List<AugmentedImage> images = new List<AugmentedImage>();

        public static void init(ScriptableObject imgCollection, GameObject camera)
        {
            Logger.log(Logger.Type.Info, "backend = ARCore");

            if (imgCollection == null)
            {
                Logger.log(Logger.Type.Error, "no image collection!");
                return;
            }

            var session = camera.GetComponent<ARCoreSession>();
            session.SessionConfig.AugmentedImageDatabase = (AugmentedImageDatabase)imgCollection;
            camera.SetActive(true);
        }

        private void Update()
        {
            Session.GetTrackables<AugmentedImage>(images, TrackableQueryFilter.New);

            foreach (var i in images)
            {
                IAnchorAdd(i);
            }

            Session.GetTrackables<AugmentedImage>(images, TrackableQueryFilter.Updated);

            foreach (var i in images)
            {
                IAnchorUpdate(i);
            }
        }

        //private static void WAnchorAdd(AugmentedImage anchor)
        //{
        //    Logger.log(Logger.Type.Info, "WAnchorAdd");

        //    if (!tracking)
        //    {
        //        Logger.log(Logger.Type.Info, "tracking paused. Ignore.");
        //        return;
        //    }

        //    if (!anchors.Add(anchor.DatabaseIndex.ToString()))
        //    {
        //        Logger.log(Logger.Type.Warning, "anchor was already added: {0}", anchor.DatabaseIndex);
        //        return;
        //    }

        //    if (WAnchorAdded == null)
        //    {
        //        Logger.log(Logger.Type.Info, "no delegates for this event.");
        //        return;
        //    }

        //    Logger.log(Logger.Type.Info, "dispatch event.");
        //    WAnchor a = new WAnchor(anchor.DatabaseIndex.ToString(), anchor.CenterPose);
        //    WAnchorAdded(a);
        //}

        //private static void WAnchorUpdate(ARUserAnchor anchor)
        //{
        //    Logger.log(Logger.Type.Info, "WAnchorUpdate");

        //    if (!tracking)
        //    {
        //        Logger.log(Logger.Type.Info, "tracking paused. Ignore.");
        //        return;
        //    }

        //    if (!anchors.Contains(anchor.identifier))
        //    {
        //        Logger.log(Logger.Type.Warning, "=> WAnchorAdd (tracking was probably off and just turned on).");
        //        WAnchorAdd(anchor);
        //        return;
        //    }

        //    if (WAnchorUpdated == null)
        //    {
        //        Logger.log(Logger.Type.Info, "no delegates for this event.");
        //        return;
        //    }

        //    Logger.log(Logger.Type.Info, "dispatch event.");
        //    WAnchor a = new WAnchor(anchor.identifier, anchor.transform);
        //    WAnchorUpdated(a);
        //}

        //private static void WAnchorRemove(ARUserAnchor anchor)
        //{
        //    Logger.log(Logger.Type.Error, "WAnchorRemove not implemented");
        //}

        private static void IAnchorAdd(AugmentedImage anchor)
        {
            Logger.log(Logger.Type.Info, "IAnchorAdd");

            if (!tracking)
            {
                Logger.log(Logger.Type.Info, "tracking paused. Ignore.");
                return;
            }

            if (!anchors.Add(anchor.DatabaseIndex.ToString()))
            {
                Logger.log(Logger.Type.Warning, "anchor was already added: {0}", anchor.DatabaseIndex);
                return;
            }

            if (IAnchorAdded == null)
            {
                Logger.log(Logger.Type.Info, "no delegates for this event.");
                return;
            }

            Logger.log(Logger.Type.Info, "dispatch event.");
            bool isTracking = anchor.TrackingState == TrackingState.Tracking;
            IAnchor a = new IAnchor(anchor.DatabaseIndex.ToString(), anchor.CenterPose, isTracking, anchor.Name);
            IAnchorAdded(a);
        }

        private static void IAnchorUpdate(AugmentedImage anchor)
        {
            Logger.log(Logger.Type.Info, "IAnchorUpdate");

            if (!tracking)
            {
                Logger.log(Logger.Type.Info, "tracking paused. Ignore.");
                return;
            }

            if (!anchors.Contains(anchor.DatabaseIndex.ToString()))
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
            
            bool isTracking = anchor.TrackingState == TrackingState.Tracking;
            IAnchor a = new IAnchor(anchor.DatabaseIndex.ToString(), anchor.CenterPose, isTracking, anchor.Name);
            IAnchorUpdated(a);
        }

        //private static void IAnchorRemove(ARImageAnchor anchor)
        //{
        //    Logger.log(Logger.Type.Error, "IAnchorRemove not implemented");
        //}
    }
}
