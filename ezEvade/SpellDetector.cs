using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    internal class SpellDetector
    {
        public delegate void OnCreateSpellHandler(Spell spell);

        //public static event OnDeleteSpellHandler OnDeleteSpell;

        public static Dictionary<int, Spell> Spells = new Dictionary<int, Spell>();
        public static Dictionary<int, Spell> DrawSpells = new Dictionary<int, Spell>();
        public static Dictionary<string, SpellData> OnProcessSpells = new Dictionary<string, SpellData>();
        public static Dictionary<string, SpellData> OnMissileSpells = new Dictionary<string, SpellData>();
        public static Dictionary<string, SpellData> WindupSpells = new Dictionary<string, SpellData>();
        private static int _spellIdCount;
        private static Menu _menu;

        public SpellDetector(Menu mainMenu)
        {
            GameObject.OnCreate += SpellMissile_OnCreate;
            GameObject.OnDelete += SpellMissile_OnDelete;
            Obj_AI_Base.OnProcessSpellCast += Game_ProcessSpell;

            _menu = mainMenu;
            Game_OnGameLoad();
        }

        private static Obj_AI_Hero MyHero
        {
            get { return ObjectManager.Player; }
        }

        private static float GameTime
        {
            get { return Game.ClockTime*1000; }
        }

        public static event OnCreateSpellHandler OnCreateSpell;

        private void Game_OnGameLoad()
        {
            //Game.PrintChat("SpellDetector loaded");
            //menu.AddSubMenu(new Menu("SpellDetector", "SpellDetector"));

            LoadSpellDictionary();
        }

        private void SpellMissile_OnCreate(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid<Obj_SpellMissile>())
            {
                return;
            }

            var missile = (Obj_SpellMissile) obj;
            SpellData spellData;

            if (missile.SpellCaster == null || missile.SpellCaster.Team == MyHero.Team || missile.SData.Name == null ||
                !OnMissileSpells.TryGetValue(missile.SData.Name, out spellData) || missile.StartPosition == null ||
                missile.EndPosition == null)
            {
                return;
            }

            if (!(missile.StartPosition.Distance(MyHero.Position) < spellData.Range + 1000))
            {
                return;
            }

            var hero = missile.SpellCaster;
            if (hero.IsVisible)
            {
                if (spellData.UsePackets)
                {
                    CreateSpellData(hero, missile.StartPosition, missile.EndPosition, spellData, obj);
                    return;
                }

                foreach (
                    var spell in
                        Spells.Select(entry => entry.Value).Where(spell => spell.Info.MissileName == missile.SData.Name
                                                                           &&
                                                                           spell.HeroId == missile.SpellCaster.NetworkId)
                    )
                {
                    spell.SpellObject = obj;
                }
            }
            else
            {
                CreateSpellData(hero, missile.StartPosition, missile.EndPosition, spellData, obj);
            }
        }

        private void SpellMissile_OnDelete(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid<Obj_SpellMissile>())
            {
                return;
            }

            //var missile = (Obj_SpellMissile) obj;
            //SpellData spellData;

            foreach (var spell in Spells.Values.ToList().Where(
                s => (s.SpellObject != null && s.SpellObject.NetworkId == obj.NetworkId))) // IsAlive
            {
                DeleteSpell(spell.SpellId);
            }
        }

        public void RemoveNonDangerousSpells()
        {
            foreach (var spell in Spells.Values.ToList().Where(
                s => (s.Info.Dangerlevel < 2)))
            {
                DeleteSpell(spell.SpellId);
            }
        }

        private void Game_ProcessSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args)
        {
            SpellData spellData;
            if (hero.Team == MyHero.Team || !OnProcessSpells.TryGetValue(args.SData.Name, out spellData))
            {
                return;
            }

            if (spellData.UsePackets == false)
            {
                CreateSpellData(hero, args.Start, args.End, spellData, null);
            }
        }

        private void CreateSpellData(Obj_AI_Base hero, Vector3 startStartPos, Vector3 spellEndPos, SpellData spellData,
            GameObject obj)
        {
            if (!(startStartPos.Distance(MyHero.Position) < spellData.Range + 1000))
            {
                return;
            }

            var startPosition = startStartPos.To2D();
            var endPosition = spellEndPos.To2D();
            var direction = (endPosition - startPosition).Normalized();
            float endTick = 0;

            if (spellData.RangeCap) // For diana q
            {
                if (endPosition.Distance(startPosition) > spellData.Range)
                {
                    //var heroCastPos = hero.ServerPosition.To2D();
                    //direction = (endPosition - heroCastPos).Normalized();
                    endPosition = startPosition + direction*spellData.Range;
                }
            }

            switch (spellData.SpellType)
            {
                case SpellType.Line:
                    endTick = spellData.SpellDelay + (spellData.Range/spellData.ProjectileSpeed)*1000;
                    endPosition = startPosition + direction*spellData.Range;

                    if (obj != null)
                        endTick -= spellData.SpellDelay;
                    break;
                case SpellType.Circular:
                    endTick = spellData.SpellDelay;

                    if (spellData.ProjectileSpeed == 0)
                    {
                        endPosition = hero.ServerPosition.To2D();
                    }
                    else if (spellData.ProjectileSpeed > 0)
                    {
                        endTick = endTick + 1000*startPosition.Distance(endPosition)/spellData.ProjectileSpeed;
                    }
                    break;
                case SpellType.Cone:
                    break;
            }

            var newSpell = new Spell
            {
                StartTime = GameTime,
                EndTime = GameTime + endTick,
                StartPos = startPosition,
                EndPos = endPosition,
                Direction = direction,
                HeroId = hero.NetworkId,
                Info = spellData
            };


            if (obj != null)
            {
                newSpell.SpellObject = obj;
                newSpell.ProjectileId = obj.NetworkId;
            }

            var spellId = CreateSpell(newSpell);
            Utility.DelayAction.Add((int) endTick, () => DeleteSpell(spellId));
        }

        private static int CreateSpell(Spell newSpell)
        {
            var spellId = _spellIdCount++;
            newSpell.SpellId = spellId;

            DrawSpells.Add(spellId, newSpell);

            if (Evade.IsDodgeDangerousEnabled() && newSpell.Info.Dangerlevel < 2)
            {
                return spellId;
            }

            Spells.Add(spellId, newSpell);
            if (OnCreateSpell != null)
            {
                OnCreateSpell(newSpell);
            }

            return spellId;
        }

        private static void DeleteSpell(int spellId)
        {
            Spells.Remove(spellId);
            DrawSpells.Remove(spellId);
        }

        public static Vector2 GetCurrentSpellPosition(Spell spell)
        {
            var spellPos = spell.StartPos;
            if (GameTime > spell.StartTime + spell.Info.SpellDelay)
            {
                spellPos = spell.StartPos +
                           spell.Direction*spell.Info.ProjectileSpeed*
                           (GameTime - spell.StartTime - spell.Info.SpellDelay)/1000;
            }

            if (spell.SpellObject != null)
            {
                spellPos = spell.SpellObject.Position.To2D();
            }
            return spellPos;
        }

        private static void LoadSpellDictionary()
        {
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>())
            {
                if (hero.IsMe)
                {
                    foreach (var spell in SpellDatabase.Spells.Where(
                        s => (s.CharName == hero.ChampionName))
                        .Where(spell => !WindupSpells.ContainsKey(spell.SpellName)))
                    {
                        WindupSpells.Add(spell.SpellName, spell);
                    }
                }

                if (hero.Team == MyHero.Team)
                {
                    continue;
                }

                foreach (var spell in SpellDatabase.Spells.Where(
                    s => (s.CharName == hero.ChampionName && s.DefaultOff == false)))
                {
                    //Game.PrintChat(spell.spellName);

                    if (spell.MissileName == string.Empty)
                    {
                        spell.MissileName = spell.SpellName;
                    }

                    if (OnProcessSpells.ContainsKey(spell.SpellName))
                    {
                        continue;
                    }

                    OnProcessSpells.Add(spell.SpellName, spell);
                    OnMissileSpells.Add(spell.MissileName, spell);
                }
            }
        }
    }
}