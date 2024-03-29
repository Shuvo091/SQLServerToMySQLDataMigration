﻿SELECT 
  lngEmp_id AS Id, 
  vchEmpFirst_nm AS FirstName, 
  vchEmpLast_nm AS LastName, 
  CASE strEmpStatus_cd WHEN 'O' THEN 1 WHEN 'C' THEN 2 WHEN 'H' THEN 3 ELSE NULL END AS StatusId, 
  txtEmpTips_desc AS Tips, 
  vchEmp_add AS HomeAddress, 
  vchEmp_city AS City, 
  CASE strEmp_state WHEN 'AK' THEN 2 WHEN 'AL' THEN 1 WHEN 'AR' THEN 4 WHEN 'AZ' THEN 3 WHEN 'CA' THEN 5 WHEN 'CO' THEN 6 WHEN 'CT' THEN 7 WHEN 'FL' THEN 9 WHEN 'GA' THEN 10 WHEN 'HI' THEN 11 WHEN 'IA' THEN 15 WHEN 'ID' THEN 12 WHEN 'II' THEN 13 WHEN 'IL' THEN 13 WHEN 'IN' THEN 14 WHEN 'IO' THEN 15 WHEN 'KS' THEN 16 WHEN 'KY' THEN 17 WHEN 'MA' THEN 21 WHEN 'ME' THEN 19 WHEN 'MI' THEN 22 WHEN 'MN' THEN 23 WHEN 'MO' THEN 25 WHEN 'NB' THEN 27 WHEN 'NC' THEN 33 WHEN 'NE' THEN 27 WHEN 'NJ' THEN 30 WHEN 'NM' THEN 31 WHEN 'NV' THEN 28 WHEN 'NY' THEN 32 WHEN 'OH' THEN 35 WHEN 'OK' THEN 36 WHEN 'OR' THEN 37 WHEN 'q' THEN NULL WHEN 'SC' THEN 40 WHEN 'TN' THEN 42 WHEN 'TX' THEN 43 WHEN 'VA' THEN 46 WHEN 'WA' THEN 47 WHEN 'WI' THEN 49 ELSE NULL END AS StateId, 
  strEmp_zip AS ZipCode, 
  NULL AS CountyId, 
  NULL AS Email, 
  strEmpHome_ph AS HomePhone, 
  NULL AS SecondaryHomePhone, 
  NULL AS CellPhone, 
  strEmpWork_ph AS WorkPhone, 
  datEmpBirth_dt AS Birthdate, 
  CASE strEmpSex_cd WHEN 'M' THEN 1 WHEN 'F' THEN 2 ELSE NULL END AS GenderId, 
  CASE vchEmpRace_nm WHEN 'White' THEN 6 WHEN 'African American' THEN 3 WHEN 'Asian' THEN 1 WHEN 'Philipino' THEN 5 WHEN 'Latin American' THEN 4 ELSE NULL END AS RaceId, 
  NULL AS SocialSecurity, 
  datInsurance_expires AS InsuranceExpiration, 
  datDriversLicense_expires AS DriversLicenseExpiration, 
  CAST(smyEmpPayRoll_rate as FLOAT) AS PayrollRate 
FROM 
  dbo.Employees
