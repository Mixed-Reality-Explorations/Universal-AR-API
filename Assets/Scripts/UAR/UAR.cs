﻿using System.Collections;
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
    }
        protected HashSet<string> anchors;

        public Api(ScriptableObject imgCollection) { this.imgCollection = imgCollection; }