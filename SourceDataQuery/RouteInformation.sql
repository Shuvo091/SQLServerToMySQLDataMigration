SELECT 
  TOP 100 PERCENT lngMealsScheduleMas_id AS Id, 
  CAST(1 as bit) AS Enabled, 
  CASE bytDay_week WHEN 1 THEN 0 WHEN 2 THEN 1 WHEN 3 THEN 2 WHEN 4 THEN 3 WHEN 5 THEN 4 WHEN 6 THEN 5 WHEN 7 THEN 6 ELSE NULL END AS DayOfWeek, 
  CASE lngRoute_id WHEN 2 THEN 1 WHEN 3 THEN 8 WHEN 4 THEN 5 WHEN 5 THEN 4 WHEN 6 THEN 7 WHEN 8 THEN 9 WHEN 16 THEN 3 WHEN 19 THEN 10 WHEN 23 THEN 11 WHEN 24 THEN 12 WHEN 25 THEN 13 WHEN 27 THEN 2 WHEN 28 THEN 6 ELSE NULL END AS RouteId, 
  CASE vchMeal_cd WHEN '1 a day' THEN 1 WHEN '2 a Day Reg' THEN 2 WHEN '2 skim, spice, caf' THEN 3 WHEN 'Friday Special' THEN 4 WHEN 'Friday Special #1' THEN 5 WHEN 'Friday Special #2' THEN 6 WHEN 'Friday Special #3' THEN 7 WHEN 'Friday Special #4' THEN 8 WHEN 'no Salt' THEN 9 WHEN 'Regular Meal' THEN 10 WHEN 'S1-DiabLoCholNORice' THEN 11 WHEN 'SA1-NO MUSHROOMS' THEN 12 WHEN 'Sp Veg' THEN 13 WHEN 'Special Lactose Free' THEN 14 WHEN 'Special-Diabetic' THEN 15 WHEN 'Special-DiabLoChol' THEN 16 WHEN 'Special-LoChlSdmDiab' THEN 17 WHEN 'Special-LoChol' THEN 18 WHEN 'Special-LoCholSodm' THEN 19 WHEN 'Special-LoSodm' THEN 20 WHEN 'Special-LoSodmDiab' THEN 21 WHEN 'Special-Substitution' THEN 22 WHEN 'Supper Bag' THEN 23 ELSE NULL END AS MealTypeId, 
  TRIM(STR(decDelivery_no)) AS DeliveryNumber, 
  vchRemarks_desc AS Description, 
  lngClient_id AS MealsOnWheelsRecordId 
FROM 
  dbo.mealsScheduleMaster
WHERE lngClient_id <> 5081
ORDER BY 
  lngMealsScheduleMas_id
