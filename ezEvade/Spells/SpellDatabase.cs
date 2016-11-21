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
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
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
                spellType = SpellType.Circular
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
                spellType = SpellType.Line
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
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ahri",
                dangerlevel = 3,
                name = "Orb of Deception Back",
                missileName = "AhriOrbReturn",
                projectileSpeed = 915,
                radius = 100,
                range = 925,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "AhriOrbofDeception2",
                spellType = SpellType.Line,
                isSpecial = true
            });

            #endregion Ahri

            #region Alistar

            Spells.Add(
            new SpellData
            {
                charName = "Alistar",
                defaultOff = true,
                dangerlevel = 3,
                name = "Pulverize",
                radius = 365,
                range = 365,
                spellKey = SpellSlot.Q,
                spellName = "Pulverize",
                spellType = SpellType.Circular
            });
                
            #endregion Alistar

            #region Amumu

            Spells.Add(
            new SpellData
            {
                charName = "Amumu",
                dangerlevel = 4,
                missileName = "",
                name = "Curse of the Sad Mummy",
                radius = 560,
                range = 560,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "CurseoftheSadMummy",
                spellType = SpellType.Circular
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
                secondaryRadius = 210,
                extraDrawHeight = 165,
                spellName = "FlashFrostSpell",
                spellType = SpellType.Line
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

            Spells.Add(
            new SpellData
            {
                charName = "Annie",
                dangerlevel = 4,
                missileName = "Incinerate",
                name = "Summom: Tibbers",
                radius = 290,
                range = 600,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "InfernalGuardian",
                spellType = SpellType.Circular
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
                //collisionObjects = new[] { CollisionObjectType.EnemyChampions },
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
                range = 1350,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "Volley",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                isSpecial = true
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
                dangerlevel = 4,
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
                projectileSpeed = 1600,
                radius = 80,
                range = 1150, // estimate radius can q
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "AzirQWrapper",
                spellType = SpellType.Line,
                isSpecial = true,
                noProcess = true
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
                dangerlevel = 3,
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
                fixedRange = true
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
                spellType = SpellType.Circular
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
                projectileSpeed = 2000, //1600
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
                dangerlevel = 2,
                name = "Pillar of Flame",
                radius = 250,
                range = 1100,
                spellDelay = 850,
                spellKey = SpellSlot.W,
                spellName = "BrandW",
                spellType = SpellType.Circular
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
                radius = 115,
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
                name = "Piltover Peacemaker",
                projectileSpeed = 2200,
                radius = 90,
                range = 1300,
                spellDelay = 625,
                spellKey = SpellSlot.Q,
                spellName = "CaitlynPiltoverPeacemaker",
                spellType = SpellType.Line
            });

            Spells.Add(
            new SpellData
            {
                charName = "Caitlyn",
                dangerlevel = 3,
                name = "Yordle Trap",
                radius = 75,
                range = 800,
                spellKey = SpellSlot.W,
                spellName = "CaitlynYordleTrap",
                trapBaseName = "CaitlynTrap",
                spellType = SpellType.Circular,
                hasTrap = true
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
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
            });

            #endregion Caitlyn

            #region Cassiopeia

            Spells.Add(
            new SpellData
            {
                angle = 40,
                charName = "Cassiopeia",
                dangerlevel = 4,
                name = "Petrifying Gaze",
                radius = 20,
                range = 825,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "CassiopeiaR",
                spellType = SpellType.Cone
            });

            //Spells.Add(
            //new SpellData
            //{
            //    charName = "Cassiopeia",
            //    dangerlevel = 1,
            //    name = "Miasama",
            //    //missileName = "CassiopeiaWMissile",
            //    //projectileSpeed = 2800,
            //    radius = 200,
            //    range = 900,
            //    spellDelay = 250,
            //    extraEndTime = 500,
            //    spellKey = SpellSlot.W,
            //    spellName = "CassiopeiaW",
            //    spellType = SpellType.Circular,
            //    trapTroyName = "cassiopeia_base_w_wcircle_tar_" + Situation.EmitterTeam() + ".troy",
            //    //updatePosition = false,
            //    hasTrap = true
            //});

            Spells.Add(
            new SpellData
            {
                charName = "Cassiopeia",
                dangerlevel = 1,
                missileName = "CassiopeiaQ",
                name = "Noxious Blast",
                radius = 200,
                range = 850,
                spellDelay = 825,
                spellKey = SpellSlot.Q,
                spellName = "CassiopeiaQ",
                spellType = SpellType.Circular
            });

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
                radius = 20,
                range = 650,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "FeralScream",
                spellType = SpellType.Cone
            });

            Spells.Add(
            new SpellData
            {
                charName = "Chogath",
                dangerlevel = 3,
                missileName = "Rupture",
                name = "Rupture",
                radius = 250,
                range = 950,
                spellDelay = 1200,
                spellKey = SpellSlot.Q,
                spellName = "Rupture",
                spellType = SpellType.Circular,
                extraDrawHeight = 45
            });

            #endregion Chogath

            #region Corki

            Spells.Add(
            new SpellData
            {
                charName = "Corki",
                dangerlevel = 3,
                missileName = "MissileBarrageMissile2",
                name = "Missile Barrage Big",
                projectileSpeed = 2000,
                radius = 40,
                range = 1500,
                spellDelay = 175,
                spellKey = SpellSlot.R,
                spellName = "MissileBarrage2",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
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
                extraDrawHeight = 110
            });

            Spells.Add(
            new SpellData
            {
                charName = "Corki",
                dangerlevel = 2,
                missileName = "MissileBarrageMissile",
                name = "Missile Barrage",
                projectileSpeed = 2000,
                radius = 40,
                range = 1300,
                spellDelay = 175,
                spellKey = SpellSlot.R,
                spellName = "MissileBarrage",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
            });

            #endregion Corki

            #region Darius
            Spells.Add(
            new SpellData
            {
                charName = "Darius",
                dangerlevel = 2,
                name = "Decimate [Beta]",
                radius = 425,
                range = 425,
                spellDelay = 750,
                spellKey = SpellSlot.Q,
                spellName = "DariusCleave",
                spellType = SpellType.Circular,
                defaultOff = true
            });

            Spells.Add(
            new SpellData
            {
                angle = 25,
                charName = "Darius",
                dangerlevel = 3,
                missileName = "DariusAxeGrabCone",
                name = "Axe Cone Grab",
                radius = 20,
                range = 570,
                spellDelay = 320,
                spellKey = SpellSlot.E,
                spellName = "DariusAxeGrabCone",
                spellType = SpellType.Cone
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
                extraEndTime = 250
            });

            #endregion Diana

            #region DrMundo

            Spells.Add(
            new SpellData
            {
                charName = "DrMundo",
                dangerlevel = 2,
                missileName = "InfectedCleaverMissile",
                name = "Infected Cleaver",
                projectileSpeed = 2000,
                radius = 60,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "InfectedCleaverMissileCast",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
            });

            #endregion DrMundo

            #region Draven

            Spells.Add(
            new SpellData
            {
                charName = "Draven",
                dangerlevel = 2,
                missileName = "DravenR",
                name = "Whirling Death",
                projectileSpeed = 2000,
                radius = 160,
                range = 25000,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "DravenRCast",
                spellType = SpellType.Line
            });

            Spells.Add(
            new SpellData
            {
                charName = "Draven",
                dangerlevel = 3,
                missileName = "DravenDoubleShotMissile",
                name = "Stand Aside",
                projectileSpeed = 1400,
                radius = 130,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "DravenDoubleShot",
                spellType = SpellType.Line
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
                missileName = "ekkoqreturn", // todo: add special spell
                name = "Timewinder (Return)",
                projectileSpeed = 2300,
                radius = 100,
                range = 1250,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "ekkoqreturn",
                spellType = SpellType.Line
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ekko",
                dangerlevel = 2,
                missileName = "ekkowmis",
                name = "Parallel Convergence",
                radius = 375,
                range = 1600,
                spellDelay = 2250,
                spellKey = SpellSlot.W,
                spellName = "ekkow",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ekko",
                dangerlevel = 3,
                missileName = "EkkoR",
                name = "Chronobreak",
                radius = 375,
                range = 1600,
                spellDelay = 100,
                spellKey = SpellSlot.R,
                spellName = "EkkoR",
                spellType = SpellType.Circular,
                isSpecial = true
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
                radius = 55,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "EliseHumanE",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
            });

            #endregion Elise

            #region Evelynn

            Spells.Add(
            new SpellData
            {
                charName = "Evelynn",
                dangerlevel = 4,
                name = "Agony's Embrace",
                radius = 350,
                range = 650,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "EvelynnR",
                spellType = SpellType.Circular
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
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ezreal",
                dangerlevel = 2,
                missileName = "EzrealTrueshotBarrage",
                name = "Trueshot Barrage",
                projectileSpeed = 2000,
                radius = 160,
                range = 25000,
                spellDelay = 1000,
                spellKey = SpellSlot.R,
                spellName = "EzrealTrueshotBarrage",
                spellType = SpellType.Line
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
                spellType = SpellType.Line
            });

            #endregion Ezreal

            #region Fiora

            Spells.Add(
            new SpellData
            {
                charName = "Fiora",
                dangerlevel = 2,
                name = "Riposte",
                projectileSpeed = 3200,
                radius = 70,
                range = 800,
                spellDelay = 500,
                spellKey = SpellSlot.W,
                spellName = "FioraW",
                spellType = SpellType.Line
            });

            #endregion Fiora

            #region Fizz

            Spells.Add(
            new SpellData
            {
                charName = "Fizz",
                dangerlevel = 2,
                name = "Urchin Strike",
                projectileSpeed = 1400,
                radius = 150,
                range = 550,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "FizzQ",
                spellType = SpellType.Line,
                isSpecial = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Fizz",
                dangerlevel = 4,
                missileName = "FizzRMissile",
                name = "Chum the Waters [Beta]",
                projectileSpeed = 1350,
                radius = 120,
                range = 1275,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "FizzR",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, },
                secondaryRadius = 250,
                hasEndExplosion = true,
                useEndPosition = true

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
                spellType = SpellType.Line
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
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Galio",
                dangerlevel = 4,
                name = "Idol Of Durand",
                radius = 575,
                range = 575,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "GalioIdolOfDurand",
                spellType = SpellType.Circular
            });

            #endregion Galio

            #region Gnar

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 2,
                missileName = "gnarbigqmissile",
                name = "Boulder Toss",
                projectileSpeed = 2000,
                radius = 90,
                range = 1150,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "gnarbigq",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
            });

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 4,
                missileName = "GnarR",
                name = "GNAR!",
                radius = 590,
                range = 590,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "GnarR", // todo: check wall
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                dangerlevel = 3,
                name = "Wallop",
                radius = 100,
                range = 525,
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
                missileName = "GnarQMissile",
                extraMissileNames = new[] { "GnarQMissileReturn" }, // todo: special spell
                projectileSpeed = 2400,
                radius = 60,
                range = 1125,
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
                projectileSpeed = 880,
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
                radius = 350,
                range = 475,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "gnarbige",
                spellType = SpellType.Circular
            });

            #endregion Gnar

            #region Gragas

            Spells.Add(
            new SpellData
            {
                charName = "Gragas",
                dangerlevel = 2,
                missileName = "GragasQMissile",
                name = "Barrel Roll",
                projectileSpeed = 1000,
                radius = 260,
                range = 975,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "GragasQ",
                spellType = SpellType.Circular,
                extraDrawHeight = 50
            });

            Spells.Add(
            new SpellData
            {
                charName = "Gragas",
                dangerlevel = 2,
                name = "Barrel Roll",
                radius = 270,
                range = 975,
                spellKey = SpellSlot.Q,
                spellName = "GragasQ",
                spellType = SpellType.Circular,
                extraDrawHeight = 45,
                trapTroyName = "gragas_base_q_" + Situation.EmitterTeam() + ".troy",
                hasTrap = true
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

            Spells.Add(
            new SpellData
            {
                charName = "Graves",
                dangerlevel = 3,
                missileName = "GravesQLineMis",
                name = "End of the Line",
                projectileSpeed = 3000,
                radius = 72,
                range = 800,
                spellDelay = 250,
                extraEndTime = 1300,
                spellKey = SpellSlot.Q,
                spellName = "GravesQLineSpell",
                spellType = SpellType.Line,
                fixedRange = true,
                isSpecial = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Graves",
                dangerlevel = 2,
                missileName = "GravesQReturn",
                name = "End of the Line (Return)",
                projectileSpeed = 1600,
                radius = 100,
                range = 900,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "GravesQLineSpell",
                spellType = SpellType.Line
            });

            Spells.Add(
            new SpellData
            {
                charName = "Graves",
                dangerlevel = 1,
                defaultOff = true,
                missileName = "GravesSmokeGrenadeBoom",
                name = "Smoke Screen",
                projectileSpeed = 1500,
                radius = 250,
                range = 900,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "GravesSmokeGrenade",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Graves",
                dangerlevel = 4,
                missileName = "GravesChargeShot",
                name = "Collateral Damage",
                projectileSpeed = 2100,
                radius = 100,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "GravesChargeShot",
                spellType = SpellType.Line,
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Graves",
                dangerlevel = 4,
                missileName = "GravesChargeShotFxMissile",
                name = "Collateral Damage (Explosion)",
                projectileSpeed = 2000,
                radius = 100,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "GravesChargeShotFxMissile",
                extraMissileNames = new []{ "GravesChargeShotFxMissile2" },
                spellType = SpellType.Line,
                fixedRange = true
            });

            #endregion Graves

            #region Hecarim

            Spells.Add(
            new SpellData
            {
                charName = "Hecarim",
                dangerlevel = 4,
                name = "Onslaught of Shadows",
                projectileSpeed = 1250,
                radius = 40,
                range = 1500,
                spellDelay = 100,
                spellKey = SpellSlot.R,
                spellName = "HecarimUltMissile",
                spellType = SpellType.Line,
                usePackets = true,
                fixedRange = true
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
                range = 1350,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "HeimerdingerW",
                spellType = SpellType.Line
            });

            Spells.Add(
            new SpellData
            {
                charName = "Heimerdinger",
                dangerlevel = 3,
                missileName = "HeimerdingerESpell",
                name = "CH-2 Electron Storm Grenade",
                projectileSpeed = 1200,
                radius = 150,
                range = 925,
                spellDelay = 325,
                spellKey = SpellSlot.E,
                spellName = "HeimerdingerE",
                extraMissileNames = new[] { "heimerdingerespell_ult", "heimerdingerespell_ult2", "heimerdingerespell_ult3" },
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Heimerdinger",
                dangerlevel = 2,
                missileName = "HeimerdingerTurretEnergyBlast",
                name = "Turret Energy Blast",
                projectileSpeed = 1650,
                radius = 50,
                range = 1000,
                spellDelay = 435,
                spellKey = SpellSlot.Q,
                spellName = "HeimerdingerTurretEnergyBlast",
                spellType = SpellType.Line,
                isSpecial = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Heimerdinger",
                dangerlevel = 3,
                missileName = "HeimerdingerTurretBigEnergyBlast",
                name = "Big Turret Energy Blast",
                projectileSpeed = 1800,
                radius = 75,
                range = 1000,
                spellDelay = 350,
                spellKey = SpellSlot.Q,
                spellName = "HeimerdingerTurretBigEnergyBlast",
                spellType = SpellType.Line
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
                radius = 100,
                range = 850,
                spellDelay = 750,
                spellKey = SpellSlot.Q,
                spellName = "IllaoiQ",
                spellType = SpellType.Line,
                fixedRange = true
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
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Illaoi",
                dangerlevel = 3,
                name = "Leap of Faith",
                range = 500,
                radius = 450,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "IllaoiR",
                spellType = SpellType.Circular
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
                radius = 120,
                range = 1200,
                spellDelay = 0,
                spellKey = SpellSlot.R,
                spellName = "IreliaTranscendentBlades",
                spellType = SpellType.Line,
                usePackets = true
            });

            #endregion Irelia
                
            #region Ivern
         
            Spells.Add(
            new SpellData
            {
                charName = "Ivern",                  
                dangerlevel = 3,
                name = "Rootcaller",
                projectileSpeed = 1300,
                radius = 65,
                range = 1150,
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
                spellType = SpellType.Line
            });

            Spells.Add(
            new SpellData
            {
                charName = "JarvanIV",
                dangerlevel = 3,
                name = "Dragon Strike EQ",
                projectileSpeed = 1800,
                radius = 120,
                range = 845,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "JarvanIVDragonStrike2",
                spellType = SpellType.Line,
                useEndPosition = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "JarvanIV",
                dangerlevel = 1,
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
                defaultOff = true

            });
            #endregion JarvanIV

            #region Jayce

            Spells.Add(
            new SpellData
            {
                charName = "Jayce",
                dangerlevel = 2,
                name = "Shock Blast",
                projectileSpeed = 1450,
                radius = 70,
                range = 1170,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                hasEndExplosion = true,
                spellName = "jayceshockblastmis",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                secondaryRadius = 210
            });

            Spells.Add(
            new SpellData
            {
                charName = "Jayce",
                dangerlevel = 3,
                name = "Shock Blast Fast",
                projectileSpeed = 2350,
                radius = 70,
                range = 1600,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                hasEndExplosion = true,
                spellName = "jayceshockblastwallmis",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                secondaryRadius = 210,
                fixedRange = true
            });

            #endregion Jayce

            #region Jinx

            Spells.Add(
            new SpellData
            {
                charName = "Jinx",
                dangerlevel = 3,
                name = "Super Mega Death Rocket!",
                projectileSpeed = 1700,
                radius = 140,
                range = 25000,
                spellDelay = 600,
                spellKey = SpellSlot.R,
                spellName = "JinxR",
                extraMissileNames = new [] { "JinxRWrapper" },
                spellType = SpellType.Line,
                fixedRange = true
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
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Jinx",
                dangerlevel = 3,
                name = "Flame Chompers!",
                //missileName = "JinxEHit",
                //projectileSpeed = 2300,
                radius = 120,
                range = 900,
                spellDelay = 1200,
                spellKey = SpellSlot.E,
                spellName = "JinxE",
                spellType = SpellType.Circular,
                hasTrap = true,
                trapBaseName = "jinxmine",
                updatePosition = false
            });

            #endregion Jinx

            #region Jhin   

            Spells.Add(
            new SpellData
            {
                charName = "Jhin",
                dangerlevel = 3,
                //missileName = "JhinWMissile", there is no missile
                name = "Deadly Flourish",
                radius = 40,
                range = 2550,
                spellDelay = 650,
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
                dangerlevel = 2,
                missileName = "JhinRShotMis",
                name = "Curtain Call",
                projectileSpeed = 5000,
                radius = 80,
                range = 3500,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "JhinRShot",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions },
                extraMissileNames = new[] { "JhinRShotMis4" },
                fixedRange = true
            });

            #endregion

            #region Kalista

            Spells.Add(
            new SpellData
            {
                charName = "Kalista",
                dangerlevel = 2,
                missileName = "kalistamysticshotmis",
                name = "Pierce",
                projectileSpeed = 1700,
                radius = 45,
                range = 1200,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KalistaMysticShot",
                extraMissileNames = new []{ "kalistamysticshotmistrue" },
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
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
                radius = 70,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KarmaQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
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
                radius = 190,
                range = 875,
                spellDelay = 625,
                spellKey = SpellSlot.Q,
                spellName = "KarthusLayWasteA1",
                spellType = SpellType.Circular,
                extraSpellNames = new[] { "karthuslaywastea2", "karthuslaywastea3", "karthuslaywastedeada1", "karthuslaywastedeada2", "karthuslaywastedeada3" }
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
                radius = 270,
                range = 450,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "RiftWalk",
                spellType = SpellType.Circular
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
                spellType = SpellType.Cone
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
                range = 1050,
                spellDelay = 125,
                spellKey = SpellSlot.Q,
                spellName = "KennenShurikenHurlMissile1",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
        
            });
            #endregion Kennen

            #region Khazix

            Spells.Add(
            new SpellData
            {
                charName = "Khazix",
                dangerlevel = 1,
                missileName = "KhazixWMissile",
                name = "Void Spike",
                projectileSpeed = 1700,
                radius = 70,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "KhazixW",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
            });

            Spells.Add(
            new SpellData
            {
                angle = 22,
                charName = "Khazix",
                dangerlevel = 2,
                isThreeWay = true,
                name = "Void Spike Evolved",
                projectileSpeed = 1700,
                radius = 70,
                range = 1025,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "khazixwlong",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions }
            });

            Spells.Add(
            new SpellData
            {
                charName = "Khazix",
                dangerlevel = 1,
                missileName = "khazixe",
                name = "Leap",
                projectileSpeed = 1200,
                radius = 300,
                range = 700,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "KhazixE",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Khazix",
                dangerlevel = 1,
                missileName = "khazixelong",
                name = "Leap Evolved",
                projectileSpeed = 1200,
                radius = 300,
                range = 900,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "khazixelong",
                spellType = SpellType.Circular
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
                //splits = 5 this "splits" is not even implemented
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
                spellType = SpellType.Line
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
                spellType = SpellType.Line
            });

            #endregion Kled

            #region KogMaw

            Spells.Add(
            new SpellData
            {
                charName = "KogMaw",
                dangerlevel = 2,
                missileName = "KogMawQ",
                name = "Caustic Spittle",
                projectileSpeed = 1650,
                radius = 70,
                range = 1200,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KogMawQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "KogMaw",
                dangerlevel = 2,
                missileName = "KogMawVoidOozeMissile",
                name = "Void Ooze",
                projectileSpeed = 1350,
                radius = 120,
                range = 1360,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "KogMawVoidOoze",
                spellType = SpellType.Line,
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "KogMaw",
                dangerlevel = 2,
                missileName = "KogMawLivingArtillery",
                name = "Living Artillery",
                radius = 235,
                range = 2200,
                spellDelay = 1100,
                spellKey = SpellSlot.R,
                spellName = "KogMawLivingArtillery",
                spellType = SpellType.Circular
            });

            #endregion KogMaw

            #region Leblanc

            Spells.Add(
            new SpellData
            {
                charName = "Leblanc",
                dangerlevel = 3,
                name = "Ethereal Chains [Beta]",
                missileName = "LeblancEMissile",
                projectileSpeed = 1750,
                radius = 55,
                range = 960,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "LeblancE",
                extraMissileNames = new []{ "LeblancRE" },
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Leblanc",
                dangerlevel = 2,
                name = "Distortion [Beta]",
                projectileSpeed = 1450,
                radius = 250,
                range = 600,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "LeblancW",
                spellType = SpellType.Circular
            });

            #endregion Leblanc

            #region LeeSin

            Spells.Add(
            new SpellData
            {
                charName = "LeeSin",
                dangerlevel = 3,
                missileName = "BlindMonkQOne",
                name = "Sonic Wave",
                projectileSpeed = 1800,
                radius = 60,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BlindMonkQOne",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
            });

            //TODO: Add LeeSin R?
            //Spells.Add(
            //new SpellData
            //{
            //    charName = "LeeSin",
            //    dangerlevel = 3,
            //    missileName = "",
            //    name = "Dragon's Rage",
            //    projectileSpeed = 1000,
            //    radius = 0,
            //    range = 850,
            //    spellDelay = 250,
            //    spellKey = SpellSlot.R,
            //    isSpecial = true,
            //    spellName = "blindmonkrkick",
            //    spellType = SpellType.Line,
            //    noProcess = true
            //});

            #endregion LeeSin

            #region Leona

            Spells.Add(
            new SpellData
            {
                charName = "Leona",
                dangerlevel = 4,
                missileName = "LeonaSolarFlare",
                name = "Solar Flare",
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
                range = 900,
                spellDelay = 200,
                spellKey = SpellSlot.E,
                spellName = "LeonaZenithBlade",
                spellType = SpellType.Line,
                fixedRange = true
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
                radius = 450,
                range = 450,
                spellDelay = 125,
                spellKey = SpellSlot.W,
                spellName = "LissandraW",
                spellType = SpellType.Circular,
                defaultOff = true
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
                range = 700,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LissandraQ",
                spellType = SpellType.Line,
                fixedRange = true
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
                range = 1025,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LissandraQShards",
                spellType = SpellType.Line
            });

            Spells.Add(
            new SpellData
            {
                charName = "Lissandra",
                dangerlevel = 1,
                name = "Glacial Path",
                missileName = "LissandraEMissile",
                projectileSpeed = 850,
                radius = 125,
                range = 1025,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "LissandraE",
                spellType = SpellType.Line,
                fixedRange = true
            });

            #endregion Lissandra

            #region Lucian

            Spells.Add(
            new SpellData
            {
                charName = "Lucian",
                dangerlevel = 1,
                defaultOff = true,
                missileName = "lucianwmissile",
                name = "Ardent Blaze",
                projectileSpeed = 1600,
                radius = 80,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "lucianw",
                hasEndExplosion = true,
                secondaryRadius = 145,
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Lucian",
                dangerlevel = 3,
                isSpecial = true,
                missileName = "LucianQ",
                name = "Piercing Light",
                radius = 65,
                range = 1140,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LucianQ",
                spellType = SpellType.Line
            });

            Spells.Add(
            new SpellData
            {
                charName = "Lucian",
                dangerlevel = 2,
                missileName = "lucianrmissile",
                name = "The Culling",
                projectileSpeed = 2800,
                radius = 110,
                range = 1400,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "lucianrmis",
                extraMissileNames = new[] { "lucianrmissileoffhand" },
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                dontcheckDuplicates = true,
                fixedRange = true
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
                radius = 60,
                range = 925,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LuluQ",
                extraSpellNames = new[] { "LuluQPix" },
                spellType = SpellType.Line,
                isSpecial = true
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
                projectileSpeed = 1300,
                radius = 330,
                range = 1100,
                spellDelay = 250,
                extraEndTime = 500,
                spellKey = SpellSlot.E,
                spellName = "LuxLightStrikeKugel",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Lux",
                dangerlevel = 2,
                name = "Lucent Singularity",
                radius = 330,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "LuxLightStrikeKugel",
                spellType = SpellType.Circular,
                trapTroyName = "lux_base_e_tar_aoe_" + Situation.EmitterColor() + ".troy",
                extraDrawHeight = -100,
                hasTrap = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Lux",
                dangerlevel = 4,
                //missileName = "LuxRVfxMis", this missile is detected to late
                name = "Final Spark",
                radius = 190,
                range = 3300,
                spellDelay = 1000,
                spellKey = SpellSlot.R,
                spellName = "LuxMaliceCannon",
                spellType = SpellType.Line,
                fixedRange = true
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
                fixedRange = true
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
            //Spells.Add(
            //new SpellData
            //{
            //    charName = "Maokai",
            //    dangerlevel = 3,
            //    name = "Arcane Smash KnockBack",
            //    radius = 100,
            //    range = 100,
            //    spellDelay = 250,
            //    spellKey = SpellSlot.Q,
            //    spellName = "MaokaiTrunkLine",
            //    spellType = SpellType.Circular,
            //});

            Spells.Add(
            new SpellData
            {
                charName = "Maokai",
                dangerlevel = 1,
                name = "Sapling Toss",
                projectileSpeed = 1000,
                radius = 250,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "MaokaiSapling2",
                spellType = SpellType.Circular
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
                projectileSpeed = 2000,
                radius = 300,
                range = 1000,
                spellDelay = 0,
                spellKey = SpellSlot.R,
                spellName = "UFSlash",
                spellType = SpellType.Circular
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
                radius = 85,
                range = 900,
                sideRadius = 400,
                spellDelay = 830,
                spellKey = SpellSlot.Q,
                spellName = "MalzaharQ",
                spellType = SpellType.Line
            });

            #endregion Malzahar

            #region MonkeyKing

            //Spells.Add(
            //new SpellData
            //{
            //    charName = "MonkeyKing",
            //    dangerlevel = 4,
            //    defaultOff = true,
            //    name = "Cyclone",
            //    radius = 450,
            //    range = 450,
            //    spellDelay = 0,
            //    spellKey = SpellSlot.R,
            //    spellName = "MonkeyKingSpinToWin",
            //    spellType = SpellType.Circular
            //});

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
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Morgana",
                dangerlevel = 2,
                name = "Tormented Soil",
                radius = 279,
                range = 1300,
                spellKey = SpellSlot.W,
                spellName = "TormentedSoil",
                spellType = SpellType.Circular,
                trapTroyName = "morgana_base_w_tar_" + Situation.EmitterColor() + ".troy",
                hasTrap = true
            });

            #endregion Morgana

            #region Nami

            Spells.Add(
            new SpellData
            {
                charName = "Nami",
                dangerlevel = 3,
                missileName = "namiqmissile",
                name = "Aqua Prison",
                projectileSpeed = 2500,
                radius = 200,
                range = 875,
                spellDelay = 450,
                spellKey = SpellSlot.Q,
                spellName = "namiq",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Nami",
                dangerlevel = 2,
                missileName = "namirmissile",
                name = "Tidal Wave",
                projectileSpeed = 850,
                radius = 250,
                range = 2750,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "NamiR",
                spellType = SpellType.Line,
                fixedRange = true
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
                range = 1150,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "NautilusAnchorDrag",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
            });

            #endregion Nautilus

            #region Nidalee

            Spells.Add(
            new SpellData
            {
                charName = "Nidalee",
                dangerlevel = 3,
                missileName = "JavelinToss",
                name = "Javelin Toss",
                projectileSpeed = 1300,
                radius = 40,
                range = 1500,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "JavelinToss",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Nidalee",
                dangerlevel = 3,
                name = "Bushwhack",
                radius = 80,
                range = 1500,
                spellKey = SpellSlot.W,
                spellName = "Bushwhack",
                spellType = SpellType.Circular,
                trapTroyName = "nidalee_base_w_tc_" + Situation.EmitterColor() + ".troy",
                hasTrap = true
            });

            #endregion Nidalee

            #region Nocturne

            Spells.Add(
            new SpellData
            {
                charName = "Nocturne",
                dangerlevel = 2,
                missileName = "NocturneDuskbringer",
                name = "Duskbringer",
                projectileSpeed = 1400,
                radius = 60,
                range = 1200,
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
                spellType = SpellType.Line
            });

            #endregion Olaf

            #region Orianna

            Spells.Add(
            new SpellData
            {
                charName = "Orianna",
                dangerlevel = 2,
                missileName = "OrianaIzunaCommand",
                name = "Commnad: Attack",
                projectileSpeed = 1200,
                radius = 80,
                secondaryRadius = 145,
                range = 1650,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                useEndPosition = true,
                //hasEndExplosion = true,
                spellName = "OrianaIzunaCommand",
                spellType = SpellType.Line,
                isSpecial = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Orianna",
                dangerlevel = 2,
                missileName = "OrianaDissonanceCommand",
                name = "Command: Dissonance",
                radius = 250,
                range = 1825,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "OrianaDissonanceCommand",
                spellType = SpellType.Circular,
                defaultOff = true
            });

            //TODO: Add Orianna E

            Spells.Add(
            new SpellData
            {
                charName = "Orianna",
                dangerlevel = 4,
                missileName = "OrianaDetonateCommand",
                name = "Command: Shockwave",
                radius = 410,
                range = 410,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "OrianaDetonateCommand",
                spellType = SpellType.Circular
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
                radius = 200,
                range = 450,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "PoppyRSpellInstant",
                spellType = SpellType.Circular
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
                fixedRange = true
            });

            #endregion

            #region Quinn

            Spells.Add(
            new SpellData
            {
                charName = "Quinn",
                dangerlevel = 3,
                missileName = "QuinnQ",
                name = "Blinding Assault",
                projectileSpeed = 1550,
                radius = 60,
                range = 1050,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "QuinnQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
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
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "RekSai",
                dangerlevel = 3,
                missileName = "ReksaiWBurrowed",
                name = "Unburrow",
                projectileSpeed = 2300,
                radius = 160,
                range = 160,
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
                name = "Savagery [Beta]",
                radius = 150,
                range = 500,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "RengarQ2",
                extraSpellNames = new[] { "RengarQ2Emp" },
                spellType = SpellType.Line
            });

            Spells.Add(
            new SpellData
            {
                charName = "Rengar",
                dangerlevel = 3,
                missileName = "RengarEMis",
                name = "Bola Strike [Beta]",
                projectileSpeed = 1500,
                radius = 70,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "RengarE",
                extraSpellNames = new []{ "RengarEEmp" },
                extraMissileNames = new [] { "RengerEEmpMis" },
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },

            });

            #endregion Rengar

            #region Riven
                
            Spells.Add(
            new SpellData
            {
                charName = "Riven",
                dangerlevel = 3,
                defaultOff = true,
                missileName = "RivenMartyr",
                name = "Ki Burst",
                radius = 280,
                range = 650,
                spellDelay = 0,
                spellKey = SpellSlot.W,
                spellName = "RivenMartyr",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                angle = 15,
                charName = "Riven",
                dangerlevel = 4,
                isThreeWay = true,
                name = "Wind Slash",
                projectileSpeed = 1600, 
                radius = 100,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "RivenIzunaBlade",
                spellType = SpellType.Line,
                isSpecial = true,
                fixedRange = true
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
                radius = 60,
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "RumbleGrenade",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
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
                spellDelay = 0,
                spellKey = SpellSlot.R,
                spellName = "RumbleCarpetBomb",
                spellType = SpellType.Line,
                usePackets = true,
                fixedRange = true
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
                fixedRange = true
            });

            #endregion Ryze

            #region Sejuani
            Spells.Add(
            new SpellData
            {
                charName = "Sejuani",
                dangerlevel = 3,
                name = "Arctic Assault",
                projectileSpeed = 1250,
                radius = 75,
                range = 850,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "SejuaniArcticAssault",
                spellType = SpellType.Line
            });

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
                hasEndExplosion = true,
                secondaryRadius = 350,
                spellKey = SpellSlot.R,
                spellName = "SejuaniGlacialPrisonCast",
                extraSpellNames = new [] { "SejuaniGlacialPrison" },
                spellType = SpellType.Line
            });

            #endregion Sejuani

            #region Shen

            Spells.Add(
            new SpellData
            {
                charName = "Shen",
                dangerlevel = 3,
                missileName = "ShenE",
                name = "Shadow Dash",
                projectileSpeed = 1450,
                radius = 50,
                range = 600,
                spellDelay = 0,
                spellKey = SpellSlot.E,
                spellName = "ShenE",
                spellType = SpellType.Line,
            });

            #endregion Shen

            #region Shyvana

            Spells.Add(
            new SpellData
            {
                charName = "Shyvana",
                dangerlevel = 2,
                missileName = "ShyvanaFireballMissile",
                name = "Flame Breath",
                projectileSpeed = 1700,
                radius = 60,
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "ShyvanaFireball",
                spellType = SpellType.Line,
                fixedRange = true
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
                //splits = 5 this "splits" is not even implemented
            });

            Spells.Add(
            new SpellData
            {
                charName = "Shyvana",
                dangerlevel = 3,
                missileName = "ShyvanaTransformCast",
                name = "Dragon's Descent",
                projectileSpeed = 1250,
                radius = 160,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "ShyvanaTransformCast",
                spellType = SpellType.Line,

            });

            #endregion Shyvana

            #region Sion

            //TODO: Sion Q, special code?
                
            Spells.Add(
            new SpellData
            {
                charName = "Sion",
                dangerlevel = 3,
                missileName = "SionEMissile",
                name = "Roar of the Slayer",
                projectileSpeed = 1800,
                radius = 80,
                range = 850,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "SionE",
                spellType = SpellType.Line,
                isSpecial = true,
                fixedRange = true
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
                range = 300,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "SionR",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions },
                isSpecial = true
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
                spellType = SpellType.Line,
                fixedRange = true
            });

            #endregion Sivir

            #region Skarner

            Spells.Add(
            new SpellData
            {
                charName = "Skarner",
                dangerlevel = 3,
                missileName = "SkarnerFractureMissile",
                name = "Fracture",
                projectileSpeed = 1450,
                radius = 70,
                range = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "SkarnerFracture",
                spellType = SpellType.Line,
                fixedRange = true

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
                fixedRange = true
            });
            #endregion Sona

            #region Soraka

            Spells.Add(
            new SpellData
            {
                charName = "Soraka",
                dangerlevel = 1,
                name = "Starcall",
                missileName = "SorakaQMissile",
                projectileSpeed = 1100,
                radius = 260,
                range = 970,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "SorakaQ",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Soraka",
                dangerlevel = 3,
                defaultOff = true,
                name = "Equinox",
                radius = 260,
                range = 875,
                spellDelay = 1750,
                spellKey = SpellSlot.E,
                spellName = "SorakaE",
                spellType = SpellType.Circular
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
                radius = 250,
                range = 900,
                spellDelay = 1100,
                spellKey = SpellSlot.W,
                spellName = "SwainShadowGrasp",
                spellType = SpellType.Circular
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
                projectileSpeed = 2000,
                radius = 90,
                range = 850,
                spellDelay = 0f,
                spellKey = SpellSlot.E,
                spellName = "SyndraE",
                extraSpellNames = new []{ "syndrae5" },
                spellType = SpellType.Line,
                isSpecial = true
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
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "syndrawcast",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Syndra",
                dangerlevel = 2,
                missileName = "SyndraQSpell",
                name = "Dark Sphere",
                radius = 210,
                range = 800,
                spellDelay = 600,
                spellKey = SpellSlot.Q,
                spellName = "SyndraQ",
                spellType = SpellType.Circular
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
                radius = 70,
                range = 800,
                spellKey = SpellSlot.Q,
                spellName = "TahmKenchQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
            });

            #endregion TahmKench

            #region Talon

            Spells.Add(
            new SpellData
            {
                charName = "Talon",
                dangerlevel = 4,
                name = "Shadow Assault [Beta]",
                projectileSpeed = 2400,
                radius = 140,
                range = 550,
                spellKey = SpellSlot.R,
                spellName = "talonrmisone",
                extraMissileNames = new []{ "talonrmistwo" },
                spellType = SpellType.Line,
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                angle = 14,
                charName = "Talon",
                dangerlevel = 3,
                isThreeWay = true,
                missileName = "talonwmissile",
                name = "Rake [Beta]",
                projectileSpeed = 2300,
                radius = 75,
                range = 900,
                spellKey = SpellSlot.W,
                spellName = "talonw",
                spellType = SpellType.Line,
                fixedRange = true,
                isSpecial = true
            });

            Spells.Add(
            new SpellData
            {
                angle = 14,
                charName = "Talon",
                dangerlevel = 3,
                isThreeWay = true,
                name = "Rake Return [Beta]",
                projectileSpeed = 3000,
                radius = 75,
                range = 900,
                spellKey = SpellSlot.W,
                spellName = "talonwmissiletwo",
                spellType = SpellType.Line,
                fixedRange = true,
                isSpecial = true
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
                defaultOff = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Taliyah",
                dangerlevel = 3,
                name = "Seismic Shove",
                radius = 165,
                range = 900,
                spellDelay = 450,
                extraEndTime = 1000,
                spellKey = SpellSlot.W,
                spellName = "TaliyahWVC",
                extraSpellNames =  new []{ "TaliyahW" },
                spellType = SpellType.Circular
            });

            #endregion Taliyah

            #region Taric

            Spells.Add(
            new SpellData
            {
                charName = "Taric",
                dangerlevel = 2,
                missileName = "TaricEMissile",
                name = "Dazzle",
                radius = 100,
                range = 750,
                fixedRange = true,
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
                dangerlevel = 2,
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
                fixedRange = true
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
                spellKey = SpellSlot.E,
                spellName = "ThreshE",
                extraSpellNames = new [] { "ThreshEFlay" },
                spellType = SpellType.Line,
                fixedRange = true,
                usePackets = true
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
                projectileSpeed = 1000,
                radius = 270,
                range = 900,
                spellDelay = 250,
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
                dangerlevel = 3,
                missileName = "TwitchSprayandPrayAttack",
                name = "Spray and Pray",
                projectileSpeed = 4000,
                radius = 65,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "TwitchSprayandPrayAttack",
                spellType = SpellType.Line,
                isSpecial = true
            });

            #endregion Twitch

            #region Urgot

            Spells.Add(
            new SpellData
            {
                charName = "Urgot",
                dangerlevel = 2,
                name = "Acid Hunter",
                projectileSpeed = 1600,
                radius = 60,
                range = 1000,
                spellDelay = 150,
                spellKey = SpellSlot.Q,
                spellName = "UrgotHeatseekingLineMissile",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Urgot",
                dangerlevel = 3,
                missileName = "UrgotPlasmaGrenadeBoom",
                name = "Noxian Corrosive Charge",
                projectileSpeed = 1750,
                radius = 250,
                range = 900,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "UrgotPlasmaGrenade",
                spellType = SpellType.Circular
            });

            #endregion Urgot

            #region Varus

            Spells.Add(
            new SpellData
            {
                charName = "Varus",
                dangerlevel = 2,
                name = "Hail of Arrows",
                projectileSpeed = 1500,
                radius = 235,
                range = 925,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "VarusE",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Varus",
                dangerlevel = 2,
                missileName = "varusqmissile",
                name = "Piercing Arrow",
                projectileSpeed = 1900,
                radius = 75,
                range = 1525,
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "varusq",
                spellType = SpellType.Line,
                usePackets = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Varus",
                dangerlevel = 3,
                name = "Chain of Corruption",
                missileName = "VarusRMissile",
                projectileSpeed = 1950,
                radius = 120,
                range = 1250,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "VarusR",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, },
                fixedRange = true
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
                projectileSpeed = 2200,
                radius = 70,
                range = 950,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "VeigarBalefulStrike",
                spellType = SpellType.Line,
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Veigar",
                dangerlevel = 2,
                missileName = "VeigarDarkMatter",
                name = "Dark Matter",
                radius = 225,
                range = 900,
                spellDelay = 1200,
                spellKey = SpellSlot.W,
                spellName = "VeigarDarkMatter",
                spellType = SpellType.Circular
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
                defaultOff = true
            });

            #endregion Veigar

            #region Velkoz

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 3,
                name = "Tectonic Disruption",
                projectileSpeed = 1500,
                radius = 225,
                range = 800,
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "VelkozE",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 2,
                missileName = "VelkozWMissile",
                name = "Void Rift",
                projectileSpeed = 1700,
                radius = 90,
                range = 1150,
                extraEndTime = 1000,
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "VelkozW",
                spellType = SpellType.Line,
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 2,
                name = "Plasma Fission (Split)",
                projectileSpeed = 2100,
                radius = 50,
                range = 1100,
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "VelkozQMissileSplit",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                usePackets = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                dangerlevel = 2,
                missileName = "VelkozQMissile",
                name = "Plasma Fission",
                projectileSpeed = 1300,
                radius = 55,
                range = 1250,
                spellDelay = 250f,
                spellKey = SpellSlot.Q,
                spellName = "VelkozQ",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
            });
            #endregion Velkoz

            #region Vi

            Spells.Add(
            new SpellData
            {
                charName = "Vi",
                dangerlevel = 3,
                name = "Vault Breaker",
                projectileSpeed = 1500,
                radius = 90,
                range = 775,
                spellKey = SpellSlot.Q,
                spellName = "ViQMissile",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, },
                usePackets = true
            });

            #endregion Vi

            #region Viktor

            Spells.Add(
            new SpellData
            {
                charName = "Viktor",
                dangerlevel = 3,
                missileName = "ViktorDeathRayMissile",
                name = "Death Ray",
                projectileSpeed = 1050,
                radius = 75,
                range = 815,
                spellKey = SpellSlot.E,
                spellName = "ViktorDeathRay",
                extraMissileNames = new[] { "ViktorEAugMissile", },
                spellType = SpellType.Line,
                usePackets = true,
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Viktor",
                dangerlevel = 3,
                name = "Death Ray Aftershock",
                spellDelay = 500,
                radius = 75,
                range = 815,
                spellKey = SpellSlot.E,
                spellName = "ViktorDeathRay3",
                spellType = SpellType.Line,
                fixedRange = true,
                usePackets = true
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
                missileName = "VladimirR", // mage update
                name = "Hemoplague",
                radius = 375,
                range = 700,
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "VladimirR", // mage update
                spellType = SpellType.Circular
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
                radius = 280,
                range = 1000,
                spellDelay = 750,
                spellKey = SpellSlot.W,
                spellName = "XerathArcaneBarrage2",
                spellType = SpellType.Circular,
                extraDrawHeight = 45
            });

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                dangerlevel = 2,
                missileName = "XerathArcanopulse2",
                name = "Arcanopulse",
                radius = 70,
                range = 1525,
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "XerathArcanopulse2",
                useEndPosition = true,
                spellType = SpellType.Line
            });

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                dangerlevel = 3,
                name = "Rite of the Arcane",
                missileName = "XerathLocusPulse",
                radius = 200,
                range = 5600,
                spellDelay = 600,
                spellKey = SpellSlot.R,
                spellName = "xerathrmissilewrapper",
                extraSpellNames = new [] { "XerathLocusPulse" },
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                dangerlevel = 3,
                missileName = "XerathMageSpearMissile",
                name = "Shocking Orb",
                projectileSpeed = 1600,
                radius = 60,
                range = 1125,
                spellDelay = 200,
                spellKey = SpellSlot.E,
                spellName = "XerathMageSpear",
                spellType = SpellType.Line,
                collisionObjects = new[] { CollisionObjectType.EnemyChampions, CollisionObjectType.EnemyMinions },
                fixedRange = true
            });

            #endregion Xerath

            #region Yasuo

            Spells.Add(
            new SpellData
            {
                charName = "Yasuo",
                dangerlevel = 3,
                missileName = "YasuoQ3Mis",
                name = "Steel Tempest (Tornado)",
                projectileSpeed = 1250,
                radius = 90,
                range = 1150,
                spellDelay = 300,
                spellKey = SpellSlot.Q,
                spellName = "YasuoQ3W",
                spellType = SpellType.Line,
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Yasuo",
                dangerlevel = 2,
                missileName = "yasuoq",
                extraMissileNames = new[] { "yasuoq2" },
                name = "Steel Tempest",
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
            Spells.Add(
             new SpellData
             {
                 charName = "Yorick",
                 dangerlevel = 3,
                 name = "Dark Procession [Beta]",
                 radius = 250,
                 range = 600,
                 spellDelay = 500,
                 spellKey = SpellSlot.W,
                 extraEndTime = 1000,
                 spellName = "YorickW",
                 spellType = SpellType.Circular
             });


            Spells.Add(
             new SpellData
             {
                 charName = "Yorick",
                 dangerlevel = 3,
                 //missileName = "YorickEMissile",
                 name = "Mourning Mist [Beta]",
                 projectileSpeed = 750,
                 radius = 125,
                 range = 580,
                 spellDelay = 250,
                 spellKey = SpellSlot.E,
                 spellName = "YorickE",
                 spellType = SpellType.Line,
                 updatePosition = false,
                 isSpecial = true
             });

            #endregion Yorick

            #region Zac

            Spells.Add(
            new SpellData
            {
                charName = "Zac",
                dangerlevel = 3,
                missileName = "ZacQ",
                name = "Stretching Strike",
                radius = 120,
                range = 550,
                spellDelay = 400,
                spellKey = SpellSlot.Q,
                spellName = "ZacQ",
                spellType = SpellType.Line,
                fixedRange = true
            });
            
            Spells.Add(
            new SpellData
            {
                charName = "Zac",
                dangerlevel = 3,
                name = "Elastic Slingshot [Beta]",
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
                dangerlevel = 3,
                missileName = "ZedQMissile",
                name = "Razor Shuriken",
                projectileSpeed = 1700,
                radius = 50,
                range = 925,
                spellDelay = 250,
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
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                dangerlevel = 2,
                missileName = "ZiggsW",
                name = "Satchel Charge",
                projectileSpeed = 2000,
                radius = 275,
                range = 1000,
                spellDelay = 250,
                extraEndTime = 1000,
                spellKey = SpellSlot.W,
                spellName = "ZiggsW",
                spellType = SpellType.Circular
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                dangerlevel = 2,
                name = "Bouncing Bomb",
                projectileSpeed = 1700,
                radius = 150,
                range = 850,
                spellDelay = 125,
                spellKey = SpellSlot.Q,
                spellName = "ZiggsQ",
                spellType = SpellType.Circular,
                isSpecial = true,
                noProcess = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                dangerlevel = 4,
                missileName = "ZiggsR",
                name = "Mega Inferno Bomb",
                projectileSpeed = 1550,
                radius = 500,
                range = 5300,
                spellDelay = 400,
                spellKey = SpellSlot.R,
                spellName = "ZiggsR",
                spellType = SpellType.Circular,
                defaultOff = true,
                isSpecial = true
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
                radius = 150,
                range = 900,
                extraEndTime = 1000,
                spellDelay = 650,
                spellKey = SpellSlot.Q,
                spellName = "ZileanQ",
                spellType = SpellType.Circular,
                isSpecial = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Zilean",
                dangerlevel = 3,
                spellName = "ZileanQ",
                name = "Time Bomb",
                radius = 160,
                range = 900,
                spellKey = SpellSlot.Q,
                spellType = SpellType.Circular,
                trapTroyName = "zilean_base_q_timebombground" + Situation.EmitterColor() + ".troy",
                extraDrawHeight = -100,
                hasTrap = true
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
                projectileSpeed = 1400, // 1150
                radius = 70,
                range = 1150, 
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "ZyraE",
                spellType = SpellType.Line,
                fixedRange = true
            });

            Spells.Add(
            new SpellData
            {
                charName = "Zyra",
                dangerlevel = 2,
                missileName = "ZyraQ",
                name = "Deadly Bloom",
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
                name = "Stranglethorns",
                radius = 525,
                range = 700,
                extraEndTime = 2000,
                spellDelay = 500,
                spellKey = SpellSlot.R,
                spellName = "ZyraR",
                extraSpellNames = new[] { "ZyraBrambleZone" },
                spellType = SpellType.Circular,
                defaultOff = true
            });

            #endregion Zyra
        }
    }
}
