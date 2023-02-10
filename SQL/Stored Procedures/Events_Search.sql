-- =============================================
-- Author: David Dickson
-- Create date: MM/DD/YEAR
-- Description: [Events_Search] for [dbo].[Events]
-- Code Reviewer: Redacted

-- MODIFIED BY: author
-- MODIFIED DATE: M/DD/YEAR
-- Code Reviewer:
-- Note:
-- =============================================

	ALTER PROC [dbo].[Events_Search]
		@PageIndex int
		,@PageSize int
		,@Query nvarchar(50) 
      
      
	AS
  
  
/*---------TEST CODE---------

	DECLARE	@Query nvarchar(50) = 'Los'
		,@PageIndex int = 0
		,@PageSize int = 5;

	EXECUTE [dbo].[Events_Search]
		@PageIndex
		,@PageSize
		,@Query

*/---------TEST CODE---------


BEGIN

	DECLARE @offset int = @pageIndex * @pageSize

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
		,TotalCount = COUNT(1) OVER()
		
	FROM [dbo].[Events] AS e INNER JOIN [dbo].[EventTypes] AS et
	ON e.EventTypeId = et.Id
		INNER JOIN [dbo].[EventStatus] AS es
	ON e.EventStatusId = es.Id
		INNER JOIN [dbo].[Venues] as v
	ON v.Id = e.VenueId
		INNER JOIN [dbo].[Locations] as l
	ON v.LocationId = l.Id
		INNER JOIN [dbo].[LocationTypes] as lt
	ON lt.Id = l.LocationTypeId
		INNER JOIN [dbo].[States] as s
	ON s.Id = l.StateId

	WHERE 
	(
		et.Name LIKE '%' + @Query + '%' OR
		e.Name LIKE '%' + @Query + '%' OR
		e.Summary LIKE '%' + @Query + '%' OR
		v.Name LIKE '%' + @Query + '%' OR
		l.City LIKE '%' + @Query + '%' OR
		s.Name LIKE '%' + @Query + '%' OR
		l.Zip LIKE '%' + @Query + '%' OR
		e.isFree LIKE '%' + @Query + '%' OR
		e.DateStart LIKE '%' + @Query + '%' 
	)

	Order by e.Id DESC
		OFFSET @offSet Rows
		Fetch Next @PageSize Rows ONLY

END
