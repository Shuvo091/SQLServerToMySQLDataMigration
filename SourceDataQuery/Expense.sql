SELECT 
  lngExp_id AS Id, 
  CAST(1 as bit) AS Enabled, 
  datExp_date AS Date, 
  CASE strExp_type WHEN 'M' THEN 1 WHEN 'C' THEN 2 WHEN 'E' THEN 3 WHEN 'O' THEN 4 ELSE 4 END AS ExpenseTypeId, 
  CAST(sngExp_units as INT) AS Quantity, 
  vchExp_desc AS Note, 
  CAST(smyExpUnit_amt as FLOAT) AS UnitCost, 
  lngClient_id AS HomeCareRecordId 
FROM 
  dbo.ClientExpense
