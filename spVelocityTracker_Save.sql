SET NOCOUNT ON;

------------------------------------------------------------------------------------
-- Drop Stored Procedure if it already exists
------------------------------------------------------------------------------------
IF EXISTS ( SELECT  *
			FROM    sys.objects
			WHERE   object_id = OBJECT_ID(N'Velocity.spVelocityTracker_Save')
					AND type IN ( N'P', N'PC' ) )
	DROP PROCEDURE Velocity.spVelocityTracker_Save;
GO
------------------------------------------------------------------------------------
--Create Stored Procedure
------------------------------------------------------------------------------------
CREATE PROCEDURE Velocity.spVelocityTracker_Save

	@Projects					NVARCHAR(255)		,	
	@TaskTitle					NVARCHAR(255)	   ,
	@TaskDescription			NVARCHAR(255)	   ,
	@TotalEstimate				INT				   ,
	@Employee					NVARCHAR(255)	   ,
	@EstimatedHours				INT				   ,
	@ActualHours				INT				

AS 
BEGIN

	DECLARE @Now DATETIME = GETDATE()
	DECLARE @Record_Count INT = 0	

	-- Check to see if we already have an existing record
	SET @Record_Count = (SELECT COUNT(*) FROM Velocity.VelocityTracker WHERE Projects = @Projects  AND Employee = @Employee)

	IF @Record_Count = 0 BEGIN

		-- Insert
		INSERT INTO
			Velocity.VelocityTracker (Projects, TaskTitle, TaskDescription, TotalEstimate, Employee, EstimatedHours, ActualHours)
		VALUES
			(@Projects, @TaskTitle, @TaskDescription, @TotalEstimate, @Employee, @EstimatedHours, @ActualHours)

	END ELSE BEGIN
		
		-- Update
		UPDATE
			Velocity.VelocityTracker
		SET		
			TaskTitle			= @TaskTitle		,
			TaskDescription		= @TaskDescription		,
			TotalEstimate		= @TotalEstimate		,
			EstimatedHours		= @EstimatedHours		,
			ActualHours		= @ActualHours

		WHERE
			Projects = @Projects
		AND
			Employee = @Employee

	END

			
END
GO

