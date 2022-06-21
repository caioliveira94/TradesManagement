/****** Object:  Table [dbo].[ClientSector]    Script Date: 6/20/2022 11:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientSector](
	[Id] [int] NOT NULL,
	[Sector] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ClientSector] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TradeCategories]    Script Date: 6/20/2022 11:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TradeCategories](
	[CategoryName] [varchar](50) NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_TradeCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trades]    Script Date: 6/20/2022 11:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TradeValue] [decimal](18, 3) NOT NULL,
	[ClientSectorId] [int] NOT NULL,
	[TradeCategoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Trades]  WITH CHECK ADD  CONSTRAINT [FK_Trades_ClientSector] FOREIGN KEY([ClientSectorId])
REFERENCES [dbo].[ClientSector] ([Id])
GO
ALTER TABLE [dbo].[Trades] CHECK CONSTRAINT [FK_Trades_ClientSector]
GO
ALTER TABLE [dbo].[Trades]  WITH CHECK ADD  CONSTRAINT [FK_Trades_TradeCategories] FOREIGN KEY([TradeCategoryId])
REFERENCES [dbo].[TradeCategories] ([Id])
GO
ALTER TABLE [dbo].[Trades] CHECK CONSTRAINT [FK_Trades_TradeCategories]
GO
/****** Object:  StoredProcedure [dbo].[TradesCategorySET]    Script Date: 6/20/2022 11:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TradesCategorySET]
AS

--DECLARES
DECLARE @privateSector AS INT = (SELECT id FROM ClientSector WHERE Sector = 'PRIVATE');
DECLARE @publicSector AS INT = (SELECT id FROM ClientSector WHERE Sector = 'PUBLIC');

DECLARE @lowRisk AS INT = (SELECT id FROM TradeCategories WHERE CategoryName = 'LOWRISK');
DECLARE @MediumRisk AS INT = (SELECT id FROM TradeCategories WHERE CategoryName = 'MEDIUMRISK');
DECLARE @highRisk AS INT = (SELECT id FROM TradeCategories WHERE CategoryName = 'HIGHRISK');


--FOR BETTER PERFORMANCE WITH A LARGE TABLE
SELECT Id INTO #TradesUnCat FROM Trades WHERE TradeCategoryId IS NULL

--SET CATEGORY FOR UNCATEGORIZED TRADES
--LOW RISK
UPDATE t 
	SET t.TradeCategoryId = @lowRisk
	FROM
	#TradesUnCat tu
	INNER JOIN Trades t (NOLOCK) ON tu.Id = t.Id
 WHERE t.TradeValue <= 1000000.0 AND t.ClientSectorId = @publicSector --TO BE CONFIRMED WITH USER IF EQUALS 1M FOR PUBLIC SECTOR IS LOW RISK

--MEDIUM RISK
UPDATE t 
	SET t.TradeCategoryId = @MediumRisk
	FROM
	#TradesUnCat tu
	INNER JOIN Trades t (NOLOCK) ON tu.Id = t.Id
WHERE t.TradeValue > 1000000.0 AND t.ClientSectorId = @publicSector 
												OR t.TradeValue <= 1000000.0 AND t.ClientSectorId = @privateSector --TO BE CONFIRMED WITH USER

--HIGH RISK
UPDATE t 
	SET t.TradeCategoryId = @highRisk
	FROM
	#TradesUnCat tu
	INNER JOIN Trades t (NOLOCK) ON tu.Id = t.Id
WHERE t.TradeValue > 1000000.0 AND t.ClientSectorId = @privateSector


DROP TABLE #TradesUnCat
GO
