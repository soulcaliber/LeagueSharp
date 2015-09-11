using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

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
                missileName = "SummonerSnowball",
                name = "Poro Throw",
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
                name = "AatroxQ",
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
                dangerlevel = 1,
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
                dangerlevel = 2,
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
                dangerlevel = 3,
                name = "Pulverize",
                radius = 365,
                range = 365,
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
                name = "CurseoftheSadMummy",
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
                angle = 25,
                charName = "Annie",
                dangerlevel = 2,
                name = "Incinerate",
                radius = 80,
                range = 625,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "Incinerate",
                spellType = SpellType.Cone,

            });

            //RIP tibbers flash dodge
            /*
            Spells.Add(
            new SpellData
            {
                charName = "Annie",
                dangerlevel = 4,
                name = "InfernalGuardian",
                radius = 290,
                range = 600,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "InfernalGuardian",
                spellType = SpellType.Circular,

            });*/

            #endregion Annie

            #region Ashe

            Spells.Add(
            new SpellData
            {
                charName = "Ashe",
                dangerlevel = 3,
                name = "Enchanted Arrow",
                projectileSpeed = 1600,
                radius = 130,
                range = 25000,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "EnchantedCrystalArrow",
                spellType = SpellType.Line,
                //collisionObjects = new[] { CollisionObjectType.EnemyChampions, },
            });

            Spells.Add(
            new SpellData
            {
                angle = 5,
                charName = "Ashe",
                dangerlevel = 2,
                //missileName = "VolleyAttack",
                name = "Volley",
                projectileSpeed = 1500,
                radius = 20,
                range = 1200,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "Volley",
                spellType = SpellType.Line,
                isSpecial = true,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
            });
            #endregion Ashe

            #region Azir

            Spells.Add(
            new SpellData
            {
                charName = "Azir",
                dangerlevel = 2,
                name = "AzirQ",
                projectileSpeed = 1000,
                radius = 80,
                range = 800,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "AzirQ",
                spellType = SpellType.Line,
                usePackets = true,
                isSpecial = true,

            });
            #endregion Azir

            #region Bard

            Spells.Add(
            new SpellData
            {
                charName = "Bard",
                dangerlevel = 2,
                name = "BardQ",
                missileName = "BardQMissile",
                projectileSpeed = 1600,
                radius = 60,
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BardQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

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
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Blitzcrank

            #region Brand

            Spells.Add(
            new SpellData
            {
                charName = "Brand",
                dangerlevel = 3,
                missileName = "BrandBlazeMissile",
                name = "BrandBlaze",
                projectileSpeed = 2000, //1600
                radius = 60,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BrandBlaze",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                
            });

            Spells.Add(
            new SpellData
            {
                charName = "Brand",
                dangerlevel = 2,
                name = "Pillar of Flame",
                radius = 250,
                range = 1100,
                spellDelay = 850,
                spellKey = SpellSlot.W,
                spellName = "BrandFissure",
                spellType = SpellType.Circular,

            });
            #endregion Brand

            #region Braum

            Spells.Add(
            new SpellData
            {
                charName = "Braum",
                dangerlevel = 4,
                name = "GlacialFissure",
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
                name = "BraumQ",
                projectileSpeed = 1200,
                //spellDelay = 30000,
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
                dangerlevel = 2,
                missileName = "CaitlynEntrapmentMissile",
                name = "Caitlyn Entrapment",
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
                angle = 40,
                charName = "Cassiopeia",
                dangerlevel = 4,
                name = "CassiopeiaPetrifyingGaze",
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
                name = "CassiopeiaNoxiousBlast",
                radius = 200,
                range = 600,
                spellDelay = 825,
                spellKey = SpellSlot.Q,
                spellName = "CassiopeiaNoxiousBlast",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Cassiopeia",
                dangerlevel = 1,
                name = "CassiopeiaMiasma",
                radius = 220,
                range = 850,
                spellDelay = 250,
                projectileSpeed = 2500,
                spellKey = SpellSlot.W,
                spellName = "CassiopeiaMiasma",
                spellType = SpellType.Circular,

            });
            #endregion Cassiopeia

            #region Chogath

            Spells.Add(
            new SpellData
            {
                angle = 30,
                charName = "Chogath",
                dangerlevel = 2,
                name = "FeralScream",
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
                name = "Rupture",
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

            Spells.Add(
            new SpellData
            {
                angle = 25,
                charName = "Darius",
                dangerlevel = 3,
                name = "DariusAxeGrabCone",
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
                name = "DianaArc",
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
                name = "DravenR",
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
                name = "EkkoQ",
                missileName = "ekkoqmis",
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
                name = "EkkoW",
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
                name = "EkkoR",
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
                name = "EvelynnR",
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
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ezreal",
                dangerlevel = 2,
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

            //Testing purpose
            /*Spells.Add(
            new SpellData
            {
                charName = "Ezreal",
                dangerlevel = 1,
                name = "Essence Flux2",
                radius = 250,
                range = 1050,
                spellDelay = 825,
                spellKey = SpellSlot.W,
                spellName = "EzrealEssenceFlux",
                spellType = SpellType.Circular,

            });*/

            #endregion Ezreal

            #region Fiora

            Spells.Add(
            new SpellData
            {
                charName = "Fiora",
                dangerlevel = 1,
                missileName = "FioraWMissile",
                name = "FioraW",
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
                name = "Fizz ULT",
                projectileSpeed = 1350,
                radius = 120,
                range = 1275,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "FizzMarinerDoom",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, },
                //hasEndExplosion = true,
                secondaryRadius = 250,
                useEndPosition = true,
                //extraEndTime = 1000,

            });
            #endregion Fizz

            #region Galio

            Spells.Add(
            new SpellData
            {
                charName = "Galio",
                dangerlevel = 2,
                name = "GalioRighteousGust",
                projectileSpeed = 1300,
                radius = 160,
                range = 1280,
                spellKey = SpellSlot.E,
                spellName = "GalioRighteousGust",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Galio",
                dangerlevel = 2,
                name = "GalioResoluteSmite",
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
                name = "GalioIdolOfDurand",
                radius = 600,
                range = 600,
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
                name = "Boulder Toss",
                projectileSpeed = 2000,
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
                dangerlevel = 3,
                name = "GnarUlt",
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
                projectileSpeed = 2400,
                radius = 60,
                range = 1185,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "GnarQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            /*Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 2,
                name = "GnarE",
                spellName = "GnarE",
                range = 475,
                spellDelay = 0,
                radius = 150,
                fixedRange = true,
                projectileSpeed = 900,
                spellKey = SpellSlot.E,
                spellType = SpellType.Circular,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 2,
                name = "GnarBigE",
                spellName = "gnarbige",
                range = 475,
                spellDelay = 0,
                radius = 100,
                fixedRange = true,
                projectileSpeed = 800,
                spellKey = SpellSlot.E,
                spellType = SpellType.Circular,
            });*/

            #endregion Gnar

            #region Gragas

            Spells.Add(
            new SpellData
            {
                charName = "Gragas",
                dangerlevel = 2,
                name = "Barrel Roll",
                projectileSpeed = 1000,
                radius = 250,
                range = 975,
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
                name = "Barrel Roll",
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
                name = "GragasExplosiveCask",
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

            Spells.Add(
            new SpellData
            {
                angle = 18,
                charName = "Graves",
                dangerlevel = 2,
                isThreeWay = true,
                isSpecial = true,
                missileName = "GravesClusterShotAttack",
                name = "Buckshot",
                projectileSpeed = 2000,
                radius = 60,
                range = 1025,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "GravesClusterShot",
                spellType = SpellType.Line,

            });

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
                dangerlevel = 3,
                name = "HecarimUlt",
                projectileSpeed = 1100,
                radius = 300,
                range = 1500,
                spellDelay = 10,
                spellKey = SpellSlot.R,
                spellName = "HecarimUlt",
                spellType = SpellType.Circular,

            });
            #endregion Hecarim

            #region Heimerdinger

            Spells.Add(
            new SpellData
            {
                charName = "Heimerdinger",
                dangerlevel = 2,
                missileName = "HeimerdingerESpell",
                name = "HeimerdingerE",
                projectileSpeed = 1750,
                radius = 135,
                range = 925,
                spellDelay = 325,
                spellKey = SpellSlot.E,
                spellName = "HeimerdingerE",
                spellType = SpellType.Circular,

            });
            #endregion Heimerdinger

            #region Irelia

            Spells.Add(
            new SpellData
            {
                charName = "Irelia",
                dangerlevel = 2,
                missileName = "ireliatranscendentbladesspell",
                name = "IreliaTranscendentBlades",
                projectileSpeed = 1600,
                radius = 120,
                range = 1200,
                spellKey = SpellSlot.R,
                spellDelay = 0,
                spellName = "IreliaTranscendentBlades",
                spellType = SpellType.Line,
                usePackets = true,
                defaultOff = true,
            });
            #endregion Irelia

            #region Janna

            Spells.Add(
            new SpellData
            {
                charName = "Janna",
                dangerlevel = 2,
                missileName = "HowlingGaleSpell",
                name = "HowlingGaleSpell",
                projectileSpeed = 900,
                radius = 120,
                range = 1700,
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
                name = "JarvanIVDragonStrike",
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
                dangerlevel = 2,
                name = "JarvanIVDragonStrike",
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
                dangerlevel = 3,
                name = "JarvanIVCataclysm",
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
                name = "JayceShockBlastCharged",
                projectileSpeed = 2350,
                radius = 70,
                range = 1170,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "JayceShockBlastWall",
                spellType = SpellType.Line,
                usePackets = true,
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
                name = "JayceShockBlast",
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
                name = "JinxR",
                projectileSpeed = 1700, //accelerates to 2600
                radius = 140,
                range = 25000,
                spellDelay = 600,
                spellKey = SpellSlot.R,
                spellName = "JinxR",
                spellType = SpellType.Line,
                //collisionObjects = new[] { CollisionObjectType.EnemyChampions, },
            });

            Spells.Add(
            new SpellData
            {
                charName = "Jinx",
                dangerlevel = 3,
                //missileName = "JinxWMissile",
                name = "Zap",
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

            #region Kalista

            Spells.Add(
            new SpellData
            {
                charName = "Kalista",
                dangerlevel = 2,
                missileName = "kalistamysticshotmistrue",
                name = "KalistaQ",
                projectileSpeed = 2000,
                radius = 70,
                range = 1200,
                spellDelay = 350,
                spellKey = SpellSlot.Q,
                spellName = "KalistaMysticShot",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Kalista

            #region Karma

            Spells.Add(
            new SpellData
            {
                charName = "Karma",
                dangerlevel = 2,
                missileName = "KarmaQMissile",
                name = "KarmaQ",
                projectileSpeed = 1700,
                radius = 90,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KarmaQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            Spells.Add(
            new SpellData
            {
                charName = "Karma",
                dangerlevel = 2,
                missileName = "KarmaQMissileMantra",
                name = "KarmaQMantra",
                projectileSpeed = 1700,
                radius = 90,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KarmaQMissileMantra",
                spellType = SpellType.Line,
                usePackets = true,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Karma

            #region Karthus

            Spells.Add(
            new SpellData
            {
                charName = "Karthus",
                dangerlevel = 1,
                name = "Lay Waste",
                radius = 190,
                range = 875,
                spellDelay = 900,
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
                name = "RiftWalk",
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
                name = "ForcePulse",
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
                spellDelay = 180,
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
                dangerlevel = 1,
                missileName = "KhazixWMissile",
                name = "KhazixW",
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
                dangerlevel = 1,
                isThreeWay = true,
                name = "khazixwlong",
                projectileSpeed = 1700,
                radius = 70,
                range = 1025,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "khazixwlong",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Khazix

            #region KogMaw

            Spells.Add(
            new SpellData
            {
                charName = "KogMaw",
                dangerlevel = 1,
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
                name = "KogMawVoidOoze",
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
                name = "Living Artillery",
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
                dangerlevel = 2,
                name = "Ethereal Chains R",
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
                dangerlevel = 2,
                name = "Ethereal Chains",
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
                name = "LeblancSlideM",
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
                name = "LeblancSlide",
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
            #endregion LeeSin

            #region Leona

            Spells.Add(
            new SpellData
            {
                charName = "Leona",
                dangerlevel = 4,
                name = "Leona Solar Flare",
                radius = 300,
                range = 1200,
                spellDelay = 1000,
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
                spellDelay = 200,
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
                name = "LissandraW",
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
                name = "Ice Shard",
                projectileSpeed = 2250,
                radius = 75,
                range = 825,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LissandraQ",
                spellType = SpellType.Line,

            });
            #endregion Lissandra

            #region Lucian

            Spells.Add(
            new SpellData
            {
                charName = "Lucian",
                dangerlevel = 1,
                defaultOff = true,
                name = "LucianW",
                projectileSpeed = 1600,
                radius = 80,
                range = 1000,
                spellDelay = 300,
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
                //defaultOff = true,
                isSpecial = true,
                name = "LucianQ",
                projectileSpeed = float.MaxValue,
                radius = 65,
                range = 1140,
                spellDelay = 350,
                spellKey = SpellSlot.Q,
                spellName = "LucianQ",
                spellType = SpellType.Line,

            });
            #endregion Lucian

            #region Lulu

            Spells.Add(
            new SpellData
            {
                charName = "Lulu",
                dangerlevel = 2,
                missileName = "LuluQMissile",
                name = "LuluQ",
                projectileSpeed = 1450,
                radius = 80,
                range = 925,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LuluQ",
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
                name = "LuxLightStrikeKugel",
                projectileSpeed = 1400,
                radius = 340,
                range = 1100,
                extraEndTime = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "LuxLightStrikeKugel",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lux",
                dangerlevel = 3,
                name = "Lux Malice Cannon",
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
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Lux

            #region Malphite

            Spells.Add(
            new SpellData
            {
                charName = "Malphite",
                dangerlevel = 4,
                name = "UFSlash",
                projectileSpeed = 2000,
                radius = 300,
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
                //extraEndTime = 750,
                //defaultOff = true,
                isSpecial = true,
                isWall = true,
                //missileName = "AlZaharCalloftheVoidMissile",
                name = "AlZaharCalloftheVoid",
                projectileSpeed = 1600,
                radius = 85,
                range = 900,
                sideRadius = 400,
                spellDelay = 1000,
                spellKey = SpellSlot.Q,
                spellName = "AlZaharCalloftheVoid",
                spellType = SpellType.Line,

            });
            #endregion Malzahar

            #region MonkeyKing

            /*Spells.Add(
            new SpellData
            {
                charName = "MonkeyKing",
                dangerlevel = 3,
                name = "MonkeyKingSpinToWin",
                radius = 225,
                range = 300,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "MonkeyKingSpinToWin",
                spellType = SpellType.Circular,
                defaultOff = true,
            });*/
            #endregion MonkeyKing

            #region Morgana

            Spells.Add(
            new SpellData
            {
                charName = "Morgana",
                dangerlevel = 3,
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
                name = "NamiQ",
                radius = 200,
                range = 875,
                spellDelay = 1000,
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
                name = "NamiR",
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
                range = 1250,
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
                name = "Javelin Toss",
                projectileSpeed = 1300,
                radius = 60,
                range = 1500,
                spellDelay = 250,
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
                name = "NocturneDuskbringer",
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
                dangerlevel = 1,
                name = "Undertow",
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

            Spells.Add(
            new SpellData
            {
                charName = "Orianna",
                dangerlevel = 2,
                //hasEndExplosion = true,
                name = "OrianaIzunaCommand",
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
                dangerlevel = 4,
                name = "OrianaDetonateCommand",
                radius = 410,
                range = 410,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "OrianaDetonateCommand",
                spellType = SpellType.Circular,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Orianna",
                dangerlevel = 2,
                name = "OrianaDissonanceCommand",
                radius = 250,
                range = 1825,
                spellKey = SpellSlot.W,
                spellName = "OrianaDissonanceCommand",
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
                radius = 100,
                range = 650,
                spellDelay = 1000,
                spellKey = SpellSlot.E,
                spellName = "PantheonE",
                spellType = SpellType.Cone,

            });
            #endregion Pantheon

            #region Quinn

            Spells.Add(
            new SpellData
            {
                charName = "Quinn",
                dangerlevel = 2,
                missileName = "QuinnQMissile",
                name = "QuinnQ",
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
                name = "RekSaiQ",
                projectileSpeed = 1950,
                radius = 65,
                range = 1500,
                spellDelay = 125,
                spellKey = SpellSlot.E,
                spellName = "reksaiqburrowed",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion RekSai

            #region Rengar

            Spells.Add(
            new SpellData
            {
                charName = "Rengar",
                dangerlevel = 2,
                missileName = "RengarEFinal",
                name = "Bola Strike",
                projectileSpeed = 1500,
                radius = 70,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "RengarE",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Rengar

            #region Riven

            Spells.Add(
            new SpellData
            {
                angle = 15,
                charName = "Riven",
                dangerlevel = 2,
                isThreeWay = true,
                name = "WindSlash",
                projectileSpeed = 1600,
                radius = 100,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "rivenizunablade",
                spellType = SpellType.Line,
                isSpecial = true,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Riven",
                dangerlevel = 2,
                defaultOff = true,
                name = "RivenW",
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
                dangerlevel = 1,
                missileName = "RumbleGrenadeMissile",
                name = "RumbleGrenade",
                projectileSpeed = 2000,
                radius = 90,
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "RumbleGrenade",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });
            #endregion Rumble

            #region Ryze
            
            Spells.Add(
            new SpellData
            {
                charName = "Ryze",
                dangerlevel = 2,
                missileName = "RyzeQ",
                name = "RyzeQ",
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
                dangerlevel = 4,
                missileName = "SejuaniGlacialPrison",
                name = "SejuaniR",
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
                name = "ShadowDash",
                projectileSpeed = 1250,
                radius = 75,
                range = 700,
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
                name = "ShyvanaFireball",
                projectileSpeed = 1700,
                radius = 60,
                range = 950,
                spellKey = SpellSlot.E,
                spellName = "ShyvanaFireball",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Shyvana",
                dangerlevel = 3,
                name = "ShyvanaTransformCast",
                projectileSpeed = 1100,
                radius = 160,
                range = 1000,
                spellDelay = 10,
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
                dangerlevel = 2,
                //missileName = "SionEMissile",
                name = "SionE",
                projectileSpeed = 1800,
                radius = 80,
                range = 800,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "SionE",
                spellType = SpellType.Line,
                isSpecial = true,

            });
            #endregion Sion

            #region Sivir

            Spells.Add(
            new SpellData
            {
                charName = "Sivir",
                dangerlevel = 2,
                missileName = "SivirQMissile",
                name = "Boomerang Blade",
                projectileSpeed = 1350,
                radius = 100,
                range = 1275,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "SivirQ",
                spellType = SpellType.Line,

            });
            #endregion Sivir

            #region Skarner

            Spells.Add(
            new SpellData
            {
                charName = "Skarner",
                dangerlevel = 2,
                missileName = "SkarnerFractureMissile",
                name = "SkarnerFracture",
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
                name = "SorakaQ",
                projectileSpeed = 1100,
                radius = 260,
                range = 970,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "SorakaQ",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Soraka",
                dangerlevel = 3,
                name = "SorakaE",
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
                angle = 30,
                charName = "Syndra",
                dangerlevel = 3,
                name = "SyndraE",
                usePackets = true,
                projectileSpeed = 1500,
                radius = 140,
                range = 800,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "SyndraE",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Syndra",
                dangerlevel = 2,
                name = "SyndraW",
                projectileSpeed = 1450,
                radius = 220,
                range = 925,
                spellDelay = 0,
                spellKey = SpellSlot.W,
                spellName = "syndrawcast",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Syndra",
                dangerlevel = 2,
                name = "SyndraQ",
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
                dangerlevel = 2,
                missileName = "tahmkenchqmissile",
                name = "TahmKenchQ",
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
                name = "TalonRake",
                projectileSpeed = 2300,
                radius = 75,
                range = 780,
                spellKey = SpellSlot.W,
                spellName = "TalonRake",
                spellType = SpellType.Line,
                splits = 3,
                isSpecial = true,
            });
            #endregion Talon

            #region Thresh

            Spells.Add(
            new SpellData
            {
                charName = "Thresh",
                dangerlevel = 3,
                missileName = "ThreshQMissile",
                name = "ThreshQ",
                projectileSpeed = 1900,
                radius = 70,
                range = 1100,
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
                name = "ThreshE",
                projectileSpeed = 2000,
                radius = 110,
                range = 1075,
                spellDelay = 0,
                defaultOff = true,
                spellKey = SpellSlot.E,
                spellName = "ThreshE",
                spellType = SpellType.Line,
                usePackets = true,

            });
            #endregion Thresh

            #region TwistedFate

            Spells.Add(
            new SpellData
            {
                angle = 28,
                charName = "TwistedFate",
                dangerlevel = 2,
                isThreeWay = true,
                missileName = "SealFateMissile",
                name = "Loaded Dice",
                projectileSpeed = 1000,
                radius = 40,
                range = 1450,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "WildCards",
                spellType = SpellType.Line,
                isSpecial = true,
            });
            #endregion TwistedFate

            #region Urgot

            Spells.Add(
            new SpellData
            {
                charName = "Urgot",
                dangerlevel = 1,
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
                dangerlevel = 2,
                name = "Plasma Grenade",
                projectileSpeed = 1750,
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
                defaultOff = true,
                name = "Varus E",
                projectileSpeed = 1500,
                radius = 235,
                range = 925,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "VarusE",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Varus",
                dangerlevel = 2,
                missileName = "VarusQMissile",
                name = "VarusQMissile",
                projectileSpeed = 1900,
                radius = 70,
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
                name = "VarusR",
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
                name = "VeigarBalefulStrike",
                radius = 70,
                range = 950,
                spellDelay = 250,
                projectileSpeed = 1750,
                spellKey = SpellSlot.Q,
                spellName = "VeigarBalefulStrike",
                missileName = "VeigarBalefulStrikeMis",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Veigar",
                dangerlevel = 2,
                name = "VeigarDarkMatter",
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
                dangerlevel = 4,
                name = "VeigarEventHorizon",
                radius = 425,
                range = 700,
                spellDelay = 500,
                extraEndTime = 3500,
                spellKey = SpellSlot.E,
                spellName = "VeigarEventHorizon",
                spellType = SpellType.Circular,
            });

            #endregion Veigar

            #region Velkoz

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 2,
                name = "VelkozE",
                projectileSpeed = 1500,
                radius = 225,
                range = 950,
                spellKey = SpellSlot.E,
                spellName = "VelkozE",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 1,
                name = "VelkozW",
                projectileSpeed = 1700,
                radius = 88,
                range = 1100,
                spellKey = SpellSlot.W,
                spellName = "VelkozW",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 2,
                name = "VelkozQMissileSplit",
                projectileSpeed = 2100,
                radius = 45,
                range = 1100,
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
                name = "VelkozQ",
                projectileSpeed = 1300,
                radius = 50,
                range = 1250,
                spellKey = SpellSlot.Q,
                missileName = "VelkozQMissile",
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
                name = "ViQMissile",
                projectileSpeed = 1500,
                radius = 90,
                range = 725,
                spellKey = SpellSlot.Q,
                spellName = "ViQMissile",
                spellType = SpellType.Line,
                usePackets = true,
                defaultOff = true,
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
                name = "ViktorDeathRay",
                projectileSpeed = 780,
                radius = 80,
                range = 800,
                spellKey = SpellSlot.E,
                spellName = "ViktorDeathRay",
                extraMissileNames = new[] { "viktoreaugmissile", },
                spellType = SpellType.Line,
                usePackets = true,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Viktor",
                dangerlevel = 2,
                name = "ViktorDeathRay3",
                projectileSpeed = float.MaxValue,
                spellDelay = 500,
                radius = 80,
                range = 800,
                spellKey = SpellSlot.E,
                spellName = "ViktorDeathRay3",
                spellType = SpellType.Line,                
            });

            /*Spells.Add(
            new SpellData
            {
                charName = "Viktor",
                dangerlevel = 2,
                missileName = "ViktorDeathRayMissile2",
                name = "ViktorDeathRay2",
                projectileSpeed = 1500,
                radius = 80,
                range = 800,
                spellKey = SpellSlot.E,
                spellName = "ViktorDeathRay2",
                spellType = SpellType.Line,
                usePackets = true,

            });*/

            Spells.Add(
            new SpellData
            {
                charName = "Viktor",
                dangerlevel = 2,
                name = "GravitonField",
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
                name = "VladimirHemoplague",
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
                name = "XerathArcaneBarrage2",
                radius = 280,
                range = 1100,
                spellDelay = 750,
                spellKey = SpellSlot.W,
                spellName = "XerathArcaneBarrage2",
                spellType = SpellType.Circular,
                extraDrawHeight = 45,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                dangerlevel = 1,
                name = "XerathArcanopulse2",
                projectileSpeed = float.MaxValue,
                radius = 70,
                range = 1525,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "xeratharcanopulse2",
                extraSpellNames = new [] {"XerathArcanopulseChargeUp"},
                useEndPosition = true,
                spellType = SpellType.Line,
                //isSpecial = true,
            });

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                dangerlevel = 2,
                name = "XerathLocusOfPower2",
                radius = 200,
                range = 5600,
                spellDelay = 600,
                spellKey = SpellSlot.R,
                spellName = "xerathrmissilewrapper",
                spellType = SpellType.Circular,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                dangerlevel = 3,
                missileName = "XerathMageSpearMissile",
                name = "XerathMageSpear",
                projectileSpeed = 1600,
                spellDelay = 200,
                radius = 60,
                range = 1125,
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
                missileName = "yasuoq3mis",
                name = "Steel Tempest3",
                projectileSpeed = 1200,
                radius = 90,
                range = 1025,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "yasuoq3w",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Yasuo",
                dangerlevel = 2,
                name = "Steel Tempest",
                projectileSpeed = float.MaxValue,
                radius = 35,
                range = 500,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "YasuoQW",
                extraSpellNames = new [] {"yasuoq2w"},
                spellType = SpellType.Line,
                defaultOff = true,

            });

            #endregion Yasuo

            #region Zac
            #endregion Zac

            #region Zed

            Spells.Add(
            new SpellData
            {
                charName = "Zed",
                dangerlevel = 2,
                name = "ZedShuriken",                
                projectileSpeed = 1700,
                radius = 50,
                range = 925,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "ZedQ",
                spellType = SpellType.Line,
            });

            /*Spells.Add(
            new SpellData
            {
                charName = "Zed",
                dangerlevel = 1,
                name = "ZedE",
                radius = 290,
                range = 290,
                spellKey = SpellSlot.E,
                spellName = "ZedE",
                spellType = SpellType.Circular,
                isSpecial = true,
                defaultOff = true,
            });*/

            #endregion Zed

            #region Ziggs

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                dangerlevel = 1,
                name = "ZiggsE",
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
                dangerlevel = 1,
                name = "ZiggsW",
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
                name = "ZiggsQ",
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
                name = "ZiggsR",
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
                dangerlevel = 2,
                name = "ZileanQ",
                projectileSpeed = 2000,
                radius = 250,
                range = 900,
                spellDelay = 300,
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
                projectileSpeed = 1400, //1150
                radius = 70,
                range = 1150,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "ZyraGraspingRoots",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Zyra",
                dangerlevel = 2,
                missileName = "ZyraPassiveDeathMissile",
                name = "Zyra Passive",
                projectileSpeed = 2200,
                radius = 70,
                range = 1474,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "zyrapassivedeathmanager",
                spellType = SpellType.Line,

            });

            Spells.Add(
            new SpellData
            {
                charName = "Zyra",
                dangerlevel = 2,
                name = "Deadly Bloom",
                radius = 260,
                range = 825,
                spellDelay = 800,
                spellKey = SpellSlot.Q,
                spellName = "ZyraQFissure",
                spellType = SpellType.Circular,

            });

            /*Spells.Add(
            new SpellData
            {
                charName = "Zyra",
                dangerlevel = 4,
                name = "ZyraR",
                radius = 525,
                range = 700,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "ZyraBrambleZone",
                spellType = SpellType.Circular,

            });*/
            #endregion Zyra
        }
    }
}
