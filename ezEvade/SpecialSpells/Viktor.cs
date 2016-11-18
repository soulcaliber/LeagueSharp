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
    class Viktor : ChampionPlugin
    {
        static Viktor()
        {

        }

        public void LoadSpecialSpell(SpellData spellData)
        {
            if (spellData.spellName == "ViktorDeathRay3")
            {
                Obj_AI_Minion.OnCreate += OnCreateObj_ViktorDeathRay3;
            }
        }

        private static void OnCreateObj_ViktorDeathRay3(GameObject obj, EventArgs args)
        {
            if (!obj.IsValid<MissileClient>())
                return;

            MissileClient missile = (MissileClient)obj;

            SpellData spellData;

            if (missile.SpellCaster != null && missile.SpellCaster.CheckTeam() &&
                missile.SData.Name != null && missile.SData.Name.ToLower() == "viktoreaugmissile"
                && SpellDetector.onMissileSpells.TryGetValue("viktordeathray3", out spellData)
                && missile.StartPosition != null && missile.EndPosition != null)
            {
                var newData = (SpellData) spellData.Clone();
                var missileDist = missile.EndPosition.To2D().Distance(missile.StartPosition.To2D());

                newData.spellDelay = missileDist / 1.5f + 1000;
                SpellDetector.CreateSpellData(missile.SpellCaster, missile.StartPosition, missile.EndPosition, newData);
            }
        }
    }
}
