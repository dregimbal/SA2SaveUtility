using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SA2SaveUtility {
    public class WriteSave {
        private bool DebugLogs = true;
        private string outputDir = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString() + @"\output";
        SaveType ToSaveType = SaveType.PC;
        private byte[] saveFileBytes;
        private SavedValues SavedValues;

        public void WriteSaveFile(SavedValues saveFile) {
            SavedValues = saveFile;
            // Make some space
            AllocateSaveFileByteArray();

            // Write the header and lead-in
            WriteDeviceSpecificData();
            WriteDeviceAgnosticData();

            // Write the mission data
            WriteMissionData();

            // Write the Kart data
            WriteKartData();

            // Write the Boss data
            WriteBossData();

            // Calculate and write the checksum
            WriteChecksum();

            // Write the save to a file
            CreateSaveFile();
        }

        private void AllocateSaveFileByteArray() {

            //Setup dialog OpenFileDialog for loading save file
            OpenFileDialog loadSave = new OpenFileDialog();
            loadSave.Filter = "Sonic Adventure 2 Main/Chao Save|*.*";
            loadSave.Title = "Load a template";

            if (loadSave.ShowDialog() == DialogResult.OK) {
                saveFileBytes = File.ReadAllBytes(loadSave.FileName);
            } else {
                saveFileBytes = Main.loadedSave;
            }
            //saveFileBytes = new byte[0x6000];
        }

        private void CreateSaveFile() {
            if (!Directory.Exists(outputDir)) {
                Directory.CreateDirectory(outputDir);
            }
            string fileName = outputDir + @"\SONIC2B__S01";
            int index = 1;
            while (true) {
                if (!File.Exists(fileName)) {
                    break;
                } else {
                    fileName = outputDir + @"\SONIC2B__S" + index.ToString("00");
                    index++;
                }
            }
            File.WriteAllBytes(fileName, saveFileBytes);
            MessageBox.Show("The save file has been written to " + fileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void WriteDeviceSpecificData() {
            if (ToSaveType == SaveType.GAMECUBE) {
                WriteUInt16BE(SavedValues.Lives, StaticOffsets.Main.Lives);
                WriteUInt32BE(SavedValues.Rings, StaticOffsets.Main.Rings);
                WriteInt(SavedValues.TextLanguage, StaticOffsets.GameCube.TextLanguage, 1);
                WriteInt(SavedValues.VoiceLanguage, StaticOffsets.GameCube.VoiceLanguage, 1);
            } else {
                WriteUInt16LE(SavedValues.Lives, StaticOffsets.Main.Lives);
                WriteUInt32LE(SavedValues.Rings, StaticOffsets.Main.Rings);
                WriteInt(SavedValues.TextLanguage, StaticOffsets.Main.TextLanguage, 1);
                WriteInt(SavedValues.VoiceLanguage, StaticOffsets.Main.VoiceLanguage, 1);

            }
        }

        /// <summary>
        /// Read data from the save file that is the same across systems
        /// </summary>
        private void WriteDeviceAgnosticData() {
            WriteString("SONIC ADVENTURE 2 BATTLE        EMBLEM: ");
            WriteString(SavedValues.FileTitle, 0x27);
            int frames = (int)(SavedValues.PlayTimeSpan.TotalSeconds * 60);
            WriteInt(frames, StaticOffsets.Main.PlayTime);

            int emblemFrames = (int)(SavedValues.EmblemResultsTimeSpan.TotalSeconds * 60);
            WriteInt(emblemFrames, StaticOffsets.Main.EmblemResultsTime);


            saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Sonic] = SavedValues.SonicCW;
            saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Tails] = SavedValues.TailsCW;
            saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Knuckles] = SavedValues.KnucklesCW;
            saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Shadow] = SavedValues.ShadowCW;
            saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Eggman] = SavedValues.EggmanCW;
            saveFileBytes[StaticOffsets.Unlocks.Chao.Characters.Rouge] = SavedValues.RougeCW;
            saveFileBytes[StaticOffsets.Unlocks.Sonic.LightShoes] = SavedValues.SonicLS;
            saveFileBytes[StaticOffsets.Unlocks.Sonic.AncientLight] = SavedValues.SonicAL;
            saveFileBytes[StaticOffsets.Unlocks.Sonic.Magic] = SavedValues.SonicMG;
            saveFileBytes[StaticOffsets.Unlocks.Sonic.Flame] = SavedValues.SonicFR;
            saveFileBytes[StaticOffsets.Unlocks.Sonic.Bounce] = SavedValues.SonicBB;
            saveFileBytes[StaticOffsets.Unlocks.Sonic.MM] = SavedValues.SonicMM;
            saveFileBytes[StaticOffsets.Unlocks.Tails.Booster] = SavedValues.TailsBo;
            saveFileBytes[StaticOffsets.Unlocks.Tails.Bazooka] = SavedValues.TailsBa;
            saveFileBytes[StaticOffsets.Unlocks.Tails.Laser] = SavedValues.TailsL;
            saveFileBytes[StaticOffsets.Unlocks.Tails.MM] = SavedValues.TailsMM;
            saveFileBytes[StaticOffsets.Unlocks.Knuckles.Shovel] = SavedValues.KnucklesSC;
            saveFileBytes[StaticOffsets.Unlocks.Knuckles.Sun] = SavedValues.KnucklesS;
            saveFileBytes[StaticOffsets.Unlocks.Knuckles.Hammer] = SavedValues.KnucklesHG;
            saveFileBytes[StaticOffsets.Unlocks.Knuckles.Air] = SavedValues.KnucklesAN;
            saveFileBytes[StaticOffsets.Unlocks.Knuckles.MM] = SavedValues.KnucklesMM;
            saveFileBytes[StaticOffsets.Unlocks.Shadow.Air] = SavedValues.ShadowAS;
            saveFileBytes[StaticOffsets.Unlocks.Shadow.AncientLight] = SavedValues.ShadowAL;
            saveFileBytes[StaticOffsets.Unlocks.Shadow.Flame] = SavedValues.ShadowFR;
            saveFileBytes[StaticOffsets.Unlocks.Shadow.MM] = SavedValues.ShadowMM;
            saveFileBytes[StaticOffsets.Unlocks.Eggman.Jet] = SavedValues.EggmanJE;
            saveFileBytes[StaticOffsets.Unlocks.Eggman.Cannon] = SavedValues.EggmanLC;
            saveFileBytes[StaticOffsets.Unlocks.Eggman.Laser] = SavedValues.EggmanLB;
            saveFileBytes[StaticOffsets.Unlocks.Eggman.Armor] = SavedValues.EggmanPA;
            saveFileBytes[StaticOffsets.Unlocks.Eggman.MM] = SavedValues.EggmanMM;
            saveFileBytes[StaticOffsets.Unlocks.Rouge.Pick] = SavedValues.RougePN;
            saveFileBytes[StaticOffsets.Unlocks.Rouge.Treasure] = SavedValues.RougeTS;
            saveFileBytes[StaticOffsets.Unlocks.Rouge.Boots] = SavedValues.RougeIB;
            saveFileBytes[StaticOffsets.Unlocks.Rouge.MM] = SavedValues.RougeMM;
            saveFileBytes[StaticOffsets.Emblems.Chao.Karate.Beginner] = SavedValues.KarateB;
            saveFileBytes[StaticOffsets.Emblems.Chao.Karate.Standard] = SavedValues.KarateS;
            saveFileBytes[StaticOffsets.Emblems.Chao.Karate.Expert] = SavedValues.KarateE;
            saveFileBytes[StaticOffsets.Emblems.Chao.Karate.Super] = SavedValues.KarateSu;
            saveFileBytes[StaticOffsets.Emblems.Chao.Race.Beginner] = SavedValues.RaceB;
            saveFileBytes[StaticOffsets.Emblems.Chao.Race.Jewel] = SavedValues.RaceJ;
            saveFileBytes[StaticOffsets.Emblems.Chao.Race.Challenge] = SavedValues.RaceC;
            saveFileBytes[StaticOffsets.Emblems.Chao.Race.Hero] = SavedValues.RaceH;
            saveFileBytes[StaticOffsets.Emblems.Chao.Race.Dark] = SavedValues.RaceD;
            saveFileBytes[StaticOffsets.Unlocks.Themes.Amy] = SavedValues.ThemeA;
            saveFileBytes[StaticOffsets.Unlocks.Themes.Maria] = SavedValues.ThemeM;
            saveFileBytes[StaticOffsets.Unlocks.Themes.Secretary] = SavedValues.ThemeS;
            saveFileBytes[StaticOffsets.Unlocks.Themes.Omochao] = SavedValues.ThemeO;
            saveFileBytes[StaticOffsets.Main.GreenHill] = SavedValues.GreenH;
            saveFileBytes[StaticOffsets.Unlocks.Kart.KartSonic] = SavedValues.KartS;
            saveFileBytes[StaticOffsets.Unlocks.Kart.KartShadow] = SavedValues.KartSh;
            saveFileBytes[StaticOffsets.Unlocks.Kart.KartTails] = SavedValues.KartT;
            saveFileBytes[StaticOffsets.Unlocks.Kart.KartEggman] = SavedValues.KartE;
            saveFileBytes[StaticOffsets.Unlocks.Kart.KartKnuckles] = SavedValues.KartK;
            saveFileBytes[StaticOffsets.Unlocks.Kart.KartRouge] = SavedValues.KartR;
            saveFileBytes[StaticOffsets.Emblems.AllARanks.Sonic] = SavedValues.ARankSonic;
            saveFileBytes[StaticOffsets.Emblems.AllARanks.Shadow] = SavedValues.ARankShadow;
            saveFileBytes[StaticOffsets.Emblems.AllARanks.Tails] = SavedValues.ARankTails;
            saveFileBytes[StaticOffsets.Emblems.AllARanks.Eggman] = SavedValues.ARankEggman;
            saveFileBytes[StaticOffsets.Emblems.AllARanks.Knuckles] = SavedValues.ARankKnuckles;
            saveFileBytes[StaticOffsets.Emblems.AllARanks.Rouge] = SavedValues.ARankRouge;

        }

        /// <summary>
        /// Read mission data from the savefile
        /// </summary>
        private void WriteMissionData() {
            // Loop through each level
            foreach (LevelValues level in SavedValues.LevelList) {
                int levelOffset = StaticOffsets.Missions.StartingOffsets.Where(x => x.Key == level.LevelName).First().Value;
                DebugWriteOffset("Writing level " + level.LevelName, levelOffset);
                foreach (MissionValues mission in level.Missions) {
                    // Grades are one byte
                    saveFileBytes[levelOffset + mission.Number] = mission.Grade;

                    DebugWriteOffset("Writing plays " + mission.Plays, levelOffset + 0x06 + (0x02 * mission.Number));
                    if (ToSaveType == SaveType.GAMECUBE) {
                        // Play count is Big Endian on GameCube
                        WriteUInt16BE(mission.Plays, levelOffset + 0x06 + (0x02 * mission.Number));
                    } else {
                        // Play count is Little Endian on PC
                        WriteUInt16LE(mission.Plays, levelOffset + 0x06 + (0x02 * mission.Number));
                    }

                    foreach (MissionHighScore highScore in mission.HighScores) {
                        WriteHighScore(levelOffset, mission.Number, highScore);
                    }
                }
            }
        }

        private void WriteHighScore(int levelOffset, int mission, MissionHighScore highScore) {
            int startOffset = levelOffset;

            // Grades and Plays is 16 bytes
            startOffset += 0x10;

            // 36 bytes per mission
            startOffset += mission * 0x24;

            // 12 bytes per score
            startOffset += highScore.Number * 0x0C;

            DebugWriteOffset("Writing rings " + highScore.Rings, levelOffset);
            // Write ring and score values
            // GC stores as big endian
            // PC stores as little endian
            if (ToSaveType == SaveType.GAMECUBE) {
                WriteUInt16BE(highScore.Rings, startOffset + 0x00);
                WriteUInt32BE(highScore.Score, startOffset + 0x04);
            } else {
                WriteUInt16LE(highScore.Rings, startOffset + 0x00);
                WriteUInt32LE(highScore.Score, startOffset + 0x04);
            }

            // Write time
            WriteTime(highScore.Time, startOffset + 0x08);

        }

        private void WriteTime(TimeSpan time, int offset = 0) {
            WriteInt(time.Minutes, offset + 0x00, 1);
            WriteInt(time.Seconds, offset + 0x01, 1);
            WriteInt((int)(time.Milliseconds / 10), offset + 0x02, 1);
        }

        private void WriteKartData() {
            foreach (KartRace kartRace in SavedValues.KartRaces) {
                int levelOffset = StaticOffsets.Karts.StartingOffsets.Where(x => x.Key == kartRace.RaceName).First().Value;
                DebugWriteOffset("Writing kart race " + kartRace.RaceName, levelOffset);
                WriteInt(kartRace.Emblem, levelOffset + 0x0C);
                foreach (KartRaceHighScore highScore in kartRace.Scores) {
                    int raceOffset = levelOffset + (highScore.Number * 0x04);
                    WriteTime(highScore.Time, raceOffset);
                    WriteInt(highScore.Character, levelOffset + 0x03, 1);
                }
            }
        }

        private void WriteBossData() {
            foreach (BossAttack bossAttack in SavedValues.BossAttacks) {
                int levelOffset = StaticOffsets.Boss.StartingOffsets.Where(x => x.Key == bossAttack.BossName).First().Value;
                DebugWriteOffset("Writing boss attack " + bossAttack.BossName, levelOffset);
                WriteInt(bossAttack.Emblem, levelOffset + 0x0C);
                for (int i = 0; i < bossAttack.Times.Count; i++) {
                    // 4 bytes per high score entry
                    int timeOffset = levelOffset + 0x18 + 0x0C * i;
                    WriteTime(bossAttack.Times[i], timeOffset);
                }
            }
        }

        private void WriteChecksum() {

            byte[] checksum = new byte[4];
            List<byte> newSave = new List<byte>();

            checksum = BitConverter.GetBytes(saveFileBytes.Skip(0x2844).ToArray().Select(x => (int)x).Sum());
            newSave.AddRange(saveFileBytes.Take(0x2840).ToArray());
            newSave.AddRange(checksum);
            newSave.AddRange(saveFileBytes.Skip(0x2844).ToArray());

            DebugWrite("Checksum value was " + BitConverter.ToString(saveFileBytes.Skip(0x2840).Take(4).ToArray()));
            DebugWrite("Checksum value is now " + BitConverter.ToString(checksum));

            saveFileBytes = newSave.ToArray();

        }

        private void WriteUInt16BE(UInt16 data, int offset = 0) {
            byte[] dataBytes = BitConverter.GetBytes(data);

            if (BitConverter.IsLittleEndian) {
                Array.Reverse(dataBytes);
            }
            for (int i = 0; i < 2; i++) {
                saveFileBytes[offset + i] = dataBytes[i];
            }
        }

        private void WriteUInt16LE(UInt16 data, int offset = 0) {
            byte[] dataBytes = BitConverter.GetBytes(data);

            if (!BitConverter.IsLittleEndian) {
                Array.Reverse(dataBytes);
            }
            for (int i = 0; i < 2; i++) {
                saveFileBytes[offset + i] = dataBytes[i];
            }
        }

        private void WriteUInt32BE(UInt32 data, int offset = 0) {
            byte[] dataBytes = BitConverter.GetBytes(data);

            if (BitConverter.IsLittleEndian) {
                Array.Reverse(dataBytes);
            }
            for (int i = 0; i < 4; i++) {
                saveFileBytes[offset + i] = dataBytes[i];
            }
        }

        private void WriteUInt32LE(UInt32 data, int offset = 0) {
            byte[] dataBytes = BitConverter.GetBytes(data);

            if (!BitConverter.IsLittleEndian) {
                Array.Reverse(dataBytes);
            }
            for (int i = 0; i < 4; i++) {
                saveFileBytes[offset + i] = dataBytes[i];
            }
        }

        private void WriteInt(int data, int offset = 0, int length = 4, bool bigEndian = false) {
            byte[] dataBytes = BitConverter.GetBytes(data);

            if (BitConverter.IsLittleEndian == bigEndian) {
                Array.Reverse(dataBytes);
            }
            for (int i = 0; i < length; i++) {
                saveFileBytes[offset + i] = dataBytes[i];
            }
        }

        private void WriteString(string data, int offset = 0, bool bigEndian = false) {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            if (BitConverter.IsLittleEndian == bigEndian) {
                Array.Reverse(dataBytes);
            }
            for (int i = 0; i < dataBytes.Length; i++) {
                saveFileBytes[offset + i] = dataBytes[i];
            }

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

            if (ToSaveType == SaveType.GAMECUBE) {
                message += " - GC offset " + (offset + 0x40) + " (0x" + (offset + 0x40).ToString("X4") + ")";
            } else {
                message += " - offset " + offset + " (0x" + offset.ToString("X4") + ")";
            }
            Debug.WriteLine(message);
        }

    }

}
