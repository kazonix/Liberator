using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Liberator;

public class MemoryHandler
{

    [DllImport("kernel32.dll")]
    private static extern bool ReadProcessMemory(IntPtr handle, ulong address, byte[] buffer, UIntPtr size, ref long bytesRead);

    [DllImport("kernel32.dll")]
    private static extern bool WriteProcessMemory(IntPtr handle, ulong address, byte[] buffer, UIntPtr size, ref long bytesWritten);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(uint access, bool inheritHandle, uint processId);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);

    [DllImport("ntdll.dll", SetLastError = true)]
    private static extern int NtQueryInformationThread(IntPtr threadHandle, MemoryHandler.ThreadInfoClass infoClass, IntPtr info, int infoLength, IntPtr returnLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenThread(MemoryHandler.ThreadAccess access, bool inheritHandle, uint threadId);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandleA(string moduleName);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetProcAddress(IntPtr module, string procName);

    private static void DisableSelfTerminate()
    {
        long num = 0L;
        byte[] array = new byte[] { 0xC3 };
        ulong a = (ulong)(long)MemoryHandler.GetProcAddress(MemoryHandler.GetModuleHandleA("kernel32.dll"), "TerminateProcess");
        ulong a2 = (ulong)(long)MemoryHandler.GetProcAddress(MemoryHandler.GetModuleHandleA("ntdll.dll"), "NtTerminateProcess");
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, a, array, (UIntPtr)1UL, ref num);
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, a2, array, (UIntPtr)1UL, ref num);
    }

    public MemoryHandler(string processName)
    {
        MemoryHandler._processName = processName;
        Process process = Process.GetProcessesByName(processName).FirstOrDefault<Process>();
        try
        {
            MemoryHandler.BaseAddress = (ulong)(long)process.MainModule.BaseAddress;
            if (MemoryHandler._processHandle != IntPtr.Zero)
            {
                MemoryHandler.CloseHandle(MemoryHandler._processHandle);
            }

            MemoryHandler._processHandle = MemoryHandler.OpenProcess(2035711U, false, (uint)process.Id);
            MemoryHandler.DisableSelfTerminate();
            Modules = process.Modules;
            Threads = process.Threads;
            ProcessId = process.Id;
            TotalProcessorTimeMs = process.TotalProcessorTime.TotalMilliseconds;
        }
        catch
        {
        }
        _offsetTable[0] = _tableSeed0;
        _offsetTable[1] = _tableSeed1;
        _offsetTable[3] = _tableSeed2;
        _offsetTable[3] = _tableSeed8;
        _offsetTable[4] = _tableSeed4;
        _offsetTable[5] = _tableSeed5;
        _offsetTable[12] = _tableSeed0;
        _offsetTable[6] = 0UL;
        _offsetTable[7] = _tableSeed7;
        _offsetTable[26] = 57806UL;
        _offsetTable[8] = _tableSeed8;
        _offsetTable[10] = _tableSeed8;
        _offsetTable[9] = _tableSeed9;
        _offsetTable[11] = _tableSeed0;
        _offsetTable[10] = _tableSeed2;
        _offsetTable[24] = 6UL;
        _offsetTable[1] = 40UL;
        _offsetTable[11] = _offsetTable[11] - _offsetTable[1] + 1UL;
        _offsetTable[10] = _offsetTable[10] * 2UL;
        _offsetTable[7] = _offsetTable[7] * 2UL;
        _offsetTable[11] = 255UL;
        _offsetTable[4] = 9254UL;
        _offsetTable[23] = 744756UL;
        _offsetTable[22] = 35000UL;
        checked
        {
            _offsetTable[(int)((IntPtr)(unchecked(17UL + _offsetTable[24])))] = _offsetTable[(int)((IntPtr)(unchecked(17UL + _offsetTable[24])))] / _offsetTable[24];
        }
    }

    public static MemoryHandler.ThreadBasicInfo GetThreadBasicInformation(IntPtr threadHandle)
    {
        MemoryHandler.ThreadBasicInfo threadInfo = new MemoryHandler.ThreadBasicInfo();
        int num = Marshal.SizeOf(typeof(MemoryHandler.ThreadBasicInfo));
        IntPtr intPtr = Marshal.AllocHGlobal(num);
        MemoryHandler.NtQueryInformationThread(threadHandle, MemoryHandler.ThreadInfoClass.ThreadBasicInformation, intPtr, num, IntPtr.Zero);
        Marshal.PtrToStructure<MemoryHandler.ThreadBasicInfo>(intPtr, threadInfo);
        Marshal.FreeHGlobal(intPtr);
        intPtr = IntPtr.Zero;
        return threadInfo;
    }

    private ulong DecodeOffset(int value)
    {
        ulong num = DecodeOffsetStage1((ulong)((long)value));
        num = DecodeOffsetStage2(num);
        num = DecodeOffsetStage3(num);
        return DecodeOffsetStage4(num);
    }

    public bool IsAttached()
    {
        return ReadInt32(MemoryHandler.BaseAddress) > 0;
    }

    public ulong ReadActiveThreadPointer()
    {
        IEnumerator enumerator = Process.GetProcessesByName(MemoryHandler._processName).FirstOrDefault<Process>().Threads.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                ProcessThread processThread = (ProcessThread)enumerator.Current;
                if (processThread.ThreadState == ThreadState.Running)
                {
                    MemoryHandler.ThreadBasicInfo threadInfo = MemoryHandler.GetThreadBasicInformation(MemoryHandler.OpenThread(MemoryHandler.ThreadAccess.All, false, (uint)processThread.Id));
                    ulong num = ReadPointer(threadInfo.TebBaseAddress + 88UL);
                    return ReadPointer(num);
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
        return 0UL;
    }

    public ulong ResolvePointerChain(ulong baseOffset, int moduleIndex, params int[] offsets)
    {
        baseOffset = DecodeOffsetStage1(baseOffset);
        baseOffset = DecodeOffsetStage2(baseOffset);
        baseOffset = DecodeOffsetStage3(baseOffset);
        baseOffset = DecodeOffsetStage4(baseOffset);
        ulong num;
        if (moduleIndex > -1)
        {
            num = (ulong)((long)Modules[moduleIndex].BaseAddress + (long)baseOffset);
        }
        else
        {
            num = MemoryHandler.BaseAddress + baseOffset;
        }
        byte[] array = new byte[8];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, num, array, (UIntPtr)8UL, ref MemoryHandler._bytesTransferred);
        for (int i = 0; i < offsets.Length - 1; i++)
        {
            num = BitConverter.ToUInt64(array, 0) + DecodeOffset(offsets[i]);
            MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, num, array, (UIntPtr)8UL, ref MemoryHandler._bytesTransferred);
        }
        return BitConverter.ToUInt64(array, 0) + DecodeOffset(offsets[offsets.Length - 1]);
    }

    public ulong ResolveOffsetChain(ulong baseAddress, params int[] offsets)
    {
        byte[] array = new byte[8];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, baseAddress, array, (UIntPtr)8UL, ref MemoryHandler._bytesTransferred);
        for (int i = 0; i < offsets.Length - 1; i++)
        {
            baseAddress = BitConverter.ToUInt64(array, 0) + (ulong)((long)offsets[i]);
            MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, baseAddress, array, (UIntPtr)8UL, ref MemoryHandler._bytesTransferred);
        }
        return BitConverter.ToUInt64(array, 0) + (ulong)((long)offsets[offsets.Length - 1]);
    }

    private ulong DecodeBitOffsetStage2(ulong value)
    {
        byte[] array = new byte[] { 25, 100 };
        BitConverter.ToInt16(array, 0);
        return value - (ulong)((long)BitConverter.ToInt16(array, 0) * 2L) + 1UL;
    }

    public static string DecodeHexString(string hex)
    {
        try
        {
            string text = string.Empty;
            for (int i = 0; i < hex.Length; i += 2)
            {
                hex.Substring(i, 2);
                text += Convert.ToChar(Convert.ToUInt32(hex.Substring(i, 2), 16)).ToString();
            }
            return text;
        }
        catch
        {
        }
        return string.Empty;
    }

    private ulong DecodeOffsetStage1(ulong value)
    {
        return value - _offsetTable[11];
    }

    public string StripDashes(string text)
    {
        return text.Replace("-", "");
    }

    private ulong DecodeStringOffsetStage1(ulong value)
    {
        int num = 20;
        return value + _offsetTable[23] + (ulong)((long)num * 20L);
    }

    public string ReadAsciiString(ulong address, int length)
    {
        bool flag = false;
        byte[] array = new byte[length];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)((ulong)((long)array.Length)), ref MemoryHandler._bytesTransferred);
        int num = 0;
        string text = BitConverter.ToString(array);
        char[] array2 = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] != '-')
            {
                array2[num++] = text[i];
            }
        }
        string text2 = new string(array2, 0, num);
        string text3 = string.Empty;
        for (int j = 0; j < text2.Length; j += 2)
        {
            string text4 = string.Empty;
            text4 = text2.Substring(j, 2);
            if (flag && text4 == "00")
            {
                return text3;
            }
            if (text4 == "00")
            {
                flag = true;
            }
            else
            {
                flag = false;
                text3 += Convert.ToChar(Convert.ToUInt32(text4, 16)).ToString();
            }
        }
        return text3;
    }

    public string ReadScrambledString(ulong address, int length, bool relative)
    {
        address = DecodeStringOffset(address);
        length = (int)DecodeStringOffset((ulong)((long)length));
        if (relative)
        {
            address += MemoryHandler.BaseAddress;
        }
        byte[] array = new byte[length];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)((ulong)((long)array.Length)), ref MemoryHandler._bytesTransferred);
        string text = BitConverter.ToString(array);
        int num = 0;
        char[] array2 = new char[text.Length];
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] != '-')
            {
                array2[num++] = text[i];
            }
        }
        return MemoryHandler.DecodeHexString(new string(array2, 0, num));
    }

    private ulong DecodeStringOffsetStage3(ulong value)
    {
        value /= 5UL;
        return value;
    }

    private ulong DecodeStringOffsetStage2(ulong value)
    {
        return value - 436734UL;
    }

    public float ReadSingle(ulong address)
    {
        byte[] array = new byte[4];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)4UL, ref MemoryHandler._bytesTransferred);
        return BitConverter.ToSingle(array, 0);
    }

    public int ReadByteAsInt32(ulong address)
    {
        byte[] array = new byte[1];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)1UL, ref MemoryHandler._bytesTransferred);
        return (int)array[0];
    }

    private ulong DecodeOffsetStage2(ulong value)
    {
        return value - _offsetTable[4];
    }

    public short ReadInt16(ulong address)
    {
        byte[] array = new byte[2];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)2UL, ref MemoryHandler._bytesTransferred);
        return (short)array[0];
    }

    private ulong DecodeBitOffsetStage1(ulong value)
    {
        ulong num = 2UL;
        ulong num2 = _offsetTable[26] * (num * num);
        return value - num2;
    }

    public uint ReadByteAsUInt32(ulong address)
    {
        byte[] array = new byte[1];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)1UL, ref MemoryHandler._bytesTransferred);
        return (uint)array[0];
    }

    private ulong DecodeBitOffsetStage3(ulong value)
    {
        byte[] array = new byte[] { 211, 112 };
        for (int i = 0; i < array.Count<byte>(); i++)
        {
            byte[] array2 = array;
            int num = i;
            array2[num] += 1;
            byte[] array3 = array;
            int num2 = i;
            array3[num2] += 1;
            byte[] array4 = array;
            int num3 = i;
            array4[num3] += 1;
            byte[] array5 = array;
            int num4 = i;
            array5[num4] += 1;
        }
        ulong num5 = (ulong)((long)BitConverter.ToInt16(array, 0));
        return value - num5;
    }

    public int ReadInt32(ulong address)
    {
        byte[] array = new byte[4];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)4UL, ref MemoryHandler._bytesTransferred);
        return BitConverter.ToInt32(array, 0);
    }

    public static uint ReadUInt32(ulong address)
    {
        byte[] array = new byte[4];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)4UL, ref MemoryHandler._bytesTransferred);
        return BitConverter.ToUInt32(array, 0);
    }

    public ulong ReadPointer(ulong address)
    {
        byte[] array = new byte[8];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)6UL, ref MemoryHandler._bytesTransferred);
        return BitConverter.ToUInt64(array, 0);
    }

    public bool ReadScrambledBool(ulong address, bool relative)
    {
        address = DecodeBitOffset(address) / 2UL;
        if (relative)
        {
            address = address - 1UL + MemoryHandler.BaseAddress;
        }
        byte[] array = new byte[1];
        MemoryHandler.ReadProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)1UL, ref MemoryHandler._bytesTransferred);
        return BitConverter.ToBoolean(array, 0);
    }

    public void WriteBool(ulong address, bool value)
    {
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, BitConverter.GetBytes(value), (UIntPtr)((ulong)((long)BitConverter.GetBytes(value).Length)), ref MemoryHandler._bytesTransferred);
    }

    private ulong DecodeOffsetStage4(ulong value)
    {
        return value / (_offsetTable[8] - 62UL);
    }

    public void WriteInt32(ulong address, int value)
    {
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, BitConverter.GetBytes(value), (UIntPtr)((ulong)((long)BitConverter.GetBytes(value).Length)), ref MemoryHandler._bytesTransferred);
    }

    public void WriteUInt32(ulong address, uint value)
    {
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, BitConverter.GetBytes(value), (UIntPtr)((ulong)((long)BitConverter.GetBytes(value).Length)), ref MemoryHandler._bytesTransferred);
    }

    private ulong DecodeBitOffset(ulong value)
    {
        value = DecodeBitOffsetStage1(value);
        value = DecodeBitOffsetStage2(value);
        value = DecodeBitOffsetStage3(value);
        return value - 420UL;
    }

    public void WriteInt64(ulong address, long value)
    {
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, BitConverter.GetBytes(value), (UIntPtr)((ulong)((long)BitConverter.GetBytes(value).Length)), ref MemoryHandler._bytesTransferred);
    }

    public void WriteAngleDegrees(ulong address, double degrees)
    {
        float num = Convert.ToSingle(degrees * 3.141592653589793 / 180.0);
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, BitConverter.GetBytes(num), (UIntPtr)((ulong)((long)BitConverter.GetBytes(num).Length)), ref MemoryHandler._bytesTransferred);
    }

    public void WriteSingle(ulong address, float value)
    {
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, BitConverter.GetBytes(value), (UIntPtr)((ulong)((long)BitConverter.GetBytes(value).Length)), ref MemoryHandler._bytesTransferred);
    }

    private ulong DecodeStringOffset(ulong value)
    {
        value = DecodeStringOffsetStage1(value) - 400UL;
        value = DecodeStringOffsetStage2(value);
        value = DecodeStringOffsetStage3(value);
        return value;
    }

    public void WriteDouble(ulong address, double value)
    {
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, BitConverter.GetBytes(value), (UIntPtr)((ulong)((long)BitConverter.GetBytes(value).Length)), ref MemoryHandler._bytesTransferred);
    }

    public void WriteScrambledInts(ulong address, bool relative, int[] values)
    {
        ulong num = _offsetTable[26] * 4UL;
        int num2 = 3086;
        address -= num;
        byte[] array = new byte[] { 25, 100 };
        ulong num3 = 6UL;
        ulong num4 = 2UL * _offsetTable[24];
        address = address - (ulong)((long)BitConverter.ToInt16(array, 0) * 2L) + 1UL;
        byte[] array2 = new byte[] { 213, 114 };
        for (int i = 0; i < array2.Count<byte>(); i++)
        {
            byte[] array3 = array2;
            int num5 = i;
            array3[num5] += 1;
            byte[] array4 = array2;
            int num6 = i;
            array4[num6] += 1;
        }
        ulong num7 = (ulong)((long)BitConverter.ToInt16(array2, 0));
        address -= num7;
        address -= 420UL;
        address -= 1UL;
        address -= 1UL;
        address -= 1UL;
        address -= 1UL;
        address /= 2UL;
        address += 1UL;
        if (relative)
        {
            address += MemoryHandler.BaseAddress;
        }
        byte[] array5 = new byte[values.Count<int>()];
        for (int j = 0; j < values.Count<int>(); j++)
        {
            byte b = Convert.ToByte(values[j] + (int)num4 + 1);
            values[j] = (int)b;
            array5[j] = Convert.ToByte(values[j]);
            num3 += 1UL;
            num2++;
        }
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, array5, (UIntPtr)((ulong)((long)values.Length)), ref MemoryHandler._bytesTransferred);
    }

    public void WriteNops(ulong address, int count, bool useNop, bool relative)
    {
        address = DecodeBitOffset(address);
        address /= 2UL;
        if (relative)
        {
            address = address - 1UL + MemoryHandler.BaseAddress;
        }
        byte b = 0;
        if (useNop)
        {
            b = 144;
        }
        byte[] array = new byte[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = b;
        }
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, array, (UIntPtr)((ulong)((long)array.Length)), ref MemoryHandler._bytesTransferred);
    }

    private ulong DecodeOffsetStage3(ulong value)
    {
        ulong num = _offsetTable[10] - 70UL - 25UL;
        return value - num;
    }

    public void WriteString(ulong address, string value, bool unicode)
    {
        if (unicode)
        {
            MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, Encoding.Unicode.GetBytes(value), (UIntPtr)((ulong)((long)(value.Length * 2))), ref MemoryHandler._bytesTransferred);
            MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address + (ulong)((long)(value.Length * 2)), new byte[2], (UIntPtr)2UL, ref MemoryHandler._bytesTransferred);
            return;
        }
        MemoryHandler.WriteProcessMemory(MemoryHandler._processHandle, address, Encoding.ASCII.GetBytes(value), (UIntPtr)((ulong)((long)value.Length)), ref MemoryHandler._bytesTransferred);
        WriteNops(address + (ulong)((long)value.Length), 1, false, false);
    }

    private ulong _tableSeed0 = 6UL;

    private ulong _tableSeed1 = 254UL;

    private ulong _tableSeed2 = 54UL;
    private ulong _tableSeed4 = 90UL;

    private ulong _tableSeed5 = 9000UL;
    private ulong _tableSeed7 = 7UL;

    private ulong _tableSeed8 = 64UL;

    private ulong _tableSeed9 = 4UL;
    private ulong[] _offsetTable = new ulong[29];

    public static ulong BaseAddress;

    public int ProcessId;

    private static long _bytesTransferred;

    public ProcessModuleCollection Modules;

    private static IntPtr _processHandle;

    private static string _processName = "";

    public ProcessThreadCollection Threads;

    public double TotalProcessorTimeMs;

    public enum ThreadInfoClass
    {

        ThreadBasicInformation
    }

    [Flags]
    public enum ThreadAccess : uint
    {

        All = 2032639U
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 48)]
    public class ThreadBasicInfo
    {

        public uint ExitStatus;

        public uint Padding;

        public ulong TebBaseAddress;

        public ulong UniqueProcessId;

        public ulong UniqueThreadId;

        public ulong AffinityMask;

        public uint Priority;

        public uint BasePriority;
    }
}
