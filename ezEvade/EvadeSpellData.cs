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
    public enum CastType
    {
        Position,
        Target,
        Self,
    }

    public enum SpellValidTargets
    {
        AllyMinions,
        EnemyMinions,

        AllyChampions,
        EnemyChampions,
    }

    public enum EvadeType
    {
        Blink,
        Dash,
        Invulnerability,
        MovementSpeedBuff,
        Shield,
        SpellShield,
    }

    class EvadeSpellData
    {
        public string charName;
        public SpellSlot spellKey = SpellSlot.Q;
        public int dangerlevel = 1;
        public string spellName;
        public string name;
        public bool checkSpellName = false;
        public float spellDelay = 250;
        public float range;
        public float speed;
        public bool fixedRange = false;
        public EvadeType evadeType;
        public bool isReversed = false;
        public bool isSummonerSpell = false;

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
