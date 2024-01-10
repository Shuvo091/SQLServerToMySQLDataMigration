SELECT 
  CAST(row_number() OVER (ORDER BY t1.lngClient_id) AS INT) AS Id, 
  t1.lngEmp_id AS StaffId, 
  t1.lngClient_id AS ClientId, 
  NULL AS BillingTypeId, 
  TRIM(STR(t1.lngClient_id)) AS Subject, 
  t2.sdtBegin_tm AS StartTime, 
  t2.sdtEnd_tm AS EndTime, 
  NULL AS StartTimezone, 
  NULL AS EndTimezone, 
  NULL AS Location, 
  t2.bytDay_week, 
  NULL AS Description, 
  CAST(0 as bit) AS IsAllDay, 
  NULL AS RecurrenceID, 
  CASE t2.bytDay_week WHEN 1 THEN 'FREQ=WEEKLY;BYDAY=SU;' WHEN 2 THEN 'FREQ=WEEKLY;BYDAY=MO;' WHEN 3 THEN 'FREQ=WEEKLY;BYDAY=TU;' WHEN 4 THEN 'FREQ=WEEKLY;BYDAY=WE;' WHEN 5 THEN 'FREQ=WEEKLY;BYDAY=TH;' WHEN 6 THEN 'FREQ=WEEKLY;BYDAY=FR;' WHEN 7 THEN 'FREQ=WEEKLY;BYDAY=SA;' ELSE NULL END AS RecurrenceRule,
  NULL AS RecurrenceException, 
  CAST(0 as bit) AS IsReadOnly, 
  CAST(0 as bit) AS IsBlock, 
  NULL AS FollowingId, 
  NULL AS Guid,
  NULL AS BillRate
FROM 
  dbo.qryEmpScheduleMasterFinal t1 FULL 
  OUTER JOIN dbo.homeScheduleMaster t2 ON (t1.lngEmp_id = t2.lngEmp_id OR (t1.lngEmp_id IS NULL AND t2.lngEmp_id IS NULL))
  AND t1.lngClient_id = t2.lngClient_id
  AND t1.bytHour_day = t2.bytHour_day
WHERE 
  ((t1.lngEmp_id IS NOT NULL) 
  OR (t1.lngClient_id IS NOT NULL))
  AND t2.sdtBegin_tm IS NOT NULL
  AND t2.sdtEnd_tm IS NOT NULL