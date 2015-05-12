using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseControl
{
    class MouseControl
    {
        // Methods
        public static uint Click()
        {
            INPUT structure = new INPUT
            {
                type = InputType.INPUT_MOUSE
            };
            structure.mi.dx = 0;
            structure.mi.dy = 0;
            structure.mi.mouseData = 0;
            structure.mi.dwFlags = MOUSEEVENTF.LEFTDOWN;
            structure.mi.time = 0;
            structure.mi.dwExtraInfo = GetMessageExtraInfo();
            INPUT input2 = structure;
            input2.mi.dwFlags = MOUSEEVENTF.LEFTUP;
            INPUT[] pInputs = new INPUT[] { structure, input2 };
            return SendInput(2, pInputs, Marshal.SizeOf(structure));
        }

        public static Position CurrentMousePos()
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            return new Position { X = x, Y = y };
        }

        public static void DeltaMove(int dx, int dy)
        {
            Move(CurrentMousePos().X + dx, CurrentMousePos().Y + dy);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetMessageExtraInfo();
        public static uint MouseDownLeft()
        {
            INPUT structure = new INPUT
            {
                type = InputType.INPUT_MOUSE
            };
            structure.mi.dx = 0;
            structure.mi.dy = 0;
            structure.mi.mouseData = 0;
            structure.mi.dwFlags = MOUSEEVENTF.LEFTDOWN;
            structure.mi.time = 0;
            structure.mi.dwExtraInfo = GetMessageExtraInfo();
            INPUT[] pInputs = new INPUT[] { structure };
            return SendInput(1, pInputs, Marshal.SizeOf(structure));
        }

        public static uint MouseUpLeft()
        {
            INPUT structure = new INPUT
            {
                type = InputType.INPUT_MOUSE
            };
            structure.mi.dx = 0;
            structure.mi.dy = 0;
            structure.mi.mouseData = 0;
            structure.mi.dwFlags = MOUSEEVENTF.LEFTUP;
            structure.mi.time = 0;
            structure.mi.dwExtraInfo = GetMessageExtraInfo();
            INPUT[] pInputs = new INPUT[] { structure };
            return SendInput(1, pInputs, Marshal.SizeOf(structure));
        }

        public static uint Move(int x, int y)
        {
            float width = Screen.PrimaryScreen.Bounds.Width;
            float height = Screen.PrimaryScreen.Bounds.Height;
            INPUT structure = new INPUT
            {
                type = InputType.INPUT_MOUSE
            };
            structure.mi.dx = (int)Math.Round((double)(x * (65535f / width)), 0);
            structure.mi.dy = (int)Math.Round((double)(y * (65535f / height)), 0);
            structure.mi.mouseData = 0;
            structure.mi.dwFlags = MOUSEEVENTF.ABSOLUTE | MOUSEEVENTF.MOVE;
            structure.mi.time = 0;
            structure.mi.dwExtraInfo = GetMessageExtraInfo();
            INPUT[] pInputs = new INPUT[] { structure };
            return SendInput(1, pInputs, Marshal.SizeOf(structure));
        }

        public static uint RightClick()
        {
            INPUT structure = new INPUT
            {
                type = InputType.INPUT_MOUSE
            };
            structure.mi.dx = 0;
            structure.mi.dy = 0;
            structure.mi.mouseData = 0;
            structure.mi.dwFlags = MOUSEEVENTF.RIGHTDOWN;
            structure.mi.time = 0;
            structure.mi.dwExtraInfo = GetMessageExtraInfo();
            INPUT input2 = structure;
            input2.mi.dwFlags = MOUSEEVENTF.RIGHTUP;
            INPUT[] pInputs = new INPUT[] { structure, input2 };
            return SendInput(2, pInputs, Marshal.SizeOf(structure));
        }

        public static uint ScrollDown(int amount)
        {
            INPUT structure = new INPUT
            {
                type = InputType.INPUT_MOUSE
            };
            structure.mi.dx = 0;
            structure.mi.dy = 0;
            structure.mi.mouseData = amount;
            structure.mi.dwFlags = MOUSEEVENTF.WHEEL;
            structure.mi.time = 0;
            structure.mi.dwExtraInfo = GetMessageExtraInfo();
            INPUT[] pInputs = new INPUT[] { structure };
            return SendInput(1, pInputs, Marshal.SizeOf(structure));
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        // Nested Types
        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public MouseControl.InputType type;
            public MouseControl.MOUSEINPUT mi;
        }

        private enum InputType
        {
            INPUT_MOUSE,
            INPUT_KEYBOARD,
            INPUT_HARDWARE
        }

        [Flags]
        private enum MOUSEEVENTF
        {
            ABSOLUTE = 0x8000,
            LEFTDOWN = 2,
            LEFTUP = 4,
            MIDDLEDOWN = 0x20,
            MIDDLEUP = 0x40,
            MOVE = 1,
            RIGHTDOWN = 8,
            RIGHTUP = 0x10,
            VIRTUALDESK = 0x4000,
            WHEEL = 0x800,
            XDOWN = 0x80,
            XUP = 0x100
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public MouseControl.MOUSEEVENTF dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Position
        {
            public int X;
            public int Y;
        }



    }
}
