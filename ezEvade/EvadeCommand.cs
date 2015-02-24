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
    public enum EvadeOrderCommand
    {
        None,
        MoveTo,
        CastSpell
    }

    class EvadeCommand
    {
        private static Obj_AI_Hero myHero { get { return ObjectManager.Player; } }
        private static float gameTime { get { return Game.ClockTime * 1000; } }

        public EvadeOrderCommand order;
        public Vector2 targetPosition;
        public float timestamp;
        public bool isProcessed;
        public EvadeSpellData evadeSpellData;

        public EvadeCommand()
        {
            this.timestamp = gameTime;
            this.isProcessed = false;
        }

        public static void MoveTo(Vector2 movePos)
        {
            Evade.lastEvadeCommand = new EvadeCommand
            {
                order = EvadeOrderCommand.MoveTo,
                targetPosition = movePos,
                timestamp = gameTime,
                isProcessed = false
            };
            myHero.IssueOrder(GameObjectOrder.MoveTo, movePos.To3D(), false);
        }

        public static void CastSpell(EvadeSpellData spellData, Vector2 movePos)
        {
            EvadeSpell.lastSpellEvadeCommand = new EvadeCommand
            {
                order = EvadeOrderCommand.CastSpell,
                targetPosition = movePos,
                evadeSpellData = spellData,
                timestamp = gameTime,
                isProcessed = false
            };

            myHero.Spellbook.CastSpell(spellData.spellKey, movePos.To3D(), false);
        }

        public static void CastSpell(EvadeSpellData spellData)
        {
            EvadeSpell.lastSpellEvadeCommand = new EvadeCommand
            {
                order = EvadeOrderCommand.CastSpell,
                evadeSpellData = spellData,
                timestamp = gameTime,
                isProcessed = false
            };

            myHero.Spellbook.CastSpell(spellData.spellKey,false);
        }
    }
}
