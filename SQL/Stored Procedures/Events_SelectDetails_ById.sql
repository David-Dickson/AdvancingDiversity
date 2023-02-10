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



ALTER proc [dbo].[Events_SelectDetails_ById] 
  @Id int
					

as
/*
	
	Declare @Id int = 18
		

	Execute [dbo].[Events_SelectDetails_ById] 
  @Id
								

*/

Begin



	Select e.Id
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

		
		From dbo.Events as e inner join dbo.EventTypes as et
		on e.EventTypeId = et.Id
		inner join dbo.EventStatus as es
		on e.EventStatusId = es.Id
		inner join dbo.Venues as v
		on v.Id = e.VenueId
		inner join dbo.Locations as l
		on v.LocationId = l.Id
		inner join dbo.LocationTypes as lt
		on lt.Id = l.LocationTypeId
		inner join dbo.States as s
		on s.Id = l.StateId

		Where e.Id = @Id
	

End
