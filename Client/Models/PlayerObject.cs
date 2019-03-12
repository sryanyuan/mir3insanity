﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Controls;
using Client.Envir;
using Client.Scenes;
using Library;
using SlimDX;
using SlimDX.Direct3D9;
using Frame = Library.Frame;
using S = Library.Network.ServerPackets;

namespace Client.Models
{
    public class PlayerObject : MapObject
    {
        public override ObjectType Race => ObjectType.Player;

        public const int FemaleOffSet = 5000, AssassinOffSet = 50000, RightHandOffSet = 50;

        #region Weaopon Librarys
        public Dictionary<int, LibraryFile> WeaponList = new Dictionary<int, LibraryFile>
        {
            [0] = LibraryFile.M_Weapon1,
            [1] = LibraryFile.M_Weapon2,
            [2] = LibraryFile.M_Weapon3,
            [3] = LibraryFile.M_Weapon4,
            [4] = LibraryFile.M_Weapon5,
            [5] = LibraryFile.M_Weapon6,
            [6] = LibraryFile.M_Weapon7,
            [10] = LibraryFile.M_Weapon10,
            [11] = LibraryFile.M_Weapon11,
            [12] = LibraryFile.M_Weapon12,
            [13] = LibraryFile.M_Weapon13,
            [14] = LibraryFile.M_Weapon14,
            [15] = LibraryFile.M_Weapon15,
            [16] = LibraryFile.M_Weapon16,

            [0 + FemaleOffSet] = LibraryFile.WM_Weapon1,
            [1 + FemaleOffSet] = LibraryFile.WM_Weapon2,
            [2 + FemaleOffSet] = LibraryFile.WM_Weapon3,
            [3 + FemaleOffSet] = LibraryFile.WM_Weapon4,
            [4 + FemaleOffSet] = LibraryFile.WM_Weapon5,
            [5 + FemaleOffSet] = LibraryFile.WM_Weapon6,
            [6 + FemaleOffSet] = LibraryFile.WM_Weapon7,
            [10 + FemaleOffSet] = LibraryFile.WM_Weapon10,
            [11 + FemaleOffSet] = LibraryFile.WM_Weapon11,
            [12 + FemaleOffSet] = LibraryFile.WM_Weapon12,
            [13 + FemaleOffSet] = LibraryFile.WM_Weapon13,
            [14 + FemaleOffSet] = LibraryFile.WM_Weapon14,
            [15 + FemaleOffSet] = LibraryFile.WM_Weapon15,
            [16 + FemaleOffSet] = LibraryFile.WM_Weapon16,

            [120] = LibraryFile.M_WeaponADL1,
            [122] = LibraryFile.M_WeaponADL2,
            [126] = LibraryFile.M_WeaponADL6,
            [120 + RightHandOffSet] = LibraryFile.M_WeaponADR1,
            [122 + RightHandOffSet] = LibraryFile.M_WeaponADR2,
            [126 + RightHandOffSet] = LibraryFile.M_WeaponADR6,

            [110] = LibraryFile.M_WeaponAOH1,
            [111] = LibraryFile.M_WeaponAOH2,
            [112] = LibraryFile.M_WeaponAOH3,
            [113] = LibraryFile.M_WeaponAOH3,
            [114] = LibraryFile.M_WeaponAOH4,
            [115] = LibraryFile.M_WeaponAOH5,
            [116] = LibraryFile.M_WeaponAOH6,


            [120 + FemaleOffSet] = LibraryFile.WM_WeaponADL1,
            [122 + FemaleOffSet] = LibraryFile.WM_WeaponADL2,
            [126 + FemaleOffSet] = LibraryFile.WM_WeaponADL6,
            [120 + FemaleOffSet + RightHandOffSet] = LibraryFile.WM_WeaponADR1,
            [122 + FemaleOffSet + RightHandOffSet] = LibraryFile.WM_WeaponADR2,
            [126 + FemaleOffSet + RightHandOffSet] = LibraryFile.WM_WeaponADR6,

            [110 + FemaleOffSet] = LibraryFile.WM_WeaponAOH1,
            [111 + FemaleOffSet] = LibraryFile.WM_WeaponAOH2,
            [112 + FemaleOffSet] = LibraryFile.WM_WeaponAOH3,
            [113 + FemaleOffSet] = LibraryFile.WM_WeaponAOH3,
            [114 + FemaleOffSet] = LibraryFile.WM_WeaponAOH4,
            [115 + FemaleOffSet] = LibraryFile.WM_WeaponAOH5,
            [116 + FemaleOffSet] = LibraryFile.WM_WeaponAOH6,
        };
        #endregion

        #region Helmet Librarys
        public Dictionary<int, LibraryFile> HelmetList = new Dictionary<int, LibraryFile>
        {
            [0] = LibraryFile.M_Helmet1,
            [1] = LibraryFile.M_Helmet2,
            [2] = LibraryFile.M_Helmet3,
            [3] = LibraryFile.M_Helmet4,
            [4] = LibraryFile.M_Helmet5,

            [10] = LibraryFile.M_Helmet11,
            [11] = LibraryFile.M_Helmet12,
            [12] = LibraryFile.M_Helmet13,
            [13] = LibraryFile.M_Helmet14,

            [0 + FemaleOffSet] = LibraryFile.WM_Helmet1,
            [1 + FemaleOffSet] = LibraryFile.WM_Helmet2,
            [2 + FemaleOffSet] = LibraryFile.WM_Helmet3,
            [3 + FemaleOffSet] = LibraryFile.WM_Helmet4,
            [4 + FemaleOffSet] = LibraryFile.WM_Helmet5,

            [10 + FemaleOffSet] = LibraryFile.WM_Helmet11,
            [11 + FemaleOffSet] = LibraryFile.WM_Helmet12,
            [12 + FemaleOffSet] = LibraryFile.WM_Helmet13,
            [13 + FemaleOffSet] = LibraryFile.WM_Helmet14,

            [0 + AssassinOffSet] = LibraryFile.M_HelmetA1,
            [1 + AssassinOffSet] = LibraryFile.M_HelmetA2,
            [2 + AssassinOffSet] = LibraryFile.M_HelmetA3,
            [3 + AssassinOffSet] = LibraryFile.M_HelmetA4,

            [0 + AssassinOffSet + FemaleOffSet] = LibraryFile.WM_HelmetA1,
            [1 + AssassinOffSet + FemaleOffSet] = LibraryFile.WM_HelmetA2,
            [2 + AssassinOffSet + FemaleOffSet] = LibraryFile.WM_HelmetA3,
            [3 + AssassinOffSet + FemaleOffSet] = LibraryFile.WM_HelmetA4,
        };
        #endregion

        #region Armour Librarys
        public Dictionary<int, LibraryFile> ArmourList = new Dictionary<int, LibraryFile>
        {
            [0] = LibraryFile.M_Hum,
            [1] = LibraryFile.M_HumEx1,
            [2] = LibraryFile.M_HumEx2,
            [3] = LibraryFile.M_HumEx3,
            [4] = LibraryFile.M_HumEx4,
            [10] = LibraryFile.M_HumEx10,
            [11] = LibraryFile.M_HumEx11,
            [12] = LibraryFile.M_HumEx12,
            [13] = LibraryFile.M_HumEx13,


            [0 + FemaleOffSet] = LibraryFile.WM_Hum,
            [1 + FemaleOffSet] = LibraryFile.WM_HumEx1,
            [2 + FemaleOffSet] = LibraryFile.WM_HumEx2,
            [3 + FemaleOffSet] = LibraryFile.WM_HumEx3,
            [4 + FemaleOffSet] = LibraryFile.WM_HumEx4,
            [10 + FemaleOffSet] = LibraryFile.WM_HumEx10,
            [11 + FemaleOffSet] = LibraryFile.WM_HumEx11,
            [12 + FemaleOffSet] = LibraryFile.WM_HumEx12,
            [13 + FemaleOffSet] = LibraryFile.WM_HumEx13,


            [0 + AssassinOffSet] = LibraryFile.M_HumA,
            [1 + AssassinOffSet] = LibraryFile.M_HumAEx1,
            [2 + AssassinOffSet] = LibraryFile.M_HumAEx2,
            [3 + AssassinOffSet] = LibraryFile.M_HumAEx3,

            [0 + AssassinOffSet + FemaleOffSet] = LibraryFile.WM_HumA,
            [1 + AssassinOffSet + FemaleOffSet] = LibraryFile.WM_HumAEx1,
            [2 + AssassinOffSet + FemaleOffSet] = LibraryFile.WM_HumAEx2,
            [3 + AssassinOffSet + FemaleOffSet] = LibraryFile.WM_HumAEx3,
        };
        #endregion

        #region Shield Librarys
        public Dictionary<int, LibraryFile> ShieldList = new Dictionary<int, LibraryFile>
        {
            [0] = LibraryFile.M_Shield1,
            [1] = LibraryFile.M_Shield2,

            [0 + FemaleOffSet] = LibraryFile.WM_Shield1,
            [1 + FemaleOffSet] = LibraryFile.WM_Shield2,

            [100] = LibraryFile.EquipEffect_Part,

            [100 + FemaleOffSet] = LibraryFile.EquipEffect_Part,
        };
        #endregion


        public string GuildRank
        {
            get { return _GuildRank; }
            set
            {
                if (_GuildRank == value) return;

                _GuildRank = value;

                NameChanged();
            }
        }
        private string _GuildRank;


        public virtual MirClass Class { get; set; }

        public MirLibrary HairLibrary, HelmetLibrary;
        public int HairType;
        public Color HairColour;
        public int HairTypeOffSet;
        public int HelmetShape;

        public int HairFrame => DrawFrame + (HairType - 1) * HairTypeOffSet;
        public int HelmetFrame => DrawFrame + ((HelmetShape % 10) - 1) * ArmourShapeOffSet + ArmourShift;

        public MirLibrary WeaponLibrary1, WeaponLibrary2;
        public int WeaponShapeOffSet;
        public int WeaponShape, LibraryWeaponShape;
        public int WeaponFrame => DrawFrame + (WeaponShape % 10) * WeaponShapeOffSet;

        public MirLibrary BodyLibrary;
        public int ArmourShapeOffSet;
        public int ArmourShape;
        public int ArmourShift;
        public Color ArmourColour;
        public int ArmourFrame => DrawFrame + (ArmourShape % 11) * ArmourShapeOffSet + ArmourShift;

        public MirLibrary HorseLibrary, HorseShapeLibrary, HorseShapeLibrary2;
        public int HorseShape;
        public int HorseFrame => DrawFrame + ((int)Horse - 1) * 5000;
        public HorseType Horse;

        public MirLibrary ShieldLibrary;
        public int ShieldShape;
        public int ShieldFrame
        {
            get
            {
                if (ShieldShape < 1000)
                    return DrawFrame + ((ShieldShape % 10) - 1) * ArmourShapeOffSet + ArmourShift;
                else
                    return 900 + 200 * ((ShieldShape % 10) - 1) + 10 * (byte)Direction + (GameScene.Game.MapControl.Animation % 4);
            }
        }

        public MirLibrary EmblemLibrary;
        public int EmblemShape;

        public int ArmourImage, WeaponImage;
        public ItemEffect ArmourEffect;

        public bool DrawWeapon;


        public int CharacterIndex;

        public PlayerObject()
        {
            
        }
        public PlayerObject(S.ObjectPlayer info)
        {
            CharacterIndex = info.Index;

            ObjectID = info.ObjectID;

            Name = info.Name;
            NameColour = info.NameColour;

            Class = info.Class;
            Gender = info.Gender;

            Poison = info.Poison;

            foreach (BuffType type in info.Buffs)
                VisibleBuffs.Add(type);

            Title = info.GuildName;

            CurrentLocation = info.Location;
            Direction = info.Direction;
            
            HairType = info.HairType;
            HairColour = info.HairColour;

            ArmourShape = info.Armour;
            ArmourColour = info.ArmourColour;
            LibraryWeaponShape = info.Weapon;
            HorseShape = info.HorseShape;
            HelmetShape = info.Helmet;
            ShieldShape = info.Shield;
            EmblemShape = info.Emblem;

            ArmourImage = info.ArmourImage;
            ArmourEffect = info.ArmourEffect;
            WeaponImage = info.WeaponImage;


            Light = info.Light;

            Dead = info.Dead;
            Horse = info.Horse;

            UpdateLibraries();
            
            SetFrame(new ObjectAction(!Dead ? MirAction.Standing : MirAction.Dead, MirDirection.Up, CurrentLocation));

            GameScene.Game.MapControl.AddObject(this);
        }

        public void UpdateLibraries()
        {
            LibraryFile file;

            WeaponLibrary2 = null;

            Frames = new Dictionary<MirAnimation, Frame>(FrameSet.Players);

            CEnvir.LibraryList.TryGetValue(LibraryFile.Horse, out HorseLibrary);

            HorseShapeLibrary = null;
            HorseShapeLibrary2 = null;

            if (LibraryWeaponShape >= 1000)
                WeaponShape = LibraryWeaponShape - 1000;
            else
                WeaponShape = LibraryWeaponShape;


            switch (HorseShape)
            {
                case 1:
                    CEnvir.LibraryList.TryGetValue(LibraryFile.HorseIron, out HorseShapeLibrary);
                    break;
                case 2:
                    CEnvir.LibraryList.TryGetValue(LibraryFile.HorseSilver, out HorseShapeLibrary);
                    break;
                case 3:
                    CEnvir.LibraryList.TryGetValue(LibraryFile.HorseGold, out HorseShapeLibrary);
                    break;
                case 4:
                    CEnvir.LibraryList.TryGetValue(LibraryFile.HorseBlue, out HorseShapeLibrary);
                    break;
                case 5:
                    CEnvir.LibraryList.TryGetValue(LibraryFile.HorseDark, out HorseShapeLibrary);
                    CEnvir.LibraryList.TryGetValue(LibraryFile.HorseDarkEffect, out HorseShapeLibrary2);
                    break;
            }

            switch (EmblemShape)
            {
                case 1:
                case 2:
                    CEnvir.LibraryList.TryGetValue(LibraryFile.GameInter, out EmblemLibrary);
                    break;
                case 3:
                    CEnvir.LibraryList.TryGetValue(LibraryFile.GameInter2, out EmblemLibrary);
                    break;
            }

            switch (Class)
            {
                case MirClass.Warrior:
                case MirClass.Wizard:
                case MirClass.Taoist:
                    ArmourShapeOffSet = 5000;
                    WeaponShapeOffSet = 5000;
                    HairTypeOffSet = 5000;

                    switch (Gender)
                    {
                        case MirGender.Male:

                            if (!ArmourList.TryGetValue(ArmourShape / 11, out file))
                            {
                                file = LibraryFile.M_Hum;
                                ArmourShape = 0;
                            }

                            CEnvir.LibraryList.TryGetValue(file, out BodyLibrary);

                            CEnvir.LibraryList.TryGetValue(LibraryFile.M_Hair, out HairLibrary);


                            if (!HelmetList.TryGetValue(HelmetShape / 10, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out HelmetLibrary);


                            if (!WeaponList.TryGetValue(LibraryWeaponShape / 10, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out WeaponLibrary1);

                            if (!ShieldList.TryGetValue(ShieldShape / 10, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out ShieldLibrary);                            
                            break;
                        case MirGender.Female:
                            if (!ArmourList.TryGetValue(ArmourShape / 11 + FemaleOffSet, out file))
                            {
                                file = LibraryFile.WM_Hum;
                                ArmourShape = 0;
                            }

                            CEnvir.LibraryList.TryGetValue(file, out BodyLibrary);

                            CEnvir.LibraryList.TryGetValue(LibraryFile.WM_Hair, out HairLibrary);

                            if (!HelmetList.TryGetValue(HelmetShape / 10 + FemaleOffSet, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out HelmetLibrary);

                            if (!WeaponList.TryGetValue(LibraryWeaponShape / 10 + FemaleOffSet, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out WeaponLibrary1);

                            if (!ShieldList.TryGetValue(ShieldShape / 10 + FemaleOffSet, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out ShieldLibrary);
                            break;
                    }
                    break;
                case MirClass.Assassin:
                    ArmourShapeOffSet = 5000;
                    WeaponShapeOffSet = 5000;
                    HairTypeOffSet = 5000;

                    switch (Gender)
                    {
                        case MirGender.Male:
                            if (!ArmourList.TryGetValue(ArmourShape / 11, out file))
                            {
                                file = LibraryFile.M_Hum;
                                ArmourShape = 0;
                            }

                            CEnvir.LibraryList.TryGetValue(file, out BodyLibrary);

                            CEnvir.LibraryList.TryGetValue(LibraryFile.M_Hair, out HairLibrary);

                            if (!HelmetList.TryGetValue(HelmetShape / 10, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out HelmetLibrary);

                            if (!WeaponList.TryGetValue(LibraryWeaponShape / 10, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out WeaponLibrary1);

                            if (!ShieldList.TryGetValue(ShieldShape / 10, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out ShieldLibrary);

                            if (LibraryWeaponShape < 1200) break;

                            if (!WeaponList.TryGetValue(LibraryWeaponShape / 10 + RightHandOffSet, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out WeaponLibrary2);
                            break;
                        case MirGender.Female:
                            if (!ArmourList.TryGetValue(ArmourShape / 11 + FemaleOffSet, out file))
                            {
                                file = LibraryFile.WM_Hum;
                                ArmourShape = 0;
                            }

                            CEnvir.LibraryList.TryGetValue(file, out BodyLibrary);
                            CEnvir.LibraryList.TryGetValue(LibraryFile.WM_Hair, out HairLibrary);

                            if (!HelmetList.TryGetValue(HelmetShape / 10 + FemaleOffSet, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out HelmetLibrary);

                            if (!WeaponList.TryGetValue(LibraryWeaponShape / 10 + FemaleOffSet, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out WeaponLibrary1);

                            if (!ShieldList.TryGetValue(ShieldShape / 10 + FemaleOffSet, out file)) file = LibraryFile.None;
                            CEnvir.LibraryList.TryGetValue(file, out ShieldLibrary);

                            if (LibraryWeaponShape < 1200) break;

                            if (!WeaponList.TryGetValue(LibraryWeaponShape / 10 + FemaleOffSet + RightHandOffSet, out file)) file = LibraryFile.None;

                            CEnvir.LibraryList.TryGetValue(file, out WeaponLibrary2);
                            break;
                    }
                    break;
            }
            
        }

        public override void SetAnimation(ObjectAction action)
        {
            MirAnimation animation;
            DrawWeapon = true;
            MagicType type;
            switch (action.Action)
            {
                case MirAction.Standing:
                    //if(VisibleBuffs.Contains(BuffType.Stealth))
                    animation = MirAnimation.Standing;

                    if (CEnvir.Now < StanceTime)
                        animation = MirAnimation.Stance;

                    if (VisibleBuffs.Contains(BuffType.Cloak))
                        animation = MirAnimation.CreepStanding;

                    if (Horse != HorseType.None)
                        animation = MirAnimation.HorseStanding;

                    if (VisibleBuffs.Contains(BuffType.DragonRepulse))
                        animation = MirAnimation.DragonRepulseMiddle;
                    else if (CurrentAnimation == MirAnimation.DragonRepulseMiddle)
                        animation = MirAnimation.DragonRepulseEnd;

                    if (VisibleBuffs.Contains(BuffType.ElementalHurricane))
                        animation = MirAnimation.ChannellingMiddle;

                    break;
                case MirAction.Moving:
                    //if(VisibleBuffs.Contains(BuffType.Stealth))
                  
                    animation = MirAnimation.Walking;

                    if (Horse != HorseType.None)
                        animation = MirAnimation.HorseWalking;

                    if ((MagicType)action.Extra[1] == MagicType.ShoulderDash || (MagicType)action.Extra[1] == MagicType.Assault)
                        animation = MirAnimation.Combat8;
                    else if(VisibleBuffs.Contains(BuffType.Cloak))
                        animation = VisibleBuffs.Contains(BuffType.GhostWalk) ? MirAnimation.CreepWalkFast : MirAnimation.CreepWalkSlow;
                    else if ((int) action.Extra[0] >= 2)
                    {
                        animation = MirAnimation.Running;
                        if (Horse != HorseType.None)
                            animation = MirAnimation.HorseRunning;
                    }
                    break;
                case MirAction.Pushed:
                    animation = MirAnimation.Pushed;
                    break;
                case MirAction.Attack:
                    type = (MagicType)action.Extra[1];
                    animation = Functions.GetAttackAnimation(Class, LibraryWeaponShape, type);
                    break;
                case MirAction.Mining:
                    animation = Functions.GetAttackAnimation(Class, LibraryWeaponShape, MagicType.None);
                    break;
                case MirAction.RangeAttack:
                    animation = MirAnimation.Combat1;
                    break;
                case MirAction.Spell:
                    type = (MagicType) action.Extra[0];
                    
                    animation = Functions.GetMagicAnimation(type);

                    if (type == MagicType.PoisonousCloud)
                        DrawWeapon = false;

                    if (VisibleBuffs.Contains(BuffType.ElementalHurricane))
                        animation = MirAnimation.ChannellingEnd;
                    break;
                    // case MirAction.Struck:
                    //    animation = MirAnimation.Struck;
                    // if (Horse != HorseType.None)
                    //    animation = MirAnimation.HorseStruck;
                    //break;
                case MirAction.Die:
                    animation = MirAnimation.Die;
                    break;
                case MirAction.Dead:
                    animation = MirAnimation.Dead;
                    break;
                case MirAction.Harvest:
                    animation = MirAnimation.Harvest;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            CurrentAnimation = animation;
            if (!Frames.TryGetValue(CurrentAnimation, out CurrentFrame))
                CurrentFrame = Frame.EmptyFrame;
        }

        public sealed override void SetFrame(ObjectAction action)
        {
            base.SetFrame(action);

            switch (action.Action)
            {
                case MirAction.Spell:
                case MirAction.Attack:
                    StanceTime = CEnvir.Now.AddSeconds(3);
                    break;
            }
        }

        public override void FrameIndexChanged()
        {
            switch (CurrentAction)
            {
                case MirAction.Spell:
                    switch (MagicType)
                    {
                        case MagicType.SeismicSlam:
                            if (FrameIndex == 4)
                            {
                                Effects.Add(new MirEffect(700, 7, TimeSpan.FromMilliseconds(120), LibraryFile.MonMagicEx7, 10, 35, Globals.LightningColour)
                                {
                                    Blend = true,
                                    MapTarget = Functions.Move(CurrentLocation, Direction, 2),
                                });
                            }
                            break;
                        case MagicType.CrushingWave:
                            if (FrameIndex == 4)
                            {
                                MirEffect spell;
                                Effects.Add(spell = new MirProjectile(200, 8, TimeSpan.FromMilliseconds(100), LibraryFile.MagicEx6, 35, 35, Globals.LightningColour, CurrentLocation)
                                {
                                    Blend = true,
                                    Has16Directions = false,
                                    MapTarget = Functions.Move(CurrentLocation, Direction, Globals.MagicRange),
                                    Speed = 100,
                                });
                                spell.Process();
                                DXSoundManager.Play(SoundIndex.DestructiveBlow);

                                Effects.Add(new MirEffect(300, 9, TimeSpan.FromMilliseconds(150), LibraryFile.MagicEx6, 10, 35, Globals.LightningColour)
                                {
                                    Blend = true,
                                    Direction = Direction,
                                    MapTarget = Functions.Move(CurrentLocation, Direction, 1),
                                });
                            }
                            break;
                    }
                    break;
                default:
                    base.FrameIndexChanged();
                    break;
            }
        }

        public override void SetAction(ObjectAction action)
        {
            base.SetAction(action);

            switch (CurrentAction)
            {
                case MirAction.Attack:
                    switch (MagicType)
                    {
                        #region Sweet Brier and Karma (Karma should have different attack will do later if can be bothered)
                        case MagicType.SweetBrier:
                        case MagicType.Karma:
                            if (LibraryWeaponShape >= 1200)
                            {
                                Effects.Add(new MirEffect(300, 6, TimeSpan.FromMilliseconds(100), LibraryFile.MagicEx4, 20, 70, Globals.NoneColour) //Element style?
                                {
                                    Blend = true,
                                    Target = this,
                                    DrawColour = Globals.NoneColour,
                                    Direction = Direction,
                                });
                            }
                            else if (LibraryWeaponShape >= 1100)
                            {
                                Effects.Add(new MirEffect(100, 6, TimeSpan.FromMilliseconds(100), LibraryFile.MagicEx4, 20, 70, Globals.NoneColour) //Element style?
                                {
                                    Blend = true,
                                    Target = this,
                                    DrawColour = Globals.NoneColour,
                                    Direction = Direction,
                                });
                            }

                            if (Gender == MirGender.Male)
                                DXSoundManager.Play(SoundIndex.SweetBrierMale);
                            else
                                DXSoundManager.Play(SoundIndex.SweetBrierFemale);
                            break;
                            #endregion
                    }
                    break;

            }
        }    
        
        public bool DrawWingsBehind()
        {
            switch (Direction)
            {
                case MirDirection.Up:
                case MirDirection.UpRight:
                case MirDirection.Right:
                case MirDirection.Left:
                case MirDirection.UpLeft:
                    return false;
                case MirDirection.DownRight:
                case MirDirection.Down:
                case MirDirection.DownLeft:
                    switch (ArmourImage)
                    {
                        //All
                        case 962:
                        case 972:
                            return false;
                        default:
                            return true;
                    }
            }
            return false;
        }

        public bool DrawWeaponEffectBehind()
        {
            switch (Direction)
            {                
                case MirDirection.Up:                            
                case MirDirection.DownLeft:
                case MirDirection.Left:
                case MirDirection.UpLeft:
                    return true;
            }
            return false;
        }

        public bool DrawShieldEffectBehind()
        {
            switch (Direction)
            {
                case MirDirection.UpRight:
                case MirDirection.Right:
                case MirDirection.DownRight:
                    return true;
            }
            return false;
        }

        public bool DrawWingsInfront()
        {
            switch (Direction)
            {
                case MirDirection.Up:
                case MirDirection.UpRight:
                case MirDirection.Right:
                case MirDirection.Left:
                case MirDirection.UpLeft:
                    return true;
                case MirDirection.DownRight:
                case MirDirection.Down:
                case MirDirection.DownLeft:
                    switch (ArmourImage)
                    {
                        //All
                        case 962:
                        case 972:
                            return true;
                    }
                    break;
            }
            return false;
        }

        public bool DrawWeaponEffectInfront()
        {
            switch (Direction)
            {
                case MirDirection.UpRight:
                case MirDirection.Right:
                case MirDirection.DownRight:
                case MirDirection.Down:
                    return true;
            }
            return false;
        }

        public bool DrawShieldEffectInfront()
        {
            switch (Direction)
            {
                case MirDirection.Up:
                case MirDirection.Down:
                case MirDirection.DownLeft:
                case MirDirection.Left:
                case MirDirection.UpLeft:
                    return true;
            }
            return false;
        }

        public override void Draw()
        {
            if (BodyLibrary == null) return;

            if (DrawWingsBehind())
                DrawWings();

            if (DrawWeaponEffectBehind())
                DrawWeaponEffect();

            if (DrawShieldEffectBehind())
                DrawShieldEffect();

            DrawBody(true);

            if (DrawWingsInfront())
                DrawWings();

            if (DrawWeaponEffectInfront())
                DrawWeaponEffect();

            if (DrawShieldEffectInfront())
                DrawShieldEffect();

            DrawEmblemEffect();
        }
        public override void DrawBlend()
        {
            if (BodyLibrary == null) return;

            DXManager.SetBlend(true, 0.60F);

            DrawBody(false);
            DXManager.SetBlend(false); 
        }

        public void DrawBody(bool shadow)
        {
            
            Surface oldSurface = DXManager.CurrentSurface;
            DXManager.SetSurface(DXManager.ScratchSurface);
            DXManager.Device.Clear(ClearFlags.Target, 0, 0, 0);
            DXManager.Sprite.Flush();

            int l = int.MaxValue, t = int.MaxValue, r = int.MinValue, b = int.MinValue;


            MirImage image;
            switch (Direction)
            {
                case MirDirection.Up:
                case MirDirection.DownLeft:
                case MirDirection.Left:
                case MirDirection.UpLeft:
                    if (!DrawWeapon) break;
                    image = WeaponLibrary1?.GetImage(WeaponFrame);
                    if (image == null) break;

                    WeaponLibrary1.Draw(WeaponFrame, DrawX, DrawY, Color.White, true, 1F, ImageType.Image);

                    l = Math.Min(l, DrawX + image.OffSetX);
                    t = Math.Min(t, DrawY + image.OffSetY);
                    r = Math.Max(r, image.Width + DrawX + image.OffSetX);
                    b = Math.Max(b, image.Height + DrawY + image.OffSetY);
                    break;
                default:
                    if (!DrawWeapon) break;
                    image = WeaponLibrary2?.GetImage(WeaponFrame);
                    if (image == null) break;

                    WeaponLibrary2.Draw(WeaponFrame, DrawX, DrawY, Color.White, true, 1F, ImageType.Image);

                    l = Math.Min(l, DrawX + image.OffSetX);
                    t = Math.Min(t, DrawY + image.OffSetY);
                    r = Math.Max(r, image.Width + DrawX + image.OffSetX);
                    b = Math.Max(b, image.Height + DrawY + image.OffSetY);
                    break;
            }

            switch (Direction)
            {
                case MirDirection.UpRight:
                case MirDirection.Right:
                case MirDirection.DownRight:
                    if (ShieldShape > 0 && ShieldShape < 1000)
                    {
                        image = ShieldLibrary?.GetImage(ShieldFrame);
                        if (image != null)
                        {
                            ShieldLibrary.Draw(ShieldFrame, DrawX, DrawY, Color.White, true, 1F, ImageType.Image);

                            l = Math.Min(l, DrawX + image.OffSetX);
                            t = Math.Min(t, DrawY + image.OffSetY);
                            r = Math.Max(r, image.Width + DrawX + image.OffSetX);
                            b = Math.Max(b, image.Height + DrawY + image.OffSetY);
                        }
                    }
                    break;
            }

            image = BodyLibrary?.GetImage(ArmourFrame);
            if (image != null)
            {
                BodyLibrary.Draw(ArmourFrame, DrawX, DrawY, Color.White, true, 1F, ImageType.Image);

                if (ArmourColour.ToArgb() != 0)
                    BodyLibrary.Draw(ArmourFrame, DrawX, DrawY, ArmourColour, true, 1F, ImageType.Overlay);

                l = Math.Min(l, DrawX + image.OffSetX);
                t = Math.Min(t, DrawY + image.OffSetY);
                r = Math.Max(r, image.Width + DrawX + image.OffSetX);
                b = Math.Max(b, image.Height + DrawY + image.OffSetY);
            }


            if (HelmetShape > 0)
            {
                image = HelmetLibrary?.GetImage(HelmetFrame);
                if (image != null)
                {
                    HelmetLibrary.Draw(HelmetFrame, DrawX, DrawY, Color.White, true, 1F, ImageType.Image);

                    l = Math.Min(l, DrawX + image.OffSetX);
                    t = Math.Min(t, DrawY + image.OffSetY);
                    r = Math.Max(r, image.Width + DrawX + image.OffSetX);
                    b = Math.Max(b, image.Height + DrawY + image.OffSetY);
                }
            }
            else
            {
                image = HairLibrary.GetImage(HairFrame);
                if (HairType > 0 && image != null)
                {
                    HairLibrary.Draw(HairFrame, DrawX, DrawY, HairColour, true, 1F, ImageType.Image);

                    l = Math.Min(l, DrawX + image.OffSetX);
                    t = Math.Min(t, DrawY + image.OffSetY);
                    r = Math.Max(r, image.Width + DrawX + image.OffSetX);
                    b = Math.Max(b, image.Height + DrawY + image.OffSetY);
                }
            }

            switch (Direction)
            {
                case MirDirection.UpRight:
                case MirDirection.Right:
                case MirDirection.DownRight:
                case MirDirection.Down:
                    if (!DrawWeapon) break;
                    image = WeaponLibrary1?.GetImage(WeaponFrame);
                    if (image == null) break;

                    WeaponLibrary1.Draw(WeaponFrame, DrawX, DrawY, Color.White, true, 1F, ImageType.Image);

                    l = Math.Min(l, DrawX + image.OffSetX);
                    t = Math.Min(t, DrawY + image.OffSetY);
                    r = Math.Max(r, image.Width + DrawX + image.OffSetX);
                    b = Math.Max(b, image.Height + DrawY + image.OffSetY);
                    break;
                default:
                    if (!DrawWeapon) break;
                    image = WeaponLibrary2?.GetImage(WeaponFrame);
                    if (image == null) break;

                    WeaponLibrary2.Draw(WeaponFrame, DrawX, DrawY, Color.White, true, 1F, ImageType.Image);

                    l = Math.Min(l, DrawX + image.OffSetX);
                    t = Math.Min(t, DrawY + image.OffSetY);
                    r = Math.Max(r, image.Width + DrawX + image.OffSetX);
                    b = Math.Max(b, image.Height + DrawY + image.OffSetY);
                    break;
            }

            switch (Direction)
            {
                case MirDirection.Up:
                case MirDirection.Down:
                case MirDirection.DownLeft:
                case MirDirection.Left:
                case MirDirection.UpLeft:
                    if (ShieldShape > 0 && ShieldShape < 1000)
                    {
                        image = ShieldLibrary?.GetImage(ShieldFrame);
                        if (image != null)
                        {
                            ShieldLibrary.Draw(ShieldFrame, DrawX, DrawY, Color.White, true, 1F, ImageType.Image);

                            l = Math.Min(l, DrawX + image.OffSetX);
                            t = Math.Min(t, DrawY + image.OffSetY);
                            r = Math.Max(r, image.Width + DrawX + image.OffSetX);
                            b = Math.Max(b, image.Height + DrawY + image.OffSetY);
                        }
                    }
                    break;
            }

            DXManager.SetSurface(oldSurface);
            float oldOpacity = DXManager.Opacity;
            
            if (shadow)
            {
                switch (CurrentAnimation)
                {
                    case MirAnimation.HorseStanding:
                    case MirAnimation.HorseWalking:
                    case MirAnimation.HorseRunning:
                    case MirAnimation.HorseStruck:
                        HorseLibrary?.Draw(HorseFrame, DrawX, DrawY, Color.Black, true, 0.5F, ImageType.Shadow);
                        break;
                    default:
                        DrawShadow2(l, t, r, b);
                        break;
                }
            }

            if (oldOpacity != Opacity && !DXManager.Blending) DXManager.SetOpacity(Opacity);


            switch (CurrentAnimation)
            {
                case MirAnimation.HorseStanding:
                case MirAnimation.HorseWalking:
                case MirAnimation.HorseRunning:
                case MirAnimation.HorseStruck:

                    switch (HorseShape)
                    {
                        case 0:
                            HorseLibrary?.Draw(HorseFrame, DrawX, DrawY, Color.White, true, Opacity, ImageType.Image);
                            break;
                        case 1:
                        case 2:
                        case 3:
                            HorseShapeLibrary?.Draw(HorseFrame, DrawX, DrawY, Color.White, true, Opacity, ImageType.Image);
                            break;
                        case 4:
                            HorseShapeLibrary?.Draw(DrawFrame, DrawX, DrawY, Color.White, true, Opacity, ImageType.Image);
                            break;
                        case 5:
                            HorseShapeLibrary?.Draw(DrawFrame, DrawX, DrawY, Color.White, true, Opacity, ImageType.Image);
                            if(shadow)
                                HorseShapeLibrary2?.DrawBlend(DrawFrame, DrawX, DrawY, Color.White, true, Opacity, ImageType.Image);
                            break;

                    }

                    break;
            }



            DXManager.Sprite.Draw(DXManager.ScratchTexture, Rectangle.FromLTRB(l, t, r, b), Vector3.Zero, new Vector3(l, t, 0), DrawColour);
            CEnvir.DPSCounter++;
            if (oldOpacity != Opacity && !DXManager.Blending) DXManager.SetOpacity(oldOpacity);

            }
        public void DrawShadow2(int l, int t, int r, int b)
        {
            MirImage image = BodyLibrary?.GetImage(ArmourFrame);

            if (image == null) return;
            
            int w = (DrawX + image.OffSetX) - l;
            int h = (DrawY + image.OffSetY) - t;

            Matrix m = Matrix.Scaling(1F, 0.5f, 0);

            m.M21 = -0.50F;
            DXManager.Sprite.Transform = m * Matrix.Translation(DrawX + image.ShadowOffSetX - w + (image.Height)/2 + h / 2, DrawY + image.ShadowOffSetY  - h /2, 0);

            DXManager.Device.SetSamplerState(0, SamplerState.MinFilter, TextureFilter.None);

            float oldOpacity = DXManager.Opacity;
            if (oldOpacity != 0.5F) DXManager.SetOpacity(0.5F);
            DXManager.Sprite.Draw(DXManager.ScratchTexture, Rectangle.FromLTRB(l, t, r, b), Vector3.Zero, Vector3.Zero, Color.Black);

            DXManager.Sprite.Transform = Matrix.Identity;
            DXManager.Device.SetSamplerState(0, SamplerState.MinFilter, TextureFilter.Point);

            if (0.5F != oldOpacity) DXManager.SetOpacity(oldOpacity);
        }

        public override void DrawHealth()
        {
            if (this == User && !Config.ShowUserHealth) return;

            ClientObjectData data;
            if (!GameScene.Game.DataDictionary.TryGetValue(ObjectID, out data)) return;

            if (!GameScene.Game.IsAlly(ObjectID) && User.Buffs.All(x => x.Type != BuffType.Developer)) return;

            if (data.MaxHealth > 0)
            {
                MirLibrary library;

                if (!CEnvir.LibraryList.TryGetValue(LibraryFile.Interface, out library)) return;


                float percent = Math.Min(1, Math.Max(0, data.Health / (float)data.MaxHealth));

                if (percent == 0) return;

                Size size = library.GetSize(79);

                Color color = Color.OrangeRed;

                library.Draw(80, DrawX, DrawY - 55, Color.White, false, 1F, ImageType.Image);
                library.Draw(79, DrawX + 1, DrawY - 55 + 1, color, new Rectangle(0, 0, (int)(size.Width * percent), size.Height), 1F, ImageType.Image);
            }

            if (data.MaxMana > 0)
            {
                MirLibrary library;

                if (!CEnvir.LibraryList.TryGetValue(LibraryFile.Interface, out library)) return;


                float percent = Math.Min(1, Math.Max(0, data.Mana / (float)data.MaxMana));

                if (percent == 0) return;

                Size size = library.GetSize(79);

                Color color = Color.DodgerBlue;


                library.Draw(80, DrawX, DrawY - 51, Color.White, false, 1F, ImageType.Image);
                library.Draw(79, DrawX + 1, DrawY - 51 + 1, color, new Rectangle(0, 0, (int)(size.Width * percent), size.Height), 1F, ImageType.Image);
            }
        }

        private void DrawWings()
        {
            if (!Config.DrawEffects) return;

            switch (CurrentAction)
            {
                case MirAction.Die:
                case MirAction.Dead:
                    break;
                default:
                    MirLibrary library;

                    

                    switch (ArmourImage)
                    {
                        //All
                        case 962:
                        case 972:
                            if (!CEnvir.LibraryList.TryGetValue(LibraryFile.EquipEffect_Part, out library)) return;
                            library.DrawBlend(820 + (GameScene.Game.MapControl.Animation / 2) % 13, DrawX, DrawY, Color.White, true, 0.7f, ImageType.Image);
                            //820 ~ 832
                            break;

                        //War
                        case 963:
                        case 973:
                            if (!CEnvir.LibraryList.TryGetValue(LibraryFile.EquipEffect_Part, out library)) return;
                            library.DrawBlend(400 + (GameScene.Game.MapControl.Animation / 2) % 15 + (int)Direction * 20, DrawX, DrawY, Color.White, true, 1f, ImageType.Image);
                            // 400 - 414 // 420 ....
                            break;
                        case 3320:
                        case 3330:
                            if (!CEnvir.LibraryList.TryGetValue(LibraryFile.EquipEffect_Full, out library)) return;
                            if (ArmourEffect == ItemEffect.Level75ArmourUpgrade)
                                library.DrawBlend(50000 + ((byte)Gender * 5000) + DrawFrame, DrawX, DrawY, Color.White, true, 1f, ImageType.Image);
                            break;
                        //Wiz
                        case 964:
                        case 974:
                            if (!CEnvir.LibraryList.TryGetValue(LibraryFile.EquipEffect_Part, out library)) return;
                            library.DrawBlend(200 + (GameScene.Game.MapControl.Animation / 2) % 15 + (int)Direction * 20, DrawX, DrawY, Color.White, true, 1f, ImageType.Image);
                            // 200 - 214 // 220 ....
                            break;
                        case 3340:
                        case 3350:
                            if (!CEnvir.LibraryList.TryGetValue(LibraryFile.EquipEffect_Full, out library)) return;
                            if (ArmourEffect == ItemEffect.Level75ArmourUpgrade)
                                library.DrawBlend(60000 + ((byte)Gender * 5000) + DrawFrame, DrawX, DrawY, Color.White, true, 1f, ImageType.Image);
                            break;

                        //Tao
                        case 965:
                        case 975:
                            if (!CEnvir.LibraryList.TryGetValue(LibraryFile.EquipEffect_Part, out library)) return;
                            library.DrawBlend(0 + (GameScene.Game.MapControl.Animation / 2) % 15 + (int)Direction * 20, DrawX, DrawY, Color.White, true, 1f, ImageType.Image);
                            // 000 - 014 // 020 ....
                            break;
                        case 3360:
                        case 3370:
                            if (!CEnvir.LibraryList.TryGetValue(LibraryFile.EquipEffect_Full, out library)) return;
                            if (ArmourEffect == ItemEffect.Level75ArmourUpgrade)
                                library.DrawBlend(70000 + ((byte)Gender * 5000) + DrawFrame, DrawX, DrawY, Color.White, true, 1f, ImageType.Image);
                            break;

                        //Ass
                        case 2007:
                        case 2017:
                            if (!CEnvir.LibraryList.TryGetValue(LibraryFile.EquipEffect_Part, out library)) return;
                            library.DrawBlend(600 + (GameScene.Game.MapControl.Animation / 2) % 13 + (int)Direction * 20, DrawX, DrawY, Color.White, true, 1f, ImageType.Image);
                            // 600 - 614 // 620 ....
                            break;
                        case 3380:
                        case 3390:
                            if (!CEnvir.LibraryList.TryGetValue(LibraryFile.EquipEffect_Full, out library)) return;
                            if (ArmourEffect == ItemEffect.Level75ArmourUpgrade)
                                library.DrawBlend(80000 + ((byte)Gender * 5000) + DrawFrame, DrawX, DrawY, Color.White, true, 1f, ImageType.Image);
                            break;

                    }

                    break;
            }
        }

        public void DrawWeaponEffect()
        {
            if (!Config.DrawEffects) return;

            switch (CurrentAction)
            {
                case MirAction.Die:
                case MirAction.Dead:
                    break;
                default:
                    MirLibrary library;

                    if (!CEnvir.LibraryList.TryGetValue(LibraryFile.EquipEffect_Full, out library)) return;                    

                    switch (WeaponImage)
                    {
                        //Warrior/Wiz/Tao
                        case 1076: //Chaotic Heaven Blade
                            library.DrawBlend(40000 + 5000 * (byte)Gender + DrawFrame, DrawX, DrawY, Color.White, true, 0.8f, ImageType.Image);
                            break;

                        //Sin
                        case 2550: //Chaotic Heaven glaive
                            library.DrawBlend(20000 + 5000 * (byte)Gender + DrawFrame, DrawX, DrawY, Color.White, true, 0.8f, ImageType.Image);
                            break;
                    }

                    break;
            }
        }

        public void DrawShieldEffect()
        {
            if (!Config.DrawEffects) return;

            switch (CurrentAction)
            {
                case MirAction.Die:
                case MirAction.Dead:
                    break;
                default:
                    if (ShieldShape >= 1000)
                    {
                        ShieldLibrary.DrawBlend(ShieldFrame + 100, DrawX, DrawY, Color.White, true, 0.8f, ImageType.Image);
                        ShieldLibrary.Draw(ShieldFrame, DrawX, DrawY, Color.White, true, 1F, ImageType.Image);
                    }

                    break;
            }
        }

        private void DrawEmblemEffect()
        {
            if (!Config.DrawEffects) return;
            if (EmblemLibrary == null) return;

            switch (CurrentAction)
            {
                case MirAction.Die:
                case MirAction.Dead:
                    break;
                default:
                    switch (EmblemShape)
                    {
                        //Masters Emblem
                        case 1:
                            EmblemLibrary.DrawBlend(500 + (GameScene.Game.MapControl.Animation) % 40, DrawX, DrawY, Color.White, true, 0.7f, ImageType.Image);
                            break;

                        //Kings Emblem
                        case 2:
                            EmblemLibrary.DrawBlend(3800 + (GameScene.Game.MapControl.Animation) % 32, DrawX, DrawY, Color.White, true, 0.7f, ImageType.Image);
                            EmblemLibrary.DrawBlend(3850 + (GameScene.Game.MapControl.Animation) % 28, DrawX, DrawY, Color.White, true, 0.7f, ImageType.Image);
                            break;

                        //Zirconian Emblem
                        case 3:
                            EmblemLibrary.DrawBlend(640 + (GameScene.Game.MapControl.Animation) % 33, DrawX, DrawY, Color.White, true, 0.7f, ImageType.Image);
                            EmblemLibrary.DrawBlend(600 + (GameScene.Game.MapControl.Animation) % 28, DrawX, DrawY, Color.White, true, 0.7f, ImageType.Image);
                            break;
                    }
                    break;
            }
        }

        public override bool MouseOver(Point p)
        {
            if (BodyLibrary != null && BodyLibrary.VisiblePixel(ArmourFrame, new Point(p.X - DrawX, p.Y - DrawY), false, true))
                return true;

            if (HairType >= 0 && HairLibrary != null && HairLibrary.VisiblePixel(HairFrame, new Point(p.X - DrawX, p.Y - DrawY), false, true))
                return true;

            if (HelmetShape >= 0 && HelmetLibrary != null && HelmetLibrary.VisiblePixel(HelmetFrame, new Point(p.X - DrawX, p.Y - DrawY), false, true))
                return true;

            if (LibraryWeaponShape >= 0 && WeaponLibrary1 != null && WeaponLibrary1.VisiblePixel(WeaponFrame, new Point(p.X - DrawX, p.Y - DrawY), false, true))
                return true;

            if (LibraryWeaponShape >= 0 && WeaponLibrary2 != null && WeaponLibrary2.VisiblePixel(WeaponFrame, new Point(p.X - DrawX, p.Y - DrawY), false, true))
                return true;

            if (ShieldShape >= 0 && ShieldLibrary != null && ShieldLibrary.VisiblePixel(ShieldFrame, new Point(p.X - DrawX, p.Y - DrawY), false, true))
                return true;



            switch (CurrentAnimation)
            {
                case MirAnimation.HorseStanding:
                case MirAnimation.HorseWalking:
                case MirAnimation.HorseRunning:
                case MirAnimation.HorseStruck:
                    if (HorseLibrary != null && HorseLibrary.VisiblePixel(HorseFrame, new Point(p.X - DrawX, p.Y - DrawY), false, true))
                        return true;
                    break;
            }

            return false;
        }
        
        public override void PlayStruckSound()
        {
            DXSoundManager.Play(Gender == MirGender.Male ? SoundIndex.MaleStruck : SoundIndex.FemaleStruck);
            DXSoundManager.Play(SoundIndex.GenericStruckPlayer);
        }
        public override void PlayDieSound()
        {
            DXSoundManager.Play(Gender == MirGender.Male ? SoundIndex.MaleDie : SoundIndex.FemaleDie);
        }
        public override void PlayAttackSound()
        {
            if (Class != MirClass.Assassin)
                PlayCommonSounds();
            else
                PlayAssassinSounds();
        }

        private void PlayAssassinSounds()
        {

            if (LibraryWeaponShape >= 1200)
                DXSoundManager.Play(SoundIndex.ClawAttack);
            else if (LibraryWeaponShape >= 1100)
                DXSoundManager.Play(SoundIndex.GlaiveAttack);
            else
                PlayCommonSounds(); //?
        }
        private void PlayCommonSounds()
        {
            switch (LibraryWeaponShape)
            {
                case 100:
                    DXSoundManager.Play(SoundIndex.WandSwing);
                    break;
                case 9:
                case 101:
                    DXSoundManager.Play(SoundIndex.WoodSwing);
                    break;
                case 102:
                    DXSoundManager.Play(SoundIndex.AxeSwing);
                    break;
                case 103:
                    DXSoundManager.Play(SoundIndex.DaggerSwing);
                    break;
                case 104:
                    DXSoundManager.Play(SoundIndex.ShortSwordSwing);
                    break;
                case 26:
                case 105:
                    DXSoundManager.Play(SoundIndex.IronSwordSwing);
                    break;
                default:
                    DXSoundManager.Play(SoundIndex.FistSwing);
                    break;
            }
        }
    }
}
