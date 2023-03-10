-- =============================================
-- Author: David Dickson
-- Create date: MM/DD/YEAR
-- Description: [Events_Insert] for dbo.Events inserts into Events,Venues,Locations
-- Code Reviewer: Redacted

-- MODIFIED BY: author
-- MODIFIED DATE: MM/DD/YEAR
-- Code Reviewer:
-- Note:
-- =============================================


ALTER PROC [dbo].[Events_Insert_V2]
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
		,@Id int OUTPUT
				 
AS


/*


	DECLARE @LocationTypeId int = 3
		,@LineOne nvarchar(255) = '1 Facebook Way'
		,@LineTwo nvarchar(255) ='Suite 500'
		,@City nvarchar(255) ='Cuepertino'
		,@Zip nvarchar(50) ='90000'
		,@StateId int = 8
		,@Latitude float =100
		,@Longitude float =100
		,@CreatedBy int	=1
		,@VenueName nvarchar(255) ='Auditorium'
		,@VenueDescription nvarchar(4000) ='Its an auditorium'
		,@VenueUrl nvarchar(255) ='facebook.com'
		,@EventTypeId int =1
		,@Name nvarchar(255) = 'Recruting Fair'
		,@Summary nvarchar(255)='Recruting Fair for diverse hires.'
		,@ShortDescription nvarchar(4000) ='Recruiting Fair is an event where you can find a your next job.'
		,@EventStatusId int = 1
		,@ImageUrl nvarchar(400) = 'https://tinyurl.com/2ykack73'
		,@ExternalSiteUrl nvarchar(400) ='facebook.com'
		,@isFree bit = 'true'
		,@DateStart datetime2(7) ='2022-06-15'
		,@DateEnd datetime2(7) ='2022-06-18'
		,@Id int =0


	EXECUTE [dbo].[Events_Insert]
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
		,@Id OUTPUT

	EXECUTE [dbo].[Events_SelectDetails_ById] @Id


*/


BEGIN
		

	INSERT INTO [dbo].[Locations]
		([LocationTypeId]
		,[LineOne]
		,[LineTwo]
		,[City]
		,[Zip]
		,[StateId]
		,[Latitude]
		,[Longitude]
		,[CreatedBy]
		,[ModifiedBy])
	VALUES
		(@LocationTypeId
		,@LineOne 
		,@LineTwo
		,@City
		,@Zip
		,@StateId
		,@Latitude
		,@Longitude
		,@CreatedBy
		,@CreatedBy)

	DECLARE @LocationId int =SCOPE_IDENTITY()


	INSERT INTO [dbo].[Venues]
		([Name]
		,[Description]
		,[LocationId]
		,[Url]
		,[CreateBy]
		,[ModifiedBy])
	 VALUES
		(@VenueName
		,@VenueDescription
		,@LocationId
		,@VenueUrl
		,@CreatedBy 
		,@CreatedBy)
		
	DECLARE @VenueId int =SCOPE_IDENTITY()


	INSERT INTO [dbo].[Events]
		([EventTypeId]
		,[Name]
		,[Summary]
		,[ShortDescription]
		,[VenueId]
		,[EventStatusId]
		,[ImageUrl]
		,[ExternalSiteUrl]
		,[isFree]
		,[DateStart]
		,[DateEnd]
		,[CreatedBy])
	 VALUES
		(@EventTypeId
		,@Name 
		,@Summary
		,@ShortDescription
		,@VenueId
		,@EventStatusId
		,@ImageUrl
		,@ExternalSiteUrl
		,@isFree
		,@DateStart
		,@DateEnd
		,@CreatedBy)

	SET @Id = SCOPE_IDENTITY()


END 
