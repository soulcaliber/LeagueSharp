using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using SharpDX;

namespace ezEvade.Helpers
{
    internal class CollisionCandidate
    {
        public Obj_AI_Base candidateHero;
        public int candidateId;
        public Vector2 startPos;
        public Vector2 endPos;
        public Vector2 currentPos;
        public SpellData spellInfo;
        public float spellTime;
        public float startTick;
        public float endTime;

        // new collision candidate
        public CollisionCandidate(Obj_AI_Base hero, Vector2 v, int id, float startTick, Vector2 start, Vector2 end, SpellData data, float endtime)
        {
            this.candidateHero = hero;
            this.currentPos = v;
            this.candidateId = id;
            this.startTick = startTick;
            this.startPos = start;
            this.endPos = end;
            this.spellInfo = data;
            this.endTime = endtime;
        }
    }
}
