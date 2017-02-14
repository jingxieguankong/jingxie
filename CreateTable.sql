drop table "tb_ActionLog" cascade constraints;

drop table "tb_Cabinet" cascade constraints;

drop table "tb_EqtCategory" cascade constraints;

drop table "tb_Equipment" cascade constraints;

drop table "tb_Feature" cascade constraints;

drop table "tb_Officer" cascade constraints;

drop table "tb_Organization" cascade constraints;

drop table "tb_PoliceType" cascade constraints;

drop table "tb_Role" cascade constraints;

drop table "tb_StandardEquipment" cascade constraints;

drop table "tb_Station" cascade constraints;

drop table "tb_StockAlert" cascade constraints;

drop table "tb_Storage" cascade constraints;

drop table "tb_menu" cascade constraints;

/*==============================================================*/
/* Table: "tb_ActionLog"                                        */
/*==============================================================*/
create table "tb_ActionLog" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              smallint             not null,
   "UserId"             varchar(32)          not null,
   "StartTime"          number(18,0)         not null,
   "EndTime"            number(18,0)         not null,
   "IPv4"               varchar(32),
   "IPv6"               varchar(128),
   "Mac"                varchar(128),
   "FeatureId"          varchar(32)          not null,
   constraint PK_TB_ACTIONLOG primary key ("Id")
);

comment on table "tb_ActionLog" is
'用户操作日志';

comment on column "tb_ActionLog"."Id" is
'主键';

comment on column "tb_ActionLog"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

/*==============================================================*/
/* Table: "tb_Cabinet"                                          */
/*==============================================================*/
create table "tb_Cabinet" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              smallint             not null,
   "OrgId"              varchar(32)          not null,
   "Name"               varchar(128)         not null,
   "StationId"          int                  not null,
   "Lon"                float                not null,
   "Lat"                float                not null,
   "OfficerId"          varchar(32),
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
'绑定的基站ID，每个警械柜都有一个基站与之绑定';

comment on column "tb_Cabinet"."Lon" is
'GPS 经度';

comment on column "tb_Cabinet"."Lat" is
'GPS 纬度';

comment on column "tb_Cabinet"."OfficerId" is
'警械柜负责人，关联警员表主键，NULL 标识未指定负责警员';

/*==============================================================*/
/* Table: "tb_EqtCategory"                                      */
/*==============================================================*/
create table "tb_EqtCategory" 
(
   "Id"                 varchar(32)          not null,
   "Name"               varchar(128)         not null,
   "Code"               varchar(32)          not null,
   "Pid"                varchar(32),
   "Layer"              smallint             not null,
   "IsDel"              smallint             not null,
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

/*==============================================================*/
/* Table: "tb_Equipment"                                        */
/*==============================================================*/
create table "tb_Equipment" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              smallint             not null,
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
   "Status"             smallint             not null,
   "Power"              smallint             not null,
   "HitCount"           smallint             not null,
   "IsLost"             smallint             not null,
   "IsChanged"          smallint             not null,
   "ChangeTime"         number(18,0)         not null,
   "FactorTime"         number(18,0)         not null,
   "ExpiredTime"        number(18,0)         not null,
   "PurchaseTime"       number(18,0)         not null,
   "Dispatched"         smallint             not null,
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

comment on column "tb_Equipment"."HitCount" is
'强撞击次数。这是一个大于 0 的值，0 ：不限制，其它表示最大被允许撞击的次数。';

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
/* Table: "tb_Feature"                                          */
/*==============================================================*/
create table "tb_Feature" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              smallint             not null,
   "RoleId"             varchar(32)          not null,
   "MenuId"             varchar(32)          not null,
   "ActId"              varchar(128)         not null,
   "Status"             smallint             not null,
   "ActRemark"          varchar(512),
   constraint PK_TB_FEATURE primary key ("Id")
);

comment on table "tb_Feature" is
'描述角色功能内容';

comment on column "tb_Feature"."Id" is
'主键';

comment on column "tb_Feature"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_Feature"."RoleId" is
'角色主键';

comment on column "tb_Feature"."MenuId" is
'菜单主键';

comment on column "tb_Feature"."ActId" is
'功能键标识。例如：添加，编辑，删除，查看，导入，导出 ... 等等';

comment on column "tb_Feature"."Status" is
'功能状态。0：正常；-1：禁止；';

/*==============================================================*/
/* Table: "tb_Officer"                                          */
/*==============================================================*/
create table "tb_Officer" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              smallint             not null,
   "OrgId"              varchar(32)          not null,
   "PtId"               varchar(32)          not null,
   "CabId"              varchar(32),
   "Name"               varchar(128)         not null,
   "Sex"                smallint             not null,
   "IdentyCode"         varchar(32)          not null,
   "Tel"                varchar(64),
   "AtrUrl"             varchar(256),
   "DigitalID"          varchar(128),
   "Account"            varchar(32)          not null,
   "Passwd"             varchar(128)         not null,
   "Status"             smallint             not null,
   "SigninStatus"       smallint             not null,
   "SignupDate"         long                 not null,
   "RoleId"             varchar(32)          not null,
   constraint PK_TB_OFFICER primary key ("Id")
);

comment on table "tb_Officer" is
'描述警员的详细信息，并描述警员的登录账户信息';

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

comment on column "tb_Officer"."Passwd" is
'警员登录密码。登录时，需要结合登录账户ID，或者警号，或者手机号码同时进行一致性验证数据有效性。同时在验证之前需要校验账户的状态，在账户处于“ 异常 ”状态时，数据一律无效';

comment on column "tb_Officer"."Status" is
'说明警员登录账户的状态。0：正常；-1：异常；-2：已锁定；';

comment on column "tb_Officer"."SigninStatus" is
'警员账户登录状态。1：在线；0：未登录；-1：离线；-2：超时离线；';

comment on column "tb_Officer"."SignupDate" is
'警员账户创建时间，这是一个精确到秒级的时间戳';

/*==============================================================*/
/* Table: "tb_Organization"                                     */
/*==============================================================*/
create table "tb_Organization" 
(
   "Id"                 varchar(32)          not null,
   "Name"               varchar(128)         not null,
   "Code"               varchar(128)         not null,
   "Pid"                varchar(32),
   "Layer"              smallint             not null,
   "IsDel"              smallint             not null,
   constraint PK_TB_ORGANIZATION primary key ("Id")
);

comment on table "tb_Organization" is
'组织机构内容';

comment on column "tb_Organization"."Id" is
'组织机构主键标识';

comment on column "tb_Organization"."Name" is
'组织机构名称';

comment on column "tb_Organization"."Code" is
'组织机构代码，总是等于上一级组织机构代码+当前组织机构代码';

comment on column "tb_Organization"."Pid" is
'自引用外键，上一级组织机构主键';

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
   "IsDel"              smallint             not null,
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
/* Table: "tb_Role"                                             */
/*==============================================================*/
create table "tb_Role" 
(
   "Id"                 varchar(32)          not null,
   "Name"               varchar(256)         not null,
   "Remarks"            varchar(512),
   "Status"             smallint             not null,
   "IsDel"              smallint             not null,
   "OrgId"              varchar(32)          not null,
   constraint PK_TB_ROLE primary key ("Id")
);

comment on table "tb_Role" is
'系统角色';

comment on column "tb_Role"."Id" is
'主键';

comment on column "tb_Role"."Name" is
'名称';

comment on column "tb_Role"."Remarks" is
'备注';

comment on column "tb_Role"."Status" is
'状态，0：正常；-1：异常；-2：冻结。';

comment on column "tb_Role"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_Role"."OrgId" is
'管理组织机构表主键，标识角色所属组织机构';

/*==============================================================*/
/* Table: "tb_StandardEquipment"                                */
/*==============================================================*/
create table "tb_StandardEquipment" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              smallint             not null,
   "PtId"               varchar(32)          not null,
   "CateId"             varchar(32)          not null,
   "Num"                smallint             default 0 not null,
   "IsPrimary"          smallint             not null,
   "IsRequire"          smallint             not null,
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
   "IsDel"              smallint             not null,
   "OrgId"              varchar(32)          not null,
   "Code"               int                  not null,
   "Lon"                float                not null,
   "Lat"                float                not null,
   constraint PK_TB_STATION primary key ("Id")
);

comment on table "tb_Station" is
'基站内容';

comment on column "tb_Station"."Id" is
'主键';

comment on column "tb_Station"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_Station"."OrgId" is
'关联组织机构表主键，标识所属组织机构';

comment on column "tb_Station"."Code" is
'绑定的基站ID，每个警械柜都有一个基站与之绑定';

comment on column "tb_Station"."Lon" is
'GPS 经度';

comment on column "tb_Station"."Lat" is
'GPS 纬度';

/*==============================================================*/
/* Table: "tb_StockAlert"                                       */
/*==============================================================*/
create table "tb_StockAlert" 
(
   "Id"                 varchar(32)          not null,
   "IsDel"              smallint             not null,
   "OrgId"              varchar(32)          not null,
   "LibId"              varchar(32)          not null,
   "CateId"             varchar(32)          not null,
   "MinCount"           int                  default 0 not null,
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
   "IsDel"              smallint             not null,
   "OrgId"              varchar(32)          not null,
   "Name"               varchar(128)         not null,
   "StationId"          int                  not null,
   "Lon"                float                not null,
   "Lat"                float                not null,
   "OfficerId"          varchar(32),
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
'绑定的基站ID';

comment on column "tb_Storage"."Lon" is
'GPS 经度';

comment on column "tb_Storage"."Lat" is
'GPS 纬度';

comment on column "tb_Storage"."OfficerId" is
'警械库负责人，关联警员表主键，NULL 标识未指定负责警员';

/*==============================================================*/
/* Table: "tb_menu"                                             */
/*==============================================================*/
create table "tb_menu" 
(
   "Id"                 varchar(32)          not null,
   "Title"              varchar(128)         not null,
   "Order"              smallint             not null,
   "Src"                varchar(512)         not null,
   "Remarks"            varchar(1024),
   "IsDel"              smallint             not null,
   "Pid"                varchar(32),
   constraint PK_TB_MENU primary key ("Id")
);

comment on table "tb_menu" is
'功能菜单';

comment on column "tb_menu"."Id" is
'主键';

comment on column "tb_menu"."Title" is
'标题';

comment on column "tb_menu"."Order" is
'序列标识，用于排序';

comment on column "tb_menu"."Src" is
'功能页面路径';

comment on column "tb_menu"."Remarks" is
'菜单的备注内容';

comment on column "tb_menu"."IsDel" is
'逻辑删除标识。1：标识已删除；0：标识未删除';

comment on column "tb_menu"."Pid" is
'上一级菜单主键';
