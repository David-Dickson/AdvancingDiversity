-- =============================================
-- Author: David Dickson
-- Create date: MM/DD/YEAR
-- Description: [Events_SelectDetails_ById] for dbo.Events
-- Code Reviewer: Redacted

-- MODIFIED BY: author
-- MODIFIED DATE: M/DD/YEAR
-- Code Reviewer:
-- Note:
-- =============================================


	ALTER PROC [dbo].[Events_SelectDetails_ById] 
  		@Id int
					

AS


/*
	
	DECLARE @Id int = 18
		

	EXECUTE [dbo].[Events_SelectDetails_ById] 
  		@Id
								

*/

BEGIN


	SELECT e.Id
		,e.EventTypeId
		,et.Name as EventType
		,e.Name
		,e.Summary
		,e.ShortDescription
		,e.VenueId
		,v.Name as VenueName
		,v.Description as VenueDescription
		,v.Url as VenueUrl
		,l.Id
		,l.LocationTypeId
		,lt.Name as VenueType
		,l.LineOne
		,l.LineTwo
		,l.City
		,l.StateId
		,s.Name as State
		,l.Zip as ZipCode
		,l.Latitude
		,l.Longitude
		,l.DateCreated
		,l.DateModified
		,l.CreatedBy
		,l.ModifiedBy
		,e.Id as EventStatusId
		,es.Name as EventStatus
		,e.ImageUrl
		,e.ExternalSiteUrl
		,e.isFree
		,e.DateStart
		,e.DateEnd
		,e.CreatedBy

		
	FROM dbo.Events AS e INNER JOIN [dbo].[EventTypes] AS et
	ON e.EventTypeId = et.Id
		INNER JOIN [dbo].[EventStatus] AS es
	ON e.EventStatusId = es.Id
		INNER JOIN [dbo].[Venues] AS v
	ON v.Id = e.VenueId
		INNER JOIN [dbo].[Locations] AS l
	ON v.LocationId = l.Id
		INNER JOIN [dbo].[LocationTypes] AS lt
	ON lt.Id = l.LocationTypeId
		INNER JOIN [dbo].[States] AS s
	ON s.Id = l.StateId

	WHERE e.Id = @Id
	

END
