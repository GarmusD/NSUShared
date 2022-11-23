using System.Collections.Generic;
using System.ComponentModel;

namespace NSU.Shared.NSUUI
{
    public partial class Scenario
    {
        public partial class Builder
        {
            public class ScenarioObject : INotifyPropertyChanged
            {
                public static readonly string PropChangeName = "Name";
                public static readonly string PropChangeControlObjectName = "ControlObjectName";
                public static readonly string PropChangeText = "Text";
                public static readonly string PropChangeAction = "Action";
                public static readonly string PropChangeSpecialFunction = "SpecialFunction";
                public static readonly string PropChangeLeft = "Left";
                public static readonly string PropChangeTop = "Top";
                public static readonly string PropChangeRotation = "Rotation";

                public event PropertyChangedEventHandler PropertyChanged;

                public NSUUIClass UIClass { get; }
                public string Name { get => _name; set => SetName(value); }
                public string ControlObjectName { get => _controlName; set => SetControlObjectName(value); }
                public string Text { get => Text; set => SetText(value); }
                public string Action { get => _action; set => SetAction(value); }
                public string SpecialFunction { get => _specialFunction; set => SetSpecialFunction(value); }
                public int Left { get => _left; set => SetLeft(value); }
                public int Top { get => _top; set => SetTop(value); }
                public int Rotation { get => _rotation; set => SetRotation(value); }
                public INSUUIBase Object { get; set; }
                public object UIObject { get; set; }
                
                
                private string _name;
                private string _controlName;
                private string _text;
                private string _action;
                private string _specialFunction;
                private int _left;
                private int _top;
                private int _rotation;
                
                public ScenarioObject(NSUUIClass cls)
                {
                    UIClass = cls;
                    _name = "noname";
                    _text = "Tekstas";
                    _action = string.Empty;
                    _specialFunction = string.Empty;
                    _left = 0;
                    _top = 0;
                    _rotation = 0;
                    Object = null;
                    UIObject = null;
                }

                private void SetName(string value)
                {
                    if (_name != value)
                    {
                        _name = value;
                        RaisePropertyChanged(PropChangeName);
                    }
                }

                private void SetControlObjectName(string value)
                {
                    if (_controlName != value)
                    {
                        _controlName = value;
                        RaisePropertyChanged(PropChangeControlObjectName);
                    }
                }

                private void SetText(string value)
                {
                    if (!_text.Equals(value))
                    {
                        _text = value;
                        RaisePropertyChanged(PropChangeText);
                    }
                }

                private void SetAction(string value)
                {
                    if (_action != value)
                    {
                        _action = value;
                        RaisePropertyChanged(PropChangeAction);
                    }
                }

                private void SetSpecialFunction(string value)
                {
                    if (_specialFunction != value)
                    {
                        _specialFunction = value;
                        RaisePropertyChanged(PropChangeSpecialFunction);
                    }
                }

                private void SetLeft(int value)
                {
                    if (_left != value)
                    {
                        _left = value;
                        RaisePropertyChanged(PropChangeLeft);
                    }
                }

                private void SetTop(int value)
                {
                    if (_top != value)
                    {
                        _top = value;
                        RaisePropertyChanged(PropChangeTop);
                    }
                }

                private void SetRotation(int value)
                {
                    if (_rotation != value)
                    {
                        _rotation = value;
                        if (Object != null)
                        {
                            if (Object is INSUUICircPump circPump)
                            {
                                circPump.SetRotation(_rotation);
                                RaisePropertyChanged(PropChangeRotation);
                            }
                            else if (Object is INSUUIGraphics graphics)
                            {
                                //TODO Implement graphics.SetRotation(rotation);
                            }
                        }
                    }
                }

                private void RaisePropertyChanged(string prop)
                {
                    var evt = PropertyChanged;
                    evt?.Invoke(this, new PropertyChangedEventArgs(prop));
                }
                
                public override string ToString()
                {
                    return Name;
                }

            }
        }
    }
}

