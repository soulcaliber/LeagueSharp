using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace ezEvade
{
    class SpellWindupDatabase
    {
        public static List<SpellData> Spells = new List<SpellData>();

        static SpellWindupDatabase()
        {
            #region Aatrox

            Spells.Add(
            new SpellData
            {
                charName = "Aatrox",
                name = "Blade of Torment",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "AatroxE",

            });

            #endregion Aatrox

            #region Ahri

            Spells.Add(
            new SpellData
            {
                charName = "Ahri",
                name = "Orb of Deception",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "AhriOrbofDeception",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ahri",
                name = "Charm",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "AhriSeduce",

            });
            #endregion Ahri

            #region Akali

            Spells.Add(
            new SpellData
            {
                charName = "Akali",
                name = "AkaliMota",
                spellKey = SpellSlot.Q,
                spellName = "AkaliMota",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Akali",
                name = "AkaliSmokeBomb",
                spellKey = SpellSlot.W,
                spellName = "AkaliSmokeBomb",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Akali",
                name = "AkaliShadowSwipe",
                spellKey = SpellSlot.E,
                spellName = "AkaliShadowSwipe",

            });
            #endregion Akali

            #region Alistar

            Spells.Add(
            new SpellData
            {
                charName = "Alistar",
                name = "Pulverize",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "Pulverize",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Alistar",
                name = "TriumphantRoar",
                spellKey = SpellSlot.E,
                spellName = "TriumphantRoar",

            });

            #endregion Alistar

            #region Azir
            Spells.Add(
            new SpellData
            {
                charName = "Azir",
                name = "AzirQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "AzirQ",
            });

            Spells.Add(
            new SpellData
            {
                charName = "Azir",
                name = "AzirW",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "AzirW",
            });
            #endregion Azir

            #region Amumu

            Spells.Add(
            new SpellData
            {
                charName = "Amumu",
                name = "Tantrum",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "Tantrum",

            });
            #endregion Amumu

            #region Anivia

            Spells.Add(
            new SpellData
            {
                charName = "Anivia",
                name = "Flash Frost",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "FlashFrostSpell",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Anivia",
                name = "Frostbite",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "Frostbite",

            });
            #endregion Anivia

            #region Annie

            Spells.Add(
            new SpellData
            {
                charName = "Annie",
                name = "InfernalGuardian",
                spellKey = SpellSlot.R,
                spellName = "InfernalGuardian",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Annie",
                name = "Disintegrate",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "Disintegrate",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Annie",
                name = "Incinerate",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "Incinerate",

            });
            #endregion Annie

            #region Ashe

            Spells.Add(
            new SpellData
            {
                charName = "Ashe",
                name = "Volley",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "Volley",

            });

            #endregion Ashe

            #region Bard

            Spells.Add(
            new SpellData
            {
                charName = "Bard",
                name = "BardQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BardQ",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Bard",
                name = "BardW",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "BardW",

            });
            #endregion Bard

            #region Blitzcrank

            Spells.Add(
            new SpellData
            {
                charName = "Blitzcrank",
                name = "Rocket Grab",
                spellDelay = 685,
                spellKey = SpellSlot.Q,
                spellName = "RocketGrabMissile",

            });
            #endregion Blitzcrank

            #region Brand

            Spells.Add(
            new SpellData
            {
                charName = "Brand",
                name = "BrandConflagration",
                spellKey = SpellSlot.E,
                spellName = "BrandConflagration",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Brand",
                name = "BrandBlaze",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BrandBlaze",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Brand",
                name = "Pillar of Flame",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "BrandFissure",

            });
            #endregion Brand

            #region Braum

            Spells.Add(
            new SpellData
            {
                charName = "Braum",
                name = "BraumQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BraumQ",

            });
            #endregion Braum

            #region Caitlyn

            Spells.Add(
            new SpellData
            {
                charName = "Caitlyn",
                name = "Piltover Peacemaker",
                spellDelay = 625,
                spellKey = SpellSlot.Q,
                spellName = "CaitlynPiltoverPeacemaker",

            });

            #endregion Caitlyn

            #region Cassiopeia

            Spells.Add(
            new SpellData
            {
                charName = "Cassiopeia",
                name = "Twin Fang",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "CassiopeiaTwinFang",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Cassiopeia",
                name = "CassiopeiaMiasma",
                spellKey = SpellSlot.W,
                spellName = "CassiopeiaMiasma",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Cassiopeia",
                name = "Noxious Blast",
                spellKey = SpellSlot.Q,
                spellName = "CassiopeiaNoxiousBlast",

            });

            #endregion Cassiopeia

            #region Chogath

            Spells.Add(
            new SpellData
            {
                charName = "Chogath",
                name = "FeralScream",
                spellDelay = 500,
                spellKey = SpellSlot.W,
                spellName = "FeralScream",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Chogath",
                name = "Rupture",
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "Rupture",

            });
            #endregion Chogath

            #region Corki

            Spells.Add(
            new SpellData
            {
                charName = "Corki",
                name = "Missile Barrage",
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "MissileBarrage",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Corki",
                name = "Phosphorus Bomb",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "PhosphorusBomb",

            });
            #endregion Corki

            #region Darius

            Spells.Add(
            new SpellData
            {
                charName = "Darius",
                name = "Decimate",
                spellDelay = 230,
                spellKey = SpellSlot.Q,
                spellName = "DariusCleave",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Darius",
                name = "Apprehend",
                spellDelay = 320,
                spellKey = SpellSlot.E,
                spellName = "DariusAxeGrabCone",

            });
            #endregion Darius

            #region Diana

            Spells.Add(
            new SpellData
            {
                charName = "Diana",
                name = "DianaVortex",
                spellKey = SpellSlot.E,
                spellName = "DianaVortex",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Diana",
                name = "DianaArc",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "DianaArc",

            });
            #endregion Diana

            #region DrMundo

            Spells.Add(
            new SpellData
            {
                charName = "DrMundo",
                name = "Infected Cleaver",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "InfectedCleaverMissile",

            });
            #endregion DrMundo

            #region Draven

            Spells.Add(
            new SpellData
            {
                charName = "Draven",
                name = "Stand Aside",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "DravenDoubleShot",

            });
            #endregion Draven

            #region Ekko
            Spells.Add(
            new SpellData
            {
                charName = "Ekko",
                name = "EkkoQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "EkkoQ",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ekko",
                name = "EkkoW",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "EkkoW",

            });
            #endregion Ekko

            #region Elise

            Spells.Add(
            new SpellData
            {
                charName = "Elise",
                name = "Volatile Spiderling",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "EliseHumanW",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Elise",
                name = "Venomous Bite",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "EliseSpiderQCast",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Elise",
                name = "Cocoon",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "EliseHumanE",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Elise",
                name = "Neurotoxin",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "EliseHumanQ",

            });
            #endregion Elise

            #region Evelynn

            Spells.Add(
            new SpellData
            {
                charName = "Evelynn",
                name = "Ravage",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "EvelynnQ",

            });

            #endregion Evelynn

            #region Ezreal

            Spells.Add(
            new SpellData
            {
                charName = "Ezreal",
                name = "Essence Flux",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "EzrealEssenceFlux",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ezreal",
                name = "Mystic Shot",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "EzrealMysticShot",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ezreal",
                name = "Trueshot Barrage",
                spellDelay = 1000,
                spellKey = SpellSlot.R,
                spellName = "EzrealTrueshotBarrage",

            });
            #endregion Ezreal

            #region FiddleSticks

            Spells.Add(
            new SpellData
            {
                charName = "FiddleSticks",
                name = "Crowstorm",
                spellKey = SpellSlot.R,
                spellName = "Crowstorm",

            });

            Spells.Add(
            new SpellData
            {
                charName = "FiddleSticks",
                name = "Terrify",
                spellKey = SpellSlot.Q,
                spellName = "Terrify",

            });

            Spells.Add(
            new SpellData
            {
                charName = "FiddleSticks",
                name = "Dark Wind",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "FiddlesticksDarkWind",

            });
            #endregion FiddleSticks

            #region Galio

            Spells.Add(
            new SpellData
            {
                charName = "Galio",
                name = "GalioResoluteSmite",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "GalioResoluteSmite",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Galio",
                name = "GalioRighteousGust",
                spellKey = SpellSlot.E,
                spellName = "GalioRighteousGust",

            });

            #endregion Galio

            #region Gangplank

            Spells.Add(
            new SpellData
            {
                charName = "Gangplank",
                name = "CannonBarrage",
                spellKey = SpellSlot.R,
                spellName = "CannonBarrage",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Gangplank",
                name = "Parley",
                spellKey = SpellSlot.Q,
                spellName = "Parley",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Gangplank",
                name = "RaiseMorale",
                spellKey = SpellSlot.E,
                spellName = "RaiseMorale",

            });
            #endregion Gangplank

            #region Gnar
            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                name = "GnarQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "GnarQ",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Gnar",
                name = "GnarW",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "gnarbigw",

            });
            #endregion Gnar

            #region Gragas

            Spells.Add(
            new SpellData
            {
                charName = "Gragas",
                name = "Barrel Roll",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "GragasBarrelRoll",

            });
            #endregion Gragas

            #region Graves

            Spells.Add(
            new SpellData
            {
                charName = "Graves",
                name = "Smoke Screen",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "GravesSmokeGrenade",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Graves",
                name = "Buckshot",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "GravesClusterShot",

            });
            #endregion Graves

            #region Hecarim
            #endregion Hecarim

            #region Heimerdinger

            Spells.Add(
            new SpellData
            {
                charName = "Heimerdinger",
                name = "HeimerdingerE",
                spellKey = SpellSlot.E,
                spellName = "HeimerdingerE",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Heimerdinger",
                name = "HeimerdingerW",
                spellKey = SpellSlot.W,
                spellName = "HeimerdingerW",

            });
            #endregion Heimerdinger

            #region Janna

            Spells.Add(
            new SpellData
            {
                charName = "Janna",
                name = "SowTheWind",
                spellKey = SpellSlot.W,
                spellName = "SowTheWind",

            });
            #endregion Janna

            #region Jayce

            Spells.Add(
            new SpellData
            {
                charName = "Jayce",
                name = "JayceShockBlast",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "jayceshockblast",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Jayce",
                name = "JayceQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "JayceToTheSkies",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Jayce",
                name = "Thundering Blow",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "JayceThunderingBlow",

            });
            #endregion Jayce

            #region Jinx

            Spells.Add(
            new SpellData
            {
                charName = "Jinx",
                name = "Zap",
                spellDelay = 600,
                spellKey = SpellSlot.W,
                spellName = "JinxWMissile",

            });
            #endregion Jinx

            #region Kalista

            Spells.Add(
            new SpellData
            {
                charName = "Kalista",
                name = "KalistaMysticShot",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KalistaMysticShot",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Kalista",
                name = "KalistaW",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "KalistaW",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Kalista",
                name = "KalistaE",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "KalistaE",

            });
            #endregion Kalista

            #region Karma

            Spells.Add(
            new SpellData
            {
                charName = "Karma",
                name = "KarmaQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KarmaQ",

            });
            #endregion Karma

            #region Karthus

            Spells.Add(
            new SpellData
            {
                charName = "Karthus",
                name = "Lay Waste",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "karthuslaywastea3",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Karthus",
                name = "WallOfPain",
                spellKey = SpellSlot.W,
                spellName = "WallOfPain",

            });
            #endregion Karthus

            #region Kassadin

            Spells.Add(
            new SpellData
            {
                charName = "Kassadin",
                name = "Force Pulse",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "ForcePulse",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Kassadin",
                name = "Null Sphere",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "NullLance",

            });
            #endregion Kassadin

            #region Katarina

            Spells.Add(
            new SpellData
            {
                charName = "Katarina",
                name = "KatarinaQ",
                spellKey = SpellSlot.Q,
                spellName = "KatarinaQ",

            });
            #endregion Katarina

            #region Kayle

            Spells.Add(
            new SpellData
            {
                charName = "Kayle",
                name = "Reckoning",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "JudicatorReckoning",

            });
            #endregion Kayle

            #region Kennen

            Spells.Add(
            new SpellData
            {
                charName = "Kennen",
                name = "Thundering Shuriken",
                spellDelay = 180,
                spellKey = SpellSlot.Q,
                spellName = "KennenShurikenHurlMissile1",

            });
            #endregion Kennen

            #region Khazix

            Spells.Add(
            new SpellData
            {
                charName = "Khazix",
                name = "KhazixQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KhazixQ",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Khazix",
                name = "KhazixW",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "KhazixW",

            });
            #endregion Khazix

            #region KogMaw

            Spells.Add(
            new SpellData
            {
                charName = "KogMaw",
                name = "Living Artillery",
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "KogMawLivingArtillery",

            });

            Spells.Add(
            new SpellData
            {
                charName = "KogMaw",
                name = "Caustic Spittle",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "KogMawCausticSpittle",

            });

            Spells.Add(
            new SpellData
            {
                charName = "KogMaw",
                name = "Void Ooze",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "KogMawVoidOozeMissile",

            });
            #endregion KogMaw

            #region Leblanc

            Spells.Add(
            new SpellData
            {
                charName = "Leblanc",
                name = "Sigil of Silence",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LeblancChaosOrb",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Leblanc",
                name = "Ethereal Chains",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "LeblancSoulShackle",

            });
            #endregion Leblanc

            #region LeeSin

            Spells.Add(
            new SpellData
            {
                charName = "LeeSin",
                name = "Sonic Wave",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BlindMonkQOne",

            });

            Spells.Add(
            new SpellData
            {
                charName = "LeeSin",
                name = "BlindMonkEOne",
                spellKey = SpellSlot.E,
                spellName = "BlindMonkEOne",

            });
            #endregion LeeSin

            #region Lissandra

            Spells.Add(
            new SpellData
            {
                charName = "Lissandra",
                name = "LissandraQ",
                spellKey = SpellSlot.Q,
                spellName = "LissandraQ",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lissandra",
                name = "LissandraW",
                spellKey = SpellSlot.W,
                spellName = "LissandraW",

            });

            #endregion Lissandra

            #region Lucian

            Spells.Add(
            new SpellData
            {
                charName = "Lucian",
                name = "LucianQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LucianQ",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lucian",
                name = "LucianW",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "LucianW",

            });
            #endregion Lucian

            #region Lulu

            Spells.Add(
            new SpellData
            {
                charName = "Lulu",
                name = "LuluQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LuluQ",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lulu",
                name = "LuluE",
                spellKey = SpellSlot.E,
                spellName = "LuluE",

            });
            #endregion Lulu

            #region Lux

            Spells.Add(
            new SpellData
            {
                charName = "Lux",
                name = "Light Binding",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "LuxLightBinding",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lux",
                name = "LuxLightStrikeKugel",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "LuxLightStrikeKugel",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Lux",
                name = "LuxPrismaticWave",
                spellKey = SpellSlot.W,
                spellName = "LuxPrismaticWave",

            });
            #endregion Lux

            #region Malphite

            Spells.Add(
            new SpellData
            {
                charName = "Malphite",
                name = "Seismic Shard",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "SeismicShard",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Malphite",
                name = "Ground Slam",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "Landslide",

            });
            #endregion Malphite

            #region Malzahar

            Spells.Add(
            new SpellData
            {
                charName = "Malzahar",
                name = "AlZaharCalloftheVoid",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "AlZaharCalloftheVoid",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Malzahar",
                name = "Null Zone",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "AlZaharNullZone",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Malzahar",
                name = "Malefic Visions",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "AlZaharMaleficVisions",

            });
            #endregion Malzahar

            #region Maokai

            Spells.Add(
            new SpellData
            {
                charName = "Maokai",
                name = "MaokaiTrunkLine",
                spellKey = SpellSlot.Q,
                spellName = "MaokaiTrunkLine",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Maokai",
                name = "MaokaiSapling2",
                spellKey = SpellSlot.E,
                spellName = "MaokaiSapling2",

            });
            #endregion Maokai

            #region MissFortune

            Spells.Add(
            new SpellData
            {
                charName = "MissFortune",
                name = "MissFortuneRicochetShot",
                spellKey = SpellSlot.Q,
                spellName = "MissFortuneRicochetShot",

            });

            Spells.Add(
            new SpellData
            {
                charName = "MissFortune",
                name = "MissFortuneScattershot",
                spellKey = SpellSlot.E,
                spellName = "MissFortuneScattershot",

            });
            #endregion MissFortune

            #region Mordekaiser

            Spells.Add(
            new SpellData
            {
                charName = "Mordekaiser",
                name = "MordekaiserSyphonOfDestruction",
                spellKey = SpellSlot.E,
                spellName = "MordekaiserSyphonOfDestruction",

            });
            #endregion Mordekaiser

            #region Morgana

            Spells.Add(
            new SpellData
            {
                charName = "Morgana",
                name = "TormentedSoil",
                spellKey = SpellSlot.W,
                spellName = "TormentedSoil",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Morgana",
                name = "Dark Binding",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "DarkBindingMissile",

            });

            #endregion Morgana

            #region Nami

            Spells.Add(
            new SpellData
            {
                charName = "Nami",
                name = "NamiQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "NamiQ",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Nami",
                name = "TidecallersBlessing",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "NamiE",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Nami",
                name = "Ebb and Flow",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "NamiW",

            });
            #endregion Nami

            #region Nasus

            Spells.Add(
            new SpellData
            {
                charName = "Nasus",
                name = "Wither",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "NasusW",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Nasus",
                name = "NasusE",
                spellKey = SpellSlot.E,
                spellName = "NasusE",

            });
            #endregion Nasus

            #region Nautilus

            Spells.Add(
            new SpellData
            {
                charName = "Nautilus",
                name = "NautilusSplashZone",
                spellKey = SpellSlot.E,
                spellName = "NautilusSplashZone",

            });

            #endregion Nautilus

            #region Nidalee

            Spells.Add(
            new SpellData
            {
                charName = "Nidalee",
                name = "PrimalSurge",
                spellKey = SpellSlot.E,
                spellName = "PrimalSurge",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Nidalee",
                name = "Javelin Toss",
                spellDelay = 125,
                spellKey = SpellSlot.Q,
                spellName = "JavelinToss",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Nidalee",
                name = "Swipe",
                spellKey = SpellSlot.E,
                spellName = "Swipe",

            });
            #endregion Nidalee

            #region Nocturne

            Spells.Add(
            new SpellData
            {
                charName = "Nocturne",
                name = "Unspeakable Horror",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "NocturneUnspeakableHorror",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Nocturne",
                name = "NocturneDuskbringer",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "NocturneDuskbringer",

            });
            #endregion Nocturne

            #region Nunu

            Spells.Add(
            new SpellData
            {
                charName = "Nunu",
                name = "IceBlast",
                spellDelay = 400,
                spellKey = SpellSlot.E,
                spellName = "IceBlast",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Nunu",
                name = "Consume",
                spellDelay = 400,
                spellKey = SpellSlot.Q,
                spellName = "Consume",

            });
            #endregion Nunu

            #region Olaf

            Spells.Add(
            new SpellData
            {
                charName = "Olaf",
                name = "Reckless Swing",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "OlafRecklessStrike",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Olaf",
                name = "Undertow",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "OlafAxeThrow",

            });
            #endregion Olaf

            #region Pantheon

            Spells.Add(
            new SpellData
            {
                charName = "Pantheon",
                name = "Pantheon_Throw",
                spellKey = SpellSlot.Q,
                spellName = "Pantheon_Throw",

            });

            #endregion Pantheon

            #region Quinn

            Spells.Add(
            new SpellData
            {
                charName = "Quinn",
                name = "QuinnQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "QuinnQ",

            });
            #endregion Quinn

            #region Renekton

            Spells.Add(
            new SpellData
            {
                charName = "Renekton",
                name = "RenektonCleave",
                spellKey = SpellSlot.Q,
                spellName = "RenektonCleave",

            });

            #endregion Renekton

            #region Rengar

            Spells.Add(
            new SpellData
            {
                charName = "Rengar",
                name = "Bola Strike",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "RengarE",

            });
            #endregion Rengar

            #region Riven

            Spells.Add(
            new SpellData
            {
                charName = "Riven",
                name = "Ki Burst",
                spellDelay = 267,
                spellKey = SpellSlot.W,
                spellName = "RivenMartyr",

            });
            #endregion Riven

            #region Rumble

            Spells.Add(
            new SpellData
            {
                charName = "Rumble",
                name = "RumbleGrenade",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "RumbleGrenade",

            });
            #endregion Rumble

            #region Ryze

            Spells.Add(
            new SpellData
            {
                charName = "Ryze",
                name = "Overload",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "Overload",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ryze",
                name = "Rune Prison",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "RunePrison",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ryze",
                name = "Spell Flux",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "SpellFlux",

            });
            #endregion Ryze

            #region Shaco

            Spells.Add(
            new SpellData
            {
                charName = "Shaco",
                name = "JackInTheBox",
                spellKey = SpellSlot.W,
                spellName = "JackInTheBox",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Shaco",
                name = "TwoShivPoison",
                spellKey = SpellSlot.E,
                spellName = "TwoShivPoison",

            });
            #endregion Shaco

            #region Shen

            Spells.Add(
            new SpellData
            {
                charName = "Shen",
                name = "ShenVorpalStar",
                spellKey = SpellSlot.Q,
                spellName = "ShenVorpalStar",

            });
            #endregion Shen

            #region Shyvana

            Spells.Add(
            new SpellData
            {
                charName = "Shyvana",
                name = "ShyvanaFireball",
                spellKey = SpellSlot.E,
                spellName = "ShyvanaFireball",

            });
            #endregion Shyvana

            #region Sion

            Spells.Add(
            new SpellData
            {
                charName = "Sion",
                name = "CrypticGaze",
                spellKey = SpellSlot.Q,
                spellName = "CrypticGaze",

            });
            #endregion Sion

            #region Sivir

            Spells.Add(
            new SpellData
            {
                charName = "Sivir",
                name = "Boomerang Blade",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "SivirQ",

            });
            #endregion Sivir

            #region Skarner

            Spells.Add(
            new SpellData
            {
                charName = "Skarner",
                name = "Fracture",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "SkarnerFracture",

            });
            #endregion Skarner

            #region Soraka

            Spells.Add(
            new SpellData
            {
                charName = "Soraka",
                name = "Infuse",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "Infuse",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Soraka",
                name = "Starcall",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "Starcall",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Soraka",
                name = "AstralBlessing",
                spellKey = SpellSlot.W,
                spellName = "AstralBlessing",

            });
            #endregion Soraka

            #region Swain

            Spells.Add(
            new SpellData
            {
                charName = "Swain",
                name = "Nevermove",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "SwainShadowGrasp",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Swain",
                name = "Torment",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "SwainTorment",

            });
            #endregion Swain

            #region Syndra

            Spells.Add(
            new SpellData
            {
                charName = "Syndra",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "SyndraE",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Syndra",
                name = "SyndraQ",
                spellDelay = 200,
                spellKey = SpellSlot.Q,
                spellName = "SyndraQ",

            });
            #endregion Syndra

            #region Talon

            Spells.Add(
            new SpellData
            {
                charName = "Talon",
                name = "Rake",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "TalonRake",

            });

            #endregion Talon

            #region Taric

            Spells.Add(
            new SpellData
            {
                charName = "Taric",
                name = "Shatter",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "Shatter",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Taric",
                name = "Radiance",
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "TaricHammerSmash",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Taric",
                name = "Imbue",
                spellKey = SpellSlot.Q,
                spellName = "Imbue",

            });
            #endregion Taric

            #region Teemo

            Spells.Add(
            new SpellData
            {
                charName = "Teemo",
                spellDelay = 250,
                spellKey = SpellSlot.R,
                spellName = "BantamTrap",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Teemo",
                name = "BlindingDart",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "BlindingDart",

            });
            #endregion Teemo

            #region Thresh

            Spells.Add(
            new SpellData
            {
                charName = "Thresh",
                name = "ThreshQ",
                spellDelay = 500,
                spellKey = SpellSlot.Q,
                spellName = "ThreshQ",

            });
            #endregion Thresh

            #region Tristana

            Spells.Add(
            new SpellData
            {
                charName = "Tristana",
                name = "Explosive Shot",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "DetonatingShot",

            });
            #endregion Tristana

            #region Trundle

            Spells.Add(
            new SpellData
            {
                charName = "Trundle",
                name = "TrundleCircle",
                spellKey = SpellSlot.E,
                spellName = "TrundleCircle",

            });
            #endregion Trundle

            #region Tryndamere

            Spells.Add(
            new SpellData
            {
                charName = "Tryndamere",
                name = "MockingShout",
                spellKey = SpellSlot.W,
                spellName = "MockingShout",

            });
            #endregion Tryndamere

            #region TwistedFate

            Spells.Add(
            new SpellData
            {
                charName = "TwistedFate",
                name = "Loaded Dice",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "WildCards",

            });
            #endregion TwistedFate

            #region Twitch

            Spells.Add(
            new SpellData
            {
                charName = "Twitch",
                name = "Expunge",
                spellKey = SpellSlot.E,
                spellName = "Expunge",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Twitch",
                name = "TwitchVenomCask",
                spellKey = SpellSlot.W,
                spellName = "TwitchVenomCask",

            });
            #endregion Twitch

            #region Urgot

            Spells.Add(
            new SpellData
            {
                charName = "Urgot",
                name = "UrgotPlasmaGrenade",
                spellKey = SpellSlot.E,
                spellName = "UrgotPlasmaGrenade",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Urgot",
                name = "UrgotHeatseekingMissile",
                spellKey = SpellSlot.Q,
                spellName = "UrgotHeatseekingMissile",

            });

            #endregion Urgot

            #region Varus

            Spells.Add(
            new SpellData
            {
                charName = "Varus",
                name = "Varus E",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "VarusE",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Varus",
                name = "Varus Q Missile",
                spellDelay = 0,
                spellKey = SpellSlot.Q,
                spellName = "VarusQ",

            });
            #endregion Varus

            #region Vayne

            Spells.Add(
            new SpellData
            {
                charName = "Vayne",
                name = "VayneCondemn",
                spellKey = SpellSlot.E,
                spellName = "VayneCondemn",

            });
            #endregion Vayne

            #region Veigar

            Spells.Add(
            new SpellData
            {
                charName = "Veigar",
                name = "VeigarDarkMatter",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "VeigarDarkMatter",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Veigar",
                name = "Baleful Strike",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "VeigarBalefulStrike",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Veigar",
                name = "VeigarEventHorizon",
                spellKey = SpellSlot.E,
                spellName = "VeigarEventHorizon",

            });
            #endregion Veigar

            #region Velkoz

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                name = "VelkozW",
                spellKey = SpellSlot.W,
                spellName = "VelkozW",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                name = "VelkozQ",
                spellKey = SpellSlot.Q,
                spellName = "VelkozQ",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Velkoz",
                name = "VelkozE",
                spellKey = SpellSlot.E,
                spellName = "VelkozE",

            });
            #endregion Velkoz

            #region Vi
            #endregion Vi

            #region Viktor

            Spells.Add(
            new SpellData
            {
                charName = "Viktor",
                name = "ViktorPowerTransfer",
                spellKey = SpellSlot.Q,
                spellName = "ViktorPowerTransfer",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Viktor",
                name = "ViktorGravitonField",
                spellKey = SpellSlot.W,
                spellName = "ViktorGravitonField",

            });

            #endregion Viktor

            #region Vladimir

            Spells.Add(
            new SpellData
            {
                charName = "Vladimir",
                name = "VladimirTidesofBlood",
                spellKey = SpellSlot.E,
                spellName = "VladimirTidesofBlood",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Vladimir",
                name = "VladimirHemoplague",
                spellDelay = 389,
                spellKey = SpellSlot.R,
                spellName = "VladimirHemoplague",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Vladimir",
                name = "VladimirTransfusion",
                spellKey = SpellSlot.Q,
                spellName = "VladimirTransfusion",

            });
            #endregion Vladimir

            #region Warwick

            Spells.Add(
            new SpellData
            {
                charName = "Warwick",
                name = "Hungering Strike",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "HungeringStrike",

            });
            #endregion Warwick

            #region Xerath

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                name = "XerathMageSpear",
                spellKey = SpellSlot.E,
                spellName = "XerathMageSpear",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Xerath",
                name = "XerathArcaneBarrage2",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "XerathArcaneBarrage2",

            });
            #endregion Xerath

            #region Yasuo

            Spells.Add(
            new SpellData
            {
                charName = "Yasuo",
                name = "Steel Tempest3",
                spellDelay = 175,
                spellKey = SpellSlot.Q,
                spellName = "YasuoQW",

            });

            #endregion Yasuo

            #region Yorick

            Spells.Add(
            new SpellData
            {
                charName = "Yorick",
                name = "YorickDecayed",
                spellKey = SpellSlot.W,
                spellName = "YorickDecayed",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Yorick",
                name = "YorickRavenous",
                spellKey = SpellSlot.E,
                spellName = "YorickRavenous",

            });
            #endregion Yorick

            #region Zac

            Spells.Add(
            new SpellData
            {
                charName = "Zac",
                name = "ZacQ",
                spellKey = SpellSlot.Q,
                spellName = "ZacQ",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Zac",
                name = "ZacW",
                spellKey = SpellSlot.W,
                spellName = "ZacW",

            });
            #endregion Zac

            #region Zed

            Spells.Add(
            new SpellData
            {
                charName = "Zed",
                name = "ZedShuriken",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "ZedShuriken",

            });
            #endregion Zed

            #region Ziggs

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                name = "ZiggsW",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "ZiggsW",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                name = "ZiggsE",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "ZiggsE",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Ziggs",
                name = "ZiggsQ",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "ZiggsQ",

            });

            #endregion Ziggs

            #region Zilean

            Spells.Add(
            new SpellData
            {
                charName = "Zilean",
                name = "Time Bomb",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "TimeBomb",

            });

            #endregion Zilean

            #region Zyra

            Spells.Add(
            new SpellData
            {
                charName = "Zyra",
                name = "Grasping Roots",
                spellDelay = 250,
                spellKey = SpellSlot.E,
                spellName = "ZyraGraspingRoots",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Zyra",
                name = "Rampant Growth",
                spellDelay = 250,
                spellKey = SpellSlot.W,
                spellName = "ZyraSeed",

            });

            Spells.Add(
            new SpellData
            {
                charName = "Zyra",
                name = "Deadly Bloom",
                spellDelay = 250,
                spellKey = SpellSlot.Q,
                spellName = "ZyraQFissure",

            });
            #endregion Zyra
        }
    }
}
