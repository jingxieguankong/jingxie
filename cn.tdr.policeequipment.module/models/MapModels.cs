namespace cn.tdr.policeequipment.module.models
{
    using System.Collections.Generic;
    using data.entity;

    public class OfficerDispatchQueryModel
    {
        public Officer officer { get; set; }

        public OfficerLocation location { get; set; }

        public Station station { get; set; }

        public PoliceType ptp { get; set; }

        public Organization org { get; set; }
    }

    public class OfficerAttendanceQueryModel
    {
        public OfficerAttendance atd { get; set; }

        public Officer officer { get; set; }

        public PoliceType ptp { get; set; }

        public Organization org { get; set; }

        public IEnumerable<OfficerAttendanceTrackModel> tracks { get; set; }
    }

    public class OfficerAttendanceTrackModel
    {
        public Station station { get; set; }

        public OfficerMoveTrail track { get; set; }
    }
}
