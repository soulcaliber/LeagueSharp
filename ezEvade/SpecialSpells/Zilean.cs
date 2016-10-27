using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade.SpecialSpells
{
    class Zilean : ChampionPlugin
    {
        internal const string ObjName = "TimeBombGroundRed";
        private static readonly List<GameObject> _bombs = new List<GameObject>();
        private static readonly Dictionary<float, Vector3> _qSpots = new Dictionary<float, Vector3>();

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "ZileanQ")
            {
                Game.OnUpdate += Game_OnUpdate;
                GameObject.OnCreate += GameObject_OnCreate;
                GameObject.OnDelete += GameObject_OnDelete;
                SpellDetector.OnProcessSpecialSpell += SpellDetector_OnProcessSpecialSpell;
            }
        }

        private void Game_OnUpdate(EventArgs args)
        {
            _bombs.RemoveAll(i => !i.IsValid || i.IsDead || !i.IsVisible);

            foreach (var spot in _qSpots.ToArray())
            {
                var timestamp = spot.Key;
                if (Game.Time - timestamp >= 2.5f * 0.6f)
                {
                    _qSpots.Remove(timestamp);
                }
            }
        }

        private void GameObject_OnCreate(GameObject bomb, EventArgs args)
        {
            if (bomb.Name.Contains(ObjName))
            {
                if (!_bombs.Contains(bomb))
                {
                    RemovePairsNear(bomb.Position);
                    _bombs.Add(bomb);
                }
            }
        }

        private void GameObject_OnDelete(GameObject bomb, EventArgs args)
        {
            if (bomb.Name.Contains(ObjName))
            {
                _bombs.RemoveAll(i => i.NetworkId == bomb.NetworkId);
            }
        }


        private void SpellDetector_OnProcessSpecialSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName == "ZileanQ")
            {
                var end = args.End;
                if (args.Start.Distance(end) > spellData.range)
                    end = args.Start + (args.End - args.Start).Normalized() * spellData.range;

                foreach (var bomb in _bombs.Where(b => b.IsValid && !b.IsDead && b.IsVisible))
                {
                    var newData = (SpellData) spellData.Clone();
                    newData.radius = 350;

                    if (end.Distance(bomb.Position) <= newData.radius)
                    {
                        SpellDetector.CreateSpellData(hero, hero.ServerPosition, bomb.Position, newData, null, 0, true, SpellType.Circular, false, newData.radius);
                        SpellDetector.CreateSpellData(hero, hero.ServerPosition, end, newData, null, 0, true, SpellType.Circular, false, newData.radius);
                        specialSpellArgs.noProcess = true;
                    }
                }

                foreach (var bombPosition in _qSpots.Values)
                {
                    var newData = (SpellData) spellData.Clone();
                    newData.radius = 350;

                    if (end.Distance(bombPosition) <= newData.radius)
                    {
                        SpellDetector.CreateSpellData(hero, hero.ServerPosition, bombPosition, newData, null, 0, true, SpellType.Circular, false, newData.radius);
                        SpellDetector.CreateSpellData(hero, hero.ServerPosition, end, newData, null, 0, true, SpellType.Circular, false, newData.radius);
                        specialSpellArgs.noProcess = true;
                    }
                }

                _qSpots.Add(Game.Time, end);
            }
        }

        private static void RemovePairsNear(Vector3 pos)
        {
            foreach (var pair in _qSpots.ToArray().Where(o => o.Value.Distance(pos) <= 30))
            {
                _qSpots.Remove(pair.Key);
            }
        }
    }
}
