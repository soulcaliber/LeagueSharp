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
    class EvadeSpellDatabase
    {
        public static List<EvadeSpellData> Spells = new List<EvadeSpellData>();

        static EvadeSpellDatabase()
        {
            #region Caitlyn

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Caitlyn",
                dangerlevel = 3,
                name = "CaitlynEntrapment",
                spellName = "CaitlynEntrapment",
                range = 490,
                spellDelay = 0,
                speed = 1000,
                isReversed = true,
                fixedRange = true,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
            });

            #endregion
            
            #region Corki

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Corki",
                dangerlevel = 3,
                name = "CarpetBomb",
                spellName = "CarpetBomb",
                range = 790,
                spellDelay = 0,
                speed = 975,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.Dash,
            });

            #endregion
            
            #region Ezreal

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Ezreal",
                dangerlevel = 2,
                name = "ArcaneShift",
                spellName = "EzrealArcaneShift",
                range = 450,
                spellDelay = 150,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Blink,
            });

            #endregion

            #region Gragas

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Gragas",
                dangerlevel = 2,
                name = "BodySlam",
                spellName = "GragasBodySlam",
                range = 600,
                spellDelay = 0,
                speed = 900,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
            });

            #endregion

            #region Graves

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Graves",
                dangerlevel = 2,
                name = "QuickDraw",
                spellName = "GravesMove",
                range = 425,
                spellDelay = 0,
                speed = 1250,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
            });

            #endregion

            #region Kassadin

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Kassadin",
                dangerlevel = 1,
                name = "RiftWalk",
                range = 450,
                spellDelay = 150,
                spellKey = SpellSlot.R,
                evadeType = EvadeType.Blink,
            });

            #endregion

            #region Kayle

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Kayle",
                dangerlevel = 4,
                name = "Intervention",
                spellName = "JudicatorIntervention",
                spellDelay = 50,
                spellKey = SpellSlot.R,
                evadeType = EvadeType.SpellShield, //Invulnerability
            });

            #endregion

            #region Leblanc

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Leblanc",
                dangerlevel = 2,
                name = "Distortion",
                spellName = "LeblancSlide",
                range = 600,
                spellDelay = 0,
                speed = 1600,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.Dash,
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Leblanc",
                dangerlevel = 2,
                name = "DistortionR",
                spellName = "LeblancSlideM",
                checkSpellName = true,
                range = 600,
                spellDelay = 0,
                speed = 1600,
                spellKey = SpellSlot.R,
                evadeType = EvadeType.Dash,
            });
                        
            #endregion

            #region Lucian

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Lucian",
                dangerlevel = 1,
                name = "RelentlessPursuit",
                spellName = "LucianE",
                range = 425,
                spellDelay = 0,
                speed = 1350,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
            });

            #endregion

            #region Morgana

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Morgana",
                dangerlevel = 3,
                name = "BlackShield",
                spellName = "BlackShield",
                spellDelay = 50,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.SpellShield,
            });

            #endregion

            #region Nocturne

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Nocturne",
                dangerlevel = 3,
                name = "ShroudofDarkness",
                spellName = "NocturneShroudofDarkness",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.SpellShield,
            });

            #endregion

            #region Riven

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Riven",
                dangerlevel = 1,
                name = "BrokenWings",
                spellName = "RivenTriCleave",
                range = 260,
                fixedRange = true,
                spellDelay = 0,
                speed = 560,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.Dash,
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Riven",
                dangerlevel = 1,
                name = "Valor",
                spellName = "RivenFeint",
                range = 325,
                fixedRange = true,
                spellDelay = 0,
                speed = 1200,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
            });

            #endregion

            #region Sivir

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Sivir",
                dangerlevel = 2,
                name = "SivirE",
                spellName = "SivirE",
                spellDelay = 50,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.SpellShield,
            });

            #endregion

            #region Tristana

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Tristana",
                dangerlevel = 3,
                name = "RocketJump",
                spellName = "RocketJump",
                range = 900,
                spellDelay = 250,
                speed = 1100,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.Dash,
            });         

            #endregion

            #region Tryndamare

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Tryndamare",
                dangerlevel = 3,
                name = "SpinningSlash",
                spellName = "Slash",
                range = 660,
                spellDelay = 0,
                speed = 900,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.Dash,
            });    

            #endregion

            #region Vayne

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Vayne",
                dangerlevel = 1,
                name = "Tumble",
                spellName = "VayneTumble",
                range = 300,
                fixedRange = true,
                speed = 900,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.Dash,
            });

            #endregion

            Spells.Add(
            new EvadeSpellData
            {
                charName = "AllChampions",
                dangerlevel = 4,
                name = "Flash",
                spellName = "summonerflash",
                range = 400,
                spellDelay = 50,
                isSummonerSpell = true,
                spellKey = SpellSlot.R,
                evadeType = EvadeType.Blink,
            });
        }
    }
}
