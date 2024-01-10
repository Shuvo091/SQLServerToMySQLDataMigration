SELECT 
  CAST(row_number() OVER (ORDER BY dbo.EmpInService.lngEmp_id) AS INT) AS Id, 
  dbo.EmpInService.lngEmp_id AS StaffId, 
  CAST(1 as bit) AS Enabled, 
  dbo.EmpInService.datInServ_dt AS Date, 
  CASE dbo.tblInServiceTypes.vchInServType_desc WHEN 'In-Service All HCAs' THEN 1 WHEN 'Show Up Pay' THEN 2 WHEN 'Pre-Service HCA' THEN 3 WHEN 'Makeup In-Service ALL HCAs' THEN 4 ELSE NULL END AS InServiceTypeId, 
  dbo.tblInServiceRates.vchInServRate_desc, 
  CAST(dbo.EmpInService.sngInServ_hrs as FLOAT) AS Hours, 
  CAST(CASE WHEN dbo.tblInServiceRates.smyInServRate_amt IS NULL THEN 0 ELSE dbo.tblInServiceRates.smyInServRate_amt END as FLOAT) AS PayRate, 
  NULL AS Note, 
  NULL AS EnteredById,
  dbo.EmpInService.datInServ_dt AS EnteredOn, 
  dbo.EmpInService.lngInServRate_id, 
  dbo.EmpInService.lngInServType_id 
FROM 
  dbo.EmpInService 
  INNER JOIN dbo.tblInServiceRates ON dbo.EmpInService.lngInServRate_id = dbo.tblInServiceRates.lngInServRate_id 
  INNER JOIN dbo.tblInServiceTypes ON dbo.EmpInService.lngInServType_id = dbo.tblInServiceTypes.lngInServType_id
WHERE 
	dbo.EmpInService.lngEmp_id IS NOT NULL 
	AND dbo.EmpInService.datInServ_dt IS NOT NULL 
	AND dbo.tblInServiceTypes.vchInServType_desc IS NOT NULL
	AND dbo.EmpInService.sngInServ_hrs IS NOT NULL
