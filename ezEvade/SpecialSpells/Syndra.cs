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
    class Syndra : ChampionPlugin
    {
        private const string _sphereName = "syndrasphere";
        private static readonly List<Obj_AI_Minion> _spheres = new List<Obj_AI_Minion>();
   
        static Syndra()
        {
            var syndra = HeroManager.Enemies.FirstOrDefault(x => x.ChampionName == "Syndra");
            if (syndra != null)
            {
                Game.OnUpdate += Game_OnUpdate;
                Obj_AI_Base.OnPlayAnimation += Obj_AI_Base_OnPlayAnimation;
                GameObject.OnCreate += GameObject_OnCreate;
                GameObject.OnDelete += GameObject_OnDelete;
            }
        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName.ToLower() == "syndrae")
            {
                SpellDetector.OnProcessSpecialSpell += SpellDetector_OnProcessSpecialSpell;
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            _spheres.RemoveAll(i => !i.IsValid || i.IsDead);
        }

        private static void Obj_AI_Base_OnPlayAnimation(Obj_AI_Base sender, GameObjectPlayAnimationEventArgs args)
        {
            var sphere = sender as Obj_AI_Minion;
            if (sphere != null && sphere.CharData.BaseSkinName == _sphereName &&
                sphere.Team == ObjectManager.Player.Team)
            {
                if (args.Animation == "Death")
                {
                    _spheres.RemoveAll(i => i.NetworkId == sphere.NetworkId);
                }
            }
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            var sphere = sender as Obj_AI_Minion;
            if (sphere != null && sphere.CharData.BaseSkinName == _sphereName &&
                sphere.Team == ObjectManager.Player.Team) 
            {
                if (!_spheres.Contains(sphere))
                {
                    _spheres.Add(sphere);
                }
            }
        }

        private static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            var sphere = sender as Obj_AI_Minion;
            if (sphere != null && sphere.CharData.BaseSkinName == _sphereName &&
                sphere.Team == ObjectManager.Player.Team)
            {
                _spheres.RemoveAll(i => i.NetworkId == sphere.NetworkId);
            }
        }

        private void SpellDetector_OnProcessSpecialSpell(Obj_AI_Base hero, GameObjectProcessSpellCastEventArgs args, SpellData spellData, SpecialSpellEventArgs specialSpellArgs)
        {
            if (spellData.spellName.ToLower() == "syndrae")
            {
                var estart = args.Start;
                var eend = args.End;

                foreach (var sphere in _spheres.Where(s => s.IsValid && !s.IsDead))
                {
                    // check if e whill hit the sphere
                    var proj = sphere.Position.To2D().ProjectOn(estart.To2D(), eend.To2D());
                    if (sphere.Position.To2D().Distance(proj.SegmentPoint) <= sphere.BoundingRadius + 110)
                    {
                        var start = sphere.Position;
                        var end = hero.ServerPosition + (sphere.Position - hero.ServerPosition).Normalized() * spellData.range;
                        SpellDetector.CreateSpellData(hero, start, end, spellData, sphere);
                    }
                }
            }
        }
    }
}
