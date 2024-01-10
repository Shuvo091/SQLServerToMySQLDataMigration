SELECT 
  CAST(row_number() OVER (ORDER BY lngClient_id) AS INT) AS Id,
  lngClient_id AS ClientId,
  NULL AS CaseManagerId, 
  t1.vchCaseMgr_nm AS CaseManagerName,
  datMeals_start AS MealsOpenDate, 
  datMeals_end AS MealsCloseDate, 
  CAST(CASE bolUseBillTo_fl WHEN 0 THEN 0 ELSE 1 END as bit) AS UserBilltoAddress, 
  CAST(CASE bolPrimaryBill_fl WHEN 0 THEN 0 ELSE 1 END as bit) AS BillClientAsPrimary, 
  strMeal_desc AS Instructions, 
  strBuilding_type + ': ' + strRoute_desc AS RouteDirections, 
  NULL AS CloseDate, 
  NULL AS OpenDate, 
  txtServiceTips_desc AS Tips,
  NULL AS StatusId
FROM 
  dbo.mealsServiceData t1 
WHERE 
  (lngClient_id NOT IN (0, 2157, 2158, 2260, 2441, 2519, 2658, 2747, 2871, 2913, 3063, 3144, 3247, 3332, 3707, 3862, 4319, 4320, 4668, 5081, 5252, 5335, 5545, 5872, 6012, 8056, 10344))