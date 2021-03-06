USE [vltn]
GO
/****** Object:  Table [dbo].[Close_tbl]    Script Date: 11/12/2014 21:24:20 ******/
DROP TABLE [dbo].[Close_tbl]
GO
/****** Object:  Table [dbo].[Close_tbl]    Script Date: 11/12/2014 21:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Close_tbl](
	[Close_Date] [date] NOT NULL,
	[Amount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Close_tbl] PRIMARY KEY CLUSTERED 
(
	[Close_Date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Close_tbl] ([Close_Date], [Amount]) VALUES (CAST(0x15390B00 AS Date), CAST(14000.00 AS Decimal(18, 2)))
