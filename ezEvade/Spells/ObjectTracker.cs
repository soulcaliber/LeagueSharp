using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    public class ObjectTrackerInfo
    {
        public GameObject obj;
        public Vector3 position;
        public Vector3 direction;
        public string Name;
        public int OwnerNetworkID;
        public bool usePosition = false;
        public float timestamp = 0;

        public ObjectTrackerInfo(GameObject obj)
        {
            this.obj = obj;
            this.Name = obj.Name;
            this.timestamp = EvadeUtils.TickCount;
        }

        public ObjectTrackerInfo(GameObject obj, string name)
        {
            this.obj = obj;
            this.Name = name;
            this.timestamp = EvadeUtils.TickCount;
        }

        public ObjectTrackerInfo(string name, Vector3 position)
        {
            this.Name = name;
            this.usePosition = true;
            this.position = position;

            this.timestamp = EvadeUtils.TickCount;
        }
    }

    public static class ObjectTracker
    {
        public static Dictionary<int, ObjectTrackerInfo> objTracker = new Dictionary<int, ObjectTrackerInfo>();
        public static int objTrackerID = 0;

        static ObjectTracker()
        {
            Obj_AI_Minion.OnCreate += HiuCreate_ObjectTracker;
            //Obj_AI_Minion.OnCreate += HiuDelete_ObjectTracker;
        }

        public static void AddObjTrackerPosition(string name, Vector3 position, float timeExpires)
        {
            objTracker.Add(objTrackerID, new ObjectTrackerInfo(name, position));

            int trackerID = objTrackerID; //store the id for deletion
            DelayAction.Add((int)timeExpires, () => objTracker.Remove(objTrackerID));

            objTrackerID += 1;
        }

        private static void HiuCreate_ObjectTracker(GameObject obj, EventArgs args)
        {
            if (obj.IsEnemy && obj.Type == GameObjectType.obj_AI_Minion
                && !ObjectTracker.objTracker.ContainsKey(obj.NetworkId))
            {
                var minion = obj as Obj_AI_Minion;

                if (minion.BaseSkinName.Contains("testcube"))
                {
                    ObjectTracker.objTracker.Add(obj.NetworkId, new ObjectTrackerInfo(obj, "hiu"));
                    DelayAction.Add(250, () => ObjectTracker.objTracker.Remove(obj.NetworkId));
                }
            }
        }

        private static void HiuDelete_ObjectTracker(GameObject obj, EventArgs args)
        {
            if (ObjectTracker.objTracker.ContainsKey(obj.NetworkId))
            {
                ObjectTracker.objTracker.Remove(obj.NetworkId);
            }
        }

        public static Vector2 GetLastHiuOrientation()
        {
            var objList = ObjectTracker.objTracker.Values.Where(o => o.Name == "hiu");
            var sortedObjList = objList.OrderByDescending(o => o.timestamp);

            if (sortedObjList.Count() >= 2)
            {
                var pos1 = sortedObjList.First().obj.Position;
                var pos2 = sortedObjList.ElementAt(1).obj.Position;

                return (pos2.To2D() - pos1.To2D()).Normalized();
            }

            return Vector2.Zero;
        }
    }
}
