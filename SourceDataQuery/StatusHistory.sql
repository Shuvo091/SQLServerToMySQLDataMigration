﻿SELECT
  lngStatus_id AS Id, 
  CASE strStatus_fl WHEN 'O' THEN 1 WHEN 'C' THEN 2 WHEN 'H' THEN 3 ELSE NULL END AS StatusId, 
  datStatus_dt AS Date, 
  CASE WHEN vchStatus_desc LIKE 'Referral - 211 Information Hotline' THEN 1 WHEN vchStatus_desc LIKE 'Referral - Anonymous' THEN 2 WHEN vchStatus_desc LIKE 'Referral - Assisted Living' THEN 3 WHEN vchStatus_desc LIKE 'Referral - Carle MCCD' THEN 4 WHEN vchStatus_desc LIKE 'Referral - CCU -  Client Requested SRC' THEN 5 WHEN vchStatus_desc LIKE 'Referral - CCU - Assigned By CCU' THEN 6 WHEN vchStatus_desc LIKE 'Referral - Client Before' THEN 7 WHEN vchStatus_desc LIKE 'Referral - Cumberland Case Coordination Unit' THEN 8 WHEN vchStatus_desc LIKE 'Referral - Discharge Social Services- Carle' THEN 9 WHEN vchStatus_desc LIKE '%Referral - Discharge Social Services- Provena%' THEN 10 WHEN vchStatus_desc LIKE '%Referral - Doctor%' THEN 11 WHEN vchStatus_desc LIKE 'Referral - Elder Abuse Report' THEN 12 WHEN vchStatus_desc LIKE 'Referral - Family or Friend' THEN 13 WHEN vchStatus_desc LIKE 'Referral - Home Health- Carle' THEN 14 WHEN vchStatus_desc LIKE 'Referral - Home Health- Other' THEN 15 WHEN vchStatus_desc LIKE 'Referral - Home Health- Provena' THEN 16 WHEN vchStatus_desc LIKE 'Referral - IDHS' THEN 17 WHEN vchStatus_desc LIKE 'Referral - IDOA' THEN 18 WHEN vchStatus_desc LIKE 'Referral - Media' THEN 19 WHEN vchStatus_desc LIKE 'Referral - Mental Health Center' THEN 20 WHEN vchStatus_desc LIKE 'Referral - Nursing Home' THEN 21 WHEN vchStatus_desc LIKE '  Nursing Home' THEN 21 WHEN vchStatus_desc LIKE 'Referral - ORS' THEN 22 WHEN vchStatus_desc LIKE '   OR referral' THEN 22 WHEN vchStatus_desc LIKE ' O R referral' THEN 22 WHEN vchStatus_desc LIKE ' OR referral' THEN 22 WHEN vchStatus_desc LIKE '%Referral - Other%' THEN 23 WHEN vchStatus_desc LIKE 'Referral - None' THEN 23 WHEN vchStatus_desc LIKE 'Referral - Police/Fire' THEN 24 WHEN vchStatus_desc LIKE 'Referral - PR Event' THEN 25 WHEN vchStatus_desc LIKE 'Referral - Public Health' THEN 26 WHEN vchStatus_desc LIKE 'Referral - Senior Housing' THEN 27 WHEN vchStatus_desc LIKE 'Referral - Social Service Agency' THEN 28 WHEN vchStatus_desc LIKE 'Referral - SRC Program' THEN 29 WHEN vchStatus_desc LIKE 'Referral - Veteran' THEN 30 WHEN vchStatus_desc LIKE 'Referral - Web Page' THEN 31 WHEN vchStatus_desc LIKE 'Referral - Yellow Pages' THEN 32 WHEN vchStatus_desc LIKE '%Referral - CCP%' THEN 53 WHEN vchStatus_desc LIKE '%Referral%' THEN 23 WHEN vchStatus_desc LIKE 'Termination Reason - Agency Initiated' THEN 33 WHEN vchStatus_desc LIKE 'Termination Reason - ANE Administrative Closure' THEN 34 WHEN vchStatus_desc LIKE 'Termination Reason - ANE Unsubstantiated' THEN 35 WHEN vchStatus_desc LIKE 'Termination Reason - Deceased' THEN 36 WHEN vchStatus_desc LIKE '%Deceased%' THEN 36 WHEN vchStatus_desc LIKE 'Termination Reason - Dissatisfied with Service (Financial)' THEN 37 WHEN vchStatus_desc LIKE 'Termination Reason - Dissatisfied with Service (Quality)' THEN 38 WHEN vchStatus_desc LIKE 'Termination Reason - Dissatisfied with Service (Schedule)' THEN 39 WHEN vchStatus_desc LIKE 'Termination Reason - Moved Out of Area' THEN 40 WHEN vchStatus_desc LIKE 'Termination Reason - moved out of state' THEN 40 WHEN vchStatus_desc LIKE 'Termination Reason - Moved to Assistive Setting' THEN 41 WHEN vchStatus_desc LIKE 'Termination Reason - Needs Met' THEN 42 WHEN vchStatus_desc LIKE ' closing-needs met' THEN 42 WHEN vchStatus_desc LIKE ' Abuse Substantiated. Clt no longer at risk for abuse' THEN 42 WHEN vchStatus_desc LIKE 'Termination Reason - Service Completed' THEN 42 WHEN vchStatus_desc LIKE '%Service Completed%' THEN 42 WHEN vchStatus_desc LIKE 'Termination Reason - Nursing Home' THEN 43 WHEN vchStatus_desc LIKE '%Nursing Home%' THEN 43 WHEN vchStatus_desc LIKE 'Termination Reason - Nursing Home Rehabilitation' THEN 44 WHEN vchStatus_desc LIKE 'Termination Reason - Refused Service' THEN 45 WHEN vchStatus_desc LIKE 'Termination Reason - Rehab Client  (Resumed Independence)' THEN 46 WHEN vchStatus_desc LIKE 'Termination Reason - Service Not Started' THEN 47 WHEN vchStatus_desc LIKE 'Termination Reason - Transferred to another SRC program' THEN 48 WHEN vchStatus_desc LIKE ' Case closed- Case transferred to vermillion county.' THEN 48 WHEN vchStatus_desc LIKE 'Termination Reason - Vendor Transfer (CCP) (Financial)' THEN 49 WHEN vchStatus_desc LIKE 'Termination Reason - Vendor Transfer (CCP) (Quality)' THEN 50 WHEN vchStatus_desc LIKE 'Termination Reason - Vendor Transfer (CCP) (Schedule)' THEN 51 WHEN vchStatus_desc LIKE 'Termination Reason - Vendor Transfer' THEN 54 WHEN vchStatus_desc LIKE '%Termination Reason - On hold for over 60 days%' THEN 55 WHEN vchStatus_desc LIKE '%Termination Reason - Other%' THEN 56 WHEN vchStatus_desc LIKE '%Termination%' THEN 56 WHEN vchStatus_desc LIKE '%Terminated%' THEN 56 WHEN vchStatus_desc LIKE '%Termed%' THEN 56 WHEN vchStatus_desc LIKE '%Cancel%' THEN 56 WHEN vchStatus_desc LIKE '%Cancekl%' THEN 56 WHEN vchStatus_desc LIKE 'Hold' THEN 52 WHEN vchStatus_desc LIKE '%Hold%' THEN 52 WHEN strStatus_fl LIKE 'O' THEN 23 WHEN strStatus_fl LIKE 'C' THEN 56 WHEN strStatus_fl LIKE 'H' THEN 52 ELSE NULL END AS StatusReasonId, 
  vchStatus_desc, 
  null AS EnteredById, 
  datStatus_dt AS EnteredOn, 
  vchService_nm, 
  CASE vchService_nm WHEN 'caSelfNeflect' THEN lngClient_id WHEN 'caSeniorProtect' THEN lngClient_id ELSE NULL END AS APSRecordId, 
  CASE vchService_nm WHEN 'HomeCare' THEN lngClient_id ELSE NULL END AS HomeCareRecordId, 
  CASE vchService_nm WHEN 'MealsOnWheels' THEN lngClient_id ELSE NULL END AS MealsOnWheelsRecordId, 
  CASE vchService_nm WHEN 'Transportation' THEN lngClient_id ELSE NULL END AS TransportationRecordId, 
  CASE vchService_nm WHEN 'caregiver' THEN lngClient_id WHEN 'caOutreach' THEN lngClient_id WHEN 'caCounseling' THEN lngClient_id WHEN 'caOutreachcaOutreach' THEN lngClient_id ELSE NULL END AS CounselingRecordId, 
  (
    SELECT 
      1 
    FROM 
      dbo.ClientServiceStatus t2 
    WHERE 
      t2.lngStatus_id = t1.lngStatus_id 
      AND t2.vchStatus_desc LIKE '%pearls%'
  ) AS ProgramInfoId, 
  CASE vchService_nm WHEN 'caregiver' THEN 3 WHEN 'caOutreach' THEN 1 WHEN 'caCounseling' THEN 2 WHEN 'caOutreachcaOutreach' THEN 2 ELSE NULL END AS SubServiceId 
FROM 
  dbo.ClientServiceStatus t1
