﻿SELECT  
dbo.Clients.lngClient_id AS Id,  
dbo.Clients.vchClientFirst_nm AS FirstName,  
dbo.Clients.vchClientLast_nm AS LastName,  
SUBSTRING(    dbo.Clients.vchClientMiddle_nm,    1, 1  ) AS MI,  
dbo.Clients.vchClientNick_nm AS PreferredName,  
dbo.Clients.txtTips_desc AS tips,  
dbo.Clients.vchClient_add AS HomeAddress,  
dbo.Clients.vchClient_city AS City,  
CASE strClient_state WHEN 'AK' THEN 2 WHEN 'AL' THEN 1 WHEN 'AR' THEN 4 WHEN 'AZ' THEN 3 WHEN 'CA' THEN 5 WHEN 'CO' THEN 6 WHEN 'CT' THEN 7 WHEN 'FL' THEN 9 WHEN 'GA' THEN 10 WHEN 'HI' THEN 11 WHEN 'IA' THEN 15 WHEN 'ID' THEN 12 WHEN 'II' THEN 13 WHEN 'IL' THEN 13 WHEN 'IN' THEN 14 WHEN 'IO' THEN 15 WHEN 'KS' THEN 16 WHEN 'KY' THEN 17 WHEN 'MA' THEN 21 WHEN 'ME' THEN 19 WHEN 'MI' THEN 22 WHEN 'MN' THEN 23 WHEN 'MO' THEN 25 WHEN 'NC' THEN 33 WHEN 'NE' THEN 27 WHEN 'NJ' THEN 30 WHEN 'NM' THEN 31 WHEN 'NV' THEN 28 WHEN 'NY' THEN 32 WHEN 'OH' THEN 35 WHEN 'OK' THEN 36 WHEN 'OR' THEN 37 WHEN 'q' THEN NULL WHEN 'SC' THEN 40 WHEN 'TN' THEN 42 WHEN 'TX' THEN 43 WHEN 'VA' THEN 46 WHEN 'WA' THEN 47 WHEN 'WI' THEN 49 ELSE NULL END AS StateId,  dbo.Clients.strClient_zip AS ZipCode,  CASE vchClientCounty_nm WHEN 'Champaign' THEN 1 WHEN 'Coles' THEN 2 WHEN 'Cumberland' THEN 3 WHEN 'Dewitt' THEN 4 WHEN 'Douglas' THEN 5 WHEN 'Ford' THEN 6 WHEN 'Iroquois' THEN 7 WHEN 'Macon' THEN 8 WHEN 'McLean' THEN 9 WHEN 'Other' THEN 12 WHEN 'Piatt' THEN 10 WHEN 'Vermillion' THEN 11 WHEN 'Vermilllion' THEN 11 WHEN '' THEN NULL ELSE NULL END AS CountyId,  
NULL AS Email,  
dbo.Clients.datBirth_dt AS Birthdate,  
dbo.Clients.strHome_ph AS HomePhone,  
dbo.Clients.strOther_ph AS OtherPhone,  
dbo.Clients.vchOtherPhone_desc AS OtherPhoneDesc,  
NULL AS BillingName, dbo.Clients.vchBilling_add AS BillingAddress,  
dbo.Clients.vchBilling_city AS BillingCity,  
CASE strBilling_state WHEN 'AK' THEN 2 WHEN 'AL' THEN 1 WHEN 'AR' THEN 4 WHEN 'AZ' THEN 3 WHEN 'CA' THEN 5 WHEN 'CO' THEN 6 WHEN 'CT' THEN 7 WHEN 'FL' THEN 9 WHEN 'GA' THEN 10 WHEN 'HI' THEN 11 WHEN 'IA' THEN 15 WHEN 'ID' THEN 12 WHEN 'II' THEN 13 WHEN 'IL' THEN 13 WHEN 'IN' THEN 14 WHEN 'IO' THEN 15 WHEN 'KS' THEN 16 WHEN 'KY' THEN 17 WHEN 'MA' THEN 21 WHEN 'ME' THEN 19 WHEN 'MI' THEN 22 WHEN 'MN' THEN 23 WHEN 'MO' THEN 25 WHEN 'NC' THEN 33 WHEN 'NE' THEN 27 WHEN 'NJ' THEN 30 WHEN 'NM' THEN 31 WHEN 'NV' THEN 28 WHEN 'NY' THEN 32 WHEN 'OH' THEN 35 WHEN 'OK' THEN 36 WHEN 'OR' THEN 37 WHEN 'q' THEN NULL WHEN 'SC' THEN 40 WHEN 'TN' THEN 42 WHEN 'TX' THEN 43 WHEN 'VA' THEN 46 WHEN 'WA' THEN 47 WHEN 'WI' THEN 49 ELSE NULL END AS BillingStateId,  
dbo.Clients.strSS_num AS SocialSecurity, 
NULL AS Medicaid,  
NULL AS InsuranceId, 
CASE WHEN dbo.ClientStatsMain.bitBSDpass_flag IS NULL THEN CAST(0 as bit) ELSE dbo.ClientStatsMain.bitBSDpass_flag END AS DASHPass,  
CASE dbo.ClientStatsMain.strSex_cd WHEN 'M' THEN 1 WHEN 'F' THEN 2 ELSE NULL END AS GenderId,  
CASE dbo.ClientStatsMain.bytRace_stat WHEN '1' THEN 5 WHEN '2' THEN 3 WHEN '33' THEN 4 WHEN '4' THEN 2 WHEN '5' THEN 6 WHEN '6' THEN 7 WHEN '7' THEN 1 WHEN '8' THEN 9 WHEN '9' THEN 8 ELSE NULL END AS RaceId,  
CAST (dbo.ClientStatsMain.bytEthnic_stat as INT) AS EthnicityId,  
CASE CAST (dbo.ClientStatsMain.bytMarital_stat AS INT) WHEN 0 THEN NULL ELSE CAST(dbo.ClientStatsMain.bytMarital_stat AS INT) END AS MaritalStatusId,
CASE CAST (dbo.ClientStatsMain.bytLiving_stat AS INT) WHEN 0 THEN NULL ELSE CAST(dbo.ClientStatsMain.bytLiving_stat AS INT) END AS LivingStatusId,
CASE CAST (dbo.ClientStatsMain.bytHousing_stat AS INT) WHEN 0 THEN NULL ELSE CAST(dbo.ClientStatsMain.bytHousing_stat AS INT) END AS LivingArrangementId,  
CASE strPoverty_stat WHEN 'A' THEN 4 WHEN 'B' THEN 1 WHEN 'C' THEN 2 WHEN 'D' THEN 3 ELSE NULL END AS PovertyLevelId,  dbo.Clients.strBilling_zip AS BillingZip,  
NULL AS IntakeDate,  
NULL AS IntakeStaff,  
NULL AS IntakeNotes,  
NULL AS ReferredById,  
NULL AS IfRefferedByOther,  
NULL AS PresentingProblem,  
NULL AS PetId,  
NULL AS HouseholdIncome,
CASE dbo.ClientStatsMain.vchSrHousing_nm WHEN 'CC' THEN 11 WHEN 'Champaign- Urbana Rehabilitation and Nursing.' THEN 16 WHEN 'DTU' THEN 1 WHEN 'EOM' THEN 21 WHEN 'EU' THEN 7 WHEN 'EURB' THEN 7 WHEN 'FH' THEN 3 WHEN 'HM' THEN 22 WHEN 'Homer' THEN 22 WHEN 'MA' THEN 20 WHEN 'MHS' THEN 20 WHEN 'NECNWU' THEN 9 WHEN 'NEU' THEN 6 WHEN 'North Logan in Danville IL' THEN 22 WHEN 'NU' THEN 8 WHEN 'Nursing home' THEN NULL WHEN 'NWC' THEN 13 WHEN 'PES' THEN 17 WHEN 'RB' THEN 12 WHEN 'RBM' THEN 12 WHEN 'SCM' THEN 4 WHEN 'SEU' THEN 5 WHEN 'SJO' THEN 18 WHEN 'SU' THEN 2 WHEN 'SV' THEN 16 WHEN 'SVY' THEN 16 WHEN 'SWC' THEN 15 WHEN 'TBR' THEN 19 WHEN 'TPP' THEN 17 WHEN 'WC' THEN 14 WHEN 'WS' THEN 10 ELSE NULL END AS DistrictId 
FROM  dbo.Clients 
FULL  OUTER JOIN dbo.ClientStatsMain ON dbo.Clients.lngClient_id = dbo.ClientStatsMain.lngClient_id