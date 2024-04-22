namespace LegoKbdForms;

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.Wave;


public class KeyboardHook
{
    private const int WH_KEYBOARD_LL = 13;
    private const int WM_KEYDOWN = 0x0100;
    private static LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    private static Random _random = new();

    // WaveOutEvent for sound playback
    private static WaveOutEvent waveOutEvent = new WaveOutEvent();

    public static void HookKeyboard()
    {
        _hookID = SetHook(_proc);
    }

    public static void UnhookKeyboard()
    {
        UnhookWindowsHookEx(_hookID);
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
        using (var curModule = curProcess.MainModule)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
        }
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);

            // Play sound asynchronously
            ThreadPool.QueueUserWorkItem(state => PlaySound("LegoNormalized-"+_random.Next(1,23)+".wav"));
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    private static void PlaySound(string fileName)
    {
        string soundFilePath = Path.Combine(Directory.GetCurrentDirectory(), "sounds", fileName);
        try
        {
            if (File.Exists(soundFilePath))
            {
                using (var audioFile = new AudioFileReader(soundFilePath))
                using (var waveOutEvent = new WaveOutEvent())
                {
                    waveOutEvent.Init(audioFile);
                    waveOutEvent.Play();
                    // Wait for playback to finish
                    while (waveOutEvent.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            else
            {
                Console.WriteLine("Sound file not found: " + soundFilePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error playing sound: " + ex.Message);
        }
    }



    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);
}


