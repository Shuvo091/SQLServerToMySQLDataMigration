SELECT 
  lngStaff_id AS Id, 
  Cast(1 as bit) AS Enabled, 
  (
    SELECT 
      COUNT(lngStaff_id) 
    FROM 
      dbo.tblSupportStaff AS t1 
    WHERE 
      t1.lngStaff_id <= t2.lngStaff_id 
      AND t1.vchStaffFirst_nm NOT LIKE 'Homecare' 
      AND t1.vchStaffFirst_nm NOT LIKE 'Voice'
  ) AS DisplayOrder, 
  vchStaffFirst_nm AS FirstName, 
  vchStaffLast_nm AS LastName, 
  NULL AS PhoneNumber, 
  NULL AS Email, 
  NULL AS Agency 
FROM 
  dbo.tblSupportStaff t2 
  Join (
    SELECT 
      min(lngStaff_id) as Id 
    FROM 
      dbo.tblSupportStaff t3 
    GROUP BY 
      vchStaffFirst_nm, 
      vchStaffLast_nm
  ) t3 on t2.lngStaff_id = t3.Id 
WHERE 
  (vchStaffFirst_nm IS NOT NULL) 
  AND (vchStaffLast_nm IS NOT NULL) 
  AND (
    vchStaffFirst_nm NOT LIKE 'Homecare'
  ) 
  AND (vchStaffFirst_nm NOT LIKE 'Voice') 
ORDER BY 
  lngStaff_id
