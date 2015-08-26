using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            #region Ahri

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Ahri",
                dangerlevel = 4,
                name = "AhriTumble",
                spellName = "AhriTumble",
                range = 500,
                spellDelay = 50,
                speed = 1575,
                spellKey = SpellSlot.R,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
            });

            #endregion

            #region Caitlyn

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Caitlyn",
                dangerlevel = 3,
                name = "CaitlynEntrapment",
                spellName = "CaitlynEntrapment",
                range = 490,
                spellDelay = 50,
                speed = 1000,
                isReversed = true,
                fixedRange = true,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
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
                spellDelay = 50,
                speed = 975,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
            });

            #endregion

            #region Ekko

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Ekko",
                dangerlevel = 3,
                name = "PhaseDive",
                spellName = "EkkoE",
                range = 350,
                fixedRange = true,
                spellDelay = 50,
                speed = 1150,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Ekko",
                dangerlevel = 3,
                name = "PhaseDive2",
                spellName = "EkkoEAttack",
                range = 490,
                spellDelay = 250,
                infrontTarget = true,
                spellKey = SpellSlot.Recall,
                evadeType = EvadeType.Blink,                
                castType = CastType.Target,                
                spellTargets = new[] { SpellTargets.EnemyChampions, SpellTargets.EnemyMinions },
                isSpecial = true,
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Ekko",
                dangerlevel = 4,
                name = "Chronobreak",
                spellName = "EkkoR",
                range = 20000,
                spellDelay = 50,
                spellKey = SpellSlot.R,
                evadeType = EvadeType.Blink,
                castType = CastType.Self,
                isSpecial = true,
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
                spellDelay = 250,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Blink,
                castType = CastType.Position,
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
                spellDelay = 50,
                speed = 900,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
            });

            #endregion

            #region Gnar

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Gnar",
                dangerlevel = 3,
                name = "GnarE",
                spellName = "GnarE",
                range = 475,
                spellDelay = 50,
                speed = 900,
                checkSpellName = true,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Gnar",
                dangerlevel = 4,
                name = "GnarE",
                spellName = "gnarbige",
                range = 475,
                spellDelay = 50,
                speed = 800,
                checkSpellName = true,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
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
                spellDelay = 50,
                speed = 1250,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
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
                spellDelay = 250,
                spellKey = SpellSlot.R,
                evadeType = EvadeType.Blink,
                castType = CastType.Position,
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
                spellDelay = 250,
                spellKey = SpellSlot.R,
                evadeType = EvadeType.SpellShield, //Invulnerability
                castType = CastType.Target,
                spellTargets = new[] { SpellTargets.AllyChampions },
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
                spellDelay = 50,
                speed = 1600,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
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
                spellDelay = 50,
                speed = 1600,
                spellKey = SpellSlot.R,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
            });
                        
            #endregion

            #region LeeSin

            Spells.Add(
            new EvadeSpellData
            {
                charName = "LeeSin",
                dangerlevel = 3,
                name = "LeeSinW",
                spellName = "BlindMonkWOne",
                range = 700,
                speed = 1400,
                spellDelay = 50,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.Dash,
                castType = CastType.Target,
                spellTargets = new[] { SpellTargets.AllyChampions, SpellTargets.AllyMinions },
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
                spellDelay = 50,
                speed = 1350,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
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
                castType = CastType.Target,
                spellTargets = new[] { SpellTargets.AllyChampions },
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
                castType = CastType.Self,
            });

            #endregion

            #region Fiora

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Fiora",
                dangerlevel = 3,
                name = "FioraW",
                spellName = "FioraW",
                range = 750,
                spellDelay = 100,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.WindWall,
                castType = CastType.Position,
            });

            #endregion

            #region Fizz

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Fizz",
                dangerlevel = 3,
                name = "FizzPiercingStrike",
                spellName = "FizzPiercingStrike",
                range = 550,
                speed = 1400,
                fixedRange = true,
                spellDelay = 50,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.Dash,
                castType = CastType.Target,
                spellTargets = new[] { SpellTargets.EnemyMinions, SpellTargets.EnemyChampions },
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Fizz",
                dangerlevel = 3,
                name = "FizzJump",
                spellName = "FizzJump",
                range = 400,
                speed = 1400,
                fixedRange = true,
                spellDelay = 50,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
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
                spellDelay = 50,
                speed = 560,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
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
                spellDelay = 50,
                speed = 1200,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
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
                castType = CastType.Self,
            });

            #endregion

            #region Shaco

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Shaco",
                dangerlevel = 3,
                name = "Deceive",
                spellName = "Deceive",
                range = 400,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.Blink,
                castType = CastType.Position,
            });

            /*Spells.Add(
            new EvadeSpellData
            {
                charName = "Shaco",
                dangerlevel = 3,
                name = "JackInTheBox",
                spellName = "JackInTheBox",
                range = 425,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.WindWall,
                castType = CastType.Position,
            });*/

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
                castType = CastType.Position,
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
                spellDelay = 50,
                speed = 900,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
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
                spellDelay = 50,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
            });

            #endregion

            #region Yasuo

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Yasuo",
                dangerlevel = 2,
                name = "SweepingBlade",
                spellName = "YasuoDashWrapper",
                range = 475,
                fixedRange = true,
                speed = 1000,
                spellDelay = 50,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Dash,
                castType = CastType.Target,
                spellTargets = new[] { SpellTargets.EnemyChampions, SpellTargets.EnemyMinions },
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Yasuo",
                dangerlevel = 3,
                name = "WindWall",
                spellName = "YasuoWMovingWall",
                range = 400,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.WindWall,
                castType = CastType.Position,
            });

            #endregion

            #region MasterYi

            Spells.Add(
            new EvadeSpellData
            {
                charName = "MasterYi",
                dangerlevel = 3,
                name = "AlphaStrike",
                spellName = "AlphaStrike",
                range = 600,
                speed = float.MaxValue,
                spellDelay = 100,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.Blink,
                castType = CastType.Target,
                spellTargets = new[] { SpellTargets.EnemyChampions, SpellTargets.EnemyMinions },
            });

            #endregion

            #region Katarina

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Katarina",
                dangerlevel = 3,
                name = "KatarinaE",
                spellName = "KatarinaE",
                range = 700,
                speed = float.MaxValue,
                spellDelay = 50,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Blink, //behind target
                castType = CastType.Target,
                spellTargets = new[] { SpellTargets.Targetables },
            });

            #endregion

            #region Talon

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Talon",
                dangerlevel = 3,
                name = "Cutthroat",
                spellName = "TalonCutthroat",
                range = 700,
                speed = float.MaxValue,
                spellDelay = 50,
                spellKey = SpellSlot.E,
                evadeType = EvadeType.Blink, //behind target
                castType = CastType.Target,
                spellTargets = new[] { SpellTargets.EnemyChampions, SpellTargets.EnemyMinions },
            });

            #endregion

            #region AllChampions

            Spells.Add(
            new EvadeSpellData
            {
                charName = "AllChampions",
                dangerlevel = 4,
                name = "Flash",
                spellName = "summonerflash",
                range = 400,
                fixedRange = true, //test
                spellDelay = 50,
                isSummonerSpell = true,
                spellKey = SpellSlot.R,
                evadeType = EvadeType.Blink,
                castType = CastType.Position,
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "AllChampions",
                dangerlevel = 4,
                name = "Hourglass",
                spellName = "ZhonyasHourglass",
                spellDelay = 50,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.SpellShield, //Invulnerability
                castType = CastType.Self,
                isItem = true,
                itemID = ItemId.Zhonyas_Hourglass,
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "AllChampions",
                dangerlevel = 4,
                name = "Witchcap",
                spellName = "Witchcap",
                spellDelay = 50,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.SpellShield, //Invulnerability
                castType = CastType.Self,
                isItem = true,
                itemID = ItemId.Wooglets_Witchcap,
            });

            #endregion AllChampions
        }
    }
}
