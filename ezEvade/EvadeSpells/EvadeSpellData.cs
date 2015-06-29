using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    public delegate bool UseSpellFunc(EvadeSpellData evadeSpell, bool process = true);

    public enum CastType
    {
        Position,
        Target,
        Self,
    }

    public enum SpellTargets
    {
        AllyMinions,
        EnemyMinions,

        AllyChampions,
        EnemyChampions,

        Targetables,
    }

    public enum EvadeType
    {
        Blink,
        Dash,
        Invulnerability,
        MovementSpeedBuff,
        Shield,
        SpellShield,
        WindWall,
    }

    public class EvadeSpellData
    {
        public string charName;
        public SpellSlot spellKey = SpellSlot.Q;
        public int dangerlevel = 1;
        public string spellName;
        public string name;
        public bool checkSpellName = false;
        public float spellDelay = 250;
        public float range;
        public float speed = 0;
        public bool fixedRange = false;
        public EvadeType evadeType;
        public bool isReversed = false;
        public bool behindTarget = false;
        public bool infrontTarget = false;
        public bool isSummonerSpell = false;
        public bool isItem = false;
        public ItemId itemID = 0;
        public CastType castType = CastType.Position;
        public SpellTargets[] spellTargets = { };
        public UseSpellFunc useSpellFunc = null;
        public bool isSpecial = false;

        public EvadeSpellData()
        {

        }

        public EvadeSpellData(
            string charName,
            string name,
            SpellSlot spellKey,
            EvadeType evadeType,
            int dangerlevel
            )
        {
            this.charName = charName;
            this.name = name;
            this.spellKey = spellKey;
            this.evadeType = evadeType;
            this.dangerlevel = dangerlevel;
        }
    }
}
