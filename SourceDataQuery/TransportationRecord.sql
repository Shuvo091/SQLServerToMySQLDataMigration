SELECT 
  CAST(row_number() OVER (ORDER BY lngClient_id) AS INT) AS Id, 
  lngClient_id AS ClientId, 
  t1.vchCaseMgr_nm AS CaseManagerName,
  NULL AS CaseManagerId, 
  NULL AS CloseDate, 
  NULL AS OpenDate, 
  txtServiceTips_desc AS Tips,
  NULL AS StatusId
FROM 
  dbo.transportServiceData t1