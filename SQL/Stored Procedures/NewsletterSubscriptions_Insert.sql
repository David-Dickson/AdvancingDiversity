-- =============================================
    -- Author: David Dickson
    -- Create date: MM/DD/YEAR
    -- Description: Insert for [dbo].[NewsletterSubscriptions]
    -- Code Reviewer: Redacted 

    -- MODIFIED BY: Author
    -- MODIFIED DATE: MM/DD/YEAR
    -- Code Reviewer:
    -- Note:
-- ==============================================

ALTER PROC [dbo].[NewsletterSubscriptions_Insert]
		@Email nvarchar(255)
                ,@IsSubscribed bit
                ,@Id int OUTPUT
				
AS

/*---------------TEST CODE--------------
		

	SELECT * 
		FROM [dbo].[NewsletterSubscriptions]

	DECLARE @Id int = 0
	DECLARE @Email nvarchar(255) = 'Test32@gmail.com'
		,@IsSubscribed bit = 1
		
	EXECUTE [dbo].[NewsletterSubscriptions_Insert] 
		@Email
		,@IsSubscribed
		,@Id OUTPUT
													
	SELECT * 
		FROM [dbo].[NewsletterSubscriptions]
	
---------------------------------------*/

BEGIN

	INSERT INTO [dbo].[NewsletterSubscriptions]
		[Email]
		,[IsSubscribed]
	VALUES
		@Email
		,@IsSubscribed
		
	SET @Id = SCOPE_IDENTITY()
			
END
