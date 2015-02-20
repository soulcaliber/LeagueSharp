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
    public class Spell
    {
        public float startTime;
        public float endTime;
        public Vector2 startPos;
        public Vector2 endPos;
        public Vector2 direction;
        public int heroID;
        public int projectileID;
        public SpellData info;
        public int spellID;
        public GameObject spellObject;

        public Spell()
        {

        }
    }
}
