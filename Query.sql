INSERT INTO "tb_sys_Menu"("Id","Title","Order","Src","Remarks","IsDel","Pid")
VALUES('3BB26A13624D4CE39270699BF2E1F1D7','系统权限','0','','',0,null);

INSERT INTO "tb_sys_Menu"("Id","Title","Order","Src","Remarks","IsDel","Pid")
VALUES('175B4161DF764A5C84ED402A372FA617','系统菜单管理','0','/menu','',0,'3BB26A13624D4CE39270699BF2E1F1D7');

INSERT INTO "tb_sys_User"("Id","IsDel","OrgId","Account","Passwd","Status","SigninStatus","SignupDate","RoleId","Category")
VALUES('000B4A3CFDC540DA9CB12D132B78E37C', 0, null, 'admin', 'fFQb5W7uuaBrO1fi7NLAYQ==', 0, 0, '14876522097523529', null, 1);