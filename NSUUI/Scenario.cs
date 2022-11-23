using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace NSU.Shared.NSUUI
{
    public partial class Scenario
    {
        const string LogTag = "Scenario";
        public enum Commands
        {
            //Graphics commands
            GC_UNKNOWN = 0,
            GC_SET_COLOR = 1,
            GC_SET_BG_COLOR = 2,
            GC_DRAW_PIXEL = 3,
            GC_DRAW_LINE = 4,
            GC_DRAW_RECT = 5,
            GC_DRAW_ROUND_RECT = 6,
            GC_FILL_RECT = 7,
            GC_FILL_ROUND_RECT = 8,
            GC_DRAW_CIRCLE = 9,
            GC_FILL_CIRCLE = 10,
            GC_DRAW_ARC = 11,
            GC_FILL_ARC = 12,
            GC_DRAW_BUILT_IN_GRAPHICS = 13,//from byte array
            GC_PLAY_COMMANDS = 14,
            GC_LOAD_FILE = 15,//from file

            //UI commands
            GC_NEW_OBJECT = 16,
            GC_NEW_OBJECT_END = 17,
            //UI params
            GC_OBJECT_WIDTH = 25,
            GC_OBJECT_HEIGHT = 26,
        }

        public enum Objects
        {

        }

        enum ObjectAttribute
        {
            Name,
            Caption,
            Left,
            Top,
            CenterX,
            Width,
            Height,
        }

		short GetBufShort(byte[] buf, ref int idx)
        {
            byte cl, ch;
            ch = buf[idx++];
			cl = buf[idx++];
            return (short)((ch << 8) | cl);
        }

        public void PlayScenario(INSUWindowsManager winman, byte[] buf)
        {
            //NSULog.Debug(LogTag, "PlayScenario() - kuriamos grupes...");
            var grp = winman.CreateGroup("katiline");
            grp.IsDefault = true;
            var wnd = grp.CreateWindow("katiline");
            wnd.IsDefault = true;

            var sw = grp.SideWindow;
            //Winter mode
            var swb = sw.AddUIElement(NSUUISideElementClass.Switch) as INSUUISwitchButton;
            swb?.SetOnBMPBytes(UIBytes.UIBytes.SnaigeBMP);
            swb?.SetOffBMPBytes(UIBytes.UIBytes.SauleBMP);
            swb?.SetSwitchName("winter_mode");

            //Temperature mode
            swb = sw.AddUIElement(NSUUISideElementClass.Switch) as INSUUISwitchButton;
            swb?.SetOnBMPBytes(UIBytes.UIBytes.TempUpBMP);
            swb?.SetOffBMPBytes(UIBytes.UIBytes.TempDownBMP);
            swb?.SetSwitchName("temp_mode");

            //Hot water mode
            swb = sw.AddUIElement(NSUUISideElementClass.Switch) as INSUUISwitchButton;
            swb?.SetOnBMPBytes(UIBytes.UIBytes.HotWaterOnBMP);
            swb?.SetOffBMPBytes(UIBytes.UIBytes.HotWaterOffBMP);
            swb?.SetSwitchName("boiler_mode");

            //House - window group switch
            var grpbtn = sw.AddUIElement(NSUUISideElementClass.GroupSwitch) as INSUUIWindowGroupChangeButton;            
            grpbtn?.SetOnBMPBytes(UIBytes.UIBytes.HouseBMP);
            grpbtn?.SetGroupName("namas");

            var g = wnd.AddUIElement(NSUUIClass.Graphics) as INSUUIGraphics;
            g.Left = 0;
            g.Top = 0;
            g.Width = 714 - 4;
            g.Height = 480 - 4;
            g.SetColor(NSUColor.RGB(0, 0, 255));
            PlayScenario(g.GetDrawer(), 0, 0, UIBytes.Drawings.g_katiline_bg);
            g.UIID = "KatilineBG";

            var kaminas = wnd.AddUIElement(NSUUIClass.MonoBitmap) as INSUUIMonoBmp;
            kaminas.SetResource(NSUBitmapResource.Kaminas);
            kaminas.Left = 41;
            kaminas.Top = 12;
            kaminas.UIID = "Kaminas";

            var chimneytemp = wnd.AddUIElement(NSUUIClass.TempLabel) as INSUUITempLabel;
            chimneytemp.Left = 78;
            chimneytemp.Top = 72;
            chimneytemp.UIID = "chimneyt";
            chimneytemp.AttachKType("dumu_temp");

            var smokefan = wnd.AddUIElement(NSUUIClass.Ventilator) as INSUUIExhaustFan;
            smokefan.Left = 43;
            smokefan.Top = 145;
            smokefan.UIID = "vent";
            smokefan.AttachSmokeFan("default");

            var katilas = wnd.AddUIElement(NSUUIClass.MonoBitmap) as INSUUIMonoBmp;
            katilas.SetResource(NSUBitmapResource.Katilas);
            katilas.Left = 23;
            katilas.Top = 191;
            katilas.UIID = "Katilas";

            var ktemp = wnd.AddUIElement(NSUUIClass.TempLabel) as INSUUITempLabel;
            ktemp.CenterX = 84;
            ktemp.Top = 290;
            ktemp.UIID = "ktemp";
            ktemp.AttachTempSensor("katilas");

            var ktemp2 = wnd.AddUIElement(NSUUIClass.TempLabel) as INSUUITempLabel;
            ktemp2.CenterX = 194;
            ktemp2.Top = 247;
            ktemp2.UIID = "ktemp2";
            ktemp2.AttachTempSensor("arduino");

            var cirk1 = wnd.AddUIElement(NSUUIClass.CircPump) as INSUUICircPump;
            cirk1.Left = 192;
            cirk1.Top = 28;
            cirk1.SetRotation(1);
            cirk1.UIID = "cirk_boilerio";
            cirk1.AttachPump("boilerio");

            var boileris = wnd.AddUIElement(NSUUIClass.MonoBitmap) as INSUUIMonoBmp;
            boileris.SetResource(NSUBitmapResource.Boileris);
            boileris.Left = 241;
            boileris.Top = 12;
            boileris.UIID = "boileris";

            var boilert_virsus = wnd.AddUIElement(NSUUIClass.TempLabel) as INSUUITempLabel;
            boilert_virsus.CenterX = 288;
            boilert_virsus.Top = 50;
            boilert_virsus.UIID = "boiler_virsus";
            boilert_virsus.AttachTempSensor("boileris_virsus");

            var boilert_apacia = wnd.AddUIElement(NSUUIClass.TempLabel) as INSUUITempLabel;
            boilert_apacia.CenterX = 288;
            boilert_apacia.Top = 140;
            boilert_apacia.UIID = "boiler_apacia";
            boilert_apacia.AttachTempSensor("boileris_apacia");

            var ladom = wnd.AddUIElement(NSUUIClass.Ladomat) as INSUUILadomat;
            ladom.Left = 225;
            ladom.Top = 295;
            ladom.UIID = "Ladomatas";
            ladom.AttachLadomat("default");

            var trisakis = wnd.AddUIElement(NSUUIClass.MonoBitmap) as INSUUIMonoBmp;
            trisakis.SetResource(NSUBitmapResource.Trisakis);
            trisakis.Left = 233;
            trisakis.Top = 210;
            trisakis.UIID = "Trisakis";

            var akum = wnd.AddUIElement(NSUUIClass.MonoBitmap) as INSUUIMonoBmp;
            akum.SetResource(NSUBitmapResource.Akumuliacine);
            akum.Left = 378;
            akum.Top = 150;
            akum.UIID = "akumulc";

            var akumt1 = wnd.AddUIElement(NSUUIClass.TempLabel) as INSUUITempLabel;
            akumt1.CenterX = 464;
            akumt1.Top = 160;
            akumt1.UIID = "akumt1";
            akumt1.AttachTempSensor("akum_virsus");

            var akumt2 = wnd.AddUIElement(NSUUIClass.TempLabel) as INSUUITempLabel;
            akumt2.CenterX = 464;
            akumt2.Top = 259;
            akumt2.UIID = "akumt2";
            akumt2.AttachTempSensor("akum_vidurys");

            var akumt3 = wnd.AddUIElement(NSUUIClass.TempLabel) as INSUUITempLabel;
            akumt3.CenterX = 464;
            akumt3.Top = 359;
            akumt3.UIID = "akumt3";
            akumt3.AttachTempSensor("akum_apacia");

            var tempbar = wnd.AddUIElement(NSUUIClass.TempBar) as INSUUITempBar;
            tempbar.Left = 385;
            tempbar.Top = 160;
            tempbar.UIID = "tempbar";
            tempbar.SetTempSensors("akum_virsus", "akum_vidurys", "akum_apacia");

            var kol1 = wnd.AddUIElement(NSUUIClass.MonoBitmap) as INSUUIMonoBmp;
            kol1.SetResource(NSUBitmapResource.Kolektorius);
            kol1.Left = 566;
            kol1.Top = 84;
            kol1.UIID = "kolekt1";

            var grindys = wnd.AddUIElement(NSUUIClass.MonoBitmap) as INSUUIMonoBmp;
            grindys.SetResource(NSUBitmapResource.Grindys);
            grindys.Left = 398;
            grindys.Top = 14;
            grindys.UIID = "grindys";

            var cirk2 = wnd.AddUIElement(NSUUIClass.CircPump) as INSUUICircPump;
            cirk2.Left = 517;
            cirk2.Top = 110;
            cirk2.SetRotation(4);
            cirk2.UIID = "grindu_cirk";
            cirk2.AttachPump("grindu");

            var radiatorius = wnd.AddUIElement(NSUUIClass.MonoBitmap) as INSUUIMonoBmp;
            radiatorius.SetResource(NSUBitmapResource.Radiatorius);
            radiatorius.Left = 561;
            radiatorius.Top = 11;
            radiatorius.UIID = "radiat";

            var kol2 = wnd.AddUIElement(NSUUIClass.MonoBitmap) as INSUUIMonoBmp;
            kol2.SetResource(NSUBitmapResource.Kolektorius);
            kol2.Left = 401;
            kol2.Top = 84;
            kol2.UIID = "kolekt2";

            var paduodamas = wnd.AddUIElement(NSUUIClass.TempLabel) as INSUUITempLabel;
            paduodamas.CenterX = 450;
            paduodamas.Top = 105;
            paduodamas.UIID = "grindu_paduodamas";
            paduodamas.AttachTempSensor("grindu_paduodamas");

            var cirk3 = wnd.AddUIElement(NSUUIClass.CircPump) as INSUUICircPump;
            cirk3.Left = 665;
            cirk3.Top = 110;
            cirk3.SetRotation(4);
            cirk3.UIID = "cirk_radiatoriu";
            cirk3.AttachPump("radiatoriu");

            var btn = wnd.AddUIElement(NSUUIClass.Button) as INSUUIButton;
            btn.Left = 35;
            btn.Top = 397;
            btn.UIID = "btnIkurimas";
            btn.Caption = "Įkūrimas";
            btn.Action = NSUUIActions.KatiloIkurimas;

            var lbl = wnd.AddUIElement(NSUUIClass.Label) as INSUUILabel;
            lbl.Left = 35;
            lbl.Top = 440;
            lbl.UIID = "lblWBStatus";
            //lbl.Caption = "Katilo busena";
            lbl.SetAction(NSUUIActions.ShowWoodBoilerStatus, "default");


            //Create second windows group
            //NSULog.Debug(LogTag, "PlayScenario() - Kuriama grupe 'namas' ...");
            grp = winman.CreateGroup("namas");
            sw = grp.SideWindow;

            grpbtn = sw.AddUIElement(NSUUISideElementClass.GroupSwitch) as INSUUIWindowGroupChangeButton;
            grpbtn?.SetGroupName("katiline");
            grpbtn?.SetOnBMPBytes(UIBytes.UIBytes.BackArrowBMP);

            var wswth = sw.AddUIElement(NSUUISideElementClass.WindowSwitch) as INSUUIWindowChangeButton;
            wswth?.SetWindowName("2aukstas");
            wswth?.SetOnBMPBytes(UIBytes.UIBytes.House2AukstasOn);
            wswth?.SetOffBMPBytes(UIBytes.UIBytes.House2AukstasOff);

            wswth = sw.AddUIElement(NSUUISideElementClass.WindowSwitch) as INSUUIWindowChangeButton;
            wswth?.SetWindowName("1aukstas");
            wswth?.SetOnBMPBytes(UIBytes.UIBytes.House1AukstasOn);
            wswth?.SetOffBMPBytes(UIBytes.UIBytes.House1AukstasOff);

            wswth = sw.AddUIElement(NSUUISideElementClass.WindowSwitch) as INSUUIWindowChangeButton;
            wswth?.SetWindowName("rusys");
            wswth?.SetOnBMPBytes(UIBytes.UIBytes.HouseRusysOn);
            wswth?.SetOffBMPBytes(UIBytes.UIBytes.HouseRusysOff);

            wswth = sw.AddUIElement(NSUUISideElementClass.WindowSwitch) as INSUUIWindowChangeButton;
            wswth?.SetWindowName("oras");
            wswth?.SetOnBMPBytes(UIBytes.UIBytes.WeatherOnBMP);
            wswth?.SetOffBMPBytes(UIBytes.UIBytes.WeatherOffBMP);

            //Create windows
            INSUUIComfortZone? cz;

            wnd = grp.CreateWindow("rusys");
            wnd.IsDefault = false;
            g = wnd.AddUIElement(NSUUIClass.Graphics) as INSUUIGraphics;
            g.Left = 0;
            g.Top = 0;
            g.Width = 714 - 4;
            g.Height = 480 - 4;
            g.SetColor(NSUColor.RGB(0, 0, 255));
            PlayScenario(g.GetDrawer(), 0, 0, UIBytes.Drawings.g_rusys_bg);
            g.UIID = "RusysBG";

			cz = wnd.AddUIElement(NSUUIClass.ComfortZone) as INSUUIComfortZone;
			cz.Left = 320;
			cz.Top = 95;
			cz.AttachComfortZone("rusys2");
			
			cz = wnd.AddUIElement(NSUUIClass.ComfortZone) as INSUUIComfortZone;
			cz.Left = 320;
			cz.Top = 309;
			cz.AttachComfortZone("rusys1");

			wnd = grp.CreateWindow("1aukstas");
            wnd.IsDefault = true;
            g = wnd.AddUIElement(NSUUIClass.Graphics) as INSUUIGraphics;
            g.Left = 0;
            g.Top = 0;
            g.Width = 714 - 4;
            g.Height = 480 - 4;
            g.SetColor(NSUColor.RGB(0, 0, 255));
            PlayScenario(g.GetDrawer(), 0, 0, UIBytes.Drawings.g_1aukstas_bg);
            g.UIID = "1aukstasBG";

            cz = wnd.AddUIElement(NSUUIClass.ComfortZone) as INSUUIComfortZone;
            cz.Left = 102;
            cz.Top = 78;
            cz.AttachComfortZone("skalbykla");

            cz = wnd.AddUIElement(NSUUIClass.ComfortZone) as INSUUIComfortZone;
            cz.Left = 102;
            cz.Top = 198;
            cz.AttachComfortZone("koridorius");

            cz = wnd.AddUIElement(NSUUIClass.ComfortZone) as INSUUIComfortZone;
            cz.Left = 102;
            cz.Top = 319;
            cz.AttachComfortZone("tualetas");

            cz = wnd.AddUIElement(NSUUIClass.ComfortZone) as INSUUIComfortZone;
            cz.Left = 320;
            cz.Top = 95;
            cz.AttachComfortZone("virtuve");

            cz = wnd.AddUIElement(NSUUIClass.ComfortZone) as INSUUIComfortZone;
            cz.Left = 320;
            cz.Top = 309;
            cz.AttachComfortZone("sveciu_kambarys");

            cz = wnd.AddUIElement(NSUUIClass.ComfortZone) as INSUUIComfortZone;
            cz.Left = 472;
            cz.Top = 95;
            //cz.Width = 131;
            cz.AttachComfortZone("svetaine_didele");

            cz = wnd.AddUIElement(NSUUIClass.ComfortZone) as INSUUIComfortZone;
            cz.Left = 472;
            cz.Top = 309;
            //cz.Width = 131;
            cz.AttachComfortZone("svetaine_maza");

            cz = wnd.AddUIElement(NSUUIClass.ComfortZone) as INSUUIComfortZone;
            cz.Left = 608;
            cz.Top = 193;
            //cz.Width = 131;
            cz.AttachComfortZone("darbo_kambarys");
            


            wnd = grp.CreateWindow("2aukstas");
            wnd.IsDefault = false;
            g = wnd.AddUIElement(NSUUIClass.Graphics) as INSUUIGraphics;
            g.Left = 0;
            g.Top = 0;
            g.Width = 714 - 4;
            g.Height = 480 - 4;
            g.SetColor(NSUColor.RGB(0, 0, 255));
            PlayScenario(g.GetDrawer(), 0, 0, UIBytes.Drawings.g_2aukstas_bg);
            g.UIID = "2aukstasBG";

            wnd = grp.CreateWindow("oras");
            wnd.IsDefault = false;

            if (wnd.AddUIElement(NSUUIClass.WeatherInfo) is INSUUIWeatherInfo wi)
            {
                wi.Left = 5;
                wi.Top = 5;
                wi.Width = 704;
                wi.Height = 470;
                wi.AttachLaukoSensor("laukas");
                wi.AttachPozemioSensor("pozemis");
            }

            //NSULog.Debug(LogTag, "PlayScenario() - BAIGTA.");
        }

        public void PlayScenario(INSUUIDrawer drawer, int xbase, int ybase, byte[] buf)
        {

            if(buf.Length == 0 ) return;

            int idx = 0;
            byte cmd;

            short s1, s2, s3, s4;
            byte b1, b2, b3;
            while((cmd = buf[idx++]) > 0)
            {
                switch ((Commands)cmd)
                {
                    case Commands.GC_SET_COLOR:
                        (b1, b2, b3) = GetThreeByteParams(buf, ref idx);
                        drawer.SetColor(b1, b2, b3);
                        break;
                    case Commands.GC_SET_BG_COLOR:
                        (b1, b2, b3) = GetThreeByteParams(buf, ref idx);
                        drawer.SetBackColor(b1, b2, b3);
                        break;
                    case Commands.GC_DRAW_PIXEL:
                        (s1, s2) = GetTwoShortParams(xbase, ybase, buf, ref idx);
                        drawer.DrawPixel(s1, s2);
                        break;
                    case Commands.GC_DRAW_LINE:
                        (s1, s2, s3, s4) = GetFourShortParams(xbase, ybase, buf, ref idx);
                        drawer.DrawLine(s1, s2, s3, s4);
                        break;
                    case Commands.GC_DRAW_RECT:
                        (s1, s2, s3, s4) = GetFourShortParams(xbase, ybase, buf, ref idx);
                        drawer.DrawRect(s1, s2, s3, s4);
                        break;
                    case Commands.GC_DRAW_ROUND_RECT:
                        (s1, s2, s3, s4) = GetFourShortParams(xbase, ybase, buf, ref idx);
                        drawer.DrawRoundRect(s1, s2, s3, s4);
                        break;
                    case Commands.GC_FILL_RECT:
                        (s1, s2, s3, s4) = GetFourShortParams(xbase, ybase, buf, ref idx);
                        drawer.FillRect(s1, s2, s3, s4);
                        break;
                    case Commands.GC_FILL_ROUND_RECT:
                        (s1, s2, s3, s4) = GetFourShortParams(xbase, ybase, buf, ref idx);
                        drawer.FillRoundRect(s1, s2, s3, s4);
                        break;
                    case Commands.GC_DRAW_CIRCLE:
                        (s1, s2, s3) = GetThreeShortParams(xbase, ybase, buf, ref idx);
                        drawer.DrawCircle(s1, s2, s3);
                        break;
                    case Commands.GC_FILL_CIRCLE:
                        (s1, s2, s3) = GetThreeShortParams(xbase, ybase, buf, ref idx);
                        drawer.FillCircle(s1, s2, s3);
                        break;
                    case Commands.GC_DRAW_ARC:
                        break;
                    case Commands.GC_FILL_ARC:
                        break;

                    default:
                        break;
                }
            }
        }

        private (byte, byte, byte) GetThreeByteParams(byte[] buf, ref int idx)
        {
            return (buf[idx++], buf[idx++], buf[idx++]);
        }

        private (short, short) GetTwoShortParams(int xbase, int ybase, byte[] buf, ref int idx)
        {
            short s1 = (short)(xbase + GetBufShort(buf, ref idx));
            short s2 = (short)(ybase + GetBufShort(buf, ref idx));
            return (s1, s2);
        }

        private (short, short, short) GetThreeShortParams(int xbase, int ybase, byte[] buf, ref int idx)
        {
            short s1 = (short)(xbase + GetBufShort(buf, ref idx));
            short s2 = (short)(ybase + GetBufShort(buf, ref idx));
            short s3 = GetBufShort(buf, ref idx);
            return (s1, s2, s3);
        }

        private (short, short, short, short) GetFourShortParams(int xbase, int ybase, byte[] buf, ref int idx)
        {
            short s1 = (short)(xbase + GetBufShort(buf, ref idx));
            short s2 = (short)(ybase + GetBufShort(buf, ref idx));
            short s3 = (short)(xbase + GetBufShort(buf, ref idx));
            short s4 = (short)(ybase + GetBufShort(buf, ref idx));
            return (s1, s2, s3, s4);
        }

        void PlayNewObject(INSUWindow window, byte[] buf, int start)
        {

        }
    }
}

