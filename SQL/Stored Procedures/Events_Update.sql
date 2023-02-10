-- =============================================
-- Author: David Dickson
-- Create date: MM/DD/YEAR
-- Description: [Events_Update] for dbo.Events inserts into Events,Venues,Locations
-- Code Reviewer: redacted

-- MODIFIED BY: author
-- MODIFIED DATE: MM/DD/YEAR
-- Code Reviewer:
-- Note:
-- =============================================


	ALTER 	PROC [dbo].[Events_Update]
		@LocationTypeId int
		,@LineOne nvarchar(255)
		,@LineTwo nvarchar(255)
		,@City nvarchar(255)
		,@Zip nvarchar(50)
		,@StateId int
		,@Latitude float
		,@Longitude float
		,@CreatedBy int	
		,@VenueName nvarchar(255)
		,@VenueDescription nvarchar(4000)
		,@VenueUrl nvarchar(255)
		,@EventTypeId int
		,@Name nvarchar(255)
		,@Summary nvarchar(255)
		,@ShortDescription nvarchar(4000)
		,@EventStatusId int
		,@ImageUrl nvarchar(400)
		,@ExternalSiteUrl nvarchar(400)
		,@isFree bit
		,@DateStart datetime2(7)
		,@DateEnd datetime2(7)
		,@Id int
AS


/* -----------TEST CODE-----------------


	DECLARE @LocationTypeId int = 3
		,@LineOne nvarchar(255) = '1 Facebook Way'
		,@LineTwo nvarchar(255) ='Suite 500'
		,@City nvarchar(255) ='Cupertino'
		,@Zip nvarchar(50) ='90000'
		,@StateId int = 8
		,@Latitude float =100
		,@Longitude float =100
		,@CreatedBy int	=2
		,@VenueName nvarchar(255) ='Auditorium'
		,@VenueDescription nvarchar(4000) ='Its an auditorium'
		,@VenueUrl nvarchar(255) ='facebook.com'
		,@EventTypeId int =1
		,@Name nvarchar(255) = 'RECRUTING Fair'
		,@Summary nvarchar(255)='Recruting Fair for diverse hires.'
		,@ShortDescription nvarchar(4000) ='Recruting Fair is an event where you can find a job.'
		,@EventStatusId int = 1
		,@ImageUrl nvarchar(400) = 'https://tinyurl.com/2ykack73'
		,@ExternalSiteUrl nvarchar(400) ='facebook.com'
		,@isFree bit = 'false'
		,@DateStart datetime2(7) ='2022-06-15'
		,@DateEnd datetime2(7) ='2022-06-18'
		,@Id int =18


	EXECUTE [dbo].[Events_Update] 
		@LocationTypeId 
		,@LineOne 
		,@LineTwo
		,@City
		,@Zip 
		,@StateId 
		,@Latitude 
		,@Longitude
		,@CreatedBy 	
		,@VenueName 
		,@VenueDescription 
		,@VenueUrl 
		,@EventTypeId
		,@Name 
		,@Summary
		,@ShortDescription
		,@EventStatusId
		,@ImageUrl
		,@ExternalSiteUrl
		,@isFree
		,@DateStart
		,@DateEnd
		,@Id 

	EXECUTE [dbo].[Events_SelectDetails_ById] 
		@Id


-------------------------------------*/


BEGIN

	DECLARE @DateModified datetime2 = getutcdate()

	UPDATE [dbo].[Locations]
	SET [LocationTypeId] = @LocationTypeId
		,[LineOne] = @LineOne
		,[LineTwo] = @LineTwo
		,[City] = @City
		,[Zip] = @Zip
		,[StateId] = @StateId
		,[Latitude] = @Latitude
		,[Longitude] = @Longitude
		,[DateModified] = @DateModified
		,[ModifiedBy] = @CreatedBy
	WHERE Id= (SELECT LocationId
	FROM dbo.Venues AS v 
		INNER JOIN [dbo].[Events] AS e
	ON v.Id = e.VenueId
		WHERE e.Id =@Id)

	

	UPDATE [dbo].[Venues]
	SET 	[Name] = @VenueName
		,[Description] = @VenueDescription
		,[Url] = @VenueUrl
		,[ModifiedBy] = @CreatedBy
		,[DateModified] = @DateModified
		
	WHERE Id =(SELECT VenueId 
	FROM [dbo].[Events] AS e
	WHERE e.Id = @Id)


		
		
	UPDATE [dbo].[Events]
	SET	 [EventTypeId] = @EventTypeID
		,[Name] = @Name
		,[Summary] = @Summary
		,[ShortDescription] = @ShortDescription
		,[EventStatusId] = @EventStatusId
		,[ImageUrl] = @ImageUrl
		,[ExternalSiteUrl] = @ExternalSiteUrl
		,[isFree] = @isFree
		,[DateModified] = @DateModified
		,[DateStart] = @DateStart
		,[DateEnd] = @DateEnd
	 WHERE @Id = Id



End
