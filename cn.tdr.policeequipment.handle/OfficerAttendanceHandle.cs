namespace cn.tdr.policeequipment.handle
{
    using cn.tdr.policeequipment.interfaces;
    using data.entity;

    public class OfficerAttendanceHandle : Handle<OfficerAttendance>
    {
        public OfficerAttendanceHandle(IRepository repository) : base(repository)
        {
        }
    }
}
