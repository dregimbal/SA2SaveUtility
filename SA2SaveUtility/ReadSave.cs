using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SA2SaveUtility {
    public class ReadSave {
        public bool DebugLogs = true;
        public SaveType FromSaveType = SaveType.GAMECUBE;
        public SaveType ToSaveType = SaveType.PC;
        public bool IsChaoSave = false;
        private byte[] saveFileBytes = new byte[0];
        private SavedValues SavedValues = new SavedValues();

        public void InjestSaveFile(byte[] saveFile) {
            saveFileBytes = saveFile;

            if (saveFileBytes.Length <= 0) {
                Debug.WriteLine("Save file byte[] length <= 0");
            }
            VerifySaveFileType();
            CorrectCustomOffsets();

            ReadDeviceSpecificData();
            ReadDeviceAgnosticData();
            ReadMissionData();
        }

        /// <summary>
        /// Some save files start their data regions in different areas
        /// </summary>
        private void CorrectCustomOffsets() {
            if (Main.ReadSave.FromSaveType == SaveType.GAMECUBE) {
                SavedValues.GCFileNumber = Int32.Parse(ReadString(2, 0x12));
                DebugWrite("GameCube Save " + SavedValues.GCFileNumber);

                saveFileBytes = saveFileBytes.Skip(0x40).ToArray();
            }
        }

        /// <summary>
        /// Check the length of the save file to deduce the type
        /// </summary>
        private void VerifySaveFileType() {
            DebugWrite("Save file is " + saveFileBytes.Length + " (0x" + saveFileBytes.Length.ToString("X4") + ") bytes long");

            switch (saveFileBytes.Length) {
                case 0x6040:
                    FromSaveType = SaveType.GAMECUBE;
                    DebugWrite("File is being read as a GameCube save");
                    break;
                case 0xC820:
                case 0x6000:
                case 0x10000:
                    FromSaveType = SaveType.PC;
                    DebugWrite("File is being read as a PC save");
                    break;
                case 0x3C028:
                    FromSaveType = SaveType.XBOX;
                    DebugWrite("File is being read as an XBox save");
                    break;
                case 0x3C050:
                    FromSaveType = SaveType.PLAYSTATION;
                    DebugWrite("File is being read as a PlayStation save");
                    break;
                case 0x10040:
                    FromSaveType = SaveType.GAMECUBE;
                    IsChaoSave = true;
                    DebugWrite("File is being read as a GameCube Chao save");
                    break;
                default:
                    FromSaveType = SaveType.PC;
                    DebugWrite("File is being read as a PC save");
                    break;
            }
        }

        /// <summary>
        /// Read data from the save file that is in a different spot depending on the save file type
        /// </summary>
        private void ReadDeviceSpecificData() {
            if (Main.ReadSave.FromSaveType == SaveType.PC) {
                SavedValues.Lives = ReadInt16(StaticOffsets.Main.Lives, true);
                SavedValues.Rings = ReadInt32(StaticOffsets.Main.Rings, true);
            } else {
                SavedValues.Lives = ReadInt16(StaticOffsets.Main.Lives, false);
                SavedValues.Rings = ReadInt32(StaticOffsets.Main.Rings, false);
            }

            if (Main.ReadSave.FromSaveType == SaveType.GAMECUBE) {
                SavedValues.TextLanguage = saveFileBytes[StaticOffsets.GameCube.TextLanguage];
                SavedValues.VoiceLanguage = saveFileBytes[StaticOffsets.GameCube.VoiceLanguage];
                Debug.WriteLine("GC language int is " + SavedValues.TextLanguage);
                Debug.WriteLine("GC voice language int is " + SavedValues.VoiceLanguage);
            } else {
                SavedValues.TextLanguage = saveFileBytes[StaticOffsets.Main.TextLanguage];
                SavedValues.VoiceLanguage = saveFileBytes[StaticOffsets.Main.VoiceLanguage];
                Debug.WriteLine("Language int is " + SavedValues.TextLanguage);
                Debug.WriteLine("Voice language int is " + SavedValues.VoiceLanguage);

            }

            if (Main.ReadSave.FromSaveType == SaveType.RTE) {

            } else {

            }

            if (Main.ReadSave.FromSaveType == SaveType.PLAYSTATION) {

            } else {

            }

            if (Main.ReadSave.FromSaveType == SaveType.SA) {

            } else {

            }

            if (Main.ReadSave.FromSaveType == SaveType.XBOX) {

            } else {

            }

        }

        /// <summary>
        /// Read data from the save file that is the same across systems
        /// </summary>
        private void ReadDeviceAgnosticData() {
            SavedValues.FileTitle = ReadString(0x19, 0x27);
            DebugWrite("File Title: " + SavedValues.GCFileNumber);

            SavedValues.PlayTimeSpan = ReadTime(StaticOffsets.Main.PlayTime);
            DebugWrite("playTime: " + (int)SavedValues.PlayTimeSpan.TotalHours + ":" + SavedValues.PlayTimeSpan.Minutes + ":" + SavedValues.PlayTimeSpan.Seconds);

            SavedValues.EmblemResultsTimeSpan = ReadTime(StaticOffsets.Main.EmblemResultsTime);
            DebugWrite("emblemTime: " + (int)SavedValues.EmblemResultsTimeSpan.TotalHours + ":" + SavedValues.EmblemResultsTimeSpan.Minutes + ":" + SavedValues.EmblemResultsTimeSpan.Seconds);


            SavedValues.SonicCW = saveFileBytes[StaticOffsets.Main.ChaoWorldSonic];
            SavedValues.TailsCW = saveFileBytes[StaticOffsets.Main.ChaoWorldTails];
            SavedValues.KnucklesCW = saveFileBytes[StaticOffsets.Main.ChaoWorldKnuckles];
            SavedValues.ShadowCW = saveFileBytes[StaticOffsets.Main.ChaoWorldShadow];
            SavedValues.EggmanCW = saveFileBytes[StaticOffsets.Main.ChaoWorldEggman];
            SavedValues.RougeCW = saveFileBytes[StaticOffsets.Main.ChaoWorldRouge];
            SavedValues.SonicLS = saveFileBytes[StaticOffsets.Main.SonicLightShoes];
            SavedValues.SonicAL = saveFileBytes[StaticOffsets.Main.SonicAncientLight];
            SavedValues.SonicMG = saveFileBytes[StaticOffsets.Main.SonicMagic];
            SavedValues.SonicFR = saveFileBytes[StaticOffsets.Main.SonicFlame];
            SavedValues.SonicBB = saveFileBytes[StaticOffsets.Main.SonicBounce];
            SavedValues.SonicMM = saveFileBytes[StaticOffsets.Main.SonicMM];
            SavedValues.TailsBo = saveFileBytes[StaticOffsets.Main.TailsBooster];
            SavedValues.TailsBa = saveFileBytes[StaticOffsets.Main.TailsBazooka];
            SavedValues.TailsL = saveFileBytes[StaticOffsets.Main.TailsLaser];
            SavedValues.TailsMM = saveFileBytes[StaticOffsets.Main.TailsMM];
            SavedValues.KnucklesSC = saveFileBytes[StaticOffsets.Main.KnucklesShovel];
            SavedValues.KnucklesS = saveFileBytes[StaticOffsets.Main.KnucklesSun];
            SavedValues.KnucklesHG = saveFileBytes[StaticOffsets.Main.KnucklesHammer];
            SavedValues.KnucklesAN = saveFileBytes[StaticOffsets.Main.KnucklesAir];
            SavedValues.KnucklesMM = saveFileBytes[StaticOffsets.Main.KnucklesMM];
            SavedValues.ShadowAS = saveFileBytes[StaticOffsets.Main.ShadowAir];
            SavedValues.ShadowAL = saveFileBytes[StaticOffsets.Main.ShadowAncientLight];
            SavedValues.ShadowFR = saveFileBytes[StaticOffsets.Main.ShadowFlame];
            SavedValues.ShadowMM = saveFileBytes[StaticOffsets.Main.ShadowMM];
            SavedValues.EggmanJE = saveFileBytes[StaticOffsets.Main.EggmanJet];
            SavedValues.EggmanLC = saveFileBytes[StaticOffsets.Main.EggmanCannon];
            SavedValues.EggmanLB = saveFileBytes[StaticOffsets.Main.EggmanLaser];
            SavedValues.EggmanPA = saveFileBytes[StaticOffsets.Main.EggmanArmor];
            SavedValues.EggmanMM = saveFileBytes[StaticOffsets.Main.EggmanMM];
            SavedValues.RougePN = saveFileBytes[StaticOffsets.Main.RougePick];
            SavedValues.RougeTS = saveFileBytes[StaticOffsets.Main.RougeTreasure];
            SavedValues.RougeIB = saveFileBytes[StaticOffsets.Main.RougeBoots];
            SavedValues.RougeMM = saveFileBytes[StaticOffsets.Main.RougeMM];
            SavedValues.KarateB = saveFileBytes[StaticOffsets.Main.ChaoKarateBeginner];
            SavedValues.KarateS = saveFileBytes[StaticOffsets.Main.ChaoKarateStandard];
            SavedValues.KarateE = saveFileBytes[StaticOffsets.Main.ChaoKarateExpert];
            SavedValues.KarateSu = saveFileBytes[StaticOffsets.Main.ChaoKarateSuper];
            SavedValues.RaceB = saveFileBytes[StaticOffsets.Main.ChaoRaceBeginner];
            SavedValues.RaceJ = saveFileBytes[StaticOffsets.Main.ChaoRaceJewel];
            SavedValues.RaceC = saveFileBytes[StaticOffsets.Main.ChaoRaceChallenge];
            SavedValues.RaceH = saveFileBytes[StaticOffsets.Main.ChaoRaceHero];
            SavedValues.RaceD = saveFileBytes[StaticOffsets.Main.ChaoRaceDark];
            SavedValues.ThemeA = saveFileBytes[StaticOffsets.Main.ThemeAmy];
            SavedValues.ThemeM = saveFileBytes[StaticOffsets.Main.ThemeMaria];
            SavedValues.ThemeS = saveFileBytes[StaticOffsets.Main.ThemeSecretary];
            SavedValues.ThemeO = saveFileBytes[StaticOffsets.Main.ThemeOmochao];
            SavedValues.GreenH = saveFileBytes[StaticOffsets.Main.GreenHill];
            SavedValues.KartS = saveFileBytes[StaticOffsets.Main.KartSonic];
            SavedValues.KartSh = saveFileBytes[StaticOffsets.Main.KartShadow];
            SavedValues.KartT = saveFileBytes[StaticOffsets.Main.KartTails];
            SavedValues.KartE = saveFileBytes[StaticOffsets.Main.KartEggman];
            SavedValues.KartK = saveFileBytes[StaticOffsets.Main.KartKnuckles];
            SavedValues.KartR = saveFileBytes[StaticOffsets.Main.KartRouge];
        }

        /// <summary>
        /// Read mission data from the savefile
        /// </summary>
        private void ReadMissionData() {
            // Loop through each level
            foreach (KeyValuePair<string, int> keyValuePair in StaticOffsets.Missions.StartingOffsets) {
                LevelValues level = new LevelValues(keyValuePair.Key);
                int levelOffset = keyValuePair.Value;

                // Loop through each mission
                for (int m = 0; m < 5; m++) {
                    MissionValues mission = new MissionValues(m + 1);

                    // Read mission grade
                    mission.Grade = saveFileBytes[levelOffset + StaticOffsets.Missions.InternalOffsets.Grades[m]];

                    // Read number of times played
                    mission.Plays = saveFileBytes[levelOffset + StaticOffsets.Missions.InternalOffsets.Plays[m]];

                    // Loop through each of the three high scores
                    for (int h = 0; h < 3; h++) {
                        MissionHighScore highScore = new MissionHighScore(h);
                        // Read mission time
                        TimeSpan missionTime = TimeSpan.Zero;
                        missionTime.Add(TimeSpan.FromMinutes(saveFileBytes[levelOffset + StaticOffsets.Missions.InternalOffsets.Time[m]]));
                        missionTime.Add(TimeSpan.FromSeconds(saveFileBytes[levelOffset + StaticOffsets.Missions.InternalOffsets.Time[m] + 0x01]));
                        // The fractions of a second are stored as 1/100 of one second
                        missionTime.Add(TimeSpan.FromSeconds(saveFileBytes[levelOffset + StaticOffsets.Missions.InternalOffsets.Time[m] + 0x02] / 100));

                        mission.Time = missionTime;

                        // Read rings (Missions 1, 4, and 5)
                        mission.Rings = saveFileBytes[levelOffset + StaticOffsets.Missions.InternalOffsets.Rings[m]];

                        // Read score (Missions 1, 4, and 5)
                        mission.Score = saveFileBytes[levelOffset + StaticOffsets.Missions.InternalOffsets.Score[m]];
                    }

                }
                SavedValues.LevelList.Add(level);
            }
        }

        private MissionHighScore ReadHighScore(int levelOffset, int highScore) {
            MissionHighScore score = new MissionHighScore(highScore);
            int scoreOffset = levelOffset;
            scoreOffset += 0x10;
            scoreOffset += highScore*12;

            // Read rings (Missions 1, 4, and 5)
            score.Rings = saveFileBytes[scoreOffset + 0x00];

            // Read score (Missions 1, 4, and 5)
            score.Score = saveFileBytes[scoreOffset + 0x04];

            // Read mission time
            TimeSpan missionTime = TimeSpan.Zero;
            missionTime.Add(TimeSpan.FromMinutes(saveFileBytes[scoreOffset + 0x08]));
            missionTime.Add(TimeSpan.FromSeconds(saveFileBytes[scoreOffset + 0x09]));
            // The fractions of a second are stored as 1/100 of one second
            missionTime.Add(TimeSpan.FromSeconds(saveFileBytes[scoreOffset + 0x0A] / 100));

            score.Time = missionTime;

            return score;
        
        }

        /// <summary>
        /// Reads two bytes (16 bits) from the save file
        /// </summary>
        /// <param name="offset">The number of bytes to skip before reading</param>
        /// <param name="bigEndian">When true, the value will be read as Big Endian</param>
        /// <returns>The 2 byte integer</returns>
        private Int16 ReadInt16(int offset = 0, bool bigEndian = false) {
            byte[] bytes = saveFileBytes.Skip(offset).Take(2).ToArray();
            DebugWrite("Reading the following bytes: " + BitConverter.ToString(bytes));
            if (BitConverter.IsLittleEndian != bigEndian) {
                return BitConverter.ToInt16(bytes.Reverse().ToArray(), 0);
            }
            return BitConverter.ToInt16(bytes, 0);
        }


        /// <summary>
        /// Reads four bytes (32 bits) from the save file
        /// </summary>
        /// <param name="offset">The number of bytes to skip before reading</param>
        /// <param name="bigEndian">When true, the value will be read as Big Endian</param>
        /// <returns>The 4 byte integer</returns>
        private Int32 ReadInt32(int offset = 0, bool bigEndian = false) {
            byte[] bytes = saveFileBytes.Skip(offset).Take(4).ToArray();
            DebugWrite("Reading the following bytes: " + BitConverter.ToString(bytes));
            if (BitConverter.IsLittleEndian != bigEndian) {
                return BitConverter.ToInt32(bytes.Reverse().ToArray(), 0);
            }
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// Reads eight bytes (64 bits) from the save file
        /// </summary>
        /// <param name="offset">The number of bytes to skip before reading</param>
        /// <param name="bigEndian">When true, the value will be read as Big Endian</param>
        /// <returns>The 8 byte integer</returns>
        private Int64 ReadInt64(int offset = 0, bool bigEndian = false) {
            byte[] bytes = saveFileBytes.Skip(offset).Take(8).ToArray();
            DebugWrite("Reading the following bytes: " + BitConverter.ToString(bytes));
            if (BitConverter.IsLittleEndian != bigEndian) {
                return BitConverter.ToInt64(bytes.Reverse().ToArray(), 0);
            }
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// Reads a string from the save file
        /// </summary>
        /// <param name="length">The length of the string in number of bytes</param>
        /// <param name="offset">The number of bytes to skip before reading</param>
        /// <returns>The string</returns>
        private string ReadString(int length, int offset = 0) {
            return Encoding.UTF8.GetString(saveFileBytes.Skip(offset).Take(length).ToArray());
        }

        /// <summary>
        /// Reads four bytes from the save file and tries to convert the result into a TimeSpan
        /// Big endian is checked if the result is negative
        /// </summary>
        /// <param name="offset">The number of bytes to skip before reading</param>
        /// <returns>A TimeSpan object</returns>
        private TimeSpan ReadTime(int offset = 0) {
            // Time is stored in frames, 1/60 of a second
            int frames = ReadInt32(offset, false);
            TimeSpan time = TimeSpan.FromSeconds(frames / 60);
            if (time.CompareTo(TimeSpan.Zero) < 0) {
                DebugWrite("Time was negative: " + (int)time.TotalHours + ":" + time.Minutes + ":" + time.Seconds);
                frames = ReadInt32(offset, true);
                time = TimeSpan.FromSeconds(frames / 60);
            }
            DebugWrite("Time: " + (int)time.TotalHours + ":" + time.Minutes + ":" + time.Seconds);
            return time;
        }

        /// <summary>
        /// Writes a message to the console based on a debug flag
        /// </summary>
        /// <param name="message">The message to write</param>
        private void DebugWrite(string message) {
            if (!DebugLogs) { return; }
            Debug.WriteLine(message);
        }

    }

    public enum SaveType {
        PC,
        GAMECUBE,
        RTE,
        SA,
        XBOX,
        PLAYSTATION
    }

}
