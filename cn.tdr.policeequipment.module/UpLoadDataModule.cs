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
        // 警员位置异常时间范围，取 +/- 值
        private static readonly long OfficerLocationExceptionTimeRange = 1L * 60 * 1000 * 10000; // 1 分钟

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
            var bdm = ExecuteEquipment(data);
            if (null != bdm)
            {
                ExecuteOfficer(data, bdm);
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
                lc.UpTime = data.TTime;
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
        private TagBindModel ExecuteEquipment(UpLoadDataPackage data)
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
            var handler = new EquipmentLocationHandle(Repository);
            var lc = handler.First(t => t.EquipId == model.Id);
            var add = (null == lc);
            // 首次进入基站
            if (add && !data.IsOut)
            {
                lc = new EquipmentLocation
                {
                    EquipId = model.Id,
                    SiteId = data.SiteId,
                    Status = (short)LocationStatus.In,
                    TagId = data.TagId,
                    UpTime = data.TTime
                };
                handler.Add(lc);
                return true;
            }

            // 进入基站，并且位置发生变化
            if (!add && !data.IsOut && lc.SiteId != data.SiteId)
            {
                lc.SiteId = data.SiteId;
                lc.Status = (short)LocationStatus.In;
                lc.TagId = data.TagId;
                lc.UpTime = data.TTime;
                handler.Add(lc);
                return true;
            }

            // 离开基站，并且当前位置状态依然是进入状态
            if (!add && data.IsOut && lc.SiteId == data.SiteId && lc.Status != (short)LocationStatus.Out)
            {
                lc.TagId = data.TagId;
                lc.UpTime = data.TTime;
                lc.Status = (short)LocationStatus.Out;
                handler.Modify(lc);
            }

            return false;
        }

        private void UpgradeEquipmentTrack(UpLoadDataPackage data, Equipment model, string preSiteId)
        {
            var track = new EquipmentMoveTrail
            {
                EquipId = model.Id,
                PreSiteId = preSiteId,
                SiteId = data.SiteId,
                UpTime = data.TTime
            };
            var handler = new EquipmentTrackHandle(Repository);
            handler.Add(track);
        }

        // 警员相关处理
        //  1，判断当前数据包是否为离开数据包，如果是，标识警员已经离开当前位置
        //  2，获取当前基站信息，并判断当前基站是否为出勤初始基站（警械库或者警械柜绑定基站），如果是，添加一条出勤记录。
        //  3，如果数据包是进入当前位置，获取警员当前位置，并判断警员位置是否发生变化，如果是，进一步判断当前变化是否在异常警告时间范围内（暂定 1 分钟），如果是，新增异常信息
        //  4，如果不是异常数据，变更警员的当前位置信息，并保存警员的轨迹位置
        //  5，其它操作
        private void ExecuteOfficer(UpLoadDataPackage data, TagBindModel bdm)
        {
            ExecuteOfficerLocation(data, bdm);
        }

        // 警员位置相关处理
        private void ExecuteOfficerLocation(UpLoadDataPackage data, TagBindModel bdm)
        {
            // 更新警员位置信息
            //  返回位置是否变更，并同时返回是否位置异常状态结果
            if (UpgradeOfficerLocation(data, bdm, out UpgradeOfficerLocationResult result))
            {
                UpgradeOfficerTrack(data, bdm.Officer, result.PreLocation?.SiteId);
            }

            // 警员位置异常处理
            if (result.IsLocationException)
            {
                ExecuteOfficerLocationException(data, bdm, result.PreLocation);
            }

            // 警员出勤处理
            ExecuteOfficerAttendance(data, bdm);
        }

        // 更新警员位置
        //  返回警员位置变化状态，并同时返回警员位置变化异常状态结果
        private bool UpgradeOfficerLocation(UpLoadDataPackage data, TagBindModel bdm, out UpgradeOfficerLocationResult result)
        {
            var handler = new OfficerLocationHandle(Repository);
            var lc = handler.First(t => t.OfficerId == bdm.Officer.Id);
            result = new UpgradeOfficerLocationResult { PreLocation = lc, IsLocationException = false };

            var isEmpty = (null == lc);
            // 首次进入基站
            if (isEmpty && !data.IsOut)
            {
                lc = new OfficerLocation { OfficerId = bdm.Officer.Id, EquipId=bdm.Equipment.Id, SiteId = data.SiteId, Status = (short)LocationStatus.In, UpTime = data.TTime };
                handler.Add(lc);
                return true;
            }

            // 进入基站，并且位置发生变化
            if (!isEmpty && !data.IsOut && data.SiteId != lc.SiteId)
            {
                // 警员位置异常范围最小值
                var tmin = lc.UpTime - OfficerLocationExceptionTimeRange;
                // 警员位置异常范围最大 值
                var tmax = lc.UpTime + OfficerLocationExceptionTimeRange;
                // 进一步判断是否发生在异常时间内，并标识是否发生异常
                result.IsLocationException = (tmin <= data.TTime && tmax >= data.TTime);
                if (result.IsLocationException)
                {
                    // 位置异常，中断执行后续处理，并触发位置异常处理
                    return false;
                }

                // 位置没有发生异常，继续执行后续处理
                lc.SiteId = data.SiteId;
                lc.EquipId = bdm.Equipment.Id;
                lc.Status = (short)LocationStatus.In;
                lc.UpTime = data.TTime;
                handler.Modify(lc);
                return true;
            }

            // 离开基站
            if (!isEmpty && data.IsOut && lc.SiteId == data.SiteId)
            {
                lc.EquipId = bdm.Equipment.Id;
                lc.Status = (short)LocationStatus.Out;
                lc.UpTime = data.TTime;
                handler.Modify(lc);
            }

            return false;
        }

        private void UpgradeOfficerTrack(UpLoadDataPackage data, Officer officer, string preSiteId)
        {
            var track = new OfficerMoveTrail { OfficerId = officer.Id, PreSiteId = preSiteId, SiteId = data.SiteId, UpTime = data.TTime };
            var handler = new OfficerTrackHandle(Repository);
            handler.Add(track);
        }

        // 警员位置异常处理
        //  当同一个警员的不同装备在异常时间范围内出现在不同的位置时，触发当前异常
        private void ExecuteOfficerLocationException(UpLoadDataPackage data, TagBindModel bdm, OfficerLocation location)
        {
            var expt = new EquipmentAllopatryExcept { CTime = DateTime.Now.ToUnixTime(), DTime = 0L, OfficerId = bdm.Officer.Id, Status = (short)OfficerLocaionExceptStatus.Doing };
            var epHandler = new EquipmentAllopatryExceptHandle(Repository);
            epHandler.Add(expt);

            var aepc = new AllopatryEquipmentPosition { AeId = expt.Id, EquipId = bdm.Equipment.Id, SiteId = data.SiteId, UpTime = data.TTime };
            var aepp = new AllopatryEquipmentPosition { AeId = expt.Id, EquipId = location.EquipId, SiteId = location.SiteId, UpTime = location.UpTime };
            var apHandler = new AllopatryEquipmentPositionHandle(Repository);
            apHandler.Add(aepc);
            apHandler.Add(aepp);
        }

        // 警员出勤模块处理
        //  1，查询当前警员所在组织机构的内部基站
        //  2，查询当前警员当天最近一次警员出勤情况，并做进一步处理
        private void ExecuteOfficerAttendance(UpLoadDataPackage data, TagBindModel bdm)
        {
            var noDel = (short)DeleteStatus.No;
            var orgId = bdm.Officer.OrgId;
            var siteId = data.SiteId;
            var officerId = bdm.Officer.Id;
            
            // 查询内部考勤基站是否存在
            var siteHandler = new StationHandle(Repository);
            var site = siteHandler.First(t => t.IsDel == noDel && t.OrgId == orgId && t.SiteId == siteId);

            // 查询当天最近一次未完成的考勤记录
            var today = DateTime.Now.Date.ToUnixTime();
            var atdHandler = new OfficerAttendanceHandle(Repository);
            var atd = atdHandler.First(t => t.OfficerId == officerId && t.STime >= today && t.ETime == 0L);

            // 离开内部考勤基站，签出
            if (null != site && data.IsOut && atd != null && atd.ETime == 0L)
            {
                atd.ETime = data.TTime;
                atd.TimeLength = atd.ETime - atd.STime;
                atdHandler.Modify(atd);
            }

            // 警员进入内部考勤基站，签到
            if (null != site && !data.IsOut && atd == null)
            {
                atd = new OfficerAttendance { OfficerId = officerId, STime = data.TTime, TimeLength = 0L, ETime = 0L };
                atdHandler.Add(atd);
            }

            // 重复签到
            if (null != site && !data.IsOut && atd != null && atd.ETime != 0L)
            {
                
            }
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

        /// <summary>
        /// 更新警员位置结果
        /// </summary>
        private class UpgradeOfficerLocationResult
        {
            /// <summary>
            /// 上一次位置
            /// </summary>
            public OfficerLocation PreLocation { get; set; }

            /// <summary>
            /// 是否位置异常
            /// </summary>
            public bool IsLocationException { get; set; }
        }
    }
}
