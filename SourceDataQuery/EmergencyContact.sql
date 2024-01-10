SELECT 
  CAST(row_number() OVER (ORDER BY Name) AS INT) AS Id, * 
FROM 
  (
    SELECT 
      Cast(1 as bit) AS Enabled, 
      vchEmrgContact_nm AS Name, 
      CASE LEN(strEmrgContact_ph) WHEN 7 THEN CONCAT('217', strEmrgContact_ph) ELSE strEmrgContact_ph END AS Phone, 
      7 AS RelationshipId, 
      NULL AS Location, 
      Cast(0 as bit) AS ROI, 
      NULL AS ROIExpirationDate, 
      NULL AS Notes, 
      NULL AS ClientId, 
      NULL AS Email, 
      lngEmp_id AS StaffId 
    FROM 
      dbo.Employees 
    UNION 
    SELECT 
      Cast(1 as bit) AS Enabled, 
      NULL AS Name, 
      NULL AS Phone, 
      7 AS RelationshipId, 
      NULL AS Location, 
      Cast(0 as bit) AS ROI, 
      NULL AS ROIExpirationDate, 
      vchEmergContact_desc AS Notes, 
      lngClient_id AS ClientId, 
      NULL AS Email, 
      NULL AS StaffId 
    FROM 
      dbo.Clients 
    WHERE 
      (vchEmergContact_desc IS NOT NULL)
  ) temp
