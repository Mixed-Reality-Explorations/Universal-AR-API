using System.Collections;using System.Collections.Generic;using UnityEngine;using System;namespace UAR{    public class Logger
    {
        public enum Type
        {
            Info,
            Warning,
            Error
        }

        public static bool enabled = true;
        public static void log(Logger.Type type, string format, params object[] p)
        {
            if (enabled)
            {
                format = "[UAR]: " + format;
                switch (type)
                {
                    case Type.Info:
                        {
                            UnityEngine.Debug.LogFormat(format, p);
                            break;
                        }

                    case Type.Warning:
                        {
                            UnityEngine.Debug.LogWarningFormat(format, p);
                            break;
                        }

                    case Type.Error:
                        {
                            UnityEngine.Debug.LogErrorFormat(format, p);
                            break;
                        }
                }
                
            }
        }
    }    public class WAnchor    {        public WAnchor(string id, Matrix4x4 pose) { this.id = id; this.pose = pose; }        public readonly string id;        public  readonly Matrix4x4 pose;    }    public class IAnchor : WAnchor    {        public IAnchor(string id, Matrix4x4 pose, bool tracking, string imgName)            : base(id, pose)        { this.tracking = tracking; this.imgName = imgName; }        public readonly bool tracking;        public readonly string imgName;    }    public abstract class Api    {        protected HashSet<string> anchors = new HashSet<string>();        public readonly ScriptableObject imgCollection;        public bool tracking = true;        public Action<IAnchor> IAnchorAdded;        public Action<IAnchor> IAnchorUpdated;        public Action<IAnchor> IAnchorRemoved;        public Action<WAnchor> WAnchorAdded;        public Action<WAnchor> WAnchorUpdated;        public Action<WAnchor> WAnchorRemoved;        public Api(ScriptableObject imgCollection) { this.imgCollection = imgCollection; }    }}