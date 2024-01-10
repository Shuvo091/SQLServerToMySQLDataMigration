SELECT 
  CAST(row_number() OVER (ORDER BY dbo.EmpTypeData.lngEmp_id) AS INT) AS Id, 
  dbo.EmpTypeData.lngEmp_id AS StaffId, 
  CASE dbo.EmpTypes.vchEmpType_nm WHEN 'Church Women United Volunteer' THEN 1 WHEN 'Council of Congregations' THEN 2 WHEN 'Employee' THEN 3 WHEN 'Retired Senior Volenteer Program' THEN 4 WHEN 'Volunteer - General Not V1, V2, or V3' THEN 5 WHEN 'Volunteer - MOW' THEN 6 WHEN 'Volunteer - Reassurance Calling' THEN 7 WHEN 'Volunteer - Transportation' THEN 8 ELSE NULL END AS ClassificationId 
FROM 
  dbo.EmpTypeData 
  INNER JOIN dbo.EmpTypes ON dbo.EmpTypeData.strEmpType_id = dbo.EmpTypes.strEmpType_id
