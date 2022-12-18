using System;
using System.Collections;
#if WINDOWS_UWP
using Windows.UI.Xaml;
#endif

namespace NSU.Shared.NSUUI
{
	public enum NSUUITypes
    {
    };

    public class NSUColor
    {
        public byte R { get {return r; } }
        public byte G { get {return g; } }
        public byte B { get {return b; } }
        private byte r, g, b;

        public static NSUColor RGB(byte r, byte g, byte b)
        {
            return new NSUColor
            {
                r = r,
                g = g,
                b = b
            };
        }

        public static NSUColor ON { get { return RGB(139, 197, 63); }}
        public static NSUColor OFF { get { return RGB(255, 0, 0); }}
        public static NSUColor MANUAL { get { return RGB(127, 63, 151); }}
        public static NSUColor DISABLED { get { return RGB(112, 61, 0); } }
        public static NSUColor UNKNOWN {get { return RGB(0, 0, 0); }}
    }

    public enum NSUUIActions
    {
        NoAction,
        ShowWoodBoilerStatus,
        KatiloIkurimas
    };

    public enum NSUUIClass
    {
        None,
        MonoBitmap,
        Button,
        Input,
        Label,
        TempLabel,
        Graphics,
        SwitchButton,
        Ladomat,
        ExhaustFan,
        CircPump,
        TempBar,
        WeatherInfo,
        ComfortZone
    };

    public enum NSUUISideElementClass
    {
        Unknown,
        Switch,
        WindowSwitch,
        GroupSwitch
    }

    public enum NSUBitmapResource
    {
        UnknownResource,
        Akumuliacine,        
        Boileris,
        Cirkuliacinis,
        Grindys,
        Kaminas,
        Katilas,
        Kolektorius,
        Ladomatas,
        Radiatorius,
        Trisakis,
        Ventiliatorius
    }

    public enum NSUTextAlign{
        AlignLeft,
        AlignCenter,
        AlignRight
    };

    public interface INSUUIDrawer
    {
        void SetColor(byte r, byte g, byte b);
        void SetBackColor(byte r, byte g, byte b);
        void DrawPixel(int x, int y);
        void DrawLine(int x1, int y1, int x2, int y2);
        void DrawRect(int x1, int y1, int x2, int y2);
        void DrawRoundRect(int x1, int y1, int x2, int y2);
        void FillRect(int x1, int y1, int x2, int y2);
        void FillRoundRect(int x1, int y1, int x2, int y2);
        void DrawCircle(int x, int y, int radius);
        void FillCircle(int x, int y, int radius);
    }

    public interface INSUUIMonoBmp : INSUUIBase
    {
        void SetColor(NSUColor clr);
        void SetSize(int w, int h);
        void SetResource(NSUBitmapResource r);
        void SetRotation(int rot);
        INSUUIDrawer Drawer{ get; }
    }

    public interface INSUUIBase
    {        
        NSUUIClass UIClass { get; }
        string UIID{ get; set;}
        int Width { get; set; }
        int Height { get; set; }
        int Left { get; set; }
        int Top { get; set; }
        void AttachedToWindow();
        void DeatachedFromWindow();
        void Free();
    }

    /*********************************************
     *********************************************/
    public interface INSUUIButton : INSUUIBase
    {
        void SetEnabled(bool value);
        byte FontHeight { get; set; }
        string Caption { get; set; }
        NSUUIActions Action { get; set; }
    }

    /*********************************************
    *********************************************/

    public interface INSUUILabel : INSUUIBase
    {
        string Caption { get; set; }
        NSUTextAlign TextAlign { get; set; }
        void SetAction(NSUUIActions action, string param);
    }

    /*********************************************
    *********************************************/
    public interface INSUUITempLabel : INSUUIBase
    {
        int CenterX{set;}
        void AttachTempSensor(string tsname);
        void AttachKType(string tsname);
    };

    /*********************************************
    *********************************************/
    public interface INSUUIGraphics : INSUUIBase
    {
        void SetGraphicsBytes(byte[] buf);
        void SetVectorBytes(byte[] buf);
        void SetColor(NSUColor color);
        void SetBGColor(NSUColor color);
        INSUUIDrawer GetDrawer();
    }

    /*********************************************
    *********************************************/
    public interface INSUUITempBar : INSUUIBase
    {
        void SetTempSensors(string s1, string s2, string s3);
    }

    /*********************************************
    *********************************************/
    public interface INSUUICircPump : INSUUIBase
    {
        void AttachPump(string pumpName);
        void SetRotation(int val);
    };

    /*********************************************
    *********************************************/
    public interface INSUUILadomat : INSUUIBase
    {
        void AttachLadomat(string name);
    }
    /*********************************************
    *********************************************/
    public interface INSUUIExhaustFan : INSUUIBase
    {
        void AttachExhaustFanByName(string name);
    }
    /*********************************************
    *********************************************/
    public interface INSUUIWeatherInfo : INSUUIBase
    {
        void AttachLaukoSensor(string name);
        void AttachPozemioSensor(string name);
    }
    /*********************************************
    *********************************************/
    public interface INSUUIComfortZone : INSUUIBase
    {
        void AttachComfortZone(string name);
    }
    /*********************************************
    *********************************************/
    /*********************************************
     * 
     * 
     * 
     * 
     * 
     * 
     * 
    *********************************************/
    public class SideEventArgs : EventArgs
    {
        public string Name { get; }
        public SideEventArgs(string name)
        {
            Name = name;
        }
    }
    //public delegate void SideEventHandler(object sender, SideEventArgs args);
    /*********************************************
    *********************************************/
    public interface INSUUISideElementBase
    {
#if WINDOWS_UWP
        UIElement uiElement { get; }
#endif
        NSUUISideElementClass UIClass { get; }
        void SetOnBMPBytes(ushort[] data);
        void SetOffBMPBytes(ushort[] data);
        void Free();
    }    
    /*********************************************
    *********************************************/
    public interface INSUUISideElement
    {
        
    }
    /*********************************************
    *********************************************/
    /*********************************************
    *********************************************/
    public interface INSUUISwitchButton : INSUUISideElementBase
    {
        void SetSwitchName(string name);
        event EventHandler<SideEventArgs> OnClicked;
    }
    /*********************************************
    *********************************************/
    public interface INSUUIWindowGroupChangeButton : INSUUISideElementBase
    {
        void SetGroupName(string name);
        void GroupChanged(string name);
        event EventHandler<SideEventArgs> OnClicked;
    }
    /*********************************************
    *********************************************/
    public interface INSUUIWindowChangeButton : INSUUISideElementBase
    {
        void SetWindowName(string name);
        void WindowChanged(string name);
        event EventHandler<SideEventArgs> OnClicked;
    }
    /*********************************************
    *********************************************/

    /*********************************************
     * 
     * 
     * 
     * 
     * 
     * 
     * 
    *********************************************/
    public interface INSUWindow : IEnumerable
    {
        string Name { get; }
        bool IsDefault { get; set; }
        INSUUIBase AddUIElement(NSUUIClass uiclass);
        int Count { get; }
        INSUUIBase this[int index] { get; }
    }

    public interface INSUSideWindow : IEnumerable
    {
        int Count { get; }
        INSUUISideElementBase this[int index] { get; }
        INSUUISideElementBase AddUIElement(NSUUISideElementClass uiclass);
    }

    public interface INSUWindowsGroup
    {
        string Name { get; }
        bool IsDefault { get; set; }
        INSUWindow CurrentWindow { get; }
        INSUWindow CreateWindow(string windowName);
        INSUSideWindow SideWindow { get; }
        void ActivateWindow(string name);
        event EventHandler OnWindowChanged;
    }

    public interface INSUWindowsManager
    {
        INSUWindowsGroup CreateGroup(string grpName);
        void DeleteGroup(string grpName);
        INSUWindowsGroup FindGroup(string grpName);
        int Count { get; }
        INSUWindowsGroup this[int index] { get; }
        INSUWindowsGroup CurrentGroup { get; }
        void ActivateWindowsGroup(string grpName);
        event EventHandler OnWindowsGroupChanged;
        event EventHandler OnWindowChanged;
    }
}

