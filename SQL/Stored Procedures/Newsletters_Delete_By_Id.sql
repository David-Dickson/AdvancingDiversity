-- =============================================
    -- Author: David Dickson
    -- Create date: MM/DD/YEAR
    -- Description: Delete for dbo.Newsletter_Delete_By_Id given the Id of Newsletters
    -- Code Reviewer: Redacted 

    -- MODIFIED BY: Author
    -- MODIFIED DATE: MM/DD/YEAR
    -- Code Reviewer:
    -- Note:
-- ==============================================

ALTER   PROC [dbo].[Newsletters_Delete_By_Id]
		@Id int


AS

/*---------TEST CODE---------
	DECLARE @Id int = 15

	EXECUTE [Newsletters_Delete_By_Id] @Id 

	SELECT * FROM dbo.Newsletters

---------END TEST CODE---------
*/




BEGIN


	DELETE FROM [dbo].[Newsletters]
		WHERE Id = @Id


END
