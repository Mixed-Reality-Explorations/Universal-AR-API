using System.Collections;using System.Collections.Generic;using UnityEngine;using System;namespace UAR{    public class WAnchor
    {
        public WAnchor(string id, Matrix4x4 pose) { this.id = id; this.pose = pose; }
        public readonly string id;
        public  readonly Matrix4x4 pose;
    }

    public class IAnchor : WAnchor
    {
        public IAnchor(string id, Matrix4x4 pose, bool tracking, string imgName)
            : base(id, pose)
        { this.tracking = tracking; this.imgName = imgName; }

        public readonly bool tracking;
        public readonly string imgName;
    }    public abstract class Api    {
        protected HashSet<string> anchors;        public readonly ScriptableObject imgCollection;        public bool tracking = true;        public Action<IAnchor> IAnchorAdded;        public Action<IAnchor> IAnchorUpdated;        public Action<IAnchor> IAnchorRemoved;        public Action<WAnchor> WAnchorAdded;        public Action<WAnchor> WAnchorUpdated;        public Action<WAnchor> WAnchorRemoved;

        public Api(ScriptableObject imgCollection) { this.imgCollection = imgCollection; }    }}