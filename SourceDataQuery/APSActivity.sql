SELECT 
  CAST( row_number() OVER ( ORDER BY Temp_Id ) AS INT ) AS Id, *
FROM 
  (
    SELECT 
      lngCaActivity_id AS Temp_Id, 
      Cast(1 as bit) AS Enabled, 
      datActivity_dt AS Date, 
      (
        SELECT 
          TOP 1 SUBSTRING(vchStaffFirst_nm, 1, 1) + vchStaffLast_nm + '@familyservicecc.org' 
        FROM 
          tblSupportStaff t2 
        WHERE 
          t1.lngStaff_id = t2.lngStaff_id 
          AND t2.vchStaffFirst_nm IS NOT NULL 
          AND t2.vchStaffLast_nm IS NOT NULL
      ) AS UserName, 
      CASE vchActivityType_id WHEN '5' THEN NULL WHEN '10' THEN NULL WHEN '15' THEN NULL WHEN '30' THEN NULL WHEN '45' THEN NULL WHEN '60' THEN NULL WHEN '75' THEN NULL WHEN '5/9/2017' THEN NULL WHEN '   ftf' THEN 7 WHEN ' ftf' THEN 7 WHEN ' pc' THEN 15 WHEN ' pr' THEN 19 WHEN '[r' THEN 19 WHEN '`ftf' THEN 7 WHEN '`pr' THEN 19 WHEN 'CASERECORD' THEN 3 WHEN 'EC' THEN NULL WHEN 'FTF' THEN 7 WHEN 'HOMEVISIT' THEN 11 WHEN 'HV' THEN 11 WHEN 'INOFFICE' THEN 12 WHEN 'OFFICEVISIT' THEN 13 WHEN 'ou' THEN 14 WHEN 'Per' THEN 19 WHEN 'pf' THEN 19 WHEN 'pftf' THEN 7 WHEN 'PHONECONTACT' THEN 18 WHEN 'PR' THEN 19 WHEN 'PRC' THEN 15 WHEN 'prp' THEN NULL WHEN 'PT' THEN NULL WHEN 'VM' THEN 18 WHEN 'OUTOFFICE' THEN 14 WHEN 'TRAVELTIME' THEN 22 WHEN 'p' THEN NULL WHEN 'PC' THEN 15 ELSE NULL END AS ActivityTypeId, 
      CAST(intSIS_id AS INT) AS SISId, 
      NULL AS ServiceId, 
      CAST(
        varActivity_desc AS VARCHAR(MAX)
      ) AS Note, 
      lngClient_id AS ClientId, 
      datActivity_dt AS EnteredOn, 
      intActivity_minutes AS Minutes 
    FROM 
      dbo.caspsActivity t1 
    UNION 
    SELECT 
      lngCaActivity_id AS Temp_Id, 
      Cast(1 as bit) AS Enabled, 
      datActivity_dt AS Date, 
      (
        SELECT 
          TOP 1 SUBSTRING(vchStaffFirst_nm, 1, 1) + vchStaffLast_nm + '@familyservicecc.org' 
        FROM 
          tblSupportStaff t2 
        WHERE 
          t1.lngStaff_id = t2.lngStaff_id
      ) AS UserName, 
      CASE vchActivityType_id WHEN 'HOME VISIT' THEN 11 WHEN 'IN OFFICE' THEN 12 WHEN 'IOA' THEN 12 WHEN 'TRAVEL TIME' THEN 22 WHEN 'CASE REC' THEN 3 WHEN 'INOFFICE' THEN 12 WHEN 'OFFICEVISIT' THEN 13 WHEN 'PHONECONTACT' THEN 18 WHEN 'OUT OFFICE' THEN 14 WHEN 'HOMEVISIT' THEN 11 WHEN 'PHONE CONTACT' THEN 18 WHEN 'HV' THEN 11 WHEN 'OFFICE VISIT' THEN 13 WHEN 'CASERECORD' THEN 3 WHEN 'CR' THEN 3 WHEN '' THEN NULL WHEN 'OUTOFFICE' THEN 14 WHEN 'TRAVELTIME' THEN 22 WHEN 'PC' THEN 15 ELSE NULL END AS ActivityTypeId, 
      CAST(intSIS_id AS INT) AS SISId, 
      NULL AS ServiceId, 
      CAST(
        varActivity_desc AS VARCHAR(MAX)
      ) AS Note, 
      lngClient_id AS ClientId, 
      datActivity_dt AS EnteredOn, 
      CAST(intActivity_minutes AS INT) AS Minutes 
    FROM 
      dbo.casfnActivity t1 
    UNION 
    SELECT 
      lngClientCaseNote_id AS Temp_Id,
      Cast(1 as bit) AS Enabled, 
      datNote_dt AS Date, 
      (
        SELECT 
          TOP 1 SUBSTRING(vchStaffFirst_nm, 1, 1) + vchStaffLast_nm + '@familyservicecc.org' 
        FROM 
          tblSupportStaff t2 
        WHERE 
          t1.strStaffInitials_nm LIKE t2.strStaffInitials_nm 
          AND t1.strStaffInitials_nm IS NOT NULL 
          AND t1.strStaffInitials_nm NOT LIKE '%EH%' 
          AND t1.strStaffInitials_nm NOT LIKE '%JS%' 
          AND t1.strStaffInitials_nm NOT LIKE '%MM%' 
          AND t1.strStaffInitials_nm NOT LIKE '%MP%' 
          AND t1.strStaffInitials_nm NOT LIKE '%PK%'
      ) AS UserName, 
      CASE vchNoteVia_desc WHEN 'Home Visit' THEN 11 WHEN 'Phone' THEN 18 WHEN 'Preparation' THEN NULL ELSE NULL END AS ActivityTypeId, 
      NULL AS SISId, 
      NULL AS ServiceId, 
      CAST(
        txtCaseNote_desc AS VARCHAR(MAX)
      ) AS Note, 
      lngClient_id AS ClientId, 
      datNote_dt AS EnteredOn, 
      0 AS Minutes 
    FROM 
      dbo.caspsCaseNotes t1
  ) united_table
