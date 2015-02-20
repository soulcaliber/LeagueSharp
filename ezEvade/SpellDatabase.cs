using System.Collections.Generic;
using LeagueSharp;

namespace ezEvade
{
    public static class SpellDatabase
    {
        public static List<SpellData> Spells = new List<SpellData>();

        static SpellDatabase()
        {


            #region Aatrox

            Spells.Add(
            new SpellData
            {
                CharName = "Aatrox",
                Dangerlevel = 2,
                Name = "AatroxQ",
                ProjectileSpeed = 450,
                Radius = 145,
                Range = 650,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "AatroxQ",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Aatrox",
                Dangerlevel = 0,
                Name = "Blade of Torment",
                ProjectileSpeed = 1200,
                Radius = 100,
                Range = 1075,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "AatroxE",
                SpellType = SpellType.Line,

            });
            #endregion Aatrox

            #region Ahri

            Spells.Add(
            new SpellData
            {
                CharName = "Ahri",
                Dangerlevel = 1,
                MissileName = "AhriOrbMissile",
                Name = "Orb of Deception",
                ProjectileSpeed = 1750,
                Radius = 100,
                Range = 925,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "AhriOrbofDeception",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ahri",
                Dangerlevel = 2,
                MissileName = "AhriSeduceMissile",
                Name = "Charm",
                ProjectileSpeed = 1600,
                Radius = 60,
                Range = 1000,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "AhriSeduce",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ahri",
                Dangerlevel = 1,
                Name = "Orb of Deception Back",
                ProjectileSpeed = 915,
                Radius = 100,
                Range = 925,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "AhriOrbofDeception2",
                SpellType = SpellType.Line,

            });
            #endregion Ahri

            #region Alistar

            Spells.Add(
            new SpellData
            {
                CharName = "Alistar",
                Dangerlevel = 2,
                Name = "Pulverize",
                Radius = 365,
                Range = 365,
                SpellKey = SpellSlot.Q,
                SpellName = "Pulverize",
                SpellType = SpellType.Circular,

            });
            #endregion Alistar

            #region Amumu

            Spells.Add(
            new SpellData
            {
                CharName = "Amumu",
                Dangerlevel = 3,
                Name = "CurseoftheSadMummy",
                Radius = 550,
                Range = 550,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "CurseoftheSadMummy",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Amumu",
                Dangerlevel = 2,
                MissileName = "SadMummyBandageToss",
                Name = "Bandage Toss",
                ProjectileSpeed = 2000,
                Radius = 80,
                Range = 1100,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "BandageToss",
                SpellType = SpellType.Line,

            });
            #endregion Amumu

            #region Anivia

            Spells.Add(
            new SpellData
            {
                CharName = "Anivia",
                Dangerlevel = 2,
                MissileName = "FlashFrostSpell",
                Name = "Flash Frost",
                ProjectileSpeed = 600,
                Radius = 110,
                Range = 1100,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "FlashFrostSpell",
                SpellType = SpellType.Line,

            });
            #endregion Anivia

            #region Annie

            Spells.Add(
            new SpellData
            {
                Angle = 25,
                CharName = "Annie",
                Dangerlevel = 1,
                Name = "Incinerate",
                Radius = 80,
                Range = 625,
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "Incinerate",
                SpellType = SpellType.Cone,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Annie",
                Dangerlevel = 3,
                Name = "InfernalGuardian",
                Radius = 290,
                Range = 600,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "InfernalGuardian",
                SpellType = SpellType.Circular,

            });
            #endregion Annie

            #region Ashe

            Spells.Add(
            new SpellData
            {
                CharName = "Ashe",
                Dangerlevel = 3,
                Name = "Enchanted Arrow",
                ProjectileSpeed = 1600,
                Radius = 130,
                Range = 12500,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "EnchantedCrystalArrow",
                SpellType = SpellType.Line,

            });
            #endregion Ashe

            #region Azir

            Spells.Add(
            new SpellData
            {
                CharName = "Azir",
                Dangerlevel = 1,
                Name = "AzirQ",
                ProjectileSpeed = 1000,
                Radius = 80,
                Range = 800,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "AzirQ",
                SpellType = SpellType.Line,
                UsePackets = true,

            });
            #endregion Azir

            #region Blitzcrank

            Spells.Add(
            new SpellData
            {
                CharName = "Blitzcrank",
                Dangerlevel = 3,
                ExtraDelay = 75,
                MissileName = "RocketGrabMissile",
                Name = "Rocket Grab",
                ProjectileSpeed = 1800,
                Radius = 70,
                Range = 1050,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "RocketGrab",
                SpellType = SpellType.Line,

            });
            #endregion Blitzcrank

            #region Brand

            Spells.Add(
            new SpellData
            {
                CharName = "Brand",
                Dangerlevel = 2,
                MissileName = "BrandBlazeMissile",
                Name = "BrandBlaze",
                ProjectileSpeed = 1600,
                Radius = 70,
                Range = 1100,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "BrandBlaze",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Brand",
                Dangerlevel = 1,
                Name = "Pillar of Flame",
                Radius = 250,
                Range = 1100,
                SpellDelay = 825,
                SpellKey = SpellSlot.W,
                SpellName = "BrandFissure",
                SpellType = SpellType.Circular,

            });
            #endregion Brand

            #region Braum

            Spells.Add(
            new SpellData
            {
                CharName = "Braum",
                Dangerlevel = 3,
                Name = "GlacialFissure",
                ProjectileSpeed = 1125,
                Radius = 100,
                Range = 1250,
                SpellDelay = 500,
                SpellKey = SpellSlot.R,
                SpellName = "BraumRWrapper",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Braum",
                Dangerlevel = 2,
                MissileName = "BraumQMissile",
                Name = "BraumQ",
                ProjectileSpeed = 1200,
                Radius = 100,
                Range = 1000,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "BraumQ",
                SpellType = SpellType.Line,

            });
            #endregion Braum

            #region Caitlyn

            Spells.Add(
            new SpellData
            {
                CharName = "Caitlyn",
                Dangerlevel = 1,
                Name = "Piltover Peacemaker",
                ProjectileSpeed = 2200,
                Radius = 90,
                Range = 1300,
                SpellDelay = 625,
                SpellKey = SpellSlot.Q,
                SpellName = "CaitlynPiltoverPeacemaker",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Caitlyn",
                Dangerlevel = 1,
                MissileName = "CaitlynEntrapmentMissile",
                Name = "Caitlyn Entrapment",
                ProjectileSpeed = 2000,
                Radius = 80,
                Range = 950,
                SpellDelay = 125,
                SpellKey = SpellSlot.E,
                SpellName = "CaitlynEntrapment",
                SpellType = SpellType.Line,

            });
            #endregion Caitlyn

            #region Cassiopeia

            Spells.Add(
            new SpellData
            {
                Angle = 40,
                CharName = "Cassiopeia",
                Dangerlevel = 3,
                Name = "CassiopeiaPetrifyingGaze",
                Radius = 20,
                Range = 825,
                SpellDelay = 500,
                SpellKey = SpellSlot.R,
                SpellName = "CassiopeiaPetrifyingGaze",
                SpellType = SpellType.Cone,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Cassiopeia",
                Dangerlevel = 0,
                Name = "CassiopeiaNoxiousBlast",
                Radius = 170,
                Range = 600,
                SpellDelay = 625,
                SpellKey = SpellSlot.Q,
                SpellName = "CassiopeiaNoxiousBlast",
                SpellType = SpellType.Circular,

            });
            #endregion Cassiopeia

            #region Chogath

            Spells.Add(
            new SpellData
            {
                Angle = 30,
                CharName = "Chogath",
                Dangerlevel = 1,
                Name = "FeralScream",
                Radius = 20,
                Range = 650,
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "FeralScream",
                SpellType = SpellType.Cone,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Chogath",
                Dangerlevel = 2,
                Name = "Rupture",
                Radius = 275,
                Range = 950,
                SpellDelay = 825,
                SpellKey = SpellSlot.Q,
                SpellName = "Rupture",
                SpellType = SpellType.Circular,

            });
            #endregion Chogath

            #region Corki

            Spells.Add(
            new SpellData
            {
                CharName = "Corki",
                Dangerlevel = 0,
                MissileName = "MissileBarrageMissile2",
                Name = "Missile Barrage big",
                ProjectileSpeed = 2000,
                Radius = 40,
                Range = 1300,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "MissileBarrage2",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Corki",
                Dangerlevel = 1,
                MissileName = "PhosphorusBombMissile",
                Name = "Phosphorus Bomb",
                ProjectileSpeed = 1125,
                Radius = 270,
                Range = 825,
                SpellDelay = 500,
                SpellKey = SpellSlot.Q,
                SpellName = "PhosphorusBomb",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Corki",
                Dangerlevel = 0,
                MissileName = "MissileBarrageMissile",
                Name = "Missile Barrage",
                ProjectileSpeed = 2000,
                Radius = 40,
                Range = 1300,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "MissileBarrage",
                SpellType = SpellType.Line,

            });
            #endregion Corki

            #region Darius

            Spells.Add(
            new SpellData
            {
                Angle = 25,
                CharName = "Darius",
                Dangerlevel = 2,
                Name = "DariusAxeGrabCone",
                Radius = 20,
                Range = 570,
                SpellDelay = 320,
                SpellKey = SpellSlot.E,
                SpellName = "DariusAxeGrabCone",
                SpellType = SpellType.Cone,

            });
            #endregion Darius

            #region Diana

            Spells.Add(
            new SpellData
            {
                CharName = "Diana",
                Dangerlevel = 1,
                Name = "DianaArc",
                ProjectileSpeed = 1600,
                Radius = 195,
                Range = 895,
                RangeCap = true,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "DianaArc",
                SpellType = SpellType.Circular,

            });
            #endregion Diana

            #region DrMundo

            Spells.Add(
            new SpellData
            {
                CharName = "DrMundo",
                Dangerlevel = 0,
                MissileName = "InfectedCleaverMissile",
                Name = "Infected Cleaver",
                ProjectileSpeed = 2000,
                Radius = 60,
                Range = 1050,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "InfectedCleaverMissileCast",
                SpellType = SpellType.Line,

            });
            #endregion DrMundo

            #region Draven

            Spells.Add(
            new SpellData
            {
                CharName = "Draven",
                Dangerlevel = 2,
                MissileName = "DravenR",
                Name = "DravenR",
                ProjectileSpeed = 2000,
                Radius = 160,
                Range = 12500,
                SpellDelay = 500,
                SpellKey = SpellSlot.R,
                SpellName = "DravenRCast",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Draven",
                Dangerlevel = 1,
                MissileName = "DravenDoubleShotMissile",
                Name = "Stand Aside",
                ProjectileSpeed = 1400,
                Radius = 130,
                Range = 1100,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "DravenDoubleShot",
                SpellType = SpellType.Line,

            });
            #endregion Draven

            #region Elise

            Spells.Add(
            new SpellData
            {
                CharName = "Elise",
                Dangerlevel = 2,
                Name = "Cocoon",
                ProjectileSpeed = 1450,
                Radius = 70,
                Range = 1100,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "EliseHumanE",
                SpellType = SpellType.Line,

            });
            #endregion Elise

            #region Evelynn

            Spells.Add(
            new SpellData
            {
                CharName = "Evelynn",
                Dangerlevel = 2,
                Name = "EvelynnR",
                Radius = 250,
                Range = 650,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "EvelynnR",
                SpellType = SpellType.Circular,

            });
            #endregion Evelynn

            #region Ezreal

            Spells.Add(
            new SpellData
            {
                CharName = "Ezreal",
                Dangerlevel = 1,
                MissileName = "EzrealMysticShotMissile",
                Name = "Mystic Shot",
                ProjectileSpeed = 2000,
                Radius = 60,
                Range = 1200,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "EzrealMysticShot",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ezreal",
                Dangerlevel = 1,
                Name = "Trueshot Barrage",
                ProjectileSpeed = 2000,
                Radius = 160,
                Range = 20000,
                SpellDelay = 1000,
                SpellKey = SpellSlot.R,
                SpellName = "EzrealTrueshotBarrage",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ezreal",
                Dangerlevel = 0,
                MissileName = "EzrealEssenceFluxMissile",
                Name = "Essence Flux",
                ProjectileSpeed = 1600,
                Radius = 80,
                Range = 1050,
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "EzrealEssenceFlux",
                SpellType = SpellType.Line,

            });
            #endregion Ezreal

            #region Fizz

            Spells.Add(
            new SpellData
            {
                CharName = "Fizz",
                Dangerlevel = 2,
                MissileName = "FizzMarinerDoomMissile",
                Name = "Fizz ULT",
                ProjectileSpeed = 1350,
                Radius = 120,
                Range = 1275,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "FizzMarinerDoom",
                SpellType = SpellType.Line,

            });
            #endregion Fizz

            #region Galio

            Spells.Add(
            new SpellData
            {
                CharName = "Galio",
                Dangerlevel = 1,
                Name = "GalioRighteousGust",
                ProjectileSpeed = 1300,
                Radius = 120,
                Range = 1280,
                SpellKey = SpellSlot.E,
                SpellName = "GalioRighteousGust",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Galio",
                Dangerlevel = 1,
                Name = "GalioResoluteSmite",
                ProjectileSpeed = 1200,
                Radius = 235,
                Range = 1040,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "GalioResoluteSmite",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Galio",
                Dangerlevel = 3,
                Name = "GalioIdolOfDurand",
                Radius = 600,
                Range = 600,
                SpellKey = SpellSlot.R,
                SpellName = "GalioIdolOfDurand",
                SpellType = SpellType.Circular,

            });
            #endregion Galio

            #region Gnar

            Spells.Add(
            new SpellData
            {
                CharName = "Gnar",
                Dangerlevel = 1,
                Name = "Boulder Toss",
                ProjectileSpeed = 2000,
                Radius = 90,
                Range = 1150,
                SpellDelay = 500,
                SpellKey = SpellSlot.Q,
                SpellName = "gnarbigq",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Gnar",
                Dangerlevel = 2,
                Name = "GnarUlt",
                Radius = 500,
                Range = 500,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "GnarR",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Gnar",
                Dangerlevel = 2,
                Name = "Wallop",
                ProjectileSpeed = float.MaxValue,
                Radius = 100,
                Range = 600,
                SpellDelay = 600,
                SpellKey = SpellSlot.W,
                SpellName = "gnarbigw",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Gnar",
                Dangerlevel = 1,
                Name = "Boomerang Throw",
                ProjectileSpeed = 2400,
                Radius = 60,
                Range = 1185,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "GnarQ",
                SpellType = SpellType.Line,

            });
            #endregion Gnar

            #region Gragas

            Spells.Add(
            new SpellData
            {
                CharName = "Gragas",
                Dangerlevel = 1,
                Name = "Barrel Roll",
                ProjectileSpeed = 1000,
                Radius = 240,
                Range = 975,
                SpellDelay = 350,
                SpellKey = SpellSlot.Q,
                SpellName = "GragasQ",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Gragas",
                Dangerlevel = 3,
                Name = "GragasExplosiveCask",
                ProjectileSpeed = 1750,
                Radius = 350,
                Range = 1050,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "GragasR",
                SpellType = SpellType.Circular,

            });
            #endregion Gragas

            #region Graves

            Spells.Add(
            new SpellData
            {
                Angle = 18,
                CharName = "Graves",
                Dangerlevel = 1,
                IsThreeWay = true,
                MissileName = "GravesClusterShotAttack",
                Name = "Buckshot",
                ProjectileSpeed = 2000,
                Radius = 60,
                Range = 1025,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "GravesClusterShot",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Graves",
                Dangerlevel = 2,
                MissileName = "GravesChargeShotShot",
                Name = "Collateral Damage",
                ProjectileSpeed = 2100,
                Radius = 100,
                Range = 1000,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "GravesChargeShot",
                SpellType = SpellType.Line,

            });
            #endregion Graves

            #region Hecarim

            Spells.Add(
            new SpellData
            {
                CharName = "Hecarim",
                Dangerlevel = 2,
                Name = "HecarimUlt",
                ProjectileSpeed = 1100,
                Radius = 300,
                Range = 1500,
                SpellDelay = 10,
                SpellKey = SpellSlot.R,
                SpellName = "HecarimUlt",
                SpellType = SpellType.Circular,

            });
            #endregion Hecarim

            #region Heimerdinger

            Spells.Add(
            new SpellData
            {
                CharName = "Heimerdinger",
                Dangerlevel = 1,
                MissileName = "HeimerdingerESpell",
                Name = "HeimerdingerE",
                ProjectileSpeed = 1750,
                Radius = 135,
                Range = 925,
                SpellDelay = 325,
                SpellKey = SpellSlot.E,
                SpellName = "HeimerdingerE",
                SpellType = SpellType.Circular,

            });
            #endregion Heimerdinger

            #region Janna

            Spells.Add(
            new SpellData
            {
                CharName = "Janna",
                Dangerlevel = 1,
                MissileName = "HowlingGaleSpell",
                Name = "HowlingGaleSpell",
                ProjectileSpeed = 900,
                Radius = 120,
                Range = 1700,
                SpellKey = SpellSlot.Q,
                SpellName = "HowlingGale",
                SpellType = SpellType.Line,
                UsePackets = true,

            });
            #endregion Janna

            #region JarvanIV

            Spells.Add(
            new SpellData
            {
                CharName = "JarvanIV",
                Dangerlevel = 1,
                Name = "JarvanIVDragonStrike",
                ProjectileSpeed = 2000,
                Radius = 110,
                Range = 845,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "JarvanIVDragonStrike",
                SpellType = SpellType.Line,

            });
            #endregion JarvanIV

            #region Jayce

            Spells.Add(
            new SpellData
            {
                CharName = "Jayce",
                Dangerlevel = 1,
                MissileName = "JayceShockBlastWallMis",
                Name = "JayceShockBlastCharged",
                ProjectileSpeed = 2350,
                Radius = 70,
                Range = 1600,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "JayceShockBlastWall",
                SpellType = SpellType.Line,
                UsePackets = true,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Jayce",
                Dangerlevel = 1,
                MissileName = "JayceShockBlastMis",
                Name = "JayceShockBlast",
                ProjectileSpeed = 1450,
                Radius = 70,
                Range = 1050,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "jayceshockblast",
                SpellType = SpellType.Line,

            });
            #endregion Jayce

            #region Jinx

            Spells.Add(
            new SpellData
            {
                CharName = "Jinx",
                Dangerlevel = 2,
                Name = "JinxR",
                ProjectileSpeed = 1700,
                Radius = 120,
                Range = 25000,
                SpellDelay = 600,
                SpellKey = SpellSlot.R,
                SpellName = "JinxR",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Jinx",
                Dangerlevel = 2,
                MissileName = "JinxWMissile",
                Name = "Zap",
                ProjectileSpeed = 3300,
                Radius = 70,
                Range = 1500,
                SpellDelay = 600,
                SpellKey = SpellSlot.W,
                SpellName = "JinxWMissile",
                SpellType = SpellType.Line,

            });
            #endregion Jinx

            #region Kalista

            Spells.Add(
            new SpellData
            {
                CharName = "Kalista",
                Dangerlevel = 1,
                MissileName = "KalistaQMissile",
                Name = "KalistaQ",
                ProjectileSpeed = 2000,
                Radius = 70,
                Range = 1200,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "KalistaMysticShot",
                SpellType = SpellType.Line,

            });
            #endregion Kalista

            #region Karma

            Spells.Add(
            new SpellData
            {
                CharName = "Karma",
                Dangerlevel = 1,
                MissileName = "KarmaQMissile",
                Name = "KarmaQ",
                ProjectileSpeed = 1700,
                Radius = 90,
                Range = 1050,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "KarmaQ",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Karma",
                Dangerlevel = 1,
                MissileName = "KarmaQMissileMantra",
                Name = "KarmaQMantra",
                ProjectileSpeed = 1700,
                Radius = 90,
                Range = 1050,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "KarmaQMissileMantra",
                SpellType = SpellType.Line,
                UsePackets = true,

            });
            #endregion Karma

            #region Karthus

            Spells.Add(
            new SpellData
            {
                CharName = "Karthus",
                Dangerlevel = 0,
                Name = "Lay Waste",
                Radius = 180,
                Range = 875,
                SpellDelay = 900,
                SpellKey = SpellSlot.Q,
                SpellName = "karthuslaywastedeada3",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Karthus",
                Dangerlevel = 0,
                Name = "Lay Waste",
                Radius = 180,
                Range = 875,
                SpellDelay = 900,
                SpellKey = SpellSlot.Q,
                SpellName = "karthuslaywastedeada2",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Karthus",
                Dangerlevel = 0,
                Name = "Lay Waste",
                Radius = 180,
                Range = 875,
                SpellDelay = 900,
                SpellKey = SpellSlot.Q,
                SpellName = "karthuslaywastea2",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Karthus",
                Dangerlevel = 0,
                Name = "Lay Waste",
                Radius = 180,
                Range = 875,
                SpellDelay = 900,
                SpellKey = SpellSlot.Q,
                SpellName = "karthuslaywastedeada1",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Karthus",
                Dangerlevel = 0,
                Name = "Lay Waste",
                Radius = 180,
                Range = 875,
                SpellDelay = 900,
                SpellKey = SpellSlot.Q,
                SpellName = "KarthusLayWasteA1",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Karthus",
                Dangerlevel = 0,
                Name = "Lay Waste",
                Radius = 180,
                Range = 875,
                SpellDelay = 900,
                SpellKey = SpellSlot.Q,
                SpellName = "karthuslaywastea3",
                SpellType = SpellType.Circular,

            });
            #endregion Karthus

            #region Kassadin

            Spells.Add(
            new SpellData
            {
                CharName = "Kassadin",
                Dangerlevel = 0,
                Name = "RiftWalk",
                Radius = 150,
                Range = 700,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "RiftWalk",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                Angle = 40,
                CharName = "Kassadin",
                Dangerlevel = 1,
                Name = "ForcePulse",
                Radius = 20,
                Range = 700,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "ForcePulse",
                SpellType = SpellType.Cone,

            });
            #endregion Kassadin

            #region Kennen

            Spells.Add(
            new SpellData
            {
                CharName = "Kennen",
                Dangerlevel = 1,
                MissileName = "KennenShurikenHurlMissile1",
                Name = "Thundering Shuriken",
                ProjectileSpeed = 1700,
                Radius = 50,
                Range = 1175,
                SpellDelay = 180,
                SpellKey = SpellSlot.Q,
                SpellName = "KennenShurikenHurlMissile1",
                SpellType = SpellType.Line,

            });
            #endregion Kennen

            #region Khazix

            Spells.Add(
            new SpellData
            {
                CharName = "Khazix",
                Dangerlevel = 0,
                MissileName = "KhazixWMissile",
                Name = "KhazixW",
                ProjectileSpeed = 1700,
                Radius = 70,
                Range = 1100,
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "KhazixW",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                Angle = 22,
                CharName = "Khazix",
                Dangerlevel = 0,
                IsThreeWay = true,
                Name = "khazixwlong",
                ProjectileSpeed = 1700,
                Radius = 70,
                Range = 1025,
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "khazixwlong",
                SpellType = SpellType.Line,

            });
            #endregion Khazix

            #region KogMaw

            Spells.Add(
            new SpellData
            {
                CharName = "KogMaw",
                Dangerlevel = 0,
                Name = "Caustic Spittle",
                ProjectileSpeed = 1250,
                Radius = 60,
                Range = 1125,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "KogMawQ",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "KogMaw",
                Dangerlevel = 0,
                Name = "KogMawVoidOoze",
                ProjectileSpeed = 1400,
                Radius = 120,
                Range = 1360,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "KogMawVoidOoze",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "KogMaw",
                Dangerlevel = 1,
                Name = "Living Artillery",
                Radius = 175,
                Range = 2200,
                SpellDelay = 825,
                SpellKey = SpellSlot.R,
                SpellName = "KogMawLivingArtillery",
                SpellType = SpellType.Circular,

            });
            #endregion KogMaw

            #region Leblanc

            Spells.Add(
            new SpellData
            {
                CharName = "Leblanc",
                Dangerlevel = 1,
                Name = "Ethereal Chains R",
                ProjectileSpeed = 1600,
                Radius = 70,
                Range = 960,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "LeblancSoulShackleM",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Leblanc",
                Dangerlevel = 1,
                Name = "Ethereal Chains",
                ProjectileSpeed = 1600,
                Radius = 70,
                Range = 960,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "LeblancSoulShackle",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Leblanc",
                Dangerlevel = 0,
                Name = "LeblancSlideM",
                ProjectileSpeed = 1600,
                Radius = 250,
                Range = 725,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "LeblancSlideM",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Leblanc",
                Dangerlevel = 0,
                Name = "LeblancSlide",
                ProjectileSpeed = 1600,
                Radius = 250,
                Range = 725,
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "LeblancSlide",
                SpellType = SpellType.Circular,

            });
            #endregion Leblanc

            #region LeeSin

            Spells.Add(
            new SpellData
            {
                CharName = "LeeSin",
                Dangerlevel = 2,
                Name = "Sonic Wave",
                ProjectileSpeed = 1800,
                Radius = 60,
                Range = 1100,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "BlindMonkQOne",
                SpellType = SpellType.Line,

            });
            #endregion LeeSin

            #region Leona

            Spells.Add(
            new SpellData
            {
                CharName = "Leona",
                Dangerlevel = 3,
                Name = "Leona Solar Flare",
                Radius = 250,
                Range = 1200,
                SpellDelay = 825,
                SpellKey = SpellSlot.R,
                SpellName = "LeonaSolarFlare",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Leona",
                Dangerlevel = 2,
                ExtraDistance = 65,
                MissileName = "LeonaZenithBladeMissile",
                Name = "Zenith Blade",
                ProjectileSpeed = 2000,
                Radius = 95,
                Range = 975,
                SpellDelay = 200,
                SpellKey = SpellSlot.E,
                SpellName = "LeonaZenithBlade",
                SpellType = SpellType.Line,

            });
            #endregion Leona

            #region Lissandra

            Spells.Add(
            new SpellData
            {
                CharName = "Lissandra",
                Dangerlevel = 2,
                Name = "LissandraW",
                ProjectileSpeed = float.MaxValue,
                Radius = 450,
                Range = 725,
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "LissandraW",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Lissandra",
                Dangerlevel = 1,
                Name = "Ice Shard",
                ProjectileSpeed = 2250,
                Radius = 75,
                Range = 825,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "LissandraQ",
                SpellType = SpellType.Line,

            });
            #endregion Lissandra

            #region Lucian

            Spells.Add(
            new SpellData
            {
                CharName = "Lucian",
                Dangerlevel = 0,
                DefaultOff = true,
                Name = "LucianW",
                ProjectileSpeed = 1600,
                Radius = 80,
                Range = 1000,
                SpellDelay = 300,
                SpellKey = SpellSlot.W,
                SpellName = "LucianW",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Lucian",
                Dangerlevel = 1,
                DefaultOff = true,
                Name = "LucianQ",
                ProjectileSpeed = float.MaxValue,
                Radius = 65,
                Range = 1140,
                SpellDelay = 350,
                SpellKey = SpellSlot.Q,
                SpellName = "LucianQ",
                SpellType = SpellType.Line,

            });
            #endregion Lucian

            #region Lulu

            Spells.Add(
            new SpellData
            {
                CharName = "Lulu",
                Dangerlevel = 1,
                MissileName = "LuluQMissile",
                Name = "LuluQ",
                ProjectileSpeed = 1450,
                Radius = 80,
                Range = 925,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "LuluQ",
                SpellType = SpellType.Line,

            });
            #endregion Lulu

            #region Lux

            Spells.Add(
            new SpellData
            {
                CharName = "Lux",
                Dangerlevel = 1,
                Name = "LuxLightStrikeKugel",
                ProjectileSpeed = 1400,
                Radius = 285,
                Range = 1100,
                SpellDelay = 500,
                SpellKey = SpellSlot.E,
                SpellName = "LuxLightStrikeKugel",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Lux",
                Dangerlevel = 2,
                Name = "Lux Malice Cannon",
                ProjectileSpeed = float.MaxValue,
                Radius = 110,
                Range = 3500,
                SpellDelay = 1000,
                SpellKey = SpellSlot.R,
                SpellName = "LuxMaliceCannon",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Lux",
                Dangerlevel = 2,
                MissileName = "LuxLightBindingMis",
                Name = "Light Binding",
                ProjectileSpeed = 1200,
                Radius = 70,
                Range = 1300,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "LuxLightBinding",
                SpellType = SpellType.Line,

            });
            #endregion Lux

            #region Malphite

            Spells.Add(
            new SpellData
            {
                CharName = "Malphite",
                Dangerlevel = 3,
                Name = "UFSlash",
                ProjectileSpeed = 2000,
                Radius = 300,
                Range = 1000,
                SpellDelay = 0,
                SpellKey = SpellSlot.R,
                SpellName = "UFSlash",
                SpellType = SpellType.Circular,

            });
            #endregion Malphite

            #region Malzahar

            Spells.Add(
            new SpellData
            {
                CharName = "Malzahar",
                Dangerlevel = 1,
                ExtraEndTime = 750,
                IsWall = true,
                MissileName = "AlZaharCalloftheVoidMissile",
                Name = "AlZaharCalloftheVoid",
                ProjectileSpeed = float.MaxValue,
                Radius = 85,
                Range = 900,
                SideRadius = 400,
                SpellDelay = 1350,
                SpellKey = SpellSlot.Q,
                SpellName = "AlZaharCalloftheVoid",
                SpellType = SpellType.Line,

            });
            #endregion Malzahar

            #region MonkeyKing

            Spells.Add(
            new SpellData
            {
                CharName = "MonkeyKing",
                Dangerlevel = 2,
                Name = "MonkeyKingSpinToWin",
                Radius = 225,
                Range = 300,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "MonkeyKingSpinToWin",
                SpellType = SpellType.Circular,

            });
            #endregion MonkeyKing

            #region Morgana

            Spells.Add(
            new SpellData
            {
                CharName = "Morgana",
                Dangerlevel = 2,
                Name = "Dark Binding",
                ProjectileSpeed = 1200,
                Radius = 80,
                Range = 1300,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "DarkBindingMissile",
                SpellType = SpellType.Line,

            });
            #endregion Morgana

            #region Nami

            Spells.Add(
            new SpellData
            {
                CharName = "Nami",
                Dangerlevel = 2,
                MissileName = "NamiQMissile",
                Name = "NamiQ",
                ProjectileSpeed = 1500,
                Radius = 180,
                Range = 1625,
                SpellDelay = 525,
                SpellKey = SpellSlot.Q,
                SpellName = "NamiQ",
                SpellType = SpellType.Circular,

            });
            #endregion Nami

            #region Nautilus

            Spells.Add(
            new SpellData
            {
                CharName = "Nautilus",
                Dangerlevel = 2,
                MissileName = "NautilusAnchorDragMissile",
                Name = "Dredge Line",
                ProjectileSpeed = 2000,
                Radius = 80,
                Range = 1080,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "NautilusAnchorDrag",
                SpellType = SpellType.Line,

            });
            #endregion Nautilus

            #region Nidalee

            Spells.Add(
            new SpellData
            {
                CharName = "Nidalee",
                Dangerlevel = 1,
                Name = "Javelin Toss",
                ProjectileSpeed = 1300,
                Radius = 60,
                Range = 1500,
                SpellDelay = 125,
                SpellKey = SpellSlot.Q,
                SpellName = "JavelinToss",
                SpellType = SpellType.Line,

            });
            #endregion Nidalee

            #region Nocturne

            Spells.Add(
            new SpellData
            {
                CharName = "Nocturne",
                Dangerlevel = 0,
                Name = "NocturneDuskbringer",
                ProjectileSpeed = 1400,
                Radius = 60,
                Range = 1125,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "NocturneDuskbringer",
                SpellType = SpellType.Line,

            });
            #endregion Nocturne

            #region Olaf

            Spells.Add(
            new SpellData
            {
                CharName = "Olaf",
                Dangerlevel = 0,
                Name = "Undertow",
                ProjectileSpeed = 1600,
                Radius = 90,
                Range = 1000,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "OlafAxeThrowCast",
                SpellType = SpellType.Line,

            });
            #endregion Olaf

            #region Orianna

            Spells.Add(
            new SpellData
            {
                CharName = "Orianna",
                Dangerlevel = 1,
                HasEndExplosion = true,
                Name = "OrianaIzunaCommand",
                ProjectileSpeed = 1200,
                Radius = 110,
                Range = 1000,
                SpellDelay = 0,
                SpellKey = SpellSlot.Q,
                SpellName = "OrianaIzunaCommand",
                SpellType = SpellType.Circular,
                UsePackets = true,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Orianna",
                Dangerlevel = 3,
                Name = "OrianaDetonateCommand",
                Radius = 400,
                Range = 410,
                SpellDelay = 500,
                SpellKey = SpellSlot.R,
                SpellName = "OrianaDetonateCommand",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Orianna",
                Dangerlevel = 1,
                Name = "OrianaDissonanceCommand",
                Radius = 250,
                Range = 1825,
                SpellKey = SpellSlot.W,
                SpellName = "OrianaDissonanceCommand",
                SpellType = SpellType.Circular,

            });
            #endregion Orianna

            #region Pantheon

            Spells.Add(
            new SpellData
            {
                Angle = 35,
                CharName = "Pantheon",
                Dangerlevel = 1,
                Name = "Heartseeker",
                Radius = 100,
                Range = 650,
                SpellDelay = 1000,
                SpellKey = SpellSlot.E,
                SpellName = "PantheonE",
                SpellType = SpellType.Cone,

            });
            #endregion Pantheon

            #region Quinn

            Spells.Add(
            new SpellData
            {
                CharName = "Quinn",
                Dangerlevel = 1,
                MissileName = "QuinnQMissile",
                Name = "QuinnQ",
                ProjectileSpeed = 1550,
                Radius = 80,
                Range = 1050,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "QuinnQ",
                SpellType = SpellType.Line,

            });
            #endregion Quinn

            #region Rengar

            Spells.Add(
            new SpellData
            {
                CharName = "Rengar",
                Dangerlevel = 0,
                MissileName = "RengarEFinal",
                Name = "Bola Strike",
                ProjectileSpeed = 1500,
                Radius = 70,
                Range = 1000,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "RengarE",
                SpellType = SpellType.Line,

            });
            #endregion Rengar

            #region Riven

            Spells.Add(
            new SpellData
            {
                Angle = 15,
                CharName = "Riven",
                Dangerlevel = 1,
                IsThreeWay = true,
                Name = "WindSlash",
                ProjectileSpeed = 2200,
                Radius = 100,
                Range = 1075,
                SpellKey = SpellSlot.R,
                SpellName = "rivenizunablade",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Riven",
                Dangerlevel = 1,
                DefaultOff = true,
                Name = "RivenW",
                ProjectileSpeed = 1500,
                Radius = 280,
                Range = 650,
                SpellDelay = 267,
                SpellKey = SpellSlot.W,
                SpellName = "RivenMartyr",
                SpellType = SpellType.Circular,

            });
            #endregion Riven

            #region Rumble

            Spells.Add(
            new SpellData
            {
                CharName = "Rumble",
                Dangerlevel = 0,
                MissileName = "RumbleGrenadeMissile",
                Name = "RumbleGrenade",
                ProjectileSpeed = 2000,
                Radius = 90,
                Range = 950,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "RumbleGrenade",
                SpellType = SpellType.Line,

            });
            #endregion Rumble

            #region Sejuani

            Spells.Add(
            new SpellData
            {
                CharName = "Sejuani",
                Dangerlevel = 3,
                MissileName = "SejuaniGlacialPrison",
                Name = "SejuaniR",
                ProjectileSpeed = 1600,
                Radius = 110,
                Range = 1200,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "SejuaniGlacialPrisonCast",
                SpellType = SpellType.Line,

            });
            #endregion Sejuani

            #region Shen

            Spells.Add(
            new SpellData
            {
                CharName = "Shen",
                Dangerlevel = 2,
                Name = "ShadowDash",
                ProjectileSpeed = 1250,
                Radius = 75,
                Range = 700,
                SpellDelay = 0,
                SpellKey = SpellSlot.E,
                SpellName = "ShenShadowDash",
                SpellType = SpellType.Line,

            });
            #endregion Shen

            #region Shyvana

            Spells.Add(
            new SpellData
            {
                CharName = "Shyvana",
                Dangerlevel = 0,
                DefaultOff = true,
                Name = "ShyvanaFireball",
                ProjectileSpeed = 1700,
                Radius = 60,
                Range = 950,
                SpellKey = SpellSlot.E,
                SpellName = "ShyvanaFireball",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Shyvana",
                Dangerlevel = 2,
                Name = "ShyvanaTransformCast",
                ProjectileSpeed = 1100,
                Radius = 160,
                Range = 1000,
                SpellDelay = 10,
                SpellKey = SpellSlot.R,
                SpellName = "ShyvanaTransformCast",
                SpellType = SpellType.Line,

            });
            #endregion Shyvana

            #region Sivir

            Spells.Add(
            new SpellData
            {
                CharName = "Sivir",
                Dangerlevel = 1,
                MissileName = "SivirQMissile",
                Name = "Boomerang Blade",
                ProjectileSpeed = 1350,
                Radius = 100,
                Range = 1275,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "SivirQ",
                SpellType = SpellType.Line,

            });
            #endregion Sivir

            #region Skarner

            Spells.Add(
            new SpellData
            {
                CharName = "Skarner",
                Dangerlevel = 1,
                MissileName = "SkarnerFractureMissile",
                Name = "SkarnerFracture",
                ProjectileSpeed = 1400,
                Radius = 60,
                Range = 1000,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "SkarnerFracture",
                SpellType = SpellType.Line,

            });
            #endregion Skarner

            #region Sona

            Spells.Add(
            new SpellData
            {
                CharName = "Sona",
                Dangerlevel = 3,
                Name = "Crescendo",
                ProjectileSpeed = 2400,
                Radius = 150,
                Range = 1000,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "SonaR",
                SpellType = SpellType.Line,

            });
            #endregion Sona

            #region Soraka

            Spells.Add(
            new SpellData
            {
                CharName = "Soraka",
                Dangerlevel = 1,
                Name = "SorakaQ",
                ProjectileSpeed = 1100,
                Radius = 250,
                Range = 970,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "SorakaQ",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Soraka",
                Dangerlevel = 2,
                Name = "SorakaE",
                Radius = 275,
                Range = 925,
                SpellDelay = 1750,
                SpellKey = SpellSlot.E,
                SpellName = "SorakaE",
                SpellType = SpellType.Circular,

            });
            #endregion Soraka

            #region Swain

            Spells.Add(
            new SpellData
            {
                CharName = "Swain",
                Dangerlevel = 2,
                Name = "Nevermove",
                Radius = 250,
                Range = 900,
                SpellDelay = 925,
                SpellKey = SpellSlot.W,
                SpellName = "SwainShadowGrasp",
                SpellType = SpellType.Circular,

            });
            #endregion Swain

            #region Syndra

            Spells.Add(
            new SpellData
            {
                Angle = 30,
                CharName = "Syndra",
                Dangerlevel = 2,
                Name = "SyndraE",
                UsePackets = true,
                ProjectileSpeed = 1500,
                Radius = 140,
                Range = 800,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "SyndraE",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Syndra",
                Dangerlevel = 1,
                Name = "SyndraW",
                ProjectileSpeed = 1450,
                Radius = 180,
                Range = 925,
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "syndrawcast",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Syndra",
                Dangerlevel = 0,
                Name = "SyndraQ",
                Radius = 130,
                Range = 800,
                SpellDelay = 500,
                SpellKey = SpellSlot.Q,
                SpellName = "SyndraQ",
                SpellType = SpellType.Circular,

            });
            #endregion Syndra

            #region Talon

            Spells.Add(
            new SpellData
            {
                Angle = 20,
                CharName = "Talon",
                Dangerlevel = 1,
                IsThreeWay = true,
                Name = "TalonRake",
                ProjectileSpeed = 2300,
                Radius = 75,
                Range = 780,
                SpellKey = SpellSlot.W,
                SpellName = "TalonRake",
                SpellType = SpellType.Line,
                Splits = 3,

            });
            #endregion Talon

            #region Thresh

            Spells.Add(
            new SpellData
            {
                CharName = "Thresh",
                Dangerlevel = 2,
                MissileName = "ThreshQMissile",
                Name = "ThreshQ",
                ProjectileSpeed = 1900,
                Radius = 70,
                Range = 1100,
                SpellDelay = 500,
                SpellKey = SpellSlot.Q,
                SpellName = "ThreshQ",
                SpellType = SpellType.Line,

            });
            #endregion Thresh

            #region TwistedFate

            Spells.Add(
            new SpellData
            {
                Angle = 28,
                CharName = "TwistedFate",
                Dangerlevel = 1,
                IsThreeWay = true,
                MissileName = "SealFateMissile",
                Name = "Loaded Dice",
                ProjectileSpeed = 1000,
                Radius = 40,
                Range = 1575,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "WildCards",
                SpellType = SpellType.Line,

            });
            #endregion TwistedFate

            #region Urgot

            Spells.Add(
            new SpellData
            {
                CharName = "Urgot",
                Dangerlevel = 0,
                Name = "Acid Hunter",
                ProjectileSpeed = 1600,
                Radius = 60,
                Range = 1000,
                SpellDelay = 175,
                SpellKey = SpellSlot.Q,
                SpellName = "UrgotHeatseekingLineMissile",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Urgot",
                Dangerlevel = 1,
                Name = "Plasma Grenade",
                ProjectileSpeed = 1750,
                Radius = 150,
                Range = 900,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "UrgotPlasmaGrenade",
                SpellType = SpellType.Circular,

            });
            #endregion Urgot

            #region Varus

            Spells.Add(
            new SpellData
            {
                CharName = "Varus",
                Dangerlevel = 0,
                DefaultOff = true,
                Name = "Varus E",
                ProjectileSpeed = 1500,
                Radius = 275,
                Range = 925,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "VarusE",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Varus",
                Dangerlevel = 1,
                MissileName = "VarusQMissile",
                Name = "Varus Q Missile",
                ProjectileSpeed = 1900,
                Radius = 70,
                Range = 1600,
                SpellDelay = 0,
                SpellKey = SpellSlot.Q,
                SpellName = "varusq",
                SpellType = SpellType.Line,
                UsePackets = true,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Varus",
                Dangerlevel = 2,
                Name = "VarusR",
                ProjectileSpeed = 1950,
                Radius = 100,
                Range = 1200,
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "VarusR",
                SpellType = SpellType.Line,

            });
            #endregion Varus

            #region Veigar

            Spells.Add(
            new SpellData
            {
                CharName = "Veigar",
                Dangerlevel = 0,
                Name = "VeigarDarkMatter",
                Radius = 225,
                Range = 900,
                SpellDelay = 1250,
                SpellKey = SpellSlot.W,
                SpellName = "VeigarDarkMatter",
                SpellType = SpellType.Circular,

            });
            #endregion Veigar

            #region Velkoz

            Spells.Add(
            new SpellData
            {
                CharName = "Velkoz",
                Dangerlevel = 1,
                Name = "VelkozE",
                ProjectileSpeed = 1500,
                Radius = 200,
                Range = 950,
                SpellKey = SpellSlot.E,
                SpellName = "VelkozE",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Velkoz",
                Dangerlevel = 0,
                DefaultOff = true,
                Name = "VelkozW",
                ProjectileSpeed = 1200,
                Radius = 90,
                Range = 1100,
                SpellKey = SpellSlot.W,
                SpellName = "VelkozW",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Velkoz",
                Dangerlevel = 1,
                Name = "VelkozQMissileSplit",
                ProjectileSpeed = 2100,
                Radius = 90,
                Range = 900,
                SpellKey = SpellSlot.Q,
                SpellName = "VelkozQMissileSplit",
                SpellType = SpellType.Line,
                UsePackets = true,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Velkoz",
                Dangerlevel = 1,
                Name = "VelkozQ",
                ProjectileSpeed = 1300,
                Radius = 90,
                Range = 1200,
                SpellKey = SpellSlot.Q,
                MissileName = "VelkozQMissile",
                SpellName = "VelkozQ",
                SpellType = SpellType.Line,

            });
            #endregion Velkoz

            #region Vi

            Spells.Add(
            new SpellData
            {
                CharName = "Vi",
                Dangerlevel = 2,
                Name = "ViQMissile",
                ProjectileSpeed = 1500,
                Radius = 90,
                Range = 2000,
                SpellKey = SpellSlot.Q,
                SpellName = "ViQMissile",
                SpellType = SpellType.Line,
                UsePackets = true,

            });
            #endregion Vi

            #region Viktor

            Spells.Add(
            new SpellData
            {
                CharName = "Viktor",
                Dangerlevel = 1,
                MissileName = "ViktorDeathRayMissile",
                Name = "ViktorDeathRay",
                ProjectileSpeed = 780,
                Radius = 90,
                Range = 1100,
                SpellKey = SpellSlot.E,
                SpellName = "ViktorDeathRay",
                SpellType = SpellType.Line,
                UsePackets = true,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Viktor",
                Dangerlevel = 1,
                MissileName = "ViktorDeathRayMissile2",
                Name = "ViktorDeathRay",
                ProjectileSpeed = 780,
                Radius = 90,
                Range = 1100,
                SpellKey = SpellSlot.E,
                SpellName = "ViktorDeathRayFixMissileAugmented",
                SpellType = SpellType.Line,
                UsePackets = true,

            });
            #endregion Viktor

            #region Vladimir

            Spells.Add(
            new SpellData
            {
                CharName = "Vladimir",
                Dangerlevel = 2,
                Name = "VladimirHemoplague",
                Radius = 300,
                Range = 700,
                SpellDelay = 389,
                SpellKey = SpellSlot.R,
                SpellName = "VladimirHemoplague",
                SpellType = SpellType.Circular,

            });
            #endregion Vladimir

            #region Xerath

            Spells.Add(
            new SpellData
            {
                CharName = "Xerath",
                Dangerlevel = 1,
                Name = "XerathArcaneBarrage2",
                Radius = 200,
                Range = 1100,
                SpellDelay = 725,
                SpellKey = SpellSlot.W,
                SpellName = "XerathArcaneBarrage2",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Xerath",
                Dangerlevel = 0,
                Name = "XerathArcanopulse2",
                ProjectileSpeed = float.MaxValue,
                Radius = 80,
                Range = 1525,
                SpellDelay = 425,
                SpellKey = SpellSlot.Q,
                SpellName = "xeratharcanopulse2",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Xerath",
                Dangerlevel = 1,
                Name = "XerathLocusOfPower2",
                Radius = 200,
                Range = 5600,
                SpellDelay = 750,
                SpellKey = SpellSlot.R,
                SpellName = "xerathrmissilewrapper",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Xerath",
                Dangerlevel = 2,
                MissileName = "XerathMageSpearMissile",
                Name = "XerathMageSpear",
                ProjectileSpeed = 1600,
                Radius = 60,
                Range = 1125,
                SpellKey = SpellSlot.E,
                SpellName = "XerathMageSpear",
                SpellType = SpellType.Line,

            });
            #endregion Xerath

            #region Yasuo

            Spells.Add(
            new SpellData
            {
                CharName = "Yasuo",
                Dangerlevel = 2,
                Name = "Steel Tempest3",
                ProjectileSpeed = 1500,
                Radius = 90,
                Range = 1025,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "yasuoq3w",
                SpellType = SpellType.Line,

            });
            #endregion Yasuo

            #region Zac
            #endregion Zac

            #region Zed

            Spells.Add(
            new SpellData
            {
                CharName = "Zed",
                Dangerlevel = 1,
                Name = "ZedShuriken",
                ProjectileSpeed = 1700,
                Radius = 50,
                Range = 925,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "ZedShuriken",
                SpellType = SpellType.Line,

            });
            #endregion Zed

            #region Ziggs

            Spells.Add(
            new SpellData
            {
                CharName = "Ziggs",
                Dangerlevel = 0,
                Name = "ZiggsE",
                ProjectileSpeed = 3000,
                Radius = 235,
                Range = 2000,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "ZiggsE",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ziggs",
                Dangerlevel = 0,
                Name = "ZiggsW",
                ProjectileSpeed = 3000,
                Radius = 210,
                Range = 2000,
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "ZiggsW",
                SpellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ziggs",
                Dangerlevel = 1,
                Name = "ZiggsQ",
                ProjectileSpeed = 1700,
                Radius = 90,
                Range = 850,
                RangeCap = true,
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "ZiggsQ",
                SpellType = SpellType.Circular,

            });
            #endregion Ziggs

            #region Zyra

            Spells.Add(
            new SpellData
            {
                CharName = "Zyra",
                Dangerlevel = 2,
                Name = "Grasping Roots",
                ProjectileSpeed = 1150,
                Radius = 80,
                Range = 1150,
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "ZyraGraspingRoots",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Zyra",
                Dangerlevel = 1,
                MissileName = "ZyraPassiveDeathMissile",
                Name = "Zyra Passive",
                ProjectileSpeed = 2000,
                Radius = 80,
                Range = 1474,
                SpellDelay = 500,
                SpellKey = SpellSlot.Q,
                SpellName = "zyrapassivedeathmanager",
                SpellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Zyra",
                Dangerlevel = 1,
                Name = "Deadly Bloom",
                Radius = 220,
                Range = 825,
                SpellDelay = 750,
                SpellKey = SpellSlot.Q,
                SpellName = "ZyraQFissure",
                SpellType = SpellType.Circular,

            });
            #endregion Zyra
        }
    }
}
