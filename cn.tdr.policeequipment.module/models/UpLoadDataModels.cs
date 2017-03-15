namespace cn.tdr.policeequipment.module.models
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Xml;
    using common;

    public class UpLoadDataPackage
    {
        /// <summary>
        /// 离开基站延时发送时间间隔
        /// </summary>
        private static readonly long OutDelayInterval = 1L * 60 * 1000 * 10000; // 1 分钟

        public UpLoadDataPackage(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            Init(doc);
        }

        public UpLoadDataPackage(XmlNode node)
        {
            Init(node);
        }

        private void Init(XmlNode node)
        {
            DataPackage = node;
            EqType = node.SelectSingleNode("Data/EQType")?.InnerText;
            CmdId = node.SelectSingleNode("Data/CommandID")?.InnerText;

            var stime = node.SelectSingleNode("Data/Time")?.InnerText;
            var sdate = DateTime.Now;
            PTime = 0L;
            if (DateTime.TryParse(stime, out sdate))
            {
                PTime = sdate.ToUnixTime();
            }

            SiteId = node.SelectSingleNode("Data/EQID")?.InnerText;
            var sbuff = node.SelectSingleNode("Data/Content")?.InnerText;
            Data = new byte[0];
            if (!string.IsNullOrWhiteSpace(sbuff))
            {
                var pattern = "[0-9A-Fa-f]{2}";
                var items = Regex.Matches(sbuff, pattern);
                var arr = new List<byte>();
                for (int i = 0; i < items.Count; i++)
                {
                    byte val = 0x00;
                    if (byte.TryParse(items[i].Value, out val))
                    {
                        arr.Add(val);
                    }
                }

                Data = arr.ToArray();
            }
        }

        /// <summary>
        /// 正文数据包
        /// </summary>
        public XmlNode DataPackage { get; private set; }

        /// <summary>
        /// 基站设备类型，暂时不用
        /// </summary>
        public string EqType { get; private set; }

        /// <summary>
        /// 基站数据命令字，只需解析F003的命令字
        /// </summary>
        public string CmdId { get; private set; }

        /// <summary>
        /// 数据平台接收数据包时间
        /// </summary>
        public long PTime { get; private set; }

        /// <summary>
        /// 基站设备编号
        /// </summary>
        public string SiteId { get; private set; }

        /// <summary>
        /// 数据正文
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// 标签最后一次上报时间
        /// </summary>
        public long TTime
        {
            get
            {
                var dtime = 0L;
                if (6 < Data.Length)
                {
                    var year = Data[0] + 2000;
                    var month = (int)Data[1];
                    var day = (int)Data[2];
                    var hour = (int)Data[3];
                    var min = (int)Data[4];
                    var sec = (int)Data[5];
                    var time = new DateTime(year, month, day, hour, min, sec);
                    dtime = time.ToUnixTime();
                }
                return dtime;
            }
        }

        /// <summary>
        /// SN 编号
        /// </summary>
        public string SerialNumber
        {
            get
            {
                if (7 > Data.Length)
                {
                    return null;
                }

                return $"{(int)Data[6]}";
            }
        }

        /// <summary>
        /// 标签设备类型编号
        /// </summary>
        public string TagType
        {
            get
            {
                if (9 > Data.Length)
                {
                    return null;
                }

                var tp = Data[7] << 8;
                tp |= Data[8];
                return $"{tp}";
            }
        }

        /// <summary>
        /// 区域编号
        /// </summary>
        public string Area
        {
            get
            {
                if (10 > Data.Length)
                {
                    return null;
                }

                return $"{(int)Data[9]}";
            }
        }

        /// <summary>
        /// 标签编号
        /// </summary>
        public string TagId
        {
            get
            {
                if (13 > Data.Length)
                {
                    return null;
                }

                var id = Data[10] >> 8;
                id |= Data[11];
                id >>= 8;
                id |= Data[12];

                return $"{id}";
            }
        }

        /// <summary>
        /// 标签上报数据命令字
        /// </summary>
        public string TagCmdId
        {
            get
            {
                if (14 > Data.Length)
                {
                    return null;
                }
                return $"{(int)Data[13]}";
            }
        }

        /// <summary>
        /// 省份编号
        /// </summary>
        public string ProvinceId
        {
            get
            {
                if (15 > Data.Length)
                {
                    return null;
                }

                return $"{(int)Data[14]}";
            }
        }

        /// <summary>
        /// 城市编号
        /// </summary>
        public string CityId
        {
            get
            {
                if (16 > Data.Length)
                {
                    return null;
                }

                return $"{Data[15]}";
            }
        }

        /// <summary>
        /// 是否离开。true 标识离开；否则，标识进入
        /// </summary>
        public bool IsOut
        {
            get
            {
                if (0 < PTime && 0 < TTime)
                {
                    return OutDelayInterval <= (PTime - TTime);
                }

                return false;
            }
        }
    }
}
