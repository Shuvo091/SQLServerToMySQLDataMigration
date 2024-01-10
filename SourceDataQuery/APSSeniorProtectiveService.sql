﻿SELECT 
    CAST(row_number() OVER (ORDER BY lngClient_id) AS INT) AS Id,
    lngClient_id AS ClientId, 
    (
    SELECT 
        TOP 1 SUBSTRING(vchStaffFirst_nm, 1, 1) + vchStaffLast_nm + '@familyservicecc.org' 
    FROM 
        tblSupportStaff t2 
    WHERE 
        t1.vchCaseMgr_nm LIKE t2.vchStaffFirst_nm + ' ' + t2.vchStaffLast_nm + ' (' + t2.vchStaff_type + ')' 
        AND t1.vchCaseMgr_nm IS NOT NULL
    ) AS CaseWorkerUserName, 
    NULL AS StatusId, 
    NULL AS CloseDate, 
    NULL AS OpenDate, 
    CAST(
    txtServiceTips_desc AS NVARCHAR(MAX)
    ) AS Tips  
FROM 
    dbo.caspsServiceData t1