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
    public static class DelayAction
    {
        public delegate void Callback();

        public static List<Action> ActionList = new List<Action>();

        static DelayAction()
        {
            Game.OnUpdate += GameOnOnGameUpdate;
        }

        private static void GameOnOnGameUpdate(EventArgs args)
        {
            for (var i = ActionList.Count - 1; i >= 0; i--)
            {
                if (ActionList[i].Time <= EvadeUtils.TickCount)
                {
                    try
                    {
                        if (ActionList[i].CallbackObject != null)
                        {
                            ActionList[i].CallbackObject();
                            //Will somehow result in calling ALL non-internal marked classes of the called assembly and causes NullReferenceExceptions.
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    ActionList.RemoveAt(i);
                }
            }
        }

        public static void Add(int time, Callback func)
        {
            var action = new Action(time, func);
            ActionList.Add(action);
        }

        public struct Action
        {
            public Callback CallbackObject;
            public int Time;

            public Action(int time, Callback callback)
            {
                Time = time + (int)EvadeUtils.TickCount;
                CallbackObject = callback;
            }
        }
    }
}
