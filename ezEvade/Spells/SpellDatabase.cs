using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

//TODO: Add Cone detection

namespace ezEvade
{
    public static class SpellDatabase
    {
        public static List<SpellData> Spells = new List<SpellData>();

        static SpellDatabase()
        {
            #region AllChampions

            Spells.Add(
            new SpellData
            {
                charName = "AllChampions",
                dangerlevel = 1,
                missileName = "summonersnowball",
                name = "Mark",
                projectileSpeed = 1300,
                radius = 60,
                range = 1600,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "summonersnowball",
                extraSpellNames = new[] { "summonerporothrow", },
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            #endregion AllChampions

            #region Aatrox

            Spells.Add(
            new SpellData
            {
                charName = "Aatrox",
                dangerlevel = 3,
                missileName = "AatroxQ",
                name = "Dark Flight",
                projectileSpeed = 450,
                radius = 285,
                range = 650,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "AatroxQ",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Aatrox",
                dangerlevel = 2,
                missileName = "AatroxE",
                name = "Blade of Torment",
                projectileSpeed = 1200,
                radius = 100,
                range = 1075,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "AatroxE",
                spellType = SpellType.Line,

            });
            #endregion Aatrox

            #region Ahri

            Spells.Add(
            new SpellData
            {
                charName = "Ahri",
                dangerlevel = 2,
                missileName = "AhriOrbMissile",
                name = "Orb of Deception",
                projectileSpeed = 1750,
                radius = 100,
                range = 925,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "AhriOrbofDeception",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ahri",
                dangerlevel = 3,
                missileName = "AhriSeduceMissile",
                name = "Charm",
                projectileSpeed = 1550,
                radius = 60,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "AhriSeduce",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ahri",
                dangerlevel = 3,
                missileName = "AhriOrbofDeception2",
                name = "Orb of Deception Back",
                projectileSpeed = 915,
                radius = 100,
                range = 925,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "AhriOrbofDeception2",
                spellType = SpellType.Line,

            });
            #endregion Ahri

            #region Alistar

            Spells.Add(
            new SpellData
            {
                charName = "Alistar",
                defaultOff = true,
                dangerlevel = 3,
                missileName = "Pulverize",
                name = "Pulverize",
                projectileSpeed = float.MaxValue,
                radius = 365,
                range = 365,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "Pulverize",
                spellType = SpellType.Circular,

            });
            #endregion Alistar

            #region Amumu

            Spells.Add(
            new SpellData
            {
                charName = "Amumu",
                dangerlevel = 4,
                missileName = "CurseoftheSadMummy",
                name = "Curse of the Sad Mummy",
                projectileSpeed = float.MaxValue,
                radius = 560,
                range = 560,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "CurseoftheSadMummy",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Amumu",
                dangerlevel = 3,
                missileName = "SadMummyBandageToss",
                name = "Bandage Toss",
                projectileSpeed = 2000,
                radius = 80,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BandageToss",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Amumu

            #region Anivia

            Spells.Add(
            new SpellData
            {
                charName = "Anivia",
                dangerlevel = 3,
                missileName = "FlashFrostSpell",
                name = "Flash Frost",
                projectileSpeed = 850,
                radius = 110,
                range = 1250,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "FlashFrostSpell",
                spellType = SpellType.Line,

            });
            #endregion Anivia

            #region Annie

            Spells.Add(
            new SpellData
            {
                charName = "Annie",
                dangerlevel = 2,
                missileName = "Incinerate",
                name = "Incinerate",
                projectileSpeed = float.MaxValue,
                radius = 80,
                range = 625,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "Incinerate",
                spellType = SpellType.Cone,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Annie",
                dangerlevel = 4,
                missileName = "Incinerate",
                name = "Summom: Tibbers",
                projectileSpeed = float.MaxValue,
                radius = 290,
                range = 600,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "InfernalGuardian",
                spellType = SpellType.Circular,
                defaultOff = true
            });

            #endregion Annie

            #region Ashe

            Spells.Add(
            new SpellData
            {
                charName = "Ashe",
                dangerlevel = 3,
                missileName = "EnchantedCrystalArrow",
                name = "Enchanted Crystal Arrow",
                projectileSpeed = 1600,
                radius = 130,
                range = 25000,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "EnchantedCrystalArrow",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions },
            });

            Spells.Add(
            new SpellData
            {
                angle = 5,
                charName = "Ashe",
                dangerlevel = 2,
                name = "Volley",
                projectileSpeed = 1500,
                radius = 20,
                range = 1400,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "Volley",
                spellType = SpellType.Line,
                isSpecial = true,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
            });
            #endregion Ashe

            #region Aurelion Sol

            Spells.Add(
            new SpellData
            {
                charName = "AurelionSol",
                dangerlevel = 2,
                missileName = "AurelionSolQMissile",
                name = "Starsurge",
                projectileSpeed = 850,
                radius = 180,
                range = 1500,
                fixedRange = true,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "AurelionSolQ",
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "AurelionSol",
                dangerlevel = 3,
                missileName = "AurelionSolRBeamMissile",
                name = "Voice of Light",
                projectileSpeed = 4600,
                radius = 120,
                range = 1420,
                fixedRange = true,
                spellDelay = 300,
                spellKey = SpellSlot.R,
                spellName = "AurelionSolR",
                spellType = SpellType.Line
            });

            #endregion Aurelion Sol

            #region Azir

            Spells.Add(
            new SpellData
            {
                charName = "Azir",
                dangerlevel = 2,
                name = "Conquering Sands",
                projectileSpeed = 1000,
                radius = 80,
                range = 850,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "AzirQWrapper",
                noProcess = true,
                spellType = SpellType.Line,
                isSpecial = true,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Azir",
                dangerlevel = 3,
                name = "Emperor's Divide",
                missileName = "AzirSoldierRMissile",
                projectileSpeed = 1400,
                radius = 450,
                range = 700,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "AzirR",
                spellType = SpellType.Line,
            });

            #endregion Azir

            #region Bard

            Spells.Add(
            new SpellData
            {
                charName = "Bard",
                dangerlevel = 2,
                missileName = "BardQMissile",
                name = "Cosmic Binding",
                projectileSpeed = 1600,
                radius = 60,
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BardQ",
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Bard",
                dangerlevel = 2,
                name = "Tempered Fate",
                missileName = "BardR",
                projectileSpeed = 2100,
                radius = 350,
                range = 3400,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "BardR",
                spellType = SpellType.Circular,
            });

            #endregion Bard

            #region Blitzcrank

            Spells.Add(
            new SpellData
            {
                charName = "Blitzcrank",
                dangerlevel = 3,
                extraDelay = 75,
                missileName = "RocketGrabMissile",
                name = "Rocket Grab",
                projectileSpeed = 1800,
                radius = 70,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "RocketGrab",
                extraSpellNames = new[] { "RocketGrabMissile" },
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Blitzcrank",
                dangerlevel = 2,
                name = "StaticField",
                radius = 600,
                range = 600,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "StaticField",
                spellType = SpellType.Circular,

            });
            #endregion Blitzcrank

            #region Brand

            Spells.Add(
            new SpellData
            {
                charName = "Brand",
                dangerlevel = 3,
                missileName = "BrandQMissile",
                name = "Sear",
                projectileSpeed = 1600,
                radius = 60,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BrandQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
            });

            Spells.Add(
            new SpellData
            {
                charName = "Brand",
                dangerlevel = 3,
                missileName = "BrandFissure",
                name = "Pillar of Flame",
                projectileSpeed = float.MaxValue,
                radius = 250,
                range = 1100,
                spellDelay = 500,
                spellKey = SpellSlot.W,
                spellName = "BrandW",
                spellType = SpellType.Circular,

            });
            #endregion Brand

            #region Braum

            Spells.Add(
            new SpellData
            {
                charName = "Braum",
                dangerlevel = 4,
                missileName = "braumrmissile",
                name = "Glacial Fissure",
                projectileSpeed = 1125,
                radius = 100,
                range = 1250,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "BraumRWrapper",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Braum",
                dangerlevel = 3,
                missileName = "BraumQMissile",
                name = "Winter's Bite",
                projectileSpeed = 1200,
                spellDelay = 250,
                radius = 100,
                range = 1000,
                spellKey = SpellSlot.Q,
                spellName = "BraumQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Braum

            #region Caitlyn

            Spells.Add(
            new SpellData
            {
                charName = "Caitlyn",
                dangerlevel = 2,
                missileName = "CaitlynPiltoverPeacemaker",
                name = "Piltover Peacemaker",
                projectileSpeed = 2200,
                radius = 90,
                range = 1300,
                spellDelay = 625,
                spellKey = SpellSlot.Q,
                spellName = "CaitlynPiltoverPeacemaker",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Caitlyn",
                dangerlevel = 3,
                missileName = "CaitlynEntrapmentMissile",
                name = "90 Caliber Net",
                projectileSpeed = 2000,
                radius = 80,
                range = 950,
                spellDelay = 125,
                spellKey = SpellSlot.E,
                spellName = "CaitlynEntrapment",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Caitlyn

            #region Cassiopeia

            Spells.Add(
            new SpellData
            {
                angle = 60,
                charName = "Cassiopeia",
                dangerlevel = 4,
                missileName = "CassiopeiaPetrifyingGaze",
                name = "Petrifying Gaze",
                projectileSpeed = float.MaxValue,
                radius = 20,
                range = 825,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "CassiopeiaPetrifyingGaze",
                spellType = SpellType.Cone,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Cassiopeia",
                dangerlevel = 1,
                missileName = "CassiopeiaQ",
                name = "Noxious Blast",
                projectileSpeed = float.MaxValue,
                radius = 200,
                range = 600,
                spellDelay = 400,
                spellKey = SpellSlot.Q,
                spellName = "CassiopeiaQ",
                spellType = SpellType.Circular,

            });

            //TODO: Add Cassiopeia W

            #endregion Cassiopeia

            #region Chogath

            Spells.Add(
            new SpellData
            {
                angle = 30,
                charName = "Chogath",
                dangerlevel = 2,
                missileName = "FeralScream",
                name = "Feral Scream",
                projectileSpeed = float.MaxValue,
                radius = 20,
                range = 650,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "FeralScream",
                spellType = SpellType.Cone,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Chogath",
                dangerlevel = 3,
                missileName = "Rupture",
                name = "Rupture",
                projectileSpeed = float.MaxValue,
                radius = 250,
                range = 950,
                spellDelay = 1200,
                spellKey = SpellSlot.Q,
                spellName = "Rupture",
                spellType = SpellType.Circular,
                extraDrawHeight = 45,
            });
            #endregion Chogath

            #region Corki

            Spells.Add(
            new SpellData
            {
                charName = "Corki",
                dangerlevel = 1,
                missileName = "MissileBarrageMissile2",
                name = "Missile Barrage big",
                projectileSpeed = 2000,
                radius = 40,
                range = 1500,
                spellDelay = 175,
                spellKey = SpellSlot.R,
                spellName = "MissileBarrage2",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Corki",
                dangerlevel = 2,
                missileName = "PhosphorusBombMissile",
                name = "Phosphorus Bomb",
                projectileSpeed = 1125,
                radius = 270,
                range = 825,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "PhosphorusBomb",
                spellType = SpellType.Circular,
                extraDrawHeight = 110,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Corki",
                dangerlevel = 1,
                missileName = "MissileBarrageMissile",
                name = "Missile Barrage",
                projectileSpeed = 2000,
                radius = 40,
                range = 1300,
                spellDelay = 175,
                spellKey = SpellSlot.R,
                spellName = "MissileBarrage",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Corki

            #region Darius

            //TODO: Add Darius Q

            Spells.Add(
            new SpellData
            {
                angle = 50,
                charName = "Darius",
                dangerlevel = 3,
                missileName = "DariusAxeGrabCone",
                projectileSpeed = float.MaxValue,
                name = "Axe Cone Grab",
                radius = 20,
                range = 570,
                spellDelay = 320,
                spellKey = SpellSlot.E,
                spellName = "DariusAxeGrabCone",
                spellType = SpellType.Cone,

            });
            #endregion Darius

            #region Diana

            Spells.Add(
            new SpellData
            {
                charName = "Diana",
                dangerlevel = 3,
                name = "Crescent Strike",
                projectileSpeed = 1400,
                radius = 50,
                range = 850,
                fixedRange = true,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "DianaArc",
                spellType = SpellType.Arc,
                hasEndExplosion = true,
                secondaryRadius = 195,
                extraEndTime = 250,
            });
            #endregion Diana

            #region DrMundo

            Spells.Add(
            new SpellData
            {
                charName = "DrMundo",
                dangerlevel = 1,
                missileName = "InfectedCleaverMissile",
                name = "Infected Cleaver",
                projectileSpeed = 2000,
                radius = 60,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "InfectedCleaverMissileCast",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion DrMundo

            #region Draven

            Spells.Add(
            new SpellData
            {
                charName = "Draven",
                dangerlevel = 3,
                missileName = "DravenR",
                name = "Whirling Death",
                projectileSpeed = 2000,
                radius = 160,
                range = 25000,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "DravenRCast",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Draven",
                dangerlevel = 2,
                missileName = "DravenDoubleShotMissile",
                name = "Stand Aside",
                projectileSpeed = 1400,
                radius = 130,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "DravenDoubleShot",
                spellType = SpellType.Line,

            });
            #endregion Draven

            #region Ekko

            Spells.Add(
            new SpellData
            {
                charName = "Ekko",
                dangerlevel = 3,
                missileName = "ekkoqmis",
                name = "Timewinder",
                projectileSpeed = 1650,
                radius = 60,
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "EkkoQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions },
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ekko",
                dangerlevel = 3,
                missileName = "EkkoW",
                name = "Parallel Convergence",
                projectileSpeed = 1650,
                radius = 375,
                range = 1600,
                spellDelay = 3750,
                spellKey = SpellSlot.W,
                spellName = "EkkoW",
                spellType = SpellType.Circular,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ekko",
                dangerlevel = 3,
                missileName = "EkkoR",
                name = "Chronobreak",
                projectileSpeed = 1650,
                radius = 375,
                range = 1600,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "EkkoR",
                spellType = SpellType.Circular,
                isSpecial = true,
            });

            #endregion Ekko

            #region Elise

            Spells.Add(
            new SpellData
            {
                charName = "Elise",
                dangerlevel = 3,
                missileName = "EliseHumanE",
                name = "Cocoon",
                projectileSpeed = 1600,
                radius = 70,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "EliseHumanE",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Elise

            #region Evelynn

            Spells.Add(
            new SpellData
            {
                charName = "Evelynn",
                dangerlevel = 3,
                missileName = "EvelynnR",
                name = "Agony's Embrace",
                projectileSpeed = float.MaxValue,
                radius = 350,
                range = 650,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "EvelynnR",
                spellType = SpellType.Circular,

            });
            #endregion Evelynn

            #region Ezreal

            Spells.Add(
            new SpellData
            {
                charName = "Ezreal",
                dangerlevel = 2,
                missileName = "EzrealMysticShotMissile",
                name = "Mystic Shot",
                projectileSpeed = 2000,
                radius = 60,
                range = 1200,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "EzrealMysticShot",
                extraSpellNames = new[] { "ezrealmysticshotwrapper", },
                extraMissileNames = new[] { "EzrealMysticShotPulseMissile" },
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ezreal",
                dangerlevel = 2,
                missileName = "EzrealTrueshotBarrag",
                name = "Trueshot Barrage",
                projectileSpeed = 2000,
                radius = 160,
                range = 20000,
                spellDelay = 1000,
                spellKey = SpellSlot.R,
                spellName = "EzrealTrueshotBarrage",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ezreal",
                dangerlevel = 1,
                missileName = "EzrealEssenceFluxMissile",
                name = "Essence Flux",
                projectileSpeed = 1600,
                radius = 80,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "EzrealEssenceFlux",
                spellType = SpellType.Line,

            });

            #endregion Ezreal

            #region Fiora

            Spells.Add(
            new SpellData
            {
                charName = "Fiora",
                dangerlevel = 1,
                missileName = "FioraWMissile",
                name = "Riposte",
                projectileSpeed = 3200,
                radius = 70,
                range = 750,
                spellDelay = 500,
                spellKey = SpellSlot.W,
                spellName = "FioraW",
                spellType = SpellType.Line,

            });

            #endregion Fiora

            #region Fizz

            Spells.Add(
            new SpellData
            {
                charName = "Fizz",
                dangerlevel = 2,
                name = "FizzPiercingStrike",
                projectileSpeed = 1400,
                radius = 150,
                range = 550,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "FizzPiercingStrike",
                spellType = SpellType.Line,
                isSpecial = true,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Fizz",
                dangerlevel = 3,
                missileName = "FizzMarinerDoomMissile",
                name = "Chum the Waters",
                projectileSpeed = 1350,
                radius = 120,
                range = 1275,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "FizzMarinerDoom",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, },
                secondaryRadius = 250,
                useEndPosition = true,

            });
            #endregion Fizz

            #region Galio

            Spells.Add(
            new SpellData
            {
                charName = "Galio",
                dangerlevel = 2,
                missileName = "GalioRighteousGust",
                name = "Righteous Gust",
                projectileSpeed = 1300,
                radius = 120,
                range = 1280,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "GalioRighteousGust",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Galio",
                dangerlevel = 2,
                missileName = "GalioResoluteSmite",
                name = "Resolute Smite",
                projectileSpeed = 1200,
                radius = 235,
                range = 1040,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "GalioResoluteSmite",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Galio",
                dangerlevel = 4,
                name = "Idol Of Durand",
                radius = 600,
                range = 600,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "GalioIdolOfDurand",
                spellType = SpellType.Circular,

            });
            #endregion Galio

            #region Gnar

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 2,
                missileName = "gnarbigq",
                name = "Boulder Toss",
                projectileSpeed = 2100,
                radius = 90,
                range = 1150,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "gnarbigq",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 4,
                missileName = "GnarR",
                name = "GNAR!",
                projectileSpeed = float.MaxValue,
                radius = 500,
                range = 500,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "GnarR",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 3,
                missileName = "gnarbigw",
                name = "Wallop",
                projectileSpeed = float.MaxValue,
                radius = 100,
                range = 600,
                spellDelay = 600,
                spellKey = SpellSlot.W,
                spellName = "gnarbigw",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 2,
                name = "Boomerang Throw",
                missileName = "GnarQ",
                extraMissileNames = new[] { "GnarQMissileReturn" },
                projectileSpeed = 2400,
                radius = 60,
                range = 1185,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "GnarQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
            });

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 2,
                missileName = "GnarE",
                name = "Hop",
                projectileSpeed = 900,
                radius = 150,
                range = 475,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "GnarE",
                spellType = SpellType.Circular,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 2,
                missileName = "gnarbige",
                name = "Crunch",
                projectileSpeed = 800,
                radius = 100,
                range = 475,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "gnarbige",
                spellType = SpellType.Circular,
            });
            #endregion Gnar

            #region Gragas

            Spells.Add(
            new SpellData
            {
                charName = "Gragas",
                dangerlevel = 2,
                missileName = "GragasQ",
                name = "Barrel Roll",
                projectileSpeed = 1000,
                radius = 250,
                range = 975,
                extraEndTime = 4500,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "GragasQ",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Gragas",
                dangerlevel = 3,
                missileName = "GragasE",
                name = "Body Slam",
                projectileSpeed = 1200,
                radius = 200,
                range = 950,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "GragasE",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Gragas",
                dangerlevel = 4,
                missileName = "GragasR",
                name = "Explosive Cask",
                projectileSpeed = 1750,
                radius = 350,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "GragasR",
                spellType = SpellType.Circular,

            });
            #endregion Gragas

            #region Graves

            //TODO: Fix return, add end split
            Spells.Add(
            new SpellData
            {
                charName = "Graves",
                dangerlevel = 2,
                missileName = "GravesQLineMis",
                extraMissileNames = new[] { "GravesQReturn" },
                name = "End of the Line",
                projectileSpeed = 3000,
                radius = 40,
                range = 808,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "GravesQLineSpell",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Graves",
                dangerlevel = 3,
                name = "Smoke Screen",
                projectileSpeed = 1000,
                radius = 250,
                range = 950,
                extraEndTime = 4000,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "GravesSmokeGrenade",
                spellType = SpellType.Circular,

            });

            //TODO: Add Cone at end
            Spells.Add(
            new SpellData
            {
                charName = "Graves",
                dangerlevel = 3,
                missileName = "GravesChargeShotShot",
                name = "Collateral Damage",
                projectileSpeed = 2100,
                radius = 100,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "GravesChargeShot",
                spellType = SpellType.Line,

            });
            #endregion Graves

            #region Hecarim

            Spells.Add(
            new SpellData
            {
                charName = "Hecarim",
                dangerlevel = 4,
                missileName = "HecarimUlt",
                extraMissileNames = new[] { "hecarimultmissileskn4r1", "hecarimultmissileskn4r2", "hecarimultmissileskn411", "hecarimultmissileskn412" },
                name = "Onslaught of Shadows",
                projectileSpeed = 1100,
                radius = 400,
                range = 1500,
                spellDelay = 10,
                spellKey = SpellSlot.R,
                spellName = "HecarimUlt",
                spellType = SpellType.Line,

            });
            #endregion Hecarim

            #region Heimerdinger

            Spells.Add(
            new SpellData
            {
                charName = "Heimerdinger",
                dangerlevel = 2,
                missileName = "HeimerdingerWAttack2",
                extraMissileNames = new[] { "HeimerdingerWAttack2Ult" },
                name = "Hextech Micro-Rockets",
                projectileSpeed = 1800,
                radius = 70,
                range = 1500,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "HeimerdingerW",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Heimerdinger",
                dangerlevel = 2,
                missileName = "HeimerdingerESpell",
                extraMissileNames = new[] { "heimerdingerespell_ult" },
                name = "CH-2 Electron Storm Grenade",
                projectileSpeed = 1750,
                radius = 135,
                range = 925,
                spellDelay = 325,
                spellKey = SpellSlot.E,
                spellName = "HeimerdingerE",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Heimerdinger",
                dangerlevel = 2,
                name = "Turret Energy Blast",
                projectileSpeed = 1650,
                radius = 50,
                range = 1000,
                spellDelay = 435,
                spellKey = SpellSlot.Q,
                spellName = "HeimerdingerTurretEnergyBlast",
                spellType = SpellType.Line,
                isSpecial = true,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Heimerdinger",
                dangerlevel = 3,
                name = "Turret Energy Blast",
                projectileSpeed = 1650,
                radius = 75,
                range = 1000,
                spellDelay = 350,
                spellKey = SpellSlot.Q,
                spellName = "HeimerdingerTurretBigEnergyBlast",
                spellType = SpellType.Line,

            });
            #endregion Heimerdinger

            #region Illaoi

            Spells.Add(
            new SpellData
            {
                charName = "Illaoi",
                dangerlevel = 3,
                missileName = "IllaoiQ",
                name = "Tentacle Smash",
                projectileSpeed = float.MaxValue,
                radius = 100,
                range = 850,
                spellDelay = 750,
                spellKey = SpellSlot.Q,
                spellName = "IllaoiQ",
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Illaoi",
                dangerlevel = 3,
                missileName = "Illaoiemis",
                name = "Test of Spirit",
                projectileSpeed = 1900,
                radius = 50,
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "IllaoiE",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Illaoi",
                dangerlevel = 3,
                name = "Leap of Faith",
                range = 0,
                radius = 450,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "IllaoiR",
                spellType = SpellType.Circular,
            });
            #endregion Illaoi

            #region Irelia

            Spells.Add(
            new SpellData
            {
                charName = "Irelia",
                dangerlevel = 2,
                missileName = "ireliatranscendentbladesspell",
                name = "Transcendent Blades",
                projectileSpeed = 1600,
                radius = 65,
                range = 1200,
                spellDelay = 0,
                spellKey = SpellSlot.R,
                spellName = "IreliaTranscendentBlades",
                spellType = SpellType.Line,
                usePackets = true,
                defaultOff = true,
            });
            #endregion Irelia
                
            #region Ivern
         
            Spells.Add(
            new SpellData
            {
                charName = "Ivern",                  
                dangerlevel = 3,
                missileName = "IvernQ",
                name = "Rootcaller",
                projectileSpeed = 1300,
                radius = 65,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "IvernQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyMinions, CollisionObjectType.EnemyChampions },
            });
            #endregion Ivern

            #region Janna

            Spells.Add(
            new SpellData
            {
                charName = "Janna",
                dangerlevel = 2,
                missileName = "HowlingGaleSpell",
                name = "Howling Gale",
                projectileSpeed = 900,
                radius = 120,
                range = 1700,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "HowlingGale",
                spellType = SpellType.Line,
                usePackets = true,

            });
            #endregion Janna

            #region JarvanIV

            Spells.Add(
            new SpellData
            {
                charName = "JarvanIV",
                dangerlevel = 2,
                missileName = "JarvanIVDragonStrike",
                name = "Dragon Strike",
                projectileSpeed = 2000,
                radius = 80,
                range = 845,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "JarvanIVDragonStrike",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "JarvanIV",
                dangerlevel = 3,
                missileName = "JarvanIVDragonStrike2",
                name = "Dragon Strike EQ",
                projectileSpeed = 1800,
                radius = 120,
                range = 845,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "JarvanIVDragonStrike2",
                spellType = SpellType.Line,
                useEndPosition = true,

            });

            Spells.Add(
            new SpellData
            {
                charName = "JarvanIV",
                dangerlevel = 2,
                name = "Demacian Standard",
                radius = 175,
                range = 800,
                spellDelay = 500,
                spellKey = SpellSlot.E,
                spellName = "JarvanIVDemacianStandard",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "JarvanIV",
                dangerlevel = 3,
                name = "Cataclysm",
                projectileSpeed = 1900,
                radius = 350,
                range = 825,
                spellDelay = 0,
                spellKey = SpellSlot.R,
                spellName = "JarvanIVCataclysm",
                spellType = SpellType.Circular,

            });
            #endregion JarvanIV

            #region Jayce

            Spells.Add(
            new SpellData
            {
                charName = "Jayce",
                dangerlevel = 3,
                missileName = "JayceShockBlastWallMis",
                name = "Shock Blast Fast",
                projectileSpeed = 2350,
                radius = 70,
                range = 1170,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "JayceQAccel",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                hasEndExplosion = true,
                secondaryRadius = 250,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Jayce",
                dangerlevel = 2,
                missileName = "JayceShockBlastMis",
                name = "Shock Blast",
                projectileSpeed = 1450,
                radius = 70,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "jayceshockblast",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                hasEndExplosion = true,
                secondaryRadius = 175,
            });
            #endregion Jayce

            #region Jinx

            Spells.Add(
            new SpellData
            {
                charName = "Jinx",
                dangerlevel = 3,
                missileName = "JinxR",
                name = "Super Mega Death Rocket!",
                projectileSpeed = 1700,
                radius = 120,
                range = 25000,
                spellDelay = 600,
                spellKey = SpellSlot.R,
                spellName = "JinxR",
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Jinx",
                dangerlevel = 3,
                missileName = "JinxWMissile",
                name = "Zap!",
                projectileSpeed = 3300,
                radius = 60,
                range = 1500,
                spellDelay = 600,
                spellKey = SpellSlot.W,
                spellName = "JinxWMissile",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Jinx

            #region Jhin   

            Spells.Add(
            new SpellData
            {
                charName = "Jhin",
                dangerlevel = 3,
                missileName = "JhinWMissile",
                name = "Deadly Flourish",
                projectileSpeed = 5000,
                radius = 40,
                range = 3000,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "JhinW",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions },
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Jhin",
                dangerlevel = 3,
                missileName = "JhinRShotMis",
                name = "Curtain Call",
                projectileSpeed = 5000,
                radius = 80,
                range = 3500,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "JhinRShot",
                extraSpellNames = new[] { "JhinRShotFinal" },
                spellType = SpellType.Line,
                fixedRange = true,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions },
                extraMissileNames = new[] { "JhinRShotMis4" }
            });

            #endregion

            #region Kalista

            Spells.Add(
            new SpellData
            {
                charName = "Kalista",
                dangerlevel = 2,
                missileName = "kalistamysticshotmistrue",
                name = "Pierce",
                projectileSpeed = 2000,
                radius = 70,
                range = 1200,
                spellDelay = 350,
                spellKey = SpellSlot.Q,
                spellName = "KalistaMysticShot",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            //TODO: Add Kalista R?

            #endregion Kalista

            #region Karma

            Spells.Add(
            new SpellData
            {
                charName = "Karma",
                dangerlevel = 2,
                missileName = "KarmaQMissile",
                name = "Inner Flame",
                projectileSpeed = 1700,
                radius = 90,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KarmaQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            //TODO: Fix end circle extra end time
            Spells.Add(
            new SpellData
            {
                charName = "Karma",
                dangerlevel = 3,
                missileName = "KarmaQMissileMantra",
                name = "Soulflare (Mantra)",
                projectileSpeed = 1700,
                radius = 90,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KarmaQMissileMantra",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                hasEndExplosion = true,
                secondaryRadius = 250,
                usePackets = true

            });
            #endregion Karma

            #region Karthus

            Spells.Add(
            new SpellData
            {
                charName = "Karthus",
                dangerlevel = 2,
                missileName = "KarthusLayWasteA1",
                name = "Lay Waste",
                projectileSpeed = float.MaxValue,
                radius = 190,
                range = 875,
                spellDelay = 625,
                spellKey = SpellSlot.Q,
                spellName = "KarthusLayWasteA1",
                spellType = SpellType.Circular,
                extraSpellNames = new[] { "karthuslaywastea2", "karthuslaywastea3", "karthuslaywastedeada1", "karthuslaywastedeada2", "karthuslaywastedeada3" },

            });

            #endregion Karthus

            #region Kassadin

            Spells.Add(
            new SpellData
            {
                charName = "Kassadin",
                dangerlevel = 1,
                missileName = "RiftWalk",
                name = "RiftWalk",
                projectileSpeed = float.MaxValue,
                radius = 270,
                range = 700,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "RiftWalk",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                angle = 40,
                charName = "Kassadin",
                dangerlevel = 2,
                name = "Force Pulse",
                radius = 20,
                range = 700,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "ForcePulse",
                spellType = SpellType.Cone,

            });
            #endregion Kassadin

            #region Kennen

            Spells.Add(
            new SpellData
            {
                charName = "Kennen",
                dangerlevel = 2,
                missileName = "KennenShurikenHurlMissile1",
                name = "Thundering Shuriken",
                projectileSpeed = 1700,
                radius = 50,
                range = 1175,
                spellDelay = 125,
                spellKey = SpellSlot.Q,
                spellName = "KennenShurikenHurlMissile1",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Kennen

            #region Khazix

            Spells.Add(
            new SpellData
            {
                charName = "Khazix",
                dangerlevel = 2,
                missileName = "KhazixWMissile",
                name = "Void Spike",
                projectileSpeed = 1700,
                radius = 70,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "KhazixW",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                angle = 22,
                charName = "Khazix",
                dangerlevel = 2,
                isThreeWay = true,
                missileName = "khazixwlong",
                name = "Void Spike Evolved",
                projectileSpeed = 1700,
                radius = 70,
                range = 1025,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "khazixwlong",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Khazix",
                dangerlevel = 1,
                missileName = "KhazixE",
                name = "Leap",
                projectileSpeed = 1500,
                radius = 300,
                range = 900,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "KhazixE",
                spellType = SpellType.Circular,
            });
            #endregion Khazix

            #region Kled

            Spells.Add(
            new SpellData
            {
                angle = 5,
                charName = "Kled",
                dangerlevel = 2,
                isThreeWay = true,
                missileName = "KledRiderQMissile",
                name = "Pocket Pistol",
                projectileSpeed = 3000,
                radius = 40,
                range = 700,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KledRiderQ",
                spellType = SpellType.Line,
                splits = 5,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Kled",
                dangerlevel = 3,
                missileName = "KledQMissile",
                name = "Beartrap on a Rope",
                projectileSpeed = 1600,
                radius = 45,
                range = 800,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KledQ",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Kled",
                dangerlevel = 2,
                name = "Jousting",
                projectileSpeed = 945,
                radius = 125,
                range = 750,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "KledE",
                spellType = SpellType.Line,

            });
            #endregion Kled

            #region KogMaw

            Spells.Add(
            new SpellData
            {
                charName = "KogMaw",
                dangerlevel = 2,
                missileName = "KogMawQMis",
                name = "Caustic Spittle",
                projectileSpeed = 1650,
                radius = 70,
                range = 1125,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KogMawQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
            });

            Spells.Add(
            new SpellData
            {
                charName = "KogMaw",
                dangerlevel = 1,
                missileName = "KogMawVoidOozeMissile",
                name = "Void Ooze",
                projectileSpeed = 1400,
                radius = 120,
                range = 1360,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "KogMawVoidOoze",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "KogMaw",
                dangerlevel = 2,
                missileName = "KogMawLivingArtillery",
                name = "Living Artillery",
                projectileSpeed = float.MaxValue,
                radius = 235,
                range = 2200,
                spellDelay = 1100,
                spellKey = SpellSlot.R,
                spellName = "KogMawLivingArtillery",
                spellType = SpellType.Circular,

            });
            #endregion KogMaw

            #region Leblanc

            Spells.Add(
            new SpellData
            {
                charName = "Leblanc",
                dangerlevel = 3,
                missileName = "LeblancSoulShackleM",
                name = "Ethereal Chains (Mimic)",
                projectileSpeed = 1600,
                radius = 70,
                range = 960,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "LeblancSoulShackleM",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Leblanc",
                dangerlevel = 3,
                name = "Ethereal Chains",
                missileName = "LeblancSoulShackle",
                projectileSpeed = 1600,
                radius = 70,
                range = 960,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "LeblancSoulShackle",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Leblanc",
                dangerlevel = 1,
                missileName = "LeblancSlideM",
                name = "Distortion (Mimic)",
                projectileSpeed = 1600,
                radius = 250,
                range = 725,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "LeblancSlideM",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Leblanc",
                dangerlevel = 1,
                missileName = "LeblancSlide",
                name = "Distortion",
                projectileSpeed = 1600,
                radius = 250,
                range = 725,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "LeblancSlide",
                spellType = SpellType.Circular,

            });
            #endregion Leblanc

            #region LeeSin

            Spells.Add(
            new SpellData
            {
                charName = "LeeSin",
                dangerlevel = 3,
                missileName = "BlindMonkQOn",
                name = "Sonic Wave",
                projectileSpeed = 1800,
                radius = 60,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BlindMonkQOne",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            //TODO: Add LeeSin R?

            #endregion LeeSin

            #region Leona

            Spells.Add(
            new SpellData
            {
                charName = "Leona",
                dangerlevel = 4,
                missileName = "LeonaSolarFlare",
                name = "Solar Flare",
                projectileSpeed = float.MaxValue,
                radius = 250,
                range = 1200,
                spellDelay = 625,
                spellKey = SpellSlot.R,
                spellName = "LeonaSolarFlare",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Leona",
                dangerlevel = 3,
                extraDistance = 65,
                missileName = "LeonaZenithBladeMissile",
                name = "Zenith Blade",
                projectileSpeed = 2000,
                radius = 70,
                range = 975,
                spellDelay = 350,
                spellKey = SpellSlot.E,
                spellName = "LeonaZenithBlade",
                spellType = SpellType.Line,

            });
            #endregion Leona

            #region Lissandra

            Spells.Add(
            new SpellData
            {
                charName = "Lissandra",
                dangerlevel = 3,
                missileName = "LissandraW",
                name = "Ring of Frost",
                projectileSpeed = float.MaxValue,
                radius = 450,
                range = 725,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "LissandraW",
                spellType = SpellType.Circular,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Lissandra",
                dangerlevel = 2,
                missileName = "LissandraQMissile",
                name = "Ice Shard",
                projectileSpeed = 2200,
                radius = 75,
                range = 825,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LissandraQ",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lissandra",
                dangerlevel = 2,
                name = "Ice Shard Extended",
                missileName = "lissandraqshards",
                projectileSpeed = 2200,
                radius = 90,
                range = 825,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LissandraQShards",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lissandra",
                dangerlevel = 2,
                name = "Glacial Path",
                missileName = "LissandraEMissile",
                projectileSpeed = 850,
                radius = 125,
                range = 1025,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "LissandraE",
                extraSpellNames = new[] { "LissandraEMissile" },
                spellType = SpellType.Line,

            });
            #endregion Lissandra

            #region Lucian

            //TODO: Add explosion
            Spells.Add(
            new SpellData
            {
                charName = "Lucian",
                dangerlevel = 1,
                defaultOff = true,
                missileName = "LucianW",
                name = "Ardent Blaze",
                projectileSpeed = 1600,
                radius = 80,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "LucianW",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lucian",
                dangerlevel = 2,
                isSpecial = true,
                missileName = "LucianQ",
                name = "Piercing Light",
                projectileSpeed = float.MaxValue,
                radius = 65,
                range = 1140,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LucianQ",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lucian",
                dangerlevel = 3,
                missileName = "lucianrmissileoffhand",
                extraMissileNames = new[] { "lucianrmissile" },
                name = "The Culling",
                projectileSpeed = 2800,
                radius = 110,
                range = 1400,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "LucianRMis",
                spellType = SpellType.Line,
                extraSpellNames = new[] { "LucianR" },
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                dontcheckDuplicates = true,
                defaultOff = true
            });
            
            #endregion Lucian

            #region Lulu

            Spells.Add(
            new SpellData
            {
                charName = "Lulu",
                dangerlevel = 2,
                missileName = "LuluQMissile",
                extraMissileNames = new[] { "LuluQMissileTwo" },
                name = "Glitterlance",
                projectileSpeed = 1450,
                radius = 80,
                range = 925,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LuluQ",
                extraSpellNames = new[] { "LuluQMissile", "LuluQPix" },
                spellType = SpellType.Line,
                isSpecial = true,

            });
            #endregion Lulu

            #region Lux

            Spells.Add(
            new SpellData
            {
                charName = "Lux",
                dangerlevel = 2,
                missileName = "LuxLightStrikeKugel",
                name = "Lucent Singularity",
                projectileSpeed = 1400,
                radius = 340,
                range = 1100,
                extraEndTime = 5500,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "LuxLightStrikeKugel",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lux",
                dangerlevel = 4,
                missileName = "LuxRVfxMis",
                name = "Final Spark",
                projectileSpeed = float.MaxValue,
                radius = 110,
                range = 3500,
                spellDelay = 1000,
                spellKey = SpellSlot.R,
                spellName = "LuxMaliceCannon",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lux",
                dangerlevel = 3,
                missileName = "LuxLightBindingMis",
                name = "Light Binding",
                projectileSpeed = 1200,
                radius = 70,
                range = 1300,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LuxLightBinding",
                spellType = SpellType.Line

            });
            #endregion Lux

            #region Maokai

            Spells.Add(
            new SpellData
            {
                charName = "Maokai",
                dangerlevel = 3,
                name = "Arcane Smash",
                projectileSpeed = 1000,
                radius = 110,
                range = 600,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "MaokaiTrunkLine",
                spellType = SpellType.Line,
            });

            //TODO: Fix detection
            Spells.Add(
            new SpellData
            {
                charName = "Maokai",
                dangerlevel = 3,
                name = "Arcane Smash KnockBack",
                radius = 100,
                range = 100,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "MaokaiTrunkLine",
                spellType = SpellType.Circular,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Maokai",
                dangerlevel = 2,
                name = "Sapling Toss",
                projectileSpeed = 1000,
                radius = 250,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "MaokaiSapling2",
                spellType = SpellType.Circular,

            });
            #endregion Maokai

            #region Mordekaiser

            Spells.Add(
            new SpellData
            {
                angle = 45,
                charName = "Mordekaiser",
                dangerlevel = 3,
                name = "Syphon Of Destruction",
                radius = 160,
                range = 675,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "MordekaiserSyphonOfDestruction",
                spellType = SpellType.Cone,

            });
            #endregion Mordekaiser

            #region Malphite

            Spells.Add(
            new SpellData
            {
                charName = "Malphite",
                dangerlevel = 4,
                missileName = "UFSlash",
                name = "Unstoppable Force",
                projectileSpeed = 1500,
                radius = 270,
                range = 1000,
                spellDelay = 0,
                spellKey = SpellSlot.R,
                spellName = "UFSlash",
                spellType = SpellType.Circular,

            });
            #endregion Malphite

            #region Malzahar

            Spells.Add(
            new SpellData
            {
                charName = "Malzahar",
                dangerlevel = 2,
                isSpecial = true,
                isWall = true,
                name = "Call of the Void",
                projectileSpeed = 1600,
                radius = 85,
                range = 900,
                sideRadius = 400,
                spellDelay = 1000,
                spellKey = SpellSlot.Q,
                spellName = "MalzaharQ",
                spellType = SpellType.Line,

            });
            #endregion Malzahar

            #region MonkeyKing

            Spells.Add(
            new SpellData
            {
                charName = "MonkeyKing",
                dangerlevel = 4,
                name = "Cyclone",
                radius = 225,
                range = 300,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "MonkeyKingSpinToWin",
                spellType = SpellType.Circular
            });

            #endregion MonkeyKing

            #region Morgana

            Spells.Add(
            new SpellData
            {
                charName = "Morgana",
                dangerlevel = 3,
                missileName = "DarkBindingMissile",
                name = "Dark Binding",
                projectileSpeed = 1200,
                radius = 80,
                range = 1300,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "DarkBindingMissile",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Morgana

            #region Nami

            Spells.Add(
            new SpellData
            {
                charName = "Nami",
                dangerlevel = 3,
                missileName = "NamiQ",
                extraMissileNames = new[] { "namiqmissile" },
                name = "Aqua Prison",
                projectileSpeed = float.MaxValue,
                radius = 200,
                range = 875,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "NamiQ",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Nami",
                dangerlevel = 4,
                missileName = "NamiRMissile",
                name = "Tidal Wave",
                projectileSpeed = 850,
                radius = 250,
                range = 2750,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "NamiR",
                spellType = SpellType.Line,

            });

            #endregion Nami

            #region Nautilus

            Spells.Add(
            new SpellData
            {
                charName = "Nautilus",
                dangerlevel = 3,
                missileName = "NautilusAnchorDragMissile",
                name = "Dredge Line",
                projectileSpeed = 2000,
                radius = 90,
                range = 1080,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "NautilusAnchorDrag",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Nautilus

            #region Nidalee

            Spells.Add(
            new SpellData
            {
                charName = "Nidalee",
                dangerlevel = 2,
                missileName = "JavelinToss",
                name = "Javelin Toss",
                projectileSpeed = 1300,
                radius = 40,
                range = 1500,
                spellDelay = 125,
                spellKey = SpellSlot.Q,
                spellName = "JavelinToss",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Nidalee

            #region Nocturne

            Spells.Add(
            new SpellData
            {
                charName = "Nocturne",
                dangerlevel = 1,
                missileName = "NocturneDuskbringer",
                name = "Duskbringer",
                projectileSpeed = 1400,
                radius = 60,
                range = 1125,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "NocturneDuskbringer",
                spellType = SpellType.Line,

            });
            #endregion Nocturne

            #region Olaf

            Spells.Add(
            new SpellData
            {
                charName = "Olaf",
                dangerlevel = 2,
                missileName = "olafaxethrow",
                name = "Axe Throw",
                projectileSpeed = 1600,
                radius = 90,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "OlafAxeThrowCast",
                spellType = SpellType.Line,

            });
            #endregion Olaf

            #region Orianna

            //TODO: Add ball width
            Spells.Add(
            new SpellData
            {
                charName = "Orianna",
                dangerlevel = 2,
                missileName = "OrianaIzunaCommand",
                name = "Commnad: Attack",
                projectileSpeed = 1200,
                radius = 80,
                secondaryRadius = 170,
                range = 2000,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "OrianaIzunaCommand",
                spellType = SpellType.Line,
                isSpecial = true,
                useEndPosition = true,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Orianna",
                dangerlevel = 2,
                missileName = "OrianaDissonanceCommand",
                name = "Command: Dissonance",
                projectileSpeed = float.MaxValue,
                radius = 250,
                range = 1825,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "OrianaDissonanceCommand",
                spellType = SpellType.Circular,
            });

            //TODO: Add Orianna E

            Spells.Add(
            new SpellData
            {
                charName = "Orianna",
                dangerlevel = 4,
                missileName = "OrianaDetonateCommand",
                name = "Command: Shockwave",
                projectileSpeed = float.MaxValue,
                radius = 410,
                range = 410,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "OrianaDetonateCommand",
                spellType = SpellType.Circular,
            });
            #endregion Orianna

            #region Pantheon

            Spells.Add(
            new SpellData
            {
                angle = 35,
                charName = "Pantheon",
                dangerlevel = 2,
                name = "Heartseeker",
                radius = 50,
                range = 600,
                extraEndTime = 750,
                spellDelay = 1000,
                spellKey = SpellSlot.E,
                spellName = "PantheonE",
                spellType = SpellType.Cone,

            });
            #endregion Pantheon

            #region Poppy

            Spells.Add(
            new SpellData
            {
                charName = "Poppy",
                dangerlevel = 2,
                missileName = "PoppyQ",
                name = "Hammer Shock",
                radius = 100,
                range = 450,
                extraEndTime = 1000,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "PoppyQ",
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Poppy",
                dangerlevel = 4,
                name = "Keeper's Verdict (Knockup)",
                radius = 110,
                range = 450,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "PoppyRSpellInstant",
                spellType = SpellType.Circular,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Poppy",
                dangerlevel = 3,
                name = "Keeper's Verdict (Line)",
                radius = 100,
                range = 1150,
                projectileSpeed = 1750,
                spellDelay = 300,
                spellKey = SpellSlot.R,
                spellName = "PoppyRSpell",
                missileName = "PoppyRMissile",
                spellType = SpellType.Line,
            });
            #endregion

            #region Quinn

            Spells.Add(
            new SpellData
            {
                charName = "Quinn",
                dangerlevel = 3,
                missileName = "QuinnQMissile",
                name = "Blinding Assault",
                projectileSpeed = 1550,
                radius = 80,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "QuinnQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Quinn

            #region RekSai

            Spells.Add(
            new SpellData
            {
                charName = "RekSai",
                dangerlevel = 2,
                missileName = "RekSaiQBurrowedMis",
                name = "Prey Seeker",
                projectileSpeed = 1950,
                radius = 65,
                range = 1500,
                spellDelay = 125,
                spellKey = SpellSlot.Q,
                spellName = "ReksaiQBurrowed",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "RekSai",
                dangerlevel = 3,
                missileName = "",
                name = "Unburrow",
                projectileSpeed = 1500,
                radius = 115,
                range = 120,
                spellDelay = 250f,
                spellKey = SpellSlot.W,
                spellName = "ReksaiWBurrowed",
                spellType = SpellType.Circular
            });

            #endregion RekSai

            #region Rengar

            Spells.Add(
            new SpellData
            {
                charName = "Rengar",
                dangerlevel = 3,
                missileName = "RengarEFinal",
                name = "Bola Strike",
                projectileSpeed = 1500,
                radius = 70,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "RengarE",
                spellType = SpellType.Line,
                extraMissileNames = new[] { "RengarEFinalMAX" },
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            #endregion Rengar

            #region Riven

            Spells.Add(
            new SpellData
            {
                angle = 40,
                charName = "Riven",
                dangerlevel = 4,
                isThreeWay = true,
                missileName = "RivenWindslashMissileCenter",
                name = "Wind Slash",
                projectileSpeed = 1800,
                radius = 100,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "RivenIzunaBlade",
                spellType = SpellType.Line,
                isSpecial = true,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Riven",
                dangerlevel = 3,
                defaultOff = true,
                missileName = "RivenMartyr",
                name = "Ki Burst",
                projectileSpeed = 1500,
                radius = 280,
                range = 650,
                spellDelay = 267,
                spellKey = SpellSlot.W,
                spellName = "RivenMartyr",
                spellType = SpellType.Circular,

            });
            #endregion Riven

            #region Rumble

            Spells.Add(
            new SpellData
            {
                charName = "Rumble",
                dangerlevel = 2,
                missileName = "RumbleGrenadeMissile",
                name = "Electro-Harpoon",
                projectileSpeed = 2000,
                radius = 90,
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "RumbleGrenade",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Rumble",
                dangerlevel = 4,
                missileName = "RumbleCarpetBombMissile",
                name = "Carpet Bomb",
                projectileSpeed = 1600,
                radius = 200,
                range = 1200,
                spellDelay = 400,
                spellKey = SpellSlot.R,
                spellName = "RumbleCarpetBomb",
                spellType = SpellType.Line,

            });
            #endregion Rumble

            #region Ryze

            Spells.Add(
            new SpellData
            {
                charName = "Ryze",
                dangerlevel = 2,
                missileName = "RyzeQ",
                name = "Overload",
                projectileSpeed = 1700,
                radius = 60,
                range = 900,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "RyzeQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            #endregion Ryze

            #region Sejuani
            Spells.Add(
            new SpellData
            {
                charName = "Sejuani",
                dangerlevel = 3,
                name = "Arctic Assault",
                projectileSpeed = 1600,
                radius = 70,
                range = 900,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "SejuaniArcticAssault",
                spellType = SpellType.Line,

            });

            //TODO: Add Sejuani R AOE?
            Spells.Add(
            new SpellData
            {
                charName = "Sejuani",
                dangerlevel = 4,
                missileName = "SejuaniGlacialPrison",
                name = "Glacial Prison",
                projectileSpeed = 1600,
                radius = 110,
                range = 1200,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "SejuaniGlacialPrisonCast",
                spellType = SpellType.Line,

            });
            #endregion Sejuani

            #region Shen

            Spells.Add(
            new SpellData
            {
                charName = "Shen",
                dangerlevel = 3,
                missileName = "ShenShadowDash",
                name = "Shadow Dash",
                projectileSpeed = 1250,
                radius = 75,
                range = 1600,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "ShenShadowDash",
                spellType = SpellType.Line,

            });

            #endregion Shen

            #region Shyvana

            Spells.Add(
            new SpellData
            {
                charName = "Shyvana",
                dangerlevel = 1,
                missileName = "ShyvanaFireball",
                name = "Flame Breath",
                projectileSpeed = 1700,
                radius = 60,
                range = 950,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "ShyvanaFireball",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                angle = 10,
                charName = "Shyvana",
                dangerlevel = 2,
                isThreeWay = true,
                missileName = "ShyvanaFireballDragonFxMissile",
                name = "Flame Breath Dragon",
                projectileSpeed = 2000,
                radius = 70,
                range = 850,
                extraEndTime = 200,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "shyvanafireballdragon2",
                spellType = SpellType.Line,
                splits = 5,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Shyvana",
                dangerlevel = 3,
                missileName = "ShyvanaTransformCast",
                name = "Dragon's Descent",
                projectileSpeed = 1500,
                radius = 160,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "ShyvanaTransformCast",
                spellType = SpellType.Line,

            });
            #endregion Shyvana

            #region Sion

            Spells.Add(
            new SpellData
            {
                charName = "Sion",
                dangerlevel = 3,
                missileName = "SionQ",
                name = "Decimating Smash",
                projectileSpeed = float.MaxValue,
                radius = 250,
                range = 800,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "SionQ",
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Sion",
                dangerlevel = 2,
                missileName = "SionEMissile",
                name = "Roar of the Slayer",
                projectileSpeed = 1800,
                radius = 80,
                range = 800,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "SionE",
                spellType = SpellType.Line,
                isSpecial = true,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Sion",
                dangerlevel = 3,
                missileName = "SionR",
                name = "Unstoppable Onslaught",
                projectileSpeed = 1000,
                radius = 120,
                range = 800,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "SionR",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions },

            });
            #endregion Sion

            #region Sivir

            Spells.Add(
            new SpellData
            {
                charName = "Sivir",
                dangerlevel = 2,
                missileName = "SivirQMissile",
                extraMissileNames = new[] { "SivirQMissileReturn" },
                name = "Boomerang Blade",
                projectileSpeed = 1350,
                radius = 100,
                range = 1275,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "SivirQ",
                extraSpellNames = new[] { "SivirQReturn" },
                spellType = SpellType.Line
            });

            #endregion Sivir

            #region Skarner

            Spells.Add(
            new SpellData
            {
                charName = "Skarner",
                dangerlevel = 2,
                missileName = "SkarnerFractureMissile",
                name = "Fracture",
                projectileSpeed = 1400,
                radius = 60,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "SkarnerFracture",
                spellType = SpellType.Line,

            });
            #endregion Skarner

            #region Sona

            Spells.Add(
            new SpellData
            {
                charName = "Sona",
                dangerlevel = 4,
                missileName = "SonaR",
                name = "Crescendo",
                projectileSpeed = 2400,
                radius = 150,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "SonaR",
                spellType = SpellType.Line,

            });
            #endregion Sona

            #region Soraka

            Spells.Add(
            new SpellData
            {
                charName = "Soraka",
                dangerlevel = 2,
                name = "Starcall",
                missileName = "SorakaQ",
                projectileSpeed = 1750,
                radius = 260,
                range = 970,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "SorakaQ",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Soraka",
                dangerlevel = 3,
                name = "Equinox",
                radius = 275,
                range = 925,
                spellDelay = 1750,
                spellKey = SpellSlot.E,
                spellName = "SorakaE",
                spellType = SpellType.Circular,

            });
            #endregion Soraka

            #region Swain

            Spells.Add(
            new SpellData
            {
                charName = "Swain",
                dangerlevel = 3,
                name = "Nevermove",
                missileName = "SwainShadowGrasp",
                projectileSpeed = float.MaxValue,
                radius = 250,
                range = 900,
                spellDelay = 1100,
                spellKey = SpellSlot.W,
                spellName = "SwainShadowGrasp",
                spellType = SpellType.Circular,

            });
            #endregion Swain

            #region Syndra

            Spells.Add(
            new SpellData
            {
                angle = 45,
                charName = "Syndra",
                dangerlevel = 3,
                name = "Scatter the Weak",
                missileName = "SyndraE",
                extraMissileNames = new[] { "syndrae5" },
                usePackets = true,
                projectileSpeed = 2000,
                radius = 100,
                range = 950,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "SyndraE",
                extraSpellNames = new[] { "syndrae5" },
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Syndra",
                dangerlevel = 2,
                missileName = "syndrawcast",
                name = "Force of Will",
                projectileSpeed = 1450,
                radius = 220,
                range = 925,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "SyndraWCast",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Syndra",
                dangerlevel = 2,
                missileName = "SyndraQSpell",
                name = "Dark Sphere",
                projectileSpeed = float.MaxValue,
                radius = 210,
                range = 800,
                spellDelay = 600,
                spellKey = SpellSlot.Q,
                spellName = "SyndraQ",
                spellType = SpellType.Circular,

            });
            #endregion Syndra

            #region TahmKench

            Spells.Add(
            new SpellData
            {
                charName = "TahmKench",
                dangerlevel = 3,
                missileName = "tahmkenchqmissile",
                name = "Tongue Lash",
                projectileSpeed = 2000,
                spellDelay = 250,
                radius = 90,
                range = 951,
                spellKey = SpellSlot.Q,
                spellName = "TahmKenchQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            #endregion TahmKench

            #region Talon


            Spells.Add(
            new SpellData
            {
                angle = 20,
                charName = "Talon",
                dangerlevel = 2,
                isThreeWay = true,
                missileName = "talonrakemissileone",
                name = "Rake",
                projectileSpeed = 2300,
                radius = 75,
                range = 780,
                spellKey = SpellSlot.W,
                spellName = "TalonRake",
                spellType = SpellType.Line,
                splits = 3,
                isSpecial = true,
            });

            Spells.Add(
            new SpellData
            {
                angle = 20,
                charName = "Talon",
                dangerlevel = 2,
                isThreeWay = true,
                missileName = "talonrakemissiletwo",
                name = "Rake Return",
                projectileSpeed = 1850,
                radius = 80,
                range = 800,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "TalonRake",
                spellType = SpellType.Line,
                splits = 3,
                isSpecial = true,
            });
            #endregion Talon

            #region Taliyah
            Spells.Add(
            new SpellData
            {
                charName = "Taliyah",
                dangerlevel = 2,
                missileName = "TaliyahQMis",
                projectileSpeed = 1450,
                name = "Threaded Volley",
                radius = 100,
                range = 1000,
                fixedRange = true,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "TaliyahQ",
                spellType = SpellType.Line,
                defaultOff = true,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Taliyah",
                dangerlevel = 3,
                missileName = "TaliyahWVC",
                name = "Seismic Shove",
                radius = 150,
                range = 900,
                spellDelay = 1000,
                spellKey = SpellSlot.W,
                spellName = "TaliyahWVC",
                spellType = SpellType.Circular
            });
            #endregion Taliyah

            #region Taric

            Spells.Add(
            new SpellData
            {
                charName = "Taric",
                dangerlevel = 3,
                missileName = "TaricEMissile",
                name = "Dazzle",
                radius = 100,
                range = 750,
                fixedRange = true,
                defaultOff = true,
                spellDelay = 1000,
                spellKey = SpellSlot.E,
                spellName = "TaricE",
                spellType = SpellType.Line,
                isSpecial = true
            });

            #endregion

            #region Thresh

            Spells.Add(
            new SpellData
            {
                charName = "Thresh",
                dangerlevel = 3,
                missileName = "ThreshQMissile",
                name = "Death Sentence",
                projectileSpeed = 1900,
                radius = 70,
                range = 1200,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "ThreshQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Thresh",
                dangerlevel = 3,
                missileName = "ThreshEMissile1",
                name = "Flay",
                projectileSpeed = 2000,
                radius = 110,
                range = 1075,
                spellDelay = 125,
                defaultOff = true,
                spellKey = SpellSlot.E,
                spellName = "ThreshE",
                spellType = SpellType.Line,
                usePackets = true,

            });
            #endregion Thresh

            #region Tristana

            Spells.Add(
            new SpellData
            {
                charName = "Tristana",
                dangerlevel = 1,
                missileName = "RocketJump",
                name = "Rocket Jump",
                projectileSpeed = 1500,
                radius = 270,
                range = 900,
                spellDelay = 500,
                spellKey = SpellSlot.W,
                spellName = "TristanaW",
                spellType = SpellType.Circular,
            });

            #endregion Tristana

            #region Tryndamere

            Spells.Add(
            new SpellData
            {
                charName = "Tryndamere",
                dangerlevel = 2,
                missileName = "slashCast",
                name = "Spinning Slash",
                projectileSpeed = 1300,
                radius = 95,
                range = 660,
                fixedRange = false,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "slashCast",
                spellType = SpellType.Line
            });

            #endregion Tryndamere

            #region TwistedFate

            Spells.Add(
            new SpellData
            {
                angle = 28,
                charName = "TwistedFate",
                dangerlevel = 2,
                isThreeWay = true,
                missileName = "SealFateMissile",
                name = "Wild Cards",
                projectileSpeed = 1000,
                radius = 40,
                range = 1450,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "WildCards",
                spellType = SpellType.Line,
            });
            #endregion TwistedFate

            #region Twitch

            Spells.Add(
            new SpellData
            {
                charName = "Twitch",
                dangerlevel = 2,
                missileName = "TwitchVenomCaskMissile",
                name = "Venom Cask",
                projectileSpeed = 1400,
                radius = 280,
                range = 900,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "TwitchVenomCask",
                spellType = SpellType.Circular,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Twitch",
                dangerlevel = 2,
                name = "Spray and Pray",
                projectileSpeed = 4000,
                radius = 60,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "TwitchSprayandPrayAttack",
                spellType = SpellType.Line
            });

            #endregion Twitch

            #region Urgot

            Spells.Add(
            new SpellData
            {
                charName = "Urgot",
                dangerlevel = 2,
                missileName = "UrgotHeatseekingLineMissile",
                name = "Acid Hunter",
                projectileSpeed = 1600,
                radius = 60,
                range = 1000,
                spellDelay = 175,
                spellKey = SpellSlot.Q,
                spellName = "UrgotHeatseekingLineMissile",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Urgot",
                dangerlevel = 3,
                missileName = "UrgotPlasmaGrenadeBoom",
                name = "Noxian Corrosive Charge",
                projectileSpeed = 1500,
                radius = 250,
                range = 900,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "UrgotPlasmaGrenade",
                spellType = SpellType.Circular,

            });
            #endregion Urgot

            #region Varus

            Spells.Add(
            new SpellData
            {
                charName = "Varus",
                dangerlevel = 1,
                name = "Hail of Arrows",
                missileName = "VarusE",
                extraMissileNames = new[] { "VarusEMissile", },
                projectileSpeed = 1500,
                radius = 235,
                range = 925,
                spellDelay = 1000,
                spellKey = SpellSlot.E,
                spellName = "VarusE",
                extraSpellNames = new[] { "VarusEMissile" },
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Varus",
                dangerlevel = 2,
                missileName = "VarusQMissile",
                name = "Piercing Arrow",
                projectileSpeed = 1900,
                radius = 75,
                range = 1600,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "varusq",
                spellType = SpellType.Line,
                usePackets = true,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Varus",
                dangerlevel = 3,
                name = "Chain of Corruption",
                missileName = "VarusRMissile",
                projectileSpeed = 1950,
                radius = 100,
                range = 1200,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "VarusR",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, },

            });
            #endregion Varus

            #region Veigar

            Spells.Add(
            new SpellData
            {
                charName = "Veigar",
                dangerlevel = 2,
                missileName = "VeigarBalefulStrikeMis",
                name = "Baleful Strike",
                projectileSpeed = 2000,
                radius = 70,
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "VeigarBalefulStrike",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Veigar",
                dangerlevel = 2,
                missileName = "VeigarDarkMatter",
                name = "Dark Matter",
                projectileSpeed = float.MaxValue,
                radius = 225,
                range = 900,
                spellDelay = 1350,
                spellKey = SpellSlot.W,
                spellName = "VeigarDarkMatter",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Veigar",
                dangerlevel = 3,
                name = "Event Horizon",
                radius = 375,
                range = 700,
                spellDelay = 500,
                extraEndTime = 3300,
                spellKey = SpellSlot.E,
                spellName = "VeigarEventHorizon",
                spellType = SpellType.Circular,
                defaultOff = true,
            });

            #endregion Veigar

            #region Velkoz

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 3,
                missileName = "VelkozEMissile",
                name = "Tectonic Disruption",
                projectileSpeed = 1500,
                radius = 225,
                range = 950,
                spellDelay = 500,
                spellKey = SpellSlot.E,
                spellName = "VelkozE",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 1,
                missileName = "VelkozW",
                name = "Void Rift",
                projectileSpeed = 1700,
                radius = 90,
                range = 1100,
                extraEndTime = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "VelkozW",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 2,
                missileName = "VelkozQMissileSplit",
                name = "Plasma Fission (split)",
                projectileSpeed = 2100,
                radius = 90,
                range = 900,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "VelkozQMissileSplit",
                spellType = SpellType.Line,
                usePackets = true,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 2,
                missileName = "VelkozQMissile",
                name = "Plasma Fission",
                projectileSpeed = 1300,
                radius = 90,
                range = 1200,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "VelkozQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Velkoz

            #region Vi

            Spells.Add(
            new SpellData
            {
                charName = "Vi",
                dangerlevel = 3,
                missileName = "ViQMissile",
                name = "Vault Breaker",
                projectileSpeed = 1500,
                radius = 90,
                range = 725,
                spellKey = SpellSlot.Q,
                spellName = "ViQMissile",
                spellType = SpellType.Line,
                usePackets = true,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, },

            });
            #endregion Vi

            #region Viktor

            Spells.Add(
            new SpellData
            {
                charName = "Viktor",
                dangerlevel = 2,
                missileName = "ViktorDeathRayMissile",
                name = "Death Ray",
                projectileSpeed = 1050,
                radius = 80,
                range = 800,
                spellKey = SpellSlot.E,
                spellName = "ViktorDeathRay",
                extraMissileNames = new[] { "ViktorEAugMissile", },
                spellType = SpellType.Line,
                usePackets = true,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Viktor",
                dangerlevel = 2,
                name = "Death Ray 2",
                projectileSpeed = float.MaxValue,
                spellDelay = 500,
                radius = 80,
                range = 800,
                spellKey = SpellSlot.E,
                spellName = "ViktorDeathRay3",
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Viktor",
                dangerlevel = 3,
                name = "Graviton Field",
                radius = 300,
                range = 625,
                spellDelay = 1500,
                spellKey = SpellSlot.W,
                spellName = "ViktorGravitonField",
                spellType = SpellType.Circular,
                defaultOff = true,
            });

            #endregion Viktor

            #region Vladimir

            Spells.Add(
            new SpellData
            {
                charName = "Vladimir",
                dangerlevel = 3,
                missileName = "VladimirHemoplague",
                name = "Hemoplague",
                projectileSpeed = float.MaxValue,
                radius = 375,
                range = 700,
                spellDelay = 389,
                spellKey = SpellSlot.R,
                spellName = "VladimirHemoplague",
                spellType = SpellType.Circular,

            });
            #endregion Vladimir

            #region Xerath

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                dangerlevel = 2,
                missileName = "XerathArcaneBarrage2",
                name = "Eye of Destruction",
                projectileSpeed = float.MaxValue,
                radius = 270,
                range = 1100,
                spellDelay = 700,
                spellKey = SpellSlot.W,
                spellName = "XerathArcaneBarrage2",
                spellType = SpellType.Circular,
                extraDrawHeight = 45,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                dangerlevel = 3,
                missileName = "xeratharcanopulse2",
                name = "Arcanopulse",
                projectileSpeed = float.MaxValue,
                radius = 80,
                range = 1525,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "xeratharcanopulse2",
                useEndPosition = true,
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                dangerlevel = 2,
                name = "Rite of the Arcane",
                missileName = "xerathrmissilewrapper",
                extraMissileNames = new[] { "XerathLocusPulse" },
                radius = 200,
                range = 5600,
                spellDelay = 700,
                spellKey = SpellSlot.R,
                spellName = "xerathrmissilewrapper",
                extraSpellNames = new[] { "XerathLocusPulse" },
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                dangerlevel = 3,
                missileName = "XerathMageSpearMissile",
                name = "Shocking Orb",
                projectileSpeed = 1400,
                radius = 60,
                range = 1125,
                spellDelay = 200,
                spellKey = SpellSlot.E,
                spellName = "XerathMageSpear",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Xerath

            #region Yasuo

            Spells.Add(
            new SpellData
            {
                charName = "Yasuo",
                dangerlevel = 3,
                missileName = "YasuoQ3",
                extraMissileNames = new[] { "YasuoQ3Mis" },
                name = "Steel Tempest (Tornado)",
                projectileSpeed = 1500,
                radius = 90,
                range = 1150,
                spellDelay = 100,
                spellKey = SpellSlot.Q,
                spellName = "YasuoQ3",
                extraSpellNames = new[] { "YasuoQ3W" },
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Yasuo",
                dangerlevel = 2,
                missileName = "yasuoq",
                extraMissileNames = new[] { "yasuoq2" },
                name = "Steel Tempest",
                projectileSpeed = float.MaxValue,
                radius = 40,
                range = 550,
                fixedRange = true,
                spellDelay = 400,
                spellKey = SpellSlot.Q,
                spellName = "YasuoQ",
                extraSpellNames = new[] { "YasuoQ2" },
                spellType = SpellType.Line,
                invert = true
            });

            #endregion Yasuo

            #region Yorick
            //TODO: Yorick W and E
            #endregion Yorick

            #region Zac

            Spells.Add(
            new SpellData
            {
                charName = "Zac",
                dangerlevel = 3,
                missileName = "ZacQ",
                name = "Stretching Strike",
                projectileSpeed = float.MaxValue,
                radius = 120,
                range = 550,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "ZacQ",
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Zac",
                dangerlevel = 3,
                name = "Elastic Slingshot",
                projectileSpeed = 1000,
                radius = 300,
                range = 1800,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "ZacE",
                spellType = SpellType.Circular,
            });

            //TODO: Zac R

            #endregion Zac

            #region Zed

            Spells.Add(
            new SpellData
            {
                charName = "Zed",
                dangerlevel = 2,
                missileName = "ZedQMissile",
                name = "Razor Shuriken",
                projectileSpeed = 1700,
                radius = 50,
                range = 925,
                spellDelay = 300,
                spellKey = SpellSlot.Q,
                spellName = "ZedQ",
                spellType = SpellType.Line,
            });

            //TODO: Add Zed E

            #endregion Zed

            #region Ziggs

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                dangerlevel = 1,
                missileName = "ZiggsE",
                name = "Hexplosive Minefield",
                projectileSpeed = 3000,
                radius = 235,
                range = 2000,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "ZiggsE",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                dangerlevel = 3,
                missileName = "ZiggsW",
                name = "Satchel Charge",
                projectileSpeed = 3000,
                radius = 275,
                range = 2000,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "ZiggsW",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                dangerlevel = 2,
                missileName = "ZiggsQSpell",
                extraMissileNames = new[] { "ZiggsQSpell2", "ZiggsQSpell3" },
                name = "Bouncing Bomb",
                projectileSpeed = 1700,
                radius = 150,
                range = 850,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "ZiggsQ",
                spellType = SpellType.Circular,
                isSpecial = true,
                noProcess = true,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                dangerlevel = 4,
                missileName = "ZiggsR",
                name = "Mega Inferno Bomb",
                projectileSpeed = 1500,
                radius = 550,
                range = 5300,
                spellDelay = 1500,
                spellKey = SpellSlot.R,
                spellName = "ZiggsR",
                spellType = SpellType.Circular,
            });
            #endregion Ziggs

            #region Zilean

            Spells.Add(
            new SpellData
            {
                charName = "Zilean",
                dangerlevel = 3,
                missileName = "ZileanQMissile",
                name = "Time Bomb",
                radius = 140,
                range = 900,
                extraEndTime = 400,
                spellDelay = 650,
                spellKey = SpellSlot.Q,
                spellName = "ZileanQ",
                spellType = SpellType.Circular,
            });

            #endregion Zilean

            #region Zyra

            Spells.Add(
            new SpellData
            {
                charName = "Zyra",
                dangerlevel = 3,
                name = "Grasping Roots",
                missileName = "ZyraEMissile",
                projectileSpeed = 1150,
                radius = 90,
                range = 1150,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "ZyraE",
                spellType = SpellType.Line,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Zyra",
                dangerlevel = 2,
                missileName = "ZyraQ",
                name = "Deadly Bloom",
                projectileSpeed = float.MaxValue,
                radius = 140,
                range = 800,
                spellDelay = 850,
                spellKey = SpellSlot.Q,
                spellName = "ZyraQ",
                spellType = SpellType.Line,
                isPerpendicular = true,
                secondaryRadius = 400
            });

            Spells.Add(
            new SpellData
            {
                charName = "Zyra",
                dangerlevel = 4,
                missileName = "ZyraBrambleZone",
                name = "Stranglethorns",
                projectileSpeed = float.MaxValue,
                radius = 525,
                range = 700,
                extraEndTime = 2000,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "ZyraR",
                extraSpellNames = new[] { "ZyraBrambleZone", },
                spellType = SpellType.Circular,
                defaultOff = true
            });
            #endregion Zyra
        }
    }
}
