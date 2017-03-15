namespace cn.tdr.policeequipment.module
{
    using System;
    using System.Linq;
    using common;
    using data.entity;
    using enumerates;
    using handle;
    using models;

    /// <summary>
    /// 上报数据处理模块
    /// </summary>
    public class UpLoadDataModule : Module
    {
        /// <summary>
        /// 上报数据
        /// </summary>
        /// <param name="dataArray"></param>
        public void UpLoad(params UpLoadDataPackage[] dataArray)
        {
            dataArray.Where(t => !string.IsNullOrWhiteSpace(t?.TagId)).All(
                t =>
                {
                    ExecuteCore(t);
                    return true;
                });
        }

        // 核心处理
        private void ExecuteCore(UpLoadDataPackage data)
        {
            ExecuteTagLocation(data);
            var bdm = ExecuteEquipmentLocation(data);
            if (null != bdm)
            {
                ExecuteOfficerLocation(data, bdm);
            }
            
            // 提交事务
            Repository.Commit();
        }
        
        // 标签相关处理
        //  1，标签当前位置信息处理        
        //  2，保存标签移动轨迹
        private void ExecuteTagLocation(UpLoadDataPackage data)
        {
            if (UpgradeTagLocation(data, out string preSiteId))
            {
                UpgradeTagTack(data, preSiteId);
            }
        }

        // 更新标签位置，并返回变更状态。true 标识位置发生变化或者首次记录，并返回上一次位置的基站标识
        private bool UpgradeTagLocation(UpLoadDataPackage data, out string preSiteId)
        {
            preSiteId = null;
            var handler = new TagLocationHandle(Repository);
            var lc = handler.First(t => t.TagId == data.TagId);
            var isAdd = (null == lc);

            // 没有之前的记录，并且标签进入基站
            if (isAdd && !data.IsOut)
            {
                lc = new TagLocation
                {
                    SiteId = data.SiteId,
                    Status = (short)(data.IsOut ? LocationStatus.Out : LocationStatus.In),
                    TagId = data.TagId,
                    UpTime = data.TTime
                };
                handler.Add(lc);
                return true;
            }

            // 标签进入基站，并且位置发生变化
            if (!isAdd && !data.IsOut && lc.SiteId != data.SiteId)
            {
                preSiteId = lc.SiteId;

                // 更新当前时间
                lc.SiteId = data.SiteId;
                lc.Status = (short)LocationStatus.In;
                lc.UpTime = data.TTime;
                handler.Modify(lc);

                return true;
            }

            // 标签离开当前基站
            if (!isAdd && data.IsOut && lc.SiteId == data.SiteId)
            {
                lc.Status = (short)LocationStatus.Out;
                handler.Modify(lc);
            }

            return false;
        }

        // 更新标签的移动轨迹
        private void UpgradeTagTack(UpLoadDataPackage data, string preSiteId)
        {
            var track = new TagMoveTrail
            {
                CTime = DateTime.Now.ToUnixTime(),
                PreSiteId = preSiteId,
                PTime = data.PTime,
                SiteId = data.SiteId,
                TagId = data.TagId,
                TTime = data.TTime
            };
            var handler = new TagTrackHandle(Repository);
            handler.Add(track);
        }

        // 警械相关处理
        //  1，获取标签绑定的警械信息
        //  2，警械当前位置信息处理
        //  3，保存警械移动轨迹
        //  4，其它
        private TagBindModel ExecuteEquipmentLocation(UpLoadDataPackage data)
        {
            var eqHandler = new EquipmentHandle(Repository);
            var offHandler = new OfficerHandle(Repository);
            var noDel = (short)DeleteStatus.No;
            var query =
                from eqt in eqHandler.All(t => t.IsDel == noDel)
                join officer in offHandler.All(t => t.IsDel == noDel) on eqt.OfficerId equals officer.Id
                select new { eqt = eqt, officer = officer };
            var tagId = data.TagId;
            var m = query.Where(t => t.eqt.TagId == tagId).FirstOrDefault();
            if (null != m)
            {
                var bdm = new TagBindModel { Equipment = m.eqt, Officer = m.officer };
                ExecuteEquipmentLocation(data, bdm);
                return bdm;
            }
            return null;
        }

        // 警械相关处理核心
        private void ExecuteEquipmentLocation(UpLoadDataPackage data, TagBindModel model)
        {
            ExecuteEquipmentLocation(data, model.Equipment);
        }

        private void ExecuteEquipmentLocation(UpLoadDataPackage data, Equipment model)
        {
            if (UpgradeEquipmentLocation(data, model, out string preSiteId))
            {
                UpgradeEquipmentTrack(data, model, preSiteId);
            }
        }

        private bool UpgradeEquipmentLocation(UpLoadDataPackage data, Equipment model, out string preSiteId)
        {
            preSiteId = null;
            return false;
        }

        private void UpgradeEquipmentTrack(UpLoadDataPackage data, Equipment model, string preSiteId)
        {

        }

        // 警员相关处理
        //  1，判断当前数据包是否为离开数据包，如果是，标识警员已经离开当前位置
        //  2，获取当前基站信息，并判断当前基站是否为出勤初始基站（警械库或者警械柜绑定基站），如果是，添加一条出勤记录。
        //  3，如果数据包是进入当前位置，获取警员当前位置，并判断警员位置是否发生变化，如果是，进一步判断当前变化是否在异常警告时间范围内（暂定 1 分钟），如果是，新增异常信息
        //  4，如果不是异常数据，变更警员的当前位置信息，并保存警员的轨迹位置
        //  5，其它操作
        private void ExecuteOfficerLocation(UpLoadDataPackage data, TagBindModel equipment)
        {

        }

        /// <summary>
        /// 标签绑定模型
        /// </summary>
        private class TagBindModel
        {
            /// <summary>
            /// 警械
            /// </summary>
            public Equipment Equipment { get; set; }

            /// <summary>
            /// 警员
            /// </summary>
            public Officer Officer { get; set; }
        }
    }
}
