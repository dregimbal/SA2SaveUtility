using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SA2SaveUtility {
    public class ReadSave {
        public bool DebugLogs = true;
        public SaveType FromSaveType = SaveType.GAMECUBE;
        public bool IsChaoSave = false;
        private byte[] saveFileBytes = new byte[0];
        private SavedValues SavedValues;

        public void InjestSaveFile(byte[] saveFile) {
            SavedValues = new SavedValues();
            saveFileBytes = saveFile;
            Debug.WriteLine("Save file byte[] length: 0x" + saveFileBytes.Length.ToString("x4"));
            DebugWrite("From save type: " + FromSaveType);
            if (saveFileBytes.Length <= 0) {
                Debug.WriteLine("Save file byte[] length <= 0");
            }

            VerifySaveFileType();
            CorrectCustomOffsets();

            ReadDeviceSpecificData();
            ReadDeviceAgnosticData();
            ReadMissionData();
            ReadKartData();
            ReadBossData();
        }

        public SavedValues GetSavedValues() {
            return SavedValues;
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
            } else {
            }

            if (Main.ReadSave.FromSaveType == SaveType.GAMECUBE) {
                SavedValues.Lives = ReadUInt16BE(StaticOffsets.Main.Lives);
                SavedValues.Rings = ReadUInt32BE(StaticOffsets.Main.Rings);
                SavedValues.TextLanguage = saveFileBytes[StaticOffsets.GameCube.TextLanguage];
                SavedValues.VoiceLanguage = saveFileBytes[StaticOffsets.GameCube.VoiceLanguage];
                Debug.WriteLine("GC language int is " + SavedValues.TextLanguage);
                Debug.WriteLine("GC voice language int is " + SavedValues.VoiceLanguage);
            } else {
                SavedValues.Lives = ReadUInt16LE(StaticOffsets.Main.Lives);
                SavedValues.Rings = ReadUInt32LE(StaticOffsets.Main.Rings);
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
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            DebugWrite("File Title: " + rgx.Replace(SavedValues.FileTitle, String.Empty));

            SavedValues.PlayTimeSpan = ReadTime(StaticOffsets.Main.PlayTime);
            DebugWrite("playTime: " + (int)SavedValues.PlayTimeSpan.TotalHours + ":" + SavedValues.PlayTimeSpan.Minutes + ":" + SavedValues.PlayTimeSpan.Seconds);

            SavedValues.EmblemResultsTimeSpan = ReadTime(StaticOffsets.Main.EmblemResultsTime);
            DebugWrite("emblemTime: " + (int)SavedValues.EmblemResultsTimeSpan.TotalHours + ":" + SavedValues.EmblemResultsTimeSpan.Minutes + ":" + SavedValues.EmblemResultsTimeSpan.Seconds);


            SavedValues.SonicCW = saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Sonic];
            SavedValues.TailsCW = saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Tails];
            SavedValues.KnucklesCW = saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Knuckles];
            SavedValues.ShadowCW = saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Shadow];
            SavedValues.EggmanCW = saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Eggman];
            SavedValues.RougeCW = saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Rouge];
            SavedValues.SonicLS = saveFileBytes[StaticOffsets.Unlocks.Sonic.LightShoes];
            SavedValues.SonicAL = saveFileBytes[StaticOffsets.Unlocks.Sonic.AncientLight];
            SavedValues.SonicMG = saveFileBytes[StaticOffsets.Unlocks.Sonic.Magic];
            SavedValues.SonicFR = saveFileBytes[StaticOffsets.Unlocks.Sonic.Flame];
            SavedValues.SonicBB = saveFileBytes[StaticOffsets.Unlocks.Sonic.Bounce];
            SavedValues.SonicMM = saveFileBytes[StaticOffsets.Unlocks.Sonic.MM];
            SavedValues.TailsBo = saveFileBytes[StaticOffsets.Unlocks.Tails.Booster];
            SavedValues.TailsBa = saveFileBytes[StaticOffsets.Unlocks.Tails.Bazooka];
            SavedValues.TailsL = saveFileBytes[StaticOffsets.Unlocks.Tails.Laser];
            SavedValues.TailsMM = saveFileBytes[StaticOffsets.Unlocks.Tails.MM];
            SavedValues.KnucklesSC = saveFileBytes[StaticOffsets.Unlocks.Knuckles.Shovel];
            SavedValues.KnucklesS = saveFileBytes[StaticOffsets.Unlocks.Knuckles.Sun];
            SavedValues.KnucklesHG = saveFileBytes[StaticOffsets.Unlocks.Knuckles.Hammer];
            SavedValues.KnucklesAN = saveFileBytes[StaticOffsets.Unlocks.Knuckles.Air];
            SavedValues.KnucklesMM = saveFileBytes[StaticOffsets.Unlocks.Knuckles.MM];
            SavedValues.ShadowAS = saveFileBytes[StaticOffsets.Unlocks.Shadow.Air];
            SavedValues.ShadowAL = saveFileBytes[StaticOffsets.Unlocks.Shadow.AncientLight];
            SavedValues.ShadowFR = saveFileBytes[StaticOffsets.Unlocks.Shadow.Flame];
            SavedValues.ShadowMM = saveFileBytes[StaticOffsets.Unlocks.Shadow.MM];
            SavedValues.EggmanJE = saveFileBytes[StaticOffsets.Unlocks.Eggman.Jet];
            SavedValues.EggmanLC = saveFileBytes[StaticOffsets.Unlocks.Eggman.Cannon];
            SavedValues.EggmanLB = saveFileBytes[StaticOffsets.Unlocks.Eggman.Laser];
            SavedValues.EggmanPA = saveFileBytes[StaticOffsets.Unlocks.Eggman.Armor];
            SavedValues.EggmanMM = saveFileBytes[StaticOffsets.Unlocks.Eggman.MM];
            SavedValues.RougePN = saveFileBytes[StaticOffsets.Unlocks.Rouge.Pick];
            SavedValues.RougeTS = saveFileBytes[StaticOffsets.Unlocks.Rouge.Treasure];
            SavedValues.RougeIB = saveFileBytes[StaticOffsets.Unlocks.Rouge.Boots];
            SavedValues.RougeMM = saveFileBytes[StaticOffsets.Unlocks.Rouge.MM];
            SavedValues.KarateB = saveFileBytes[StaticOffsets.Emblems.Chao.Karate.Beginner];
            SavedValues.KarateS = saveFileBytes[StaticOffsets.Emblems.Chao.Karate.Standard];
            SavedValues.KarateE = saveFileBytes[StaticOffsets.Emblems.Chao.Karate.Expert];
            SavedValues.KarateSu = saveFileBytes[StaticOffsets.Emblems.Chao.Karate.Super];
            SavedValues.RaceB = saveFileBytes[StaticOffsets.Emblems.Chao.Race.Beginner];
            SavedValues.RaceJ = saveFileBytes[StaticOffsets.Emblems.Chao.Race.Jewel];
            SavedValues.RaceC = saveFileBytes[StaticOffsets.Emblems.Chao.Race.Challenge];
            SavedValues.RaceH = saveFileBytes[StaticOffsets.Emblems.Chao.Race.Hero];
            SavedValues.RaceD = saveFileBytes[StaticOffsets.Emblems.Chao.Race.Dark];
            SavedValues.ThemeA = saveFileBytes[StaticOffsets.Unlocks.Themes.Amy];
            SavedValues.ThemeM = saveFileBytes[StaticOffsets.Unlocks.Themes.Maria];
            SavedValues.ThemeS = saveFileBytes[StaticOffsets.Unlocks.Themes.Secretary];
            SavedValues.ThemeO = saveFileBytes[StaticOffsets.Unlocks.Themes.Omochao];
            SavedValues.GreenH = saveFileBytes[StaticOffsets.Main.GreenHill];
            SavedValues.KartS = saveFileBytes[StaticOffsets.Unlocks.Kart.KartSonic];
            SavedValues.KartSh = saveFileBytes[StaticOffsets.Unlocks.Kart.KartShadow];
            SavedValues.KartT = saveFileBytes[StaticOffsets.Unlocks.Kart.KartTails];
            SavedValues.KartE = saveFileBytes[StaticOffsets.Unlocks.Kart.KartEggman];
            SavedValues.KartK = saveFileBytes[StaticOffsets.Unlocks.Kart.KartKnuckles];
            SavedValues.KartR = saveFileBytes[StaticOffsets.Unlocks.Kart.KartRouge];
            SavedValues.ARankSonic = saveFileBytes[StaticOffsets.Emblems.AllARanks.Sonic];
            SavedValues.ARankShadow = saveFileBytes[StaticOffsets.Emblems.AllARanks.Shadow];
            SavedValues.ARankTails = saveFileBytes[StaticOffsets.Emblems.AllARanks.Tails];
            SavedValues.ARankEggman = saveFileBytes[StaticOffsets.Emblems.AllARanks.Eggman];
            SavedValues.ARankKnuckles = saveFileBytes[StaticOffsets.Emblems.AllARanks.Knuckles];
            SavedValues.ARankRouge = saveFileBytes[StaticOffsets.Emblems.AllARanks.Rouge];
        }

        /// <summary>
        /// Read mission data from the savefile
        /// </summary>
        private void ReadMissionData() {
            // Loop through each level
            foreach (KeyValuePair<string, int> keyValuePair in StaticOffsets.Missions.StartingOffsets) {
                LevelValues level = new LevelValues(keyValuePair.Key);
                int levelOffset = keyValuePair.Value;

                // Loop through each of the 5 missions
                for (int m = 0; m < 5; m++) {
                    DebugWriteOffset("\nReading level " + level.LevelName + " M#" + m, levelOffset);
                    MissionValues mission = new MissionValues(m);

                    // Read mission grade
                    int gradeOffset = levelOffset + m;
                    mission.Grade = saveFileBytes[gradeOffset];
                    DebugWriteOffset("Grade: " + mission.Grade, gradeOffset);

                    // Read number of times played
                    if (FromSaveType == SaveType.GAMECUBE) {
                        mission.Plays = ReadUInt16BE(levelOffset + 0x06 + (0x02 * m));
                    } else {
                        mission.Plays = ReadUInt16LE(levelOffset + 0x06 + (0x02 * m));
                    }
                    if (mission.Plays != 0) {
                        DebugWriteOffset("Plays: " + mission.Plays + " ("+ mission.Plays.ToString("X4") + ")", levelOffset + 0x06 + (0x02 * m));
                    }
                    // Loop through each of the three high scores
                    for (int h = 0; h < 3; h++) {
                        mission.HighScores.Add(ReadHighScore(levelOffset, m, h));
                    }
                    level.Missions.Add(mission);
                }
                SavedValues.LevelList.Add(level);
            }
        }

        private MissionHighScore ReadHighScore(int levelOffset, int mission, int highScore) {
            MissionHighScore score = new MissionHighScore(highScore);
            int startOffset = levelOffset;

            // Grades and Plays is 16 bytes
            startOffset += 0x10;

            // 36 bytes per mission
            startOffset += mission * 0x24;

            // 12 bytes per score
            startOffset += highScore * 0x0C;

            // Read ring and score values
            // GC stores as big endian
            // PC stores as little endian
            if (FromSaveType == SaveType.GAMECUBE) {
                score.Rings = ReadUInt16BE(startOffset);
                score.Score = ReadUInt32BE(startOffset + 0x04);
            } else {
                score.Rings = ReadUInt16LE(startOffset);
                score.Score = ReadUInt32LE(startOffset + 0x04);
            }
            if (score.Rings != 0 || score.Score != 0) {
                DebugWriteOffset("Reading highscore #" + highScore + " - rings " + score.Rings, startOffset);
                DebugWriteOffset("Reading highscore #" + highScore + " - score " + score.Score, startOffset + 0x04);
            }

            // Read mission time
            int minutes = saveFileBytes[startOffset + 0x08];
            int seconds = saveFileBytes[startOffset + 0x09];
            int milliseconds = saveFileBytes[startOffset + 0x0A] * 10;

            TimeSpan highScoreTime = new TimeSpan(0, 0, minutes, seconds, milliseconds);

            score.Time = highScoreTime;

            return score;

        }

        private void ReadKartData() {
            // Loop through each kart race type
            foreach (KeyValuePair<string, int> keyValuePair in StaticOffsets.Karts.StartingOffsets) {
                KartRace race = new KartRace(keyValuePair.Key);
                int raceOffset = keyValuePair.Value;
                for (int i = 0; i < 3; i++) {
                    KartRaceHighScore score = new KartRaceHighScore();
                    score.Number = i;

                    // 4 bytes per high score entry
                    int timeOffset = raceOffset + 4 * i;

                    // First three bytes are the time
                    int minutes = saveFileBytes[timeOffset + 0x00];
                    int seconds = saveFileBytes[timeOffset + 0x01];
                    int milliseconds = saveFileBytes[timeOffset + 0x02] * 10;

                    TimeSpan raceTime = new TimeSpan(0, 0, minutes, seconds, milliseconds);
                    score.Time = raceTime;

                    // Fourth byte is the character who raced
                    score.Character = saveFileBytes[timeOffset + 0x03];

                    race.Scores.Add(score);
                }
                race.Emblem = saveFileBytes[raceOffset + 0x0C];
                SavedValues.KartRaces.Add(race);
            }
        }

        private void ReadBossData() {
            // Loop through each boss type
            foreach (KeyValuePair<string, int> keyValuePair in StaticOffsets.Boss.StartingOffsets) {
                BossAttack boss = new BossAttack(keyValuePair.Key);
                int attackOffset = keyValuePair.Value;
                boss.Emblem = saveFileBytes[attackOffset];
                for (int i = 0; i < 3; i++) {

                    // 4 bytes per high score entry
                    int timeOffset = attackOffset + 0x18 + 0x0C * i;

                    // Read boss time
                    int minutes = saveFileBytes[timeOffset + 0x00];
                    int seconds = saveFileBytes[timeOffset + 0x01];
                    int milliseconds = saveFileBytes[timeOffset + 0x02] * 10;

                    TimeSpan attackTime = new TimeSpan(0, 0, minutes, seconds, milliseconds);

                    boss.Times.Add(attackTime);
                }
                SavedValues.BossAttacks.Add(boss);
            }
        }

        /// <summary>
        /// Reads one byte (8 bits) from the save file
        /// </summary>
        /// <returns>The byte</returns>
        private byte ReadInt8(int offset = 0) {
            byte bytes = saveFileBytes.Skip(offset).Take(1).First();
            DebugWriteOffset("Int8 Reading " + bytes.ToString("X1"), offset);
            return bytes;
        }

        /// <summary>
        /// Reads two bytes (16 bits) from the save file
        /// </summary>
        /// <param name="offset">The number of bytes to skip before reading</param>
        /// <param name="bigEndian">When true, the value will be read as Big Endian</param>
        /// <returns>The 2 byte integer</returns>
        private Int16 ReadInt16(int offset = 0, bool bigEndian = false) {
            byte[] bytes;
            if (BitConverter.IsLittleEndian == bigEndian) {
                bytes = saveFileBytes.Skip(offset).Take(2).Reverse().ToArray();
            } else {
                bytes = saveFileBytes.Skip(offset).Take(2).ToArray();
            }
            if (BitConverter.ToInt16(bytes, 0) != 0) {
                DebugWriteOffset("Int16 Reading " + BitConverter.ToString(bytes) + " = Int16 " + BitConverter.ToInt16(bytes, 0) + " or UInt16 " + BitConverter.ToUInt16(bytes, 0), offset);
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
            byte[] bytes;
            if (BitConverter.IsLittleEndian == bigEndian) {
                bytes = saveFileBytes.Skip(offset).Take(4).Reverse().ToArray();
            } else {
                bytes = saveFileBytes.Skip(offset).Take(4).ToArray();
            }
            if (BitConverter.ToInt32(bytes, 0) != 0) {

                DebugWriteOffset("Int32 Reading " + BitConverter.ToString(bytes) + " = Int32 " + BitConverter.ToInt32(bytes, 0) + " or UInt32 " + BitConverter.ToUInt32(bytes, 0), offset);
            }
            return BitConverter.ToInt32(bytes, 0);
        }

        private UInt16 ReadUInt16BE(int offset = 0) {
            byte[] bytes = saveFileBytes.Skip(offset).Take(2).ToArray();
            if (BitConverter.IsLittleEndian) {
                bytes = bytes.Reverse().ToArray();
            }
            if (BitConverter.ToUInt16(bytes, 0) != 0) {
                DebugWriteOffset("UInt16BE Reading " + BitConverter.ToString(bytes) + " = UInt16 " + BitConverter.ToUInt16(bytes, 0) + " or Int16 " + BitConverter.ToInt16(bytes, 0), offset);
            }
            return BitConverter.ToUInt16(bytes, 0);
        }

        private UInt16 ReadUInt16LE(int offset = 0) {
            byte[] bytes = saveFileBytes.Skip(offset).Take(2).ToArray();
            if (!BitConverter.IsLittleEndian) {
                bytes = bytes.Reverse().ToArray();
            }
            if (BitConverter.ToUInt16(bytes, 0) != 0) {
                DebugWriteOffset("UInt16LE Reading " + BitConverter.ToString(bytes) + " = UInt16 " + BitConverter.ToUInt16(bytes, 0) + " or Int16 " + BitConverter.ToInt16(bytes, 0), offset);
            }
            return BitConverter.ToUInt16(bytes, 0);
        }

        private UInt32 ReadUInt32LE(int offset = 0) {
            byte[] bytes = saveFileBytes.Skip(offset).Take(4).ToArray();
            if (!BitConverter.IsLittleEndian) {
                bytes = bytes.Reverse().ToArray();
            }
            if (BitConverter.ToUInt32(bytes, 0) != 0) {
                DebugWriteOffset("UInt32LE Reading " + BitConverter.ToString(bytes) + " = UInt32 " + BitConverter.ToUInt32(bytes, 0) + " or Int32 " + BitConverter.ToInt32(bytes, 0), offset);
            }
            return BitConverter.ToUInt32(bytes, 0);
        }
        private UInt32 ReadUInt32BE(int offset = 0) {
            byte[] bytes = saveFileBytes.Skip(offset).Take(4).ToArray();
            if (BitConverter.IsLittleEndian) {
                bytes = bytes.Reverse().ToArray();
            }
            if (BitConverter.ToUInt32(bytes, 0) != 0) {

                DebugWriteOffset("UInt32BE Reading " + BitConverter.ToString(bytes) + " = UInt32 " + BitConverter.ToUInt32(bytes, 0) + " or Int32 " + BitConverter.ToInt32(bytes, 0), offset);
            }
            return BitConverter.ToUInt32(bytes, 0);
        }

        private UInt32 ReadUInt32(int offset = 0) {
            byte[] bytes;
            bytes = saveFileBytes.Skip(offset).Take(4).ToArray();
            if (BitConverter.ToUInt32(bytes, 0) != 0) {
                DebugWriteOffset("UInt32 Reading " + BitConverter.ToString(bytes) + " = UInt32 " + BitConverter.ToUInt32(bytes, 0) + " or Int32 " + BitConverter.ToInt32(bytes, 0), offset);
            }
            return BitConverter.ToUInt32(bytes, 0);
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
            UInt32 frames;

            if (FromSaveType == SaveType.GAMECUBE) {
                frames = ReadUInt32BE(offset);
            } else {
                frames = ReadUInt32LE(offset);
            }

            DebugWrite("Calculating time from " + frames + " frames");

            TimeSpan time = TimeSpan.FromSeconds(frames / 60);
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

        /// <summary>
        /// Writes a message to the console based on a debug flag
        /// Converts the offset to hex and adds 0x40 for gamecube saves
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="offset">The offset to include</param>
        private void DebugWriteOffset(string message, int offset) {
            if (!DebugLogs) { return; }

            if (FromSaveType == SaveType.GAMECUBE) {
                message += " - GC offset " + (offset + 0x40) + " (0x" + (offset + 0x40).ToString("X4") + ") - 0x" + saveFileBytes[offset].ToString("X4");
            } else {
                message += " - offset " + offset + " (0x" + offset.ToString("X4") + ") - 0x" + saveFileBytes[offset].ToString("X4");
            }
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
