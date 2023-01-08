using System;
using System.Xml.Linq;

namespace NSU.Shared.NSUXMLConfig
{
    public enum ConfigSection
    {
        ConfigID,
        Switches,
        TSensors,
        RelayModules,
        TempTriggers,
        CirculationPumps,
        Collectors,
        ComfortZones,
        KTypes,
        WaterBoilers,
        WoodBoilers
    };

    public class NSUXMLConfig
    {
        private readonly string LogTag = "NSUXMLConfig";
        private readonly string xRootName = "NSUConfig";
        private readonly string xConfigID = "ConfigID";
        private readonly string xSwitches = "Switches";
        private readonly string xTSensors = "TSensors";
        private readonly string xRelayModules = "RelayModules";
        private readonly string xTempTriggers = "TempTriggers";
        private readonly string xCircPumps = "CirculationPumps";
        private readonly string xCollectors = "Collectors";
        private readonly string xComfortZones = "ComfortZones";
        private readonly string xKTypes = "KTypes";
        private readonly string xWaterBoilers = "WaterBoilers";
        private readonly string xWoodBoilers = "WoodBoilers";

        XDocument xdoc;
        XElement root;

        public Guid ConfigID
        {
            get { return configID; }
            set
            {
                configID = value;
                var cfgIdSection = GetConfigSection(ConfigSection.ConfigID);
                if (cfgIdSection != null)
                    cfgIdSection.Element("Value").Value = configID.ToString();
            }
        }
        private Guid configID = Guid.Empty;

        public NSUXMLConfig()
        {
            xdoc = CreateNew();
            root = xdoc.Root;
            configID = Guid.Parse(GetConfigSection(ConfigSection.ConfigID).Element("Value").Value);
        }

        public bool Load(string xmlString)
        {
            try
            {
                xdoc = XDocument.Parse(xmlString);
                root = xdoc.Root;
                configID = Guid.Parse(GetConfigSection(ConfigSection.ConfigID).Element("Value").Value);
                return true;
            }
            catch (Exception ex)
            { 
                //if(System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debug.WriteLine($"NSUXMLConfig Load exception: {ex}");
                return false; 
            }
        }

        public void Clear()
        {
            xdoc = CreateNew();
            root = xdoc.Root;
            configID = Guid.Parse(GetConfigSection(ConfigSection.ConfigID).Element("Value").Value);
        }

        private XDocument CreateNew()
        {
            var xDoc = new XDocument();
            xDoc.Add(
                new XElement(xRootName,
                    new XElement(xConfigID,
                        new XElement("Value", Guid.Empty.ToString())
                    ),
                    new XElement(xSwitches),
                    new XElement(xTSensors),
                    new XElement(xRelayModules),
                    new XElement(xTempTriggers),
                    new XElement(xCircPumps),
                    new XElement(xCollectors),
                    new XElement(xComfortZones),
                    new XElement(xKTypes),
                    new XElement(xWaterBoilers),
                    new XElement(xWoodBoilers)
                )
            );
            return xDoc;
        }

        public XElement GetConfigSection(ConfigSection section)
        {
            return section switch
            {
                ConfigSection.ConfigID => root.Element(xConfigID),
                ConfigSection.Switches => root.Element(xSwitches),
                ConfigSection.TSensors => root.Element(xTSensors),
                ConfigSection.RelayModules => root.Element(xRelayModules),
                ConfigSection.TempTriggers => root.Element(xTempTriggers),
                ConfigSection.CirculationPumps => root.Element(xCircPumps),
                ConfigSection.Collectors => root.Element(xCollectors),
                ConfigSection.ComfortZones => root.Element(xComfortZones),
                ConfigSection.KTypes => root.Element(xKTypes),
                ConfigSection.WaterBoilers => root.Element(xWaterBoilers),
                ConfigSection.WoodBoilers => root.Element(xWoodBoilers),
                _ => throw new NotImplementedException($"XML Config section [{section}] not implemented."),
            };
        }

        public string GetXDocAsString()
        {
            return xdoc.ToString(SaveOptions.DisableFormatting);
        }

    }

}

