-- =============================================
    -- Author: David Dickson
    -- Create date: M/DD/YEAR
    -- Description: Insert records for dbo.Newsletters_Insert with the parameters listed below
    -- Code Reviewer: Jorge Calderon

    -- MODIFIED BY: Author
    -- MODIFIED DATE: M/DD/YEAR
    -- Code Reviewer:
    -- Note:
-- ==============================================

ALTER   PROC [dbo].[Newsletters_Insert]
					 @TemplateId int
					,@Name nvarchar(100)
					,@CoverPhoto nvarchar(255)
					,@DateToPublish datetime2(7)
					,@DateToExpire datetime2(7)
					,@CreatedBy int
					,@Id int OUTPUT

AS

/*
------ TEST CODE ------

			DECLARE @Id int = 0
					,@TemplateId int = 52
					,@Name nvarchar(100) = 'Test Once '
					,@CoverPhoto nvarchar(100) = 'https://bit.ly/3Qww6Cb'
					,@DateToPublish datetime2(7) = '2022-09-18'
					,@DateToExpire datetime2(7) = '2022-10-23'
					,@CreatedBy int = 41

			EXECUTE dbo.Newsletters_Insert 					 
					 @TemplateId
					,@Name
					,@CoverPhoto
					,@DateToPublish
					,@DateToExpire
					,@CreatedBy
					,@Id OUTPUT 

			SELECT * 
			FROM dbo.Newsletters
			
			SELECT * 
			FROM dbo.NewsletterTemplates

			SELECT * 
			FROM dbo.Users

			
------ END TEST CODE ------
*/

BEGIN 
		  
			
			INSERT INTO [dbo].[Newsletters]
					   ([TemplateId]
					   ,[Name]
					   ,[CoverPhoto]
					   ,[DateToPublish]
					   ,[DateToExpire]
					   ,[CreatedBy])
				 VALUES
					   (@TemplateId,
              @Name,
              @CoverPhoto,
              @DateToPublish,
              @DateToExpire,
              @CreatedBy
              )
				
			
				SET @Id = SCOPE_IDENTITY()

END
