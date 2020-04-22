using System;
using System.Collections.Generic;

namespace SA2SaveUtility {
    public class SavedValues {

        public byte[] SaveFileBytes = new byte[0];

        public SavedValues() {

        }

        public int GCFileNumber = 0;
        public string FileTitle = "Sonic Adventure 2 Save";
        public TimeSpan PlayTimeSpan = TimeSpan.FromSeconds(0);
        public TimeSpan EmblemResultsTimeSpan = TimeSpan.FromSeconds(0);
        public Int16 Lives = 0;
        public Int32 Rings = 0;
        public int TextLanguage = 0;
        public int VoiceLanguage = 0;

        public int SonicCW = 0;
        public int TailsCW = 0;
        public int KnucklesCW = 0;
        public int ShadowCW = 0;
        public int EggmanCW = 0;
        public int RougeCW = 0;
        public int SonicLS = 0;
        public int SonicAL = 0;
        public int SonicMG = 0;
        public int SonicFR = 0;
        public int SonicBB = 0;
        public int SonicMM = 0;
        public int TailsBo = 0;
        public int TailsBa = 0;
        public int TailsL = 0;
        public int TailsMM = 0;
        public int KnucklesSC = 0;
        public int KnucklesS = 0;
        public int KnucklesHG = 0;
        public int KnucklesAN = 0;
        public int KnucklesMM = 0;
        public int ShadowAS = 0;
        public int ShadowAL = 0;
        public int ShadowFR = 0;
        public int ShadowMM = 0;
        public int EggmanJE = 0;
        public int EggmanLC = 0;
        public int EggmanLB = 0;
        public int EggmanPA = 0;
        public int EggmanMM = 0;
        public int RougePN = 0;
        public int RougeTS = 0;
        public int RougeIB = 0;
        public int RougeMM = 0;
        public int KarateB = 0;
        public int KarateS = 0;
        public int KarateE = 0;
        public int KarateSu = 0;
        public int RaceB = 0;
        public int RaceJ = 0;
        public int RaceC = 0;
        public int RaceH = 0;
        public int RaceD = 0;
        public int ThemeA = 0;
        public int ThemeM = 0;
        public int ThemeS = 0;
        public int ThemeO = 0;
        public int GreenH = 0;
        public int KartS = 0;
        public int KartSh = 0;
        public int KartT = 0;
        public int KartE = 0;
        public int KartK = 0;
        public int KartR = 0;

        public List<LevelValues> LevelList = new List<LevelValues>();
    }

    public class LevelValues {
        public string LevelName;
        public List<MissionValues> Missions = new List<MissionValues>();

        public LevelValues(string name) {
            LevelName = name;
        }
    }

    public class MissionValues {
        public int Number = 0;
        public int Grade = 0;
        public int Plays = 0;
        public List<MissionHighScore> HighScores = new List<MissionHighScore>();

        public MissionValues(int num) {
            Number = num;
        }
    }
    public class MissionHighScore {
        public int Number = 0;
        public int Rings = 0;
        public int Score = 0;
        public TimeSpan Time = TimeSpan.FromSeconds(0);
        public MissionHighScore(int num) {
            Number = num;
        }
    }

    public static class StaticOffsets {
        public static class GameCube {
            public static int TextLanguage = 0x2848;
            public static int VoiceLanguage = 0x2849;
        }
        public static class Main {
            public static int TextLanguage = 0x2849;
            public static int VoiceLanguage = 0x284A;
            public static int EmblemCount = 0x284E;
            public static int Lives = 0x285C;
            public static int EmblemResultsTime = 0x2868;
            public static int PlayTime = 0x286C;
            public static int Rings = 0x2870;
            public static int GreenHill = 0x4034;
            public static int ChaoRaceBeginner = 0x5A64;
            public static int ChaoRaceJewel = 0x5A65;
            public static int ChaoRaceChallenge = 0x5A66;
            public static int ChaoRaceHero = 0x5A67;
            public static int ChaoRaceDark = 0x5A68;
            public static int ChaoWorldSonic = 0x5A69;
            public static int ChaoWorldShadow = 0x5A6A;
            public static int ChaoWorldKnuckles = 0x5A6D;
            public static int ChaoWorldRouge = 0x5A6E;
            public static int ChaoWorldTails = 0x5A6F;
            public static int ChaoWorldEggman = 0x5A70;
            public static int ChaoKarateBeginner = 0x5A71;
            public static int ChaoKarateStandard = 0x5A72;
            public static int ChaoKarateExpert = 0x5A73;
            public static int ChaoKarateSuper = 0x5A74;
            public static int ChaoKarateSuperUnlocked = 0x5A75;
            public static int SonicLightShoes = 0x5A77;
            public static int SonicAncientLight = 0x5A78;
            public static int SonicMagic = 0x5A79;
            public static int SonicFlame = 0x5A7A;
            public static int SonicBounce = 0x5A7B;
            public static int SonicMM = 0x5A7C;
            public static int TailsBooster = 0x5A7D;
            public static int TailsBazooka = 0x5A7E;
            public static int TailsLaser = 0x5A7F;
            public static int TailsMM = 0x5A80;
            public static int KnucklesShovel = 0x5A81;
            public static int KnucklesSun = 0x5A82;
            public static int KnucklesHammer = 0x5A83;
            public static int KnucklesAir = 0x5A84;
            public static int KnucklesMM = 0x5A85;
            public static int ShadowAir = 0x5A87;
            public static int ShadowAncientLight = 0x5A88;
            public static int ShadowFlame = 0x5A89;
            public static int ShadowMM = 0x5A8A;
            public static int EggmanJet = 0x5A8B;
            public static int EggmanCannon = 0x5A8C;
            public static int EggmanLaser = 0x5A8D;
            public static int EggmanArmor = 0x5A8E;
            public static int EggmanMM = 0x5A8F;
            public static int RougePick = 0x5A90;
            public static int RougeTreasure = 0x5A91;
            public static int RougeBoots = 0x5A92;
            public static int RougeMM = 0x5A93;
            public static int KartSonic = 0x5AD3;
            public static int KartTails = 0x5AD4;
            public static int KartKnuckles = 0x5AD5;
            public static int KartShadow = 0x5AD6;
            public static int KartEggman = 0x5AD7;
            public static int KartRouge = 0x5AD8;
            public static int ThemeAmy = 0x5ADB;
            public static int ThemeMaria = 0x5ADC;
            public static int ThemeSecretary = 0x5ADD;
            public static int ThemeOmochao = 0x5ADE;
            public static int AllASonic = 0x5CCD;
            public static int AllAShadow = 0x5CCE;
            public static int AllATails = 0x5CCF;
            public static int AllAEggman = 0x5CD0;
            public static int AllAKnuckles = 0x5CD1;
            public static int AllARouge = 0x5CD2;
            public static int HeroStoryComplete = 0x5CD3;
            public static int DarkStoryComplete = 0x5CD4;
            public static int LastStoryComplete = 0x5CD5;
            public static int BossAttackHero = 0x5CD6;
            public static int BossAttackDark = 0x5CD7;
            public static int BossAttackAll = 0x5CD8;
            public static int AllACannonsCore = 0x5CD9;
        }
        public static class Missions {
            public static Dictionary<string, int> StartingOffsets = new Dictionary<string, int>()
            {
                { "City Escape", 0x326C },
                { "Wild Canyon", 0x34B8 },
                { "Prison Lane", 0x2F5C },
                { "Metal Harbour", 0x3020 },
                { "Green Forest", 0x2AC4 },
                { "Pumpkin Hill", 0x2C4C },
                { "Mission Street", 0x357C },
                { "Aquatic Mine", 0x2DD4 },
                { "Route 101", 0x5668 },
                { "Hidden Base", 0x3A14 },
                { "Pyramid Cave", 0x3DE8 },
                { "Death Chamber", 0x3B9C },
                { "Eternal Engine", 0x3AD8 },
                { "Meteor Herd", 0x40F8 },
                { "Crazy Gadget", 0x3950 },
                { "Final Rush", 0x3F70 },

                { "Iron Gate", 0x30E4 },
                { "Dry Lagoon", 0x3640 },
                { "Sand Ocean", 0x388C },
                { "Radical Highway", 0x3330 },
                { "Egg Quarters", 0x3C60 },
                { "Lost Colony", 0x3D24 },
                { "Weapons Bed", 0x31A8 },
                { "Security Hall", 0x2E98 },
                { "White Jungle", 0x2B88 },
                { "Route 202", 0x572C },
                { "Sky Rail", 0x2D10 },
                { "Mad Space", 0x4A28 },
                { "Cosmic Wall", 0x4964 },
                { "Final Chase", 0x4718 },

                { "Cannon's Core", 0x4280 }
            };

            public static class InternalOffsets {
                // Level data is stored in 0xC0 (192) bytes

                // Grades are 1 byte
                // 01 = E
                // 02 = D
                // 03 = C
                // 04 = B
                // 05 = A
                public static int[] Grades = { 0x00, 0x01, 0x02, 0x03, 0x04, };

                // There is one byte filled with 0x00 after the grades
                // This is likely so the data aligns to 2-bytes
                public static int[] BufferGrade = { 0x05 };


                // Plays are stored as 2 bytes
                // 0500 = 5 plays
                // 0200 = 2 plays
                public static int[] Plays = { 0x06, 0x08, 0x0A, 0x0C, 0x0E, };

                // Rings are stored as 4 bytes
                // 5A01 0000 = 346
                public static int[] Rings = { 0x10, 0x34, 0x58, 0x7C, 0xA0, };

                // Score is stored as 4 bytes
                // 0C35 0000 = 13580
                public static int[] Score = { 0x14, 0x38, 0x5C, 0x80, 0xA4, };

                // Time is stored as 3 bytes, 1 byte for each of "minutes", "seconds", and "milliseconds"
                // 0412 07 -> 04 12 07
                // 04 = 4 minutes
                // 12 = 18 seconds
                // 07 = 0.07 seconds or 70 milliseconds
                public static int[] Time = { 0x18, 0x3C, 0x60, 0x84, 0xA8, };

                // There is one byte filled with 0x00 after the time
                // This is likely so the data aligns to 2-bytes
                public static int[] BufferTime = { 0x1B, 0x3F, 0x63, 0x87, 0xAB, };

                // Blocks of 24 bytes are 2nd and 3rd high score data 
                public static int[] Unknown = { 0x1C, 0x40, 0x64, 0x88, 0xAC, };

            }
        }

    }

    public class KartValues {
        public uint FirstT = 0x00;
        public uint FirstC = 0x03;
        public uint SecondT = 0x04;
        public uint SecondC = 0x07;
        public uint ThirdT = 0x08;
        public uint ThirdC = 0x0B;
        public uint Emblem = 0x0C;

    }


    class BossValues {
        public uint Emblem = 0x00;
        public uint FirstT = 0x18;
        public uint SecondT = 0x24;
        public uint ThirdT = 0x30;
    }


    class ChaoValues {
        public uint Name = 0x12;
        public uint SwimBar = 0x20;
        public uint FlyBar = 0x21;
        public uint RunBar = 0x22;
        public uint PowerBar = 0x23;
        public uint StaminaBar = 0x24;
        public uint LuckBar = 0x25;
        public uint IntelligenceBar = 0x26;
        public uint SwimGrade = 0x28;
        public uint FlyGrade = 0x29;
        public uint RunGrade = 0x2A;
        public uint PowerGrade = 0x2B;
        public uint StaminaGrade = 0x2C;
        public uint LuckGrade = 0x2D;
        public uint IntelligenceGrade = 0x2E;
        public uint SwimLevel = 0x30;
        public uint FlyLevel = 0x31;
        public uint RunLevel = 0x32;
        public uint PowerLevel = 0x33;
        public uint StaminaLevel = 0x34;
        public uint LuckLevel = 0x35;
        public uint IntelligenceLevel = 0x36;
        public uint SwimPoints = 0x38;
        public uint FlyPoints = 0x3A;
        public uint RunPoints = 0x3C;
        public uint PowerPoints = 0x3E;
        public uint StaminaPoints = 0x40;
        public uint LuckPoints = 0x42;
        public uint IntelligencePoints = 0x44;
        public uint ChaoType = 0x80;
        public uint Garden = 0x81;
        public uint Happiness = 0x82;
        public uint InitChao = 0x84;
        public uint Lifespan1 = 0x8A;
        public uint Lifespan2 = 0x8C;
        public uint Reincarnations = 0x8E;
        public uint Run2PowerTranformation = 0xA8;
        public uint Swim2FlyTransformation = 0xAC;
        public uint Alignment = 0xB0;
        public uint TransformationMagnitude = 0xC0;
        public uint Eyes = 0xD1;
        public uint Mouth = 0xD2;
        public uint Emotiball = 0xD3;
        public uint Hat = 0xD5;
        public uint HiddenFeet = 0xD6;
        public uint Medal = 0xD7;
        public uint Colour = 0xD8;
        public uint MonoTone = 0xD9;
        public uint Texture = 0xDA;
        public uint Shiny = 0xDB;
        public uint EggColour = 0xDC;
        public uint BodyType = 0xDD;
        public uint BodyTypeAnimal = 0xDE;
        public uint SA2AnimalBehaviours = 0x118;
        public uint SA2ArmsPart = 0x11C;
        public uint SA2EarsPart = 0x11D;
        public uint SA2ForeheadPart = 0x11E;
        public uint SA2HornsPart = 0x11F;
        public uint SA2LegsPart = 0x120;
        public uint SA2TailPart = 0x121;
        public uint SA2WingsPart = 0x122;
        public uint SA2FacePart = 0x123;
        public uint Joy = 0x12C;
        public uint UrgeToCry = 0x12E;
        public uint Fear = 0x12F;
        public uint Dizziness = 0x131;
        public uint Sleepiness = 0x134;
        public uint Tiredness = 0x136;
        public uint Hunger = 0x138;
        public uint DesireToMate = 0x13A;
        public uint Boredom = 0x13C;
        public uint Energy = 0x148;
        public uint Normal2Curious = 0x14A;
        public uint CryBaby2Energetic = 0x14C;
        public uint Naive2Normal = 0x14D;
        public uint Normal2BigEater = 0x150;
        public uint Normal2Carefree = 0x155;
        public uint FavouriteFruit = 0x157;
        public uint Cough = 0x15A;
        public uint Cold = 0x15B;
        public uint Rash = 0x15C;
        public uint RunnyNose = 0x15D;
        public uint Hiccups = 0x15E;
        public uint StomachAche = 0x15F;
        public uint SA2ClassroomSkills = 0x160;
        public uint SA2Toys = 0x164;
        public uint SA2SonicBond = 0x16C;
        public uint SA2ShadowBond = 0x172;
        public uint SA2TailsBond = 0x178;
        public uint SA2EggmanBond = 0x17E;
        public uint SA2KnucklesBond = 0x184;
        public uint SA2RougeBond = 0x18A;
        public uint ResetTrigger = 0x438;
        public uint DNASwimGrade1 = 0x494;
        public uint DNASwimGrade2 = 0x495;
        public uint DNAFlyGrade1 = 0x496;
        public uint DNAFlyGrade2 = 0x497;
        public uint DNARunGrade1 = 0x498;
        public uint DNARunGrade2 = 0x499;
        public uint DNAPowerGrade1 = 0x49A;
        public uint DNAPowerGrade2 = 0x49B;
        public uint DNAStaminaGrade1 = 0x49C;
        public uint DNAStaminaGrade2 = 0x49D;
        public uint DNALuckGrade1 = 0x49E;
        public uint DNALuckGrade2 = 0x49F;
        public uint DNAIntelligenceGrade1 = 0x4A0;
        public uint DNAIntelligenceGrade2 = 0x4A1;
        public uint DNAFavouriteFruit1 = 0x4C6;
        public uint DNAFavouriteFruit2 = 0x4C7;
        public uint DNAColour1 = 0x4CC;
        public uint DNAColour2 = 0x4CD;
        public uint DNAMonoTone1 = 0x4CE;
        public uint DNAMonoTone2 = 0x4CF;
        public uint DNATexture1 = 0x4D0;
        public uint DNATexture2 = 0x4D1;
        public uint DNAShiny1 = 0x4D2;
        public uint DNAShiny2 = 0x4D3;
        public uint DNAEggColour1 = 0x4D4;
        public uint DNAEggColour2 = 0x4D5;
        public uint SAAnimalBehaviours = 0x4E0;
        public uint SAArmsPart = 0x4E4;
        public uint SAEarsPart = 0x4E5;
        public uint SAForeheadPart = 0x4E6;
        public uint SAHornsPart = 0x4E7;
        public uint SALegsPart = 0x4E8;
        public uint SATailPart = 0x4E9;
        public uint SAWingsPart = 0x4EA;
        public uint SASonicBond = 0x4FC;
        public uint SATailsBond = 0x502;
        public uint SAKnucklesBond = 0x508;
        public uint SAAmyBond = 0x50E;
        public uint SAE102Bond = 0x514;
        public uint SABigBond = 0x51A;
    }
}
