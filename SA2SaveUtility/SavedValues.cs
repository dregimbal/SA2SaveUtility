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

        // Chao World Characters
        public int SonicCW = 0;
        public int TailsCW = 0;
        public int KnucklesCW = 0;
        public int ShadowCW = 0;
        public int EggmanCW = 0;
        public int RougeCW = 0;

        // Upgrades
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

        // Chao Karate
        public int KarateB = 0;
        public int KarateS = 0;
        public int KarateE = 0;
        public int KarateSu = 0;

        // Chao Races
        public int RaceB = 0;
        public int RaceJ = 0;
        public int RaceC = 0;
        public int RaceH = 0;
        public int RaceD = 0;

        // Unlocked themes
        public int ThemeA = 0;
        public int ThemeM = 0;
        public int ThemeS = 0;
        public int ThemeO = 0;

        // Green hill
        public int GreenH = 0;

        // Kart Racing alt karts
        public int KartS = 0;
        public int KartSh = 0;
        public int KartT = 0;
        public int KartE = 0;
        public int KartK = 0;
        public int KartR = 0;

        // All A Ranks
        public int ARankSonic = 0;
        public int ARankShadow = 0;
        public int ARankTails = 0;
        public int ARankEggman = 0;
        public int ARankKnuckles = 0;
        public int ARankRouge = 0;

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

        public static class Unlocks {

            public static class Chao {

                /// <summary>
                /// Characters that can be used in the chao world
                /// </summary>
                public static class Characters {
                    public static int Sonic = 0x5A69;
                    public static int Shadow = 0x5A6A;
                    public static int Knuckles = 0x5A6D;
                    public static int Rouge = 0x5A6E;
                    public static int Tails = 0x5A6F;
                    public static int Eggman = 0x5A70;
                }
            }

            /// <summary>
            /// Alternative karts
            /// </summary>
            public static class Kart {
                public static int KartSonic = 0x5AD3;
                public static int KartTails = 0x5AD4;
                public static int KartKnuckles = 0x5AD5;
                public static int KartShadow = 0x5AD6;
                public static int KartEggman = 0x5AD7;
                public static int KartRouge = 0x5AD8;
            }

            public static class Sonic {
                public static int LightShoes = 0x5A77;
                public static int AncientLight = 0x5A78;
                public static int Magic = 0x5A79;
                public static int Flame = 0x5A7A;
                public static int Bounce = 0x5A7B;
                public static int MM = 0x5A7C;
            }

            public static class Tails {
                public static int Booster = 0x5A7D;
                public static int Bazooka = 0x5A7E;
                public static int Laser = 0x5A7F;
                public static int MM = 0x5A80;

            }

            public static class Knuckles {
                public static int Shovel = 0x5A81;
                public static int Sun = 0x5A82;
                public static int Hammer = 0x5A83;
                public static int Air = 0x5A84;
                public static int MM = 0x5A85;
            }

            public static class Shadow {
                public static int Air = 0x5A87;
                public static int AncientLight = 0x5A88;
                public static int Flame = 0x5A89;
                public static int MM = 0x5A8A;

            }

            public static class Eggman {
                public static int Jet = 0x5A8B;
                public static int Cannon = 0x5A8C;
                public static int Laser = 0x5A8D;
                public static int Armor = 0x5A8E;
                public static int MM = 0x5A8F;
            }

            public static class Rouge {
                public static int Pick = 0x5A90;
                public static int Treasure = 0x5A91;
                public static int Boots = 0x5A92;
                public static int MM = 0x5A93;

            }

            public static class Themes {
                public static int Amy = 0x5ADB;
                public static int Maria = 0x5ADC;
                public static int Secretary = 0x5ADD;
                public static int Omochao = 0x5ADE;
            }
        }

        public static class Emblems {
            public static class Chao {
                public static class Race {
                    public static int Beginner = 0x5A64;
                    public static int Jewel = 0x5A65;
                    public static int Challenge = 0x5A66;
                    public static int Hero = 0x5A67;
                    public static int Dark = 0x5A68;
                }

                public static class Karate {
                    public static int Beginner = 0x5A71;
                    public static int Standard = 0x5A72;
                    public static int Expert = 0x5A73;
                    public static int Super = 0x5A74;
                }

            }

            public static class AllARanks {
                public static int Sonic = 0x5CCD;
                public static int Shadow = 0x5CCE;
                public static int Tails = 0x5CCF;
                public static int Eggman = 0x5CD0;
                public static int Knuckles = 0x5CD1;
                public static int Rouge = 0x5CD2;

            }
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
            public static int ChaoKarateSuperUnlocked = 0x5A75;
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


}
