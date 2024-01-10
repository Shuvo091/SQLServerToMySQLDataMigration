SELECT 
  CAST(ROW_NUMBER() OVER (ORDER BY lngClient_id) AS INT) AS Id, 
  lngClient_id AS ClientId, 
  CASE lngFund_id WHEN 0 THEN 1 WHEN 1 THEN 2 WHEN 2 THEN 3 WHEN 3 THEN 4 WHEN 4 THEN 5 WHEN 5 THEN 6 WHEN 7 THEN 7 WHEN 8 THEN 8 WHEN 9 THEN 9 WHEN 60 THEN NULL WHEN 61 THEN 11 WHEN 65 THEN 12 WHEN 999 THEN 13 WHEN 1000 THEN 14 WHEN 1010 THEN 15 ELSE NULL END AS FundId, 
  NULL AS CaseManagerId,
  t1.vchCaseMgr_nm AS CaseManagerName,
  TRIM(STR(sngServiceMax_amt)) AS ServiceMax, 
  NULL AS AuthorizationEndDate, 
  NULL AS CloseDate, 
  NULL AS OpenDate, 
  txtServiceTips_desc AS Tips, 
  NULL AS StandardBillRate, 
  CAST(smyBill_rate as FLOAT) AS ActualBillRate, 
  CAST(sngHours_req as FLOAT) AS HoursRequired, 
  CAST(sngFrequency_amt as INT) AS Frequency,
  NULL AS StatusId,
  NULL AS LastDateOfService,
  NULL AS AuthorizationNumber
FROM 
  dbo.homeServiceData t1