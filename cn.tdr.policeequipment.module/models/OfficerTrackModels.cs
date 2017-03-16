namespace cn.tdr.policeequipment.module.models
{
    using data.entity;

    public class OfficerTrackModel
    {
        public OfficerMoveTrail track { get; set; }

        public Officer officer { get; set; }

        public Organization org { get; set; }
    }
}
