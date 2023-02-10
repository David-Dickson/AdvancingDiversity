-- =============================================
    -- Author: David Dickson
    -- Create date: MM/DD/YEAR
    -- Description: Delete for [dbo].[Newsletter_Delete_By_Id] given the Id of Newsletters
    -- Code Reviewer: Redacted 

    -- MODIFIED BY: Author
    -- MODIFIED DATE: MM/DD/YEAR
    -- Code Reviewer:
    -- Note:
-- ==============================================


ALTER PROC [dbo].[NewsLetters_SelectAll_Subscribed]
    		@PageIndex int
    		,@PageSize int
						
AS


/*-----------------TEST CODE-------------------

	DECLARE @PageIndex int = 0
		,@PageSize int = 3

	EXECUTE [dbo].[Newsletters_SelectAll] 
		@PageIndex
		,@PageSize

-----------------------------------------------*/


BEGIN

	DECLARE @offset int = @PageIndex * @PageSize

	SELECT Email
		,IsSubscribed as BitType
	  	,DateCreated
	  	,DateModified

	FROM [dbo].[NewsletterSubscriptions]
		WHERE IsSubscribed = 1

	ORDER BY Id
	
	OFFSET @offset ROWS
	FETCH NEXT @PageSize ROWS Only 

END
