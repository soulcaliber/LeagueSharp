using System.Collections.Generic;
using LeagueSharp;

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
                CharName = "Aatrox",
                Name = "Blade of Torment",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "AatroxE",

            });

            #endregion Aatrox

            #region Ahri

            Spells.Add(
            new SpellData
            {
                CharName = "Ahri",
                Name = "Orb of Deception",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "AhriOrbofDeception",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ahri",
                Name = "Charm",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "AhriSeduce",

            });
            #endregion Ahri

            #region Akali

            Spells.Add(
            new SpellData
            {
                CharName = "Akali",
                Name = "AkaliMota",
                SpellKey = SpellSlot.Q,
                SpellName = "AkaliMota",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Akali",
                Name = "AkaliSmokeBomb",
                SpellKey = SpellSlot.W,
                SpellName = "AkaliSmokeBomb",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Akali",
                Name = "AkaliShadowSwipe",
                SpellKey = SpellSlot.E,
                SpellName = "AkaliShadowSwipe",

            });
            #endregion Akali

            #region Alistar

            Spells.Add(
            new SpellData
            {
                CharName = "Alistar",
                Name = "Pulverize",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "Pulverize",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Alistar",
                Name = "TriumphantRoar",
                SpellKey = SpellSlot.E,
                SpellName = "TriumphantRoar",

            });

            #endregion Alistar

            #region Amumu

            Spells.Add(
            new SpellData
            {
                CharName = "Amumu",
                Name = "Tantrum",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "Tantrum",

            });
            #endregion Amumu

            #region Anivia

            Spells.Add(
            new SpellData
            {
                CharName = "Anivia",
                Name = "Flash Frost",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "FlashFrostSpell",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Anivia",
                Name = "Frostbite",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "Frostbite",

            });
            #endregion Anivia

            #region Annie

            Spells.Add(
            new SpellData
            {
                CharName = "Annie",
                Name = "InfernalGuardian",
                SpellKey = SpellSlot.R,
                SpellName = "InfernalGuardian",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Annie",
                Name = "Disintegrate",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "Disintegrate",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Annie",
                Name = "Incinerate",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "Incinerate",

            });
            #endregion Annie

            #region Ashe

            Spells.Add(
            new SpellData
            {
                CharName = "Ashe",
                Name = "Volley",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "Volley",

            });

            #endregion Ashe

            #region Blitzcrank

            Spells.Add(
            new SpellData
            {
                CharName = "Blitzcrank",
                Name = "Rocket Grab",
                SpellDelay = 685,
                SpellKey = SpellSlot.Q,
                SpellName = "RocketGrabMissile",

            });
            #endregion Blitzcrank

            #region Brand

            Spells.Add(
            new SpellData
            {
                CharName = "Brand",
                Name = "BrandConflagration",
                SpellKey = SpellSlot.E,
                SpellName = "BrandConflagration",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Brand",
                Name = "BrandBlaze",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "BrandBlaze",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Brand",
                Name = "Pillar of Flame",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "BrandFissure",

            });
            #endregion Brand

            #region Braum

            Spells.Add(
            new SpellData
            {
                CharName = "Braum",
                Name = "BraumQ",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "BraumQ",

            });
            #endregion Braum

            #region Caitlyn

            Spells.Add(
            new SpellData
            {
                CharName = "Caitlyn",
                Name = "Piltover Peacemaker",
                SpellDelay = 625,
                SpellKey = SpellSlot.Q,
                SpellName = "CaitlynPiltoverPeacemaker",

            });

            #endregion Caitlyn

            #region Cassiopeia

            Spells.Add(
            new SpellData
            {
                CharName = "Cassiopeia",
                Name = "Twin Fang",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "CassiopeiaTwinFang",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Cassiopeia",
                Name = "CassiopeiaMiasma",
                SpellKey = SpellSlot.W,
                SpellName = "CassiopeiaMiasma",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Cassiopeia",
                Name = "Noxious Blast",
                SpellKey = SpellSlot.Q,
                SpellName = "CassiopeiaNoxiousBlast",

            });

            #endregion Cassiopeia

            #region Chogath

            Spells.Add(
            new SpellData
            {
                CharName = "Chogath",
                Name = "FeralScream",
                SpellDelay = 500,
                SpellKey = SpellSlot.W,
                SpellName = "FeralScream",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Chogath",
                Name = "Rupture",
                SpellDelay = 500,
                SpellKey = SpellSlot.Q,
                SpellName = "Rupture",

            });
            #endregion Chogath

            #region Corki

            Spells.Add(
            new SpellData
            {
                CharName = "Corki",
                Name = "Missile Barrage",
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "MissileBarrage",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Corki",
                Name = "Phosphorus Bomb",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "PhosphorusBomb",

            });
            #endregion Corki

            #region Darius

            Spells.Add(
            new SpellData
            {
                CharName = "Darius",
                Name = "Decimate",
                SpellDelay = 230,
                SpellKey = SpellSlot.Q,
                SpellName = "DariusCleave",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Darius",
                Name = "Apprehend",
                SpellDelay = 320,
                SpellKey = SpellSlot.E,
                SpellName = "DariusAxeGrabCone",

            });
            #endregion Darius

            #region Diana

            Spells.Add(
            new SpellData
            {
                CharName = "Diana",
                Name = "DianaVortex",
                SpellKey = SpellSlot.E,
                SpellName = "DianaVortex",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Diana",
                Name = "DianaArc",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "DianaArc",

            });
            #endregion Diana

            #region DrMundo

            Spells.Add(
            new SpellData
            {
                CharName = "DrMundo",
                Name = "Infected Cleaver",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "InfectedCleaverMissile",

            });
            #endregion DrMundo

            #region Draven

            Spells.Add(
            new SpellData
            {
                CharName = "Draven",
                Name = "Stand Aside",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "DravenDoubleShot",

            });
            #endregion Draven

            #region Elise

            Spells.Add(
            new SpellData
            {
                CharName = "Elise",
                Name = "Volatile Spiderling",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "EliseHumanW",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Elise",
                Name = "Venomous Bite",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "EliseSpiderQCast",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Elise",
                Name = "Cocoon",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "EliseHumanE",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Elise",
                Name = "Neurotoxin",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "EliseHumanQ",

            });
            #endregion Elise

            #region Evelynn

            Spells.Add(
            new SpellData
            {
                CharName = "Evelynn",
                Name = "Ravage",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "EvelynnQ",

            });

            #endregion Evelynn

            #region Ezreal

            Spells.Add(
            new SpellData
            {
                CharName = "Ezreal",
                Name = "Essence Flux",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "EzrealEssenceFlux",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ezreal",
                Name = "Mystic Shot",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "EzrealMysticShot",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ezreal",
                Name = "Trueshot Barrage",
                SpellDelay = 1000,
                SpellKey = SpellSlot.R,
                SpellName = "EzrealTrueshotBarrage",

            });
            #endregion Ezreal

            #region FiddleSticks

            Spells.Add(
            new SpellData
            {
                CharName = "FiddleSticks",
                Name = "Crowstorm",
                SpellKey = SpellSlot.R,
                SpellName = "Crowstorm",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "FiddleSticks",
                Name = "Terrify",
                SpellKey = SpellSlot.Q,
                SpellName = "Terrify",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "FiddleSticks",
                Name = "Dark Wind",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "FiddlesticksDarkWind",

            });
            #endregion FiddleSticks

            #region Galio

            Spells.Add(
            new SpellData
            {
                CharName = "Galio",
                Name = "GalioResoluteSmite",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "GalioResoluteSmite",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Galio",
                Name = "GalioRighteousGust",
                SpellKey = SpellSlot.E,
                SpellName = "GalioRighteousGust",

            });

            #endregion Galio

            #region Gangplank

            Spells.Add(
            new SpellData
            {
                CharName = "Gangplank",
                Name = "CannonBarrage",
                SpellKey = SpellSlot.R,
                SpellName = "CannonBarrage",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Gangplank",
                Name = "Parley",
                SpellKey = SpellSlot.Q,
                SpellName = "Parley",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Gangplank",
                Name = "RaiseMorale",
                SpellKey = SpellSlot.E,
                SpellName = "RaiseMorale",

            });
            #endregion Gangplank

            #region Gragas

            Spells.Add(
            new SpellData
            {
                CharName = "Gragas",
                Name = "Barrel Roll",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "GragasBarrelRoll",

            });
            #endregion Gragas

            #region Graves

            Spells.Add(
            new SpellData
            {
                CharName = "Graves",
                Name = "Smoke Screen",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "GravesSmokeGrenade",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Graves",
                Name = "Buckshot",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "GravesClusterShot",

            });
            #endregion Graves

            #region Hecarim
            #endregion Hecarim

            #region Heimerdinger

            Spells.Add(
            new SpellData
            {
                CharName = "Heimerdinger",
                Name = "HeimerdingerE",
                SpellKey = SpellSlot.E,
                SpellName = "HeimerdingerE",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Heimerdinger",
                Name = "HeimerdingerW",
                SpellKey = SpellSlot.W,
                SpellName = "HeimerdingerW",

            });
            #endregion Heimerdinger

            #region Janna

            Spells.Add(
            new SpellData
            {
                CharName = "Janna",
                Name = "SowTheWind",
                SpellKey = SpellSlot.W,
                SpellName = "SowTheWind",

            });
            #endregion Janna

            #region Jayce

            Spells.Add(
            new SpellData
            {
                CharName = "Jayce",
                Name = "JayceShockBlast",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "jayceshockblast",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Jayce",
                Name = "JayceQ",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "JayceToTheSkies",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Jayce",
                Name = "Thundering Blow",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "JayceThunderingBlow",

            });
            #endregion Jayce

            #region Jinx

            Spells.Add(
            new SpellData
            {
                CharName = "Jinx",
                Name = "Zap",
                SpellDelay = 600,
                SpellKey = SpellSlot.W,
                SpellName = "JinxWMissile",

            });
            #endregion Jinx

            #region Karma

            Spells.Add(
            new SpellData
            {
                CharName = "Karma",
                Name = "KarmaQ",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "KarmaQ",

            });
            #endregion Karma

            #region Karthus

            Spells.Add(
            new SpellData
            {
                CharName = "Karthus",
                Name = "Lay Waste",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "karthuslaywastea3",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Karthus",
                Name = "WallOfPain",
                SpellKey = SpellSlot.W,
                SpellName = "WallOfPain",

            });
            #endregion Karthus

            #region Kassadin

            Spells.Add(
            new SpellData
            {
                CharName = "Kassadin",
                Name = "Force Pulse",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "ForcePulse",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Kassadin",
                Name = "Null Sphere",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "NullLance",

            });
            #endregion Kassadin

            #region Katarina

            Spells.Add(
            new SpellData
            {
                CharName = "Katarina",
                Name = "KatarinaQ",
                SpellKey = SpellSlot.Q,
                SpellName = "KatarinaQ",

            });
            #endregion Katarina

            #region Kayle

            Spells.Add(
            new SpellData
            {
                CharName = "Kayle",
                Name = "Reckoning",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "JudicatorReckoning",

            });
            #endregion Kayle

            #region Kennen

            Spells.Add(
            new SpellData
            {
                CharName = "Kennen",
                Name = "Thundering Shuriken",
                SpellDelay = 180,
                SpellKey = SpellSlot.Q,
                SpellName = "KennenShurikenHurlMissile1",

            });
            #endregion Kennen

            #region Khazix

            Spells.Add(
            new SpellData
            {
                CharName = "Khazix",
                Name = "KhazixQ",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "KhazixQ",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Khazix",
                Name = "KhazixW",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "KhazixW",

            });
            #endregion Khazix

            #region KogMaw

            Spells.Add(
            new SpellData
            {
                CharName = "KogMaw",
                Name = "Living Artillery",
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "KogMawLivingArtillery",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "KogMaw",
                Name = "Caustic Spittle",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "KogMawCausticSpittle",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "KogMaw",
                Name = "Void Ooze",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "KogMawVoidOozeMissile",

            });
            #endregion KogMaw

            #region Leblanc

            Spells.Add(
            new SpellData
            {
                CharName = "Leblanc",
                Name = "Sigil of Silence",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "LeblancChaosOrb",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Leblanc",
                Name = "Ethereal Chains",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "LeblancSoulShackle",

            });
            #endregion Leblanc

            #region LeeSin

            Spells.Add(
            new SpellData
            {
                CharName = "LeeSin",
                Name = "Sonic Wave",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "BlindMonkQOne",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "LeeSin",
                Name = "BlindMonkEOne",
                SpellKey = SpellSlot.E,
                SpellName = "BlindMonkEOne",

            });
            #endregion LeeSin

            #region Lissandra

            Spells.Add(
            new SpellData
            {
                CharName = "Lissandra",
                Name = "LissandraQ",
                SpellKey = SpellSlot.Q,
                SpellName = "LissandraQ",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Lissandra",
                Name = "LissandraW",
                SpellKey = SpellSlot.W,
                SpellName = "LissandraW",

            });

            #endregion Lissandra

            #region Lucian

            Spells.Add(
            new SpellData
            {
                CharName = "Lucian",
                Name = "LucianQ",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "LucianQ",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Lucian",
                Name = "LucianW",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "LucianW",

            });
            #endregion Lucian

            #region Lulu

            Spells.Add(
            new SpellData
            {
                CharName = "Lulu",
                Name = "LuluQ",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "LuluQ",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Lulu",
                Name = "LuluE",
                SpellKey = SpellSlot.E,
                SpellName = "LuluE",

            });
            #endregion Lulu

            #region Lux

            Spells.Add(
            new SpellData
            {
                CharName = "Lux",
                Name = "Light Binding",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "LuxLightBinding",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Lux",
                Name = "LuxLightStrikeKugel",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "LuxLightStrikeKugel",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Lux",
                Name = "LuxPrismaticWave",
                SpellKey = SpellSlot.W,
                SpellName = "LuxPrismaticWave",

            });
            #endregion Lux

            #region Malphite

            Spells.Add(
            new SpellData
            {
                CharName = "Malphite",
                Name = "Seismic Shard",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "SeismicShard",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Malphite",
                Name = "Ground Slam",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "Landslide",

            });
            #endregion Malphite

            #region Malzahar

            Spells.Add(
            new SpellData
            {
                CharName = "Malzahar",
                Name = "AlZaharCalloftheVoid",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "AlZaharCalloftheVoid",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Malzahar",
                Name = "Null Zone",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "AlZaharNullZone",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Malzahar",
                Name = "Malefic Visions",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "AlZaharMaleficVisions",

            });
            #endregion Malzahar

            #region Maokai

            Spells.Add(
            new SpellData
            {
                CharName = "Maokai",
                Name = "MaokaiTrunkLine",
                SpellKey = SpellSlot.Q,
                SpellName = "MaokaiTrunkLine",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Maokai",
                Name = "MaokaiSapling2",
                SpellKey = SpellSlot.E,
                SpellName = "MaokaiSapling2",

            });
            #endregion Maokai

            #region MissFortune

            Spells.Add(
            new SpellData
            {
                CharName = "MissFortune",
                Name = "MissFortuneRicochetShot",
                SpellKey = SpellSlot.Q,
                SpellName = "MissFortuneRicochetShot",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "MissFortune",
                Name = "MissFortuneScattershot",
                SpellKey = SpellSlot.E,
                SpellName = "MissFortuneScattershot",

            });
            #endregion MissFortune

            #region Mordekaiser

            Spells.Add(
            new SpellData
            {
                CharName = "Mordekaiser",
                Name = "MordekaiserSyphonOfDestruction",
                SpellKey = SpellSlot.E,
                SpellName = "MordekaiserSyphonOfDestruction",

            });
            #endregion Mordekaiser

            #region Morgana

            Spells.Add(
            new SpellData
            {
                CharName = "Morgana",
                Name = "TormentedSoil",
                SpellKey = SpellSlot.W,
                SpellName = "TormentedSoil",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Morgana",
                Name = "Dark Binding",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "DarkBindingMissile",

            });

            #endregion Morgana

            #region Nami

            Spells.Add(
            new SpellData
            {
                CharName = "Nami",
                Name = "NamiQ",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "NamiQ",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Nami",
                Name = "TidecallersBlessing",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "NamiE",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Nami",
                Name = "Ebb and Flow",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "NamiW",

            });
            #endregion Nami

            #region Nasus

            Spells.Add(
            new SpellData
            {
                CharName = "Nasus",
                Name = "Wither",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "NasusW",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Nasus",
                Name = "NasusE",
                SpellKey = SpellSlot.E,
                SpellName = "NasusE",

            });
            #endregion Nasus

            #region Nautilus

            Spells.Add(
            new SpellData
            {
                CharName = "Nautilus",
                Name = "NautilusSplashZone",
                SpellKey = SpellSlot.E,
                SpellName = "NautilusSplashZone",

            });

            #endregion Nautilus

            #region Nidalee

            Spells.Add(
            new SpellData
            {
                CharName = "Nidalee",
                Name = "PrimalSurge",
                SpellKey = SpellSlot.E,
                SpellName = "PrimalSurge",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Nidalee",
                Name = "Javelin Toss",
                SpellDelay = 125,
                SpellKey = SpellSlot.Q,
                SpellName = "JavelinToss",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Nidalee",
                Name = "Swipe",
                SpellKey = SpellSlot.E,
                SpellName = "Swipe",

            });
            #endregion Nidalee

            #region Nocturne

            Spells.Add(
            new SpellData
            {
                CharName = "Nocturne",
                Name = "Unspeakable Horror",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "NocturneUnspeakableHorror",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Nocturne",
                Name = "NocturneDuskbringer",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "NocturneDuskbringer",

            });
            #endregion Nocturne

            #region Nunu

            Spells.Add(
            new SpellData
            {
                CharName = "Nunu",
                Name = "IceBlast",
                SpellDelay = 400,
                SpellKey = SpellSlot.E,
                SpellName = "IceBlast",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Nunu",
                Name = "Consume",
                SpellDelay = 400,
                SpellKey = SpellSlot.Q,
                SpellName = "Consume",

            });
            #endregion Nunu

            #region Olaf

            Spells.Add(
            new SpellData
            {
                CharName = "Olaf",
                Name = "Reckless Swing",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "OlafRecklessStrike",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Olaf",
                Name = "Undertow",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "OlafAxeThrow",

            });
            #endregion Olaf

            #region Pantheon

            Spells.Add(
            new SpellData
            {
                CharName = "Pantheon",
                Name = "Pantheon_Throw",
                SpellKey = SpellSlot.Q,
                SpellName = "Pantheon_Throw",

            });

            #endregion Pantheon

            #region Quinn

            Spells.Add(
            new SpellData
            {
                CharName = "Quinn",
                Name = "QuinnQ",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "QuinnQ",

            });
            #endregion Quinn

            #region Renekton

            Spells.Add(
            new SpellData
            {
                CharName = "Renekton",
                Name = "RenektonCleave",
                SpellKey = SpellSlot.Q,
                SpellName = "RenektonCleave",

            });

            #endregion Renekton

            #region Rengar

            Spells.Add(
            new SpellData
            {
                CharName = "Rengar",
                Name = "Bola Strike",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "RengarE",

            });
            #endregion Rengar

            #region Riven

            Spells.Add(
            new SpellData
            {
                CharName = "Riven",
                Name = "Ki Burst",
                SpellDelay = 267,
                SpellKey = SpellSlot.W,
                SpellName = "RivenMartyr",

            });
            #endregion Riven

            #region Rumble

            Spells.Add(
            new SpellData
            {
                CharName = "Rumble",
                Name = "RumbleGrenade",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "RumbleGrenade",

            });
            #endregion Rumble

            #region Ryze

            Spells.Add(
            new SpellData
            {
                CharName = "Ryze",
                Name = "Overload",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "Overload",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ryze",
                Name = "Rune Prison",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "RunePrison",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ryze",
                Name = "Spell Flux",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "SpellFlux",

            });
            #endregion Ryze

            #region Shaco

            Spells.Add(
            new SpellData
            {
                CharName = "Shaco",
                Name = "JackInTheBox",
                SpellKey = SpellSlot.W,
                SpellName = "JackInTheBox",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Shaco",
                Name = "TwoShivPoison",
                SpellKey = SpellSlot.E,
                SpellName = "TwoShivPoison",

            });
            #endregion Shaco

            #region Shen

            Spells.Add(
            new SpellData
            {
                CharName = "Shen",
                Name = "ShenVorpalStar",
                SpellKey = SpellSlot.Q,
                SpellName = "ShenVorpalStar",

            });
            #endregion Shen

            #region Shyvana

            Spells.Add(
            new SpellData
            {
                CharName = "Shyvana",
                Name = "ShyvanaFireball",
                SpellKey = SpellSlot.E,
                SpellName = "ShyvanaFireball",

            });
            #endregion Shyvana

            #region Sion

            Spells.Add(
            new SpellData
            {
                CharName = "Sion",
                Name = "CrypticGaze",
                SpellKey = SpellSlot.Q,
                SpellName = "CrypticGaze",

            });
            #endregion Sion

            #region Sivir

            Spells.Add(
            new SpellData
            {
                CharName = "Sivir",
                Name = "Boomerang Blade",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "SivirQ",

            });
            #endregion Sivir

            #region Skarner

            Spells.Add(
            new SpellData
            {
                CharName = "Skarner",
                Name = "Fracture",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "SkarnerFracture",

            });
            #endregion Skarner

            #region Soraka

            Spells.Add(
            new SpellData
            {
                CharName = "Soraka",
                Name = "Infuse",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "Infuse",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Soraka",
                Name = "Starcall",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "Starcall",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Soraka",
                Name = "AstralBlessing",
                SpellKey = SpellSlot.W,
                SpellName = "AstralBlessing",

            });
            #endregion Soraka

            #region Swain

            Spells.Add(
            new SpellData
            {
                CharName = "Swain",
                Name = "Nevermove",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "SwainShadowGrasp",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Swain",
                Name = "Torment",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "SwainTorment",

            });
            #endregion Swain

            #region Syndra

            Spells.Add(
            new SpellData
            {
                CharName = "Syndra",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "SyndraE",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Syndra",
                Name = "SyndraQ",
                SpellDelay = 200,
                SpellKey = SpellSlot.Q,
                SpellName = "SyndraQ",

            });
            #endregion Syndra

            #region Talon

            Spells.Add(
            new SpellData
            {
                CharName = "Talon",
                Name = "Rake",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "TalonRake",

            });

            #endregion Talon

            #region Taric

            Spells.Add(
            new SpellData
            {
                CharName = "Taric",
                Name = "Shatter",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "Shatter",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Taric",
                Name = "Radiance",
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "TaricHammerSmash",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Taric",
                Name = "Imbue",
                SpellKey = SpellSlot.Q,
                SpellName = "Imbue",

            });
            #endregion Taric

            #region Teemo

            Spells.Add(
            new SpellData
            {
                CharName = "Teemo",
                SpellDelay = 250,
                SpellKey = SpellSlot.R,
                SpellName = "BantamTrap",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Teemo",
                Name = "BlindingDart",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "BlindingDart",

            });
            #endregion Teemo

            #region Thresh

            Spells.Add(
            new SpellData
            {
                CharName = "Thresh",
                Name = "ThreshQ",
                SpellDelay = 500,
                SpellKey = SpellSlot.Q,
                SpellName = "ThreshQ",

            });
            #endregion Thresh

            #region Tristana

            Spells.Add(
            new SpellData
            {
                CharName = "Tristana",
                Name = "Explosive Shot",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "DetonatingShot",

            });
            #endregion Tristana

            #region Trundle

            Spells.Add(
            new SpellData
            {
                CharName = "Trundle",
                Name = "TrundleCircle",
                SpellKey = SpellSlot.E,
                SpellName = "TrundleCircle",

            });
            #endregion Trundle

            #region Tryndamere

            Spells.Add(
            new SpellData
            {
                CharName = "Tryndamere",
                Name = "MockingShout",
                SpellKey = SpellSlot.W,
                SpellName = "MockingShout",

            });
            #endregion Tryndamere

            #region TwistedFate

            Spells.Add(
            new SpellData
            {
                CharName = "TwistedFate",
                Name = "Loaded Dice",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "WildCards",

            });
            #endregion TwistedFate

            #region Twitch

            Spells.Add(
            new SpellData
            {
                CharName = "Twitch",
                Name = "Expunge",
                SpellKey = SpellSlot.E,
                SpellName = "Expunge",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Twitch",
                Name = "TwitchVenomCask",
                SpellKey = SpellSlot.W,
                SpellName = "TwitchVenomCask",

            });
            #endregion Twitch

            #region Urgot

            Spells.Add(
            new SpellData
            {
                CharName = "Urgot",
                Name = "UrgotPlasmaGrenade",
                SpellKey = SpellSlot.E,
                SpellName = "UrgotPlasmaGrenade",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Urgot",
                Name = "UrgotHeatseekingMissile",
                SpellKey = SpellSlot.Q,
                SpellName = "UrgotHeatseekingMissile",

            });

            #endregion Urgot

            #region Varus

            Spells.Add(
            new SpellData
            {
                CharName = "Varus",
                Name = "Varus E",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "VarusE",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Varus",
                Name = "Varus Q Missile",
                SpellDelay = 0,
                SpellKey = SpellSlot.Q,
                SpellName = "VarusQ",

            });
            #endregion Varus

            #region Vayne

            Spells.Add(
            new SpellData
            {
                CharName = "Vayne",
                Name = "VayneCondemn",
                SpellKey = SpellSlot.E,
                SpellName = "VayneCondemn",

            });
            #endregion Vayne

            #region Veigar

            Spells.Add(
            new SpellData
            {
                CharName = "Veigar",
                Name = "VeigarDarkMatter",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "VeigarDarkMatter",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Veigar",
                Name = "Baleful Strike",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "VeigarBalefulStrike",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Veigar",
                Name = "VeigarEventHorizon",
                SpellKey = SpellSlot.E,
                SpellName = "VeigarEventHorizon",

            });
            #endregion Veigar

            #region Velkoz

            Spells.Add(
            new SpellData
            {
                CharName = "Velkoz",
                Name = "VelkozW",
                SpellKey = SpellSlot.W,
                SpellName = "VelkozW",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Velkoz",
                Name = "VelkozQ",
                SpellKey = SpellSlot.Q,
                SpellName = "VelkozQ",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Velkoz",
                Name = "VelkozE",
                SpellKey = SpellSlot.E,
                SpellName = "VelkozE",

            });
            #endregion Velkoz

            #region Vi
            #endregion Vi

            #region Viktor

            Spells.Add(
            new SpellData
            {
                CharName = "Viktor",
                Name = "ViktorPowerTransfer",
                SpellKey = SpellSlot.Q,
                SpellName = "ViktorPowerTransfer",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Viktor",
                Name = "ViktorGravitonField",
                SpellKey = SpellSlot.W,
                SpellName = "ViktorGravitonField",

            });

            #endregion Viktor

            #region Vladimir

            Spells.Add(
            new SpellData
            {
                CharName = "Vladimir",
                Name = "VladimirTidesofBlood",
                SpellKey = SpellSlot.E,
                SpellName = "VladimirTidesofBlood",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Vladimir",
                Name = "VladimirHemoplague",
                SpellDelay = 389,
                SpellKey = SpellSlot.R,
                SpellName = "VladimirHemoplague",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Vladimir",
                Name = "VladimirTransfusion",
                SpellKey = SpellSlot.Q,
                SpellName = "VladimirTransfusion",

            });
            #endregion Vladimir

            #region Warwick

            Spells.Add(
            new SpellData
            {
                CharName = "Warwick",
                Name = "Hungering Strike",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "HungeringStrike",

            });
            #endregion Warwick

            #region Xerath

            Spells.Add(
            new SpellData
            {
                CharName = "Xerath",
                Name = "XerathMageSpear",
                SpellKey = SpellSlot.E,
                SpellName = "XerathMageSpear",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Xerath",
                Name = "XerathArcaneBarrage2",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "XerathArcaneBarrage2",

            });
            #endregion Xerath

            #region Yasuo

            Spells.Add(
            new SpellData
            {
                CharName = "Yasuo",
                Name = "Steel Tempest3",
                SpellDelay = 175,
                SpellKey = SpellSlot.Q,
                SpellName = "YasuoQW",

            });

            #endregion Yasuo

            #region Yorick

            Spells.Add(
            new SpellData
            {
                CharName = "Yorick",
                Name = "YorickDecayed",
                SpellKey = SpellSlot.W,
                SpellName = "YorickDecayed",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Yorick",
                Name = "YorickRavenous",
                SpellKey = SpellSlot.E,
                SpellName = "YorickRavenous",

            });
            #endregion Yorick

            #region Zac

            Spells.Add(
            new SpellData
            {
                CharName = "Zac",
                Name = "ZacQ",
                SpellKey = SpellSlot.Q,
                SpellName = "ZacQ",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Zac",
                Name = "ZacW",
                SpellKey = SpellSlot.W,
                SpellName = "ZacW",

            });
            #endregion Zac

            #region Zed

            Spells.Add(
            new SpellData
            {
                CharName = "Zed",
                Name = "ZedShuriken",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "ZedShuriken",

            });
            #endregion Zed

            #region Ziggs

            Spells.Add(
            new SpellData
            {
                CharName = "Ziggs",
                Name = "ZiggsW",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "ZiggsW",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ziggs",
                Name = "ZiggsE",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "ZiggsE",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Ziggs",
                Name = "ZiggsQ",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "ZiggsQ",

            });

            #endregion Ziggs

            #region Zilean

            Spells.Add(
            new SpellData
            {
                CharName = "Zilean",
                Name = "Time Bomb",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "TimeBomb",

            });

            #endregion Zilean

            #region Zyra

            Spells.Add(
            new SpellData
            {
                CharName = "Zyra",
                Name = "Grasping Roots",
                SpellDelay = 250,
                SpellKey = SpellSlot.E,
                SpellName = "ZyraGraspingRoots",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Zyra",
                Name = "Rampant Growth",
                SpellDelay = 250,
                SpellKey = SpellSlot.W,
                SpellName = "ZyraSeed",

            });

            Spells.Add(
            new SpellData
            {
                CharName = "Zyra",
                Name = "Deadly Bloom",
                SpellDelay = 250,
                SpellKey = SpellSlot.Q,
                SpellName = "ZyraQFissure",

            });
            #endregion Zyra
        }
    }
}
