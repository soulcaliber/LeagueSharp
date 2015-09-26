using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace xAwareness
{
    class TeleportTracker
    {
        public static Dictionary<int, TeleportInfo> teleportInfos = new Dictionary<int, TeleportInfo>();

        static TeleportTracker()
        {
            Obj_AI_Base.OnTeleport += Game_OnTeleport;
            Obj_AI_Base.OnCreate += Game_OnCreateObject;
            
            Initialize();
        }

        public static void Initialize()
        {
            foreach (var hero in HeroManager.AllHeroes)
            {
                teleportInfos.Add(hero.NetworkId, new TeleportInfo(hero));
            }
        }

        private static void Game_OnCreateObject(GameObject sender, EventArgs args)
        {
            if (sender.Type == GameObjectType.obj_GeneralParticleEmitter &&
                sender.Name.Contains("global_ss_teleport"))
            {
                foreach (var info in teleportInfos.Values)
                {
                    if (info.isTeleporting && HelperUtils.TickCount - info.startTime < 1000)
                    {
                        if (sender.Name.Contains("turret"))
                        {
                            info.isTurretTeleport = true;
                        }

                        info.position = sender.Position;
                    }
                }
            }
        }

        private static void Game_OnTeleport(Obj_AI_Base sender, GameObjectTeleportEventArgs args)
        {
            var hero = sender as Obj_AI_Hero;
            if (hero != null)
            {
                var packet = Packet.S2C.Teleport.Decoded(sender, args);

                if (packet.Type == Packet.S2C.Teleport.Type.Teleport)
                {
                    var duration = 0;

                    if (packet.Status == Packet.S2C.Teleport.Status.Finish)
                    {
                        duration = 300;

                        var info = teleportInfos[sender.NetworkId];
                        if (info.isTurretTeleport)
                        {
                            duration = 240;
                        }

                        info.isTeleporting = false;
                    }
                    else if (packet.Status == Packet.S2C.Teleport.Status.Abort)
                    {
                        duration = 200;

                        var info = teleportInfos[sender.NetworkId];
                        info.isTeleporting = false;
                    }

                    if (packet.Status == Packet.S2C.Teleport.Status.Start)
                    {
                        var info = teleportInfos[sender.NetworkId];
                        info.isTeleporting = true;
                        info.isTurretTeleport = false;
                        info.isRecalling = false;
                        info.position = Vector3.Zero;
                        info.startTime = HelperUtils.TickCount;
                    }
                    else
                    {
                        var info = teleportInfos[sender.NetworkId];
                        info.isTeleporting = false;
                        info.isTurretTeleport = false;
                    }
                }
            }
        }
    }

    class TeleportInfo
    {
        public float startTime = 0;
        public float endTime = 0;
        public Vector3 position = Vector3.Zero;
        public bool isTurretTeleport = false;
        public bool isTeleporting = false;
        public bool isRecalling = false;
        public Obj_AI_Hero hero;

        public TeleportInfo(Obj_AI_Hero hero)
        {
            this.hero = hero;
        }
    }
}
