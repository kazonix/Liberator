using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Liberator;

internal class R6S
{

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenThread(long access, bool inheritHandle, uint threadId);

    [DllImport("kernel32.dll")]
    private static extern bool TerminateThread(IntPtr handle, uint exitCode);

    [DllImport("kernel32.dll")]
    private static extern uint SuspendThread(IntPtr handle);

    public R6S()
    {
        long[] array = new long[] { 29678537814, 29678538048, 29678538049, 33848225616, 39182347906, 2448834880 };
        _madHouseGametypes = array;
        _harvardValueA = unchecked((long)((ulong)(-1597699174)));
        _harvardValueB = 857538214L;
        _oldHerefordValueB = 127951053400L;
        _oldHerefordValueA = 708483531L;
        _processName = "RainbowSix";
        _build = GameBuild.None;
        ResolveProcessName();
    }

    private void ResolveProcessName()
    {
        if (Process.GetProcessesByName(_processName).Length != 0)
        {
            return;
        }
        _processName = "RainbowSixGame";
        if (Process.GetProcessesByName(_processName).Length != 0)
        {
            return;
        }
        _processName = "RainbowSix";
    }

    public bool IsAttached()
    {
        bool flag;
        try
        {
            flag = Memory.IsAttached();
        }
        catch
        {
            flag = false;
        }
        return flag;
    }

    private bool IsKnownBuildString(string version)
    {
        string[] field = R6S.BuildSignatures;
        for (int i = 0; i < field.Length; i++)
        {
            if (field[i] == version)
            {
                return true;
            }
        }
        return false;
    }

    public Season GetSeason()
    {
        return _season;
    }

    public string GetBuildVersion(GameBuild build)
    {
        string text = build.ToString();
        return text.Substring(text.LastIndexOf('_') + 1);
    }

    public GameBuild DetectBuild()
    {
        string text = Memory.ReadScrambledString(218880543UL, 312763, true);
        if (IsKnownBuildString(text))
        {
            _season = Season.Y1S0_Launch;
            _build = GameBuild.Y1S0_8194013;
            return _build;
        }
        string text2 = Memory.ReadScrambledString(220276943UL, 312763, true);
        if (IsKnownBuildString(text2))
        {
            _season = Season.Y1S1_Black_Ice;
            _build = GameBuild.Y1S1_8519860;
            return _build;
        }
        string text3 = Memory.ReadScrambledString(225420783UL, 312763, true);
        if (IsKnownBuildString(text3))
        {
            _season = Season.Y1S2_Dust_Line;
            _build = GameBuild.Y1S2_9132097;
            return _build;
        }
        string text4 = Memory.ReadScrambledString(225661103UL, 312798, true);
        if (IsKnownBuildString(text4))
        {
            _season = Season.Y1S3_Skull_Rain;
            _build = GameBuild.Y1S3_9654076;
            return _build;
        }
        string text5 = Memory.ReadScrambledString(226234543UL, 312788, true);
        if (IsKnownBuildString(text5))
        {
            _season = Season.Y1S3_Skull_Rain;
            _build = GameBuild.Y1S3_9860556;
            return _build;
        }
        string text6 = Memory.ReadScrambledString(236107663UL, 312793, true);
        if (IsKnownBuildString(text6))
        {
            _season = Season.Y1S4_Red_Crow;
            _build = GameBuild.Y1S4_10211195;
            return _build;
        }
        string text7 = Memory.ReadScrambledString(244061183UL, 312813, true);
        if (IsKnownBuildString(text7))
        {
            _season = Season.Y2S1_Velvet_Shell;
            _build = GameBuild.Y2S1_10751226;
            return _build;
        }
        string text8 = Memory.ReadScrambledString(246344703UL, 312803, true);
        if (IsKnownBuildString(text8))
        {
            _season = Season.Y2S2_Health;
            _build = GameBuild.Y2S2_11216230;
            return _build;
        }
        string text9 = Memory.ReadScrambledString(274039823UL, 312813, true);
        if (IsKnownBuildString(text9))
        {
            _season = Season.Y2S3_Blood_Orchid;
            _build = GameBuild.Y2S3_11432634;
            return _build;
        }
        string text10 = Memory.ReadScrambledString(274986223UL, 312813, true);
        if (IsKnownBuildString(text10))
        {
            _season = Season.Y2S3_Blood_Orchid;
            _build = GameBuild.Y2S3_11493221;
            return _build;
        }
        string text11 = Memory.ReadScrambledString(278393733UL, 312803, true);
        if (IsKnownBuildString(text11))
        {
            _season = Season.Y2S4_White_Noise;
            _build = GameBuild.Y2S4_11553121;
            return _build;
        }
        string text12 = Memory.ReadScrambledString(278414453UL, 312803, true);
        if (IsKnownBuildString(text12))
        {
            _season = Season.Y2S4_White_Noise;
            _build = GameBuild.Y2S4_11580709;
            return _build;
        }
        string text13 = Memory.ReadScrambledString(283744933UL, 312803, true);
        if (IsKnownBuildString(text13))
        {
            _season = Season.Y3S1_Chimera;
            _build = GameBuild.Y3S1_11726982;
            return _build;
        }
        string text14 = Memory.ReadScrambledString(286610693UL, 312813, true);
        if (IsKnownBuildString(text14))
        {
            _season = Season.Y3S2_Para_Bellum;
            _build = GameBuild.Y3S2_11938214;
            return _build;
        }
        string text15 = Memory.ReadScrambledString(286632133UL, 312823, true);
        if (IsKnownBuildString(text15))
        {
            _season = Season.Y3S2_Para_Bellum;
            _build = GameBuild.Y3S2_11965022;
            return _build;
        }
        string text16 = Memory.ReadScrambledString(291376213UL, 312813, true);
        if (IsKnownBuildString(text16))
        {
            _season = Season.Y3S3_Grim_Sky;
            _build = GameBuild.Y3S3_12213419;
            return _build;
        }
        string text17 = Memory.ReadScrambledString(291892293UL, 312803, true);
        if (IsKnownBuildString(text17))
        {
            _season = Season.Y3S3_Grim_Sky;
            _build = GameBuild.Y3S3_12362767;
            return _build;
        }
        string text18 = Memory.ReadScrambledString(309087568UL, 312803, true);
        if (IsKnownBuildString(text18))
        {
            _season = Season.Y3S4_Wind_Bastion;
            _build = GameBuild.Y3S4_12512571;
            return _build;
        }
        string text19 = Memory.ReadScrambledString(310451248UL, 312803, true);
        if (IsKnownBuildString(text19))
        {
            _season = Season.Y4S2_Phantom_Sight;
            _build = GameBuild.Y4S2_13147883;
            return _build;
        }
        string text20 = Memory.ReadScrambledString(303250688UL, 312803, true);
        if (IsKnownBuildString(text20))
        {
            _season = Season.Y4S1_Burnt_Horizon;
            _build = GameBuild.Y4S1_12815133;
            return _build;
        }
        string text21 = Memory.ReadScrambledString(303250688UL, 312813, true);
        if (IsKnownBuildString(text21))
        {
            _season = Season.Y4S1_Burnt_Horizon;
            _build = GameBuild.Y4S1_12863847;
            return _build;
        }
        string text22 = Memory.ReadScrambledString(283683493UL, 312803, true);
        if (IsKnownBuildString(text22))
        {
            _season = Season.Y3S1_Chimera;
            _build = GameBuild.Y3S1_11706399;
            return _build;
        }
        string text23 = Memory.ReadScrambledString(318105008UL, 312813, true);
        if (IsKnownBuildString(text23))
        {
            _season = Season.Y4S3_Ember_Rise;
            _build = GameBuild.Y4S3_13632147;
            return _build;
        }
        string text24 = Memory.ReadScrambledString(323528128UL, 312813, true);
        if (IsKnownBuildString(text24))
        {
            _season = Season.Y4S4_Shifting_Tides;
            _build = GameBuild.Y4S4_13777760;
            return _build;
        }
        string text25 = Memory.ReadScrambledString(322863248UL, 312813, true);
        if (IsKnownBuildString(text25))
        {
            _season = Season.Y4S4_Shifting_Tides;
            _build = GameBuild.Y4S4_13924517;
            return _build;
        }
        return GameBuild.None;
    }

    public int KillGuardThread(int lastPid)
    {
        ResolveProcessName();
        Memory = new MemoryHandler(_processName);
        if (Memory.ProcessId != lastPid)
        {
            ProcessThread processThread = null;
            double num = 0.0;
            IEnumerator enumerator = Memory.Threads.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    ProcessThread processThread2 = (ProcessThread)enumerator.Current;
                    if (processThread2.PriorityLevel == ThreadPriorityLevel.Lowest && processThread2.BasePriority == 6)
                    {
                        if (processThread2.ThreadState == ThreadState.Wait && processThread2.WaitReason == ThreadWaitReason.Suspended)
                        {
                            return lastPid;
                        }
                        double num2 = processThread2.TotalProcessorTime.TotalMilliseconds / Memory.TotalProcessorTimeMs;
                        if (num2 > num)
                        {
                            processThread = processThread2;
                        }
                        num = num2;
                    }
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            if (num > 0.0)
            {
                R6S.TerminateThread(R6S.OpenThread(2035711L, false, (uint)processThread.Id), 0U);
                return Memory.ProcessId;
            }
            return lastPid;
        }
        return lastPid;
    }

    public void Reattach()
    {
        ResolveProcessName();
        Memory = new MemoryHandler(_processName);
    }

    public void SetHarvard(bool enabled)
    {
        GameBuild field = _build;
        ulong num3;
        if (field == GameBuild.Y1S0_8194013)
        {
            MemoryHandler field2 = Memory;
            ulong num = 185628562UL;
            int num2 = -1;
            int[] array = new int[] { 10482, 9522, 9778, 9538, 11186 };
            num3 = field2.ResolvePointerChain(num, num2, array);
        }
        else
        {
            switch (field)
            {
                case GameBuild.Y1S1_8519860:
                    {
                        MemoryHandler field3 = Memory;
                        ulong num4 = 190846258UL;
                        int num5 = -1;
                        int[] array2 = new int[] { 10642, 9762, 9522, 9666, 0 };
                        num3 = field3.ResolvePointerChain(num4, num5, array2);
                        break;
                    }
                case GameBuild.Y1S2_9132097:
                    {
                        MemoryHandler field4 = Memory;
                        ulong num6 = 183320002UL;
                        int num7 = -1;
                        int[] array3 = new int[] { 9634, 9810, 9570, 9906, 11298, 9522, 13682 };
                        num3 = field4.ResolvePointerChain(num6, num7, array3);
                        break;
                    }
                case GameBuild.Y1S4_10211195:
                    {
                        MemoryHandler field5 = Memory;
                        ulong num8 = 108150610UL;
                        int num9 = -1;
                        int[] array4 = new int[] { 9586, 9538, 11762 };
                        num3 = field5.ResolvePointerChain(num8, num9, array4);
                        break;
                    }
                default:
                    return;
            }
        }
        if (enabled)
        {
            Memory.WriteInt64(num3, _harvardValueA);
            return;
        }
        Memory.WriteInt64(num3, _harvardValueB);
    }

    public void SetOldHereford(bool enabled)
    {
        if (_build == GameBuild.Y4S2_13147883)
        {
            MemoryHandler field = Memory;
            ulong num = 126484546UL;
            int num2 = -1;
            int[] array = new int[] { 9554, 9666, 13842 };
            ulong num3 = field.ResolvePointerChain(num, num2, array);
            if (enabled)
            {
                Memory.WriteInt64(num3, _oldHerefordValueA);
                return;
            }
            Memory.WriteInt64(num3, _oldHerefordValueB);
        }
    }

    public void EndRound()
    {
        if (!IsInMatch())
        {
            return;
        }
        ulong num;
        switch (_build)
        {
            case GameBuild.Y2S3_11432634:
                num = Memory.ResolvePointerChain(148992066UL, -1, new int[] { 14614 });
                break;
            case GameBuild.Y3S1_11726982:
                num = Memory.ResolvePointerChain(156344802UL, -1, new int[] { 13838 });
                break;
            case GameBuild.Y3S3_12213419:
                num = Memory.ResolvePointerChain(157394466UL, -1, new int[] { 14758 });
                break;
            case GameBuild.Y3S3_12362767:
                num = Memory.ResolvePointerChain(157744626UL, -1, new int[] { 13910 });
                break;
            case GameBuild.Y1S3_9860556:
                num = Memory.ResolvePointerChain(203090562UL, -1, new int[] { 14242 });
                break;
            case GameBuild.Y4S2_13147883:
                num = Memory.ResolvePointerChain(167503682UL, -1, new int[] { 14142 });
                break;
            case GameBuild.Y4S1_12863847:
            case GameBuild.Y4S1_12815133:
                num = Memory.ResolvePointerChain(164803922UL, -1, new int[] { 13958 });
                break;
            case GameBuild.Y3S1_11706399:
                return;
            case GameBuild.Y4S3_13632147:
                num = Memory.ResolvePointerChain(172280226UL, -1, new int[] { 14728 });
                break;
            case GameBuild.Y4S4_13924517:
                num = Memory.ResolvePointerChain(173385250UL, -1, new int[] { 15062 });
                break;
            case GameBuild.Y5S1_14303219:
                num = Memory.ResolvePointerChain(175281986UL, -1, new int[] { 14446 });
                break;
            case GameBuild.Y1S1_8519860:
                num = Memory.ResolvePointerChain(190831378UL, -1, new int[] { 14146 });
                break;
            case GameBuild.Y1S2_9132097:
                num = Memory.ResolvePointerChain(177527042UL, -1, new int[] { 14146 });
                break;
            case GameBuild.Y1S4_10211195:
                num = Memory.ResolvePointerChain(108180802UL, -1, new int[] { 14322 });
                break;
            case GameBuild.Y2S1_10751226:
                num = Memory.ResolvePointerChain(141210818UL, -1, new int[] { 14486 });
                break;
            case GameBuild.Y2S2_11216230:
                num = Memory.ResolvePointerChain(142138994UL, -1, new int[] { 14454 });
                break;
            case GameBuild.Y2S4_11553121:
                num = Memory.ResolvePointerChain(150093074UL, -1, new int[] { 14702 });
                break;
            case GameBuild.Y3S2_11965022:
                num = Memory.ResolvePointerChain(165169250UL, -1, new int[] { 13646 });
                break;
            case GameBuild.Y3S4_12512571:
                num = Memory.ResolvePointerChain(166803730UL, -1, new int[] { 13990 });
                break;
            case GameBuild.Y2S3_11493221:
                num = Memory.ResolvePointerChain(148632002UL, -1, new int[] { 14614 });
                break;
            case GameBuild.Y1S3_9654076:
                num = Memory.ResolvePointerChain(177846434UL, -1, new int[] { 14242 });
                break;
            case GameBuild.Y4S4_13777760:
                num = Memory.ResolvePointerChain(173640962UL, -1, new int[] { 15062 });
                break;
            case GameBuild.Y2S4_11580709:
                num = Memory.ResolvePointerChain(150102034UL, -1, new int[] { 14702 });
                break;
            case GameBuild.Y3S2_11938214:
                num = Memory.ResolvePointerChain(155388946UL, -1, new int[] { 13646 });
                break;
            default:
                return;
        }
        Memory.WriteBool(num, true);
    }

    public void EndMatch()
    {
        if (!IsInMatch())
        {
            return;
        }
        ulong num = 0UL;
        switch (_build)
        {
            case GameBuild.Y2S3_11432634:
                num = Memory.ResolvePointerChain(148992066UL, -1, new int[] { 14636 });
                break;
            case GameBuild.Y3S1_11726982:
                num = Memory.ResolvePointerChain(156344802UL, -1, new int[] { 13858 });
                break;
            case GameBuild.Y3S3_12213419:
                num = Memory.ResolvePointerChain(157394466UL, -1, new int[] { 14778 });
                break;
            case GameBuild.Y3S3_12362767:
                num = Memory.ResolvePointerChain(157744626UL, -1, new int[] { 13930 });
                break;
            case GameBuild.Y1S3_9860556:
                num = Memory.ResolvePointerChain(203090562UL, -1, new int[] { 14266 });
                break;
            case GameBuild.Y4S2_13147883:
                num = Memory.ResolvePointerChain(167503682UL, -1, new int[] { 14164 });
                break;
            case GameBuild.Y4S1_12863847:
            case GameBuild.Y4S1_12815133:
                num = Memory.ResolvePointerChain(164803922UL, -1, new int[] { 13980 });
                break;
            case GameBuild.Y3S1_11706399:
                return;
            case GameBuild.Y4S3_13632147:
                num = Memory.ResolvePointerChain(172280226UL, -1, new int[] { 14750 });
                break;
            case GameBuild.Y4S4_13924517:
                num = Memory.ResolvePointerChain(173385250UL, -1, new int[] { 15084 });
                break;
            case GameBuild.Y5S1_14303219:
                num = Memory.ResolvePointerChain(175281986UL, -1, new int[] { 14464 });
                break;
            case GameBuild.Y1S1_8519860:
                break;
            case GameBuild.Y1S2_9132097:
                num = Memory.ResolvePointerChain(177527042UL, -1, new int[] { 14170 });
                break;
            case GameBuild.Y1S4_10211195:
                num = Memory.ResolvePointerChain(108180802UL, -1, new int[] { 14346 });
                break;
            case GameBuild.Y2S1_10751226:
                num = Memory.ResolvePointerChain(141210818UL, -1, new int[] { 14510 });
                break;
            case GameBuild.Y2S2_11216230:
                num = Memory.ResolvePointerChain(142138994UL, -1, new int[] { 14480 });
                break;
            case GameBuild.Y2S4_11553121:
                num = Memory.ResolvePointerChain(150093074UL, -1, new int[] { 14724 });
                break;
            case GameBuild.Y3S2_11965022:
                num = Memory.ResolvePointerChain(165169250UL, -1, new int[] { 13666 });
                break;
            case GameBuild.Y3S4_12512571:
                num = Memory.ResolvePointerChain(166803730UL, -1, new int[] { 14012 });
                break;
            case GameBuild.Y2S3_11493221:
                num = Memory.ResolvePointerChain(148632002UL, -1, new int[] { 14636 });
                break;
            case GameBuild.Y1S3_9654076:
                num = Memory.ResolvePointerChain(177846434UL, -1, new int[] { 14266 });
                break;
            case GameBuild.Y4S4_13777760:
                num = Memory.ResolvePointerChain(173640962UL, -1, new int[] { 15084 });
                break;
            case GameBuild.Y2S4_11580709:
                num = Memory.ResolvePointerChain(150102034UL, -1, new int[] { 14724 });
                break;
            case GameBuild.Y3S2_11938214:
                num = Memory.ResolvePointerChain(155388946UL, -1, new int[] { 13666 });
                break;
            default:
                return;
        }
        Memory.WriteBool(num, true);
    }

    public bool IsInMatch()
    {
        ulong num;
        switch (_build)
        {
            case GameBuild.Y1S0_8194013:
                num = Memory.ResolvePointerChain(185612930UL, -1, new int[] { 12810 });
                goto IL_0450;
            case GameBuild.Y2S3_11432634:
                num = Memory.ResolvePointerChain(148992066UL, -1, new int[] { 13362 });
                goto IL_0450;
            case GameBuild.Y3S1_11726982:
                num = Memory.ResolvePointerChain(156344802UL, -1, new int[] { 12498 });
                goto IL_0450;
            case GameBuild.Y3S3_12213419:
                num = Memory.ResolvePointerChain(157394466UL, -1, new int[] { 14570 });
                goto IL_0450;
            case GameBuild.Y3S3_12362767:
                num = Memory.ResolvePointerChain(157744626UL, -1, new int[] { 13722 });
                goto IL_0450;
            case GameBuild.Y1S3_9860556:
                num = Memory.ResolvePointerChain(203090562UL, -1, new int[] { 12858 });
                goto IL_0450;
            case GameBuild.Y4S2_13147883:
                num = Memory.ResolvePointerChain(167503682UL, -1, new int[] { 13954 });
                goto IL_0450;
            case GameBuild.Y4S1_12863847:
            case GameBuild.Y4S1_12815133:
                num = Memory.ResolvePointerChain(164803922UL, -1, new int[] { 13770 });
                goto IL_0450;
            case GameBuild.Y4S3_13632147:
                num = Memory.ResolvePointerChain(172280226UL, -1, new int[] { 14530 });
                goto IL_0450;
            case GameBuild.Y4S4_13924517:
                num = Memory.ResolvePointerChain(173385250UL, -1, new int[] { 14866 });
                goto IL_0450;
            case GameBuild.Y5S1_14303219:
                num = Memory.ResolvePointerChain(175281986UL, -1, new int[] { 10002 });
                goto IL_0450;
            case GameBuild.Y1S1_8519860:
                num = Memory.ResolvePointerChain(190831378UL, -1, new int[] { 12810 });
                goto IL_0450;
            case GameBuild.Y1S2_9132097:
                num = Memory.ResolvePointerChain(177527042UL, -1, new int[] { 12794 });
                goto IL_0450;
            case GameBuild.Y1S4_10211195:
                num = Memory.ResolvePointerChain(108180802UL, -1, new int[] { 12938 });
                goto IL_0450;
            case GameBuild.Y2S1_10751226:
                num = Memory.ResolvePointerChain(141210818UL, -1, new int[] { 13082 });
                goto IL_0450;
            case GameBuild.Y2S2_11216230:
                num = Memory.ResolvePointerChain(142138994UL, -1, new int[] { 13266 });
                goto IL_0450;
            case GameBuild.Y2S4_11553121:
                num = Memory.ResolvePointerChain(150093074UL, -1, new int[] { 13410 });
                goto IL_0450;
            case GameBuild.Y3S2_11965022:
                num = Memory.ResolvePointerChain(165169250UL, -1, new int[] { 12274 });
                goto IL_0450;
            case GameBuild.Y3S4_12512571:
                num = Memory.ResolvePointerChain(166803730UL, -1, new int[] { 13802 });
                goto IL_0450;
            case GameBuild.Y2S3_11493221:
                num = Memory.ResolvePointerChain(148632002UL, -1, new int[] { 13362 });
                goto IL_0450;
            case GameBuild.Y1S3_9654076:
                num = Memory.ResolvePointerChain(177846434UL, -1, new int[] { 12858 });
                goto IL_0450;
            case GameBuild.Y4S4_13777760:
                num = Memory.ResolvePointerChain(173640962UL, -1, new int[] { 14866 });
                goto IL_0450;
            case GameBuild.Y2S4_11580709:
                num = Memory.ResolvePointerChain(150102034UL, -1, new int[] { 13410 });
                goto IL_0450;
            case GameBuild.Y3S2_11938214:
                num = Memory.ResolvePointerChain(155388946UL, -1, new int[] { 12274 });
                goto IL_0450;
        }
        return false;
    IL_0450:
        if (_season <= Season.Y1S3_Skull_Rain)
        {
            if (Memory.ReadByteAsInt32(num) == 6)
            {
                return true;
            }
        }
        return _season > Season.Y1S3_Skull_Rain && Memory.ReadByteAsInt32(num) == 7;
    }

    public void SetInfiniteTime(bool enabled)
    {
        ulong num;
        switch (_build)
        {
            case GameBuild.Y1S0_8194013:
                num = Memory.ResolvePointerChain(185612930UL, -1, new int[] { 14090 });
                break;
            case GameBuild.Y2S3_11432634:
                num = Memory.ResolvePointerChain(148992066UL, -1, new int[] { 14602 });
                break;
            case GameBuild.Y3S1_11726982:
                num = Memory.ResolvePointerChain(156344802UL, -1, new int[] { 13826 });
                break;
            case GameBuild.Y3S3_12213419:
                num = Memory.ResolvePointerChain(157394466UL, -1, new int[] { 14746 });
                break;
            case GameBuild.Y3S3_12362767:
                num = Memory.ResolvePointerChain(157744626UL, -1, new int[] { 13898 });
                break;
            case GameBuild.Y1S3_9860556:
                num = Memory.ResolvePointerChain(203090562UL, -1, new int[] { 14234 });
                break;
            case GameBuild.Y4S2_13147883:
                num = Memory.ResolvePointerChain(167503682UL, -1, new int[] { 14130 });
                break;
            case GameBuild.Y4S1_12863847:
            case GameBuild.Y4S1_12815133:
                num = Memory.ResolvePointerChain(164803922UL, -1, new int[] { 13946 });
                break;
            case GameBuild.Y3S1_11706399:
                return;
            case GameBuild.Y4S3_13632147:
                num = Memory.ResolvePointerChain(172280226UL, -1, new int[] { 14716 });
                break;
            case GameBuild.Y4S4_13924517:
                num = Memory.ResolvePointerChain(173385250UL, -1, new int[] { 15050 });
                break;
            case GameBuild.Y5S1_14303219:
                num = Memory.ResolvePointerChain(175281986UL, -1, new int[] { 14434 });
                break;
            case GameBuild.Y1S1_8519860:
                num = Memory.ResolvePointerChain(190831378UL, -1, new int[] { 14138 });
                break;
            case GameBuild.Y1S2_9132097:
                num = Memory.ResolvePointerChain(177527042UL, -1, new int[] { 14138 });
                break;
            case GameBuild.Y1S4_10211195:
                num = Memory.ResolvePointerChain(108180802UL, -1, new int[] { 14314 });
                break;
            case GameBuild.Y2S1_10751226:
                num = Memory.ResolvePointerChain(141210818UL, -1, new int[] { 14474 });
                break;
            case GameBuild.Y2S2_11216230:
                num = Memory.ResolvePointerChain(142138994UL, -1, new int[] { 14442 });
                break;
            case GameBuild.Y2S4_11553121:
                num = Memory.ResolvePointerChain(150093074UL, -1, new int[] { 14690 });
                break;
            case GameBuild.Y3S2_11965022:
                num = Memory.ResolvePointerChain(165169250UL, -1, new int[] { 13634 });
                break;
            case GameBuild.Y3S4_12512571:
                num = Memory.ResolvePointerChain(166803730UL, -1, new int[] { 13978 });
                break;
            case GameBuild.Y2S3_11493221:
                num = Memory.ResolvePointerChain(148632002UL, -1, new int[] { 14602 });
                break;
            case GameBuild.Y1S3_9654076:
                num = Memory.ResolvePointerChain(177846434UL, -1, new int[] { 14234 });
                break;
            case GameBuild.Y4S4_13777760:
                num = Memory.ResolvePointerChain(173640962UL, -1, new int[] { 15050 });
                break;
            case GameBuild.Y2S4_11580709:
                num = Memory.ResolvePointerChain(150102034UL, -1, new int[] { 14690 });
                break;
            case GameBuild.Y3S2_11938214:
                num = Memory.ResolvePointerChain(155388946UL, -1, new int[] { 13634 });
                break;
            default:
                return;
        }
        Memory.WriteBool(num, !enabled);
    }

    public bool GetInfiniteTime()
    {
        ulong num;
        switch (_build)
        {
            case GameBuild.Y1S0_8194013:
                num = Memory.ResolvePointerChain(185612930UL, -1, new int[] { 14090 });
                goto IL_0450;
            case GameBuild.Y2S3_11432634:
                num = Memory.ResolvePointerChain(148992066UL, -1, new int[] { 14602 });
                goto IL_0450;
            case GameBuild.Y3S1_11726982:
                num = Memory.ResolvePointerChain(156344802UL, -1, new int[] { 13826 });
                goto IL_0450;
            case GameBuild.Y3S3_12213419:
                num = Memory.ResolvePointerChain(157394466UL, -1, new int[] { 14746 });
                goto IL_0450;
            case GameBuild.Y3S3_12362767:
                num = Memory.ResolvePointerChain(157744626UL, -1, new int[] { 13898 });
                goto IL_0450;
            case GameBuild.Y1S3_9860556:
                num = Memory.ResolvePointerChain(203090562UL, -1, new int[] { 14234 });
                goto IL_0450;
            case GameBuild.Y4S2_13147883:
                num = Memory.ResolvePointerChain(167503682UL, -1, new int[] { 14130 });
                goto IL_0450;
            case GameBuild.Y4S1_12863847:
            case GameBuild.Y4S1_12815133:
                num = Memory.ResolvePointerChain(164803922UL, -1, new int[] { 13946 });
                goto IL_0450;
            case GameBuild.Y4S3_13632147:
                num = Memory.ResolvePointerChain(172280226UL, -1, new int[] { 14716 });
                goto IL_0450;
            case GameBuild.Y4S4_13924517:
                num = Memory.ResolvePointerChain(173385250UL, -1, new int[] { 15050 });
                goto IL_0450;
            case GameBuild.Y5S1_14303219:
                num = Memory.ResolvePointerChain(175281986UL, -1, new int[] { 14434 });
                goto IL_0450;
            case GameBuild.Y1S1_8519860:
                num = Memory.ResolvePointerChain(190831378UL, -1, new int[] { 14138 });
                goto IL_0450;
            case GameBuild.Y1S2_9132097:
                num = Memory.ResolvePointerChain(177527042UL, -1, new int[] { 14138 });
                goto IL_0450;
            case GameBuild.Y1S4_10211195:
                num = Memory.ResolvePointerChain(108180802UL, -1, new int[] { 14314 });
                goto IL_0450;
            case GameBuild.Y2S1_10751226:
                num = Memory.ResolvePointerChain(141210818UL, -1, new int[] { 14474 });
                goto IL_0450;
            case GameBuild.Y2S2_11216230:
                num = Memory.ResolvePointerChain(142138994UL, -1, new int[] { 14442 });
                goto IL_0450;
            case GameBuild.Y2S4_11553121:
                num = Memory.ResolvePointerChain(150093074UL, -1, new int[] { 14690 });
                goto IL_0450;
            case GameBuild.Y3S2_11965022:
                num = Memory.ResolvePointerChain(165169250UL, -1, new int[] { 13634 });
                goto IL_0450;
            case GameBuild.Y3S4_12512571:
                num = Memory.ResolvePointerChain(166803730UL, -1, new int[] { 13978 });
                goto IL_0450;
            case GameBuild.Y2S3_11493221:
                num = Memory.ResolvePointerChain(148632002UL, -1, new int[] { 14602 });
                goto IL_0450;
            case GameBuild.Y1S3_9654076:
                num = Memory.ResolvePointerChain(177846434UL, -1, new int[] { 14234 });
                goto IL_0450;
            case GameBuild.Y4S4_13777760:
                num = Memory.ResolvePointerChain(173640962UL, -1, new int[] { 15050 });
                goto IL_0450;
            case GameBuild.Y2S4_11580709:
                num = Memory.ResolvePointerChain(150102034UL, -1, new int[] { 14690 });
                goto IL_0450;
            case GameBuild.Y3S2_11938214:
                num = Memory.ResolvePointerChain(155388946UL, -1, new int[] { 13634 });
                goto IL_0450;
        }
        return false;
    IL_0450:
        return !Memory.ReadScrambledBool(num, false);
    }

    public void SetDisablePrimaryWeapon(bool enabled)
    {
        if (enabled)
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    Memory.WriteNops(26913056UL, 5, true, true);
                    return;
                case GameBuild.Y2S3_11432634:
                    Memory.WriteNops(34973556UL, 5, true, true);
                    return;
                case GameBuild.Y3S1_11726982:
                    Memory.WriteNops(45645956UL, 5, true, true);
                    return;
                case GameBuild.Y3S3_12213419:
                    Memory.WriteNops(38509460UL, 5, true, true);
                    return;
                case GameBuild.Y3S3_12362767:
                    Memory.WriteNops(38402164UL, 5, true, true);
                    return;
                case GameBuild.Y1S3_9860556:
                    Memory.WriteNops(27210644UL, 5, true, true);
                    return;
                case GameBuild.Y4S2_13147883:
                    Memory.WriteNops(45820294UL, 5, true, true);
                    return;
                case GameBuild.Y4S1_12863847:
                    Memory.WriteNops(33971134UL, 5, true, true);
                    return;
                case GameBuild.Y3S1_11706399:
                case GameBuild.Y5S1_14303219:
                    break;
                case GameBuild.Y4S3_13632147:
                    Memory.WriteNops(45241408UL, 5, true, true);
                    return;
                case GameBuild.Y4S1_12815133:
                    Memory.WriteNops(33971230UL, 5, true, true);
                    return;
                case GameBuild.Y4S4_13924517:
                    Memory.WriteNops(34200722UL, 5, true, true);
                    return;
                case GameBuild.Y1S1_8519860:
                    Memory.WriteNops(27012798UL, 5, true, true);
                    return;
                case GameBuild.Y1S2_9132097:
                    Memory.WriteNops(26556190UL, 5, true, true);
                    return;
                case GameBuild.Y1S4_10211195:
                    Memory.WriteNops(28312916UL, 5, true, true);
                    return;
                case GameBuild.Y2S1_10751226:
                    Memory.WriteNops(30650100UL, 5, true, true);
                    return;
                case GameBuild.Y2S2_11216230:
                    Memory.WriteNops(31083892UL, 5, true, true);
                    return;
                case GameBuild.Y2S4_11553121:
                    Memory.WriteNops(35071204UL, 5, true, true);
                    return;
                case GameBuild.Y3S2_11965022:
                    Memory.WriteNops(43741796UL, 5, true, true);
                    return;
                case GameBuild.Y3S4_12512571:
                    Memory.WriteNops(37185694UL, 5, true, true);
                    return;
                case GameBuild.Y2S3_11493221:
                    Memory.WriteNops(35215508UL, 5, true, true);
                    return;
                case GameBuild.Y1S3_9654076:
                    Memory.WriteNops(27165428UL, 5, true, true);
                    return;
                case GameBuild.Y4S4_13777760:
                    Memory.WriteNops(32533682UL, 5, true, true);
                    return;
                case GameBuild.Y2S4_11580709:
                    Memory.WriteNops(35066628UL, 5, true, true);
                    return;
                case GameBuild.Y3S2_11938214:
                    Memory.WriteNops(43711044UL, 5, true, true);
                    return;
                default:
                    return;
            }
        }
        else
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    {
                        MemoryHandler field = Memory;
                        ulong num = 26913056UL;
                        bool flag = true;
                        int[] array = new int[] { 219, 129, 223, 237, 242 };
                        field.WriteScrambledInts(num, flag, array);
                        return;
                    }
                case GameBuild.Y2S3_11432634:
                    {
                        MemoryHandler field2 = Memory;
                        ulong num2 = 34973556UL;
                        bool flag2 = true;
                        int[] array2 = new int[] { 219, 87, 223, 238, 242 };
                        field2.WriteScrambledInts(num2, flag2, array2);
                        return;
                    }
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field3 = Memory;
                        ulong num3 = 45645956UL;
                        bool flag3 = true;
                        int[] array3 = new int[] { 219, 159, 180, 166, 242 };
                        field3.WriteScrambledInts(num3, flag3, array3);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field4 = Memory;
                        ulong num4 = 38509460UL;
                        bool flag4 = true;
                        int[] array4 = new int[] { 219, 71, -8, 239, 242 };
                        field4.WriteScrambledInts(num4, flag4, array4);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field5 = Memory;
                        ulong num5 = 38402164UL;
                        bool flag5 = true;
                        int[] array5 = new int[] { 219, 39, 230, 238, 242 };
                        field5.WriteScrambledInts(num5, flag5, array5);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                    {
                        MemoryHandler field6 = Memory;
                        ulong num6 = 27210644UL;
                        bool flag6 = true;
                        int[] array6 = new int[] { 219, 87, 13, 238, 242 };
                        field6.WriteScrambledInts(num6, flag6, array6);
                        return;
                    }
                case GameBuild.Y4S2_13147883:
                    {
                        MemoryHandler field7 = Memory;
                        ulong num7 = 45820294UL;
                        bool flag7 = true;
                        int[] array7 = new int[] { 219, 110, 169, 237, 242 };
                        field7.WriteScrambledInts(num7, flag7, array7);
                        return;
                    }
                case GameBuild.Y4S1_12863847:
                    {
                        MemoryHandler field8 = Memory;
                        ulong num8 = 33971134UL;
                        bool flag8 = true;
                        int[] array8 = new int[] { 219, 114, 58, 30, -13 };
                        field8.WriteScrambledInts(num8, flag8, array8);
                        return;
                    }
                case GameBuild.Y3S1_11706399:
                case GameBuild.Y5S1_14303219:
                    break;
                case GameBuild.Y4S3_13632147:
                    {
                        MemoryHandler field9 = Memory;
                        ulong num9 = 45241408UL;
                        bool flag9 = true;
                        int[] array9 = new int[] { 219, 241, 191, 236, 242 };
                        field9.WriteScrambledInts(num9, flag9, array9);
                        return;
                    }
                case GameBuild.Y4S1_12815133:
                    {
                        MemoryHandler field10 = Memory;
                        ulong num10 = 33971230UL;
                        bool flag10 = true;
                        int[] array10 = new int[] { 219, 114, 58, 30, -13 };
                        field10.WriteScrambledInts(num10, flag10, array10);
                        return;
                    }
                case GameBuild.Y4S4_13924517:
                    {
                        MemoryHandler field11 = Memory;
                        ulong num11 = 34200722UL;
                        bool flag11 = true;
                        int[] array11 = new int[] { 219, 152, 152, 215, 242 };
                        field11.WriteScrambledInts(num11, flag11, array11);
                        break;
                    }
                case GameBuild.Y1S1_8519860:
                    {
                        MemoryHandler field12 = Memory;
                        ulong num12 = 27012798UL;
                        bool flag12 = true;
                        int[] array12 = new int[] { 219, 34, 199, 237, 242 };
                        field12.WriteScrambledInts(num12, flag12, array12);
                        return;
                    }
                case GameBuild.Y1S2_9132097:
                    {
                        MemoryHandler field13 = Memory;
                        ulong num13 = 26556190UL;
                        bool flag13 = true;
                        int[] array13 = new int[] { 219, 130, 214, 237, 242 };
                        field13.WriteScrambledInts(num13, flag13, array13);
                        return;
                    }
                case GameBuild.Y1S4_10211195:
                    {
                        MemoryHandler field14 = Memory;
                        ulong num14 = 28312916UL;
                        bool flag14 = true;
                        int[] array14 = new int[] { 219, 167, 40, 237, 242 };
                        field14.WriteScrambledInts(num14, flag14, array14);
                        return;
                    }
                case GameBuild.Y2S1_10751226:
                    {
                        MemoryHandler field15 = Memory;
                        ulong num15 = 30650100UL;
                        bool flag15 = true;
                        int[] array15 = new int[] { 219, 71, 148, 236, 242 };
                        field15.WriteScrambledInts(num15, flag15, array15);
                        return;
                    }
                case GameBuild.Y2S2_11216230:
                    {
                        MemoryHandler field16 = Memory;
                        ulong num16 = 31083892UL;
                        bool flag16 = true;
                        int[] array16 = new int[] { 219, 231, 130, 236, 242 };
                        field16.WriteScrambledInts(num16, flag16, array16);
                        return;
                    }
                case GameBuild.Y2S4_11553121:
                    {
                        MemoryHandler field17 = Memory;
                        ulong num17 = 35071204UL;
                        bool flag17 = true;
                        int[] array17 = new int[] { 219, 127, 185, 21, -13 };
                        field17.WriteScrambledInts(num17, flag17, array17);
                        return;
                    }
                case GameBuild.Y3S2_11965022:
                    {
                        MemoryHandler field18 = Memory;
                        ulong num18 = 43741796UL;
                        bool flag18 = true;
                        int[] array18 = new int[] { 219, 191, 27, 180, 242 };
                        field18.WriteScrambledInts(num18, flag18, array18);
                        return;
                    }
                case GameBuild.Y3S4_12512571:
                    {
                        MemoryHandler field19 = Memory;
                        ulong num19 = 37185694UL;
                        bool flag19 = true;
                        int[] array19 = new int[] { 219, 2, 4, 238, 242 };
                        field19.WriteScrambledInts(num19, flag19, array19);
                        return;
                    }
                case GameBuild.Y2S3_11493221:
                    {
                        MemoryHandler field20 = Memory;
                        ulong num20 = 35215508UL;
                        bool flag20 = true;
                        int[] array20 = new int[] { 219, 135, 223, 238, 242 };
                        field20.WriteScrambledInts(num20, flag20, array20);
                        return;
                    }
                case GameBuild.Y1S3_9654076:
                    {
                        MemoryHandler field21 = Memory;
                        ulong num21 = 27165428UL;
                        bool flag21 = true;
                        int[] array21 = new int[] { 219, 23, 12, 238, 242 };
                        field21.WriteScrambledInts(num21, flag21, array21);
                        return;
                    }
                case GameBuild.Y4S4_13777760:
                    {
                        MemoryHandler field22 = Memory;
                        ulong num22 = 32533682UL;
                        bool flag22 = true;
                        int[] array22 = new int[] { 219, 40, 54, 236, 242 };
                        field22.WriteScrambledInts(num22, flag22, array22);
                        return;
                    }
                case GameBuild.Y2S4_11580709:
                    {
                        MemoryHandler field23 = Memory;
                        ulong num23 = 35066628UL;
                        bool flag23 = true;
                        int[] array23 = new int[] { 219, 143, 186, 21, -13 };
                        field23.WriteScrambledInts(num23, flag23, array23);
                        return;
                    }
                case GameBuild.Y3S2_11938214:
                    {
                        MemoryHandler field24 = Memory;
                        ulong num24 = 43711044UL;
                        bool flag24 = true;
                        int[] array24 = new int[] { 219, 47, 23, 180, 242 };
                        field24.WriteScrambledInts(num24, flag24, array24);
                        return;
                    }
                default:
                    return;
            }
        }
    }

    public void SetDisableSecondaryWeapon(bool enabled)
    {
        if (enabled)
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    Memory.WriteNops(26913130UL, 5, true, true);
                    return;
                case GameBuild.Y2S3_11432634:
                    Memory.WriteNops(34973622UL, 5, true, true);
                    return;
                case GameBuild.Y3S1_11726982:
                    Memory.WriteNops(45646008UL, 5, true, true);
                    return;
                case GameBuild.Y3S3_12213419:
                    Memory.WriteNops(38509526UL, 5, true, true);
                    return;
                case GameBuild.Y3S3_12362767:
                    Memory.WriteNops(38402230UL, 5, true, true);
                    return;
                case GameBuild.Y1S3_9860556:
                    Memory.WriteNops(27210710UL, 5, true, true);
                    return;
                case GameBuild.Y4S2_13147883:
                    Memory.WriteNops(45820360UL, 5, true, true);
                    return;
                case GameBuild.Y4S1_12863847:
                    Memory.WriteNops(33971200UL, 5, true, true);
                    return;
                case GameBuild.Y3S1_11706399:
                case GameBuild.Y5S1_14303219:
                    break;
                case GameBuild.Y4S3_13632147:
                    Memory.WriteNops(45241474UL, 5, true, true);
                    return;
                case GameBuild.Y4S1_12815133:
                    Memory.WriteNops(33971296UL, 5, true, true);
                    return;
                case GameBuild.Y4S4_13924517:
                    Memory.WriteNops(34200788UL, 5, true, true);
                    return;
                case GameBuild.Y1S1_8519860:
                    Memory.WriteNops(27012864UL, 5, true, true);
                    return;
                case GameBuild.Y1S2_9132097:
                    Memory.WriteNops(26556256UL, 5, true, true);
                    return;
                case GameBuild.Y1S4_10211195:
                    Memory.WriteNops(28312982UL, 5, true, true);
                    return;
                case GameBuild.Y2S1_10751226:
                    Memory.WriteNops(30650166UL, 5, true, true);
                    return;
                case GameBuild.Y2S2_11216230:
                    Memory.WriteNops(31083958UL, 5, true, true);
                    return;
                case GameBuild.Y2S4_11553121:
                    Memory.WriteNops(35071256UL, 5, true, true);
                    return;
                case GameBuild.Y3S2_11965022:
                    Memory.WriteNops(43741848UL, 5, true, true);
                    return;
                case GameBuild.Y3S4_12512571:
                    Memory.WriteNops(37185760UL, 5, true, true);
                    return;
                case GameBuild.Y2S3_11493221:
                    Memory.WriteNops(35215574UL, 5, true, true);
                    return;
                case GameBuild.Y1S3_9654076:
                    Memory.WriteNops(27165494UL, 5, true, true);
                    return;
                case GameBuild.Y4S4_13777760:
                    Memory.WriteNops(32533748UL, 5, true, true);
                    return;
                case GameBuild.Y2S4_11580709:
                    Memory.WriteNops(35066680UL, 5, true, true);
                    return;
                case GameBuild.Y3S2_11938214:
                    Memory.WriteNops(43711096UL, 5, true, true);
                    return;
                default:
                    return;
            }
        }
        else
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    {
                        MemoryHandler field = Memory;
                        ulong num = 26913130UL;
                        bool flag = true;
                        int[] array = new int[] { 219, 92, 223, 237, 242 };
                        field.WriteScrambledInts(num, flag, array);
                        return;
                    }
                case GameBuild.Y2S3_11432634:
                    {
                        MemoryHandler field2 = Memory;
                        ulong num2 = 34973622UL;
                        bool flag2 = true;
                        int[] array2 = new int[] { 219, 54, 223, 238, 242 };
                        field2.WriteScrambledInts(num2, flag2, array2);
                        return;
                    }
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field3 = Memory;
                        ulong num3 = 45646008UL;
                        bool flag3 = true;
                        int[] array3 = new int[] { 219, 133, 180, 166, 242 };
                        field3.WriteScrambledInts(num3, flag3, array3);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field4 = Memory;
                        ulong num4 = 38509526UL;
                        bool flag4 = true;
                        int[] array4 = new int[] { 219, 38, -8, 239, 242 };
                        field4.WriteScrambledInts(num4, flag4, array4);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field5 = Memory;
                        ulong num5 = 38402230UL;
                        bool flag5 = true;
                        int[] array5 = new int[] { 219, 6, 230, 238, 242 };
                        field5.WriteScrambledInts(num5, flag5, array5);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                    {
                        MemoryHandler field6 = Memory;
                        ulong num6 = 27210710UL;
                        bool flag6 = true;
                        int[] array6 = new int[] { 219, 54, 13, 238, 242 };
                        field6.WriteScrambledInts(num6, flag6, array6);
                        return;
                    }
                case GameBuild.Y4S2_13147883:
                    {
                        MemoryHandler field7 = Memory;
                        ulong num7 = 45820360UL;
                        bool flag7 = true;
                        int[] array7 = new int[] { 219, 77, 169, 237, 242 };
                        field7.WriteScrambledInts(num7, flag7, array7);
                        return;
                    }
                case GameBuild.Y4S1_12863847:
                    {
                        MemoryHandler field8 = Memory;
                        ulong num8 = 33971200UL;
                        bool flag8 = true;
                        int[] array8 = new int[] { 219, 81, 58, 30, -13 };
                        field8.WriteScrambledInts(num8, flag8, array8);
                        return;
                    }
                case GameBuild.Y3S1_11706399:
                case GameBuild.Y5S1_14303219:
                    break;
                case GameBuild.Y4S3_13632147:
                    {
                        MemoryHandler field9 = Memory;
                        ulong num9 = 45241474UL;
                        bool flag9 = true;
                        int[] array9 = new int[] { 219, 208, 191, 236, 242 };
                        field9.WriteScrambledInts(num9, flag9, array9);
                        return;
                    }
                case GameBuild.Y4S1_12815133:
                    {
                        MemoryHandler field10 = Memory;
                        ulong num10 = 33971296UL;
                        bool flag10 = true;
                        int[] array10 = new int[] { 219, 81, 58, 30, -13 };
                        field10.WriteScrambledInts(num10, flag10, array10);
                        return;
                    }
                case GameBuild.Y4S4_13924517:
                    {
                        MemoryHandler field11 = Memory;
                        ulong num11 = 34200788UL;
                        bool flag11 = true;
                        int[] array11 = new int[] { 219, 119, 152, 215, 242 };
                        field11.WriteScrambledInts(num11, flag11, array11);
                        break;
                    }
                case GameBuild.Y1S1_8519860:
                    {
                        MemoryHandler field12 = Memory;
                        ulong num12 = 27012864UL;
                        bool flag12 = true;
                        int[] array12 = new int[] { 219, 1, 199, 237, 242 };
                        field12.WriteScrambledInts(num12, flag12, array12);
                        return;
                    }
                case GameBuild.Y1S2_9132097:
                    {
                        MemoryHandler field13 = Memory;
                        ulong num13 = 26556256UL;
                        bool flag13 = true;
                        int[] array13 = new int[] { 219, 97, 214, 237, 242 };
                        field13.WriteScrambledInts(num13, flag13, array13);
                        return;
                    }
                case GameBuild.Y1S4_10211195:
                    {
                        MemoryHandler field14 = Memory;
                        ulong num14 = 28312982UL;
                        bool flag14 = true;
                        int[] array14 = new int[] { 219, 134, 40, 237, 242 };
                        field14.WriteScrambledInts(num14, flag14, array14);
                        return;
                    }
                case GameBuild.Y2S1_10751226:
                    {
                        MemoryHandler field15 = Memory;
                        ulong num15 = 30650166UL;
                        bool flag15 = true;
                        int[] array15 = new int[] { 219, 38, 148, 236, 242 };
                        field15.WriteScrambledInts(num15, flag15, array15);
                        return;
                    }
                case GameBuild.Y2S2_11216230:
                    {
                        MemoryHandler field16 = Memory;
                        ulong num16 = 31083958UL;
                        bool flag16 = true;
                        int[] array16 = new int[] { 219, 198, 130, 236, 242 };
                        field16.WriteScrambledInts(num16, flag16, array16);
                        return;
                    }
                case GameBuild.Y2S4_11553121:
                    {
                        MemoryHandler field17 = Memory;
                        ulong num17 = 35071256UL;
                        bool flag17 = true;
                        int[] array17 = new int[] { 219, 101, 185, 21, -13 };
                        field17.WriteScrambledInts(num17, flag17, array17);
                        return;
                    }
                case GameBuild.Y3S2_11965022:
                    {
                        MemoryHandler field18 = Memory;
                        ulong num18 = 43741848UL;
                        bool flag18 = true;
                        int[] array18 = new int[] { 219, 165, 27, 180, 242 };
                        field18.WriteScrambledInts(num18, flag18, array18);
                        return;
                    }
                case GameBuild.Y3S4_12512571:
                    {
                        MemoryHandler field19 = Memory;
                        ulong num19 = 37185760UL;
                        bool flag19 = true;
                        int[] array19 = new int[] { 219, 225, 3, 238, 242 };
                        field19.WriteScrambledInts(num19, flag19, array19);
                        return;
                    }
                case GameBuild.Y2S3_11493221:
                    {
                        MemoryHandler field20 = Memory;
                        ulong num20 = 35215574UL;
                        bool flag20 = true;
                        int[] array20 = new int[] { 219, 102, 223, 238, 242 };
                        field20.WriteScrambledInts(num20, flag20, array20);
                        return;
                    }
                case GameBuild.Y1S3_9654076:
                    {
                        MemoryHandler field21 = Memory;
                        ulong num21 = 27165494UL;
                        bool flag21 = true;
                        int[] array21 = new int[] { 219, -10, 12, 238, 242 };
                        field21.WriteScrambledInts(num21, flag21, array21);
                        return;
                    }
                case GameBuild.Y4S4_13777760:
                    {
                        MemoryHandler field22 = Memory;
                        ulong num22 = 32533748UL;
                        bool flag22 = true;
                        int[] array22 = new int[] { 219, 7, 54, 236, 242 };
                        field22.WriteScrambledInts(num22, flag22, array22);
                        return;
                    }
                case GameBuild.Y2S4_11580709:
                    {
                        MemoryHandler field23 = Memory;
                        ulong num23 = 35066680UL;
                        bool flag23 = true;
                        int[] array23 = new int[] { 219, 117, 186, 21, -13 };
                        field23.WriteScrambledInts(num23, flag23, array23);
                        return;
                    }
                case GameBuild.Y3S2_11938214:
                    {
                        MemoryHandler field24 = Memory;
                        ulong num24 = 43711096UL;
                        bool flag24 = true;
                        int[] array24 = new int[] { 219, 21, 23, 180, 242 };
                        field24.WriteScrambledInts(num24, flag24, array24);
                        return;
                    }
                default:
                    return;
            }
        }
    }

    public void SetSpecialAbility(bool enabled)
    {
        if (enabled)
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    Memory.WriteNops(26913204UL, 5, true, true);
                    return;
                case GameBuild.Y2S3_11432634:
                    Memory.WriteNops(34973688UL, 5, true, true);
                    return;
                case GameBuild.Y3S1_11726982:
                    Memory.WriteNops(45646060UL, 5, true, true);
                    return;
                case GameBuild.Y3S3_12213419:
                    Memory.WriteNops(38509592UL, 5, true, true);
                    return;
                case GameBuild.Y3S3_12362767:
                    Memory.WriteNops(38402296UL, 5, true, true);
                    return;
                case GameBuild.Y1S3_9860556:
                    Memory.WriteNops(27210776UL, 5, true, true);
                    return;
                case GameBuild.Y4S2_13147883:
                    Memory.WriteNops(45821102UL, 5, true, true);
                    return;
                case GameBuild.Y4S1_12863847:
                    Memory.WriteNops(33971744UL, 5, true, true);
                    return;
                case GameBuild.Y3S1_11706399:
                case GameBuild.Y5S1_14303219:
                    break;
                case GameBuild.Y4S3_13632147:
                    Memory.WriteNops(45242272UL, 5, true, true);
                    return;
                case GameBuild.Y4S1_12815133:
                    Memory.WriteNops(33971840UL, 5, true, true);
                    return;
                case GameBuild.Y4S4_13924517:
                    Memory.WriteNops(34201336UL, 5, true, true);
                    return;
                case GameBuild.Y1S1_8519860:
                    Memory.WriteNops(27012930UL, 5, true, true);
                    return;
                case GameBuild.Y1S2_9132097:
                    Memory.WriteNops(26556322UL, 5, true, true);
                    return;
                case GameBuild.Y1S4_10211195:
                    Memory.WriteNops(28313048UL, 5, true, true);
                    return;
                case GameBuild.Y2S1_10751226:
                    Memory.WriteNops(30650232UL, 5, true, true);
                    return;
                case GameBuild.Y2S2_11216230:
                    Memory.WriteNops(31084024UL, 5, true, true);
                    return;
                case GameBuild.Y2S4_11553121:
                    Memory.WriteNops(35071308UL, 5, true, true);
                    return;
                case GameBuild.Y3S2_11965022:
                    Memory.WriteNops(43741900UL, 5, true, true);
                    return;
                case GameBuild.Y3S4_12512571:
                    Memory.WriteNops(37185874UL, 5, true, true);
                    return;
                case GameBuild.Y2S3_11493221:
                    Memory.WriteNops(35215640UL, 5, true, true);
                    return;
                case GameBuild.Y1S3_9654076:
                    Memory.WriteNops(27165560UL, 5, true, true);
                    return;
                case GameBuild.Y4S4_13777760:
                    Memory.WriteNops(32534296UL, 5, true, true);
                    return;
                case GameBuild.Y2S4_11580709:
                    Memory.WriteNops(35066732UL, 5, true, true);
                    return;
                case GameBuild.Y3S2_11938214:
                    Memory.WriteNops(43711148UL, 5, true, true);
                    return;
                default:
                    return;
            }
        }
        else
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    {
                        MemoryHandler field = Memory;
                        ulong num = 26913204UL;
                        bool flag = true;
                        int[] array = new int[] { 219, 55, 223, 237, 242 };
                        field.WriteScrambledInts(num, flag, array);
                        return;
                    }
                case GameBuild.Y2S3_11432634:
                    {
                        MemoryHandler field2 = Memory;
                        ulong num2 = 34973688UL;
                        bool flag2 = true;
                        int[] array2 = new int[] { 219, 21, 223, 238, 242 };
                        field2.WriteScrambledInts(num2, flag2, array2);
                        return;
                    }
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field3 = Memory;
                        ulong num3 = 45646060UL;
                        bool flag3 = true;
                        int[] array3 = new int[] { 219, 107, 180, 166, 242 };
                        field3.WriteScrambledInts(num3, flag3, array3);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field4 = Memory;
                        ulong num4 = 38509592UL;
                        bool flag4 = true;
                        int[] array4 = new int[] { 219, 5, -8, 239, 242 };
                        field4.WriteScrambledInts(num4, flag4, array4);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field5 = Memory;
                        ulong num5 = 38402296UL;
                        bool flag5 = true;
                        int[] array5 = new int[] { 219, 229, 229, 238, 242 };
                        field5.WriteScrambledInts(num5, flag5, array5);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                    {
                        MemoryHandler field6 = Memory;
                        ulong num6 = 27210776UL;
                        bool flag6 = true;
                        int[] array6 = new int[] { 219, 21, 13, 238, 242 };
                        field6.WriteScrambledInts(num6, flag6, array6);
                        return;
                    }
                case GameBuild.Y4S2_13147883:
                    {
                        MemoryHandler field7 = Memory;
                        ulong num7 = 45821102UL;
                        bool flag7 = true;
                        int[] array7 = new int[] { 219, 218, 167, 237, 242 };
                        field7.WriteScrambledInts(num7, flag7, array7);
                        return;
                    }
                case GameBuild.Y4S1_12863847:
                    {
                        MemoryHandler field8 = Memory;
                        ulong num8 = 33971744UL;
                        bool flag8 = true;
                        int[] array8 = new int[] { 219, 65, 57, 30, -13 };
                        field8.WriteScrambledInts(num8, flag8, array8);
                        return;
                    }
                case GameBuild.Y3S1_11706399:
                case GameBuild.Y5S1_14303219:
                    break;
                case GameBuild.Y4S3_13632147:
                    {
                        MemoryHandler field9 = Memory;
                        ulong num9 = 45242272UL;
                        bool flag9 = true;
                        int[] array9 = new int[] { 219, 65, 190, 236, 242 };
                        field9.WriteScrambledInts(num9, flag9, array9);
                        return;
                    }
                case GameBuild.Y4S1_12815133:
                    {
                        MemoryHandler field10 = Memory;
                        ulong num10 = 33971840UL;
                        bool flag10 = true;
                        int[] array10 = new int[] { 219, 65, 57, 30, -13 };
                        field10.WriteScrambledInts(num10, flag10, array10);
                        return;
                    }
                case GameBuild.Y4S4_13924517:
                    {
                        MemoryHandler field11 = Memory;
                        ulong num11 = 34201336UL;
                        bool flag11 = true;
                        int[] array11 = new int[] { 219, 101, 151, 215, 242 };
                        field11.WriteScrambledInts(num11, flag11, array11);
                        break;
                    }
                case GameBuild.Y1S1_8519860:
                    {
                        MemoryHandler field12 = Memory;
                        ulong num12 = 27012930UL;
                        bool flag12 = true;
                        int[] array12 = new int[] { 219, 224, 198, 237, 242 };
                        field12.WriteScrambledInts(num12, flag12, array12);
                        return;
                    }
                case GameBuild.Y1S2_9132097:
                    {
                        MemoryHandler field13 = Memory;
                        ulong num13 = 26556322UL;
                        bool flag13 = true;
                        int[] array13 = new int[] { 219, 64, 214, 237, 242 };
                        field13.WriteScrambledInts(num13, flag13, array13);
                        return;
                    }
                case GameBuild.Y1S4_10211195:
                    {
                        MemoryHandler field14 = Memory;
                        ulong num14 = 28313048UL;
                        bool flag14 = true;
                        int[] array14 = new int[] { 219, 101, 40, 237, 242 };
                        field14.WriteScrambledInts(num14, flag14, array14);
                        return;
                    }
                case GameBuild.Y2S1_10751226:
                    {
                        MemoryHandler field15 = Memory;
                        ulong num15 = 30650232UL;
                        bool flag15 = true;
                        int[] array15 = new int[] { 219, 5, 148, 236, 242 };
                        field15.WriteScrambledInts(num15, flag15, array15);
                        return;
                    }
                case GameBuild.Y2S2_11216230:
                    {
                        MemoryHandler field16 = Memory;
                        ulong num16 = 31084024UL;
                        bool flag16 = true;
                        int[] array16 = new int[] { 219, 165, 130, 236, 242 };
                        field16.WriteScrambledInts(num16, flag16, array16);
                        return;
                    }
                case GameBuild.Y2S4_11553121:
                    {
                        MemoryHandler field17 = Memory;
                        ulong num17 = 35071308UL;
                        bool flag17 = true;
                        int[] array17 = new int[] { 219, 75, 185, 21, -13 };
                        field17.WriteScrambledInts(num17, flag17, array17);
                        return;
                    }
                case GameBuild.Y3S2_11965022:
                    {
                        MemoryHandler field18 = Memory;
                        ulong num18 = 43741900UL;
                        bool flag18 = true;
                        int[] array18 = new int[] { 219, 139, 27, 180, 242 };
                        field18.WriteScrambledInts(num18, flag18, array18);
                        return;
                    }
                case GameBuild.Y3S4_12512571:
                    {
                        MemoryHandler field19 = Memory;
                        ulong num19 = 37185874UL;
                        bool flag19 = true;
                        int[] array19 = new int[] { 219, 168, 3, 238, 242 };
                        field19.WriteScrambledInts(num19, flag19, array19);
                        return;
                    }
                case GameBuild.Y2S3_11493221:
                    {
                        MemoryHandler field20 = Memory;
                        ulong num20 = 35215640UL;
                        bool flag20 = true;
                        int[] array20 = new int[] { 219, 69, 223, 238, 242 };
                        field20.WriteScrambledInts(num20, flag20, array20);
                        return;
                    }
                case GameBuild.Y1S3_9654076:
                    {
                        MemoryHandler field21 = Memory;
                        ulong num21 = 27165560UL;
                        bool flag21 = true;
                        int[] array21 = new int[] { 219, 213, 11, 238, 242 };
                        field21.WriteScrambledInts(num21, flag21, array21);
                        return;
                    }
                case GameBuild.Y4S4_13777760:
                    {
                        MemoryHandler field22 = Memory;
                        ulong num22 = 32534296UL;
                        bool flag22 = true;
                        int[] array22 = new int[] { 219, -11, 53, 236, 242 };
                        field22.WriteScrambledInts(num22, flag22, array22);
                        return;
                    }
                case GameBuild.Y2S4_11580709:
                    {
                        MemoryHandler field23 = Memory;
                        ulong num23 = 35066732UL;
                        bool flag23 = true;
                        int[] array23 = new int[] { 219, 91, 186, 21, -13 };
                        field23.WriteScrambledInts(num23, flag23, array23);
                        return;
                    }
                case GameBuild.Y3S2_11938214:
                    {
                        MemoryHandler field24 = Memory;
                        ulong num24 = 43711148UL;
                        bool flag24 = true;
                        int[] array24 = new int[] { 219, -5, 23, 180, 242 };
                        field24.WriteScrambledInts(num24, flag24, array24);
                        return;
                    }
                default:
                    return;
            }
        }
    }

    public void SetDisableGadget(bool enabled)
    {
        if (enabled)
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    Memory.WriteNops(26913278UL, 5, true, true);
                    return;
                case GameBuild.Y2S3_11432634:
                    Memory.WriteNops(34973754UL, 5, true, true);
                    return;
                case GameBuild.Y3S1_11726982:
                    Memory.WriteNops(45646112UL, 5, true, true);
                    return;
                case GameBuild.Y3S3_12213419:
                    Memory.WriteNops(38509658UL, 5, true, true);
                    return;
                case GameBuild.Y3S3_12362767:
                    Memory.WriteNops(38402362UL, 5, true, true);
                    return;
                case GameBuild.Y1S3_9860556:
                    Memory.WriteNops(27210842UL, 5, true, true);
                    return;
                case GameBuild.Y4S2_13147883:
                    Memory.WriteNops(45821168UL, 5, true, true);
                    return;
                case GameBuild.Y4S1_12863847:
                    Memory.WriteNops(33971810UL, 5, true, true);
                    return;
                case GameBuild.Y3S1_11706399:
                case GameBuild.Y5S1_14303219:
                    break;
                case GameBuild.Y4S3_13632147:
                    Memory.WriteNops(45242338UL, 5, true, true);
                    return;
                case GameBuild.Y4S1_12815133:
                    Memory.WriteNops(33971906UL, 5, true, true);
                    return;
                case GameBuild.Y4S4_13924517:
                    Memory.WriteNops(34201402UL, 5, true, true);
                    return;
                case GameBuild.Y1S1_8519860:
                    Memory.WriteNops(27012996UL, 5, true, true);
                    return;
                case GameBuild.Y1S2_9132097:
                    Memory.WriteNops(26556388UL, 5, true, true);
                    return;
                case GameBuild.Y1S4_10211195:
                    Memory.WriteNops(28313114UL, 5, true, true);
                    return;
                case GameBuild.Y2S1_10751226:
                    Memory.WriteNops(30650298UL, 5, true, true);
                    return;
                case GameBuild.Y2S2_11216230:
                    Memory.WriteNops(31084090UL, 5, true, true);
                    return;
                case GameBuild.Y2S4_11553121:
                    Memory.WriteNops(35071360UL, 5, true, true);
                    return;
                case GameBuild.Y3S2_11965022:
                    Memory.WriteNops(43741952UL, 5, true, true);
                    return;
                case GameBuild.Y3S4_12512571:
                    Memory.WriteNops(37185940UL, 5, true, true);
                    return;
                case GameBuild.Y2S3_11493221:
                    Memory.WriteNops(35215706UL, 5, true, true);
                    return;
                case GameBuild.Y1S3_9654076:
                    Memory.WriteNops(27165626UL, 5, true, true);
                    return;
                case GameBuild.Y4S4_13777760:
                    Memory.WriteNops(32534362UL, 5, true, true);
                    return;
                case GameBuild.Y2S4_11580709:
                    Memory.WriteNops(35066784UL, 5, true, true);
                    return;
                case GameBuild.Y3S2_11938214:
                    Memory.WriteNops(43711200UL, 5, true, true);
                    return;
                default:
                    return;
            }
        }
        else
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    {
                        MemoryHandler field = Memory;
                        ulong num = 26913278UL;
                        bool flag = true;
                        int[] array = new int[] { 219, 18, 223, 237, 242 };
                        field.WriteScrambledInts(num, flag, array);
                        return;
                    }
                case GameBuild.Y2S3_11432634:
                    {
                        MemoryHandler field2 = Memory;
                        ulong num2 = 34973754UL;
                        bool flag2 = true;
                        int[] array2 = new int[] { 219, -12, 223, 238, 242 };
                        field2.WriteScrambledInts(num2, flag2, array2);
                        return;
                    }
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field3 = Memory;
                        ulong num3 = 45646112UL;
                        bool flag3 = true;
                        int[] array3 = new int[] { 219, 81, 180, 166, 242 };
                        field3.WriteScrambledInts(num3, flag3, array3);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field4 = Memory;
                        ulong num4 = 38509658UL;
                        bool flag4 = true;
                        int[] array4 = new int[] { 219, 228, -9, 239, 242 };
                        field4.WriteScrambledInts(num4, flag4, array4);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field5 = Memory;
                        ulong num5 = 38402362UL;
                        bool flag5 = true;
                        int[] array5 = new int[] { 219, 196, 229, 238, 242 };
                        field5.WriteScrambledInts(num5, flag5, array5);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                    {
                        MemoryHandler field6 = Memory;
                        ulong num6 = 27210842UL;
                        bool flag6 = true;
                        int[] array6 = new int[] { 219, -12, 13, 238, 242 };
                        field6.WriteScrambledInts(num6, flag6, array6);
                        return;
                    }
                case GameBuild.Y4S2_13147883:
                    {
                        MemoryHandler field7 = Memory;
                        ulong num7 = 45821168UL;
                        bool flag7 = true;
                        int[] array7 = new int[] { 219, 185, 167, 237, 242 };
                        field7.WriteScrambledInts(num7, flag7, array7);
                        return;
                    }
                case GameBuild.Y4S1_12863847:
                    {
                        MemoryHandler field8 = Memory;
                        ulong num8 = 33971810UL;
                        bool flag8 = true;
                        int[] array8 = new int[] { 219, 32, 57, 30, -13 };
                        field8.WriteScrambledInts(num8, flag8, array8);
                        return;
                    }
                case GameBuild.Y3S1_11706399:
                case GameBuild.Y5S1_14303219:
                    break;
                case GameBuild.Y4S3_13632147:
                    {
                        MemoryHandler field9 = Memory;
                        ulong num9 = 45242338UL;
                        bool flag9 = true;
                        int[] array9 = new int[] { 219, 32, 190, 236, 242 };
                        field9.WriteScrambledInts(num9, flag9, array9);
                        return;
                    }
                case GameBuild.Y4S1_12815133:
                    {
                        MemoryHandler field10 = Memory;
                        ulong num10 = 33971906UL;
                        bool flag10 = true;
                        int[] array10 = new int[] { 219, 32, 57, 30, -13 };
                        field10.WriteScrambledInts(num10, flag10, array10);
                        return;
                    }
                case GameBuild.Y4S4_13924517:
                    {
                        MemoryHandler field11 = Memory;
                        ulong num11 = 34201402UL;
                        bool flag11 = true;
                        int[] array11 = new int[] { 219, 68, 151, 215, 242 };
                        field11.WriteScrambledInts(num11, flag11, array11);
                        break;
                    }
                case GameBuild.Y1S1_8519860:
                    {
                        MemoryHandler field12 = Memory;
                        ulong num12 = 27012996UL;
                        bool flag12 = true;
                        int[] array12 = new int[] { 219, 191, 198, 237, 242 };
                        field12.WriteScrambledInts(num12, flag12, array12);
                        return;
                    }
                case GameBuild.Y1S2_9132097:
                    {
                        MemoryHandler field13 = Memory;
                        ulong num13 = 26556388UL;
                        bool flag13 = true;
                        int[] array13 = new int[] { 219, 31, 214, 237, 242 };
                        field13.WriteScrambledInts(num13, flag13, array13);
                        return;
                    }
                case GameBuild.Y1S4_10211195:
                    {
                        MemoryHandler field14 = Memory;
                        ulong num14 = 28313114UL;
                        bool flag14 = true;
                        int[] array14 = new int[] { 219, 68, 40, 237, 242 };
                        field14.WriteScrambledInts(num14, flag14, array14);
                        return;
                    }
                case GameBuild.Y2S1_10751226:
                    {
                        MemoryHandler field15 = Memory;
                        ulong num15 = 30650298UL;
                        bool flag15 = true;
                        int[] array15 = new int[] { 219, 228, 147, 236, 242 };
                        field15.WriteScrambledInts(num15, flag15, array15);
                        return;
                    }
                case GameBuild.Y2S2_11216230:
                    {
                        MemoryHandler field16 = Memory;
                        ulong num16 = 31084090UL;
                        bool flag16 = true;
                        int[] array16 = new int[] { 219, 132, 130, 236, 242 };
                        field16.WriteScrambledInts(num16, flag16, array16);
                        return;
                    }
                case GameBuild.Y2S4_11553121:
                    {
                        MemoryHandler field17 = Memory;
                        ulong num17 = 35071360UL;
                        bool flag17 = true;
                        int[] array17 = new int[] { 219, 49, 185, 21, -13 };
                        field17.WriteScrambledInts(num17, flag17, array17);
                        return;
                    }
                case GameBuild.Y3S2_11965022:
                    {
                        MemoryHandler field18 = Memory;
                        ulong num18 = 43741952UL;
                        bool flag18 = true;
                        int[] array18 = new int[] { 219, 113, 27, 180, 242 };
                        field18.WriteScrambledInts(num18, flag18, array18);
                        return;
                    }
                case GameBuild.Y3S4_12512571:
                    {
                        MemoryHandler field19 = Memory;
                        ulong num19 = 37185940UL;
                        bool flag19 = true;
                        int[] array19 = new int[] { 219, 135, 3, 238, 242 };
                        field19.WriteScrambledInts(num19, flag19, array19);
                        return;
                    }
                case GameBuild.Y2S3_11493221:
                    {
                        MemoryHandler field20 = Memory;
                        ulong num20 = 35215706UL;
                        bool flag20 = true;
                        int[] array20 = new int[] { 219, 36, 223, 238, 242 };
                        field20.WriteScrambledInts(num20, flag20, array20);
                        return;
                    }
                case GameBuild.Y1S3_9654076:
                    {
                        MemoryHandler field21 = Memory;
                        ulong num21 = 27165626UL;
                        bool flag21 = true;
                        int[] array21 = new int[] { 219, 180, 11, 238, 242 };
                        field21.WriteScrambledInts(num21, flag21, array21);
                        return;
                    }
                case GameBuild.Y4S4_13777760:
                    {
                        MemoryHandler field22 = Memory;
                        ulong num22 = 32534362UL;
                        bool flag22 = true;
                        int[] array22 = new int[] { 219, 212, 52, 236, 242 };
                        field22.WriteScrambledInts(num22, flag22, array22);
                        return;
                    }
                case GameBuild.Y2S4_11580709:
                    {
                        MemoryHandler field23 = Memory;
                        ulong num23 = 35066784UL;
                        bool flag23 = true;
                        int[] array23 = new int[] { 219, 65, 186, 21, -13 };
                        field23.WriteScrambledInts(num23, flag23, array23);
                        return;
                    }
                case GameBuild.Y3S2_11938214:
                    {
                        MemoryHandler field24 = Memory;
                        ulong num24 = 43711200UL;
                        bool flag24 = true;
                        int[] array24 = new int[] { 219, 225, 22, 180, 242 };
                        field24.WriteScrambledInts(num24, flag24, array24);
                        return;
                    }
                default:
                    return;
            }
        }
    }

    public void SetEmptySecondary(bool enabled)
    {
        if (enabled)
        {
            GameBuild @enum = _build;
            if (@enum == GameBuild.Y1S0_8194013)
            {
                Memory.WriteNops(29041568UL, 5, true, true);
                return;
            }
            if (@enum == GameBuild.Y1S3_9860556)
            {
                Memory.WriteNops(31894080UL, 5, true, true);
                return;
            }
            switch (@enum)
            {
                case GameBuild.Y1S1_8519860:
                    Memory.WriteNops(29183520UL, 5, true, true);
                    return;
                case GameBuild.Y1S2_9132097:
                    Memory.WriteNops(28852224UL, 5, true, true);
                    return;
                case GameBuild.Y1S4_10211195:
                    Memory.WriteNops(30850624UL, 5, true, true);
                    return;
                case GameBuild.Y2S1_10751226:
                    Memory.WriteNops(33545624UL, 5, true, true);
                    return;
                case GameBuild.Y2S2_11216230:
                    Memory.WriteNops(34000312UL, 5, true, true);
                    return;
                case GameBuild.Y2S4_11553121:
                case GameBuild.Y3S2_11965022:
                case GameBuild.Y3S4_12512571:
                case GameBuild.Y2S3_11493221:
                    break;
                case GameBuild.Y1S3_9654076:
                    Memory.WriteNops(31763936UL, 5, true, true);
                    return;
                default:
                    return;
            }
        }
        else
        {
            GameBuild @enum = _build;
            if (@enum == GameBuild.Y1S0_8194013)
            {
                MemoryHandler field = Memory;
                ulong num = 29041568UL;
                bool flag = true;
                int[] array = new int[] { 219, 17, 183, 239, 242 };
                field.WriteScrambledInts(num, flag, array);
                return;
            }
            if (@enum == GameBuild.Y1S3_9860556)
            {
                MemoryHandler field2 = Memory;
                ulong num2 = 31894080UL;
                bool flag2 = true;
                int[] array2 = new int[] { 219, 129, 195, 239, 242 };
                field2.WriteScrambledInts(num2, flag2, array2);
                return;
            }
            switch (@enum)
            {
                case GameBuild.Y1S1_8519860:
                    {
                        MemoryHandler field3 = Memory;
                        ulong num3 = 29041568UL;
                        bool flag3 = true;
                        int[] array3 = new int[] { 219, 225, 173, 239, 242 };
                        field3.WriteScrambledInts(num3, flag3, array3);
                        return;
                    }
                case GameBuild.Y1S2_9132097:
                    {
                        MemoryHandler field4 = Memory;
                        ulong num4 = 28852224UL;
                        bool flag4 = true;
                        int[] array4 = new int[] { 219, 49, 165, 239, 242 };
                        field4.WriteScrambledInts(num4, flag4, array4);
                        return;
                    }
                case GameBuild.Y1S4_10211195:
                    {
                        MemoryHandler field5 = Memory;
                        ulong num5 = 28852224UL;
                        bool flag5 = true;
                        int[] array5 = new int[] { 219, 17, 116, 239, 242 };
                        field5.WriteScrambledInts(num5, flag5, array5);
                        return;
                    }
                case GameBuild.Y2S1_10751226:
                    {
                        MemoryHandler field6 = Memory;
                        ulong num6 = 33545624UL;
                        bool flag6 = true;
                        int[] array6 = new int[] { 219, 181, 146, 238, 242 };
                        field6.WriteScrambledInts(num6, flag6, array6);
                        return;
                    }
                case GameBuild.Y2S2_11216230:
                    {
                        MemoryHandler field7 = Memory;
                        ulong num7 = 34000312UL;
                        bool flag7 = true;
                        int[] array7 = new int[] { 219, 85, 161, 238, 242 };
                        field7.WriteScrambledInts(num7, flag7, array7);
                        break;
                    }
                case GameBuild.Y2S4_11553121:
                case GameBuild.Y3S2_11965022:
                case GameBuild.Y3S4_12512571:
                case GameBuild.Y2S3_11493221:
                    break;
                case GameBuild.Y1S3_9654076:
                    {
                        MemoryHandler field8 = Memory;
                        ulong num8 = 31763936UL;
                        bool flag8 = true;
                        int[] array8 = new int[] { 219, 113, 207, 239, 242 };
                        field8.WriteScrambledInts(num8, flag8, array8);
                        return;
                    }
                default:
                    return;
            }
        }
    }

    public void SetUnlimitedEquipment(bool enabled)
    {
        if (enabled)
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    {
                        MemoryHandler field = Memory;
                        ulong num = 29011430UL;
                        bool flag = true;
                        int[] array = new int[] { 186, 116, 235, -12, -13, -13, -7, -13, -13, -13, 230, 182, 131, 131, 131 };
                        field.WriteScrambledInts(num, flag, array);
                        return;
                    }
                case GameBuild.Y2S3_11432634:
                    {
                        MemoryHandler field2 = Memory;
                        ulong num2 = 37137696UL;
                        bool flag2 = true;
                        int[] array2 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field2.WriteScrambledInts(num2, flag2, array2);
                        return;
                    }
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field3 = Memory;
                        ulong num3 = 33192576UL;
                        bool flag3 = true;
                        int[] array3 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field3.WriteScrambledInts(num3, flag3, array3);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field4 = Memory;
                        ulong num4 = 37330536UL;
                        bool flag4 = true;
                        int[] array4 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field4.WriteScrambledInts(num4, flag4, array4);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field5 = Memory;
                        ulong num5 = 37206792UL;
                        bool flag5 = true;
                        int[] array5 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field5.WriteScrambledInts(num5, flag5, array5);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                    {
                        MemoryHandler field6 = Memory;
                        ulong num6 = 31865574UL;
                        bool flag6 = true;
                        int[] array6 = new int[] { 186, 116, -5, -11, -13, -13, -7, -13, -13, -13, 230, 182, 131, 131, 131 };
                        field6.WriteScrambledInts(num6, flag6, array6);
                        return;
                    }
                case GameBuild.Y4S2_13147883:
                    {
                        MemoryHandler field7 = Memory;
                        ulong num7 = 40330650UL;
                        bool flag7 = true;
                        int[] array7 = new int[] { 173, -7, -13, -13, -13, 131, 131 };
                        field7.WriteScrambledInts(num7, flag7, array7);
                        return;
                    }
                case GameBuild.Y4S1_12863847:
                    {
                        MemoryHandler field8 = Memory;
                        ulong num8 = 44803386UL;
                        bool flag8 = true;
                        int[] array8 = new int[] { 173, -7, -13, -13, -13, 131, 131 };
                        field8.WriteScrambledInts(num8, flag8, array8);
                        return;
                    }
                case GameBuild.Y3S1_11706399:
                    break;
                case GameBuild.Y4S3_13632147:
                    {
                        MemoryHandler field9 = Memory;
                        ulong num9 = 38585390UL;
                        bool flag9 = true;
                        int[] array9 = new int[] { 176, -7, -13, -13, -13, 131, 131 };
                        field9.WriteScrambledInts(num9, flag9, array9);
                        return;
                    }
                case GameBuild.Y4S1_12815133:
                    {
                        MemoryHandler field10 = Memory;
                        ulong num10 = 44803482UL;
                        bool flag10 = true;
                        int[] array10 = new int[] { 173, -7, -13, -13, -13, 131, 131 };
                        field10.WriteScrambledInts(num10, flag10, array10);
                        return;
                    }
                case GameBuild.Y4S4_13924517:
                    {
                        MemoryHandler field11 = Memory;
                        ulong num11 = 42170908UL;
                        bool flag11 = true;
                        int[] array11 = new int[] { 186, 52, 47, -7, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 222 };
                        field11.WriteScrambledInts(num11, flag11, array11);
                        MemoryHandler field12 = Memory;
                        ulong num12 = 44620870UL;
                        bool flag12 = true;
                        int[] array12 = new int[] { 186, -9, 35, -7, -13, -13, -13, 131, 131, 131, 131, 131, 131, 220, 230, -12, -13, -13 };
                        field12.WriteScrambledInts(num12, flag12, array12);
                        return;
                    }
                case GameBuild.Y5S1_14303219:
                    {
                        MemoryHandler field13 = Memory;
                        ulong num13 = 39309116UL;
                        bool flag13 = true;
                        int[] array13 = new int[] { 186, 52, 71, -7, -13, -13, -13, 59, 126, 60, 11, 131, 131, 131, 131, 222 };
                        field13.WriteScrambledInts(num13, flag13, array13);
                        return;
                    }
                case GameBuild.Y1S1_8519860:
                    {
                        MemoryHandler field14 = Memory;
                        ulong num14 = 29152646UL;
                        bool flag14 = true;
                        int[] array14 = new int[] { 186, 116, 235, -12, -13, -13, -7, -13, -13, -13, 230, 182, 131, 131, 131 };
                        field14.WriteScrambledInts(num14, flag14, array14);
                        return;
                    }
                case GameBuild.Y1S2_9132097:
                    {
                        MemoryHandler field15 = Memory;
                        ulong num15 = 28822534UL;
                        bool flag15 = true;
                        int[] array15 = new int[] { 186, 116, 235, -12, -13, -13, -7, -13, -13, -13, 230, 182, 131, 131, 131 };
                        field15.WriteScrambledInts(num15, flag15, array15);
                        return;
                    }
                case GameBuild.Y1S4_10211195:
                    {
                        MemoryHandler field16 = Memory;
                        ulong num16 = 30823622UL;
                        bool flag16 = true;
                        int[] array16 = new int[] { 186, 116, -5, -11, -13, -13, -7, -13, -13, -13, 230, 182, 131, 131, 131 };
                        field16.WriteScrambledInts(num16, flag16, array16);
                        return;
                    }
                case GameBuild.Y2S1_10751226:
                    {
                        MemoryHandler field17 = Memory;
                        ulong num17 = 33508006UL;
                        bool flag17 = true;
                        int[] array17 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field17.WriteScrambledInts(num17, flag17, array17);
                        return;
                    }
                case GameBuild.Y2S2_11216230:
                    {
                        MemoryHandler field18 = Memory;
                        ulong num18 = 33964038UL;
                        bool flag18 = true;
                        int[] array18 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field18.WriteScrambledInts(num18, flag18, array18);
                        return;
                    }
                case GameBuild.Y2S4_11553121:
                    {
                        MemoryHandler field19 = Memory;
                        ulong num19 = 37283552UL;
                        bool flag19 = true;
                        int[] array19 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field19.WriteScrambledInts(num19, flag19, array19);
                        return;
                    }
                case GameBuild.Y3S2_11965022:
                    {
                        MemoryHandler field20 = Memory;
                        ulong num20 = 35096616UL;
                        bool flag20 = true;
                        int[] array20 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field20.WriteScrambledInts(num20, flag20, array20);
                        return;
                    }
                case GameBuild.Y3S4_12512571:
                    {
                        MemoryHandler field21 = Memory;
                        ulong num21 = 49100544UL;
                        bool flag21 = true;
                        int[] array21 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field21.WriteScrambledInts(num21, flag21, array21);
                        return;
                    }
                case GameBuild.Y2S3_11493221:
                    {
                        MemoryHandler field22 = Memory;
                        ulong num22 = 37384608UL;
                        bool flag22 = true;
                        int[] array22 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field22.WriteScrambledInts(num22, flag22, array22);
                        return;
                    }
                case GameBuild.Y1S3_9654076:
                    {
                        MemoryHandler field23 = Memory;
                        ulong num23 = 31736454UL;
                        bool flag23 = true;
                        int[] array23 = new int[] { 186, 116, -5, -11, -13, -13, -7, -13, -13, -13, 230, 182, 131, 131, 131 };
                        field23.WriteScrambledInts(num23, flag23, array23);
                        return;
                    }
                case GameBuild.Y4S4_13777760:
                    {
                        MemoryHandler field24 = Memory;
                        ulong num24 = 30521084UL;
                        bool flag24 = true;
                        int[] array24 = new int[] { 186, 52, 47, -7, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 222 };
                        field24.WriteScrambledInts(num24, flag24, array24);
                        MemoryHandler field25 = Memory;
                        ulong num25 = 42036582UL;
                        bool flag25 = true;
                        int[] array25 = new int[] { 186, -9, 35, -7, -13, -13, -13, 131, 131, 131, 220, 233, -12, -13, -13 };
                        field25.WriteScrambledInts(num25, flag25, array25);
                        return;
                    }
                case GameBuild.Y2S4_11580709:
                    {
                        MemoryHandler field26 = Memory;
                        ulong num26 = 37273792UL;
                        bool flag26 = true;
                        int[] array26 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field26.WriteScrambledInts(num26, flag26, array26);
                        return;
                    }
                case GameBuild.Y3S2_11938214:
                    {
                        MemoryHandler field27 = Memory;
                        ulong num27 = 35062184UL;
                        bool flag27 = true;
                        int[] array27 = new int[] { 173, -7, -13, -13, -13, 131, 131, 131, 131 };
                        field27.WriteScrambledInts(num27, flag27, array27);
                        return;
                    }
                default:
                    return;
            }
        }
        else
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    {
                        MemoryHandler field28 = Memory;
                        ulong num28 = 29011430UL;
                        bool flag28 = true;
                        int[] array28 = new int[] { 115, 172, -11, -11, -13, -13, -13, 103, -5, 46, 132, 235, -12, -13, -13 };
                        field28.WriteScrambledInts(num28, flag28, array28);
                        return;
                    }
                case GameBuild.Y2S3_11432634:
                    {
                        MemoryHandler field29 = Memory;
                        ulong num29 = 37137696UL;
                        bool flag29 = true;
                        int[] array29 = new int[] { 115, 172, 68, -10, -13, -13, -13, 103, 12 };
                        field29.WriteScrambledInts(num29, flag29, array29);
                        return;
                    }
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field30 = Memory;
                        ulong num30 = 33192576UL;
                        bool flag30 = true;
                        int[] array30 = new int[] { 115, 172, 85, -10, -13, -13, -13, 103, 12 };
                        field30.WriteScrambledInts(num30, flag30, array30);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field31 = Memory;
                        ulong num31 = 37330536UL;
                        bool flag31 = true;
                        int[] array31 = new int[] { 115, 172, 85, -10, -13, -13, -13, 103, 12 };
                        field31.WriteScrambledInts(num31, flag31, array31);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field32 = Memory;
                        ulong num32 = 37206792UL;
                        bool flag32 = true;
                        int[] array32 = new int[] { 115, 172, 85, -10, -13, -13, -13, 103, 12 };
                        field32.WriteScrambledInts(num32, flag32, array32);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                    {
                        MemoryHandler field33 = Memory;
                        ulong num33 = 31865574UL;
                        bool flag33 = true;
                        int[] array33 = new int[] { 115, 172, 5, -11, -13, -13, -13, 103, -5, 46, 132, -5, -11, -13, -13 };
                        field33.WriteScrambledInts(num33, flag33, array33);
                        return;
                    }
                case GameBuild.Y4S2_13147883:
                    {
                        MemoryHandler field34 = Memory;
                        ulong num34 = 40330650UL;
                        bool flag34 = true;
                        int[] array34 = new int[] { 115, 172, 85, -10, -13, -13, -13 };
                        field34.WriteScrambledInts(num34, flag34, array34);
                        return;
                    }
                case GameBuild.Y4S1_12863847:
                    {
                        MemoryHandler field35 = Memory;
                        ulong num35 = 44803386UL;
                        bool flag35 = true;
                        int[] array35 = new int[] { 115, 172, 69, -10, -13, -13, -13 };
                        field35.WriteScrambledInts(num35, flag35, array35);
                        return;
                    }
                case GameBuild.Y3S1_11706399:
                    break;
                case GameBuild.Y4S3_13632147:
                    {
                        MemoryHandler field36 = Memory;
                        ulong num36 = 38585390UL;
                        bool flag36 = true;
                        int[] array36 = new int[] { 115, 172, 165, -10, -13, -13, -13 };
                        field36.WriteScrambledInts(num36, flag36, array36);
                        return;
                    }
                case GameBuild.Y4S1_12815133:
                    {
                        MemoryHandler field37 = Memory;
                        ulong num37 = 44803482UL;
                        bool flag37 = true;
                        int[] array37 = new int[] { 115, 172, 69, -10, -13, -13, -13 };
                        field37.WriteScrambledInts(num37, flag37, array37);
                        return;
                    }
                case GameBuild.Y4S4_13924517:
                    {
                        MemoryHandler field38 = Memory;
                        ulong num38 = 42170908UL;
                        bool flag38 = true;
                        int[] array38 = new int[] { 124, 68, 47, 59, 126, 60, 11, 219, 28, 162, 5, -13, 59, 120, 242, 103 };
                        field38.WriteScrambledInts(num38, flag38, array38);
                        MemoryHandler field39 = Memory;
                        ulong num39 = 44620870UL;
                        bool flag39 = true;
                        int[] array39 = new int[] { 124, 31, -7, 59, 126, 132, 155, -10, -13, -13, 59, 120, 197, 103, 33, 88, 59, 126 };
                        field39.WriteScrambledInts(num39, flag39, array39);
                        return;
                    }
                case GameBuild.Y5S1_14303219:
                    {
                        MemoryHandler field40 = Memory;
                        ulong num40 = 39309116UL;
                        bool flag40 = true;
                        int[] array40 = new int[] { 124, 68, 71, 59, 126, 60, 11, 219, 236, 143, 57, -13, 59, 120, 242, 103 };
                        field40.WriteScrambledInts(num40, flag40, array40);
                        break;
                    }
                case GameBuild.Y1S1_8519860:
                    {
                        MemoryHandler field41 = Memory;
                        ulong num41 = 29152646UL;
                        bool flag41 = true;
                        int[] array41 = new int[] { 115, 172, -11, -11, -13, -13, -13, 103, -5, 46, 132, 235, -12, -13, -13 };
                        field41.WriteScrambledInts(num41, flag41, array41);
                        return;
                    }
                case GameBuild.Y1S2_9132097:
                    {
                        MemoryHandler field42 = Memory;
                        ulong num42 = 28822534UL;
                        bool flag42 = true;
                        int[] array42 = new int[] { 115, 172, -11, -11, -13, -13, -13, 103, -5, 46, 132, 235, -12, -13, -13 };
                        field42.WriteScrambledInts(num42, flag42, array42);
                        return;
                    }
                case GameBuild.Y1S4_10211195:
                    {
                        MemoryHandler field43 = Memory;
                        ulong num43 = 30823622UL;
                        bool flag43 = true;
                        int[] array43 = new int[] { 115, 172, 6, -11, -13, -13, -13, 103, -5, 46, 132, -5, -11, -13, -13 };
                        field43.WriteScrambledInts(num43, flag43, array43);
                        return;
                    }
                case GameBuild.Y2S1_10751226:
                    {
                        MemoryHandler field44 = Memory;
                        ulong num44 = 33508006UL;
                        bool flag44 = true;
                        int[] array44 = new int[] { 115, 172, 22, -11, -13, -13, -13, 103, -5 };
                        field44.WriteScrambledInts(num44, flag44, array44);
                        return;
                    }
                case GameBuild.Y2S2_11216230:
                    {
                        MemoryHandler field45 = Memory;
                        ulong num45 = 33964038UL;
                        bool flag45 = true;
                        int[] array45 = new int[] { 115, 172, 38, -11, -13, -13, -13, 103, -5 };
                        field45.WriteScrambledInts(num45, flag45, array45);
                        return;
                    }
                case GameBuild.Y2S4_11553121:
                    {
                        MemoryHandler field46 = Memory;
                        ulong num46 = 37283552UL;
                        bool flag46 = true;
                        int[] array46 = new int[] { 115, 172, 84, -10, -13, -13, -13, 103, 12 };
                        field46.WriteScrambledInts(num46, flag46, array46);
                        return;
                    }
                case GameBuild.Y3S2_11965022:
                    {
                        MemoryHandler field47 = Memory;
                        ulong num47 = 35096616UL;
                        bool flag47 = true;
                        int[] array47 = new int[] { 115, 172, 81, -10, -13, -13, -13, 103, 12 };
                        field47.WriteScrambledInts(num47, flag47, array47);
                        return;
                    }
                case GameBuild.Y3S4_12512571:
                    {
                        MemoryHandler field48 = Memory;
                        ulong num48 = 49100544UL;
                        bool flag48 = true;
                        int[] array48 = new int[] { 115, 172, 69, -10, -13, -13, -13, 103, 12 };
                        field48.WriteScrambledInts(num48, flag48, array48);
                        return;
                    }
                case GameBuild.Y2S3_11493221:
                    {
                        MemoryHandler field49 = Memory;
                        ulong num49 = 37384608UL;
                        bool flag49 = true;
                        int[] array49 = new int[] { 115, 172, 68, -10, -13, -13, -13, 103, 12 };
                        field49.WriteScrambledInts(num49, flag49, array49);
                        return;
                    }
                case GameBuild.Y1S3_9654076:
                    {
                        MemoryHandler field50 = Memory;
                        ulong num50 = 31736454UL;
                        bool flag50 = true;
                        int[] array50 = new int[] { 115, 172, 5, -11, -13, -13, -13, 103, -5, 46, 132, -5, -11, -13, -13 };
                        field50.WriteScrambledInts(num50, flag50, array50);
                        return;
                    }
                case GameBuild.Y4S4_13777760:
                    {
                        MemoryHandler field51 = Memory;
                        ulong num51 = 30521084UL;
                        bool flag51 = true;
                        int[] array51 = new int[] { 124, 68, 47, 59, 126, 60, 11, 219, 60, 204, 74, -13, 59, 120, 242, 103 };
                        field51.WriteScrambledInts(num51, flag51, array51);
                        MemoryHandler field52 = Memory;
                        ulong num52 = 42036582UL;
                        bool flag52 = true;
                        int[] array52 = new int[] { 124, 31, -7, 59, 126, 132, 155, -10, -13, -13, 59, 120, 197, 103, 33 };
                        field52.WriteScrambledInts(num52, flag52, array52);
                        return;
                    }
                case GameBuild.Y2S4_11580709:
                    {
                        MemoryHandler field53 = Memory;
                        ulong num53 = 37273792UL;
                        bool flag53 = true;
                        int[] array53 = new int[] { 115, 172, 84, -10, -13, -13, -13, 103, 12 };
                        field53.WriteScrambledInts(num53, flag53, array53);
                        return;
                    }
                case GameBuild.Y3S2_11938214:
                    {
                        MemoryHandler field54 = Memory;
                        ulong num54 = 35062184UL;
                        bool flag54 = true;
                        int[] array54 = new int[] { 115, 172, 81, -10, -13, -13, -13, 103, 12 };
                        field54.WriteScrambledInts(num54, flag54, array54);
                        return;
                    }
                default:
                    return;
            }
        }
    }

    public void SetDisplayVersion(bool enabled)
    {
        if (enabled)
        {
            switch (_build)
            {
                case GameBuild.Y3S1_11726982:
                    {
                        Memory.WriteNops(6971910UL, 6, true, true);
                        MemoryHandler field = Memory;
                        ulong num = 3838240UL;
                        bool flag = true;
                        int[] array = new int[] { 220, 210, -13, -13, -13, 131 };
                        field.WriteScrambledInts(num, flag, array);
                        MemoryHandler field2 = Memory;
                        ulong num2 = 3838702UL;
                        bool flag2 = true;
                        int[] array2 = new int[] { 67, 4, 163, -11 };
                        field2.WriteScrambledInts(num2, flag2, array2);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        Memory.WriteNops(10509750UL, 2, true, true);
                        MemoryHandler field3 = Memory;
                        ulong num3 = 8279744UL;
                        bool flag3 = true;
                        int[] array3 = new int[] { 220, 180, -13, -13, -13, 131 };
                        field3.WriteScrambledInts(num3, flag3, array3);
                        MemoryHandler field4 = Memory;
                        ulong num4 = 8280146UL;
                        bool flag4 = true;
                        int[] array4 = new int[] { 121, 86, 164, -11 };
                        field4.WriteScrambledInts(num4, flag4, array4);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        Memory.WriteNops(10363030UL, 2, true, true);
                        MemoryHandler field5 = Memory;
                        ulong num5 = 8065120UL;
                        bool flag5 = true;
                        int[] array5 = new int[] { 220, 180, -13, -13, -13, 131 };
                        field5.WriteScrambledInts(num5, flag5, array5);
                        MemoryHandler field6 = Memory;
                        ulong num6 = 8065522UL;
                        bool flag6 = true;
                        int[] array6 = new int[] { 81, 189, 164, -11 };
                        field6.WriteScrambledInts(num6, flag6, array6);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                case GameBuild.Y3S1_11706399:
                case GameBuild.Y1S1_8519860:
                case GameBuild.Y1S2_9132097:
                case GameBuild.Y1S4_10211195:
                case GameBuild.Y2S1_10751226:
                case GameBuild.Y2S2_11216230:
                case GameBuild.Y2S3_11493221:
                case GameBuild.Y1S3_9654076:
                    break;
                case GameBuild.Y4S2_13147883:
                    {
                        Memory.WriteNops(15098422UL, 2, true, true);
                        MemoryHandler field7 = Memory;
                        ulong num7 = 3475062UL;
                        bool flag7 = true;
                        int[] array7 = new int[] { 7, 35, 59, -10 };
                        field7.WriteScrambledInts(num7, flag7, array7);
                        return;
                    }
                case GameBuild.Y4S1_12863847:
                    {
                        Memory.WriteNops(15052694UL, 2, true, true);
                        MemoryHandler field8 = Memory;
                        ulong num8 = 3419624UL;
                        bool flag8 = true;
                        int[] array8 = new int[] { -13, -13, -13, -13 };
                        field8.WriteScrambledInts(num8, flag8, array8);
                        return;
                    }
                case GameBuild.Y4S3_13632147:
                    Memory.WriteNops(16066994UL, 2, true, true);
                    Memory.WriteScrambledInts(2529022UL, true, new int[] { -13 });
                    return;
                case GameBuild.Y4S1_12815133:
                    {
                        Memory.WriteNops(15052790UL, 2, true, true);
                        MemoryHandler field9 = Memory;
                        ulong num9 = 3419624UL;
                        bool flag9 = true;
                        int[] array9 = new int[] { -13, -13, -13, -13 };
                        field9.WriteScrambledInts(num9, flag9, array9);
                        return;
                    }
                case GameBuild.Y4S4_13924517:
                    Memory.WriteNops(15062582UL, 2, true, true);
                    Memory.WriteScrambledInts(2659454UL, true, new int[] { -13 });
                    return;
                case GameBuild.Y5S1_14303219:
                    Memory.WriteNops(15455190UL, 2, true, true);
                    Memory.WriteScrambledInts(2450110UL, true, new int[] { -13 });
                    return;
                case GameBuild.Y2S4_11553121:
                    {
                        Memory.WriteNops(11681030UL, 6, true, true);
                        MemoryHandler field10 = Memory;
                        ulong num10 = 3838656UL;
                        bool flag10 = true;
                        int[] array10 = new int[] { 220, 210, -13, -13, -13, 131 };
                        field10.WriteScrambledInts(num10, flag10, array10);
                        Memory.WriteScrambledInts(3839118UL, true, new int[] { 179 });
                        return;
                    }
                case GameBuild.Y3S2_11965022:
                    {
                        Memory.WriteNops(7052518UL, 6, true, true);
                        MemoryHandler field11 = Memory;
                        ulong num11 = 3945888UL;
                        bool flag11 = true;
                        int[] array11 = new int[] { 220, 180, -13, -13, -13, 131 };
                        field11.WriteScrambledInts(num11, flag11, array11);
                        Memory.WriteScrambledInts(3946290UL, true, new int[] { 161 });
                        return;
                    }
                case GameBuild.Y3S4_12512571:
                    {
                        Memory.WriteNops(16009138UL, 2, true, true);
                        MemoryHandler field12 = Memory;
                        ulong num12 = 3681800UL;
                        bool flag12 = true;
                        int[] array12 = new int[] { -13, -13, -13, -13 };
                        field12.WriteScrambledInts(num12, flag12, array12);
                        return;
                    }
                case GameBuild.Y4S4_13777760:
                    Memory.WriteNops(15042358UL, 2, true, true);
                    Memory.WriteScrambledInts(2668894UL, true, new int[] { -13 });
                    return;
                case GameBuild.Y2S4_11580709:
                    {
                        Memory.WriteNops(11672934UL, 6, true, true);
                        MemoryHandler field13 = Memory;
                        ulong num13 = 3839200UL;
                        bool flag13 = true;
                        int[] array13 = new int[] { 220, 210, -13, -13, -13, 131 };
                        field13.WriteScrambledInts(num13, flag13, array13);
                        Memory.WriteScrambledInts(3839662UL, true, new int[] { 147 });
                        return;
                    }
                case GameBuild.Y3S2_11938214:
                    {
                        Memory.WriteNops(7024614UL, 6, true, true);
                        MemoryHandler field14 = Memory;
                        ulong num14 = 3941504UL;
                        bool flag14 = true;
                        int[] array14 = new int[] { 220, 180, -13, -13, -13, 131 };
                        field14.WriteScrambledInts(num14, flag14, array14);
                        Memory.WriteScrambledInts(3941906UL, true, new int[] { 49 });
                        return;
                    }
                default:
                    return;
            }
        }
        else
        {
            switch (_build)
            {
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field15 = Memory;
                        ulong num15 = 6971910UL;
                        bool flag15 = true;
                        int[] array15 = new int[] { 2, 119, 14, -12, -13, -13 };
                        field15.WriteScrambledInts(num15, flag15, array15);
                        MemoryHandler field16 = Memory;
                        ulong num16 = 3838240UL;
                        bool flag16 = true;
                        int[] array16 = new int[] { 2, 120, 209, -13, -13, -13 };
                        field16.WriteScrambledInts(num16, flag16, array16);
                        MemoryHandler field17 = Memory;
                        ulong num17 = 3838702UL;
                        bool flag17 = true;
                        int[] array17 = new int[] { 87, 4, 163, -11 };
                        field17.WriteScrambledInts(num17, flag17, array17);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field18 = Memory;
                        ulong num18 = 10509750UL;
                        bool flag18 = true;
                        int[] array18 = new int[2];
                        array18[0] = 103;
                        field18.WriteScrambledInts(num18, flag18, array18);
                        MemoryHandler field19 = Memory;
                        ulong num19 = 8279744UL;
                        bool flag19 = true;
                        int[] array19 = new int[] { 2, 120, 179, -13, -13, -13 };
                        field19.WriteScrambledInts(num19, flag19, array19);
                        MemoryHandler field20 = Memory;
                        ulong num20 = 8280146UL;
                        bool flag20 = true;
                        int[] array20 = new int[] { 141, 86, 164, -11 };
                        field20.WriteScrambledInts(num20, flag20, array20);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field21 = Memory;
                        ulong num21 = 10363030UL;
                        bool flag21 = true;
                        int[] array21 = new int[2];
                        array21[0] = 103;
                        field21.WriteScrambledInts(num21, flag21, array21);
                        MemoryHandler field22 = Memory;
                        ulong num22 = 8065120UL;
                        bool flag22 = true;
                        int[] array22 = new int[] { 2, 120, 179, -13, -13, -13 };
                        field22.WriteScrambledInts(num22, flag22, array22);
                        MemoryHandler field23 = Memory;
                        ulong num23 = 8065522UL;
                        bool flag23 = true;
                        int[] array23 = new int[] { 101, 189, 164, -11 };
                        field23.WriteScrambledInts(num23, flag23, array23);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                case GameBuild.Y3S1_11706399:
                case GameBuild.Y1S1_8519860:
                case GameBuild.Y1S2_9132097:
                case GameBuild.Y1S4_10211195:
                case GameBuild.Y2S1_10751226:
                case GameBuild.Y2S2_11216230:
                case GameBuild.Y2S3_11493221:
                case GameBuild.Y1S3_9654076:
                    break;
                case GameBuild.Y4S2_13147883:
                    {
                        Memory.WriteScrambledInts(15098422UL, true, new int[] { 103, 20 });
                        MemoryHandler field24 = Memory;
                        ulong num24 = 3475062UL;
                        bool flag24 = true;
                        int[] array24 = new int[] { 99, 113, 51, -10 };
                        field24.WriteScrambledInts(num24, flag24, array24);
                        return;
                    }
                case GameBuild.Y4S1_12863847:
                    {
                        Memory.WriteScrambledInts(15052694UL, true, new int[] { 103, 20 });
                        MemoryHandler field25 = Memory;
                        ulong num25 = 3419624UL;
                        bool flag25 = true;
                        int[] array25 = new int[] { 242, 242, 242, 242 };
                        field25.WriteScrambledInts(num25, flag25, array25);
                        return;
                    }
                case GameBuild.Y4S3_13632147:
                    Memory.WriteScrambledInts(16066994UL, true, new int[] { 103, 12 });
                    Memory.WriteScrambledInts(2529022UL, true, new int[] { -9 });
                    return;
                case GameBuild.Y4S1_12815133:
                    {
                        Memory.WriteScrambledInts(15052790UL, true, new int[] { 103, 20 });
                        MemoryHandler field26 = Memory;
                        ulong num26 = 3419624UL;
                        bool flag26 = true;
                        int[] array26 = new int[] { 242, 242, 242, 242 };
                        field26.WriteScrambledInts(num26, flag26, array26);
                        return;
                    }
                case GameBuild.Y4S4_13924517:
                    Memory.WriteScrambledInts(15062582UL, true, new int[] { 103, 20 });
                    Memory.WriteScrambledInts(2659454UL, true, new int[] { -9 });
                    return;
                case GameBuild.Y5S1_14303219:
                    Memory.WriteScrambledInts(15455190UL, true, new int[] { 103, 20 });
                    Memory.WriteScrambledInts(2450110UL, true, new int[] { -9 });
                    break;
                case GameBuild.Y2S4_11553121:
                    {
                        MemoryHandler field27 = Memory;
                        ulong num27 = 11681030UL;
                        bool flag27 = true;
                        int[] array27 = new int[] { 2, 119, 9, -12, -13, -13 };
                        field27.WriteScrambledInts(num27, flag27, array27);
                        MemoryHandler field28 = Memory;
                        ulong num28 = 3838656UL;
                        bool flag28 = true;
                        int[] array28 = new int[] { 2, 120, 209, -13, -13, -13 };
                        field28.WriteScrambledInts(num28, flag28, array28);
                        Memory.WriteScrambledInts(3839118UL, true, new int[] { 199 });
                        return;
                    }
                case GameBuild.Y3S2_11965022:
                    {
                        MemoryHandler field29 = Memory;
                        ulong num29 = 7052518UL;
                        bool flag29 = true;
                        int[] array29 = new int[] { 2, 119, 14, -12, -13, -13 };
                        field29.WriteScrambledInts(num29, flag29, array29);
                        MemoryHandler field30 = Memory;
                        ulong num30 = 3945888UL;
                        bool flag30 = true;
                        int[] array30 = new int[] { 2, 120, 179, -13, -13, -13 };
                        field30.WriteScrambledInts(num30, flag30, array30);
                        Memory.WriteScrambledInts(3946290UL, true, new int[] { 181 });
                        return;
                    }
                case GameBuild.Y3S4_12512571:
                    {
                        Memory.WriteScrambledInts(16009138UL, true, new int[] { 103, 12 });
                        MemoryHandler field31 = Memory;
                        ulong num31 = 3681800UL;
                        bool flag31 = true;
                        int[] array31 = new int[] { 242, 242, 242, 242 };
                        field31.WriteScrambledInts(num31, flag31, array31);
                        return;
                    }
                case GameBuild.Y4S4_13777760:
                    Memory.WriteScrambledInts(15042358UL, true, new int[] { 103, 20 });
                    Memory.WriteScrambledInts(2668894UL, true, new int[] { -9 });
                    return;
                case GameBuild.Y2S4_11580709:
                    {
                        MemoryHandler field32 = Memory;
                        ulong num32 = 11672934UL;
                        bool flag32 = true;
                        int[] array32 = new int[] { 2, 119, 9, -12, -13, -13 };
                        field32.WriteScrambledInts(num32, flag32, array32);
                        MemoryHandler field33 = Memory;
                        ulong num33 = 3839200UL;
                        bool flag33 = true;
                        int[] array33 = new int[] { 2, 120, 209, -13, -13, -13 };
                        field33.WriteScrambledInts(num33, flag33, array33);
                        Memory.WriteScrambledInts(3839662UL, true, new int[] { 167 });
                        return;
                    }
                case GameBuild.Y3S2_11938214:
                    {
                        MemoryHandler field34 = Memory;
                        ulong num34 = 7024614UL;
                        bool flag34 = true;
                        int[] array34 = new int[] { 2, 119, 14, -12, -13, -13 };
                        field34.WriteScrambledInts(num34, flag34, array34);
                        MemoryHandler field35 = Memory;
                        ulong num35 = 3941504UL;
                        bool flag35 = true;
                        int[] array35 = new int[] { 2, 120, 179, -13, -13, -13 };
                        field35.WriteScrambledInts(num35, flag35, array35);
                        Memory.WriteScrambledInts(3941906UL, true, new int[] { 69 });
                        return;
                    }
                default:
                    return;
            }
        }
    }

    public void SetGodMode(bool enabled)
    {
        if (enabled)
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    {
                        MemoryHandler field = Memory;
                        ulong num = 29891808UL;
                        bool flag = true;
                        int[] array = new int[] { 186, 122, 239, -13, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field.WriteScrambledInts(num, flag, array);
                        return;
                    }
                case GameBuild.Y2S3_11432634:
                    {
                        MemoryHandler field2 = Memory;
                        ulong num2 = 38064060UL;
                        bool flag2 = true;
                        int[] array2 = new int[] { 186, 122, -5, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field2.WriteScrambledInts(num2, flag2, array2);
                        return;
                    }
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field3 = Memory;
                        ulong num3 = 34412342UL;
                        bool flag3 = true;
                        int[] array3 = new int[] { 186, 122, 11, -12, -13, -13, 181, -12, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field3.WriteScrambledInts(num3, flag3, array3);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field4 = Memory;
                        ulong num4 = 45623702UL;
                        bool flag4 = true;
                        int[] array4 = new int[] { 186, 122, 63, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field4.WriteScrambledInts(num4, flag4, array4);
                        MemoryHandler field5 = Memory;
                        ulong num5 = 47859628UL;
                        bool flag5 = true;
                        int[] array5 = new int[] { 220, 139, -12, -13, -13, 131 };
                        field5.WriteScrambledInts(num5, flag5, array5);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field6 = Memory;
                        ulong num6 = 46062582UL;
                        bool flag6 = true;
                        int[] array6 = new int[] { 186, 122, 63, -12, -13, -13, 112, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field6.WriteScrambledInts(num6, flag6, array6);
                        MemoryHandler field7 = Memory;
                        ulong num7 = 48281228UL;
                        bool flag7 = true;
                        int[] array7 = new int[] { 220, 139, -12, -13, -13, 131 };
                        field7.WriteScrambledInts(num7, flag7, array7);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                    {
                        MemoryHandler field8 = Memory;
                        ulong num8 = 30525878UL;
                        bool flag8 = true;
                        int[] array8 = new int[] { 186, 122, -5, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field8.WriteScrambledInts(num8, flag8, array8);
                        return;
                    }
                case GameBuild.Y4S2_13147883:
                    {
                        MemoryHandler field9 = Memory;
                        ulong num9 = 34626014UL;
                        bool flag9 = true;
                        int[] array9 = new int[] { 186, 122, 51, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field9.WriteScrambledInts(num9, flag9, array9);
                        return;
                    }
                case GameBuild.Y4S1_12863847:
                    {
                        MemoryHandler field10 = Memory;
                        ulong num10 = 40447990UL;
                        bool flag10 = true;
                        int[] array10 = new int[] { 186, 122, 47, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field10.WriteScrambledInts(num10, flag10, array10);
                        return;
                    }
                case GameBuild.Y3S1_11706399:
                    {
                        MemoryHandler field11 = Memory;
                        ulong num11 = 34396470UL;
                        bool flag11 = true;
                        int[] array11 = new int[] { 186, 122, 11, -12, -13, -13, 181, -12, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field11.WriteScrambledInts(num11, flag11, array11);
                        return;
                    }
                case GameBuild.Y4S3_13632147:
                    {
                        MemoryHandler field12 = Memory;
                        ulong num12 = 48538302UL;
                        bool flag12 = true;
                        int[] array12 = new int[] { 186, 122, 59, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field12.WriteScrambledInts(num12, flag12, array12);
                        return;
                    }
                case GameBuild.Y4S1_12815133:
                    {
                        MemoryHandler field13 = Memory;
                        ulong num13 = 40448086UL;
                        bool flag13 = true;
                        int[] array13 = new int[] { 186, 122, 47, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field13.WriteScrambledInts(num13, flag13, array13);
                        return;
                    }
                case GameBuild.Y4S4_13924517:
                    {
                        MemoryHandler field14 = Memory;
                        ulong num14 = 39961188UL;
                        bool flag14 = true;
                        int[] array14 = new int[] { 186, 122, 59, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field14.WriteScrambledInts(num14, flag14, array14);
                        return;
                    }
                case GameBuild.Y5S1_14303219:
                    {
                        MemoryHandler field15 = Memory;
                        ulong num15 = 51188428UL;
                        bool flag15 = true;
                        int[] array15 = new int[] { 186, 122, 91, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131 };
                        field15.WriteScrambledInts(num15, flag15, array15);
                        return;
                    }
                case GameBuild.Y1S1_8519860:
                    {
                        MemoryHandler field16 = Memory;
                        ulong num16 = 30049856UL;
                        bool flag16 = true;
                        int[] array16 = new int[] { 186, 122, 239, -13, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field16.WriteScrambledInts(num16, flag16, array16);
                        return;
                    }
                case GameBuild.Y1S2_9132097:
                    {
                        MemoryHandler field17 = Memory;
                        ulong num17 = 29762144UL;
                        bool flag17 = true;
                        int[] array17 = new int[] { 186, 122, 239, -13, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field17.WriteScrambledInts(num17, flag17, array17);
                        return;
                    }
                case GameBuild.Y1S4_10211195:
                    {
                        MemoryHandler field18 = Memory;
                        ulong num18 = 32098678UL;
                        bool flag18 = true;
                        int[] array18 = new int[] { 186, 122, -5, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field18.WriteScrambledInts(num18, flag18, array18);
                        return;
                    }
                case GameBuild.Y2S1_10751226:
                    {
                        MemoryHandler field19 = Memory;
                        ulong num19 = 34903452UL;
                        bool flag19 = true;
                        int[] array19 = new int[] { 186, 122, -5, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field19.WriteScrambledInts(num19, flag19, array19);
                        return;
                    }
                case GameBuild.Y2S2_11216230:
                    {
                        MemoryHandler field20 = Memory;
                        ulong num20 = 35349436UL;
                        bool flag20 = true;
                        int[] array20 = new int[] { 186, 122, -5, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field20.WriteScrambledInts(num20, flag20, array20);
                        return;
                    }
                case GameBuild.Y2S4_11553121:
                    {
                        MemoryHandler field21 = Memory;
                        ulong num21 = 38443708UL;
                        bool flag21 = true;
                        int[] array21 = new int[] { 186, 122, -5, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field21.WriteScrambledInts(num21, flag21, array21);
                        return;
                    }
                case GameBuild.Y3S2_11965022:
                    {
                        MemoryHandler field22 = Memory;
                        ulong num22 = 32585590UL;
                        bool flag22 = true;
                        int[] array22 = new int[] { 186, 122, 59, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field22.WriteScrambledInts(num22, flag22, array22);
                        return;
                    }
                case GameBuild.Y3S4_12512571:
                    {
                        MemoryHandler field23 = Memory;
                        ulong num23 = 43817578UL;
                        bool flag23 = true;
                        int[] array23 = new int[] { 186, 122, 47, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field23.WriteScrambledInts(num23, flag23, array23);
                        return;
                    }
                case GameBuild.Y2S3_11493221:
                    {
                        MemoryHandler field24 = Memory;
                        ulong num24 = 38314972UL;
                        bool flag24 = true;
                        int[] array24 = new int[] { 186, 122, -5, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field24.WriteScrambledInts(num24, flag24, array24);
                        return;
                    }
                case GameBuild.Y1S3_9654076:
                    {
                        MemoryHandler field25 = Memory;
                        ulong num25 = 30407040UL;
                        bool flag25 = true;
                        int[] array25 = new int[] { 186, 122, -5, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field25.WriteScrambledInts(num25, flag25, array25);
                        return;
                    }
                case GameBuild.Y4S4_13777760:
                    {
                        MemoryHandler field26 = Memory;
                        ulong num26 = 46134212UL;
                        bool flag26 = true;
                        int[] array26 = new int[] { 186, 122, 59, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field26.WriteScrambledInts(num26, flag26, array26);
                        return;
                    }
                case GameBuild.Y2S4_11580709:
                    {
                        MemoryHandler field27 = Memory;
                        ulong num27 = 38436188UL;
                        bool flag27 = true;
                        int[] array27 = new int[] { 186, 122, -5, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field27.WriteScrambledInts(num27, flag27, array27);
                        return;
                    }
                case GameBuild.Y3S2_11938214:
                    {
                        MemoryHandler field28 = Memory;
                        ulong num28 = 32545910UL;
                        bool flag28 = true;
                        int[] array28 = new int[] { 186, 122, 59, -12, -13, -13, 107, -13, -13, -13, 131, 131, 131, 131, 131, 131, 131, 131, 131 };
                        field28.WriteScrambledInts(num28, flag28, array28);
                        return;
                    }
                default:
                    return;
            }
        }
        else
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    {
                        MemoryHandler field29 = Memory;
                        ulong num29 = 29891808UL;
                        bool flag29 = true;
                        int[] array29 = new int[] { 59, 126, 194, 219, 142, 73, 241, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field29.WriteScrambledInts(num29, flag29, array29);
                        return;
                    }
                case GameBuild.Y2S3_11432634:
                    {
                        MemoryHandler field30 = Memory;
                        ulong num30 = 38064060UL;
                        bool flag30 = true;
                        int[] array30 = new int[] { 59, 126, 194, 219, 160, 136, 239, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field30.WriteScrambledInts(num30, flag30, array30);
                        return;
                    }
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field31 = Memory;
                        ulong num31 = 34412342UL;
                        bool flag31 = true;
                        int[] array31 = new int[] { 59, 126, 194, 219, 67, 95, 238, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field31.WriteScrambledInts(num31, flag31, array31);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field32 = Memory;
                        ulong num32 = 45623702UL;
                        bool flag32 = true;
                        int[] array32 = new int[] { 59, 126, 194, 219, 51, 114, 240, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field32.WriteScrambledInts(num32, flag32, array32);
                        MemoryHandler field33 = Memory;
                        ulong num33 = 47859628UL;
                        bool flag33 = true;
                        int[] array33 = new int[] { 2, 119, 138, -12, -13, -13 };
                        field33.WriteScrambledInts(num33, flag33, array33);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field34 = Memory;
                        ulong num34 = 46062582UL;
                        bool flag34 = true;
                        int[] array34 = new int[] { 59, 126, 194, 219, 131, 118, 240, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field34.WriteScrambledInts(num34, flag34, array34);
                        MemoryHandler field35 = Memory;
                        ulong num35 = 48281228UL;
                        bool flag35 = true;
                        int[] array35 = new int[] { 2, 119, 138, -12, -13, -13 };
                        field35.WriteScrambledInts(num35, flag35, array35);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                    {
                        MemoryHandler field36 = Memory;
                        ulong num36 = 30525878UL;
                        bool flag36 = true;
                        int[] array36 = new int[] { 59, 126, 194, 219, 211, 190, 240, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field36.WriteScrambledInts(num36, flag36, array36);
                        return;
                    }
                case GameBuild.Y4S2_13147883:
                    {
                        MemoryHandler field37 = Memory;
                        ulong num37 = 34626014UL;
                        bool flag37 = true;
                        int[] array37 = new int[] { 59, 126, 194, 219, 191, 134, -13, -13, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field37.WriteScrambledInts(num37, flag37, array37);
                        return;
                    }
                case GameBuild.Y4S1_12863847:
                    {
                        MemoryHandler field38 = Memory;
                        ulong num38 = 40447990UL;
                        bool flag38 = true;
                        int[] array38 = new int[] { 59, 126, 194, 219, 83, 133, -13, -13, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field38.WriteScrambledInts(num38, flag38, array38);
                        return;
                    }
                case GameBuild.Y3S1_11706399:
                    {
                        MemoryHandler field39 = Memory;
                        ulong num39 = 34396470UL;
                        bool flag39 = true;
                        int[] array39 = new int[] { 59, 126, 194, 219, 147, 99, 238, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field39.WriteScrambledInts(num39, flag39, array39);
                        return;
                    }
                case GameBuild.Y4S3_13632147:
                    {
                        MemoryHandler field40 = Memory;
                        ulong num40 = 48538302UL;
                        bool flag40 = true;
                        int[] array40 = new int[] { 59, 126, 194, 219, 239, 136, -13, -13, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field40.WriteScrambledInts(num40, flag40, array40);
                        return;
                    }
                case GameBuild.Y4S1_12815133:
                    {
                        MemoryHandler field41 = Memory;
                        ulong num41 = 40448086UL;
                        bool flag41 = true;
                        int[] array41 = new int[] { 59, 126, 194, 219, 83, 133, -13, -13, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field41.WriteScrambledInts(num41, flag41, array41);
                        return;
                    }
                case GameBuild.Y4S4_13924517:
                    {
                        MemoryHandler field42 = Memory;
                        ulong num42 = 39961188UL;
                        bool flag42 = true;
                        int[] array42 = new int[] { 59, 126, 194, 219, -4, 140, -13, -13, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field42.WriteScrambledInts(num42, flag42, array42);
                        return;
                    }
                case GameBuild.Y5S1_14303219:
                    {
                        MemoryHandler field43 = Memory;
                        ulong num43 = 51188428UL;
                        bool flag43 = true;
                        int[] array43 = new int[] { 59, 126, 194, 219, -8, 138, -13, -13, 118, 178, 91, -12, -13, -13, -13 };
                        field43.WriteScrambledInts(num43, flag43, array43);
                        return;
                    }
                case GameBuild.Y1S1_8519860:
                    {
                        MemoryHandler field44 = Memory;
                        ulong num44 = 30049856UL;
                        bool flag44 = true;
                        int[] array44 = new int[] { 59, 126, 194, 219, 14, 66, 241, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field44.WriteScrambledInts(num44, flag44, array44);
                        return;
                    }
                case GameBuild.Y1S2_9132097:
                    {
                        MemoryHandler field45 = Memory;
                        ulong num45 = 29762144UL;
                        bool flag45 = true;
                        int[] array45 = new int[] { 59, 126, 194, 219, 62, 13, 241, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field45.WriteScrambledInts(num45, flag45, array45);
                        return;
                    }
                case GameBuild.Y1S4_10211195:
                    {
                        MemoryHandler field46 = Memory;
                        ulong num46 = 32098678UL;
                        bool flag46 = true;
                        int[] array46 = new int[] { 59, 126, 194, 219, -13, 224, 239, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field46.WriteScrambledInts(num46, flag46, array46);
                        return;
                    }
                case GameBuild.Y2S1_10751226:
                    {
                        MemoryHandler field47 = Memory;
                        ulong num47 = 34903452UL;
                        bool flag47 = true;
                        int[] array47 = new int[] { 59, 126, 194, 219, 144, 185, 239, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field47.WriteScrambledInts(num47, flag47, array47);
                        return;
                    }
                case GameBuild.Y2S2_11216230:
                    {
                        MemoryHandler field48 = Memory;
                        ulong num48 = 35349436UL;
                        bool flag48 = true;
                        int[] array48 = new int[] { 59, 126, 194, 219, 96, 168, 239, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field48.WriteScrambledInts(num48, flag48, array48);
                        return;
                    }
                case GameBuild.Y2S4_11553121:
                    {
                        MemoryHandler field49 = Memory;
                        ulong num49 = 38443708UL;
                        bool flag49 = true;
                        int[] array49 = new int[] { 59, 126, 194, 219, 224, 157, 238, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field49.WriteScrambledInts(num49, flag49, array49);
                        return;
                    }
                case GameBuild.Y3S2_11965022:
                    {
                        MemoryHandler field50 = Memory;
                        ulong num50 = 32585590UL;
                        bool flag50 = true;
                        int[] array50 = new int[] { 59, 126, 194, 219, 51, 20, 238, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field50.WriteScrambledInts(num50, flag50, array50);
                        return;
                    }
                case GameBuild.Y3S4_12512571:
                    {
                        MemoryHandler field51 = Memory;
                        ulong num51 = 43817578UL;
                        bool flag51 = true;
                        int[] array51 = new int[] { 59, 126, 194, 219, 25, 158, -13, -13, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field51.WriteScrambledInts(num51, flag51, array51);
                        return;
                    }
                case GameBuild.Y2S3_11493221:
                    {
                        MemoryHandler field52 = Memory;
                        ulong num52 = 38314972UL;
                        bool flag52 = true;
                        int[] array52 = new int[] { 59, 126, 194, 219, 80, 132, 239, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field52.WriteScrambledInts(num52, flag52, array52);
                        return;
                    }
                case GameBuild.Y1S3_9654076:
                    {
                        MemoryHandler field53 = Memory;
                        ulong num53 = 30407040UL;
                        bool flag53 = true;
                        int[] array53 = new int[] { 59, 126, 194, 219, 78, 199, 240, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field53.WriteScrambledInts(num53, flag53, array53);
                        return;
                    }
                case GameBuild.Y4S4_13777760:
                    {
                        MemoryHandler field54 = Memory;
                        ulong num54 = 46134212UL;
                        bool flag54 = true;
                        int[] array54 = new int[] { 59, 126, 194, 219, 172, 138, -13, -13, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field54.WriteScrambledInts(num54, flag54, array54);
                        return;
                    }
                case GameBuild.Y2S4_11580709:
                    {
                        MemoryHandler field55 = Memory;
                        ulong num55 = 38436188UL;
                        bool flag55 = true;
                        int[] array55 = new int[] { 59, 126, 194, 219, 0, 154, 238, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field55.WriteScrambledInts(num55, flag55, array55);
                        return;
                    }
                case GameBuild.Y3S2_11938214:
                    {
                        MemoryHandler field56 = Memory;
                        ulong num56 = 32545910UL;
                        bool flag56 = true;
                        int[] array56 = new int[] { 59, 126, 194, 219, -13, 19, 238, 242, 89, 2, 97, 179, 2, 78, 179, 230, 2, 79, 227 };
                        field56.WriteScrambledInts(num56, flag56, array56);
                        return;
                    }
                default:
                    return;
            }
        }
    }

    public void SetBottomlessMagazine(bool enabled)
    {
        if (enabled)
        {
            GameBuild @enum = _build;
            switch (@enum)
            {
                case GameBuild.Y1S0_8194013:
                    {
                        MemoryHandler field = Memory;
                        ulong num = 19468806UL;
                        bool flag = true;
                        int[] array = new int[] { 59, 126, 190, 173, 242, 242, 242, 2, 220, 59, 192, 56, -13, 131 };
                        field.WriteScrambledInts(num, flag, array);
                        MemoryHandler field2 = Memory;
                        ulong num2 = 28617892UL;
                        bool flag2 = true;
                        int[] array2 = new int[] { 220, 159, 37, 173, 242, 131 };
                        field2.WriteScrambledInts(num2, flag2, array2);
                        return;
                    }
                case GameBuild.Y2S3_11432634:
                    {
                        MemoryHandler field3 = Memory;
                        ulong num3 = 25242758UL;
                        bool flag3 = true;
                        int[] array3 = new int[] { 55, 126, 227, 173, 242, 242, 242, 2, 220, 85, 2, 126, -13 };
                        field3.WriteScrambledInts(num3, flag3, array3);
                        MemoryHandler field4 = Memory;
                        ulong num4 = 43469656UL;
                        bool flag4 = true;
                        int[] array4 = new int[] { 220, 133, 227, 103, 242, 131 };
                        field4.WriteScrambledInts(num4, flag4, array4);
                        return;
                    }
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field5 = Memory;
                        ulong num5 = 22229286UL;
                        bool flag5 = true;
                        int[] array5 = new int[] { 55, 126, 227, 173, 242, 242, 242, 2, 220, 133, 4, 142, -13 };
                        field5.WriteScrambledInts(num5, flag5, array5);
                        MemoryHandler field6 = Memory;
                        ulong num6 = 42554456UL;
                        bool flag6 = true;
                        int[] array6 = new int[] { 220, 85, 225, 87, 242, 131 };
                        field6.WriteScrambledInts(num6, flag6, array6);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field7 = Memory;
                        ulong num7 = 26465798UL;
                        bool flag7 = true;
                        int[] array7 = new int[] { 173, 242, 242, 242, 2, 59, 126, 194, 220, 60, 182, 121, -13 };
                        field7.WriteScrambledInts(num7, flag7, array7);
                        MemoryHandler field8 = Memory;
                        ulong num8 = 44129446UL;
                        bool flag8 = true;
                        int[] array8 = new int[] { 220, 158, 47, 108, 242, 131 };
                        field8.WriteScrambledInts(num8, flag8, array8);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field9 = Memory;
                        ulong num9 = 28269222UL;
                        bool flag9 = true;
                        int[] array9 = new int[] { 173, 242, 242, 242, 2, 59, 126, 194, 220, 44, 82, 111, -13 };
                        field9.WriteScrambledInts(num9, flag9, array9);
                        MemoryHandler field10 = Memory;
                        ulong num10 = 44570918UL;
                        bool flag10 = true;
                        int[] array10 = new int[] { 220, 174, 147, 118, 242, 131 };
                        field10.WriteScrambledInts(num10, flag10, array10);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                    {
                        MemoryHandler field11 = Memory;
                        ulong num11 = 18756870UL;
                        bool flag11 = true;
                        int[] array11 = new int[] { 55, 126, 184, 173, 242, 242, 242, 2, 220, 135, -2, 84, -13, 131 };
                        field11.WriteScrambledInts(num11, flag11, array11);
                        MemoryHandler field12 = Memory;
                        ulong num12 = 31476798UL;
                        bool flag12 = true;
                        int[] array12 = new int[] { 220, 82, 231, 145, 242 };
                        field12.WriteScrambledInts(num12, flag12, array12);
                        return;
                    }
                case GameBuild.Y4S2_13147883:
                    {
                        MemoryHandler field13 = Memory;
                        ulong num13 = 21131846UL;
                        bool flag13 = true;
                        int[] array13 = new int[] { 176, 242, 242, 242, 2, 59, 126, 190, 220, 35, 38, 140 };
                        field13.WriteScrambledInts(num13, flag13, array13);
                        MemoryHandler field14 = Memory;
                        ulong num14 = 41212084UL;
                        bool flag14 = true;
                        int[] array14 = new int[] { 220, 183, 191, 89, 242, 131 };
                        field14.WriteScrambledInts(num14, flag14, array14);
                        return;
                    }
                default:
                    if (@enum == GameBuild.Y2S3_11493221)
                    {
                        MemoryHandler field15 = Memory;
                        ulong num15 = 4456902UL;
                        bool flag15 = true;
                        int[] array15 = new int[] { 55, 126, 227, 173, 242, 242, 242, 2, 220, 53, 92, 38, -12 };
                        field15.WriteScrambledInts(num15, flag15, array15);
                        MemoryHandler field16 = Memory;
                        ulong num16 = 44749912UL;
                        bool flag16 = true;
                        int[] array16 = new int[] { 220, 165, 137, 191, 241, 131 };
                        field16.WriteScrambledInts(num16, flag16, array16);
                        return;
                    }
                    if (@enum == GameBuild.Y1S3_9654076)
                    {
                        MemoryHandler field17 = Memory;
                        ulong num17 = 18756870UL;
                        bool flag17 = true;
                        int[] array17 = new int[] { 55, 126, 184, 173, 242, 242, 242, 2, 220, 135, -2, 84, -13, 131 };
                        field17.WriteScrambledInts(num17, flag17, array17);
                        MemoryHandler field18 = Memory;
                        ulong num18 = 31476798UL;
                        bool flag18 = true;
                        int[] array18 = new int[] { 220, 82, 231, 145, 242 };
                        field18.WriteScrambledInts(num18, flag18, array18);
                        return;
                    }
                    return;
            }
        }
        else
        {
            GameBuild @enum = _build;
            switch (@enum)
            {
                case GameBuild.Y1S0_8194013:
                    {
                        MemoryHandler field19 = Memory;
                        ulong num19 = 19468806UL;
                        bool flag19 = true;
                        int[] array19 = new int[] { 59, 118, 223, 27, 126, -8, 17, 89, 124, -8, 155, -12, 104, 38 };
                        field19.WriteScrambledInts(num19, flag19, array19);
                        MemoryHandler field20 = Memory;
                        ulong num20 = 28617892UL;
                        bool flag20 = true;
                        int[] array20 = new int[] { 59, 126, 190, 52, 126, 202 };
                        field20.WriteScrambledInts(num20, flag20, array20);
                        return;
                    }
                case GameBuild.Y2S3_11432634:
                    {
                        MemoryHandler field21 = Memory;
                        ulong num21 = 25242758UL;
                        bool flag21 = true;
                        int[] array21 = new int[] { 59, 118, 223, 43, 88, 59, 126, -9, 24, 75, -13, -13, -13 };
                        field21.WriteScrambledInts(num21, flag21, array21);
                        MemoryHandler field22 = Memory;
                        ulong num22 = 43469656UL;
                        bool flag22 = true;
                        int[] array22 = new int[] { 55, 126, 227, 52, 126, 199 };
                        field22.WriteScrambledInts(num22, flag22, array22);
                        return;
                    }
                case GameBuild.Y3S1_11726982:
                    {
                        MemoryHandler field23 = Memory;
                        ulong num23 = 22229286UL;
                        bool flag23 = true;
                        int[] array23 = new int[] { 59, 118, 223, 43, 88, 59, 126, -9, 24, 75, -13, -13, -13 };
                        field23.WriteScrambledInts(num23, flag23, array23);
                        MemoryHandler field24 = Memory;
                        ulong num24 = 42554456UL;
                        bool flag24 = true;
                        int[] array24 = new int[] { 55, 126, 227, 52, 126, 199 };
                        field24.WriteScrambledInts(num24, flag24, array24);
                        return;
                    }
                case GameBuild.Y3S3_12213419:
                    {
                        MemoryHandler field25 = Memory;
                        ulong num25 = 26465798UL;
                        bool flag25 = true;
                        int[] array25 = new int[] { 59, 118, 223, 27, 88, 59, 126, -9, 24, 75, -13, -13, -13 };
                        field25.WriteScrambledInts(num25, flag25, array25);
                        MemoryHandler field26 = Memory;
                        ulong num26 = 44129446UL;
                        bool flag26 = true;
                        int[] array26 = new int[] { 52, 126, 202, 59, 126, 194 };
                        field26.WriteScrambledInts(num26, flag26, array26);
                        return;
                    }
                case GameBuild.Y3S3_12362767:
                    {
                        MemoryHandler field27 = Memory;
                        ulong num27 = 28269222UL;
                        bool flag27 = true;
                        int[] array27 = new int[] { 59, 118, 223, 27, 88, 59, 126, -9, 24, 75, -13, -13, -13 };
                        field27.WriteScrambledInts(num27, flag27, array27);
                        MemoryHandler field28 = Memory;
                        ulong num28 = 44570918UL;
                        bool flag28 = true;
                        int[] array28 = new int[] { 52, 126, 202, 59, 126, 194 };
                        field28.WriteScrambledInts(num28, flag28, array28);
                        return;
                    }
                case GameBuild.Y1S3_9860556:
                    {
                        MemoryHandler field29 = Memory;
                        ulong num29 = 18756870UL;
                        bool flag29 = true;
                        int[] array29 = new int[] { 59, 118, 223, 43, 126, -8, 193, -12, 115, -8, 155, -12, 104, 38 };
                        field29.WriteScrambledInts(num29, flag29, array29);
                        MemoryHandler field30 = Memory;
                        ulong num30 = 31476798UL;
                        bool flag30 = true;
                        int[] array30 = new int[] { 55, 126, 184, 126, 202 };
                        field30.WriteScrambledInts(num30, flag30, array30);
                        return;
                    }
                case GameBuild.Y4S2_13147883:
                    {
                        MemoryHandler field31 = Memory;
                        ulong num31 = 21131846UL;
                        bool flag31 = true;
                        int[] array31 = new int[] { 124, 63, 23, -5, 59, 118, 223, 27, 171, 39, 24, -13 };
                        field31.WriteScrambledInts(num31, flag31, array31);
                        MemoryHandler field32 = Memory;
                        ulong num32 = 41212084UL;
                        bool flag32 = true;
                        int[] array32 = new int[] { 2, 57, 220, 59, 126, 190 };
                        field32.WriteScrambledInts(num32, flag32, array32);
                        return;
                    }
                default:
                    {
                        if (@enum != GameBuild.Y2S3_11493221)
                        {
                            return;
                        }
                        MemoryHandler field33 = Memory;
                        ulong num33 = 4456902UL;
                        bool flag33 = true;
                        int[] array33 = new int[] { 59, 118, 223, 43, 88, 59, 126, -9, 24, 75, -13, -13, -13 };
                        field33.WriteScrambledInts(num33, flag33, array33);
                        MemoryHandler field34 = Memory;
                        ulong num34 = 44749912UL;
                        bool flag34 = true;
                        int[] array34 = new int[] { 55, 126, 227, 52, 126, 199 };
                        field34.WriteScrambledInts(num34, flag34, array34);
                        return;
                    }
            }
        }
    }

    public void ApplyCorePatch(bool enabled)
    {
        if (enabled)
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    Memory.WriteNops(26845714UL, 2, true, true);
                    return;
                case GameBuild.Y2S3_11432634:
                    Memory.WriteScrambledInts(34932566UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y3S1_11726982:
                    Memory.WriteScrambledInts(45607492UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y3S3_12213419:
                    Memory.WriteScrambledInts(48218774UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y3S3_12362767:
                    Memory.WriteScrambledInts(41161174UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y1S3_9860556:
                    Memory.WriteNops(27158920UL, 2, true, true);
                    return;
                case GameBuild.Y4S2_13147883:
                    Memory.WriteScrambledInts(42694932UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y4S1_12863847:
                    Memory.WriteScrambledInts(34600460UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y3S1_11706399:
                    Memory.WriteScrambledInts(45575396UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y4S3_13632147:
                    Memory.WriteScrambledInts(40777036UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y4S1_12815133:
                    Memory.WriteScrambledInts(34600556UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y4S4_13924517:
                    Memory.WriteNops(27528250UL, 2, true, true);
                    return;
                case GameBuild.Y5S1_14303219:
                    Memory.WriteNops(55174316UL, 2, true, true);
                    return;
                case GameBuild.Y1S1_8519860:
                    Memory.WriteNops(26942696UL, 2, true, true);
                    return;
                case GameBuild.Y1S2_9132097:
                    Memory.WriteNops(26479624UL, 2, true, true);
                    return;
                case GameBuild.Y1S4_10211195:
                    Memory.WriteNops(28240104UL, 2, true, true);
                    return;
                case GameBuild.Y2S1_10751226:
                    Memory.WriteNops(30574830UL, 2, true, true);
                    return;
                case GameBuild.Y2S2_11216230:
                    Memory.WriteNops(31001902UL, 2, true, true);
                    return;
                case GameBuild.Y2S4_11553121:
                    Memory.WriteScrambledInts(35035414UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y3S2_11965022:
                    Memory.WriteScrambledInts(43702682UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y3S4_12512571:
                    Memory.WriteScrambledInts(37837290UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y2S3_11493221:
                    Memory.WriteScrambledInts(35174934UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y1S3_9654076:
                    Memory.WriteNops(27113512UL, 2, true, true);
                    return;
                case GameBuild.Y4S4_13777760:
                    Memory.WriteNops(36316410UL, 2, true, true);
                    return;
                case GameBuild.Y2S4_11580709:
                    Memory.WriteScrambledInts(35031036UL, true, new int[] { 222 });
                    return;
                case GameBuild.Y3S2_11938214:
                    Memory.WriteScrambledInts(43672122UL, true, new int[] { 222 });
                    return;
                default:
                    return;
            }
        }
        else
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    Memory.WriteScrambledInts(26845714UL, true, new int[] { 103, 32 });
                    return;
                case GameBuild.Y2S3_11432634:
                    Memory.WriteScrambledInts(34932566UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y3S1_11726982:
                    Memory.WriteScrambledInts(45607492UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y3S3_12213419:
                    Memory.WriteScrambledInts(48218774UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y3S3_12362767:
                    Memory.WriteScrambledInts(41161174UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y1S3_9860556:
                    Memory.WriteScrambledInts(27158920UL, true, new int[] { 103, 24 });
                    return;
                case GameBuild.Y4S2_13147883:
                    Memory.WriteScrambledInts(42694932UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y4S1_12863847:
                    Memory.WriteScrambledInts(34600460UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y3S1_11706399:
                    Memory.WriteScrambledInts(45575396UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y4S3_13632147:
                    Memory.WriteScrambledInts(40777036UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y4S1_12815133:
                    Memory.WriteScrambledInts(34600556UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y4S4_13924517:
                    Memory.WriteScrambledInts(27528250UL, true, new int[] { 103, -7 });
                    return;
                case GameBuild.Y5S1_14303219:
                    Memory.WriteScrambledInts(55174316UL, true, new int[] { 103, -4 });
                    return;
                case GameBuild.Y1S1_8519860:
                    Memory.WriteScrambledInts(26942696UL, true, new int[] { 103, 24 });
                    return;
                case GameBuild.Y1S2_9132097:
                    Memory.WriteScrambledInts(26942696UL, true, new int[] { 103, 24 });
                    return;
                case GameBuild.Y1S4_10211195:
                    Memory.WriteScrambledInts(28240104UL, true, new int[] { 103, 24 });
                    return;
                case GameBuild.Y2S1_10751226:
                    Memory.WriteScrambledInts(30574830UL, true, new int[] { 103, 24 });
                    return;
                case GameBuild.Y2S2_11216230:
                    Memory.WriteScrambledInts(31001902UL, true, new int[] { 103, 24 });
                    return;
                case GameBuild.Y2S4_11553121:
                    Memory.WriteScrambledInts(35035414UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y3S2_11965022:
                    Memory.WriteScrambledInts(43702682UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y3S4_12512571:
                    Memory.WriteScrambledInts(37837290UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y2S3_11493221:
                    Memory.WriteScrambledInts(35174934UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y1S3_9654076:
                    Memory.WriteScrambledInts(27113512UL, true, new int[] { 103, 24 });
                    return;
                case GameBuild.Y4S4_13777760:
                    Memory.WriteScrambledInts(36316410UL, true, new int[] { 103, -7 });
                    return;
                case GameBuild.Y2S4_11580709:
                    Memory.WriteScrambledInts(35031036UL, true, new int[] { 104 });
                    return;
                case GameBuild.Y3S2_11938214:
                    Memory.WriteScrambledInts(43672122UL, true, new int[] { 104 });
                    return;
                default:
                    return;
            }
        }
    }

    public void SetDisableAI(bool enabled)
    {
        if (enabled)
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    Memory.WriteScrambledInts(29642886UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y2S3_11432634:
                    Memory.WriteScrambledInts(42867142UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y3S1_11726982:
                    Memory.WriteScrambledInts(48302598UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y3S3_12213419:
                    Memory.WriteScrambledInts(47292838UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y3S3_12362767:
                    Memory.WriteScrambledInts(47718118UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y1S3_9860556:
                    Memory.WriteScrambledInts(38699526UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y4S2_13147883:
                    Memory.WriteScrambledInts(33702022UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y4S1_12863847:
                    Memory.WriteScrambledInts(36054950UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y3S1_11706399:
                    break;
                case GameBuild.Y4S3_13632147:
                    Memory.WriteScrambledInts(32510342UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y4S1_12815133:
                    Memory.WriteScrambledInts(36055046UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y4S4_13924517:
                    Memory.WriteScrambledInts(38906726UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y5S1_14303219:
                    Memory.WriteScrambledInts(34929894UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y1S1_8519860:
                    Memory.WriteScrambledInts(29796550UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y1S2_9132097:
                    Memory.WriteScrambledInts(36012198UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y1S4_10211195:
                    Memory.WriteScrambledInts(40822278UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y2S1_10751226:
                    Memory.WriteScrambledInts(34448262UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y2S2_11216230:
                    Memory.WriteScrambledInts(45096838UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y2S4_11553121:
                    Memory.WriteScrambledInts(49746854UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y3S2_11965022:
                    Memory.WriteScrambledInts(42815270UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y3S4_12512571:
                    Memory.WriteScrambledInts(39556294UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y2S3_11493221:
                    Memory.WriteScrambledInts(43147174UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y1S3_9654076:
                    Memory.WriteScrambledInts(38521606UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y4S4_13777760:
                    Memory.WriteScrambledInts(39014854UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y2S4_11580709:
                    Memory.WriteScrambledInts(49739590UL, true, new int[] { 182 });
                    return;
                case GameBuild.Y3S2_11938214:
                    Memory.WriteScrambledInts(42779334UL, true, new int[] { 182 });
                    return;
                default:
                    return;
            }
        }
        else
        {
            switch (_build)
            {
                case GameBuild.Y1S0_8194013:
                    Memory.WriteScrambledInts(29642886UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y2S3_11432634:
                    Memory.WriteScrambledInts(42867142UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y3S1_11726982:
                    Memory.WriteScrambledInts(48302598UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y3S3_12213419:
                    Memory.WriteScrambledInts(47292838UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y3S3_12362767:
                    Memory.WriteScrambledInts(47718118UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y1S3_9860556:
                    Memory.WriteScrambledInts(38699526UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y4S2_13147883:
                    Memory.WriteScrambledInts(33702022UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y4S1_12863847:
                    Memory.WriteScrambledInts(36054950UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y3S1_11706399:
                    break;
                case GameBuild.Y4S3_13632147:
                    Memory.WriteScrambledInts(32510342UL, true, new int[] { 51 });
                    return;
                case GameBuild.Y4S1_12815133:
                    Memory.WriteScrambledInts(36055046UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y4S4_13924517:
                    Memory.WriteScrambledInts(38906726UL, true, new int[] { 51 });
                    return;
                case GameBuild.Y5S1_14303219:
                    Memory.WriteScrambledInts(34929894UL, true, new int[] { 51 });
                    break;
                case GameBuild.Y1S1_8519860:
                    Memory.WriteScrambledInts(29796550UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y1S2_9132097:
                    Memory.WriteScrambledInts(36012198UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y1S4_10211195:
                    Memory.WriteScrambledInts(40822278UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y2S1_10751226:
                    Memory.WriteScrambledInts(34448262UL, true, new int[] { 51 });
                    return;
                case GameBuild.Y2S2_11216230:
                    Memory.WriteScrambledInts(45096838UL, true, new int[] { 51 });
                    return;
                case GameBuild.Y2S4_11553121:
                    Memory.WriteScrambledInts(49746854UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y3S2_11965022:
                    Memory.WriteScrambledInts(42815270UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y3S4_12512571:
                    Memory.WriteScrambledInts(39556294UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y2S3_11493221:
                    Memory.WriteScrambledInts(43147174UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y1S3_9654076:
                    Memory.WriteScrambledInts(38521606UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y4S4_13777760:
                    Memory.WriteScrambledInts(39014854UL, true, new int[] { 51 });
                    return;
                case GameBuild.Y2S4_11580709:
                    Memory.WriteScrambledInts(49739590UL, true, new int[] { 59 });
                    return;
                case GameBuild.Y3S2_11938214:
                    Memory.WriteScrambledInts(42779334UL, true, new int[] { 59 });
                    return;
                default:
                    return;
            }
        }
    }

    public bool IsFullyLaunched()
    {
        switch (_build)
        {
            case GameBuild.Y1S0_8194013:
                return Memory.ReadScrambledBool(185917734UL, true);
            case GameBuild.Y2S3_11432634:
                return Memory.ReadScrambledBool(148556982UL, true);
            case GameBuild.Y3S1_11726982:
                return Memory.ReadScrambledBool(155755046UL, true);
            case GameBuild.Y3S3_12213419:
                return Memory.ReadScrambledBool(157696038UL, true);
            case GameBuild.Y3S3_12362767:
                return Memory.ReadScrambledBool(158059094UL, true);
            case GameBuild.Y1S3_9860556:
                return Memory.ReadScrambledBool(178395574UL, true);
            case GameBuild.Y4S2_13147883:
                return Memory.ReadScrambledBool(167806790UL, true);
            case GameBuild.Y4S1_12863847:
            case GameBuild.Y4S1_12815133:
                return Memory.ReadScrambledBool(184062038UL, true);
            case GameBuild.Y4S3_13632147:
                return Memory.ReadScrambledBool(172428094UL, true);
            case GameBuild.Y4S4_13924517:
                return Memory.ReadScrambledBool(173688326UL, true);
            case GameBuild.Y5S1_14303219:
                return Memory.ReadScrambledBool(175585190UL, true);
            case GameBuild.Y1S1_8519860:
                return Memory.ReadScrambledBool(191136934UL, true);
            case GameBuild.Y1S2_9132097:
                return Memory.ReadScrambledBool(177832758UL, true);
            case GameBuild.Y1S4_10211195:
                return Memory.ReadScrambledBool(108486710UL, true);
            case GameBuild.Y2S1_10751226:
                return Memory.ReadScrambledBool(135524166UL, true);
            case GameBuild.Y2S2_11216230:
                return Memory.ReadScrambledBool(136451110UL, true);
            case GameBuild.Y2S4_11553121:
                return Memory.ReadScrambledBool(150406166UL, true);
            case GameBuild.Y3S2_11965022:
                return Memory.ReadScrambledBool(155716358UL, true);
            case GameBuild.Y3S4_12512571:
                return Memory.ReadScrambledBool(185964470UL, true);
            case GameBuild.Y2S3_11493221:
                return Memory.ReadScrambledBool(148945126UL, true);
            case GameBuild.Y1S3_9654076:
                return Memory.ReadScrambledBool(178152310UL, true);
            case GameBuild.Y4S4_13777760:
                return Memory.ReadScrambledBool(173944006UL, true);
            case GameBuild.Y2S4_11580709:
                return Memory.ReadScrambledBool(150415078UL, true);
            case GameBuild.Y3S2_11938214:
                return Memory.ReadScrambledBool(155704246UL, true);
        }
        return false;
    }

    public void SetMadHouseMode(int variant)
    {
        MemoryHandler field = Memory;
        ulong num = 169242450UL;
        int num2 = -1;
        int[] array = new int[] { 9650, 9570, 9522, 16210 };
        ulong num3 = field.ResolvePointerChain(num, num2, array);
        if (_build == GameBuild.Y3S3_12362767)
        {
            if (variant == 0)
            {
                Memory.WriteInt64(num3, 161293383655L);
                return;
            }
            Memory.WriteInt64(num3, _madHouseGametypes[variant - 1]);
        }
    }

    public void SetGametype(long gametypeId)
    {
        ulong num;
        switch (_build)
        {
            case GameBuild.Y1S0_8194013:
                num = Memory.ResolvePointerChain(185612930UL, -1, new int[] { 9698 });
                break;
            case GameBuild.Y2S3_11432634:
                num = Memory.ResolvePointerChain(148243874UL, -1, new int[] { 15458 });
                break;
            case GameBuild.Y3S1_11726982:
                num = Memory.ResolvePointerChain(156344802UL, -1, new int[] { 14754 });
                break;
            case GameBuild.Y3S3_12213419:
                num = Memory.ResolvePointerChain(157394466UL, -1, new int[] { 16114 });
                break;
            case GameBuild.Y3S3_12362767:
                num = Memory.ResolvePointerChain(157744626UL, -1, new int[] { 15266 });
                break;
            case GameBuild.Y1S3_9860556:
                num = Memory.ResolvePointerChain(203090562UL, -1, new int[] { 9698 });
                break;
            case GameBuild.Y4S2_13147883:
                num = Memory.ResolvePointerChain(167503682UL, -1, new int[] { 15666 });
                break;
            case GameBuild.Y4S1_12863847:
                num = Memory.ResolvePointerChain(183815826UL, -1, new int[] { 16930 });
                break;
            case GameBuild.Y3S1_11706399:
                num = Memory.ResolvePointerChain(155466818UL, -1, new int[] { 12930 });
                break;
            case GameBuild.Y4S3_13632147:
                num = Memory.ResolvePointerChain(172280226UL, -1, new int[] { 16338 });
                break;
            case GameBuild.Y4S1_12815133:
                num = Memory.ResolvePointerChain(183815826UL, -1, new int[] { 16930 });
                break;
            case GameBuild.Y4S4_13924517:
                num = Memory.ResolvePointerChain(173385250UL, -1, new int[] { 16834 });
                break;
            case GameBuild.Y5S1_14303219:
                return;
            case GameBuild.Y1S1_8519860:
                num = Memory.ResolvePointerChain(190831378UL, -1, new int[] { 9698 });
                break;
            case GameBuild.Y1S2_9132097:
                num = Memory.ResolvePointerChain(177527042UL, -1, new int[] { 9698 });
                break;
            case GameBuild.Y1S4_10211195:
                num = Memory.ResolvePointerChain(108180802UL, -1, new int[] { 9698 });
                break;
            case GameBuild.Y2S1_10751226:
                num = Memory.ResolvePointerChain(135218706UL, -1, new int[] { 9698 });
                break;
            case GameBuild.Y2S2_11216230:
                num = Memory.ResolvePointerChain(142138994UL, -1, new int[] { 15474 });
                break;
            case GameBuild.Y2S4_11553121:
                num = Memory.ResolvePointerChain(150093074UL, -1, new int[] { 15570 });
                break;
            case GameBuild.Y3S2_11965022:
                num = Memory.ResolvePointerChain(165169250UL, -1, new int[] { 15154 });
                break;
            case GameBuild.Y3S4_12512571:
                num = Memory.ResolvePointerChain(166803730UL, -1, new int[] { 15442 });
                break;
            case GameBuild.Y2S3_11493221:
                num = Memory.ResolvePointerChain(148632002UL, -1, new int[] { 15458 });
                break;
            case GameBuild.Y1S3_9654076:
                num = Memory.ResolvePointerChain(177846434UL, -1, new int[] { 9698 });
                break;
            case GameBuild.Y4S4_13777760:
                num = Memory.ResolvePointerChain(173640962UL, -1, new int[] { 16834 });
                break;
            case GameBuild.Y2S4_11580709:
                num = Memory.ResolvePointerChain(150102034UL, -1, new int[] { 15570 });
                break;
            case GameBuild.Y3S2_11938214:
                num = Memory.ResolvePointerChain(155388946UL, -1, new int[] { 15154 });
                break;
            default:
                return;
        }
        Memory.WriteInt64(num, gametypeId);
    }

    public TreeView BuildGametypeTree()
    {
        bool flag = false;
        int num = 0;
        ulong num2 = 0UL;
        ulong num3 = 0UL;
        int num4 = 40;
        int num5 = 9;
        int num6 = 0;
        int num7 = 1;
        int num8 = 2;
        int num9 = 3;
        int num10 = 4;
        int num11 = 5;
        int num12 = -1;
        bool flag2 = false;
        R6S.EventMode @enum = R6S.EventMode.None;
        List<int> list = new List<int>();
        switch (_build)
        {
            case GameBuild.Y1S0_8194013:
                {
                    MemoryHandler field = Memory;
                    ulong num13 = 185590706UL;
                    int num14 = -1;
                    int[] array = new int[] { 9618, 9666, 11394 };
                    num2 = field.ResolvePointerChain(num13, num14, array);
                    num = 184;
                    num3 = 24UL;
                    flag2 = true;
                    list.Add(2);
                    list.Add(6);
                    list.Add(7);
                    break;
                }
            case GameBuild.Y2S3_11432634:
                {
                    MemoryHandler field2 = Memory;
                    ulong num15 = 144888690UL;
                    int num16 = -1;
                    int[] array2 = new int[] { 9618, 9666, 11154 };
                    num2 = field2.ResolvePointerChain(num15, num16, array2);
                    num = 128;
                    num3 = 24UL;
                    list.Add(4);
                    num5 = 6;
                    num11 = -1;
                    break;
                }
            case GameBuild.Y3S1_11726982:
                {
                    MemoryHandler field3 = Memory;
                    ulong num17 = 155397330UL;
                    int num18 = -1;
                    int[] array3 = new int[] { 9634, 9666, 11762 };
                    num2 = field3.ResolvePointerChain(num17, num18, array3);
                    num = 128;
                    num3 = 32UL;
                    num5 = 6;
                    num11 = -1;
                    @enum = R6S.EventMode.Outbreak;
                    num12 = 4;
                    num10 = 5;
                    break;
                }
            case GameBuild.Y3S3_12213419:
                {
                    MemoryHandler field4 = Memory;
                    ulong num19 = 157346946UL;
                    int num20 = -1;
                    int[] array4 = new int[] { 9554, 9666, 13106 };
                    num2 = field4.ResolvePointerChain(num19, num20, array4);
                    num = 80;
                    num3 = 32UL;
                    num4 = 128;
                    num5 = 6;
                    num11 = -1;
                    list.Add(4);
                    break;
                }
            case GameBuild.Y3S3_12362767:
                {
                    MemoryHandler field5 = Memory;
                    ulong num21 = 157709954UL;
                    int num22 = -1;
                    int[] array5 = new int[] { 9554, 9666, 12978 };
                    num2 = field5.ResolvePointerChain(num21, num22, array5);
                    num = 80;
                    num3 = 32UL;
                    num4 = 128;
                    num5 = 6;
                    num11 = -1;
                    list.Add(4);
                    @enum = R6S.EventMode.Mad_House;
                    break;
                }
            case GameBuild.Y1S3_9860556:
                {
                    MemoryHandler field6 = Memory;
                    ulong num23 = 178046530UL;
                    int num24 = -1;
                    int[] array6 = new int[] { 9666, 9666, 12210 };
                    num2 = field6.ResolvePointerChain(num23, num24, array6);
                    num = 128;
                    num3 = 24UL;
                    num5 = 8;
                    list.Add(5);
                    list.Add(6);
                    break;
                }
            case GameBuild.Y4S2_13147883:
                {
                    MemoryHandler field7 = Memory;
                    ulong num25 = 126484546UL;
                    int num26 = -1;
                    int[] array7 = new int[] { 9554, 9666, 11762 };
                    num2 = field7.ResolvePointerChain(num25, num26, array7);
                    num = 80;
                    num3 = 32UL;
                    num4 = 128;
                    num5 = 6;
                    num11 = -1;
                    list.Add(4);
                    @enum = R6S.EventMode.Showdown;
                    break;
                }
            case GameBuild.Y4S1_12863847:
                {
                    MemoryHandler field8 = Memory;
                    ulong num27 = 123622866UL;
                    int num28 = -1;
                    int[] array8 = new int[] { 9554, 9666, 11762 };
                    num2 = field8.ResolvePointerChain(num27, num28, array8);
                    num = 80;
                    num3 = 32UL;
                    num4 = 128;
                    num5 = 6;
                    num11 = -1;
                    list.Add(4);
                    @enum = R6S.EventMode.Rainbow_Is_Magic;
                    break;
                }
            case GameBuild.Y4S3_13632147:
                {
                    MemoryHandler field9 = Memory;
                    ulong num29 = 172369474UL;
                    int num30 = -1;
                    int[] array9 = new int[] { 9922, 9682, 17458 };
                    num2 = field9.ResolvePointerChain(num29, num30, array9);
                    num = 80;
                    num3 = 32UL;
                    num4 = 128;
                    num5 = 6;
                    num11 = -1;
                    list.Add(4);
                    @enum = R6S.EventMode.Doktors_Curse_MoneyHeist;
                    break;
                }
            case GameBuild.Y4S1_12815133:
                {
                    MemoryHandler field10 = Memory;
                    ulong num31 = 123622866UL;
                    int num32 = -1;
                    int[] array10 = new int[] { 9554, 9666, 11762 };
                    num2 = field10.ResolvePointerChain(num31, num32, array10);
                    num = 80;
                    num3 = 32UL;
                    num4 = 128;
                    num5 = 6;
                    num11 = -1;
                    list.Add(4);
                    @enum = R6S.EventMode.Rainbow_Is_Magic;
                    break;
                }
            case GameBuild.Y4S4_13924517:
                {
                    MemoryHandler field11 = Memory;
                    ulong num33 = 173716082UL;
                    int num34 = -1;
                    int[] array11 = new int[] { 9698, 9554, 10546, 9906, 0 };
                    num2 = field11.ResolvePointerChain(num33, num34, array11);
                    num = 80;
                    num3 = 32UL;
                    num4 = 128;
                    num5 = 5;
                    num11 = -1;
                    @enum = R6S.EventMode.Stadium;
                    break;
                }
            case GameBuild.Y5S1_14303219:
                {
                    MemoryHandler field12 = Memory;
                    ulong num35 = 175547314UL;
                    int num36 = -1;
                    int[] array12 = new int[] { 9682, 9650, 10130, 10226 };
                    num2 = field12.ResolvePointerChain(num35, num36, array12);
                    num = 80;
                    num3 = 32UL;
                    num4 = 208;
                    num5 = 5;
                    num11 = -1;
                    @enum = R6S.EventMode.Grand_Larceny;
                    break;
                }
            case GameBuild.Y1S1_8519860:
                {
                    MemoryHandler field13 = Memory;
                    ulong num37 = 190808754UL;
                    int num38 = -1;
                    int[] array13 = new int[] { 9618, 9666, 11394 };
                    num2 = field13.ResolvePointerChain(num37, num38, array13);
                    num = 184;
                    num3 = 24UL;
                    list.Add(2);
                    list.Add(6);
                    list.Add(7);
                    break;
                }
            case GameBuild.Y1S2_9132097:
                {
                    MemoryHandler field14 = Memory;
                    ulong num39 = 177483666UL;
                    int num40 = -1;
                    int[] array14 = new int[] { 9618, 9666, 11346 };
                    num2 = field14.ResolvePointerChain(num39, num40, array14);
                    num = 184;
                    num3 = 24UL;
                    list.Add(2);
                    list.Add(6);
                    list.Add(7);
                    break;
                }
            case GameBuild.Y1S4_10211195:
                {
                    MemoryHandler field15 = Memory;
                    ulong num41 = 108137490UL;
                    int num42 = -1;
                    int[] array15 = new int[] { 9666, 9666, 12354 };
                    num2 = field15.ResolvePointerChain(num41, num42, array15);
                    num = 128;
                    num3 = 24UL;
                    num5 = 8;
                    list.Add(5);
                    list.Add(6);
                    break;
                }
            case GameBuild.Y2S1_10751226:
                {
                    MemoryHandler field16 = Memory;
                    ulong num43 = 135176610UL;
                    int num44 = -1;
                    int[] array16 = new int[] { 9666, 9666, 11778 };
                    num2 = field16.ResolvePointerChain(num43, num44, array16);
                    num = 128;
                    num3 = 24UL;
                    num5 = 11;
                    list.Add(4);
                    list.Add(5);
                    list.Add(6);
                    list.Add(8);
                    list.Add(9);
                    break;
                }
            case GameBuild.Y2S2_11216230:
                {
                    MemoryHandler field17 = Memory;
                    ulong num45 = 136103490UL;
                    int num46 = -1;
                    int[] array17 = new int[] { 9666, 9666, 11874 };
                    num2 = field17.ResolvePointerChain(num45, num46, array17);
                    num = 128;
                    num3 = 24UL;
                    num5 = 11;
                    list.Add(4);
                    list.Add(5);
                    list.Add(6);
                    list.Add(8);
                    list.Add(9);
                    break;
                }
            case GameBuild.Y2S4_11553121:
                {
                    MemoryHandler field18 = Memory;
                    ulong num47 = 146744130UL;
                    int num48 = -1;
                    int[] array18 = new int[] { 9634, 9666, 11202 };
                    num2 = field18.ResolvePointerChain(num47, num48, array18);
                    num = 128;
                    num3 = 24UL;
                    list.Add(4);
                    num5 = 6;
                    num11 = -1;
                    break;
                }
            case GameBuild.Y3S2_11965022:
                {
                    MemoryHandler field19 = Memory;
                    ulong num49 = 155366114UL;
                    int num50 = -1;
                    int[] array19 = new int[] { 9634, 9666, 11890 };
                    num2 = field19.ResolvePointerChain(num49, num50, array19);
                    num = 128;
                    num3 = 32UL;
                    num5 = 6;
                    num11 = -1;
                    list.Add(4);
                    break;
                }
            case GameBuild.Y3S4_12512571:
                {
                    MemoryHandler field20 = Memory;
                    ulong num51 = 126803890UL;
                    int num52 = -1;
                    int[] array20 = new int[] { 9554, 9666, 11762 };
                    num2 = field20.ResolvePointerChain(num51, num52, array20);
                    num = 80;
                    num3 = 32UL;
                    num4 = 128;
                    num5 = 6;
                    num11 = -1;
                    list.Add(4);
                    break;
                }
            case GameBuild.Y2S3_11493221:
                {
                    MemoryHandler field21 = Memory;
                    ulong num53 = 145276818UL;
                    int num54 = -1;
                    int[] array21 = new int[] { 9618, 9666, 11154 };
                    num2 = field21.ResolvePointerChain(num53, num54, array21);
                    num = 128;
                    num3 = 24UL;
                    list.Add(4);
                    num5 = 6;
                    num11 = -1;
                    break;
                }
            case GameBuild.Y1S3_9654076:
                {
                    MemoryHandler field22 = Memory;
                    ulong num55 = 177803298UL;
                    int num56 = -1;
                    int[] array22 = new int[] { 9634, 9666, 12162 };
                    num2 = field22.ResolvePointerChain(num55, num56, array22);
                    num = 128;
                    num3 = 24UL;
                    num5 = 8;
                    list.Add(5);
                    list.Add(6);
                    break;
                }
            case GameBuild.Y4S4_13777760:
                {
                    MemoryHandler field23 = Memory;
                    ulong num57 = 173885554UL;
                    int num58 = -1;
                    int[] array23 = new int[] { 9698, 9554, 10546, 9906, 0 };
                    num2 = field23.ResolvePointerChain(num57, num58, array23);
                    num = 80;
                    num3 = 32UL;
                    num4 = 128;
                    num5 = 5;
                    num11 = -1;
                    @enum = R6S.EventMode.Stadium;
                    break;
                }
            case GameBuild.Y2S4_11580709:
                {
                    MemoryHandler field24 = Memory;
                    ulong num59 = 146754258UL;
                    int num60 = -1;
                    int[] array24 = new int[] { 9634, 9666, 11202 };
                    num2 = field24.ResolvePointerChain(num59, num60, array24);
                    num = 128;
                    num3 = 24UL;
                    list.Add(4);
                    num5 = 6;
                    num11 = -1;
                    break;
                }
            case GameBuild.Y3S2_11938214:
                {
                    MemoryHandler field25 = Memory;
                    ulong num61 = 155352866UL;
                    int num62 = -1;
                    int[] array25 = new int[] { 9634, 9666, 11890 };
                    num2 = field25.ResolvePointerChain(num61, num62, array25);
                    num = 128;
                    num3 = 32UL;
                    num5 = 6;
                    num11 = -1;
                    list.Add(4);
                    break;
                }
        }
        TreeView treeView = new TreeView();
        ulong[] array26 = new ulong[num5];
        for (int i = 0; i < array26.Count<ulong>(); i++)
        {
            array26[i] = Memory.ReadPointer(num2 + (ulong)((long)i * (long)num3));
        }
        treeView.Nodes.AddRange(BuildGametypeNodes(true, array26, (ushort)num, (ushort)num4, (ushort)num3));
        switch (@enum)
        {
            case R6S.EventMode.Outbreak:
            case R6S.EventMode.Grand_Larceny:
                break;
            case R6S.EventMode.Mad_House:
                treeView.Nodes[0].Nodes.Add((TreeNode)treeView.Nodes[0].Nodes[2].Nodes[0].Nodes[2].Clone());
                treeView.Nodes[0].Nodes[2].Nodes[0].Nodes[2].Remove();
                treeView.Nodes[0].Nodes[5].Text = "Mad House";
                break;
            case R6S.EventMode.Rainbow_Is_Magic:
                treeView.Nodes[0].Nodes.Add((TreeNode)treeView.Nodes[0].Nodes[0].Nodes[21].Nodes[0].Clone());
                treeView.Nodes[0].Nodes[0].Nodes[21].Remove();
                treeView.Nodes[0].Nodes[5].Text = "Rainbow is Magic";
                break;
            case R6S.EventMode.Showdown:
                treeView.Nodes[0].Nodes.Add((TreeNode)treeView.Nodes[0].Nodes[1].Nodes[21].Nodes[0].Clone());
                treeView.Nodes[0].Nodes[1].Nodes[21].Remove();
                treeView.Nodes[0].Nodes[5].Text = "Showdown";
                break;
            case R6S.EventMode.Doktors_Curse_MoneyHeist:
                {
                    treeView.Nodes[0].Nodes.Add((TreeNode)treeView.Nodes[0].Nodes[5].Nodes[0].Nodes[0].Clone());
                    treeView.Nodes[0].Nodes[5].Remove();
                    treeView.Nodes[0].Nodes[5].Text = "Money Heist";
                    MemoryHandler field26 = Memory;
                    ulong num63 = 198401266UL;
                    int num64 = -1;
                    int[] array27 = new int[] { 9586, 9650, 10850, 9714 };
                    ulong num65 = field26.ResolvePointerChain(num63, num64, array27);
                    treeView.Nodes[0].Nodes.Add("Doktor's Curse");
                    treeView.Nodes[0].Nodes[treeView.Nodes[0].Nodes.Count - 1].Tag = string.Concat(Memory.ReadPointer(num65));
                    break;
                }
            case R6S.EventMode.Stadium:
                treeView.Nodes[0].Nodes.Add((TreeNode)treeView.Nodes[0].Nodes[5].Nodes[0].Nodes[0].Clone());
                treeView.Nodes[0].Nodes[5].Remove();
                treeView.Nodes[0].Nodes[5].Text = "Road To S.I. 2020";
                break;
            default:
                if (@enum == R6S.EventMode.None)
                {
                }
                break;
        }
        if (!flag)
        {
            if (list.Count<int>() > 0)
            {
                List<TreeNode> list2 = new List<TreeNode>();
                foreach (int num66 in list)
                {
                    list2.Add(treeView.Nodes[num66]);
                }
                foreach (TreeNode treeNode in list2)
                {
                    treeView.Nodes.Remove(treeNode);
                }
            }
            LabelMultiplayerNodes(treeView.Nodes[num6]);
            LabelTerroristHuntNodes(treeView.Nodes[num7]);
            LabelMatchmakingNodes(treeView.Nodes[num9]);
            LabelSituationNodes(treeView.Nodes[num8], flag2);
            if (num11 != -1)
            {
                LabelVideoReviewNodes(treeView.Nodes[num11]);
            }
            if (num10 != -1)
            {
                LabelGymNodes(treeView.Nodes[num10]);
            }
            if (num12 != -1)
            {
                LabelOutbreakNodes(treeView.Nodes[num12]);
            }
        }
        return treeView;
    }

    private TreeNode LabelOutbreakNodes(TreeNode node)
    {
        node.Text = "Outbreak";
        node.Nodes[0].Text = "Missions";
        node.Nodes[0].Nodes[0].Text = "Sierra Paradise";
        node.Nodes[0].Nodes[1].Text = "Sierra Paradise Part 2";
        node.Nodes[0].Nodes[2].Text = "Sierra Veterans Wing";
        node.Nodes[0].Nodes[3].Text = "Sierra Veterans Wing Part 2";
        node.Nodes[0].Nodes[4].Text = "The Nest";
        node.Nodes[0].Nodes[5].Text = "The Nest Part 2";
        node.Nodes[1].Nodes[0].Remove();
        node.Nodes[1].Nodes[0].Remove();
        node.Nodes[1].Nodes[0].Remove();
        node.Nodes[1].Nodes[0].Remove();
        node.Nodes[1].Text = "Development (Gym)";
        node.Nodes[1].Nodes[0].Text = "Art Review";
        node.Nodes[1].Nodes[0].Nodes[0].Text = "Sierra Paradise";
        node.Nodes[1].Nodes[0].Nodes[1].Text = "Sierra Paradise Part 2";
        node.Nodes[1].Nodes[0].Nodes[2].Text = "Sierra Veterans Wing";
        node.Nodes[1].Nodes[0].Nodes[3].Text = "Sierra Veterans Wing Part 2";
        node.Nodes[1].Nodes[0].Nodes[4].Text = "The Nest";
        node.Nodes[1].Nodes[0].Nodes[5].Text = "The Nest Part 2";
        node.Nodes[2].Remove();
        return node;
    }

    private TreeNode LabelMultiplayerNodes(TreeNode node)
    {
        node.Text = "Multiplayer";
        node.Nodes[0].Text = "Hostage";
        node.Nodes[1].Text = "Secure Area";
        node.Nodes[2].Text = "Bomb";
        if (_season >= Season.Y1S2_Dust_Line)
        {
            node.Nodes[3].Text = "Warmup";
        }
        if (_season >= Season.Y2S1_Velvet_Shell)
        {
            node.Nodes[4].Text = "Canister";
        }
        IEnumerator enumerator = node.Nodes.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                TreeNode treeNode = (TreeNode)enumerator.Current;
                IEnumerator enumerator2 = treeNode.Nodes.GetEnumerator();
                try
                {
                    while (enumerator2.MoveNext())
                    {
                        TreeNode treeNode2 = (TreeNode)enumerator2.Current;
                        treeNode2.Text = GetMapDisplayName(treeNode2.Text);
                        try
                        {
                            treeNode2.Nodes[0].Text = "Day";
                            if (treeNode2.Nodes.Count > 1)
                            {
                                treeNode2.Nodes[1].Text = "Night";
                            }
                            if (treeNode2.Text == "!!FLOYD")
                            {
                                treeNode2.Remove();
                            }
                            if (treeNode2.Text == "!!Staduim Playlist")
                            {
                                treeNode2.Remove();
                            }
                            if (treeNode2.Text == "!!Western")
                            {
                                treeNode2.Remove();
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator2 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                if (_season == Season.Y1S2_Dust_Line && treeNode.Nodes.Count > 13)
                {
                    treeNode.Nodes.RemoveAt(13);
                }
                if (_season == Season.Y2S3_Blood_Orchid && treeNode.Nodes.Count > 13)
                {
                    treeNode.Nodes.RemoveAt(17);
                }
            }
        }
        finally
        {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
        return node;
    }

    private TreeNode LabelTerroristHuntNodes(TreeNode node)
    {
        node.Text = "Terrorist Hunt";
        node.Nodes[0].Text = "Normal";
        node.Nodes[1].Text = "Hard";
        node.Nodes[2].Text = "Realistic";
        IEnumerator enumerator = node.Nodes.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                TreeNode treeNode = (TreeNode)enumerator.Current;
                IEnumerator enumerator2 = treeNode.Nodes.GetEnumerator();
                try
                {
                    while (enumerator2.MoveNext())
                    {
                        TreeNode treeNode2 = (TreeNode)enumerator2.Current;
                        treeNode2.Nodes[0].Text = "Hostage";
                        treeNode2.Nodes[1].Text = "Disarm Bomb";
                        treeNode2.Nodes[2].Text = "Elimination";
                        treeNode2.Text = GetMapDisplayName(treeNode2.Text);
                        IEnumerator enumerator3 = treeNode2.Nodes.GetEnumerator();
                        try
                        {
                            while (enumerator3.MoveNext())
                            {
                                IEnumerator enumerator4 = ((TreeNode)enumerator3.Current).Nodes.GetEnumerator();
                                try
                                {
                                    while (enumerator4.MoveNext())
                                    {
                                        TreeNode treeNode3 = (TreeNode)enumerator4.Current;
                                        treeNode3.Text = NormalizeGametypeName(treeNode3.Text);
                                    }
                                }
                                finally
                                {
                                    IDisposable disposable = enumerator4 as IDisposable;
                                    if (disposable != null)
                                    {
                                        disposable.Dispose();
                                    }
                                }
                            }
                        }
                        finally
                        {
                            IDisposable disposable = enumerator3 as IDisposable;
                            if (disposable != null)
                            {
                                disposable.Dispose();
                            }
                        }
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator2 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                if (_season == Season.Y1S2_Dust_Line && treeNode.Nodes.Count > 13)
                {
                    treeNode.Nodes.RemoveAt(13);
                }
                if (_season == Season.Y2S3_Blood_Orchid)
                {
                    if (treeNode.Nodes.Count > 13)
                    {
                        treeNode.Nodes.RemoveAt(17);
                    }
                }
                if (_season == Season.Y3S2_Para_Bellum)
                {
                    treeNode.Nodes[18].Text = "Villa";
                }
                if (_season >= Season.Y3S3_Grim_Sky)
                {
                    treeNode.Nodes[18].Text = "Hereford Base Rework";
                    treeNode.Nodes[17].Text = "Villa";
                }
            }
        }
        finally
        {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
        return node;
    }

    private string NormalizeGametypeName(string name)
    {
        string text;
        if (name == "TERROHUNT - DAY")
        {
            text = "Elimination - Day";
        }
        else if (name == "TERROHUNT - NIGHT")
        {
            text = "Elimination - Night";
        }
        else if (!(name == "ATTACK - DAY"))
        {
            if (!(name == "ATTACK - NIGHT"))
            {
                if (!(name == "DEFEND - DAY"))
                {
                    if (!(name == "DEFEND - NIGHT"))
                    {
                        text = name;
                    }
                    else
                    {
                        text = "Defend - Night";
                    }
                }
                else
                {
                    text = "Defend - Day";
                }
            }
            else
            {
                text = "Attack - Night";
            }
        }
        else
        {
            text = "Attack - Day";
        }
        return text;
    }

    private string GetMapDisplayName(string internalName)
    {
        uint num = Fnv1aHash.Compute(internalName);
        if (num > 2033926990U)
        {
            if (num > 2879110847U)
            {
                if (num <= 4021310395U)
                {
                    if (num > 3330168215U)
                    {
                        if (num <= 3936180372U)
                        {
                            if (num != 3806752707U)
                            {
                                if (num != 3936180372U)
                                {
                                    goto IL_08FF;
                                }
                                if (!(internalName == "PVP03_HerefordBase_Warmup"))
                                {
                                    goto IL_08FF;
                                }
                                goto IL_07CA;
                            }
                            else
                            {
                                if (!(internalName == "RussianCafe"))
                                {
                                    goto IL_08FF;
                                }
                                goto IL_0800;
                            }
                        }
                        else if (num != 3960599557U)
                        {
                            if (num != 4021310395U)
                            {
                                goto IL_08FF;
                            }
                            if (!(internalName == "HOUSE"))
                            {
                                goto IL_08FF;
                            }
                            goto IL_045F;
                        }
                        else
                        {
                            if (internalName == "HarvardFaculty")
                            {
                                goto IL_07A4;
                            }
                            goto IL_08FF;
                        }
                    }
                    else if (num <= 3211057450U)
                    {
                        if (num != 3005653882U)
                        {
                            if (num != 3211057450U)
                            {
                                goto IL_08FF;
                            }
                            if (!(internalName == "Yatch"))
                            {
                                goto IL_08FF;
                            }
                            goto IL_08DA;
                        }
                        else if (!(internalName == "THEMEPARK"))
                        {
                            goto IL_08FF;
                        }
                    }
                    else if (num == 3260696000U)
                    {
                        if (internalName == "Ibiza")
                        {
                            goto IL_02C2;
                        }
                        goto IL_08FF;
                    }
                    else
                    {
                        if (num != 3330168215U)
                        {
                            goto IL_08FF;
                        }
                        if (!(internalName == "!!AUSTRALIA"))
                        {
                            goto IL_08FF;
                        }
                        goto IL_0828;
                    }
                }
                else if (num > 4095869318U)
                {
                    if (num <= 4129576780U)
                    {
                        if (num == 4097244046U)
                        {
                            if (internalName == "PVP01_House_Warmup")
                            {
                                goto IL_045F;
                            }
                            goto IL_08FF;
                        }
                        else
                        {
                            if (num == 4129576780U && internalName == "ITALY")
                            {
                                goto IL_0597;
                            }
                            goto IL_08FF;
                        }
                    }
                    else if (num == 4164984447U)
                    {
                        if (internalName == "RUSSIAN CAFE")
                        {
                            goto IL_0800;
                        }
                        goto IL_08FF;
                    }
                    else
                    {
                        if (num != 4180728986U)
                        {
                            goto IL_08FF;
                        }
                        if (!(internalName == "ThemePark"))
                        {
                            goto IL_08FF;
                        }
                    }
                }
                else if (num <= 4057429500U)
                {
                    if (num != 4054117226U)
                    {
                        if (num != 4057429500U)
                        {
                            goto IL_08FF;
                        }
                        if (!(internalName == "TEMPLE"))
                        {
                            goto IL_08FF;
                        }
                        goto IL_084B;
                    }
                    else
                    {
                        if (internalName == "Gym-09-Kanal")
                        {
                            goto IL_065A;
                        }
                        goto IL_08FF;
                    }
                }
                else if (num != 4074181961U)
                {
                    if (num != 4095869318U)
                    {
                        goto IL_08FF;
                    }
                    if (internalName == "PVP06_Yatch_Warmup")
                    {
                        goto IL_08DA;
                    }
                    goto IL_08FF;
                }
                else
                {
                    if (!(internalName == "PVP13_Border_TDM_SecureArea"))
                    {
                        goto IL_08FF;
                    }
                    goto IL_08BD;
                }
                return "Theme Park";
            }
            if (num <= 2629220085U)
            {
                if (num <= 2427489702U)
                {
                    if (num != 2221452544U)
                    {
                        if (num != 2223456457U)
                        {
                            if (num != 2427489702U)
                            {
                                goto IL_08FF;
                            }
                            if (!(internalName == "Gym-04-ClubHouse"))
                            {
                                goto IL_08FF;
                            }
                            goto IL_0873;
                        }
                        else
                        {
                            if (internalName == "PVP02_Oregon_Warmup")
                            {
                                goto IL_070E;
                            }
                            goto IL_08FF;
                        }
                    }
                    else if (!(internalName == "IBIZA"))
                    {
                        goto IL_08FF;
                    }
                }
                else if (num > 2575674704U)
                {
                    if (num == 2585943601U)
                    {
                        if (internalName == "CLUB HOUSE")
                        {
                            goto IL_0873;
                        }
                        goto IL_08FF;
                    }
                    else
                    {
                        if (num == 2629220085U && internalName == "PLANE")
                        {
                            goto IL_0781;
                        }
                        goto IL_08FF;
                    }
                }
                else if (num == 2428886533U)
                {
                    if (!(internalName == "OREGON"))
                    {
                        goto IL_08FF;
                    }
                    goto IL_070E;
                }
                else
                {
                    if (num != 2575674704U)
                    {
                        goto IL_08FF;
                    }
                    if (!(internalName == "PVP05_Plane_Warmup"))
                    {
                        goto IL_08FF;
                    }
                    goto IL_0781;
                }
            }
            else if (num > 2663744245U)
            {
                if (num <= 2748309515U)
                {
                    if (num == 2708071367U)
                    {
                        if (internalName == "PVP09_Kanal_Warmup")
                        {
                            goto IL_065A;
                        }
                        goto IL_08FF;
                    }
                    else
                    {
                        if (num == 2748309515U && internalName == "CONSULATE")
                        {
                            goto IL_08F7;
                        }
                        goto IL_08FF;
                    }
                }
                else if (num != 2767282393U)
                {
                    if (num != 2879110847U)
                    {
                        goto IL_08FF;
                    }
                    if (!(internalName == "PVP10_Chalet_Warmup"))
                    {
                        goto IL_08FF;
                    }
                    goto IL_04C8;
                }
                else
                {
                    if (!(internalName == "MOROCCO"))
                    {
                        goto IL_08FF;
                    }
                    goto IL_068A;
                }
            }
            else if (num > 2652915101U)
            {
                if (num != 2652974833U)
                {
                    if (num != 2663744245U)
                    {
                        goto IL_08FF;
                    }
                    if (internalName == "Plane")
                    {
                        goto IL_0781;
                    }
                    goto IL_08FF;
                }
                else
                {
                    if (internalName == "PVP08_Bank_Warmup")
                    {
                        goto IL_0554;
                    }
                    goto IL_08FF;
                }
            }
            else if (num == 2637447336U)
            {
                if (!(internalName == "Gym-08-Bank"))
                {
                    goto IL_08FF;
                }
                goto IL_0554;
            }
            else
            {
                if (num == 2652915101U && internalName == "Gym-01-House")
                {
                    goto IL_045F;
                }
                goto IL_08FF;
            }
        IL_02C2:
            return "Coastline";
        IL_045F:
            return "House";
        }
        if (num <= 873852940U)
        {
            if (num > 563498928U)
            {
                if (num <= 609685522U)
                {
                    if (num > 576722875U)
                    {
                        if (num != 585250630U)
                        {
                            if (num == 609685522U && internalName == "CHALET")
                            {
                                goto IL_04C8;
                            }
                            goto IL_08FF;
                        }
                        else
                        {
                            if (internalName == "Gym-10-Chalet")
                            {
                                goto IL_04C8;
                            }
                            goto IL_08FF;
                        }
                    }
                    else if (num == 565061717U)
                    {
                        if (internalName == "PVP11_HarvardFaculty_Warmup")
                        {
                            goto IL_07A4;
                        }
                        goto IL_08FF;
                    }
                    else
                    {
                        if (num != 576722875U)
                        {
                            goto IL_08FF;
                        }
                        if (internalName == " HarvardFaculty")
                        {
                            goto IL_07A4;
                        }
                        goto IL_08FF;
                    }
                }
                else if (num <= 742412079U)
                {
                    if (num != 657082071U)
                    {
                        if (num != 742412079U)
                        {
                            goto IL_08FF;
                        }
                        if (!(internalName == "PVP04_ClubHouse_Warmup"))
                        {
                            goto IL_08FF;
                        }
                        goto IL_0873;
                    }
                    else
                    {
                        if (internalName == "BANK")
                        {
                            goto IL_0554;
                        }
                        goto IL_08FF;
                    }
                }
                else if (num == 794837631U)
                {
                    if (!(internalName == "Gym-07-Consulate"))
                    {
                        goto IL_08FF;
                    }
                    goto IL_08F7;
                }
                else
                {
                    if (num == 873852940U && internalName == "Italy")
                    {
                        goto IL_0597;
                    }
                    goto IL_08FF;
                }
            }
            else if (num > 138646584U)
            {
                if (num > 417641430U)
                {
                    if (num == 475010523U)
                    {
                        if (internalName == "Gym-03-HerefordBase")
                        {
                            goto IL_07CA;
                        }
                        goto IL_08FF;
                    }
                    else
                    {
                        if (num != 563498928U)
                        {
                            goto IL_08FF;
                        }
                        if (!(internalName == "YACHT"))
                        {
                            goto IL_08FF;
                        }
                        goto IL_08DA;
                    }
                }
                else if (num == 394737817U)
                {
                    if (internalName == "Gym-12-RussianCafe")
                    {
                        goto IL_0800;
                    }
                    goto IL_08FF;
                }
                else
                {
                    if (num != 417641430U)
                    {
                        goto IL_08FF;
                    }
                    if (!(internalName == "FAVELA"))
                    {
                        goto IL_08FF;
                    }
                }
            }
            else if (num != 32188569U)
            {
                if (num != 113863616U)
                {
                    if (num == 138646584U && internalName == "KANAL")
                    {
                        goto IL_065A;
                    }
                    goto IL_08FF;
                }
                else
                {
                    if (!(internalName == "Gym-11-HarvardFaculty"))
                    {
                        goto IL_08FF;
                    }
                    goto IL_07A4;
                }
            }
            else
            {
                if (internalName == "Morocco")
                {
                    goto IL_068A;
                }
                goto IL_08FF;
            }
        }
        else if (num > 1709803113U)
        {
            if (num > 2009170019U)
            {
                if (num > 2021714798U)
                {
                    if (num == 2026289086U)
                    {
                        if (internalName == "HEREFORD")
                        {
                            goto IL_07CA;
                        }
                        goto IL_08FF;
                    }
                    else
                    {
                        if (num == 2033926990U && internalName == "HerefordRework")
                        {
                            return "Hereford Base Rework";
                        }
                        goto IL_08FF;
                    }
                }
                else if (num == 2018303168U)
                {
                    if (internalName == "Gym-02-Oregon")
                    {
                        goto IL_070E;
                    }
                    goto IL_08FF;
                }
                else
                {
                    if (num != 2021714798U)
                    {
                        goto IL_08FF;
                    }
                    if (!(internalName == "Gym-14-Favela"))
                    {
                        goto IL_08FF;
                    }
                }
            }
            else if (num <= 1995654199U)
            {
                if (num != 1894910623U)
                {
                    if (num == 1995654199U && internalName == "BORDER")
                    {
                        goto IL_08BD;
                    }
                    goto IL_08FF;
                }
                else
                {
                    if (internalName == "Gym-05-Plane")
                    {
                        goto IL_0781;
                    }
                    goto IL_08FF;
                }
            }
            else if (num == 1996193591U)
            {
                if (internalName == "HARVARD FACULTY")
                {
                    goto IL_07A4;
                }
                goto IL_08FF;
            }
            else
            {
                if (num == 2009170019U && internalName == "HerefordBase")
                {
                    goto IL_07CA;
                }
                goto IL_08FF;
            }
        }
        else if (num > 1253292856U)
        {
            if (num > 1393349683U)
            {
                if (num == 1396809918U)
                {
                    if (internalName == "PVP12_RussianCafe_Warmup")
                    {
                        goto IL_0800;
                    }
                    goto IL_08FF;
                }
                else
                {
                    if (num != 1709803113U)
                    {
                        goto IL_08FF;
                    }
                    if (internalName == "Australia")
                    {
                        goto IL_0828;
                    }
                    goto IL_08FF;
                }
            }
            else if (num == 1327680604U)
            {
                if (internalName == "Temple")
                {
                    goto IL_084B;
                }
                goto IL_08FF;
            }
            else
            {
                if (num != 1393349683U)
                {
                    goto IL_08FF;
                }
                if (internalName == "ClubHouse")
                {
                    goto IL_0873;
                }
                goto IL_08FF;
            }
        }
        else if (num <= 975485280U)
        {
            if (num == 936700674U)
            {
                if (internalName == "TOWER")
                {
                    return "Tower";
                }
                goto IL_08FF;
            }
            else
            {
                if (num != 975485280U)
                {
                    goto IL_08FF;
                }
                if (internalName == "Gym-13-Border")
                {
                    goto IL_08BD;
                }
                goto IL_08FF;
            }
        }
        else if (num == 986499817U)
        {
            if (internalName == "Gym-06-Yatch")
            {
                goto IL_08DA;
            }
            goto IL_08FF;
        }
        else
        {
            if (num == 1253292856U && internalName == "PVP07_Consulate_Warmup")
            {
                goto IL_08F7;
            }
            goto IL_08FF;
        }
        return "Favela";
    IL_04C8:
        return "Chalet";
    IL_0554:
        return "Bank";
    IL_0597:
        return "Villa";
    IL_065A:
        return "Kanal";
    IL_068A:
        return "Fortress";
    IL_070E:
        return "Oregon";
    IL_0781:
        return "Presidential Plane";
    IL_07A4:
        return "Bartlett University";
    IL_07CA:
        return "Hereford Base";
    IL_0800:
        return "Kafe Dostoyevsky";
    IL_0828:
        return "Outback";
    IL_084B:
        return "Skyscraper";
    IL_0873:
        return "Club House";
    IL_08BD:
        return "Border";
    IL_08DA:
        return "Yacht";
    IL_08F7:
        return "Consulate";
    IL_08FF:
        return internalName;
    }

    private TreeNode LabelMatchmakingNodes(TreeNode node)
    {
        node.Text = "Matchmaking";
        node.Nodes[0].Text = "Casual";
        node.Nodes[0].Nodes[0].Text = "Hostage";
        node.Nodes[0].Nodes[1].Text = "Bomb";
        node.Nodes[0].Nodes[2].Text = "Secure Area";
        node.Nodes[1].Text = "Ranked";
        node.Nodes[1].Nodes[0].Text = "Hostage";
        node.Nodes[1].Nodes[1].Text = "Bomb";
        node.Nodes[1].Nodes[2].Text = "Secure Area";
        if (node.Nodes.Count == 3)
        {
            node.Nodes[2].Text = "Unranked";
            node.Nodes[2].Nodes[0].Text = "Bomb";
        }
        return node;
    }

    private TreeNode LabelSituationNodes(TreeNode node, bool advancedOrder)
    {
        node.Text = "Situations";
        node.Nodes[0].Text = "01 CQB Basics";
        node.Nodes[1].Text = "02 Suburban Extraction";
        if (advancedOrder)
        {
            node.Nodes[2].Text = "03 Tubular Assault";
            node.Nodes[3].Text = "04 Asset Protection";
            node.Nodes[4].Text = "05 Improvise Defense";
            node.Nodes[5].Text = "06 No Intel";
            node.Nodes[6].Text = "07 Cold Zero";
            node.Nodes[7].Text = "08 High Value Target";
            node.Nodes[8].Text = "09 Neutralize Cell";
        }
        else
        {
            node.Nodes[2].Text = "03 High Value Target";
            node.Nodes[3].Text = "04 Tubular Assault";
            node.Nodes[4].Text = "05 Cold Zero";
            node.Nodes[5].Text = "06 Asset Protection";
            node.Nodes[6].Text = "07 Neutralize Cell";
            node.Nodes[7].Text = "08 No Intel";
            node.Nodes[8].Text = "09 Improvise Defense";
        }
        node.Nodes[9].Text = "10 Heavily Fortified";
        node.Nodes[10].Text = "Article 5";
        IEnumerator enumerator = node.Nodes.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                TreeNode treeNode = (TreeNode)enumerator.Current;
                if (treeNode.Nodes.Count > 0)
                {
                    treeNode.Nodes[0].Text = "Normal";
                    treeNode.Nodes[1].Text = "Hard";
                    treeNode.Nodes[2].Text = "Realistic";
                }
            }
        }
        finally
        {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
        return node;
    }

    private TreeNode LabelGymNodes(TreeNode node)
    {
        node.Text = "Development (Gym)";
        node.Nodes[0].Text = "Day";
        node.Nodes[1].Text = "Night";
        IEnumerator enumerator = node.Nodes.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                TreeNode treeNode = (TreeNode)enumerator.Current;
                if (treeNode.Nodes.Count == 12)
                {
                    IEnumerator enumerator2 = treeNode.Nodes.GetEnumerator();
                    try
                    {
                        while (enumerator2.MoveNext())
                        {
                            TreeNode treeNode2 = (TreeNode)enumerator2.Current;
                            treeNode2.Text = GetMapDisplayName(treeNode2.Text);
                        }
                        continue;
                    }
                    finally
                    {
                        IDisposable disposable = enumerator2 as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }
                }
                treeNode.Text = GetMapDisplayName(treeNode.Text);
            }
        }
        finally
        {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
        if (_season == Season.Y1S2_Dust_Line)
        {
            node.Nodes.RemoveAt(15);
        }
        if (_season >= Season.Y2S3_Blood_Orchid)
        {
            node.Nodes[0].Remove();
            node.Nodes[0].Remove();
        }
        return node;
    }

    private TreeNode LabelVideoReviewNodes(TreeNode node)
    {
        node.Text = "Development (Video Review)";
        IEnumerator enumerator = node.Nodes.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                TreeNode treeNode = (TreeNode)enumerator.Current;
                treeNode.Nodes[0].Text = "House (Benchmark)";
                IEnumerator enumerator2 = treeNode.Nodes.GetEnumerator();
                try
                {
                    while (enumerator2.MoveNext())
                    {
                        TreeNode treeNode2 = (TreeNode)enumerator2.Current;
                        treeNode2.Text = GetMapDisplayName(treeNode2.Text);
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator2 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }
        finally
        {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
        return node;
    }

    private TreeNode[] BuildGametypeNodes(bool direct, ulong[] addresses, ushort nameOffset, ushort childrenOffset, ushort extraOffset)
    {
        TreeNode[] array = new TreeNode[addresses.Count<ulong>()];
        for (int i = 0; i < addresses.Count<ulong>(); i++)
        {
            ulong num = addresses[i];
            if (!direct)
            {
                num = Memory.ResolveOffsetChain(addresses[i], new int[2]);
            }
            array[i] = new TreeNode();
            string text = Memory.ReadAsciiString(Memory.ResolveOffsetChain(num + (ulong)nameOffset, new int[1]), 100);
            if (text != "")
            {
                array[i].Text = text;
            }
            else
            {
                array[i].Text = "<No Name>";
            }
            array[i].Tag = string.Concat(Memory.ReadPointer(addresses[i]));
            int num2 = (int)Memory.ReadInt16(num + (ulong)childrenOffset + 8UL);
            ulong[] array2 = new ulong[num2];
            for (int j = 0; j < num2; j++)
            {
                array2[j] = Memory.ResolveOffsetChain(num + (ulong)childrenOffset, new int[1]) + (ulong)((long)j * 8L);
            }
            array[i].Nodes.AddRange(BuildGametypeNodes(false, array2, nameOffset, childrenOffset, extraOffset));
        }
        return array;
    }

    public MemoryHandler Memory = new MemoryHandler("RainbowSix");

    public static string[] BuildSignatures = new string[]
    {
        "C1590850_D255757_S26600_8194013", "C1666147_D261342_S26785_8519860", "C1776910_D272536_S27023_9132097", "S3.0.1_C1877582_D283853_S27309_9654076", "S3.2_C1920545_D290233_S27413_9860556", "S4.0_C1999293_D302594_S27673_10211195", "Y2S1.0.1_C2127988_D331008_S28379_10751226", "Y2S2.1_C2298996_D403394_S29686_11216230", "Y2S3.1.1_C2481432_D446659_S30467_11432634", "Y2S3.2.1_C2544736_D462226_S30681_11493221",
        "Y2S4.0_C2607037_D480724_S31242_11553121", "Y2S4.1_C2630572_D485722_S31242_11580709", "Y3S1.0_C2740392_D502269_S31836_11706399", "Y3S1.0_C2763770_D504595_S31867_11726982", "Y3S2.0.1_C2969096_D529130_S32363_11938214", "Y3S2.0.1.1_C2987226_D529130_S32363_11965022", "Y3S3.0.1_C3160800_D557243_S32705_12213419", "Y3S3.2_C3262805_D573021_S32811_12362767", "Y3S4.0_C3369791_D587380_S33229_12512571", "Y4S1.0_C3582381_D626567_S33696_12815133",
        "Y4S1.0.2_C3629116_D632736_S33696_12863847", "Y4S2.0_C3919151_D683368_S34606_13147883", "Y4S3.3.1_C4483073_D795312_S36224_13632147", "Y4S4.0.0_C4655978_D845809_S36789_13777760", "Y4S4.2.0_C4783211_D876692_S37092_13924517"
    };

    public static string[] SeasonNames = new string[]
    {
        "Y1S0 Launch", "Y1S1 Operation Black Ice", "Y1S2 Operation Dust Line", "Y1S3 Operation Skull Rain", "Y1S4 Operation Red Crow", "Y2S1 Operation Velvet Shell", "Y2S2 Operation Health", "Y2S3 Operation Blood Orchid", "Y2S4 Operation White Noise", "Y3S1 Operation Chimera",
        "Y3S2 Operation Para Bellum", "Y3S3 Operation Grim Sky", "Y3S4 Operation Wind_Bastion", "Y4S1 Operation Burnt Horizon", "Y4S2 Operation Phantom Sight", "Y4S3 Operation Ember Rise", "Y4S4 Operation Shifting Tides", "Y5S1 Operation Void Edge", "Y5S2 Operation Steel Wave", "Y5S3 Operation Shadow Legacy",
        "Y5S4_TBD"
    };

    private long[] _madHouseGametypes;

    private long _harvardValueA;

    private long _harvardValueB;

    private long _oldHerefordValueB;

    private long _oldHerefordValueA;
    private Season _season;

    private string _processName;

    private GameBuild _build;

    public enum EventMode : uint
    {

        Outbreak,

        Mad_House,

        Rainbow_Is_Magic,

        Showdown,

        Doktors_Curse_MoneyHeist,

        Stadium,

        Grand_Larceny,

        None = 255U
    }
}
