﻿using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace ScreenToGif.Native
{
    internal static class Windows
    {
        /// <summary>
        /// Special window handles.
        /// </summary>
        public enum SpecialWindowHandles
        {
            /// <summary>
            /// Places the window at the top of the Z order.
            /// </summary>
            Top = 0,
            
            /// <summary>
            /// Places the window at the bottom of the Z order.
            /// If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            /// </summary>
            Bottom = 1,
            
            /// <summary>
            /// Places the window above all non-topmost windows.
            /// The window maintains its topmost position even when it is deactivated.
            /// </summary>
            TopMost = -1,
            
            /// <summary>
            /// Places the window above all non-topmost windows (that is, behind all topmost windows).
            /// This flag has no effect if the window is already a non-topmost window.
            /// </summary>
            NoTopMost = -2
        }

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            /// <summary>
            /// If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window.
            /// This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            AsyncWindowPositioning = 0x4000,

            /// <summary>
            /// Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            DeferErase = 0x2000,

            /// <summary>
            /// Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            DrawFrame = 0x0020,

            /// <summary>
            /// Applies new frame styles set using the SetWindowLong function.
            /// Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed.
            /// If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            FrameChanged = 0x0020,

            /// <summary>
            /// Hides the window.
            /// </summary>
            HideWindow = 0x0080,

            /// <summary>
            /// Does not activate the window.
            /// If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            NoActivate = 0x0010,

            /// <summary>
            /// Discards the entire contents of the client area.
            /// If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            NoCopyBits = 0x0100,

            /// <summary>
            /// Retains the current position (ignores X and Y parameters).
            /// </summary>
            NoMove = 0x0002,

            /// <summary>
            /// Does not change the owner window's position in the Z order.
            /// </summary>
            NoOwnerZOrder = 0x0200,

            /// <summary>
            /// Does not redraw changes. If this flag is set, no repainting of any kind occurs.
            /// This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved.
            /// When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            NoRedraw = 0x0008,

            /// <summary>
            /// Same as the NoOwnerZOrder flag.
            /// </summary>
            NoReposition = 0x0200,

            /// <summary>
            /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            NoSendChanging = 0x0400,

            /// <summary>
            /// Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            NoSize = 0x0001,

            /// <summary>
            /// Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            NoZOrder = 0x0004,

            /// <summary>
            /// Displays the window.
            /// </summary>
            ShowWindow = 0x0040
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);


        public static void MoveToScreen(this Window window, Monitor next, bool fullScreen = false)
        {
            if (fullScreen)
            {
                SetWindowPos(new System.Windows.Interop.WindowInteropHelper(window).Handle, (IntPtr)SpecialWindowHandles.Top,
                    (int)next.NativeBounds.Left, (int)next.NativeBounds.Top, (int)next.NativeBounds.Width, (int)next.NativeBounds.Height, SetWindowPosFlags.ShowWindow);
                return;
            }

            SetWindowPos(new System.Windows.Interop.WindowInteropHelper(window).Handle, (IntPtr)SpecialWindowHandles.Top,
                (int)next.NativeBounds.Left, (int)next.NativeBounds.Top, (int)window.Width, (int)window.Height, SetWindowPosFlags.ShowWindow);
        }
    }
}