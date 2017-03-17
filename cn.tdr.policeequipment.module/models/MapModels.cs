namespace cn.tdr.policeequipment.module.models
{
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
        public Station station { get; set; }

        public OfficerAttendance atd { get; set; }

        public Officer officer { get; set; }

        public PoliceType ptp { get; set; }

        public Organization org { get; set; }

        public OfficerMoveTrail[] tracks { get; set; }
    }
}
