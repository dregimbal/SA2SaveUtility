﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SA2SaveUtility {
    public partial class uc_Main : UserControl {

        Offsets offsets = new Offsets();

        public uint mainIndex = 0;

        public int gcFileNo = 0;

        public uc_Main() {
            InitializeComponent();

            //Missions
            foreach (KeyValuePair<string, uint> keyValuePair in offsets.main.MissionOffsets) {
                uc_Mission uc = new uc_Mission();
                TabPage tp = new TabPage();
                uc.mainIndex = mainIndex;
                uc.currentPair = keyValuePair;
                tp.Controls.Add(uc);
                tp.Text = keyValuePair.Key;
                tc_Main.Controls[1].Controls.OfType<TabControl>().First().TabPages.Add(tp);
            }

            //Kart
            foreach (KeyValuePair<string, uint> keyValuePair in offsets.main.KartOffsets) {
                uc_Kart uc = new uc_Kart();
                TabPage tp = new TabPage();
                uc.mainIndex = mainIndex;
                uc.currentPair = keyValuePair;
                tp.Controls.Add(uc);
                tp.Text = keyValuePair.Key;
                tc_Main.Controls[1].Controls.OfType<TabControl>().First().TabPages.Add(tp);
            }

            //Boss
            foreach (KeyValuePair<string, KeyValuePair<uint, uint>> keyValuePair in offsets.main.BossOffsets) {
                uc_Boss uc = new uc_Boss();
                TabPage tp = new TabPage();
                uc.mainIndex = mainIndex;
                uc.currentPair = keyValuePair;
                tp.Controls.Add(uc);
                tp.Text = keyValuePair.Key;
                tc_Main.Controls[1].Controls.OfType<TabControl>().First().TabPages.Add(tp);
            }

            //Chao
            uc_MainChao ucMC = new uc_MainChao();
            TabPage tpMC = new TabPage();
            ucMC.mainIndex = mainIndex;
            tpMC.Controls.Add(ucMC);
            tpMC.Text = "Chao";
            tc_Main.Controls[1].Controls.OfType<TabControl>().First().TabPages.Add(tpMC);
        }

        private void Btn_UnlockAll_Click(object sender, EventArgs e) {
            byte[] emblem180 = new byte[12] { 0x31, 0x38, 0x30, 0x20, 0x45, 0x6D, 0x62, 0x6C, 0x65, 0x6D, 0x73, 0x00 };

            for (int i = 0; i < emblem180.Length; i++) {
                Main.WriteByte(0x27 + i, emblem180[i], mainIndex);
            }
            Main.WriteByte((int)offsets.main.EmblemCount, 0xB4, mainIndex);

            Main.WriteByte((int)offsets.main.AllASonic, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.AllAShadow, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.AllATails, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.AllAEggman, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.AllAKnuckles, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.AllARouge, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.AllACannonsCore, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.HeroStoryComplete, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.DarkStoryComplete, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.LastStoryComplete, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.BossAttackHero, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.BossAttackDark, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.BossAttackAll, 0x01, mainIndex);

            Main.WriteByte((int)offsets.main.GreenHill, 0x01, mainIndex);

            Main.WriteByte((int)offsets.main.KartSonic, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.KartTails, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.KartKnuckles, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.KartShadow, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.KartEggman, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.KartRouge, 0x01, mainIndex);

            foreach (KeyValuePair<string, uint> keyValuePair in MainSave.offsets.main.MissionOffsets) {
                Main.WriteByte((int)keyValuePair.Value, 0x05, mainIndex);
                Main.WriteByte((int)keyValuePair.Value + 0x01, 0x05, mainIndex);
                Main.WriteByte((int)keyValuePair.Value + 0x02, 0x05, mainIndex);
                Main.WriteByte((int)keyValuePair.Value + 0x03, 0x05, mainIndex);
                Main.WriteByte((int)keyValuePair.Value + 0x04, 0x05, mainIndex);
            }
            foreach (KeyValuePair<string, uint> keyValuePair in MainSave.offsets.main.KartOffsets) {
                Main.WriteByte((int)(keyValuePair.Value + offsets.kart.Emblem), 0x01, mainIndex);
            }
            foreach (KeyValuePair<string, KeyValuePair<uint, uint>> keyValuePair in MainSave.offsets.main.BossOffsets) {
                Main.WriteByte((int)(keyValuePair.Value.Key + offsets.boss.Emblem), 0x01, mainIndex);
            }

            Main.WriteByte((int)offsets.main.ChaoKarateBeginner, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.ChaoKarateStandard, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.ChaoKarateExpert, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.ChaoKarateSuper, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.ChaoRaceBeginner, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.ChaoRaceJewel, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.ChaoRaceChallenge, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.ChaoRaceHero, 0x01, mainIndex);
            Main.WriteByte((int)offsets.main.ChaoRaceDark, 0x01, mainIndex);

            Main.tc_Main.TabPages.Clear();
            MainSave.activeMain.Clear();
            MainSave.GetMain();
        }

        private void Nud_TotalRings_ValueChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteBytes((int)offsets.main.Rings, BitConverter.GetBytes((int)nud_TotalRings.Value), mainIndex, 4); } else { Memory.WriteBytesAtAddress((int)offsets.main.RingsRTE, BitConverter.GetBytes((int)nud_TotalRings.Value)); }
        }

        private void Checkb_CWSonic_CheckedChanged(object sender, EventArgs e) {
            Main.WriteByte((int)(offsets.main.ChaoWorldSonic), Convert.ToInt32(Convert.ToInt32(checkb_CWSonic.Checked)), mainIndex);
        }

        private void Checkb_CWTails_CheckedChanged(object sender, EventArgs e) {
            Main.WriteByte((int)(offsets.main.ChaoWorldTails), Convert.ToInt32(Convert.ToInt32(checkb_CWTails.Checked)), mainIndex);
        }

        private void Checkb_CWKnuckles_CheckedChanged(object sender, EventArgs e) {
            Main.WriteByte((int)(offsets.main.ChaoWorldKnuckles), Convert.ToInt32(Convert.ToInt32(checkb_CWKnuckles.Checked)), mainIndex);
        }

        private void Checkb_CWShadow_CheckedChanged(object sender, EventArgs e) {
            Main.WriteByte((int)(offsets.main.ChaoWorldShadow), Convert.ToInt32(Convert.ToInt32(checkb_CWShadow.Checked)), mainIndex);
        }

        private void Checkb_CWEggman_CheckedChanged(object sender, EventArgs e) {
            Main.WriteByte((int)(offsets.main.ChaoWorldEggman), Convert.ToInt32(Convert.ToInt32(checkb_CWEggman.Checked)), mainIndex);
        }

        private void Checkb_CWRouge_CheckedChanged(object sender, EventArgs e) {
            Main.WriteByte((int)(offsets.main.ChaoWorldRouge), Convert.ToInt32(Convert.ToInt32(checkb_CWRouge.Checked)), mainIndex);
        }

        private void Checkb_SonicMysticMelody_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.SonicMM, Convert.ToInt32(checkb_SonicMysticMelody.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.SonicMMRTE, (byte)(checkb_SonicMysticMelody.Checked ? 1 : 0)); }
        }

        private void Checkb_SonicBounceBracelet_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.SonicBounce, Convert.ToInt32(checkb_SonicBounceBracelet.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.SonicBounceRTE, (byte)(checkb_SonicBounceBracelet.Checked ? 1 : 0)); }
        }

        private void Checkb_SonicFlameRing_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.SonicFlame, Convert.ToInt32(checkb_SonicFlameRing.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.SonicFlameRTE, (byte)(checkb_SonicFlameRing.Checked ? 1 : 0)); }
        }

        private void Checkb_SonicMagicGloves_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.SonicMagic, Convert.ToInt32(checkb_SonicMagicGloves.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.SonicMagicRTE, (byte)(checkb_SonicMagicGloves.Checked ? 1 : 0)); }
        }

        private void Checkb_SonicAncientLight_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.SonicAncientLight, Convert.ToInt32(checkb_SonicAncientLight.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.SonicAncientLightRTE, (byte)(checkb_SonicAncientLight.Checked ? 1 : 0)); }
        }

        private void Checkb_SonicLightShoes_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.SonicLightShoes, Convert.ToInt32(checkb_SonicLightShoes.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.SonicLightShoesRTE, (byte)(checkb_SonicLightShoes.Checked ? 1 : 0)); }
        }

        private void Checkb_TailsMysticMelody_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.TailsMM, Convert.ToInt32(checkb_TailsMysticMelody.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.TailsMMRTE, (byte)(checkb_TailsMysticMelody.Checked ? 1 : 0)); }
        }

        private void Checkb_TailsLaserBlaster_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.TailsLaser, Convert.ToInt32(checkb_TailsLaserBlaster.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.TailsLaser, (byte)(checkb_TailsLaserBlaster.Checked ? 1 : 0)); }
        }

        private void Checkb_TailsBazooka_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.TailsBazooka, Convert.ToInt32(checkb_TailsBazooka.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.TailsBazookaRTE, (byte)(checkb_TailsBazooka.Checked ? 1 : 0)); }
        }

        private void Checkb_TailsBooster_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.TailsBooster, Convert.ToInt32(checkb_TailsBooster.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.TailsBoosterRTE, (byte)(checkb_TailsBooster.Checked ? 1 : 0)); }
        }

        private void Checkb_KnucklesMysticMelody_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KnucklesMM, Convert.ToInt32(checkb_KnucklesMysticMelody.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KnucklesMMRTE, (byte)(checkb_KnucklesMysticMelody.Checked ? 1 : 0)); }
        }

        private void Checkb_KnucklesAirNecklace_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KnucklesAir, Convert.ToInt32(checkb_KnucklesAirNecklace.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KnucklesAirRTE, (byte)(checkb_KnucklesAirNecklace.Checked ? 1 : 0)); }
        }

        private void Checkb_KnucklesHammerGloves_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KnucklesHammer, Convert.ToInt32(checkb_KnucklesHammerGloves.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KnucklesHammer, (byte)(checkb_KnucklesHammerGloves.Checked ? 1 : 0)); }
        }

        private void Checkb_KnucklesSunglasses_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KnucklesSun, Convert.ToInt32(checkb_KnucklesSunglasses.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KnucklesSunRTE, (byte)(checkb_KnucklesSunglasses.Checked ? 1 : 0)); }
        }

        private void Checkb_KnucklesShovelClaw_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KnucklesShovel, Convert.ToInt32(checkb_KnucklesShovelClaw.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KnucklesShovelRTE, (byte)(checkb_KnucklesShovelClaw.Checked ? 1 : 0)); }
        }

        private void Checkb_ShadowMysticMelody_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.ShadowMM, Convert.ToInt32(checkb_ShadowMysticMelody.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.ShadowMMRTE, (byte)(checkb_ShadowMysticMelody.Checked ? 1 : 0)); }
        }

        private void Checkb_ShadowFlameRing_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.ShadowFlame, Convert.ToInt32(checkb_ShadowFlameRing.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.ShadowFlameRTE, (byte)(checkb_ShadowFlameRing.Checked ? 1 : 0)); }
        }

        private void Checkb_ShadowAncientLight_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.ShadowAncientLight, Convert.ToInt32(checkb_ShadowAncientLight.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.ShadowAncientLightRTE, (byte)(checkb_ShadowAncientLight.Checked ? 1 : 0)); }
        }

        private void Checkb_ShadowAirShoes_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.ShadowAir, Convert.ToInt32(checkb_ShadowAirShoes.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.ShadowAirRTE, (byte)(checkb_ShadowAirShoes.Checked ? 1 : 0)); }
        }

        private void Checkb_EggmanMysticMelody_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.EggmanMM, Convert.ToInt32(checkb_EggmanMysticMelody.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.EggmanMMRTE, (byte)(checkb_EggmanMysticMelody.Checked ? 1 : 0)); }
        }

        private void Checkb_EggmanProtectiveArmor_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.EggmanArmor, Convert.ToInt32(checkb_EggmanProtectiveArmor.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.EggmanArmorRTE, (byte)(checkb_EggmanProtectiveArmor.Checked ? 1 : 0)); }
        }

        private void Checkb_EggmanLaserBlaster_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.EggmanLaser, Convert.ToInt32(checkb_EggmanLaserBlaster.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.EggmanLaserRTE, (byte)(checkb_EggmanLaserBlaster.Checked ? 1 : 0)); }
        }

        private void Checkb_EggmanLargeCannon_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.EggmanCannon, Convert.ToInt32(checkb_EggmanLargeCannon.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.EggmanCannonRTE, (byte)(checkb_EggmanLargeCannon.Checked ? 1 : 0)); }
        }

        private void Checkb_EggmanJetEngine_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.EggmanJet, Convert.ToInt32(checkb_EggmanJetEngine.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.EggmanJetRTE, (byte)(checkb_EggmanJetEngine.Checked ? 1 : 0)); }
        }

        private void Checkb_RougeMysticMelody_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.RougeMM, Convert.ToInt32(checkb_RougeMysticMelody.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.RougeMMRTE, (byte)(checkb_RougeMysticMelody.Checked ? 1 : 0)); }
        }

        private void Checkb_RougeIronBoots_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.RougeBoots, Convert.ToInt32(checkb_RougeIronBoots.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.RougeBootsRTE, (byte)(checkb_RougeIronBoots.Checked ? 1 : 0)); }
        }

        private void Checkb_RougeTreasureScope_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.RougeTreasure, Convert.ToInt32(checkb_RougeTreasureScope.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.RougeTreasureRTE, (byte)(checkb_RougeTreasureScope.Checked ? 1 : 0)); }
        }

        private void Checkb_RougePickNails_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.RougePick, Convert.ToInt32(checkb_RougePickNails.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.RougePickRTE, (byte)(checkb_RougePickNails.Checked ? 1 : 0)); }
        }

        private void Nud_Lives_ValueChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteBytes((int)offsets.main.Lives, BitConverter.GetBytes((short)nud_Lives.Value), mainIndex, 2); } else { Memory.WriteBytesAtAddress((int)offsets.main.LivesRTE, BitConverter.GetBytes((int)nud_Lives.Value)); }
        }

        private void Checkb_Secretary_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.ThemeSecretary, Convert.ToInt32(checkb_Secretary.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.ThemeSecretaryRTE, (byte)(checkb_Secretary.Checked ? 1 : 0)); }
        }

        private void Checkb_Amy_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.ThemeAmy, Convert.ToInt32(checkb_Amy.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.ThemeAmyRTE, (byte)(checkb_Amy.Checked ? 1 : 0)); }
        }

        private void Checkb_Omochao_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.ThemeOmochao, Convert.ToInt32(checkb_Omochao.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.ThemeOmochaoRTE, (byte)(checkb_Omochao.Checked ? 1 : 0)); }
        }

        private void Checkb_Maria_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.ThemeMaria, Convert.ToInt32(checkb_Maria.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.ThemeMariaRTE, (byte)(checkb_Maria.Checked ? 1 : 0)); }
        }

        private void Checkb_GreenHill_CheckedChanged(object sender, EventArgs e) {
            Main.WriteByte((int)offsets.main.GreenHill, Convert.ToInt32(checkb_GreenHill.Checked), mainIndex);
        }

        private void Checkb_KartSonic_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KartSonic, Convert.ToInt32(checkb_KartSonic.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KartSonicRTE, (byte)(checkb_KartSonic.Checked ? 1 : 0)); }
        }

        private void Checkb_KartTails_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KartTails, Convert.ToInt32(checkb_KartTails.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KartTailsRTE, (byte)(checkb_KartTails.Checked ? 1 : 0)); }
        }

        private void Checkb_KartKnuckles_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KartKnuckles, Convert.ToInt32(checkb_KartKnuckles.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KartKnucklesRTE, (byte)(checkb_KartKnuckles.Checked ? 1 : 0)); }
        }

        private void Checkb_KartShadow_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KartShadow, Convert.ToInt32(checkb_KartShadow.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KartShadowRTE, (byte)(checkb_KartShadow.Checked ? 1 : 0)); }
        }

        private void Checkb_KartEggman_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KartEggman, Convert.ToInt32(checkb_KartEggman.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KartEggmanRTE, (byte)(checkb_KartEggman.Checked ? 1 : 0)); }
        }

        private void Checkb_KartRouge_CheckedChanged(object sender, EventArgs e) {
            if (!Main.isRTE) { Main.WriteByte((int)offsets.main.KartRouge, Convert.ToInt32(checkb_KartRouge.Checked), mainIndex); } else { Memory.WriteByteAtAddress((int)offsets.main.KartRougeRTE, (byte)(checkb_KartRouge.Checked ? 1 : 0)); }
        }

        private void Nud_PlayHour_ValueChanged(object sender, EventArgs e) {
            int time = 0;
            time += (int)(nud_PlayHour.Value * 216000);
            time += (int)(nud_PlayMinute.Value * 3600);
            time += (int)(nud_PlaySecond.Value * 60);

            Main.WriteBytes((int)offsets.main.PlayTime, BitConverter.GetBytes(time), mainIndex, 4);
        }

        private void Nud_PlayMinute_ValueChanged(object sender, EventArgs e) {
            int time = 0;
            time += (int)(nud_PlayHour.Value * 216000);
            time += (int)(nud_PlayMinute.Value * 3600);
            time += (int)(nud_PlaySecond.Value * 60);

            Main.WriteBytes((int)offsets.main.PlayTime, BitConverter.GetBytes(time), mainIndex, 4);
        }

        private void Nud_PlaySecond_ValueChanged(object sender, EventArgs e) {
            int time = 0;
            time += (int)(nud_PlayHour.Value * 216000);
            time += (int)(nud_PlayMinute.Value * 3600);
            time += (int)(nud_PlaySecond.Value * 60);

            Main.WriteBytes((int)offsets.main.PlayTime, BitConverter.GetBytes(time), mainIndex, 4);
        }

        private void Nud_EmblemHour_ValueChanged(object sender, EventArgs e) {
            int time = 0;
            time += (int)(nud_EmblemHour.Value * 216000);
            time += (int)(nud_EmblemMinute.Value * 3600);
            time += (int)(nud_EmblemSecond.Value * 60);

            byte[] timeBytes = BitConverter.GetBytes(time);
            Array.Reverse(timeBytes);
            Main.WriteBytes((int)offsets.main.EmblemResultsTime, timeBytes, mainIndex, 4);
        }

        private void Nud_EmblemMinute_ValueChanged(object sender, EventArgs e) {
            int time = 0;
            time += (int)(nud_EmblemHour.Value * 216000);
            time += (int)(nud_EmblemMinute.Value * 3600);
            time += (int)(nud_EmblemSecond.Value * 60);

            byte[] timeBytes = BitConverter.GetBytes(time);
            Array.Reverse(timeBytes);
            Main.WriteBytes((int)offsets.main.EmblemResultsTime, timeBytes, mainIndex, 4);
        }

        private void Nud_EmblemSecond_ValueChanged(object sender, EventArgs e) {
            int time = 0;
            time += (int)(nud_EmblemHour.Value * 216000);
            time += (int)(nud_EmblemMinute.Value * 3600);
            time += (int)(nud_EmblemSecond.Value * 60);
            byte[] timeBytes = BitConverter.GetBytes(time);
            Array.Reverse(timeBytes);
            Main.WriteBytes((int)offsets.main.EmblemResultsTime, timeBytes, mainIndex, 4);
        }

        private void Nud_GCFileNumber_ValueChanged(object sender, EventArgs e) {
            gcFileNo = (int)nud_GCFileNumber.Value;
        }

        private void Cb_Text_SelectedIndexChanged(object sender, EventArgs e) {
            if (!Main.isRTE) {

                if (Main.isGC) {
                    Main.WriteByte((int)offsets.main.TextLanguageGC, cb_Text.SelectedIndex, mainIndex);
                } else {
                    Main.WriteByte((int)offsets.main.TextLanguage, cb_Text.SelectedIndex, mainIndex);
                }
            } else { Memory.WriteByteAtAddress((int)offsets.main.TextLanguageRTE, (byte)cb_Text.SelectedIndex); }
        }

        private void Cb_Voice_SelectedIndexChanged(object sender, EventArgs e) {
            if (!Main.isRTE) {
                if (Main.isGC) { Main.WriteByte((int)offsets.main.VoiceLanguageGC, cb_Voice.SelectedIndex, mainIndex); } else { Main.WriteByte((int)offsets.main.VoiceLanguage, cb_Voice.SelectedIndex, mainIndex); }
            } else { Memory.WriteByteAtAddress((int)offsets.main.VoiceLanguageRTE, (byte)cb_Voice.SelectedIndex); }
        }
    }
}
