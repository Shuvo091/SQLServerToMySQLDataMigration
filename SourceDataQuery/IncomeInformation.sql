SELECT 
  CAST(ROW_NUMBER() OVER (ORDER BY lngClient_id) AS INT) AS Id, 
  CAST (1 as bit) AS Enabled, 
  CASE bytIncomeSrc_stat WHEN '1' THEN 1 WHEN '2' THEN 2 WHEN '3' THEN 3 WHEN '5' THEN 4 WHEN '6' THEN 5 ELSE NULL END AS IncomeSourceId, 
  lngClient_id AS ClientId, 
  mnyMonthly_income AS MonthlyIncome,
  mnyMonthly_income * 12 AS YearlyIncome 
FROM 
  dbo.ClientStatsMain
  where bytIncomeSrc_stat is not null