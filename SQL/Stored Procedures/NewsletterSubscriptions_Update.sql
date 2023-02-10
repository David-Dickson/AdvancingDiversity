--===========================================================================================
-- Author: David Dickson
-- Create date: MM/DD/YEAR
-- Description: [dbo].[NewsletterSubscriptions_Update] for dbo.NewsletterSubscriptions
-- Code Reviewer: Redacted

-- Modified by: author
-- Modified date: MM/DD/YEAR
-- Code Reviewer: 
-- Note:
--============================================================================================



ALTER PROC [dbo].[NewsletterSubscriptions_Update]
		@Email nvarchar(255)
		,@IsSubscribed bit

AS

/*-----------TEST CODE-----------------
		
	DECLARE @Email nvarchar(255) = 'Test01@email.com'
		,@IsSubscribed bit = 0

	EXECUTE [dbo].[NewsletterSubscriptions_Update]
		@Email
		,@IsSubscribed

	SELECT *
	FROM [dbo].[NewsletterSubscriptions]
	WHERE @Email = Email
	
-------------------------------------*/

BEGIN 

	DECLARE @DateNow datetime2 = getutcdate()

	UPDATE [dbo].[NewsletterSubscriptions]
	SET	IsSubscribed = @IsSubscribed
		,DateModified = @DateNow
	WHERE @Email = Email
			
END
