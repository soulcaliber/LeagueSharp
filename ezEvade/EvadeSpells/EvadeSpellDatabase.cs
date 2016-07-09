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

            #region Akali

            //Spells.Add(
            //new EvadeSpellData
            //{
            //    charName = "Akali",
            //    dangerlevel = 4,
            //    name = "Twilight Shroud",
            //    spellName = "AkaliSmokeBomb",
            //    spellDelay = 850,
            //    spellKey = SpellSlot.W,
            //    speedArray = new[] { 20f, 40f, 60f, 80f, 100f },
            //    evadeType = EvadeType.MovementSpeedBuff,
            //    castType = CastType.Position
            //});

            #endregion

            #region Blitzcrank

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Blitzcrank",
                dangerlevel = 3,
                name = "Overdrive",
                spellName = "Overdrive",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 70f, 75f, 80f, 85f, 90f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self,
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

            #region Draven

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Draven",
                dangerlevel = 3,
                name = "Blood Rush",
                spellName = "DravenFury",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 40f, 45f, 50f, 55f, 60f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
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

            #region Evelynn

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Evelynn",
                dangerlevel = 3,
                name = "Darl Frenzy",
                spellName = "EvelynnW",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 30f, 45f, 50f, 60f, 70f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
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

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Fiora",
                dangerlevel = 3,
                name = "FioraQ",
                spellName = "FioraQ",
                range = 340,
                fixedRange = true,
                speed = 1100,
                spellDelay = 50,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.Dash,
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
                untargetable = true,
            });

            #endregion

            #region Galio

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Galio",
                dangerlevel = 4,
                name = "Righteous Gust",
                spellName = "GalioRighteousGust",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                speedArray = new[] { 30f, 35f, 40f, 45f, 50f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Position
            });

            #endregion

            #region Garen

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Garen",
                dangerlevel = 3,
                name = "Decisive Strike",
                spellName = "GarenQ",
                spellDelay = 50,
                spellKey = SpellSlot.Q,
                speedArray = new[] { 35, 35f, 35f, 35f, 35f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
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
                name = "GnarBigE",
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

            #region Karma

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Karma",
                dangerlevel = 3,
                name = "Inspire",
                spellName = "KarmaSolkimShield",
                spellDelay = 50,
                spellKey = SpellSlot.E,
                speedArray = new[] { 40f, 45f, 50f, 55f, 60f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Target
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

            #region Kayle

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Kayle",
                dangerlevel = 3,
                name = "Divine Blessing",
                spellName = "JudicatorDivineBlessing",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 18f, 21f, 24f, 27f, 30f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Target
            });

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

            #region Kennen

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Kennen",
                dangerlevel = 4,
                name = "Lightning Rush",
                spellName = "KennenLightningRush",
                spellDelay = 50,
                spellKey = SpellSlot.E,
                speedArray = new[] { 100f, 100f, 100f, 100f, 100f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
            });

            #endregion

            #region Kindred

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Kindred",
                dangerlevel = 1,
                name = "KindredQ",
                spellName = "KindredQ",
                range = 300,
                fixedRange = true,
                speed = 733,
                spellDelay = 50,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
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

            #region Lulu

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Lulu",
                dangerlevel = 3,
                name = "Whimsy",
                spellName = "LuluW",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 30f, 30f, 30f, 35f, 40f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Target
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
                untargetable = true,
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

            #region Nunu

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Nunu",
                dangerlevel = 2,
                name = "BloodBoil",
                spellName = "BloodBoil",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 8f, 9f, 10f, 11f, 12f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Target
            });

            #endregion

            #region Nidalee

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Nidalee",
                dangerlevel = 4,
                name = "Pounce",
                spellName = "Pounce",
                range = 375,
                spellDelay = 150,
                speed = 1750,
                spellKey = SpellSlot.W,
                evadeType = EvadeType.Dash,
                castType = CastType.Position,
                isSpecial = true
            });

            #endregion

            #region Poppy

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Poppy",
                dangerlevel = 3,
                name = "Steadfast Presence",
                spellName = "PoppyW",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 27f, 29f, 31f, 33f, 35f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
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
                isSpecial = true,
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

            #region Rumble

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Rumble",
                dangerlevel = 3,
                name = "Scrap Shield",
                spellName = "RumbleShield",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 10f, 15f, 20f, 25f, 30f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
            });

            #endregion

            #region Sivir

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Sivir",
                dangerlevel = 4,
                name = "On The Hunt",
                spellName = "SivirR",
                spellDelay = 250,
                spellKey = SpellSlot.R,
                speedArray = new[] { 60f, 60f, 60f, 60f, 60f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
            });

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

            #region Skarner

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Skarner",
                dangerlevel = 3,
                name = "Exoskeleton",
                spellName = "SkarnerExoskeleton",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 16f, 20f, 24f, 28f, 32f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
            });

            #endregion

            #region Shyvana

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Shyvana",
                dangerlevel = 3,
                name = "Burnout",
                spellName = "ShyvanaImmolationAura",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 30f, 35f, 40f, 45f, 50f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
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

            #region Sona

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Sona",
                dangerlevel = 3,
                name = "Song of Celerity",
                spellName = "SonaW",
                spellDelay = 50,
                spellKey = SpellSlot.E,
                speedArray = new[] { 13f, 14f, 15f, 16f, 25f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
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


            Spells.Add(
            new EvadeSpellData
            {
                charName = "Talon",
                dangerlevel = 4,
                name = "Shadow Assualt",
                spellName = "TalonShadowAssault",
                spellDelay = 50,
                spellKey = SpellSlot.R,
                speedArray = new[] { 40f, 40f, 40f, 40f, 40f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
            });

            #endregion

            #region Teemo

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Teemo",
                dangerlevel = 3,
                name = "Move Quick",
                spellName = "MoveQuick",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 10f, 14f, 18f, 22f, 26f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
            });

            #endregion

            #region Trundle
            Spells.Add(
            new EvadeSpellData
            {
                charName = "Trundle",
                dangerlevel = 4,
                name = "Frozen Domain",
                spellName = "TrundleW",
                spellDelay = 50,
                spellKey = SpellSlot.W,
                speedArray = new[] { 20f, 25f, 30f, 35f, 40f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Position
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
                spellDelay = 500,
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

            #region Udyr

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Udyr",
                dangerlevel = 3,
                name = "Bear Stance",
                spellName = "UdyrBearStance",
                spellDelay = 50,
                spellKey = SpellSlot.E,
                speedArray = new[] { 15f, 20f, 25f, 30f, 35f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Self
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

            #region Zillean

            Spells.Add(
            new EvadeSpellData
            {
                charName = "Zilean",
                dangerlevel = 3,
                name = "Timewarp",
                spellName = "ZileanE",
                spellDelay = 50,
                spellKey = SpellSlot.E,              
                speedArray = new[] { 40f, 55f, 70f, 85f, 99f },
                evadeType = EvadeType.MovementSpeedBuff,
                castType = CastType.Target     
            });

            #endregion

            #region AllChampions

            Spells.Add(
            new EvadeSpellData
            {
                charName = "AllChampions",
                dangerlevel = 4,
                name = "Talisman of Ascension",
                spellName = "TalismanOfAscension",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.MovementSpeedBuff,
                speedArray = new[] { 40f, 40f, 40f, 40f, 40f },
                castType = CastType.Self,
                isItem = true,
                itemID = ItemId.Talisman_of_Ascension
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "AllChampions",
                dangerlevel = 4,
                name = "Youmuu's Ghostblade",
                spellName = "YoumuusGhostblade",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                evadeType = EvadeType.MovementSpeedBuff,
                speedArray = new[] { 20f, 20f, 20f, 20f, 20f },
                castType = CastType.Self,
                isItem = true,
                itemID = ItemId.Youmuus_Ghostblade
            });

            Spells.Add(
            new EvadeSpellData
            {
                charName = "AllChampions",
                dangerlevel = 4,
                name = "Flash",
                spellName = "SummonerFlash",
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
