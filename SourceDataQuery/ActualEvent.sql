SELECT 
  CAST(row_number() OVER (ORDER BY lngClient_id) AS INT) AS Id, 
  lngEmp_id AS StaffId, 
  lngClient_id AS ClientId, 
  CASE lngBillingType_id WHEN 1 THEN 2 WHEN 2 THEN 4 WHEN 3 THEN 3 WHEN 14 THEN 1 WHEN 15 THEN 5 WHEN 16 THEN 6 WHEN 17 THEN 7 WHEN 18 THEN 8 WHEN 19 THEN 9 ELSE NULL END AS BillingTypeId, 
  NULL AS FollowingID, 
  NULL AS Guid, 
  NULL AS Subject, 
  datTS_dt + ' ' + sdtBegin_tm AS StartTime, 
  datTS_dt + ' ' + sdtEnd_tm AS EndTime, 
  NULL AS StartTimezone, 
  NULL AS EndTimezone, 
  NULL AS Location, 
  NULL AS Description, 
  CAST(0 as bit) AS IsAllDay, 
  NULL AS RecurrenceID, 
  NULL AS RecurrenceRule, 
  NULL AS RecurrenceException, 
  CAST(1 as bit) AS IsReadOnly, 
  CAST(0 as bit) AS IsBlock,
  NULL AS BillRate,
  NULL AS PayrollRate
FROM 
  dbo.homeScheduleActual
