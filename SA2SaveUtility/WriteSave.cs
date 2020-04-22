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
            saveFileBytes = Main.loadedSave;
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
        /// Read data from the save file that is the same across systems
        /// </summary>
        private void WriteDeviceAgnosticData() {
            WriteString("SONIC ADVENTURE 2 BATTLE        EMBLEM: ");
            WriteString(SavedValues.FileTitle, 0x27);
            int frames = (int)(SavedValues.PlayTimeSpan.TotalSeconds * 60);
            WriteInt(frames, StaticOffsets.Main.PlayTime);

            int emblemFrames = (int)(SavedValues.EmblemResultsTimeSpan.TotalSeconds * 60);
            WriteInt(emblemFrames, StaticOffsets.Main.EmblemResultsTime);


            WriteInt(SavedValues.Lives, StaticOffsets.Main.Lives, 2);
            WriteInt(SavedValues.Rings, StaticOffsets.Main.Rings);
            WriteInt(SavedValues.TextLanguage, StaticOffsets.Main.TextLanguage, 1);
            WriteInt(SavedValues.VoiceLanguage, StaticOffsets.Main.VoiceLanguage, 1);

            WriteInt(SavedValues.SonicCW, StaticOffsets.Unlocks.Chao.Characters.Sonic, 1);
            WriteInt(SavedValues.TailsCW, StaticOffsets.Unlocks.Chao.Characters.Tails, 1);
            WriteInt(SavedValues.KnucklesCW, StaticOffsets.Unlocks.Chao.Characters.Knuckles, 1);
            WriteInt(SavedValues.ShadowCW, StaticOffsets.Unlocks.Chao.Characters.Shadow, 1);
            WriteInt(SavedValues.EggmanCW, StaticOffsets.Unlocks.Chao.Characters.Eggman, 1);
            WriteInt(SavedValues.RougeCW, StaticOffsets.Unlocks.Chao.Characters.Rouge, 1);
            WriteInt(SavedValues.SonicLS, StaticOffsets.Unlocks.Sonic.LightShoes, 1);
            WriteInt(SavedValues.SonicAL, StaticOffsets.Unlocks.Sonic.AncientLight, 1);
            WriteInt(SavedValues.SonicMG, StaticOffsets.Unlocks.Sonic.Magic, 1);
            WriteInt(SavedValues.SonicFR, StaticOffsets.Unlocks.Sonic.Flame, 1);
            WriteInt(SavedValues.SonicBB, StaticOffsets.Unlocks.Sonic.Bounce, 1);
            WriteInt(SavedValues.SonicMM, StaticOffsets.Unlocks.Sonic.MM, 1);
            WriteInt(SavedValues.TailsBo, StaticOffsets.Unlocks.Tails.Booster, 1);
            WriteInt(SavedValues.TailsBa, StaticOffsets.Unlocks.Tails.Bazooka, 1);
            WriteInt(SavedValues.TailsL, StaticOffsets.Unlocks.Tails.Laser, 1);
            WriteInt(SavedValues.TailsMM, StaticOffsets.Unlocks.Tails.MM, 1);
            WriteInt(SavedValues.KnucklesSC, StaticOffsets.Unlocks.Knuckles.Shovel, 1);
            WriteInt(SavedValues.KnucklesS, StaticOffsets.Unlocks.Knuckles.Sun, 1);
            WriteInt(SavedValues.KnucklesHG, StaticOffsets.Unlocks.Knuckles.Hammer, 1);
            WriteInt(SavedValues.KnucklesAN, StaticOffsets.Unlocks.Knuckles.Air, 1);
            WriteInt(SavedValues.KnucklesMM, StaticOffsets.Unlocks.Knuckles.MM, 1);
            WriteInt(SavedValues.ShadowAS, StaticOffsets.Unlocks.Shadow.Air, 1);
            WriteInt(SavedValues.ShadowAL, StaticOffsets.Unlocks.Shadow.AncientLight, 1);
            WriteInt(SavedValues.ShadowFR, StaticOffsets.Unlocks.Shadow.Flame, 1);
            WriteInt(SavedValues.ShadowMM, StaticOffsets.Unlocks.Shadow.MM, 1);
            WriteInt(SavedValues.EggmanJE, StaticOffsets.Unlocks.Eggman.Jet, 1);
            WriteInt(SavedValues.EggmanLC, StaticOffsets.Unlocks.Eggman.Cannon, 1);
            WriteInt(SavedValues.EggmanLB, StaticOffsets.Unlocks.Eggman.Laser, 1);
            WriteInt(SavedValues.EggmanPA, StaticOffsets.Unlocks.Eggman.Armor, 1);
            WriteInt(SavedValues.EggmanMM, StaticOffsets.Unlocks.Eggman.MM, 1);
            WriteInt(SavedValues.RougePN, StaticOffsets.Unlocks.Rouge.Pick, 1);
            WriteInt(SavedValues.RougeTS, StaticOffsets.Unlocks.Rouge.Treasure, 1);
            WriteInt(SavedValues.RougeIB, StaticOffsets.Unlocks.Rouge.Boots, 1);
            WriteInt(SavedValues.RougeMM, StaticOffsets.Unlocks.Rouge.MM, 1);
            WriteInt(SavedValues.KarateB, StaticOffsets.Emblems.Chao.Karate.Beginner, 1);
            WriteInt(SavedValues.KarateS, StaticOffsets.Emblems.Chao.Karate.Standard, 1);
            WriteInt(SavedValues.KarateE, StaticOffsets.Emblems.Chao.Karate.Expert, 1);
            WriteInt(SavedValues.KarateSu, StaticOffsets.Emblems.Chao.Karate.Super, 1);
            WriteInt(SavedValues.RaceB, StaticOffsets.Emblems.Chao.Race.Beginner, 1);
            WriteInt(SavedValues.RaceJ, StaticOffsets.Emblems.Chao.Race.Jewel, 1);
            WriteInt(SavedValues.RaceC, StaticOffsets.Emblems.Chao.Race.Challenge, 1);
            WriteInt(SavedValues.RaceH, StaticOffsets.Emblems.Chao.Race.Hero, 1);
            WriteInt(SavedValues.RaceD, StaticOffsets.Emblems.Chao.Race.Dark, 1);
            WriteInt(SavedValues.ThemeA, StaticOffsets.Unlocks.Themes.Amy, 1);
            WriteInt(SavedValues.ThemeM, StaticOffsets.Unlocks.Themes.Maria, 1);
            WriteInt(SavedValues.ThemeS, StaticOffsets.Unlocks.Themes.Secretary, 1);
            WriteInt(SavedValues.ThemeO, StaticOffsets.Unlocks.Themes.Omochao, 1);
            WriteInt(SavedValues.GreenH, StaticOffsets.Main.GreenHill, 1);
            WriteInt(SavedValues.KartS, StaticOffsets.Unlocks.Kart.KartSonic, 1);
            WriteInt(SavedValues.KartSh, StaticOffsets.Unlocks.Kart.KartShadow, 1);
            WriteInt(SavedValues.KartT, StaticOffsets.Unlocks.Kart.KartTails, 1);
            WriteInt(SavedValues.KartE, StaticOffsets.Unlocks.Kart.KartEggman, 1);
            WriteInt(SavedValues.KartK, StaticOffsets.Unlocks.Kart.KartKnuckles, 1);
            WriteInt(SavedValues.KartR, StaticOffsets.Unlocks.Kart.KartRouge, 1);
            WriteInt(SavedValues.ARankSonic, StaticOffsets.Emblems.AllARanks.Sonic, 1);
            WriteInt(SavedValues.ARankShadow, StaticOffsets.Emblems.AllARanks.Shadow, 1);
            WriteInt(SavedValues.ARankTails, StaticOffsets.Emblems.AllARanks.Tails, 1);
            WriteInt(SavedValues.ARankEggman, StaticOffsets.Emblems.AllARanks.Eggman, 1);
            WriteInt(SavedValues.ARankKnuckles, StaticOffsets.Emblems.AllARanks.Knuckles, 1);
            WriteInt(SavedValues.ARankRouge, StaticOffsets.Emblems.AllARanks.Rouge, 1);
        }

        /// <summary>
        /// Read mission data from the savefile
        /// </summary>
        private void WriteMissionData() {
            // Loop through each level
            foreach (LevelValues level in SavedValues.LevelList) {
                int levelOffset = StaticOffsets.Missions.StartingOffsets.Where(x => x.Key == level.LevelName).First().Value;
                DebugWrite("Writing " + level.LevelName + " at offset 0x" + levelOffset.ToString("X4"));
                foreach (MissionValues mission in level.Missions) {
                    WriteInt(mission.Grade, levelOffset + StaticOffsets.Missions.InternalOffsets.Grades[mission.Number], 1);
                    WriteInt(mission.Plays, levelOffset + StaticOffsets.Missions.InternalOffsets.Plays[mission.Number], 1);
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

            // Write rings
            WriteInt(highScore.Rings, startOffset + 0x00);

            // Write score
            WriteInt(highScore.Score, startOffset + 0x04);

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
                DebugWrite("Writing " + kartRace.RaceName + " at offset 0x" + levelOffset.ToString("X4"));
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
                DebugWrite("Writing " + bossAttack.BossName + " at offset 0x" + levelOffset.ToString("X4"));
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

        /// <summary>
        /// Writes a message to the console based on a debug flag
        /// </summary>
        /// <param name="message">The message to write</param>
        private void DebugWrite(string message) {
            if (!DebugLogs) { return; }
            Debug.WriteLine(message);
        }

    }

}
