SELECT 
  CAST(row_number() OVER (ORDER BY lngClient_id) AS INT) AS Id, 
  lngClient_id AS HomeCareRecordId, 
  CASE lngSubStats_id WHEN 16 THEN 4 WHEN 17 THEN 9 WHEN 18 THEN 2 WHEN 19 THEN 11 WHEN 20 THEN 10 ELSE NULL END AS CareServiceId, 
  1 AS MinimumTimes, 
  NULL AS SpecialInstruction 
FROM 
  dbo.ClientStatsSub 
WHERE 
  (
    lngSubStats_id IN (16, 17, 18, 19, 20)
  )
  AND lngClient_id <> 9164