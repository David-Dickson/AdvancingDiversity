--===========================================================================================
-- Author: David Dickson
-- Create date: MM/DD/YEAR
-- Description: [dbo].[NewsletterSubscriptions_SelectAll_Subscribed] for dbo.NewsletterSubscriptions
-- Code Reviewer: Redacted

-- Modified by: author
-- Modified date: MM/DD/YEAR
-- Code Reviewer: Oluwatosin Ehindro
-- Note:
--============================================================================================



ALTER PROC [dbo].[NewsletterSubscriptions_Select_Subscribed]
		@IsSubscribed bit

AS

/*-----------TEST CODE---------------------------------------
			
	DECLARE @IsSubscribed bit = 'True'	

	EXECUTE dbo.NewsletterSubscriptions_Select_Subscribed 
		@IsSubscribed

-----------------------------------------------------------*/

BEGIN

	SELECT Id
		,Email
		,IsSubscribed
		,DateCreated
		,DateModified
					
	FROM dbo.NewsletterSubscriptions
	WHERE IsSubscribed = @IsSubscribed

END
