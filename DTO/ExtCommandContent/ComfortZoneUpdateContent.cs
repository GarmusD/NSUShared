namespace NSU.Shared.DTO.ExtCommandContent
{
    public struct ComfortZoneUpdateContent
    {
        public byte ConfigPos { get; set; }
        public double? RoomTempHi { get; set; }
        public double? RoomTempLo { get; set; }
        public double? FloorTemoHi { get; set; }
        public double? FloorTempLo { get; set; }
        public bool? LowTempMode { get; set; }

        public ComfortZoneUpdateContent(byte configPos, double? roomTempHi, double? roomTempLo, double? floorTemoHi, double? floorTempLo, bool? lowTempMode)
        {
            ConfigPos = configPos;
            RoomTempHi = roomTempHi;
            RoomTempLo = roomTempLo;
            FloorTemoHi = floorTemoHi;
            FloorTempLo = floorTempLo;
            LowTempMode = lowTempMode;
        }
    }
}
