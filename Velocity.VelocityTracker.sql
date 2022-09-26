--=====================================================================================================
--                                  Velocity Tracker Table
--=====================================================================================================
-- Programmed By:  Jay Florendo		                                       
-- Programed Date:  Sept. 26, 20222                                        
-------------------------------------------------------------------------------------------------------
-- Purpose: Store Velocit Tracker details
--=====================================================================================================

------------------------------------------------------------------------------------
--Drop Table
------------------------------------------------------------------------------------

IF  EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID('Velocity.VelocityTracker') AND type in ('U'))
    
	BEGIN
		PRINT 'Log - Droping Velocity.VelocityTracker...';
		DROP TABLE Velocity.VelocityTracker
	END
GO

------------------------------------------------------------------------------------
--Create Table
------------------------------------------------------------------------------------

PRINT N'Log - Creating Velocity.VelocityTracker...';

CREATE TABLE Velocity.VelocityTracker (

	ID							INT					NOT NULL		IDENTITY(1,1),
	Projects					NVARCHAR(255)		NOT NULL		,
	TaskTitle					NVARCHAR(255)		NULL			,
	TaskDescription				NVARCHAR(255)		NULL			,
	TotalEstimate				INT					NULL			,
	Employee					NVARCHAR(255)		NOT NULL		,
	EstimatedHours				INT					NULL			,
	ActualHours					INT					NULL			

)
GO

------------------------------------------------------------------------------------
--Create Indexes
------------------------------------------------------------------------------------

GO

------------------------------------------------------------------------------------
--Establish Foreign Key References
------------------------------------------------------------------------------------


GO

------------------------------------------------------------------------------------
--Populate Table
------------------------------------------------------------------------------------
GO

------------------------------------------------------------------------------------
--Check/Enforce Foreign Key References
------------------------------------------------------------------------------------

PRINT 'Log - Script Done'
GO