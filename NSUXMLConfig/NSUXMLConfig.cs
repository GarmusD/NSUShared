using System;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using Serilog;

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

        private readonly ILogger _logger;

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

        public NSUXMLConfig(ILogger logger)
        {
            _logger = logger.ForContext<NSUXMLConfig>() ?? throw new ArgumentNullException(nameof(logger), "Instance of ILogger cannot be null.");
            xdoc = CreateNew();
            root = xdoc.Root;
            configID = Guid.Parse(GetConfigSection(ConfigSection.ConfigID).Element("Value").Value);
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
            switch (section)
            {
                case ConfigSection.ConfigID:
                    return root.Element(xConfigID);
                case ConfigSection.Switches:
                    return root.Element(xSwitches);
                case ConfigSection.TSensors:
                    return root.Element(xTSensors);
                case ConfigSection.RelayModules:
                    return root.Element(xRelayModules);
                case ConfigSection.TempTriggers:
                    return root.Element(xTempTriggers);
                case ConfigSection.CirculationPumps:
                    return root.Element(xCircPumps);
                case ConfigSection.Collectors:
                    return root.Element(xCollectors);
                case ConfigSection.ComfortZones:
                    return root.Element(xComfortZones);
                case ConfigSection.KTypes:
                    return root.Element(xKTypes);
                case ConfigSection.WaterBoilers:
                    return root.Element(xWaterBoilers);
                case ConfigSection.WoodBoilers:
                    return root.Element(xWoodBoilers);
                default:
                    _logger.Debug(LogTag, "XML section NOT found.");
                    throw new NotImplementedException($"XML Config section [{section}] not implemented.");
            }
        }

        public string GetXDocAsString()
        {
            return xdoc.ToString();
        }

    }

}

