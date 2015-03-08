using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public EvadeOrderCommand order;
        public Vector2 targetPosition;
        public float timestamp;
        public bool isProcessed;
        public EvadeSpellData evadeSpellData;

        public EvadeCommand()
        {
            this.timestamp = Evade.GetTickCount();
            this.isProcessed = false;
        }

        public static void MoveTo(Vector2 movePos)
        {
            Evade.lastEvadeCommand = new EvadeCommand
            {
                order = EvadeOrderCommand.MoveTo,
                targetPosition = movePos,
                timestamp = Evade.GetTickCount(),
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
                timestamp = Evade.GetTickCount(),
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
                timestamp = Evade.GetTickCount(),
                isProcessed = false
            };

            myHero.Spellbook.CastSpell(spellData.spellKey,false);
        }
    }
}
