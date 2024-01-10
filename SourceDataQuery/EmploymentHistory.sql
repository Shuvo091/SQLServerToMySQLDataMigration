SELECT 
  CAST(row_number() OVER (ORDER BY lngEmp_id) AS INT) AS Id, 
  CAST(1 as bit) AS Enabled, 
  datEmpHired_dt AS DateHired, 
  datEmpTerm_dt AS TermDate, 
  CASE WHEN datEmpTerm_dt IS NOT NULL THEN vchEmpTerm_desc ELSE NULL END AS ReasonForTermination, 
  lngEmp_id AS StaffId 
FROM 
  dbo.Employees 
WHERE 
  (datEmpHired_dt IS NOT NULL) 
  OR (datEmpTerm_dt IS NOT NULL)
