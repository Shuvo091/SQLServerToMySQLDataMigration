SELECT 
  CAST( row_number() OVER ( ORDER BY ClientId ) AS INT ) AS Id, *
FROM 
  (
    SELECT 
      lngClient_id AS ClientId, 
      (
        SELECT 
          TOP 1 SUBSTRING(vchStaffFirst_nm, 1, 1) + vchStaffLast_nm + '@familyservicecc.org' 
        FROM 
          tblSupportStaff t2 
        WHERE 
          t1.vchCaseMgr_nm LIKE t2.vchStaffFirst_nm + ' ' + t2.vchStaffLast_nm + ' (' + t2.vchStaff_type + ')' 
          AND t1.vchCaseMgr_nm IS NOT NULL
      ) AS CaseWorkerUserName, 
      NULL AS Title20Directions, 
      NULL AS CloseDate, 
      NULL AS OpenDate, 
      NULL AS StatusId, 
      CAST(
        txtServiceTips_desc AS NVARCHAR(MAX)
      ) AS Tips 
    FROM 
      dbo.cacgServiceData t1 
    UNION 
    SELECT 
      lngClient_id AS ClientId, 
      (
        SELECT 
          TOP 1 SUBSTRING(vchStaffFirst_nm, 1, 1) + vchStaffLast_nm + '@familyservicecc.org' 
        FROM 
          tblSupportStaff t2 
        WHERE 
          t1.vchCaseMgr_nm LIKE t2.vchStaffFirst_nm + ' ' + t2.vchStaffLast_nm + ' (' + t2.vchStaff_type + ')' 
          AND t1.vchCaseMgr_nm IS NOT NULL
      ) AS CaseWorkerUserName, 
      vchTitle20_desc AS Title20Directions, 
      NULL AS CloseDate, 
      NULL AS OpenDate, 
      NULL AS StatusId, 
      CAST(
        txtServiceTips_desc AS NVARCHAR(MAX)
      ) AS Tips 
    FROM 
      dbo.cacouServiceData t1 
    UNION 
    SELECT 
      lngClient_id AS ClientId, 
      (
        SELECT 
          TOP 1 SUBSTRING(vchStaffFirst_nm, 1, 1) + vchStaffLast_nm + '@familyservicecc.org' 
        FROM 
          tblSupportStaff t2 
        WHERE 
          t1.vchCaseMgr_nm LIKE t2.vchStaffFirst_nm + ' ' + t2.vchStaffLast_nm + ' (' + t2.vchStaff_type + ')' 
          AND t1.vchCaseMgr_nm IS NOT NULL
      ) AS CaseWorkerUserName, 
      vchTitle20_desc AS Title20Directions, 
      NULL AS CloseDate, 
      NULL AS OpenDate, 
      NULL AS StatusId, 
      CAST(
        txtServiceTips_desc AS NVARCHAR(MAX)
      ) AS Tips 
    FROM 
      dbo.caotrServiceData t1
  ) united_table
  -- The following order by ensure to return rows with the most columns populated
  ORDER BY (CASE WHEN ClientId IS NOT NULL THEN 1 ELSE 0 END +
          CASE WHEN CaseWorkerUserName IS NOT NULL THEN 1 ELSE 0 END +
		  CASE WHEN Title20Directions IS NOT NULL THEN 1 ELSE 0 END +
		  CASE WHEN CloseDate IS NOT NULL THEN 1 ELSE 0 END +
          CASE WHEN OpenDate IS NOT NULL THEN 1 ELSE 0 END +
		  CASE WHEN StatusId IS NOT NULL THEN 1 ELSE 0 END +
          CASE WHEN Tips IS NOT NULL THEN 1 ELSE 0 END) desc
