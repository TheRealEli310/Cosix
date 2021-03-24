using System;
using System.Collections.Generic;
using Cosmos.HAL;
using System.Text;
using Cosmos.System.Graphics;
using Cosmos.System;
using Sys = Cosmos.System;

namespace CosixKernel.Modules
{
    class CGM
    {
        private static int vstate = 0;
        public static void Init(bool db)
        {
            if (db)
            {
                VGADriverII.Initialize(VGAMode.Pixel320x200DB);
                vstate = 2;
            }
            else
            {
                VGADriverII.Initialize(VGAMode.Pixel320x200);
                vstate = 1;
            }
            MouseManager.ScreenHeight = 200;
            MouseManager.ScreenWidth = 320;
            VGADriverII.Clear(223);
            if (vstate == 2)
            {
                VGADriverII.Display();
            }
        }
        public static void Run()
        {
            VGADriverII.Clear(247);
            VGAGraphics.DrawString(0,0,"Cosix Graphics Manager",VGAColor.Black,VGAFont.Font8x8);
            VGAGraphics.DrawFilledRect(300,180,20,20,VGAColor.Red);
            VGAGraphics.DrawFilledRect((int)MouseManager.X, (int)MouseManager.Y, 2, 2, VGAColor.Blue);
            if (vstate == 2)
            {
                VGAGraphics.Display();
            }
            if ((MouseManager.X > 300) & (MouseManager.Y > 180) & (MouseManager.MouseState == MouseState.Left))
            {
                GoText();
                Terminal.TextColor = ConsoleColor.White;
                Terminal.BackColor = ConsoleColor.Black;
                Terminal.Clear();
            }
        }
        public static int VStateGet()
        {
            return vstate;
        }
        public static void GoText()
        {
            VGADriverII.Initialize(VGAMode.Text90x60);
            vstate = 0;
        }
        public enum VStates
        {
            Text = 0,
            GraphicsSB = 1,
            GraphicsDB = 2,
        }
        public class Window
        {
            public int X { get; }
            public int Y { get; }
            public VGAColor BackColor { get; }
            public Window(int x, int y, VGAColor bg)
            {
                (X, Y,BackColor) = (x, y,bg);
            }
        }
    }
}
