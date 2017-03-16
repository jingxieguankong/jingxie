namespace cn.tdr.policeequipment.module.models
{
    using data.entity;

    public class AttendanceModel
    {
        public Officer officer { get; set; }
        
        public OfficerAttendance attendance { get; set; }

        public Organization org { get; set; }
    }
}
