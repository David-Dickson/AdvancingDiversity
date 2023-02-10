-- =============================================
    -- Author: David Dickson
    -- Create date: MM/DD/YEAR
    -- Description: Update for the given parameters listed of Newsletters
    -- Code Reviewer: Redacted

    -- MODIFIED BY: Author
    -- MODIFIED DATE: MM/DD/YEAR
    -- Code Reviewer:
    -- Note:
-- ==============================================

ALTER   PROC [dbo].[Newsletters_Update]
		@TemplateId int
		,@Name nvarchar(100)
		,@CoverPhoto nvarchar(255)
		,@DateToPublish datetime2(7)
		,@DateToExpire datetime2(7)
		,@CreatedBy int
		,@Id int OUTPUT
	

AS 

/* ------ TEST CODE ------
			
	DECLARE  @Id int = 19
		,@TemplateId int = 18
		,@Name nvarchar(100) = 'Spring Technology Newsletter'
		,@CoverPhoto nvarchar(100) = 'https://bit.ly/3Qww6Cb'
		,@DateToPublish datetime2(7) = '2022-04-15'
		,@DateToExpire datetime2(7) = '2022-06-15'
		,@CreatedBy int = 22

	EXECUTE [dbo].[Newsletters_Update]					 
		@TemplateId
		,@Name
		,@CoverPhoto
		,@DateToPublish
		,@DateToExpire
		,@CreatedBy
		,@Id OUTPUT 

	SELECT * 
		FROM [dbo].[Newsletters]

	SELECT * 
		FROM [dbo].[NewsletterTemplates]

	SELECT * 
		FROM dbo.Users

------ END TEST CODE ------
*/


BEGIN


	DECLARE @DateNow datetime2(7) = GETUTCDATE()

	UPDATE [dbo].[Newsletters]
	
	SET 	
		[TemplateId] = @TemplateId
		,[Name] = @Name
		,[CoverPhoto] = @CoverPhoto
		,[DateToPublish] = @DateToPublish
		,[DateToExpire] = @DateToExpire
		,[CreatedBy] = @CreatedBy
		,[DateModified] = @DateNow
		
	 WHERE Id = @Id 


 END 
