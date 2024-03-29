﻿SELECT 
  CAST( row_number() OVER ( ORDER BY Enabled ) AS INT ) AS Id, *
FROM 
  (
    SELECT 
      CAST(1 AS BIT) AS Enabled, 
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
      ) AS CounselorUserName, 
      CASE vchActivityType_id WHEN '17' THEN NULL WHEN 'CG HC' THEN NULL WHEN 'CG HV' THEN 4 WHEN 'CG PC' THEN 5 WHEN 'CG Prep' THEN 6 WHEN 'grep prep' THEN NULL WHEN 'GRG HV' THEN 8 WHEN 'GRG PC' THEN 9 WHEN 'GRG PREP' THEN 10 ELSE NULL END AS ActivityTypeId, 
      CASE vchActivityType_id WHEN '17' THEN NULL WHEN 'CG HC' THEN 3 WHEN 'CG HV' THEN 3 WHEN 'CG PC' THEN 3 WHEN 'CG Prep' THEN 3 WHEN 'grep prep' THEN 4 WHEN 'GRG HV' THEN 4 WHEN 'GRG PC' THEN 4 WHEN 'GRG PREP' THEN 4 ELSE NULL END AS SubServiceId, 
      NULL AS ProgramInfoId, 
      CAST(
        CASE intSIS_id WHEN 99 THEN 94 ELSE intSIS_id END AS INT
      ) AS AssistedWithId, 
      CAST(
        varActivity_desc AS VARCHAR(MAX)
      ) AS Note, 
      lngClient_id AS CounselingRecordId, 
      datActivity_dt AS EnteredOn, 
      CAST(intActivity_minutes AS INT) AS Minutes 
    FROM 
      dbo.cacgActivity t1 
    UNION ALL 
    SELECT 
      CAST(1 AS BIT) AS Enabled, 
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
      ) AS CounselorUserName, 
      CASE vchActivityType_id WHEN 'ACVOCACY' THEN 1 WHEN 'HOME VISIT' THEN 11 WHEN 'E' THEN 23 WHEN 'ADVOACAY' THEN 1 WHEN 'advocay' THEN 1 WHEN 'IN OFFICE' THEN 12 WHEN 'PEARLS' THEN 16 WHEN 'ADVOCACY' THEN 1 WHEN 'SHIP' THEN NULL WHEN 'IOA' THEN 12 WHEN 'STAFFING' THEN 20 WHEN 'ADVOCACY HV' THEN 2 WHEN 'TRAVEL TIME' THEN 22 WHEN 'CASE REC' THEN 3 WHEN 'ADVICACY' THEN 1 WHEN 'ADVOCACYY' THEN 1 WHEN '201' THEN NULL WHEN '60' THEN NULL WHEN 'Avocacy' THEN 1 WHEN 'INOFFICE' THEN 12 WHEN '20' THEN NULL WHEN 'phone' THEN 18 WHEN 'OFFICEVISIT' THEN 12 WHEN 'PR' THEN 19 WHEN 'PHONECONTACT' THEN 18 WHEN '30' THEN NULL WHEN 'out office' THEN 14 WHEN 'HOMEVISIT' THEN 11 WHEN '10' THEN NULL WHEN 'case recording' THEN 3 WHEN 'FTF' THEN 7 WHEN 'SUP COUN' THEN 21 WHEN 'advoccy' THEN 1 WHEN 'PEARLS HV' THEN 17 WHEN 'PHONE CONTACT' THEN 18 WHEN 'HV' THEN 11 WHEN 'OFFICE VISIT' THEN 13 WHEN 'CASERECORD' THEN 3 WHEN 'unders' THEN NULL WHEN '' THEN NULL WHEN 'iou' THEN NULL WHEN 'OUTOFFICE' THEN 14 WHEN 'TRAVELTIME' THEN 22 WHEN 'SE REC' THEN 3 WHEN 'pc' THEN 15 ELSE NULL END AS ActivityTypeId, 
      2 AS SubServiceId, 
      CASE vchActivityType_id WHEN 'PEARLS' THEN 1 WHEN 'PEARLS HV' THEN 1 ELSE NULL END AS ProgramInfoId, 
      CASE intSIS_id WHEN 1 THEN 1 WHEN 2 THEN 2 WHEN 3 THEN 3 WHEN 4 THEN 4 WHEN 5 THEN 5 WHEN 6 THEN 6 WHEN 7 THEN 7 WHEN 8 THEN 8 WHEN 9 THEN 9 WHEN 10 THEN 10 WHEN 11 THEN 11 WHEN 12 THEN 12 WHEN 13 THEN 13 WHEN 14 THEN 14 WHEN 15 THEN 15 WHEN 16 THEN 16 WHEN 17 THEN 17 WHEN 18 THEN 18 WHEN 19 THEN 19 WHEN 20 THEN 20 WHEN 21 THEN 21 WHEN 22 THEN 22 WHEN 23 THEN 23 WHEN 24 THEN 24 WHEN 25 THEN 25 WHEN 26 THEN 26 WHEN 27 THEN 27 WHEN 28 THEN 28 WHEN 29 THEN 29 WHEN 30 THEN 30 WHEN 31 THEN 31 WHEN 32 THEN 32 WHEN 33 THEN 33 WHEN 34 THEN 34 WHEN 35 THEN 35 WHEN 36 THEN 36 WHEN 37 THEN 37 WHEN 38 THEN 38 WHEN 39 THEN 39 WHEN 40 THEN 40 WHEN 41 THEN 41 WHEN 42 THEN 42 WHEN 43 THEN 43 WHEN 44 THEN 44 WHEN 45 THEN 45 WHEN 46 THEN 46 WHEN 47 THEN 47 WHEN 48 THEN 48 WHEN 49 THEN 49 WHEN 50 THEN 50 WHEN 51 THEN 51 WHEN 52 THEN 52 WHEN 53 THEN 53 WHEN 54 THEN 54 WHEN 55 THEN 55 WHEN 56 THEN 56 WHEN 57 THEN 57 WHEN 58 THEN 58 WHEN 59 THEN 59 WHEN 60 THEN 60 WHEN 61 THEN 61 WHEN 62 THEN 62 WHEN 63 THEN 63 WHEN 64 THEN 64 WHEN 65 THEN 65 WHEN 66 THEN 66 WHEN 67 THEN 67 WHEN 68 THEN 68 WHEN 69 THEN 69 WHEN 70 THEN 70 WHEN 71 THEN 71 WHEN 72 THEN 72 WHEN 73 THEN 73 WHEN 74 THEN 74 WHEN 75 THEN 75 WHEN 76 THEN 76 WHEN 77 THEN 77 WHEN 78 THEN 78 WHEN 79 THEN 79 WHEN 80 THEN 80 WHEN 81 THEN 81 WHEN 82 THEN 82 WHEN 83 THEN 83 WHEN 84 THEN 84 WHEN 85 THEN 85 WHEN 86 THEN 86 WHEN 87 THEN 87 WHEN 88 THEN 88 WHEN 89 THEN 89 WHEN 90 THEN 90 WHEN 91 THEN 91 WHEN 92 THEN 92 WHEN 93 THEN 93 WHEN 99 THEN 94 WHEN 112 THEN 95 WHEN 113 THEN 96 WHEN 114 THEN 97 WHEN 115 THEN 98 WHEN 117 THEN 99 WHEN 118 THEN 100 WHEN 120 THEN 101 WHEN 121 THEN 102 WHEN 122 THEN 103 WHEN 461 THEN 104 WHEN 462 THEN 105 WHEN 471 THEN 106 WHEN 472 THEN 107 WHEN 473 THEN 108 WHEN 474 THEN 109 WHEN 475 THEN 110 WHEN 476 THEN 111 WHEN 477 THEN 112 WHEN 478 THEN 113 WHEN 479 THEN 114 WHEN 480 THEN 115 WHEN 481 THEN 116 WHEN 482 THEN 117 WHEN 483 THEN 118 ELSE NULL END AS AssistedWithId, 
      CAST(
        varActivity_desc AS VARCHAR(MAX)
      ) AS Note, 
      lngClient_id AS CounselingRecordId, 
      datActivity_dt AS EnteredOn, 
      CAST(intActivity_minutes AS INT) AS Minutes 
    FROM 
      dbo.cacouActivity t1 
    UNION ALL 
    SELECT 
      CAST(1 AS BIT) AS Enabled, 
      datNote_dt + ' ' + datNote_tm AS Date, 
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
      ) AS CounselorUserName, 
      CASE vchNoteVia_desc WHEN 'Conference' THEN NULL WHEN 'office' THEN 6 WHEN 'pea' THEN NULL WHEN 'Phone' THEN 18 ELSE NULL END AS ActivityTypeId, 
      2 AS SubServiceId, 
      NULL AS ProgramInfoId, 
      NULL AS AssistedWithId, 
      CAST(
        txtCaseNote_desc AS VARCHAR(MAX)
      ) AS Note, 
      lngClient_id AS CounselingRecordId, 
      datNote_dt + ' ' + datNote_tm AS EnteredOn, 
      NULL AS Minutes 
    FROM 
      dbo.cacouCaseNotes t1 
    UNION ALL 
    SELECT 
      CAST(1 AS BIT) AS Enabled, 
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
      ) AS CounselorUserName, 
      CASE vchActivityType_id WHEN '1' THEN NULL WHEN '5' THEN NULL WHEN '10' THEN NULL WHEN '15' THEN NULL WHEN '31' THEN NULL WHEN '36' THEN NULL WHEN '66' THEN NULL WHEN '80' THEN NULL WHEN '87' THEN NULL WHEN '93' THEN NULL WHEN '100' THEN NULL WHEN '106' THEN NULL WHEN '112' THEN NULL WHEN '113' THEN NULL WHEN '121' THEN NULL WHEN '128' THEN NULL WHEN '136' THEN NULL WHEN '166' THEN NULL WHEN '[' THEN NULL WHEN 'adocacy' THEN 1 WHEN 'ADVOCACY' THEN 1 WHEN 'advocay' THEN 1 WHEN 'Advoccacy' THEN 1 WHEN 'CASE REC' THEN 3 WHEN 'CASE STAFFING' THEN 20 WHEN 'CASERECORD' THEN 3 WHEN 'FTF' THEN 7 WHEN 'HOME VISIT' THEN 11 WHEN 'HOMEVISIT' THEN 11 WHEN 'hv' THEN 11 WHEN 'IN OFFICE' THEN 12 WHEN 'INOFFICE' THEN 12 WHEN 'OFFICE VISIT' THEN 13 WHEN 'OFFICEVISIT' THEN 13 WHEN 'OH' THEN NULL WHEN 'op' THEN NULL WHEN 'ophon' THEN NULL WHEN 'out office' THEN 14 WHEN 'out office act.' THEN 14 WHEN 'OUTOFFICE' THEN 14 WHEN 'pc' THEN 15 WHEN 'PEARLS' THEN 16 WHEN 'ph113' THEN NULL WHEN 'PHONE CONTACT' THEN 18 WHEN 'PHONECONTACT' THEN 18 WHEN 'PR' THEN 19 WHEN 'staffing' THEN 20 WHEN 'SUP COUN' THEN 21 WHEN 'SUP COUNS' THEN 21 WHEN 'TRAVEL TIME' THEN 22 WHEN 'TRAVELTIME' THEN 22 WHEN 'unders' THEN NULL WHEN 'Y' THEN NULL ELSE NULL END AS ActivityTypeId, 
      1 AS SubServiceId, 
      NULL AS ProgramInfoId, 
      CAST(
        CASE intSIS_id WHEN 1 THEN 1 WHEN 2 THEN 2 WHEN 3 THEN 3 WHEN 4 THEN 4 WHEN 5 THEN 5 WHEN 6 THEN 6 WHEN 7 THEN 7 WHEN 8 THEN 8 WHEN 9 THEN 9 WHEN 10 THEN 10 WHEN 11 THEN 11 WHEN 12 THEN 12 WHEN 13 THEN 13 WHEN 14 THEN 14 WHEN 15 THEN 15 WHEN 16 THEN 16 WHEN 17 THEN 17 WHEN 18 THEN 18 WHEN 19 THEN 19 WHEN 20 THEN 20 WHEN 21 THEN 21 WHEN 22 THEN 22 WHEN 23 THEN 23 WHEN 24 THEN 24 WHEN 25 THEN 25 WHEN 26 THEN 26 WHEN 27 THEN 27 WHEN 28 THEN 28 WHEN 29 THEN 29 WHEN 30 THEN 30 WHEN 31 THEN 31 WHEN 32 THEN 32 WHEN 33 THEN 33 WHEN 34 THEN 34 WHEN 35 THEN 35 WHEN 36 THEN 36 WHEN 37 THEN 37 WHEN 38 THEN 38 WHEN 39 THEN 39 WHEN 40 THEN 40 WHEN 41 THEN 41 WHEN 42 THEN 42 WHEN 43 THEN 43 WHEN 44 THEN 44 WHEN 45 THEN 45 WHEN 46 THEN 46 WHEN 47 THEN 47 WHEN 48 THEN 48 WHEN 49 THEN 49 WHEN 50 THEN 50 WHEN 51 THEN 51 WHEN 52 THEN 52 WHEN 53 THEN 53 WHEN 54 THEN 54 WHEN 55 THEN 55 WHEN 56 THEN 56 WHEN 57 THEN 57 WHEN 58 THEN 58 WHEN 59 THEN 59 WHEN 60 THEN 60 WHEN 61 THEN 61 WHEN 62 THEN 62 WHEN 63 THEN 63 WHEN 64 THEN 64 WHEN 65 THEN 65 WHEN 66 THEN 66 WHEN 67 THEN 67 WHEN 68 THEN 68 WHEN 69 THEN 69 WHEN 70 THEN 70 WHEN 71 THEN 71 WHEN 72 THEN 72 WHEN 73 THEN 73 WHEN 74 THEN 74 WHEN 75 THEN 75 WHEN 76 THEN 76 WHEN 77 THEN 77 WHEN 78 THEN 78 WHEN 79 THEN 79 WHEN 80 THEN 80 WHEN 81 THEN 81 WHEN 82 THEN 82 WHEN 83 THEN 83 WHEN 84 THEN 84 WHEN 85 THEN 85 WHEN 86 THEN 86 WHEN 87 THEN 87 WHEN 88 THEN 88 WHEN 89 THEN 89 WHEN 90 THEN 90 WHEN 91 THEN 91 WHEN 92 THEN 92 WHEN 93 THEN 93 WHEN 99 THEN 94 WHEN 112 THEN 95 WHEN 113 THEN 96 WHEN 114 THEN 97 WHEN 115 THEN 98 WHEN 117 THEN 99 WHEN 118 THEN 100 WHEN 120 THEN 101 WHEN 121 THEN 102 WHEN 122 THEN 103 WHEN 461 THEN 104 WHEN 462 THEN 105 WHEN 471 THEN 106 WHEN 472 THEN 107 WHEN 473 THEN 108 WHEN 474 THEN 109 WHEN 475 THEN 110 WHEN 476 THEN 111 WHEN 477 THEN 112 WHEN 478 THEN 113 WHEN 479 THEN 114 WHEN 480 THEN 115 WHEN 481 THEN 116 WHEN 482 THEN 117 WHEN 483 THEN 118 ELSE NULL END AS INT
      ) AS AssistedWithId, 
      CAST(
        varActivity_desc AS VARCHAR(MAX)
      ) Note, 
      lngClient_id AS CounselingRecordId, 
      datActivity_dt AS EnteredOn, 
      CAST(intActivity_minutes AS INT) AS Minutes 
    FROM 
      dbo.caotrActivity t1 
    UNION ALL 
    SELECT 
      CAST(1 AS BIT) AS Enabled, 
      datNote_dt + ' ' + datNote_tm AS Date, 
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
      ) AS CounselorUserName, 
      CASE vchNoteVia_desc WHEN 'Conference' THEN NULL WHEN 'office' THEN 6 WHEN 'pea' THEN NULL WHEN 'Phone' THEN 18 ELSE NULL END AS ActivityTypeId, 
      1 AS SubServiceId, 
      NULL AS ProgramInfoId, 
      NULL AS AssistedWithId, 
      CAST(
        txtCaseNote_desc AS VARCHAR(MAX)
      ) AS Note, 
      lngClient_id AS CounselingRecordId, 
      datNote_dt + ' ' + datNote_tm AS EnteredOn, 
      NULL AS Minutes 
    FROM 
      dbo.caotrCaseNotes t1
  ) united_table