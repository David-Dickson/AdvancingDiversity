CREATE TABLE [dbo].[NewsletterTemplates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[PrimaryImage] [nvarchar](255) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_NewsletterTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[NewsletterTemplates] ADD  CONSTRAINT [DF_NewsletterTemplate_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]

ALTER TABLE [dbo].[NewsletterTemplates] ADD  CONSTRAINT [DF_NewsletterTemplate_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]

ALTER TABLE [dbo].[NewsletterTemplates]  WITH CHECK ADD  CONSTRAINT [FK_NewsletterTemplates_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])

ALTER TABLE [dbo].[NewsletterTemplates] CHECK CONSTRAINT [FK_NewsletterTemplates_Users]
