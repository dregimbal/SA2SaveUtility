﻿using System.Collections.Generic;

namespace SA2SaveUtility
{
    class Offsets
    {
        public Save main = new Save();
        public Chao chao = new Chao();
        public Mission mission = new Mission();
    }

    class Save
    {
        public uint Rings = 0x2870;
        public uint ChaoWorldSonic = 0x5A69;
        public uint ChaoWorldTails = 0x5A6F;
        public uint ChaoWorldKnuckles = 0x5A6D;
        public uint ChaoWorldShadow = 0x5A6A;
        public uint ChaoWorldEggman = 0x5A70;
        public uint ChaoWorldRouge = 0x5A6E;

        public uint SonicLightShoes = 0x5A77;
        public uint SonicAncientLight = 0x5A78;
        public uint SonicMagic = 0x5A79;
        public uint SonicFlame = 0x5A7A;
        public uint SonicBounce = 0x5A7B;
        public uint SonicMM = 0x5A7C;

        public uint TailsBooster = 0x5A7D;
        public uint TailsBazooka = 0x5A7E;
        public uint TailsLaser = 0x5A7F;
        public uint TailsMM = 0x5A80;

        public uint KnucklesShovel = 0x5A81;
        public uint KnucklesSun = 0x5A82;
        public uint KnucklesHammer = 0x5A82;
        public uint KnucklesAir = 0x5A84;
        public uint KnucklesMM = 0x5A85;

        public Dictionary<string, uint> MissionOffsets = new Dictionary<string, uint>();
    }

    class Mission
    {
        public uint M1 = 0x00;
        public uint M2 = 0x01;
        public uint M3 = 0x02;
        public uint M4 = 0x03;
        public uint M5 = 0x04;
        public uint M1P = 0x06; //Max - 65535
        public uint M2P = 0x08;
        public uint M3P = 0x0A;
        public uint M4P = 0x0C;
        public uint M5P = 0x0E;
        public uint M1R = 0x10; //Max - 32767
        public uint M1S = 0x14; //Max - 99999999
        public uint M1T = 0x18;
        public uint M2R = 0x34;
        public uint M2S = 0x38;
        public uint M2T = 0x3C;
        public uint M3R = 0x58;
        public uint M3S = 0x5C;
        public uint M3T = 0x60;
        public uint M4R = 0x7C;
        public uint M4S = 0x80;
        public uint M4T = 0x84;
        public uint M5R = 0xA0;
        public uint M5S = 0xA4;
        public uint M5T = 0xA8;
    }

    class Chao
    {
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
