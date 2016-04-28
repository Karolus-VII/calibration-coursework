CREATE TABLE [dbo].[DataSet] (
    [Time]                INT           IDENTITY (1, 1) NOT NULL,
    [Mass]                DECIMAL NULL,
    [Temperature_Liquid]  DECIMAL NULL,
    [Temperature_Ambient] DECIMAL NULL,
    [Humidity]            DECIMAL NULL,
    [Volume]              DECIMAL NULL,
    [Actual_Flow_Rate]    DECIMAL NULL,
    [Uncertainty]         DECIMAL NULL,
    PRIMARY KEY CLUSTERED ([Time] ASC)
);

