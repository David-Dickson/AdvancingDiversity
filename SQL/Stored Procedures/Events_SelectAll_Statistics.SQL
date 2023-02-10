-- =============================================
-- Author: David Dickson
-- Create date: MM/DD/YEAR
-- Description: [dbo].[Events_SelectAll_Statistics] for dbo.Events inner join dbo.EventStatus inner join dbo.EventTypes
-- Code Reviewer: Redacted

-- MODIFIED BY: author
-- MODIFIED DATE: MM/DD/YEAR
-- Code Reviewer: 
-- Note:
-- =============================================

ALTER PROC [dbo].[Events_SelectAll_Statistics]

AS


/* ---- TEST CODE ----

	EXECUTE [dbo].[Events_SelectAll_Statistics]

*/ ---- END TEST CODE ----


BEGIN


	SELECT	t.Name AS EventType
		,s.Name AS EventStatus
		,e.IsFree
		,COUNT(s.Name) AS EventsQuantity
		,EventDateStart =	
		(
		SELECT DISTINCT	Name							
		,CAST(e.DateStart AS Date) AS DateStart									
		FROM	[dbo].[Events]
		WHERE Name = e.Name
		FOR JSON AUTO
		)
	FROM	dbo.Events AS e inner join [dbo].[EventStatus] AS s
	ON 	e.EventStatusId = s.Id
		INNER JOIN dbo.EventTypes AS t
	ON 	e.EventTypeId = t.Id
			
	GROUP BY t.Name
		,s.Name
		,e.IsFree
		,e.Name
		,e.DateStart
END
