CREATE TABLE [dbo].[NewsletterSubscriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[IsSubscribed] [bit] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_NewsletterSubscriptions] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[NewsletterSubscriptions] ADD  CONSTRAINT [DF_NewsletterSubscriptions_IsSubscribed]  DEFAULT ((1)) FOR [IsSubscribed]

ALTER TABLE [dbo].[NewsletterSubscriptions] ADD  CONSTRAINT [DF_NewsletterSubscriptions_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]

ALTER TABLE [dbo].[NewsletterSubscriptions] ADD  CONSTRAINT [DF_NewsletterSubscriptions_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]
