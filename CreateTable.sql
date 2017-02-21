drop table "tb_AllopatryEquipmentPosition" cascade constraints;

drop table "tb_Cabinet" cascade constraints;

drop table "tb_DispatchEquipmentPosition" cascade constraints;

drop table "tb_EqtCategory" cascade constraints;

drop table "tb_Equipment" cascade constraints;

drop table "tb_EquipmentAllopatryExcept" cascade constraints;

drop table "tb_EquipmentDispatch" cascade constraints;

drop table "tb_Hit" cascade constraints;

drop table "tb_Officer" cascade constraints;

drop table "tb_Organization" cascade constraints;

drop table "tb_PoliceType" cascade constraints;

drop table "tb_StandardEquipment" cascade constraints;

drop table "tb_Station" cascade constraints;

drop table "tb_StockAlert" cascade constraints;

drop table "tb_Storage" cascade constraints;

drop table "tb_TagMoveTrail" cascade constraints;

drop table "tb_sys_ActionLog" cascade constraints;

drop table "tb_sys_Feature" cascade constraints;

drop table "tb_sys_Menu" cascade constraints;

drop table "tb_sys_Role" cascade constraints;

drop table "tb_sys_User" cascade constraints;

/*==============================================================*/
/* Table: "tb_AllopatryEquipmentPosition"                       */
/*==============================================================*/
create table "tb_AllopatryEquipmentPosition" 
(
   "Id"                 varchar(32)          not null,
   "AeId"               varchar(32)          not null,
   "EquipId"            varchar(32)          not null,
   "Lon"                number(15,8)         not null,
   "Lat"                number(15,8)         not null,
   constraint PK_TB_ALLOPATRYEQUIPMENTPOSITI primary key ("Id")
);

comment on table "tb_AllopatryEquipmentPosition" is
'警员警械异地异常警械位置信息';

comment on column "tb_AllopatryEquipmentPosition"."Id" is
'主键';

comment on column "tb_AllopatryEquipmentPosition"."AeId" is
'关联警员警械异地异常主键，描述当前警械位置变化隶属于那一次警械异地变化异常';

comment on column "tb_AllopatryEquipmentPosition"."EquipId" is
'关联警械主键，标识当前位置出现的警械';

comment on column "tb_AllopatryEquipmentPosition"."Lon" is
'描述警械出现位置的 GPS 经度';

comment on column "tb_AllopatryEquipmentPosition"."Lat" is
'描述警械出现位置的 GPS 维度';

/*==============================================================*/
/* Table: "tb_Cabinet"                                          */
/*==============================================================*/
create table "tb_Cabinet" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "OrgId"              varchar(32)          not null,
   "Name"               varchar(128)         not null,
   "StationId"          varchar(32)          not null,
   "Lon"                number(15,8)         not null,
   "Lat"                number(15,8)         not null,
   "OfficerId"          varchar(32)          default '0',
   constraint PK_TB_CABINET primary key ("Id")
);

comment on table "tb_Cabinet" is
'警械柜的内容，每个组织机构有零个或者多个警械柜';

comment on column "tb_Cabinet"."Id" is
'主键';

comment on column "tb_Cabinet"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_Cabinet"."OrgId" is
'关联组织机构表主键，标识所属组织机构';

comment on column "tb_Cabinet"."Name" is
'名称';

comment on column "tb_Cabinet"."StationId" is
'基站主键';

comment on column "tb_Cabinet"."Lon" is
'GPS 经度';

comment on column "tb_Cabinet"."Lat" is
'GPS 纬度';

comment on column "tb_Cabinet"."OfficerId" is
'警械柜负责人，关联警员表主键，NULL 或者 “0” 标识未指定负责警员';

/*==============================================================*/
/* Table: "tb_DispatchEquipmentPosition"                        */
/*==============================================================*/
create table "tb_DispatchEquipmentPosition" 
(
   "Id"                 varchar(32)          not null,
   "AeId"               varchar(32)          not null,
   "EquipId"            varchar(32)          not null,
   "Lon"                number(15,8)         not null,
   "Lat"                number(15,8)         not null,
   "HdStatus"           number(4,0)          not null,
   "SiteId"             varchar(32)          not null,
   "PtStatus"           number(4,0)          not null,
   constraint PK_TB_DISPATCHEQUIPMENTPOSITIO primary key ("Id")
);

comment on table "tb_DispatchEquipmentPosition" is
'警械布控警械位置信息。当警械在布控的过程中，被布控的警械发生任何位置变化时，应将警械发生的位置变化添加到当前列表';

comment on column "tb_DispatchEquipmentPosition"."Id" is
'主键';

comment on column "tb_DispatchEquipmentPosition"."AeId" is
'关联警械布控主键，描述当前位置变化隶属于那一次警械布控';

comment on column "tb_DispatchEquipmentPosition"."EquipId" is
'关联警械主键，描述当前位置出现的警械';

comment on column "tb_DispatchEquipmentPosition"."Lon" is
'描述警械出现位置的 GPS 经度';

comment on column "tb_DispatchEquipmentPosition"."Lat" is
'描述警械出现位置的 GPS 维度';

comment on column "tb_DispatchEquipmentPosition"."HdStatus" is
'描述当前内容的处理状态。0：未处理；1：已处理。';

comment on column "tb_DispatchEquipmentPosition"."SiteId" is
'关联基站主键，标识当前警械出现的基站范围';

comment on column "tb_DispatchEquipmentPosition"."PtStatus" is
'描述警械进入或者离开当前位置。1：进入；2：离开。';

/*==============================================================*/
/* Table: "tb_EqtCategory"                                      */
/*==============================================================*/
create table "tb_EqtCategory" 
(
   "Id"                 varchar(32)          not null,
   "Name"               varchar(128)         not null,
   "Code"               varchar(32)          not null,
   "Pid"                varchar(32)          default '0' not null,
   "Layer"              number(4,0)          not null,
   "IsDel"              number(4,0)          not null,
   "HitCount"           number(4,0)          not null,
   constraint PK_TB_EQTCATEGORY primary key ("Id")
);

comment on table "tb_EqtCategory" is
'描述警械的类型信息';

comment on column "tb_EqtCategory"."Name" is
'当前类型名称';

comment on column "tb_EqtCategory"."Code" is
'当前类型编码。规则：为父级代码+当前类型代码';

comment on column "tb_EqtCategory"."Pid" is
'自引用外键，上一级类型主键';

comment on column "tb_EqtCategory"."Layer" is
'标识当前类型在类型关系树中的位置，总是等于上一级层级数 + 1';

comment on column "tb_EqtCategory"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_EqtCategory"."HitCount" is
'强撞击次数。这是一个大于 0 的值，0 ：不限制，其它表示最大被允许撞击的次数。';

/*==============================================================*/
/* Table: "tb_Equipment"                                        */
/*==============================================================*/
create table "tb_Equipment" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "OrgId"              varchar(32)          not null,
   "LibId"              varchar(32)          not null,
   "CabId"              varchar(32),
   "CateId"             varchar(32)          not null,
   "OfficerId"          varchar(32),
   "Model"              varchar(128),
   "Factory"            varchar(128),
   "FactorCode"         varchar(128),
   "TagId"              varchar(128),
   "InputTime"          number(18,0)         not null,
   "Status"             number(4,0)          not null,
   "Power"              number(4,0)          not null,
   "IsLost"             number(4,0)          not null,
   "IsChanged"          number(4,0)          not null,
   "ChangeTime"         number(18,0)         not null,
   "FactorTime"         number(18,0)         not null,
   "ExpiredTime"        number(18,0)         not null,
   "PurchaseTime"       number(18,0)         not null,
   "Dispatched"         number(4,0)          not null,
   constraint PK_TB_EQUIPMENT primary key ("Id")
);

comment on table "tb_Equipment" is
'警械装备内容';

comment on column "tb_Equipment"."Id" is
'主键';

comment on column "tb_Equipment"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_Equipment"."OrgId" is
'关联组织机构表主键，标识当前警械的归属组织机构';

comment on column "tb_Equipment"."LibId" is
'关联警械库表主键，标识警械装备存放的警械库';

comment on column "tb_Equipment"."CabId" is
'关联警械柜的主键，当警械装备被分配为警员，并且当前警员有绑定警械柜时，才有值；否则为 NULL';

comment on column "tb_Equipment"."CateId" is
'关联警械类型表主键，标识警械装备的类型';

comment on column "tb_Equipment"."OfficerId" is
'关联警员表的主键，当前警械装备被标识使用时，才有值；否则，为 NULL';

comment on column "tb_Equipment"."Model" is
'警械装备的型号';

comment on column "tb_Equipment"."Factory" is
'警械装备的生产厂家';

comment on column "tb_Equipment"."FactorCode" is
'警械装备的出厂编码';

comment on column "tb_Equipment"."TagId" is
'与警械装备绑定的标签';

comment on column "tb_Equipment"."InputTime" is
'警械装备的入库时间';

comment on column "tb_Equipment"."Status" is
'警械状态。0：标识正常；-1：标识异常；-2：标识损坏；';

comment on column "tb_Equipment"."Power" is
'标签电池电量';

comment on column "tb_Equipment"."IsLost" is
'是否遗失。0：未遗失；1：遗失。';

comment on column "tb_Equipment"."IsChanged" is
'是否已更换。0：未更换，1：已更换。';

comment on column "tb_Equipment"."ChangeTime" is
'更换时间';

comment on column "tb_Equipment"."FactorTime" is
'生产时间';

comment on column "tb_Equipment"."ExpiredTime" is
'过期时间';

comment on column "tb_Equipment"."PurchaseTime" is
'采购时间';

comment on column "tb_Equipment"."Dispatched" is
'布控状态。0：从未进行过布控，1：正在布控，2：已撤控。';

/*==============================================================*/
/* Table: "tb_EquipmentAllopatryExcept"                         */
/*==============================================================*/
create table "tb_EquipmentAllopatryExcept" 
(
   "Id"                 varchar(32)          not null,
   "OfficerId"          varchar(32)          not null,
   "CTime"              number(18,0)         not null,
   "Status"             number(4,0)          not null,
   constraint PK_TB_EQUIPMENTALLOPATRYEXCEPT primary key ("Id")
);

comment on table "tb_EquipmentAllopatryExcept" is
'警员警械异地异常';

comment on column "tb_EquipmentAllopatryExcept"."Id" is
'主键';

comment on column "tb_EquipmentAllopatryExcept"."OfficerId" is
'关联警员主键，标识被布控的警员，同时标识警员布控';

comment on column "tb_EquipmentAllopatryExcept"."CTime" is
'描述警械布控或者撤控发生的时间';

comment on column "tb_EquipmentAllopatryExcept"."Status" is
'描述当前异常的处理状态。0：未处理；1：已处理。';

/*==============================================================*/
/* Table: "tb_EquipmentDispatch"                                */
/*==============================================================*/
create table "tb_EquipmentDispatch" 
(
   "Id"                 varchar(32)          not null,
   "EquipId"            varchar(32)          not null,
   "OfficerId"          varchar(32)          default '0' not null,
   "DispatchTime"       number(18,0)         not null,
   "IsCancel"           number(4,0)          not null,
   "CancelTime"         number(18,0)         not null,
   "CancelMsg"          varchar(1024),
   constraint PK_TB_EQUIPMENTDISPATCH primary key ("Id")
);

comment on table "tb_EquipmentDispatch" is
'警械或者警员布控记录，没发生一条布控或者撤控时，当前列表需要添加一条记录';

comment on column "tb_EquipmentDispatch"."Id" is
'主键';

comment on column "tb_EquipmentDispatch"."EquipId" is
'关联警械ID，标识布控的警械';

comment on column "tb_EquipmentDispatch"."OfficerId" is
'关联警员主键，标识被布控的警员，同时标识警员布控';

comment on column "tb_EquipmentDispatch"."DispatchTime" is
'描述警械布控或者撤控发生的时间';

comment on column "tb_EquipmentDispatch"."IsCancel" is
'是否撤控。0：未撤控；1：已撤控。';

comment on column "tb_EquipmentDispatch"."CancelTime" is
'撤控时间';

comment on column "tb_EquipmentDispatch"."CancelMsg" is
'撤控原因';

/*==============================================================*/
/* Table: "tb_Hit"                                              */
/*==============================================================*/
create table "tb_Hit" 
(
   "Id"                 varchar(32)          not null,
   "TagId"              varchar(32)          not null,
   "SiteId"             varchar(32)          not null,
   "UpTime"             number(18,0)         not null,
   "CTime"              number(18,0)         not null,
   constraint PK_TB_HIT primary key ("Id")
);

comment on table "tb_Hit" is
'标签上报撞击信息记录';

comment on column "tb_Hit"."Id" is
'主键';

comment on column "tb_Hit"."TagId" is
'描述当前记录隶属于哪一个标签';

comment on column "tb_Hit"."SiteId" is
'描述当前标签在哪一个位置的基站范围内';

comment on column "tb_Hit"."UpTime" is
'描述基站上传标签的时间，标签的运动位置的排序应该以当前字段为准';

comment on column "tb_Hit"."CTime" is
'描述系统记录当前标签位置信息的时间';

/*==============================================================*/
/* Table: "tb_Officer"                                          */
/*==============================================================*/
create table "tb_Officer" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "OrgId"              varchar(32)          not null,
   "PtId"               varchar(32)          not null,
   "CabId"              varchar(32),
   "Name"               varchar(128)         not null,
   "Sex"                number(4,0)          not null,
   "IdentyCode"         varchar(32)          not null,
   "Tel"                varchar(64),
   "AtrUrl"             varchar(256),
   "DigitalID"          varchar(128),
   "SignupDate"         number(18,0)         not null,
   "UserId"             varchar(32)          not null,
   constraint PK_TB_OFFICER primary key ("Id")
);

comment on table "tb_Officer" is
'描述警员的详细信息';

comment on column "tb_Officer"."Id" is
'主键';

comment on column "tb_Officer"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_Officer"."OrgId" is
'管理组织机构表主键，标识警员所属组织机构';

comment on column "tb_Officer"."PtId" is
'关联警种表主键，标识警员所属的警种类型';

comment on column "tb_Officer"."CabId" is
'关联警械柜表主键。标识警员所属警械柜，NULL 标识该警员没有指明警械柜';

comment on column "tb_Officer"."Name" is
'警员姓名';

comment on column "tb_Officer"."Sex" is
'性别。0：标识 “ 女性 ”；1：标识 ” 男性 “；2：标识 “ 其它 ”';

comment on column "tb_Officer"."IdentyCode" is
'警员编号代码';

comment on column "tb_Officer"."Tel" is
'警员绑定的手机号码。与登录密码结合，支持系统登录';

comment on column "tb_Officer"."AtrUrl" is
'用户头像 URL 地址';

comment on column "tb_Officer"."DigitalID" is
'绑定的警员数字证书。提供警员登录系统的另一种方式，使用数字证书认证登录，需要校验数字证书的一致性，不适用登录密码。同时在验证之前需要校验账户的状态，在账户处于“ 异常 ”状态时，数据一律无效';

comment on column "tb_Officer"."SignupDate" is
'警员账户创建时间，这是一个精确到秒级的时间戳';

comment on column "tb_Officer"."UserId" is
'当前警员绑定的登录账户信息';

/*==============================================================*/
/* Table: "tb_Organization"                                     */
/*==============================================================*/
create table "tb_Organization" 
(
   "Id"                 varchar(32)          not null,
   "Name"               varchar(128)         not null,
   "Code"               varchar(128)         not null,
   "Pid"                varchar(32)          default '0' not null,
   "Layer"              number(4,0)          not null,
   "IsDel"              number(4,0)          not null,
   constraint PK_TB_ORGANIZATION primary key ("Id")
);

comment on table "tb_Organization" is
'组织机构内容，系统的数据边界是根据组织结构来划分的，本组织机构只能访问本组织机构的数据，其它组织机构是禁止访问的。但是当用户为超级管理员时，数据边界是无效的，也就是说超级管理员可以访问任何平台内的任何数据，因此谨慎保护好超级管理员账户';

comment on column "tb_Organization"."Id" is
'组织机构主键标识';

comment on column "tb_Organization"."Name" is
'组织机构名称';

comment on column "tb_Organization"."Code" is
'组织机构代码，总是等于上一级组织机构代码+当前组织机构代码';

comment on column "tb_Organization"."Pid" is
'自引用外键，上一级组织机构主键，“0” 标识顶级';

comment on column "tb_Organization"."Layer" is
'标识当前组织机构在组织机构树种的位置，它总是上一级组织机构的层级 + 1';

comment on column "tb_Organization"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除；';

/*==============================================================*/
/* Table: "tb_PoliceType"                                       */
/*==============================================================*/
create table "tb_PoliceType" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "OrgId"              varchar(32)          not null,
   "Name"               varchar(128)         not null,
   constraint PK_TB_POLICETYPE primary key ("Id")
);

comment on column "tb_PoliceType"."Id" is
'主键';

comment on column "tb_PoliceType"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_PoliceType"."OrgId" is
'组织机构主键';

comment on column "tb_PoliceType"."Name" is
'名称';

/*==============================================================*/
/* Table: "tb_StandardEquipment"                                */
/*==============================================================*/
create table "tb_StandardEquipment" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "PtId"               varchar(32)          not null,
   "CateId"             varchar(32)          not null,
   "Num"                number(4,0)          default 0 not null,
   "IsPrimary"          number(4,0)          not null,
   "IsRequire"          number(4,0)          not null,
   constraint PK_TB_STANDARDEQUIPMENT primary key ("Id")
);

comment on table "tb_StandardEquipment" is
'描述警种的标准警械装备内容';

comment on column "tb_StandardEquipment"."Id" is
'主键';

comment on column "tb_StandardEquipment"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_StandardEquipment"."PtId" is
'关联警种表主键，标识所属警种类型';

comment on column "tb_StandardEquipment"."CateId" is
'关联警械类型表主键，标识警械装备的类型';

comment on column "tb_StandardEquipment"."Num" is
'描述所属装备类型的应该装备的数量，默认值为 0';

comment on column "tb_StandardEquipment"."IsPrimary" is
'标识所属的装备类型为主要装备，主要装备总是必须装备的。0：标识非主装备；1：标识主装备';

comment on column "tb_StandardEquipment"."IsRequire" is
'标识所属的装备类型装备是否必须装备。0：非必须装备；1：标识必须装备；';

/*==============================================================*/
/* Table: "tb_Station"                                          */
/*==============================================================*/
create table "tb_Station" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "OrgId"              varchar(32)          not null,
   "SiteId"             varchar(32)          not null,
   "Lon"                number(15,8)         not null,
   "Lat"                number(15,8)         not null,
   "Category"           number(4,0)          not null,
   constraint PK_TB_STATION primary key ("Id")
);

comment on table "tb_Station" is
'基站内容，描述平台内基站的内容';

comment on column "tb_Station"."Id" is
'主键';

comment on column "tb_Station"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_Station"."OrgId" is
'关联组织机构表主键，标识所属组织机构';

comment on column "tb_Station"."SiteId" is
'绑定的基站ID，每个警械柜都有一个基站与之绑定';

comment on column "tb_Station"."Lon" is
'GPS 经度';

comment on column "tb_Station"."Lat" is
'GPS 纬度';

comment on column "tb_Station"."Category" is
'基站类型。0：一般性基站；1：绑定库房基站；2：门口基站。';

/*==============================================================*/
/* Table: "tb_StockAlert"                                       */
/*==============================================================*/
create table "tb_StockAlert" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "OrgId"              varchar(32)          not null,
   "LibId"              varchar(32)          not null,
   "CateId"             varchar(32)          not null,
   "MinCount"           number(9,0)          default 0 not null,
   constraint PK_TB_STOCKALERT primary key ("Id")
);

comment on table "tb_StockAlert" is
'警械库警械预警信息';

comment on column "tb_StockAlert"."Id" is
'主键';

comment on column "tb_StockAlert"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_StockAlert"."OrgId" is
'关联组织机构表主键，标识所属组织机构';

comment on column "tb_StockAlert"."LibId" is
'关联警械库表主键，标识所属警械库';

comment on column "tb_StockAlert"."CateId" is
'关联警械类型表主键，标识预警的警械装备的类型';

comment on column "tb_StockAlert"."MinCount" is
'警械类型的库存最低数量';

/*==============================================================*/
/* Table: "tb_Storage"                                          */
/*==============================================================*/
create table "tb_Storage" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "OrgId"              varchar(32)          not null,
   "Name"               varchar(128)         not null,
   "StationId"          varchar(32)          not null,
   "Lon"                number(15,8)         not null,
   "Lat"                number(15,8)         not null,
   "OfficerId"          varchar(32)          default '0',
   constraint PK_TB_STORAGE primary key ("Id")
);

comment on table "tb_Storage" is
'警械库内容';

comment on column "tb_Storage"."Id" is
'主键';

comment on column "tb_Storage"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_Storage"."OrgId" is
'关联组织机构表主键，标识警械库所属组织机构';

comment on column "tb_Storage"."Name" is
'警械库名称';

comment on column "tb_Storage"."StationId" is
'基站主键';

comment on column "tb_Storage"."Lon" is
'GPS 经度';

comment on column "tb_Storage"."Lat" is
'GPS 纬度';

comment on column "tb_Storage"."OfficerId" is
'警械库负责人，关联警员表主键，NULL 或者 “0” 标识未指定负责警员';

/*==============================================================*/
/* Table: "tb_TagMoveTrail"                                     */
/*==============================================================*/
create table "tb_TagMoveTrail" 
(
   "Id"                 varchar(32)          not null,
   "TagId"              varchar(32)          not null,
   "SiteId"             varchar(32)          not null,
   "Status"             number(4,0)          not null,
   "UpTime"             number(18,0)         not null,
   "CTime"              number(18,0)         not null,
   constraint PK_TB_TAGMOVETRAIL primary key ("Id")
);

comment on table "tb_TagMoveTrail" is
'标签运动轨迹，标签发生位置变化时，应该在当前列表中添加相应的记录';

comment on column "tb_TagMoveTrail"."Id" is
'主键';

comment on column "tb_TagMoveTrail"."TagId" is
'描述当前记录隶属于哪一个标签';

comment on column "tb_TagMoveTrail"."SiteId" is
'描述当前标签在哪一个位置的基站范围内';

comment on column "tb_TagMoveTrail"."Status" is
'描述当前是进入或者离开当前基站。1：进入；2：离开。';

comment on column "tb_TagMoveTrail"."UpTime" is
'描述基站上传标签的时间，标签的运动位置的排序应该以当前字段为准';

comment on column "tb_TagMoveTrail"."CTime" is
'描述系统记录当前标签位置信息的时间';

/*==============================================================*/
/* Table: "tb_sys_ActionLog"                                    */
/*==============================================================*/
create table "tb_sys_ActionLog" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "StartTime"          number(18,0)         not null,
   "EndTime"            number(18,0)         not null,
   "IPv4"               varchar(32),
   "IPv6"               varchar(128),
   "Mac"                varchar(128),
   "FeatureId"          varchar(32)          not null,
   "UserId"             varchar(32)          not null,
   "IsOk"               number(4,0)          not null,
   "ErrorMsg"           varchar(2048),
   constraint PK_TB_SYS_ACTIONLOG primary key ("Id")
);

comment on table "tb_sys_ActionLog" is
'记录用户操作日志，用户在平台发生的任何操作，都应该在当前列表中添加一条相应的记录';

comment on column "tb_sys_ActionLog"."Id" is
'主键';

comment on column "tb_sys_ActionLog"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_sys_ActionLog"."StartTime" is
'当前操作开始发生的时间';

comment on column "tb_sys_ActionLog"."EndTime" is
'当前操作结束的时间';

comment on column "tb_sys_ActionLog"."IPv4" is
'当前操作设备IPV4地址';

comment on column "tb_sys_ActionLog"."IPv6" is
'当前操作设备IPV6地址';

comment on column "tb_sys_ActionLog"."Mac" is
'当前操作设备MAC地址';

comment on column "tb_sys_ActionLog"."FeatureId" is
'关联菜单功能主键，描述当前操作发生哪一个业务功能';

comment on column "tb_sys_ActionLog"."UserId" is
'关联操作用户主键，描述当前操作是哪一个用户发生的';

comment on column "tb_sys_ActionLog"."IsOk" is
'描述当前操作完成状态。0：成功；-1：失败。';

comment on column "tb_sys_ActionLog"."ErrorMsg" is
'操作失败原因';

/*==============================================================*/
/* Table: "tb_sys_Feature"                                      */
/*==============================================================*/
create table "tb_sys_Feature" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "RoleId"             varchar(32)          default '0' not null,
   "MenuId"             varchar(32)          default '0' not null,
   "ActId"              varchar(128)         not null,
   "Status"             number(4,0)          not null,
   "ActRemark"          varchar(512),
   constraint PK_TB_SYS_FEATURE primary key ("Id")
);

comment on table "tb_sys_Feature" is
'描述角色功能内容，记录每一个角色允许访问平台的功能，没有指明角色或者角色为“0”的功能为全局业务功能，任何用户都能够访问。';

comment on column "tb_sys_Feature"."Id" is
'主键';

comment on column "tb_sys_Feature"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_sys_Feature"."RoleId" is
'角色主键';

comment on column "tb_sys_Feature"."MenuId" is
'菜单主键';

comment on column "tb_sys_Feature"."ActId" is
'功能键标识。例如：添加，编辑，删除，查看，导入，导出 ... 等等';

comment on column "tb_sys_Feature"."Status" is
'功能状态。0：正常；-1：禁止；';

comment on column "tb_sys_Feature"."ActRemark" is
'当前操作功能说明';

/*==============================================================*/
/* Table: "tb_sys_Menu"                                         */
/*==============================================================*/
create table "tb_sys_Menu" 
(
   "Id"                 varchar(32)          not null,
   "Title"              varchar(128)         not null,
   "Order"              number(4,0)          not null,
   "Src"                varchar(512),
   "Remarks"            varchar(1024),
   "IsDel"              number(4,0)          not null,
   "Pid"                varchar(32)          default '0' not null,
   constraint PK_TB_SYS_MENU primary key ("Id")
);

comment on table "tb_sys_Menu" is
'功能菜单';

comment on column "tb_sys_Menu"."Id" is
'主键';

comment on column "tb_sys_Menu"."Title" is
'标题';

comment on column "tb_sys_Menu"."Order" is
'序列标识，用于排序';

comment on column "tb_sys_Menu"."Src" is
'功能页面路径';

comment on column "tb_sys_Menu"."Remarks" is
'菜单的备注内容';

comment on column "tb_sys_Menu"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_sys_Menu"."Pid" is
'上一级菜单主键，“0” 标识顶级';

/*==============================================================*/
/* Table: "tb_sys_Role"                                         */
/*==============================================================*/
create table "tb_sys_Role" 
(
   "Id"                 varchar(32)          not null,
   "Name"               varchar(256)         not null,
   "Remarks"            varchar(512),
   "Status"             number(4,0)          not null,
   "IsDel"              number(4,0)          not null,
   "OrgId"              varchar(32)          not null,
   constraint PK_TB_SYS_ROLE primary key ("Id")
);

comment on table "tb_sys_Role" is
'系统角色，用来批量分配用户的权限的一种方式，每一个用户都有且仅有一个角色。相同角色的用户拥有相同权限的平台访问权限。';

comment on column "tb_sys_Role"."Id" is
'主键';

comment on column "tb_sys_Role"."Name" is
'名称';

comment on column "tb_sys_Role"."Remarks" is
'备注';

comment on column "tb_sys_Role"."Status" is
'状态，0：正常；-1：异常；-2：冻结。';

comment on column "tb_sys_Role"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_sys_Role"."OrgId" is
'管理组织机构表主键，标识角色所属组织机构';

/*==============================================================*/
/* Table: "tb_sys_User"                                         */
/*==============================================================*/
create table "tb_sys_User" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              number(4,0)          not null,
   "OrgId"              varchar(32)          not null,
   "Account"            varchar(32)          not null,
   "Passwd"             varchar(128)         not null,
   "Status"             number(4,0)          not null,
   "SigninStatus"       number(4,0)          not null,
   "SignupDate"         number(18,0)         not null,
   "RoleId"             varchar(32)          not null,
   "Category"           number(4,0)          not null,
   constraint PK_TB_SYS_USER primary key ("Id")
);

comment on table "tb_sys_User" is
'描述系统登录账户信息，包含两类信息：超级管理员；一般性用户。一般性管理员通常提供给警员';

comment on column "tb_sys_User"."Id" is
'主键';

comment on column "tb_sys_User"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_sys_User"."OrgId" is
'管理组织机构表主键，标识警员所属组织机构';

comment on column "tb_sys_User"."Account" is
'登录账户';

comment on column "tb_sys_User"."Passwd" is
'警员登录密码。登录时，需要结合登录账户ID，或者警号，或者手机号码同时进行一致性验证数据有效性。同时在验证之前需要校验账户的状态，在账户处于“ 异常 ”状态时，数据一律无效';

comment on column "tb_sys_User"."Status" is
'说明警员登录账户的状态。0：正常；-1：异常；-2：已锁定；';

comment on column "tb_sys_User"."SigninStatus" is
'警员账户登录状态。1：在线；0：未登录；-1：离线；-2：超时离线；';

comment on column "tb_sys_User"."SignupDate" is
'警员账户创建时间，这是一个精确到秒级的时间戳';

comment on column "tb_sys_User"."RoleId" is
'关键系统角色主键，描述用户的角色，用当前字段来确定用户的平台访问功能权限';

comment on column "tb_sys_User"."Category" is
'系统账户类型。1：超级管理员；2：管理员；3：普通用户。';