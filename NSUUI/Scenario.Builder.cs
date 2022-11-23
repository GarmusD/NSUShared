using System;
using System.Collections.Generic;
using System.Linq;

namespace NSU.Shared.NSUUI
{
	public partial class Scenario
    {
        public partial class Builder
        {
			private readonly List<ScenarioObject> _uiObjects;

            public List<ScenarioObject> UIObjectList => _uiObjects;

            public Builder()
            {
                _uiObjects = new List<ScenarioObject>();
            }

            public void Load(string filename)
            {
                throw new NotImplementedException("Scenario.Builder.Load() not implemented.");
            }

            public ScenarioObject CreateNewObject(NSUUIClass cls)
            {
                var obj = new ScenarioObject(cls);
                _uiObjects.Add(obj);
                return obj;
            }

            public int FindIndexByObject(object obj)
            {
                return _uiObjects.FindIndex(x => x.Object == obj);
            }

            public int FindIndexByUIObject(object obj)
            {
                return _uiObjects.FindIndex(x => x.UIObject == obj);
            }

        }
    }
}

