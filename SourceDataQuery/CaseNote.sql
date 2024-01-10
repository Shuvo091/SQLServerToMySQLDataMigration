SELECT 
  CAST( row_number() OVER ( ORDER BY Enabled ) AS INT ) AS Id, *
FROM 
  (
    SELECT 
      CAST(1 AS BIT) AS Enabled, 
      datEmpNote_dt AS Date, 
      datEmpNote_tm AS Time, 
      CASE vchEmpNoteVia_desc WHEN 'in-service' THEN 3 WHEN 'HCA' THEN 3 WHEN 'viocemail' THEN 1 WHEN 'Quarterly Conf.' THEN 2 WHEN 'Home Visit' THEN 3 WHEN 'Case Manager' THEN 4 WHEN 'voice mail' THEN 1 WHEN 'timesheets' THEN 7 WHEN 'letter' THEN 7 WHEN '3-Mon Eval' THEN 8 WHEN 'timesheet' THEN 5 WHEN 'visit' THEN 3 WHEN 'Qtrly. Conf.' THEN 2 WHEN 'Mail' THEN 8 WHEN 'Annual' THEN 8 WHEN 'Perf. Eval.' THEN 8 WHEN 'casnotes' THEN 4 WHEN 'notes' THEN 4 WHEN 'Preparation' THEN 7 WHEN 'Qtr. Conf. 4th' THEN 2 WHEN 'casenotes' THEN 4 WHEN 'timesheet/conference' THEN 2 WHEN ' timesheet' THEN 5 WHEN 'voicemail' THEN 1 WHEN 'Concern Form' THEN 8 WHEN 'msg' THEN 1 WHEN 'staff mtg' THEN 8 WHEN 'Page' THEN 1 WHEN 'case notes' THEN 4 WHEN 'Nurse' THEN 5 WHEN 'Conference' THEN 2 WHEN 'Phone' THEN 1 WHEN 'paged' THEN 1 WHEN 'message' THEN 1 WHEN 'vm' THEN 1 WHEN 'email/casenotes' THEN 4 WHEN 'Msge' THEN 8 WHEN 'caseotes' THEN 4 WHEN 'record review' THEN 8 WHEN 'email' THEN 8 WHEN 'timesheet/casenotes' THEN 4 WHEN 'office visit' THEN 8 WHEN 'careplan' THEN 8 WHEN 'Qtr. Conf.' THEN 2 WHEN 'memo' THEN 8 WHEN 'Face-To-Face' THEN 6 WHEN 'Quarterly Conf' THEN 2 WHEN 'office' THEN 8 ELSE NULL END AS InfoViaId, 
      CASE bytEmpNoteToFrom_cd WHEN 1 THEN 0 WHEN 2 THEN 1 END AS ToOrFrom, 
      CASE vchEmpNoteAbout_desc WHEN 'HCA' THEN 1 WHEN 'TLC staff' THEN 1 WHEN 'Case Manager' THEN 4 WHEN 'Director' THEN 1 WHEN 'Family Member' THEN 5 WHEN 'Scheduler' THEN 1 WHEN 'HomeMaker' THEN 3 WHEN 'Staff' THEN 1 WHEN 'PM' THEN 6 WHEN 'files' THEN 6 WHEN 'other' THEN 6 WHEN 'school' THEN 6 WHEN 'Client' THEN 2 WHEN 'friend' THEN 6 WHEN 'daycare' THEN 6 WHEN 'Lead HCA' THEN 1 WHEN 'Clients' THEN 2 WHEN 'volunteer' THEN 1 WHEN 'JAW' THEN 6 WHEN 'Vermillion Cty' THEN 6 WHEN 'FILE' THEN 6 WHEN 'CCU' THEN 6 WHEN 'filed' THEN 6 WHEN 'Manor Care' THEN 6 WHEN 'E/C' THEN 6 ELSE NULL END AS SourceId, 
      CAST(
        txtEmpCaseNote_desc AS VARCHAR(MAX)
      ) AS Note, 
      datEmpNote_dt AS EnteredOn, 
      lngEmp_id AS StaffId, 
      NULL AS HomeCareRecordId, 
      NULL AS MealsOnWheelsRecordId, 
      NULL AS TransportationRecordId, 
      NULL AS CounselingRecordId, 
      NULL AS APSRecordId 
    FROM 
      dbo.EmpCaseNotes 
    UNION ALL
    SELECT 
      CAST(1 AS BIT) AS Enabled, 
      datNote_dt AS Date, 
      datNote_tm AS Time, 
      CASE vchNoteVia_desc WHEN 'Phone' THEN 1 WHEN 'Conference' THEN 2 WHEN '1/4Conference' THEN 2 WHEN 'Qtrly. Conf.' THEN 2 WHEN 'teleconference' THEN 2 WHEN 'Home Visit' THEN 3 WHEN 'visit' THEN 3 WHEN 'Case Manager' THEN 4 WHEN 'Nurse' THEN 5 WHEN 'Face-to-Face' THEN 6 WHEN 'Preparation' THEN 7 ELSE NULL END AS InfoViaId, 
      CASE bytNoteToFrom_cd WHEN 1 THEN 0 WHEN 2 THEN 1 END AS ToOrFrom, 
      CASE vchNoteAbout_desc WHEN 'Staff' THEN 1 WHEN 'Client' THEN 2 WHEN 'Homemaker' THEN 3 WHEN 'Case Manager' THEN 4 WHEN 'Family Member' THEN 5 WHEN 'Client daughter' THEN 5 ELSE NULL END AS SourceId, 
      CAST(
        txtCaseNote_desc AS VARCHAR(MAX)
      ) AS Note, 
      datNote_dt AS EnteredOn, 
      NULL AS StaffId, 
      lngClient_id AS HomeCareRecordId, 
      NULL AS MealsOnWheelsRecordId, 
      NULL AS TransportationRecordId, 
      NULL AS CounselingRecordId, 
      NULL AS APSRecordId 
    FROM 
      dbo.homeCaseNotes
      UNION ALL
	SELECT 
	  CAST(1 AS BIT) AS Enabled, 
	  datNote_dt AS Date,
	  datNote_tm AS Time,
	  CASE vchNoteVia_desc WHEN 'Phone' THEN 1 WHEN 'Conference' THEN 2 WHEN '1/4Conference' THEN 2 WHEN 'Qtrly. Conf.' THEN 2 WHEN 'teleconference' THEN 2 WHEN 'Home Visit' THEN 3 WHEN 'visit' THEN 3 WHEN 'Case Manager' THEN 4 WHEN 'Nurse' THEN 5 WHEN 'Face-to-Face' THEN 6 WHEN 'Preparation' THEN 7 ELSE NULL END AS InfoViaId, 
	  CASE bytNoteToFrom_cd WHEN 1 THEN 0 WHEN 2 THEN 1 END AS ToOrFrom, 
	  CASE vchNoteAbout_desc WHEN 'Staff' THEN 1 WHEN 'Client' THEN 2 WHEN 'Homemaker' THEN 3 WHEN 'Case Manager' THEN 4 WHEN 'Family Member' THEN 5 WHEN 'Client daughter' THEN 5 ELSE NULL END AS SourceId,
	  CAST(
		txtCaseNote_desc AS VARCHAR(MAX)
	  ) AS Note, 
	  datNote_dt AS EnteredOn, 
	  NULL AS StaffId, 
	  NULL AS HomeCareRecordId, 
	  CASE lngClient_id WHEN 0 THEN NULL ELSE lngClient_id END AS MealsOnWheelsRecordId, 
	  NULL AS TransportationRecordId, 
	  NULL AS CounselingRecordId, 
	  NULL AS APSRecordId
	FROM 
	  dbo.mealsCaseNotes
	UNION ALL
	SELECT 
	  CAST(1 AS BIT) AS Enabled, 
	  datNote_dt AS Date, 
	  datNote_tm AS Time,
	  CASE vchNoteVia_desc WHEN 'Phone' THEN 1 WHEN '355-5734' THEN 1 WHEN '4/27' THEN 1 WHEN 'Conference' THEN 2 WHEN 'Home Visit' THEN 3 WHEN 'visit' THEN 3 WHEN 'Case Manager' THEN 4 WHEN 'Nurse' THEN 5 WHEN 'Face-To-Face' THEN 6 WHEN 'Preparation' THEN 7 ELSE NULL END AS InfoViaId, 
	  CASE bytNoteToFrom_cd WHEN 1 THEN 0 WHEN 2 THEN 1 ELSE NULL END AS ToOrFrom, 
	  CASE vchNoteAbout_desc WHEN 'Staff' THEN 1 WHEN 'Client' THEN 2 WHEN 'Homemaker' THEN 3 WHEN 'Case Manager' THEN 4 WHEN 'Family Member' THEN 5 WHEN 'Client daughter' THEN 5 ELSE NULL END AS SourceId,
	  CAST(
		txtCaseNote_desc AS VARCHAR(MAX)
	  ) AS Note, 
	  datNote_dt AS EnteredOn, 
	  NULL AS StaffId, 
	  NULL AS HomeCareRecordId, 
	  NULL AS MealsOnWheelsRecordId, 
	  lngClient_id AS TransportationRecordId, 
	  NULL AS CounselingRecordId, 
	  NULL AS APSRecordId 
	FROM 
	  dbo.transportCaseNotes
  ) united_table
